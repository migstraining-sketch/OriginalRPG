"""Two-character reuse proof. Adapt existing CC0 geometry; never overwrite the source candidate."""
import bpy, bmesh, math, json
from pathlib import Path
from mathutils import Vector
from mathutils.bvhtree import BVHTree
from mathutils.kdtree import KDTree

OUT = Path(__file__).resolve().parent
ART = OUT.parents[1]
bpy.ops.wm.open_mainfile(filepath=str(ART/'Garrick/Rigged-v01/Garrick_Rigged_Candidate.blend'))
scene = bpy.context.scene
scene.render.resolution_x = 1400
scene.render.resolution_y = 1050
scene.cycles.samples = 40
rig = next(o for o in scene.objects if o.type == 'ARMATURE')
rig.name = 'Garrick | shared humanoid rig'
scene.frame_set(1)
collection = bpy.data.collections.new('01 Garrick | layered workwear')
scene.collection.children.link(collection)
def move_collection(o, coll):
    for c in list(o.users_collection): c.objects.unlink(o)
    coll.objects.link(o)
move_collection(rig, collection)
for o in list(rig.children): move_collection(o, collection)

def components(mesh):
    adj = {v.index: set() for v in mesh.vertices}
    for e in mesh.edges:
        a,b = e.vertices; adj[a].add(b); adj[b].add(a)
    unseen=set(adj); result=[]
    while unseen:
        stack=[unseen.pop()]; ids=[]
        while stack:
            a=stack.pop(); ids.append(a)
            for b in adj[a]:
                if b in unseen: unseen.remove(b); stack.append(b)
        result.append(ids)
    return result

def retain_components(o, predicate):
    keep=set()
    for ids in components(o.data):
        coords=[o.matrix_world@o.data.vertices[i].co for i in ids]
        if predicate(ids,coords): keep.update(ids)
    bm=bmesh.new(); bm.from_mesh(o.data); bm.verts.ensure_lookup_table()
    bmesh.ops.delete(bm, geom=[v for v in bm.verts if v.index not in keep], context='VERTS')
    bm.to_mesh(o.data); bm.free()

def material(name, color, rough=.8, variation=.08):
    m=bpy.data.materials.new(name); m.use_nodes=True; m.diffuse_color=(*color,1)
    n=m.node_tree.nodes; l=m.node_tree.links; p=n.get('Principled BSDF')
    p.inputs['Roughness'].default_value=rough; p.inputs['Specular IOR Level'].default_value=.23
    tex=n.new('ShaderNodeTexNoise'); tex.inputs['Scale'].default_value=35; tex.inputs['Detail'].default_value=2
    ramp=n.new('ShaderNodeValToRGB')
    ramp.color_ramp.elements[0].color=(*(c*(1-variation) for c in color),1)
    ramp.color_ramp.elements[1].color=(*(c*(1+variation) for c in color),1)
    l.new(tex.outputs['Fac'],ramp.inputs[0]); l.new(ramp.outputs[0],p.inputs['Base Color'])
    return m

shirtmat=material('Work shirt | warm linen',(.65,.55,.40),.92,.06)
vestmat=material('Vest | charcoal brown wool',(.028,.021,.017),.93,.12)
leather=material('Apron | maintained dark leather',(.061,.031,.018),.76,.13)
edge=material('Seams | rubbed leather edges',(.115,.062,.030),.82,.06)
brass=material('Hardware | dull brass',(.24,.15,.061),.46,.04)
bootmat=material('Boots | dark work leather',(.045,.027,.017),.74,.14)
shirt=bpy.data.objects['Male_Peasant_Body']; shirt.name='Shirt | torso and upper sleeves'
# Retain front/back cloth, remove separate toggles, shoulder tabs and source belts.
retain_components(shirt, lambda ids,vs:len(ids)>400)
# Remove the overhanging short-sleeve flap; the independent sleeve mesh remains.
bm=bmesh.new();bm.from_mesh(shirt.data)
for sign in [-1,1]:
    point=shirt.matrix_world.inverted()@Vector((sign*.272,0,0))
    bmesh.ops.bisect_plane(bm,geom=list(bm.verts)+list(bm.edges)+list(bm.faces),dist=.00001,plane_co=point,plane_no=Vector((sign,0,0)),clear_outer=True)
