"""Garrick fitting study from licensed Quaternius geometry; no generated image stand-ins."""
import bpy,bmesh,math,json
from pathlib import Path
from mathutils import Vector
OUT=Path(__file__).resolve().parent;SRC=OUT/'Source'
bpy.ops.object.select_all(action='SELECT');bpy.ops.object.delete(use_global=False)
scene=bpy.context.scene
scene.unit_settings.system='METRIC'
def load(path):
 old=set(bpy.data.objects);bpy.ops.import_scene.gltf(filepath=str(path));return list(set(bpy.data.objects)-old)
body=load(SRC/'Body/Superhero_Male_FullBody.gltf')
outfit=load(SRC/'Outfit/Male_Peasant.gltf')
hair=load(SRC/'Hair/Hair_SimpleParted.gltf')
beard=load(SRC/'Hair/Hair_Beard.gltf')
rig=next(o for o in outfit if o.type=='ARMATURE');rig.name='Garrick — deformation skeleton'
skin=next(o for o in body if o.name.startswith('SuperHero_Male'))
bm=bmesh.new();bm.from_mesh(skin.data)
bmesh.ops.delete(bm,geom=[v for v in bm.verts if (skin.matrix_world@v.co).z<1.50 or abs((skin.matrix_world@v.co).x)>.14],context='VERTS')
bm.to_mesh(skin.data);bm.free();skin.name='Garrick — head and neck'
parts=[o for o in body+outfit+hair+beard if o.type=='MESH' and not o.name.startswith('Icosphere')]
for o in parts:
 matrix=o.matrix_world.copy();o.parent=rig;o.matrix_world=matrix
 for m in o.modifiers:
  if m.type=='ARMATURE':m.object=rig
for o in list(bpy.data.objects):
 if (o.type=='ARMATURE' and o!=rig) or o.name.startswith('Icosphere'):bpy.data.objects.remove(o,do_unlink=True)
def reshape(v):
 # Thicken the core instead of retaining the source superhero taper.
 z=v.z;waist=math.exp(-((z-1.10)/.23)**2)
 return Vector((v.x*(1.10+.19*waist),v.y*(1.08+.23*waist),v.z))*1.045
for o in parts:
 inv=o.matrix_world.inverted()
 for v in o.data.vertices:v.co=inv@reshape(o.matrix_world@v.co)
 for f in o.data.polygons:f.use_smooth=True
bpy.context.view_layer.objects.active=rig;rig.select_set(True);bpy.ops.object.mode_set(mode='EDIT')
for bone in rig.data.edit_bones:
 bone.head=reshape(bone.head);bone.tail=reshape(bone.tail)
bpy.ops.object.mode_set(mode='OBJECT')
for mat in bpy.data.materials:
 if not mat.use_nodes:continue
 p=mat.node_tree.nodes.get('Principled BSDF')
 if p:
  p.inputs['Roughness'].default_value=.78
  p.inputs['Specular IOR Level'].default_value=.22
 for n in mat.node_tree.nodes:
  if n.type=='TEX_IMAGE' and n.image and 'T_Superhero_Male_Dark' in n.image.name:
   n.image=bpy.data.images.load(str(SRC/'Body/T_Superhero_Male_Ligh.png'),check_existing=True)
  # Normal maps give the imported sculpt planes without a clay-smooth surface.
  if n.type=='NORMAL_MAP':n.inputs['Strength'].default_value=.6
def pose(name,angles):
 b=rig.pose.bones.get(name)
 if b:b.rotation_mode='XYZ';b.rotation_euler=tuple(math.radians(a) for a in angles)
pose('upperarm_l',(0,0,-67));pose('upperarm_r',(0,0,67))
pose('lowerarm_l',(0,-18,0));pose('lowerarm_r',(0,18,0))
def mat(name,color,rough=.8):
 m=bpy.data.materials.new(name);m.diffuse_color=(*color,1);m.use_nodes=True;p=m.node_tree.nodes.get('Principled BSDF');p.inputs['Base Color'].default_value=(*color,1);p.inputs['Roughness'].default_value=rough;return m
def recolor(material,color,desaturate=False):
 m=material.copy();m.name=material.name+' — Garrick palette';n=m.node_tree.nodes;l=m.node_tree.links;p=n.get('Principled BSDF');old=p.inputs['Base Color'].links[0].from_socket if p.inputs['Base Color'].is_linked else None
 if old:
  if desaturate:
   gray=n.new('ShaderNodeRGBToBW');l.new(old,gray.inputs[0]);ramp=n.new('ShaderNodeValToRGB');ramp.color_ramp.elements[0].color=(*(c*.38 for c in color),1);ramp.color_ramp.elements[1].color=(*color,1);l.new(gray.outputs[0],ramp.inputs[0]);l.new(ramp.outputs[0],p.inputs['Base Color'])
  else:
   mix=n.new('ShaderNodeMixRGB');mix.blend_type='MULTIPLY';mix.inputs[0].default_value=1;mix.inputs[2].default_value=(*color,1);l.new(old,mix.inputs[1]);l.new(mix.outputs[0],p.inputs['Base Color'])
 else:p.inputs['Base Color'].default_value=(*color,1)
 return m
