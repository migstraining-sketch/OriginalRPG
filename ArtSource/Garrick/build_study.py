"""Editable metre-scale outfit/proportion study. Face is provisional, not the approved sheet."""
import bpy, math, random, json
from pathlib import Path
from mathutils import Vector
random.seed(17)
OUT=Path(__file__).resolve().parent
bpy.ops.object.select_all(action='SELECT'); bpy.ops.object.delete(use_global=False)
for data in list(bpy.data.materials): bpy.data.materials.remove(data)
scene=bpy.context.scene
scene.unit_settings.system='METRIC'
body=bpy.data.collections.new('Garrick | editable proportion and outfit study'); scene.collection.children.link(body)
stage=bpy.data.collections.new('Presentation | not game geometry'); scene.collection.children.link(stage)
def mat(name,color,rough=.75,metal=0):
    m=bpy.data.materials.new(name);m.diffuse_color=(*color,1);m.use_nodes=True
    p=m.node_tree.nodes.get('Principled BSDF');p.inputs['Base Color'].default_value=(*color,1);p.inputs['Roughness'].default_value=rough;p.inputs['Metallic'].default_value=metal
    return m
skin=mat('Warm skin | provisional identity',(.46,.25,.15));shirt=mat('Unbleached work linen',(.57,.49,.36));vest=mat('Maintained charcoal-brown vest',(.065,.058,.047));seam=mat('Rubbed seams',(.18,.13,.082));pants=mat('Heavy dark trousers',(.085,.085,.073));leather=mat('Worn boot leather',(.08,.038,.023),.62);sole=mat('Boot sole',(.025,.022,.017));apron=mat('Oxblood work apron',(.16,.035,.026));towel=mat('Work cloth',(.62,.55,.42));hair=mat('Dark hair clumps',(.035,.023,.016));grey=mat('Beard grey accents',(.17,.135,.10));metal=mat('Dull brass hardware',(.33,.24,.11),.4,.7);eye=mat('Face study details',(.045,.026,.018));floor=mat('Studio slate',(.04,.054,.057))
def finish(o,name,material,collection=body,smooth=True):
    o.name=name
    for c in list(o.users_collection):c.objects.unlink(o)
    collection.objects.link(o)
    if material:o.data.materials.append(material)
    if o.type=='MESH' and smooth:
        for p in o.data.polygons:p.use_smooth=True
    return o
def ell(name,loc,scale,material,seg=24):
    bpy.ops.mesh.primitive_uv_sphere_add(segments=seg,ring_count=12,location=loc)
    o=bpy.context.object;o.scale=scale;bpy.ops.object.transform_apply(location=False,rotation=False,scale=True)
    return finish(o,name,material)
def box(name,loc,scale,material,bevel=.02,collection=body):
    bpy.ops.mesh.primitive_cube_add(size=1,location=loc);o=bpy.context.object;o.scale=scale;bpy.ops.object.transform_apply(location=False,rotation=False,scale=True)
    finish(o,name,material,collection,False)
    if bevel:m=o.modifiers.new('Soft worked edges','BEVEL');m.width=bevel;m.segments=3;o.modifiers.new('Corner normals','WEIGHTED_NORMAL')
    return o
def profile(name,rings,material,n=24):
    # rings = centre x,y,z and elliptical x/y radii; editable cloth/body topology.
    verts=[]
    for x,y,z,rx,ry in rings:
        verts.extend((x+rx*math.cos(i*2*math.pi/n),y+ry*math.sin(i*2*math.pi/n),z) for i in range(n))
    faces=[]
    for j in range(len(rings)-1):
        for i in range(n):a=j*n+i;b=j*n+(i+1)%n;faces.append((a,b,b+n,a+n))
    faces.extend([tuple(reversed(range(n))),tuple((len(rings)-1)*n+i for i in range(n))])
    mesh=bpy.data.meshes.new(name);mesh.from_pydata(verts,[],faces);mesh.update();o=bpy.data.objects.new(name,mesh);body.objects.link(o);o.data.materials.append(material)
    for p in mesh.polygons:p.use_smooth=True
    return o
def line(name,points,radius,material):
    curve=bpy.data.curves.new(name,'CURVE');curve.dimensions='3D';curve.bevel_depth=radius;curve.bevel_resolution=2
    spline=curve.splines.new('POLY');spline.points.add(len(points)-1)
    for p,co in zip(spline.points,points):p.co=(*co,1)
    o=bpy.data.objects.new(name,curve);body.objects.link(o);curve.materials.append(material);return o
def torus(name,loc,major,minor,material,rot=(math.pi/2,0,0)):
    bpy.ops.mesh.primitive_torus_add(major_radius=major,minor_radius=minor,major_segments=24,minor_segments=8,location=loc,rotation=rot)
    return finish(bpy.context.object,name,material)