bm.to_mesh(shirt.data);bm.free()
shirt.data.materials.clear(); shirt.data.materials.append(shirtmat)
for f in shirt.data.polygons:f.material_index=0
arms=bpy.data.objects['Male_Peasant_Arms']; arms.name='Shirt | rolled sleeves and hands'
for i,m in enumerate(arms.data.materials):
    if 'Peasant' in m.name: arms.data.materials[i]=shirtmat
feet=bpy.data.objects['Male_Peasant_Feet']; feet.name='Boots | work pair'
retain_components(feet,lambda ids,vs: not (len(ids) in (138,85,8) and min(v.z for v in vs)>.34))
feet.data.materials.clear(); feet.data.materials.append(bootmat)
for f in feet.data.polygons:f.material_index=0
bpy.data.objects['Male_Peasant_Legs'].name='Trousers | gathered work pair'

# Query clean rest-pose cloth for garment fitting and copy its deformation weights.
verts=[shirt.matrix_world@v.co for v in shirt.data.vertices]
# The source has intentional toggle holes. A closed fitting shell stops those
# holes from imprinting into a new garment, while keeping source body volume.
hull=bmesh.new()
for v in verts:
    if abs(v.x)<.25 and v.z>1.065:hull.verts.new(v)
bmesh.ops.convex_hull(hull,input=list(hull.verts),use_existing_faces=False)
hull.verts.index_update();hull.verts.ensure_lookup_table()
bvh=BVHTree.FromBMesh(hull);hull.free()
kd=KDTree(len(verts))
for i,v in enumerate(verts):kd.insert(v,i)
kd.balance()
weights=[{shirt.vertex_groups[g.group].name:g.weight for g in v.groups} for v in shirt.data.vertices]

def surface(x,z,front=True,offset=.009):
    loc,normal,index,distance=bvh.ray_cast(Vector((x,-1 if front else 1,z)),Vector((0,1 if front else -1,0)))
    if loc is None:
        return (-.11 if front else .15) + (-offset if front else offset)
    return loc.y + (-offset if front else offset)

def skin(o, mode='surface'):
    o.parent=rig
    for v in o.data.vertices:
        pos=o.matrix_world@v.co
        if mode=='head': w={'Head':1.0}
        elif mode=='hips': w={'pelvis':1.0}
        else:
            w={};near=kd.find_n(pos,3);total=sum(1/max(d,.002)**2 for _,_,d in near)
            for _,idx,d in near:
                factor=1/max(d,.002)**2/total
                for name,weight in weights[idx].items():w[name]=w.get(name,0)+weight*factor
        total=sum(w.values())
        for name,value in w.items():
            vg=o.vertex_groups.get(name) or o.vertex_groups.new(name=name)
            vg.add([v.index],value/total,'REPLACE')
    arm=o.modifiers.new('Shared skeleton deformation','ARMATURE');arm.object=rig

def mesh_object(name,vs,fs,mat,mode='surface',thick=.003):
    data=bpy.data.meshes.new(name);data.from_pydata(vs,[],fs);data.update()
    o=bpy.data.objects.new(name,data);collection.objects.link(o);data.materials.append(mat)
    for p in data.polygons:p.use_smooth=True
    skin(o,mode)
    if thick:
        sol=o.modifiers.new('Garment thickness','SOLIDIFY');sol.thickness=thick
    o['foundation_module']=name; o.asset_mark()
    return o

def grid(name,point,nx,nz,mat,mode='surface',thick=.003):
    vs=[point(i/nx,j/nz) for j in range(nz+1) for i in range(nx+1)]
    fs=[]
    for j in range(nz):
        for i in range(nx):
            a=j*(nx+1)+i;fs.append((a,a+1,a+nx+2,a+nx+1))
    return mesh_object(name,vs,fs,mat,mode,thick)

