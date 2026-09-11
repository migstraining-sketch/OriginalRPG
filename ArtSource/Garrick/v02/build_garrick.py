"""Reference-led Garrick surface study. Actual Blender geometry, not an image render substitute."""
import bpy, math, random, json
from pathlib import Path
from mathutils import Vector, Matrix
random.seed(49)
OUT=Path(__file__).resolve().parent
bpy.ops.object.select_all(action='SELECT');bpy.ops.object.delete(use_global=False)
scene=bpy.context.scene;scene.unit_settings.system='METRIC'
model=bpy.data.collections.new('GARRICK | v02 reference-led study');scene.collection.children.link(model)
stage=bpy.data.collections.new('STUDIO | cameras and lights');scene.collection.children.link(stage)
refs=bpy.data.collections.new('REFERENCES | supplied by owner');scene.collection.children.link(refs);refs.hide_render=True
def material(name,color,rough=.75,metal=0,grain=0):
    m=bpy.data.materials.new(name);m.diffuse_color=(*color,1);m.use_nodes=True;n=m.node_tree.nodes;l=m.node_tree.links;p=n.get('Principled BSDF');p.inputs['Base Color'].default_value=(*color,1);p.inputs['Roughness'].default_value=rough;p.inputs['Metallic'].default_value=metal
    if grain:
        noise=n.new('ShaderNodeTexNoise');noise.inputs['Scale'].default_value=grain;noise.inputs['Detail'].default_value=3;noise.inputs['Roughness'].default_value=.72
        ramp=n.new('ShaderNodeValToRGB');ramp.color_ramp.elements[0].position=.16;ramp.color_ramp.elements[0].color=(*(c*.47 for c in color),1);ramp.color_ramp.elements[1].position=.82;ramp.color_ramp.elements[1].color=(*(min(1,c*1.3) for c in color),1);l.new(noise.outputs['Fac'],ramp.inputs[0]);l.new(ramp.outputs[0],p.inputs['Base Color'])
        fine=n.new('ShaderNodeTexNoise');fine.inputs['Scale'].default_value=360;fine.inputs['Detail'].default_value=2;bump=n.new('ShaderNodeBump');bump.inputs['Strength'].default_value=.18;bump.inputs['Distance'].default_value=.0007;l.new(fine.outputs['Fac'],bump.inputs['Height']);l.new(bump.outputs[0],p.inputs['Normal'])
    return m
skin=material('Weathered warm skin',(.43,.235,.145),.7,grain=24)
crease=material('Warm skin creases',(.20,.087,.048));lip=material('Muted lips',(.30,.125,.091));shirt=material('Warm ivory linen',(.48,.404,.305),.9,grain=31)
cloth=material('Charcoal-brown work vest',(.042,.031,.024),.88,grain=27);trousers=material('Heavy charcoal twill',(.040,.036,.032),.9,grain=35)
leather=material('Dark rubbed leather',(.052,.026,.017),.66,grain=19);worn=material('Leather rubbed edges',(.18,.105,.052),.8,grain=55)
thread=material('Dull flax stitching',(.25,.174,.099));hair=material('Chestnut hair',(.048,.022,.012),.78,grain=9);hairlit=material('Warm hair ridges',(.102,.046,.025),.74,grain=25);beardmat=material('Close dark beard',(.052,.034,.024),.91,grain=72);grey=material('Salt in beard',(.17,.14,.108),.95)
white=material('Warm eye whites',(.50,.45,.36),.5);iris=material('Grey-hazel iris',(.13,.145,.123),.45);pupil=material('Pupils',(.008,.008,.006),.36)
brass=material('Aged brass',(.20,.12,.043),.45,.72,grain=31);iron=material('Blackened pewter',(.075,.082,.083),.38,.78,grain=24);towel=material('Cream bar towel',(.60,.50,.37),.92,grain=43);red=material('Faded oxblood towel bands',(.205,.055,.033),.9,grain=40);floor=material('Slate studio',(.027,.036,.038),.85)
def mesh(name,verts,faces,mat,smooth=True):
    d=bpy.data.meshes.new(name);d.from_pydata(verts,[],faces);d.update();o=bpy.data.objects.new(name,d);model.objects.link(o);d.materials.append(mat)
    for f in d.polygons:f.use_smooth=smooth
    return o
def move_collection(o,c):
    for old in list(o.users_collection):old.objects.unlink(o)
    c.objects.link(o)