for o in parts:
 for i,m in enumerate(o.data.materials):
  if 'Hair' in m.name:o.data.materials[i]=recolor(m,(.065,.032,.022))
  elif o.name.startswith('Male_Peasant_Arms') and 'Peasant' in m.name:o.data.materials[i]=recolor(m,(.78,.67,.49),True)
  elif o.name.startswith('Male_Peasant_Feet'):o.data.materials[i]=recolor(m,(.30,.23,.19),True)
bodycloth=next(o for o in parts if o.name.startswith('Male_Peasant_Body'))
# Paint a clean tailored vest boundary across the existing sculpted cloth rather
# than assigning whole low-poly faces, which produces a jagged edge.
attr=bodycloth.data.attributes.new('garrick_rest_position','FLOAT_VECTOR','POINT')
for v in bodycloth.data.vertices:attr.data[v.index].vector=bodycloth.matrix_world@v.co
vest=bodycloth.data.materials[0].copy();vest.name='Garrick shirt and fitted vest';bodycloth.data.materials[0]=vest
n=vest.node_tree.nodes;l=vest.node_tree.links;p=n.get('Principled BSDF');old=p.inputs['Base Color'].links[0].from_socket
a=n.new('ShaderNodeAttribute');a.attribute_name='garrick_rest_position';xyz=n.new('ShaderNodeSeparateXYZ');l.new(a.outputs['Vector'],xyz.inputs[0])
def calc(op,left,right=0):
 q=n.new('ShaderNodeMath');q.operation=op
 for i,v in enumerate([left,right]):
  if isinstance(v,(int,float)):q.inputs[i].default_value=v
  else:l.new(v,q.inputs[i])
 return q.outputs[0]
x=calc('ABSOLUTE',xyz.outputs['X']);z=xyz.outputs['Z'];vcut=calc('ADD',calc('MULTIPLY',calc('MAXIMUM',calc('SUBTRACT',z,1.35),0),.36),.025)
mask=calc('MULTIPLY',calc('GREATER_THAN',z,1.07),calc('LESS_THAN',z,1.59))
mask=calc('MULTIPLY',mask,calc('LESS_THAN',x,.265));mask=calc('MULTIPLY',mask,calc('MAXIMUM',calc('GREATER_THAN',x,vcut),calc('GREATER_THAN',xyz.outputs['Y'],.02)))
mix=n.new('ShaderNodeMixRGB');l.new(mask,mix.inputs[0]);l.new(old,mix.inputs[1]);mix.inputs[2].default_value=(.028,.017,.011,1);l.new(mix.outputs[0],p.inputs['Base Color']);p.inputs['Roughness'].default_value=.9
apronmat=mat('Dark brown work apron',(.075,.044,.031));towelmat=mat('Linen bar towel',(.68,.59,.44));stripe=mat('Oxblood towel bands',(.25,.045,.037));brass=mat('Worn brass keys',(.32,.20,.07),.45)
def panel(name,xcenter,width,top,bottom,y,material,fold=.009):
 vertices=[];faces=[];nx=14;nz=16
 for j in range(nz+1):
  t=j/nz
  for i in range(nx+1):
   u=i/nx;xx=xcenter+(u-.5)*width*(1+.06*t);zz=top+(bottom-top)*t+.006*math.sin(u*math.pi*3)*t
   yy=y+((u-.5)*2)**2*.06+math.sin(u*math.pi*7)*fold*(.2+t)+.009*math.cos(t*6+u*2)
   vertices.append((xx,yy,zz))
 for j in range(nz):
  for i in range(nx):
   a=j*(nx+1)+i;faces.append((a,a+1,a+nx+2,a+nx+1))
 mesh=bpy.data.meshes.new(name);mesh.from_pydata(vertices,[],faces);mesh.materials.append(material);o=bpy.data.objects.new(name,mesh);scene.collection.objects.link(o);o.parent=rig
 for p in mesh.polygons:p.use_smooth=True
 vg=o.vertex_groups.new(name='pelvis');vg.add(list(range(len(vertices))),1,'REPLACE');arm=o.modifiers.new('Follow hips','ARMATURE');arm.object=rig
 solid=o.modifiers.new('Cloth edge','SOLIDIFY');solid.thickness=.004
 parts.append(o);return o
