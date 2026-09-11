"""Local CC5-derived Garrick fitting candidate; original commercial export stays untouched."""
import bpy,bmesh,json,math
from pathlib import Path
from mathutils import Vector,Quaternion,Matrix
from mathutils.kdtree import KDTree
from mathutils.bvhtree import BVHTree
ROOT=Path(__file__).resolve().parents[4]/'outputs/character-assets';OUT=ROOT/'Garrick-CC5-v01';OUT.mkdir(exist_ok=True)
ART=Path(__file__).resolve().parents[2]
bpy.ops.wm.open_mainfile(filepath=str(ROOT/'CC5-Import-v01/Kevin_Blender_Test.blend'))
scene=bpy.context.scene;rig=bpy.data.objects['Armature'];rig.name='Garrick | CC humanoid'
rig.animation_data_clear()
for b in rig.pose.bones:b.matrix_basis=Matrix.Identity(4)
body=bpy.data.objects['CC_Base_Body'];native=[o for o in rig.children if o.type=='MESH']
def gauss(a,b,w):return math.exp(-((a-b)/w)**2)
def shape(v):
    x,y,z=v;core=gauss(z,1.10,.30);neck=gauss(z,1.55,.07)
    sx=1.12+.17*core+.035*neck
    if z>1.60:sx=1.065
    sy=1.13+.20*core
    if z>1.60:sy=1.035
    # Subtle cheek/under-eye plane changes rather than photoreal wrinkle sculpting.
    eye=gauss(abs(x),.032,.022)*gauss(z,1.677,.009)*gauss(y,-.074,.025)
    cheek=gauss(abs(x),.050,.022)*gauss(z,1.648,.03)*gauss(y,-.068,.028)
    return Vector((x*sx,y*sy+.0025*eye-.0015*cheek,z))*1.047
for o in native:
    inv=o.matrix_world.inverted()
    blocks=o.data.shape_keys.key_blocks if o.data.shape_keys else []
    if blocks:
        for k in blocks:
            k.value=0
            for v in k.data:v.co=inv@shape(o.matrix_world@v.co)
    else:
        for v in o.data.vertices:v.co=inv@shape(o.matrix_world@v.co)
    # Blender's base Mesh vertices can lag behind direct KeyBlock edits. Fitting,
    # masks and transfer queries must use the same deformed Basis as the renderer.
    if blocks:
        for v,k in zip(o.data.vertices,blocks[0].data):v.co=k.co
    o.data.update()
bpy.context.view_layer.objects.active=rig;rig.select_set(True);bpy.ops.object.mode_set(mode='EDIT')
for b in rig.data.edit_bones:
    b.head=rig.matrix_world.inverted()@shape(rig.matrix_world@b.head)
    b.tail=rig.matrix_world.inverted()@shape(rig.matrix_world@b.tail)
bpy.ops.object.mode_set(mode='OBJECT');bpy.context.view_layer.update()
if bpy.data.objects.get('Boxers'):bpy.data.objects['Boxers'].hide_render=True;bpy.data.objects['Boxers'].hide_viewport=True
coll=bpy.data.collections.new('Garrick | fitted workwear');scene.collection.children.link(coll)
with bpy.data.libraries.load(str(ART/'Characters/Foundation-v01/Workwear_Foundation.blend'),link=False) as (src,dst):
    dst.collections=['01 Garrick | layered workwear']
oldcoll=dst.collections[0];scene.collection.children.link(oldcoll)
oldrig=next(o for o in oldcoll.objects if o.type=='ARMATURE')
oldrig.animation_data_clear();oldrig.location=(0,0,0)
for b in oldrig.pose.bones:b.matrix_basis=Matrix.Identity(4)
bpy.context.view_layer.update()
oldparts=[o for o in oldcoll.objects if o.type=='MESH']
def old_head(name):return oldrig.matrix_world@oldrig.data.bones[name].head_local
def cc_head(name):return rig.matrix_world@rig.data.bones[name].head_local
segment={}
for side in ['l','r']:
    cc=side.upper()
    for start,end,target_start,target_end in [('upperarm_'+side,'lowerarm_'+side,'CC_Base_'+cc+'_Upperarm','CC_Base_'+cc+'_Forearm'),('lowerarm_'+side,'hand_'+side,'CC_Base_'+cc+'_Forearm','CC_Base_'+cc+'_Hand')]:
        if start not in oldrig.data.bones or end not in oldrig.data.bones:continue
        a=old_head(start);d=old_head(end)-a;ta=cc_head(target_start);td=cc_head(target_end)-ta
        segment[start]=(a,d,ta,td,d.rotation_difference(td))