def box(name,loc,size,mat,bevel=.003):
    bpy.ops.mesh.primitive_cube_add(size=1,location=loc);o=bpy.context.object;move_collection(o,model);o.name=name;o.scale=size;bpy.ops.object.transform_apply(location=False,rotation=False,scale=True);o.data.materials.append(mat)
    if bevel:m=o.modifiers.new('Worked edges','BEVEL');m.width=bevel;m.segments=2;o.modifiers.new('Surface normals','WEIGHTED_NORMAL')
    return o
def ell(name,loc,size,mat):
    bpy.ops.mesh.primitive_uv_sphere_add(segments=32,ring_count=20,location=loc);o=bpy.context.object;move_collection(o,model);o.name=name;o.scale=size;bpy.ops.object.transform_apply(location=False,rotation=False,scale=True);o.data.materials.append(mat)
    for f in o.data.polygons:f.use_smooth=True
    return o
def curve(name,pts,radius,mat):
    d=bpy.data.curves.new(name,'CURVE');d.dimensions='3D';d.resolution_u=16;d.bevel_depth=radius;d.bevel_resolution=2;s=d.splines.new('BEZIER');s.bezier_points.add(len(pts)-1)
    for p,co in zip(s.bezier_points,pts):p.co=co;p.handle_left_type='AUTO';p.handle_right_type='AUTO'
    o=bpy.data.objects.new(name,d);model.objects.link(o);d.materials.append(mat);return o
def loft(name,rings,mat,n=48,fold=0):
    # rings x,y,z, radius x, radius y. Cloth wrinkles are part of the surface.
    v=[];f=[]
    for j,(x,y,z,rx,ry) in enumerate(rings):
        for i in range(n):
            a=math.tau*i/n;wr=1+fold*(math.sin(a*8+j*.9)+.45*math.sin(a*13-j*1.7))
            v.append((x+math.cos(a)*rx*wr,y+math.sin(a)*ry*wr,z+fold*.05*math.sin(a*7+j)))
    for j in range(len(rings)-1):
        for i in range(n):a=j*n+i;b=j*n+(i+1)%n;f.append((a,b,b+n,a+n))
    f.extend([tuple(reversed(range(n))),tuple((len(rings)-1)*n+i for i in range(n))]);return mesh(name,v,f,mat)
def ribbon(name,pts,widths,depth,mat,n=10):
    # Oriented elliptical swept surface for tapered locks, tendons and folded seams.
    if len(pts)>=3:
        dense=[];sizes=[]
        for k in range(len(pts)-1):
            a=Vector(pts[max(0,k-1)]);b=Vector(pts[k]);c=Vector(pts[k+1]);d=Vector(pts[min(len(pts)-1,k+2)])
            for step in range(6):
                t=step/6;dense.append(.5*((2*b)+(-a+c)*t+(2*a-5*b+4*c-d)*t*t+(-a+3*b-3*c+d)*t*t*t));sizes.append(widths[k]*(1-t)+widths[k+1]*t)
        dense.append(Vector(pts[-1]));sizes.append(widths[-1]);pts=dense;widths=sizes
    v=[];f=[]
    for j,p in enumerate(pts):
        p=Vector(p);t=(Vector(pts[min(j+1,len(pts)-1)])-Vector(pts[max(0,j-1)])).normalized();right=t.cross(Vector((0,-1,0)))
        if right.length<.01:right=t.cross(Vector((1,0,0)))
        right.normalize();up=t.cross(right).normalized()
        for i in range(n):a=i*math.tau/n;v.append(p+right*math.cos(a)*widths[j]+up*math.sin(a)*widths[j]*depth)
    for j in range(len(pts)-1):
        for i in range(n):a=j*n+i;b=j*n+(i+1)%n;f.append((a,b,b+n,a+n))
    f.extend([tuple(reversed(range(n))),tuple((len(pts)-1)*n+i for i in range(n))]);return mesh(name,v,f,mat)
def torus(name,loc,r,thick,mat,rot=(math.pi/2,0,0)):
    bpy.ops.mesh.primitive_torus_add(major_segments=32,minor_segments=8,location=loc,major_radius=r,minor_radius=thick,rotation=rot);o=bpy.context.object;move_collection(o,model);o.name=name;o.data.materials.append(mat);return o