# New continuous shirt facing covers the removed tunic fastener apertures.
grid('Shirt | plain front facing',lambda u,t:((u-.5)*.105,surface((u-.5)*.105,1.13+t*.479,True,.004),1.13+t*.479),10,30,shirtmat)

def width(z):
    # Wide across the core, shaped away from the sleeve opening at the shoulder.
    if z<1.4:return .205
    return .205-(z-1.4)*.20

def front_point(sign,u,t):
    z=1.10+t*.502
    inner=.007+max(0,z-1.40)*.44
    x=sign*(inner+(width(z)-inner)*u)
    return (x,surface(x,z,True,.013),z)

left=grid('Vest | left front',lambda u,t:front_point(-1,u,t),12,32,vestmat)
right=grid('Vest | right front',lambda u,t:front_point(1,u,t),12,32,vestmat)
back=grid('Vest | back',lambda u,t:((u-.5)*2*width(1.10+t*.502),surface((u-.5)*2*width(1.10+t*.502),1.10+t*.502,False,.013),1.10+t*.502),24,32,vestmat)
for sign in [-1,1]:
    def side(u,t,sign=sign):
        z=1.10+t*.36;x=sign*width(z)
        yf=surface(x,z,True,.013);yb=surface(x,z,False,.013)
        return (x+sign*.008*math.sin(math.pi*u),yf+(yb-yf)*u,z)
    grid('Vest | side '+str(sign),side,8,24,vestmat)
    # True separate shoulder bridge, leaving room around the neck and arms.
    grid('Vest | shoulder '+str(sign),lambda u,t,s=sign:(s*(.097+u*.067),-.038+t*.153,1.604+.012*math.sin(math.pi*t)),8,10,vestmat)
    # Folded collar over the work shirt's existing neck opening.
    vs=[(sign*.016,-.049,1.618),(sign*.085,-.024,1.622),(sign*.113,-.097,1.578),(sign*.068,-.133,1.545),(sign*.037,-.105,1.588)]
    mesh_object('Shirt | folded collar '+str(sign),vs,[(0,1,4),(1,2,4),(2,3,4)],shirtmat)

# Replace waist-only panel with a removable bib-apron assembly.
old=bpy.data.objects.get('Garrick practical waist apron')
if old:bpy.data.objects.remove(old,do_unlink=True)
def bib_point(u,t):
    z=1.11+t*.385;w=.41-(t*.17);x=(u-.5)*w
    return (x,surface(x,z,True,.027)-.002*math.sin(u*math.pi*5),z)
grid('Apron | bib',bib_point,18,26,leather)
def skirt_point(u,t):
    z=1.113-t*.60;w=.41+t*.09;x=(u-.5)*w
    y=-.205+(2*u-1)**2*.046+.007*math.sin(u*math.pi*6)*t
    return (x,y,z+.007*math.sin(u*math.pi*3)*t)
grid('Apron | skirt',skirt_point,24,30,leather,'hips',.004)
for sign in [-1,1]:
    grid('Apron | shoulder strap '+str(sign),lambda u,t,s=sign:(s*.110+(u-.5)*.025,surface(s*.110,1.475+t*.129,True,.034),1.475+t*.129),3,18,leather)
    grid('Apron | strap over shoulder '+str(sign),lambda u,t,s=sign:(s*.110+(u-.5)*.025,-.040+t*.178,1.619+.012*math.sin(t*math.pi)),3,16,leather)
    grid('Apron | rear strap '+str(sign),lambda u,t,s=sign:(s*(.110*(1-t)+.18*t)+(u-.5)*.023,surface(s*(.110*(1-t)+.18*t),1.6-.48*t,False,.026),1.6-.48*t),3,25,leather)