def torso_fit(v):return Vector((v.x*1.15,v.y*1.15-.012,v.z*.99))
def arm_fit(v,weights):
    out=Vector();total=0
    for name,w in weights.items():
        if name in segment:
            a,d,ta,td,q=segment[name];delta=v-a;axis=d.normalized();along=delta.dot(axis)
            pos=ta+td.normalized()*along*(td.length/d.length)+q@(delta-axis*along)*1.11
        else:pos=torso_fit(v)
        out+=pos*w;total+=w
    return out/max(total,.00001)

# Body-surface weights are transferred after fitting; source rig weights are discarded.
bodyvs=[body.matrix_world@v.co for v in body.data.vertices]
kd=KDTree(len(bodyvs))
for i,v in enumerate(bodyvs):kd.insert(v,i)
kd.balance()
bvh=BVHTree.FromPolygons(bodyvs,[list(p.vertices) for p in body.data.polygons])
bodyweights=[{body.vertex_groups[g.group].name:g.weight for g in v.groups} for v in body.data.vertices]
def bind(o,rigid=None):
    o.vertex_groups.clear()
    for m in list(o.modifiers):
        if m.type=='ARMATURE':o.modifiers.remove(m)
    matrix=o.matrix_world.copy();o.parent=rig;o.matrix_world=matrix
    for v in o.data.vertices:
        p=o.matrix_world@v.co
        if rigid:weights={rigid:1}
        else:
            weights={};near=kd.find_n(p,4);denom=sum(1/max(d,.001)**2 for _,_,d in near)
            for _,idx,d in near:
                for name,w in bodyweights[idx].items():weights[name]=weights.get(name,0)+w/max(d,.001)**2/denom
        total=sum(weights.values())
        for name,w in weights.items():
            if w<.00001:continue
            group=o.vertex_groups.get(name) or o.vertex_groups.new(name=name);group.add([v.index],w/total,'REPLACE')
    m=o.modifiers.new('CC skeleton deformation','ARMATURE');m.object=rig
    # Skinning before thickness gives a consistent garment edge.
    while o.modifiers.find(m.name)>0:bpy.context.view_layer.objects.active=o;bpy.ops.object.modifier_move_up(modifier=m.name)

garments=[]
for o in oldparts:
    if o.name.startswith(('Head |','Eyes','Eyebrows','Hair_')):continue
    o.data=o.data.copy()
    if o.name.startswith('Shirt | rolled sleeves'):
        bm=bmesh.new();bm.from_mesh(o.data)
        # Discard the previous base's skin and hands, retaining only cloth.
        bmesh.ops.delete(bm,geom=[f for f in bm.faces if f.material_index!=0],context='FACES')
        bmesh.ops.delete(bm,geom=[v for v in bm.verts if not v.link_faces],context='VERTS')
        bm.to_mesh(o.data);bm.free()
    matrix=o.matrix_world.copy();inv=matrix.inverted()
    for v in o.data.vertices:
        p=matrix@v.co;weights={o.vertex_groups[g.group].name:g.weight for g in v.groups}
        p=arm_fit(p,weights) if o.name.startswith('Shirt | rolled sleeves') else torso_fit(p)
        v.co=inv@p
    for c in list(o.users_collection):c.objects.unlink(o)
    coll.objects.link(o)
    bind(o,'CC_Base_Hip' if o.name.startswith(('Apron | skirt','Apron | stitched','Inn key','Belt |','Garrick bar towel')) else None)
    garments.append(o)

# Non-destructive body visibility mask removes skin hidden by the outfit.
group=body.vertex_groups.new(name='Visible face neck forearms')
for v in body.data.vertices:
    p=body.matrix_world@v.co
    if p.z>1.565 and abs(p.x)<.20 or abs(p.x)>.435 and p.z>1.05:
        group.add([v.index],1,'REPLACE')
mask=body.modifiers.new('Body hidden beneath workwear','MASK');mask.vertex_group=group.name

# Fit the existing licensed clumped hair and beard to the CC head.
hairparts=[]
for name in ['Hair_SimpleParted']:
    o=next(o for o in oldparts if o.name.startswith(name));o.data=o.data.copy();inv=o.matrix_world.inverted();beard='Beard' in name
    for v in o.data.vertices:
        p=o.matrix_world@v.co
        p.x*=1.045;p.y+=.006;p.z-=.010
        if beard:
            loc,normal,_,dist=bvh.find_nearest(p)
            if loc is not None and loc.z>1.60:
                p=p*.23+(loc+normal*.007)*.77
            p.z-=.004*(.5+.5*math.sin(p.x*90))
        else:
            # Extend nape masses; loosen the silhouette while retaining real source topology.
            if p.y>.035:
                p.z-=.078*max(0,min(1,(1.85-p.z)/.12));p.y+=.01
            p.x+=.0035*math.sin(p.z*95+p.y*70)
        v.co=inv@p
    for c in list(o.users_collection):c.objects.unlink(o)
    coll.objects.link(o);bind(o,'CC_Base_Head');o.name='Garrick | '+('beard' if beard else 'rugged hair');hairparts.append(o)