# +Z up, front is -Y, a grounded relaxed A stance.
for side in [-1,1]:
    x=side*.155
    box('Layered boot sole', (x,-.065,.032),(.225,.37,.064),sole,.025)
    box('Heavy rounded toe', (x,-.085,.11),(.218,.34,.17),leather,.056)
    profile('Boot shaft',[(x,0,.1,.105,.115),(x,0,.28,.098,.10),(x,0,.37,.112,.105)],leather)
    line('Boot welt',[(x-.1,-.19,.079),(x+.1,-.19,.079)],.006,seam)
    profile('Trouser leg',[(x,0,.30,.091,.088),(x,0,.39,.12,.115),(x,0,.58,.114,.11),(x,0,.69,.14,.14),(side*.14,0,.91,.15,.17),(side*.12,0,1.03,.153,.175)],pants)
    for z in [.39,.45,.64]:line('Restrained trouser fold',[(x-.085,-.075,z),(x,-.123,z+.018),(x+.075,-.077,z+.009)],.009,pants)
profile('Shirt substantial core',[(0,0,.91,.27,.17),(0,0,1.08,.29,.19),(0,0,1.28,.32,.19),(0,0,1.43,.34,.175),(0,0,1.49,.24,.14)],shirt)
profile('Vest back and core',[(0,0,.955,.301,.207),(0,0,1.12,.327,.22),(0,0,1.32,.353,.219),(0,0,1.43,.356,.198),(0,0,1.485,.253,.164)],vest)
# Light open shirt neckline and two vest lapels (no armor/harness).
panel=box('Open shirt neckline',(0,-.219,1.40),(.12,.016,.17),shirt,.01)
for s in [-1,1]:
    o=box('Vest lapel',(s*.078,-.235,1.408),(.045,.016,.20),vest,.008);o.rotation_euler.y=s*-.27
    o=box('Turned linen collar',(s*.075,-.128,1.49),(.102,.055,.08),shirt,.016);o.rotation_euler.y=s*-.3
    ell('Work shoulder',(s*.315,0,1.41),(.119,.137,.119),shirt)
    profile('Loose short sleeve',[(s*.355,0,1.41,.118,.137),(s*.397,0,1.30,.117,.133),(s*.422,-.008,1.225,.118,.126)],shirt)
    profile('Rolled sleeve cuff',[(s*.415,-.008,1.24,.125,.134),(s*.425,-.008,1.19,.13,.135),(s*.433,-.008,1.175,.115,.12)],shirt)
    profile('Large work forearm',[(s*.431,-.004,1.20,.094,.105),(s*.457,-.013,1.11,.10,.098),(s*.48,-.028,1.01,.088,.085),(s*.493,-.05,.94,.067,.069)],skin)
    ell('Large palm',(s*.499,-.055,.894),(.076,.067,.104),skin)
    for i in range(4):
        o=ell('Finger study',(s*(.456+i*.028),-.069,.816+(abs(i-1.4))*.005),(.018,.035,.046),skin,16)
    ell('Thumb',(s*.427,-.084,.9),(.029,.041,.057),skin,16)
profile('Thick neck',[(0,0,1.46,.108,.098),(0,0,1.61,.107,.095)],skin)
profile('Head | provisional facial planes',[(0,-.02,1.56,.075,.079),(0,-.008,1.60,.113,.10),(0,0,1.70,.13,.113),(0,.004,1.79,.125,.112),(0,.006,1.85,.092,.086)],skin)
for s in [-1,1]:
    ell('Ear',(s*.126,.003,1.703),(.032,.025,.046),skin,16)
    ell('Brow plane',(s*.049,-.102,1.763),(.049,.019,.021),skin)
    ell('Eye placeholder',(s*.05,-.117,1.739),(.018,.009,.008),eye,16)
    line('Low relaxed brow',[(s*.019,-.123,1.765),(s*.075,-.109,1.771)],.008,hair)
ell('Nose planes',(0,-.122,1.707),(.026,.033,.042),skin)
ell('Beard jaw',(0,-.036,1.602),(.109,.09,.06),hair)
for s in [-1,1]:
    ell('Beard cheek',(s*.087,-.071,1.65),(.038,.043,.066),hair)
    ell('Moustache',(s*.028,-.118,1.666),(.037,.018,.017),hair)
    for i in range(4):
        line('Grey at beard edge',[(s*(.077+i*.004),-.091,1.641-i*.006),(s*(.07+i*.005),-.093,1.616-i*.006)],.002,grey)
line('Mouth placeholder',[(-.029,-.117,1.647),(0,-.123,1.644),(.029,-.117,1.649)],.004,eye)
ell('Hair crown',(0,.014,1.833),(.132,.12,.07),hair)
for i in range(13):
    a=i*math.tau/13;x=math.cos(a)*.097;y=.016+math.sin(a)*.077
    o=ell('Rugged hair clump',(x,y,1.836+random.uniform(-.02,.015)),(.052,.069,.055),hair,16);o.rotation_euler.z=a+.25
for s in [-1,1]:
    ell('Hair side',(s*.115,.029,1.77),(.035,.095,.084),hair)
    ell('Nape hair',(s*.067,.096,1.725),(.062,.025,.091),hair)