# Belt and seams are geometry, not painted masks; each remains an independent module.
grid('Belt | plain work belt',lambda u,t:(.216*math.sin(u*math.tau),.035-.219*math.cos(u*math.tau),1.106+t*.044),80,3,leather,'hips')
for sign in [-1,1]:
    grid('Apron | stitched side '+str(sign),lambda u,t,s=sign:(s*(.205+t*.045)+(u-.5)*.004,-.164+.007*math.sin((1 if s>0 else 0)*math.pi*6)*t,1.113-t*.60),2,30,edge,'hips',.001)

# Fix loose prop shafts: every accessory follows the hips, not just the object root.
for o in list(rig.children):
    if o.name.startswith('Inn key') and not any(m.type=='ARMATURE' for m in o.modifiers):skin(o,'hips')
    if o.type=='MESH' and 'foundation_module' not in o:o['foundation_module']=o.name

# Preserve a younger material variant before Garrick-specific age treatment.
head=bpy.data.objects['Garrick — head and neck'];head.name='Head | shared base with Garrick age treatment'
young_materials=list(head.data.materials)
def color_attribute(o,name,fn):
    a=o.data.color_attributes.new(name=name,type='FLOAT_COLOR',domain='POINT')
    for v in o.data.vertices:a.data[v.index].color=(*fn(o.matrix_world@v.co),1)
    return name

def age_color(v):
    # Broad restrained under-eye/cheek treatment, never photographic wrinkles.
    under=math.exp(-((abs(v.x)-.045)/.026)**2-((v.z-1.750)/.011)**2-((v.y+.082)/.028)**2)
    cheek=math.exp(-((abs(v.x)-.067)/.021)**2-((v.z-1.730)/.025)**2-((v.y+.065)/.04)**2)
    return (1-.11*under,1-.20*under-.04*cheek,1-.22*under-.04*cheek)
color_attribute(head,'age_tone',age_color)
for i,m in enumerate(list(head.data.materials)):
    m=m.copy();head.data.materials[i]=m;m.name='Garrick | mature skin '+str(i)
    n=m.node_tree.nodes;l=m.node_tree.links;p=n.get('Principled BSDF')
    if not p or not p.inputs['Base Color'].is_linked:continue
    source=p.inputs['Base Color'].links[0].from_socket
    attr=n.new('ShaderNodeVertexColor');attr.layer_name='age_tone'
    mix=n.new('ShaderNodeMixRGB');mix.blend_type='MULTIPLY';mix.inputs[0].default_value=1
    l.new(source,mix.inputs[1]);l.new(attr.outputs['Color'],mix.inputs[2]);l.new(mix.outputs[0],p.inputs['Base Color'])

for name in ['Hair_SimpleParted','Hair_Beard']:
    o=bpy.data.objects[name];beard='Beard' in name
    # Retain recognizable source clumps, extend the back/temples and break beard symmetry.
    inv=o.matrix_world.inverted()
    for vert in o.data.vertices:
        v=o.matrix_world@vert.co
        if beard:
            lower=max(0,(1.72-v.z)/.10)
            v.z-=.009*lower*(.6+.4*math.sin(v.x*90));v.x*=1+.025*lower
        elif v.y>.025:
            v.z-=.026*max(0,(1.85-v.z)/.12);v.y+=.010
        vert.co=inv@v
    def hair_color(v,beard=beard):
        gray=(max(0,min(1,(1.725-v.z)/.09))*.68 if beard else max(0,min(1,(abs(v.x)-.065)/.027))*max(0,min(1,(1.85-v.z)/.08))*.8)
        gray*=.7+.3*math.sin(v.x*330+v.z*175)**2
        return tuple(a*(1-gray)+b*gray for a,b in zip((.038,.020,.012),(.24,.22,.19)))
    color_attribute(o,'salt_pepper',hair_color)
    m=material('Garrick | salt and pepper '+name,(.04,.025,.014),.83)
    n=m.node_tree.nodes;l=m.node_tree.links;a=n.new('ShaderNodeVertexColor');a.layer_name='salt_pepper'
    l.new(a.outputs['Color'],n.get('Principled BSDF').inputs['Base Color'])
    o.data.materials.clear();o.data.materials.append(m)
    for f in o.data.polygons:f.material_index=0