# Beard follows the CC face surface and inherits the exported facial shape keys.
# The old head's beard shell cannot be reused without floating edges.
selected=[]
for f in body.data.polygons:
    p=sum((bodyvs[i] for i in f.vertices),Vector())/len(f.vertices)
    x,y,z=p;ax=abs(x)
    top=1.712+min(ax/.075,1)*.030
    mouth=((x/.035)**2+((z-1.700)/.011)**2)<1
    if 1.648<z<top and y<.015 and ax<.087 and not mouth:
        selected.append(f)
ids=sorted({i for f in selected for i in f.vertices});index={old:new for new,old in enumerate(ids)}
offsets=[];points=[]
for i in ids:
    p=bodyvs[i];normal=body.matrix_world.to_3x3().inverted().transposed()@body.data.vertices[i].normal;normal.normalize()
    jaw=gauss(p.z,1.66,.026)
    delta=normal*(.0015+.003*jaw);delta.z-=.0025*jaw*(.7+.3*math.sin(p.x*210))
    offsets.append(delta);points.append(p+delta)
data=bpy.data.meshes.new('CC conforming beard surface');data.from_pydata(points,[],[tuple(index[i] for i in f.vertices) for f in selected]);data.update()
beard=bpy.data.objects.new('Garrick | face-following beard',data);coll.objects.link(beard);bind(beard)
for f in data.polygons:f.use_smooth=True
beard.shape_key_add(name='Basis')
for sourcekey in body.data.shape_keys.key_blocks[1:]:
    key=beard.shape_key_add(name=sourcekey.name)
    for j,i in enumerate(ids):key.data[j].co=body.matrix_world@sourcekey.data[i].co+offsets[j]
    driver=key.driver_add('value').driver;driver.type='AVERAGE'
    var=driver.variables.new();var.name='face';var.type='SINGLE_PROP';var.targets[0].id_type='KEY';var.targets[0].id=body.data.shape_keys;var.targets[0].data_path='key_blocks['+json.dumps(sourcekey.name)+'].value'
mat=bpy.data.materials.new('Beard | dark roots and broken gray');mat.use_nodes=True
n=mat.node_tree.nodes;l=mat.node_tree.links;p=n.get('Principled BSDF');p.inputs['Roughness'].default_value=.86
color=data.color_attributes.new(name='Beard age color',type='FLOAT_COLOR',domain='POINT')
for v in data.vertices:
    x,y,z=v.co
    gray=max(.08,min(.68,(1.728-z)/.105))
    gray*=.82+.18*math.sin(x*170)**2
    color.data[v.index].color=tuple(a*(1-gray)+b*gray for a,b in zip((.020,.014,.010),(.18,.155,.13)))+(1,)
attribute=n.new('ShaderNodeVertexColor');attribute.layer_name=color.name;l.new(attribute.outputs['Color'],p.inputs['Base Color'])
data.materials.append(mat);hairparts.append(beard)