# Tailored torso: broad chest and substantial core without the toy's barrel cylinder.
loft('Shirt core',[(0,0,.96,.218,.131),(0,0,1.06,.232,.139),(0,0,1.19,.244,.14),(0,0,1.32,.266,.142),(0,.008,1.43,.282,.13),(0,.012,1.51,.277,.117),(0,.015,1.565,.167,.102)],shirt,fold=.015)
loft('Fitted dark waistcoat',[(0,0,.99,.227,.139),(0,0,1.055,.24,.15),(0,0,1.19,.254,.151),(0,0,1.32,.276,.154),(0,.008,1.43,.287,.142),(0,.01,1.505,.279,.128)],cloth)
# Real open-V shape covers neckline, pointed collar planes and fitted lapels.
mesh('Open linen V',[(0,-.155,1.32),(-.099,-.135,1.532),(0,-.116,1.55),(.099,-.135,1.532)],[(0,1,2,3)],shirt)
mesh('Open shirt neckline',[(0,-.162,1.445),(-.038,-.149,1.553),(.038,-.149,1.553)],[(0,1,2)],skin)
for s in [-1,1]:
    mesh('Waistcoat shoulder',[(s*.07,-.134,1.5),(s*.095,-.1,1.558),(s*.228,-.078,1.53),(s*.282,-.048,1.482),(s*.247,-.13,1.435)],[(0,1,2,3,4)],cloth)
    o=mesh('Turned shirt collar',[(s*.015,-.077,1.582),(s*.079,-.06,1.575),(s*.12,-.14,1.518),(s*.067,-.163,1.488),(s*.027,-.12,1.548)],[(0,1,2,3,4)],shirt);m=o.modifiers.new('Collar thickness','SOLIDIFY');m.thickness=.003
    curve('Vest V seam',[(s*.105,-.144,1.524),(s*.06,-.16,1.425),(s*.008,-.169,1.327)],.002,worn)
    curve('Waistcoat side seam',[(s*.23,-.085,1.02),(s*.251,-.092,1.22),(s*.257,-.09,1.405)],.0015,thread)