# Apron is a separate editable curved cloth mesh, no simulation dependency.
verts=[];faces=[];cols=12;rows=8
for r in range(rows):
    z=1.04-r*.062; width=.255-r*.003
    for c in range(cols):
        x=(c/(cols-1)*2-1)*width;y=-.20-.03*math.cos(x/width*math.pi/2)+.007*math.sin(c*1.7+r*.25)
        verts.append((x,y,z))
for r in range(rows-1):
    for c in range(cols-1):i=r*cols+c;faces.append((i,i+1,i+1+cols,i+cols))
mesh=bpy.data.meshes.new('Apron grid');mesh.from_pydata(verts,[],faces);mesh.update();o=bpy.data.objects.new('Oxblood work apron | separate cloth',mesh);body.objects.link(o);o.data.materials.append(apron);m=o.modifiers.new('Cloth thickness','SOLIDIFY');m.thickness=.008
profile('Apron waist tie',[(0,0,1.027,.287,.208),(0,0,1.063,.286,.207)],apron)
for z in [1.10,1.19,1.28]:ell('Vest button',(0,-.225,z),(.01,.008,.01),metal,12)
box('Folded work towel',(.214,-.221,.896),(.088,.027,.31),towel,.01)
for x in [.19,.21,.23]:line('Towel fold',[(x,-.24,1.03),(x+.007,-.247,.753)],.002,shirt)
torus('Inn key ring',(-.282,-.12,1.015),.025,.004,metal)
for i in range(3):
    x=-.29+i*.018;torus('Key bow',(x,-.145,.98),.009,.003,metal)
    box('Key shank',(x,-.145,.95),(.005,.006,.045),metal,.002)
    box('Key bit',(x+.006,-.145,.931),(.017,.007,.008),metal,.001)
# Two small repaired seams, not all-over grunge.
for s in [-1,1]:
    for i in range(5):line('Cuff repair stitch',[(s*(.44+i*.011),-.124,1.196),(s*(.443+i*.011),-.126,1.211)],.0018,seam)
# Reusable presentation rig.
box('Studio plinth',(0,0,-.046),(1.65,1.45,.08),floor,.05,stage)
box('Studio floor',(0,0,-.105),(200,200,.03),floor,0,stage)
world=bpy.data.worlds.new('Neutral studio');scene.world=world;world.use_nodes=True;world.node_tree.nodes['Background'].inputs[0].default_value=(.09,.11,.14,1);world.node_tree.nodes['Background'].inputs[1].default_value=.35
def aim(o,target):o.rotation_euler=(Vector(target)-o.location).to_track_quat('-Z','Y').to_euler()
for name,loc,power,size,col in [('Warm key',(-3,-4,5),450,4,(1,.83,.64)),('Soft fill',(3,-2,3),300,3,(.72,.82,1)),('Rim',(1,3,4),550,3,(1,.73,.49))]:
    d=bpy.data.lights.new(name,'AREA');d.energy=power;d.shape='DISK';d.size=size;d.color=col;o=bpy.data.objects.new(name,d);stage.objects.link(o);o.location=loc;aim(o,(0,0,1))
d=bpy.data.cameras.new('Study camera');cam=bpy.data.objects.new('Study camera',d);stage.objects.link(cam);scene.camera=cam;d.type='ORTHO';d.ortho_scale=2.3
scene.render.engine='CYCLES';scene.cycles.samples=32;scene.cycles.use_denoising=True
scene.render.resolution_x=1000;scene.render.resolution_y=1200;scene.render.resolution_percentage=100
scene.view_settings.view_transform='AgX'
scene['status']='PROPORTION / OUTFIT STUDY ONLY. Approved face sheet unavailable. Not final or rigged. Not installed in Unity.'
scene['authority']='origin/main 7aed489: docs/GARRICK_VISUAL_APPROVED_REFERENCE.md; owner requested Blender design work.'
scene['height_target_m']=1.90
for name,loc in [('three-quarter',(3,-6,3)),('front',(0,-6,1.45)),('back',(0,6,1.45))]:
    cam.location=loc;aim(cam,(0,0,.96));scene.render.filepath=str(OUT/(name+'.png'));bpy.ops.render.render(write_still=True)
cam.location=(3,-6,3);aim(cam,(0,0,.96))
bpy.ops.wm.save_as_mainfile(filepath=str(OUT/'Garrick_Outfit_Study_v01.blend'))
deps=bpy.context.evaluated_depsgraph_get();triangles=0
for o in body.objects:
    if o.type=='MESH':
        ev=o.evaluated_get(deps);m=ev.to_mesh();m.calc_loop_triangles();triangles+=len(m.loop_triangles);ev.to_mesh_clear()
(OUT/'verification.json').write_text(json.dumps({'status':'outfit and proportion study; unrigged; provisional face','objects':len(body.objects),'evaluated_triangles':triangles,'height_target_m':1.90,'blender':bpy.app.version_string},indent=2))