apron=panel('Garrick practical waist apron',0,.45,1.115,.52,-.197,apronmat)
towel=panel('Garrick bar towel',-.185,.13,1.13,.73,-.224,towelmat,.005);towel.data.materials.append(stripe)
for f in towel.data.polygons:
 if .757<f.center.z<.772 or .791<f.center.z<.798:f.material_index=1
def torus(name,loc,major,minor,material):
 bpy.ops.mesh.primitive_torus_add(major_radius=major,minor_radius=minor,major_segments=24,minor_segments=8,location=loc,rotation=(math.pi/2,0,0));o=bpy.context.object;o.name=name;o.data.materials.append(material);o.parent=rig;vg=o.vertex_groups.new(name='pelvis');vg.add(list(range(len(o.data.vertices))),1,'REPLACE');m=o.modifiers.new('Follow hips','ARMATURE');m.object=rig;parts.append(o);return o
torus('Inn key ring',(.20,-.225,1.07),.024,.003,brass)
for i in range(3):
 x=.188+i*.012;torus('Inn key bow '+str(i),(x,-.229-i*.002,1.032-i*.004),.009,.002,brass)
 bpy.ops.mesh.primitive_cube_add(size=1,location=(x,-.229-i*.002,.999-i*.004));o=bpy.context.object;o.name='Inn key '+str(i);o.scale=(.005,.004,.042);bpy.ops.object.transform_apply(location=False,rotation=False,scale=True);o.data.materials.append(brass);o.parent=rig;parts.append(o)
# A restrained breathing idle on the retained skeleton, useful for judging volume in motion.
for frame,angle in [(1,0),(31,1.0),(61,0)]:
 b=rig.pose.bones['spine_03'];b.rotation_mode='XYZ';b.rotation_euler.x=math.radians(angle);b.keyframe_insert(data_path='rotation_euler',frame=frame)
scene.frame_start=1;scene.frame_end=60;scene.render.fps=30;scene.frame_set(1)
floor=mat('Warm slate backdrop',(.055,.061,.063))
bpy.ops.mesh.primitive_plane_add(size=200);ground=bpy.context.object;ground.name='Studio ground';ground.location.z=-.013;ground.data.materials.append(floor)
world=bpy.data.worlds.new('Studio ambient');scene.world=world;world.use_nodes=True;world.node_tree.nodes['Background'].inputs[0].default_value=(.36,.42,.51,1);world.node_tree.nodes['Background'].inputs[1].default_value=.45
def area(name,loc,energy,color,size):
 d=bpy.data.lights.new(name,'AREA');d.energy=energy;d.color=color;d.shape='DISK';d.size=size;o=bpy.data.objects.new(name,d);scene.collection.objects.link(o);o.location=loc;o.rotation_euler=(Vector((0,0,1.1))-o.location).to_track_quat('-Z','Y').to_euler()
area('Warm window',(-3,-4,5),600,(1,.84,.67),4)
area('Soft fill',(3,-1,3),220,(.72,.83,1),3)
area('Shoulder rim',(0,3,4),500,(1,.72,.44),3)
d=bpy.data.cameras.new('Review camera');cam=bpy.data.objects.new('Review camera',d);scene.collection.objects.link(cam);scene.camera=cam;d.type='ORTHO';d.ortho_scale=2.35
scene.render.engine='CYCLES';scene.cycles.samples=32;scene.cycles.use_denoising=True
scene.render.resolution_x=850;scene.render.resolution_y=1100;scene.render.resolution_percentage=100
scene.view_settings.view_transform='AgX';scene.render.image_settings.file_format='PNG'
def view(name,pos,target=(0,0,.98),scale=2.35):
 cam.location=pos;cam.rotation_euler=(Vector(target)-cam.location).to_track_quat('-Z','Y').to_euler();cam.data.ortho_scale=scale
 scene.render.filepath=str(OUT/(name+'.png'));bpy.ops.render.render(write_still=True)
cam.location=(3,-7,3);cam.rotation_euler=(Vector((0,0,.98))-cam.location).to_track_quat('-Z','Y').to_euler()
bpy.ops.wm.save_as_mainfile(filepath=str(OUT/'Garrick_Rigged_Candidate.blend'))
view('three-quarter',(3,-7,3.0))
view('front',(0,-7,2.3))
view('portrait',(1.3,-5,2.1),(0,0,1.62),.77)
for o in parts:o.data.calc_loop_triangles()
report={'source':'Quaternius CC0 Universal Base Characters and Modular Character Outfits Fantasy','mesh_objects':len(parts),'bones':len(rig.data.bones),'triangles':sum(len(o.data.loop_triangles) for o in parts),'note':'Initial fitting study. Rig retained with breathing idle; no Unity integration or likeness approval.'}
(OUT/'verification.json').write_text(json.dumps(report,indent=2))