for z in [1.045,1.11,1.175,1.24,1.305]:ell('Vest brass button',(0,-.159,z),(.0058,.003,.0058),brass)
curve('Button placket',[(-.011,-.163,1.02),(-.011,-.162,1.31)],.0012,thread)
arm_parts={}
for s in [-1,1]:
    # Loose trousers with gathered hems and asymmetrical folds.
    x=s*.132
    loft('Trousers',[(x,0,.31,.071,.079),(x,0,.39,.10,.095),(x,-.004,.435,.121,.109),(x,.005,.50,.115,.107),(x,0,.56,.105,.099),(x,-.02,.625,.116,.108),(x,-.01,.71,.121,.122),(s*.12,0,.82,.132,.141),(s*.115,0,.94,.143,.15),(s*.108,0,1.02,.138,.151)],trousers,fold=.07)
    for j,z in enumerate([.405,.46,.55,.63,.73]):
        curve('Trouser folded ridge',[(x-.081,-.076,z+.014),(x-.025,-.114,z),(x+.058,-.094,z+.023)],.004,trousers)
    # Anatomically proportioned fitted boots, laced upper and leather cuff.
    box('Boot outsole',(x,-.057,.025),(.166,.287,.045),iron,.011)
    box('Boot heel',(x,.039,.034),(.143,.107,.057),leather,.006)
    loft('Boot vamp',[(x,-.06,.047,.077,.128),(x,-.067,.087,.079,.13),(x,-.044,.125,.077,.109),(x,-.008,.17,.067,.077),(x,0,.23,.065,.074),(x,0,.32,.069,.076),(x,0,.385,.074,.08)],leather)
    loft('Boot upper cuff',[(x,0,.356,.08,.085),(x,0,.39,.081,.087)],leather)
    curve('Toe cap seam',[(x-.06,-.112,.109),(x,-.131,.121),(x+.06,-.112,.109)],.0015,worn)
    curve('Leather welt',[(x-.07,-.145,.052),(x-.063,-.184,.052),(x+.063,-.184,.052),(x+.074,-.13,.052)],.0015,thread)
    for j in range(8):
        z=.145+j*.026;y=-.081 if z>.2 else -.105
        for a in [-1,1]:torus('Lace eyelet',(x+a*.025,y,z),.004,.0013,brass)
        curve('Crossed boot lace',[(x-.025,y-.003,z),(x+.025,y-.006,z+.02)],.0018,worn)
        curve('Crossed boot lace',[(x+.025,y-.003,z),(x-.025,y-.006,z+.02)],.0018,worn)
    box('Boot cuff strap',(x,-.087,.374),(.127,.012,.027),leather,.001)
    torus('Cuff buckle',(x+s*.048,-.097,.375),.013,.002,brass)
    # Continuous shirt sleeves: shoulder slope, soft elbow folds, rolled cuffs.
    loft('Rolled linen sleeve',[(s*.253,.006,1.518,.055,.093),(s*.295,.003,1.495,.076,.111),(s*.32,.001,1.443,.086,.108),(s*.345,-.002,1.367,.089,.098),(s*.366,-.013,1.30,.086,.097),(s*.374,-.017,1.259,.087,.096)],shirt,fold=.05)
    loft('Rolled cuff layers',[(s*.374,-.017,1.279,.091,.101),(s*.379,-.02,1.259,.092,.102),(s*.382,-.021,1.235,.087,.095)],shirt,fold=.018)
    curve('Sleeve elbow fold',[(s*.32,-.079,1.32),(s*.37,-.113,1.338),(s*.408,-.075,1.35)],.003,shirt)
    # Radius widens under the elbow, narrows into a real wrist.
    before_arm=set(model.objects)
    loft('Forearm continuous anatomy',[(s*.38,-.022,1.25,.063,.068),(s*.388,-.027,1.207,.073,.073),(s*.397,-.043,1.153,.069,.066),(s*.407,-.057,1.10,.056,.054),(s*.415,-.069,1.047,.043,.043),(s*.415,-.071,1.021,.04,.037)],skin,n=40)
    curve('Forearm tendon',[(s*.438,-.081,1.175),(s*.449,-.097,1.107),(s*.442,-.099,1.063)],.0028,skin)
    # Palms plus articulated curved fingers, rather than ball-ended mittens.
    loft('Hand palm',[(s*.415,-.072,1.031,.04,.036),(s*.418,-.083,.999,.047,.032),(s*.42,-.087,.965,.049,.03),(s*.422,-.092,.949,.046,.026)],skin,n=32)
    for j in range(4):
        x=s*(.384+j*.024);z=.954-abs(j-1.25)*.006;length=[.073,.083,.079,.062][j]
        pts=[(x,-.091,z),(x+s*.004,-.101,z-length*.43),(x+s*.005,-.114,z-length*.78),(x+s*.002,-.128,z-length)]
        ribbon('Articulated finger',pts,[.0105,.010,.0085,.006],1,skin,12)
        curve('Finger joint crease',[(x-.006,-.111,z-length*.43),(x+.006,-.111,z-length*.43)],.0007,crease)
        ell('Fingernail',(x+s*.002,-.133,z-length+.009),(.006,.0018,.008),shirt)
    ribbon('Opposed thumb',[(s*.383,-.09,1.004),(s*.365,-.117,.979),(s*.365,-.131,.949),(s*.378,-.143,.935)],[.019,.017,.013,.009],.8,skin,14)
    for j in range(3):curve('Hand tendons',[(s*(.394+j*.022),-.113,1.015),(s*(.394+j*.022),-.117,.975)],.0016,skin)
    arm_parts[s]=set(model.objects)-before_arm
# Thick neck and trapezius flow into shoulders, not a cylinder perched on top.
loft('Neck',[(0,.024,1.48,.097,.085),(0,.019,1.565,.078,.075),(0,.013,1.62,.067,.065),(0,.01,1.679,.067,.067)],skin)
for s in [-1,1]:
    curve('Neck sternomastoid',[(s*.062,-.002,1.666),(s*.047,-.064,1.603),(s*.015,-.074,1.565)],.006,skin)
# Head surface with authored front-depth fields for brow, cheek planes, muzzle and chin.
headrings=[(1.635,.041,.040),(1.65,.059,.053),(1.672,.075,.061),(1.701,.084,.067),(1.729,.086,.07),(1.759,.084,.076),(1.785,.085,.078),(1.811,.084,.081),(1.837,.079,.082),(1.858,.066,.073),(1.88,.043,.052),(1.886,.006,.013)]
def gauss(x,z,cx,cz,sx,sz):return math.exp(-((x-cx)/sx)**2-((z-cz)/sz)**2)
def facefront(x,z):
    d=.064
    d+=.014*gauss(x,z,0,1.662,.038,.019)
    d+=.016*gauss(x,z,0,1.713,.04,.018)
    for s in [-1,1]:
        d+=.024*gauss(x,z,s*.048,1.763,.025,.017)
        d+=.017*gauss(x,z,s*.04,1.804,.034,.012)
        d-=.006*gauss(x,z,s*.039,1.788,.022,.009)
    return d