# Restrained illustrated material treatment, with age around eyes and forehead.
for m in body.data.materials:
    if not m.name.startswith('Std_Skin'):continue
    n=m.node_tree.nodes;l=m.node_tree.links;p=n.get('Principled BSDF')
    for link in list(p.inputs['Roughness'].links):l.remove(link)
    p.inputs['Roughness'].default_value=.78;p.inputs['Specular IOR Level'].default_value=.20
    for node in n:
        if node.type=='NORMAL_MAP':node.inputs['Strength'].default_value=.28
    if p.inputs['Base Color'].is_linked:
        old=p.inputs['Base Color'].links[0].from_socket
        hsv=n.new('ShaderNodeHueSaturation');hsv.inputs['Saturation'].default_value=.80;hsv.inputs['Value'].default_value=.92
        l.new(old,hsv.inputs['Color']);l.new(hsv.outputs['Color'],p.inputs['Base Color'])
    if m.name=='Std_Skin_Head':
        attr=body.data.attributes.new('Garrick rest position','FLOAT_VECTOR','POINT')
        for v in body.data.vertices:attr.data[v.index].vector=body.matrix_world@v.co
        a=n.new('ShaderNodeAttribute');a.attribute_name=attr.name;xyz=n.new('ShaderNodeSeparateXYZ');l.new(a.outputs['Vector'],xyz.inputs[0])
        def mathnode(op,x,y=0):
            q=n.new('ShaderNodeMath');q.operation=op
            for i,v in enumerate((x,y)):
                if isinstance(v,(int,float)):q.inputs[i].default_value=v
                else:l.new(v,q.inputs[i])
            return q.outputs[0]
        def gaussian(v,center,width):
            d=mathnode('DIVIDE',mathnode('SUBTRACT',v,center),width)
            return mathnode('EXPONENT',mathnode('MULTIPLY',mathnode('MULTIPLY',d,d),-1))
        x=mathnode('ABSOLUTE',xyz.outputs['X']);z=xyz.outputs['Z'];y=xyz.outputs['Y']
        under=mathnode('MULTIPLY',gaussian(x,.037,.026),gaussian(z,1.755,.010))
        forehead=mathnode('ADD',gaussian(z,1.832,.0013),gaussian(z,1.846,.0011))
        forehead=mathnode('MULTIPLY',forehead,gaussian(x,0,.061))
        amount=mathnode('ADD',mathnode('MULTIPLY',under,.17),mathnode('MULTIPLY',forehead,.14))
        amount=mathnode('MULTIPLY',amount,gaussian(y,-.075,.055))
        mix=n.new('ShaderNodeMixRGB');l.new(amount,mix.inputs[0]);l.new(p.inputs['Base Color'].links[0].from_socket,mix.inputs[1]);mix.inputs[2].default_value=(.14,.074,.045,1);l.new(mix.outputs[0],p.inputs['Base Color'])

# Remove appended source actors; retain only adapted geometry and the CC rig.
for o in list(oldcoll.objects):bpy.data.objects.remove(o,do_unlink=True)
bpy.data.collections.remove(oldcoll)
rig['candidate']='CC5-derived Garrick fitting; not visual approval or Unity integration'
rig['source']='Owner-exported CC4 Kevin through Character Creator 5; original untouched'

# World-space limb aiming accounts for the exported A-pose and differing bone rolls.
def aim(name,end_name,target):
    b=rig.pose.bones[name];source=cc_head(end_name)-cc_head(name)
    source=rig.matrix_world.to_quaternion().inverted()@source;target=rig.matrix_world.to_quaternion().inverted()@Vector(target)
    worldq=source.rotation_difference(target);q=b.bone.matrix_local.to_quaternion();b.rotation_mode='QUATERNION';b.rotation_quaternion=q.inverted()@worldq@q
def pose(reach=False):
    for b in rig.pose.bones:b.matrix_basis=Matrix.Identity(4)
    for side,sign in [('L',1),('R',-1)]:
        target=(sign*.12,-.01,-.48)
        if reach and side=='L':target=(sign*.16,-.34,-.28)
        aim('CC_Base_'+side+'_Upperarm','CC_Base_'+side+'_Forearm',target)
    bpy.context.view_layer.update()
for frame,reach in [(1,False),(31,False),(61,False),(85,True),(105,True),(121,False)]:
    pose(reach)
    for side in ['L','R']:rig.pose.bones['CC_Base_'+side+'_Upperarm'].keyframe_insert('rotation_quaternion',frame=frame)
    b=rig.pose.bones['CC_Base_Spine02'];b.rotation_mode='XYZ';b.rotation_euler.x=math.radians(.7 if frame==31 else 0);b.keyframe_insert('rotation_euler',frame=frame)
rig.animation_data.action.name='Garrick | idle and reach fitting test';scene.frame_start=1;scene.frame_end=120;scene.render.fps=30;scene.frame_set(1)
cam=scene.camera;scene.cycles.samples=32;scene.render.resolution_x=900;scene.render.resolution_y=1150
cam.location=(2.3,-7,2.8);cam.rotation_euler=(Vector((0,0,1))-cam.location).to_track_quat('-Z','Y').to_euler();cam.data.ortho_scale=2.30
bpy.ops.file.pack_all();bpy.ops.wm.save_as_mainfile(filepath=str(OUT/'Garrick_CC5_Candidate.blend'))
def render(name,pos,target,scale):
    cam.location=pos;cam.rotation_euler=(Vector(target)-cam.location).to_track_quat('-Z','Y').to_euler();cam.data.ortho_scale=scale;scene.render.filepath=str(OUT/(name+'.png'));bpy.ops.render.render(write_still=True)
render('garrick-full',(2.3,-7,2.8),(0,0,1),2.30)
render('garrick-face',(1,-6,2.15),(0,0,1.72),.69)
scene.frame_set(91);render('garrick-reach',(2.3,-7,2.8),(0,0,1),2.30)
print('CC_GARRICK_BUILD_COMPLETE')