# Shared action includes a breathing idle and a restrained reaching fit test.
rig.animation_data_clear()
for frame,breath,reach in [(1,0,0),(31,1,0),(61,0,0),(81,.5,14),(101,.5,14),(121,0,0)]:
    for name,angles in [('spine_03',(breath,0,0)),('upperarm_l',(0,0,-67+reach)),('lowerarm_l',(0,-18-reach,0)),('upperarm_r',(0,0,67)),('lowerarm_r',(0,18,0))]:
        b=rig.pose.bones[name];b.rotation_mode='XYZ';b.rotation_euler=tuple(math.radians(a) for a in angles);b.keyframe_insert('rotation_euler',frame=frame)
rig.animation_data.action.name='Shared | breathe 1-60 and reach fit 61-120'
scene.frame_end=120;scene.frame_set(1)
rig['foundation_profile']='Broad working adult / Garrick fitting'
rig['animation_note']='Shared breathing and reach fit test; no walk or facial rig claim'

# A second actual assembled actor shares mesh datablocks, bone data and animation.
# It is a NON-CANON fitting mannequin, not a new NPC design.
second_coll=bpy.data.collections.new('02 Unnamed fitting test | same wardrobe and skeleton')
scene.collection.children.link(second_coll)
second=rig.copy();second.data=rig.data;second.name='Fitting test | shared humanoid rig';second_coll.objects.link(second)
second.scale=(.91,.94,.95);second['foundation_profile']='Unnamed slimmer fitting test; not approved NPC art'
second.animation_data_create();second.animation_data.action=rig.animation_data.action
clones=[]
for original in list(rig.children):
    if original.type!='MESH' or original.name.startswith(('Apron |','Garrick bar towel','Inn key','Hair_Beard')):continue
    o=original.copy();o.data=original.data;second_coll.objects.link(o);o.name='Fit test | '+original.name;o.parent=second
    for mod in o.modifiers:
        if mod.type=='ARMATURE':mod.object=second
    for idx,slot in enumerate(o.material_slots):
        inherited=original.material_slots[idx].material
        slot.link='OBJECT'
        slot.material=inherited
        if original==head:slot.material=young_materials[idx]
        elif original.name.startswith('Vest |'):slot.material=material('Variant | moss wool '+str(idx),(.078,.108,.070),.94,.07)
        elif original.name=='Hair_SimpleParted':slot.material=material('Variant | chestnut hair',(.10,.041,.018),.85,.12)
    clones.append(o)

rig.location.x=-.65;second.location.x=.65
for o in list(rig.children):
    if o.type=='MESH':o.asset_mark()
rig.asset_mark();second.asset_mark()
rig.data.name='Foundation | shared 65-bone humanoid'
cam=scene.camera
def view(name,pos,target,scale):
    cam.location=pos;cam.rotation_euler=(Vector(target)-cam.location).to_track_quat('-Z','Y').to_euler();cam.data.ortho_scale=scale
    scene.render.filepath=str(OUT/(name+'.png'));bpy.ops.render.render(write_still=True)

cam.location=(2,-8,3.0);cam.rotation_euler=(Vector((0,0,.99))-cam.location).to_track_quat('-Z','Y').to_euler();cam.data.ortho_scale=3.1
bpy.ops.file.pack_all()
bpy.ops.wm.save_as_mainfile(filepath=str(OUT/'Workwear_Foundation.blend'))
view('shared-foundation',(2,-8,3.0),(0,0,.99),3.1)
second_coll.hide_render=True
scene.render.resolution_x=900;scene.render.resolution_y=1150
view('garrick-layered',(2,-8,3),(-.65,0,.98),2.3)
view('garrick-age',(0,-6,2.15),(-.65,0,1.71),.62)
scene.frame_set(91)
view('garrick-reach-fit',(2,-8,3),(-.65,0,1.02),2.25)
scene.frame_set(1);second_coll.hide_render=False
print('FOUNDATION_BUILD_COMPLETE')