v=[];f=[];n=96
for z,rx,ry in headrings:
    for i in range(n):
        a=i*math.tau/n;x=rx*math.cos(a);y=ry*math.sin(a)
        if y<0:y=-facefront(x,z)*(-math.sin(a))**.45
        v.append((x,y+.006,z))
for j in range(len(headrings)-1):
    for i in range(n):a=j*n+i;b=j*n+(i+1)%n;f.append((a,b,b+n,a+n))
f.extend([tuple(reversed(range(n))),tuple((len(headrings)-1)*n+i for i in range(n))]);head=mesh('Sculpted head planes',v,f,skin);sub=head.modifiers.new('Face surface refinement','SUBSURF');sub.levels=2
# Nose bridge, wings, philtrum and restrained mouth anatomy.
loft('Nose bridge and tip',[(0,-.082,1.73,.011,.01),(0,-.100,1.74,.017,.017),(0,-.101,1.752,.015,.020),(0,-.09,1.77,.011,.019),(0,-.074,1.793,.01,.01),(0,-.069,1.806,.01,.004)],skin,n=32)
for s in [-1,1]:
    ell('Nose wing',(s*.015,-.086,1.739),(.009,.015,.008),skin)
    ell('Nostril',(s*.012,-.095,1.734),(.0045,.005,.0025),crease)
    # Almond eyes and eyelid rims; the brow stays attached to the face.
    x=s*.039;z=1.786
    ell('Eye',(x,-.065,z),(.022,.017,.011),white)
    ell('Iris',(x,-.0813,z),(.007,.0015,.007),iris);ell('Pupil',(x,-.0827,z),(.003,.0009,.004),pupil)
    curve('Upper eyelid',[(x-.021,-.069,z),(x-.009,-.079,z+.007),(x+.01,-.078,z+.006),(x+.021,-.069,z+.001)],.0028,skin)
    curve('Lower eyelid',[(x-.021,-.069,z),(x,-.079,z-.007),(x+.021,-.069,z+.001)],.0022,skin)
    curve('Tired lower eye',[(x-.018,-.067,z-.014),(x,-.073,z-.016),(x+.018,-.063,z-.012)],.0011,crease)
    ribbon('Expressive brow',[(s*.014,-.076,1.804),(s*.033,-.085,1.807),(s*.056,-.078,1.81),(s*.067,-.062,1.805)],[.003,.004,.003,.0003],.45,hair,8)
    for j in range(3):curve('Temple expression crease',[(s*.061,-.059,1.78-j*.004),(s*.075,-.045,1.783-j*.006)],.00075,crease)
    curve('Nasolabial plane',[(s*.018,-.077,1.737),(s*.028,-.076,1.719),(s*.03,-.068,1.7)],.0012,crease)
    ell('Ear outer',(s*.088,.007,1.757),(.021,.017,.035),skin)
    ell('Ear concha',(s*.096,-.006,1.758),(.011,.006,.022),crease)
    curve('Ear helix',[(s*.089,-.011,1.735),(s*.106,-.005,1.754),(s*.106,-.004,1.778),(s*.094,-.007,1.787)],.0035,skin)
curve('Mouth line',[(-.026,-.076,1.709),(-.01,-.082,1.711),(0,-.083,1.709),(.012,-.081,1.711),(.026,-.076,1.712)],.0018,crease)
curve('Lower lip',[(-.02,-.077,1.705),(0,-.083,1.702),(.02,-.077,1.707)],.0026,lip)
# Close beard field follows cheek/jaw. Fine tapered hair bundles break its silhouette.
for s in [-1,1]:
    for row in range(11):
        z=1.65+row*.009
        for col in range(13):
            x=s*(.007+col*.006)
            if abs(x)>.071-(max(0,1.68-z)*.65):continue
            if z>1.706 and abs(x)<.031:continue
            if z>1.726 and abs(x)<.059:continue
            y=.006-facefront(x,z)*max(.25,1-(x/.094)**2)**.25-.002
            length=random.uniform(.011,.023)
            ribbon('Beard strand',( (x,y,z),(x+s*.001,y-.003,z-length*.5),(x+s*.004,y+.002,z-length)),[.0036,.003,.0001],.4,grey if random.random()<.13 else beardmat,6)
    for j in range(10):
        x=s*(.003+j*.0027);z=1.727-abs(x)*.2;y=-.087+abs(x)*.25
        ribbon('Moustache sweep',[(x,y,z),(x+s*.006,y-.003,z-.005),(x+s*.011,y,z-.008)],[.0025,.002,.0001],.5,hair,6)
# Fitted scalp plus pointed, swept locks. No beads or spherical hair pieces.
loft('Hair underlayer',[(0,.05,1.722,.025,.018),(0,.05,1.756,.069,.043),(0,.024,1.83,.087,.081),(0,.023,1.875,.082,.078),(0,.018,1.902,.043,.049),(0,.01,1.908,.002,.004)],hair,n=48)
for i in range(17):
    t=i/16;back=t*.075;drop=t*.026
    lift=random.uniform(-.014,.016);sweep=random.uniform(-.014,.012)
    pts=[(.038+sweep,.005+back,1.883-drop),(.009+sweep,-.013+back,1.914-drop+lift),(-.056+sweep,-.06+back,1.885-drop+lift),(-.092+sweep,-.065+back,1.84-drop),(-.094+random.uniform(-.014,.008),-.055+back,1.797-drop+random.uniform(-.012,.02))]
    widths=[.001,.006,.009,.004,.0001];ribbon('Swept crown lock',pts,widths,.65,hairlit if i%4==0 else hair,10)
    if i%2==0:curve('Hair strand ridge',[(x,y-.002,z+.001) for x,y,z in pts[1:4]],.0006,hairlit)
for i in range(8):
    t=i/7;pts=[(.037,.014+t*.05,1.888),(.076,-.002+t*.06,1.895-t*.015),(.091,-.017+t*.063,1.857-t*.025),(.095,-.009+t*.065,1.816-t*.024),(.101,.003+t*.069,1.799-t*.033)]
    pts=[(x+random.uniform(-.004,.004),y,z+random.uniform(-.008,.008)) for x,y,z in pts]
    ribbon('Right parted lock',pts,[.001,.006,.008,.004,.0001],.55,hairlit if i%3==0 else hair,10)
for s in [-1,1]:
    for i in range(10):
        a=i/9;pts=[(s*.065,.025+a*.043,1.877-a*.025),(s*.091,.04+a*.044,1.844-a*.02),(s*.096,.049+a*.044,1.797-a*.025),(s*.084,.061+a*.034,1.749-a*.02),(s*.087,.057+a*.032,1.738-a*.02)]
        pts=[(x+random.uniform(-.004,.004),y,z+random.uniform(-.009,.009)) for x,y,z in pts]
        ribbon('Side and nape lock',pts,[.002,.007,.008,.004,.0001],.6,hairlit if i%4==0 else hair,10)
# Dark leather apron matches the sheet. Oxblood belongs on the towel trim.
verts=[];faces=[];cols=41;rows=31
for j in range(rows):
    t=j/(rows-1);z=1.055-t*.46;w=.225-t*.008
    for i in range(cols):
        x=(i/(cols-1)*2-1)*w;y=-.157-.032*math.cos(x/w*math.pi/2)+t*(.012*math.sin(i*.43)+.009*math.sin(i*.21+1))
        verts.append((x,y,z+.004*math.sin(i*.3)*t))
for j in range(rows-1):
    for i in range(cols-1):a=j*cols+i;faces.append((a,a+1,a+cols+1,a+cols))
apron=mesh('Curved work apron',verts,faces,leather);m=apron.modifiers.new('Leather thickness','SOLIDIFY');m.thickness=.004
for s in [-1,1]:curve('Apron worn border',[(s*.214,-.17,1.035),(s*.207,-.166,.81),(s*.207,-.16,.61)],.002,worn)
curve('Apron lower border',[(-.207,-.16,.61),(-.10,-.178,.611),(0,-.176,.61),(.11,-.161,.61),(.207,-.16,.61)],.002,worn)
# Sparse scratches restricted to apron and contact areas, no blanket noise dirt.
for i in range(95):
    x=random.uniform(-.198,.198);z=random.uniform(.625,1.027);t=(1.055-z)/.46;ix=(x/(.225-t*.008)+1)*20;y=-.157-.032*math.cos(x/(.225-t*.008)*math.pi/2)+t*(.012*math.sin(ix*.43)+.009*math.sin(ix*.21+1))-.002
    curve('Leather contact scratch',[(x,y,z),(x+random.uniform(.002,.009),y-.0005,z+random.uniform(.002,.008))],random.uniform(.0003,.0006),worn)
loft('Work belt',[(0,0,1.035,.244,.16),(0,0,1.076,.244,.16)],leather,n=72)
for x,z,sx,sz in [(-.028,1.055,.004,.034),(.028,1.055,.004,.034),(0,1.072,.06,.004),(0,1.038,.06,.004)]:box('Rectangular brass buckle',(x,-.166,z),(sx,.005,sz),brass,.001)
box('Buckle pin',(0,-.17,1.055),(.049,.003,.003),brass,.001)
for x in [.069,.099,.129]:ell('Belt hole',(x,-.153,1.056),(.002,.002,.002),pupil)
# Soft doubled towel hangs in offset folds, narrow woven red bands.
vv=[];ff=[];nx=18;ny=34
for j in range(ny):
    t=j/(ny-1)
    for i in range(nx):
        u=i/(nx-1);vv.append((.135+(u-.5)*.10,-.18-.012*math.sin(u*math.pi*5)-.025*math.sin(t*math.pi),1.064-t*.425+.01*math.sin(u*math.pi)*t))
for j in range(ny-1):
    for i in range(nx-1):a=j*nx+i;ff.append((a,a+1,a+nx+1,a+nx))
o=mesh('Draped bar towel',vv,ff,towel);o.data.materials.append(red)
for p in o.data.polygons:
    j=p.index//(nx-1)
    if j in [24,25,28]:p.material_index=1
m=o.modifiers.new('Towel thickness','SOLIDIFY');m.thickness=.0015
torus('Inn key ring',(-.228,-.14,1.025),.021,.0025,brass)
for i in range(4):
    x=-.246+i*.012;y=-.15-i*.002;z=.99-random.random()*.018
    torus('Key bow',(x,y,z),.007,.002,brass);box('Key stem',(x,y,z-.025),(.003,.004,.04),brass,.0005);box('Key bit',(x+.005,y,z-.041),(.014,.006,.007),brass,.0005)
# Painted facial albedo carries the supplied sheet's illustrated feature language.
# Mesh UVs are a first-pass front projection, not a final animation UV layout.
faceimage=bpy.data.images.load(str(OUT/'Garrick_Face_Albedo.png'));faceimage.pack()
paint=material('Reference-matched painted face',(.43,.235,.145),.89)
node=paint.node_tree.nodes.new('ShaderNodeTexImage');node.image=faceimage;node.extension='EXTEND';paint.node_tree.links.new(node.outputs['Color'],paint.node_tree.nodes['Principled BSDF'].inputs['Base Color'])
anchors=[(1.635,0),(1.665,.07),(1.707,.284),(1.74,.416),(1.786,.645),(1.809,.73),(1.843,.84),(1.886,1)]
def face_v(z):
    for (a,va),(b,vb) in zip(anchors,anchors[1:]):
        if z<=b:return va+(vb-va)*(z-a)/(b-a)
    return 1
facenames=('Sculpted head planes','Nose bridge','Nose wing')
hide=('Beard strand','Moustache sweep','Expressive brow','Tired lower eye','Temple expression crease','Nasolabial plane','Mouth line','Lower lip','Nostril','Upper eyelid','Lower eyelid','Eye','Iris','Pupil')
for o in list(model.objects):
    if o.name.startswith(hide):o.hide_render=True;o.hide_viewport=True
    if o.type=='MESH' and o.name.startswith(facenames):
        bpy.context.view_layer.update()
        o.data.materials.clear();o.data.materials.append(paint);uv=o.data.uv_layers.new(name='Face projection study')
        for poly in o.data.polygons:
            for li in poly.loop_indices:
                p=o.matrix_world@o.data.vertices[o.data.loops[li].vertex_index].co;uv.data[li].uv=(.5+p.x/.231,face_v(p.z))
        if o.name=='Sculpted head planes':
            o.data.materials.append(skin)
            for poly in o.data.polygons:
                if poly.center.y>.009:poly.material_index=1
# Broader reference head silhouette, still using the same painted registration.
bpy.context.view_layer.update()
for o in list(model.objects):
    if o.type in {'MESH','CURVE'}:
        coords=[v.co for v in o.data.vertices] if o.type=='MESH' else [p.co for s in o.data.splines for p in s.bezier_points]
        if not coords:continue
        low=min((o.matrix_world@v).z for v in coords)
        if low>1.63:o.matrix_world=Matrix.Translation((0,0,-.025))@Matrix.Diagonal((1.10,1,1,1))@o.matrix_world
# Reference images are packed into blend and visible as viewport reference empties.
for i,name in enumerate(['Garrick-Reference.png','Opening-Cast-Lineup.png']):
    image=bpy.data.images.load(str(OUT.parent/'References'/name),check_existing=True);image.pack();o=bpy.data.objects.new('Owner reference | '+name,None);refs.objects.link(o);o.empty_display_type='IMAGE';o.data=image;o.empty_display_size=2.5;o.location=(3+i*3,1,1.15);o.rotation_euler=(math.pi/2,0,0)
# Normalize study height within written envelope; retain source references as proportion authority.
bpy.context.view_layer.update();deps=bpy.context.evaluated_depsgraph_get();points=[]
for o in model.objects:
    if o.type in {'MESH','CURVE'}:
        ev=o.evaluated_get(deps);surface=ev.to_mesh();points.extend(ev.matrix_world@p.co for p in surface.vertices);ev.to_mesh_clear()
top=max(p.z for p in points);bottom=min(p.z for p in points);factor=1.9/(top-bottom)
rig=bpy.data.objects.new('Garrick | metre-scale root',None);model.objects.link(rig)
for o in list(model.objects):
    if o!=rig:o.parent=rig
rig.scale=(factor,)*3;rig.location.z=-bottom*factor
# Warm/cool studio lighting; preserves visible material values and detailed silhouette.
o=box('Studio floor',(0,0,-.035),(200,200,.05),floor,0);move_collection(o,stage)
world=bpy.data.worlds.new('Studio');scene.world=world;world.use_nodes=True;world.node_tree.nodes['Background'].inputs[0].default_value=(.065,.077,.091,1);world.node_tree.nodes['Background'].inputs[1].default_value=.4
def aim(o,target):o.rotation_euler=(Vector(target)-o.location).to_track_quat('-Z','Y').to_euler()
for name,loc,power,size,color in [('Warm key',(-3,-4,4),430,3,(1,.81,.64)),('Cool fill',(3,-2,2.5),160,2.5,(.67,.78,1)),('Copper rim',(1,2.5,3),500,2,(1,.64,.36))]:
    d=bpy.data.lights.new(name,'AREA');d.energy=power;d.shape='DISK';d.size=size;d.color=color;o=bpy.data.objects.new(name,d);stage.objects.link(o);o.location=loc;aim(o,(0,0,1))
d=bpy.data.cameras.new('Garrick presentation');cam=bpy.data.objects.new('Garrick presentation',d);stage.objects.link(cam);scene.camera=cam;d.type='ORTHO'
scene.render.engine='CYCLES';scene.cycles.samples=48;scene.cycles.use_denoising=True;scene.view_settings.view_transform='AgX';scene.render.resolution_percentage=100
scene['status']='Reference-led surface study, not a final rigged asset. Exact face/style acceptance remains with owner.'
scene['reference']='Owner-supplied Garrick turnaround + opening cast lineup, packed in this file. Prior toy blockout rejected.'
for name,loc,target,size,w,h in [('three-quarter',(2.4,-6,2.1),(0,0,.98),2.15,1100,1400),('portrait',(.7,-4,1.94),(0,0,1.68),.67,1100,1100),('front',(0,-6,1.1),(0,0,.97),2.12,1000,1400),('back',(0,6,1.3),(0,0,.97),2.12,1000,1400)]:
    cam.location=loc;aim(cam,target);d.ortho_scale=size;scene.render.resolution_x=w;scene.render.resolution_y=h;scene.render.filepath=str(OUT/(name+'.png'));bpy.ops.render.render(write_still=True)
cam.location=(2.4,-6,2.1);aim(cam,(0,0,.98));d.ortho_scale=2.15;scene.render.resolution_x=1100;scene.render.resolution_y=1400
for a in bpy.context.screen.areas:
    if a.type=='VIEW_3D':a.spaces.active.region_3d.view_perspective='CAMERA'
bpy.ops.wm.save_as_mainfile(filepath=str(OUT/'Garrick_Reference_Study_v02.blend'))
(OUT/'verification.json').write_text(json.dumps({'blender':bpy.app.version_string,'height_m':1.9,'objects':len(model.objects),'packed_reference_images':2,'rigged':False,'render_source':'actual scene geometry in Cycles'},indent=2))
