"""Garrick head study from CC0 anatomical topology and existing painted albedo."""
import bpy, bmesh, math, random, json
from pathlib import Path
from mathutils import Vector
from mathutils.bvhtree import BVHTree
random.seed(28)
OUT=Path(__file__).resolve().parent
bpy.ops.object.select_all(action='SELECT');bpy.ops.object.delete(use_global=False)
sc=bpy.context.scene
sc.unit_settings.system='METRIC'
def mat(name,col,rough=.8):
 m=bpy.data.materials.new(name);m.diffuse_color=(*col,1);m.use_nodes=True
 p=m.node_tree.nodes.get('Principled BSDF');p.inputs['Base Color'].default_value=(*col,1);p.inputs['Roughness'].default_value=rough;p.inputs['Specular IOR Level'].default_value=.18
 return m
skin=mat('Skin | warm ochre planes',(.39,.215,.137),.8)
lip=mat('Lips | muted warm',(.30,.115,.087))
hair=mat('Hair | deep umber',(.025,.015,.012),.79)
hairlight=mat('Hair | warm broken highlights',(.066,.036,.023),.8)
beard=mat('Beard | charcoal umber',(.04,.029,.024),.9)
salt=mat('Beard | restrained grey',(.12,.105,.089),.9)
brow=mat('Brows',(.036,.020,.014))
linen=mat('Linen | warm ivory',(.51,.445,.35),.96)
vest=mat('Waistcoat | brown charcoal',(.039,.027,.022),.9)
stitch=mat('Worked edges',(.115,.072,.043),.94)
white=mat('Eyes | ivory',(.52,.47,.37),.4)
iris=mat('Eyes | grey hazel',(.10,.135,.109),.38)
black=mat('Eyes | pupils',(.004,.005,.004),.35)
def mesh(name,vs,fs,ma,sub=0):
 d=bpy.data.meshes.new(name);d.from_pydata(vs,[],fs);d.update();o=bpy.data.objects.new(name,d);sc.collection.objects.link(o);d.materials.append(ma)
 for p in d.polygons:p.use_smooth=True
 if sub:m=o.modifiers.new('Surface subdivision','SUBSURF');m.levels=sub;m.render_levels=sub
 return o
def tube(name,pts,r,ma):
 d=bpy.data.curves.new(name,'CURVE');d.dimensions='3D';d.resolution_u=10;d.bevel_depth=r;d.bevel_resolution=2
 s=d.splines.new('BEZIER');s.bezier_points.add(len(pts)-1)
 for p,co in zip(s.bezier_points,pts):p.co=co;p.handle_left_type='AUTO';p.handle_right_type='AUTO'
 o=bpy.data.objects.new(name,d);sc.collection.objects.link(o);d.materials.append(ma);return o
def ball(name,co,scale,ma):
 bpy.ops.mesh.primitive_uv_sphere_add(segments=32,ring_count=20,location=co);o=bpy.context.object;o.name=name;o.scale=scale;o.data.materials.append(ma)
 for p in o.data.polygons:p.use_smooth=True
 return o
vs=[];faces=[];groups={};g='';uvs=[];faceuv=[]
for line in (OUT/'Source/base.obj').read_text().splitlines():
 a=line.split()
 if not a:continue
 if a[0]=='v':vs.append(Vector(tuple(map(float,a[1:4]))))
 elif a[0]=='vt':uvs.append(tuple(map(float,a[1:3])))
 elif a[0]=='g':g=a[1];groups[g]=[]
 elif a[0]=='f':
  f=[int(x.split('/')[0])-1 for x in a[1:]];groups[g].extend(f)
  if g=='body':faces.append(f);faceuv.append([int(x.split('/')[1])-1 for x in a[1:]])
weights={'caucasian-male-young.target':.67,'caucasian-male-old.target':.33,'universal-male-young-maxmuscle-averageweight.target':.58,'chin-width-incr.target':.36,'chin-bones-incr.target':.32,'head-square.target':.30}
for name,w in weights.items():
 for line in (OUT/'Source'/name).read_text().splitlines():
  a=line.split()
  if len(a)==4 and not a[0].startswith('#'):vs[int(a[0])]+=Vector(tuple(map(float,a[1:])))*w
def convert(p):
 x,y,z=p
 # Broaden lower face and neck; preserve anatomical eyelids and lip loops.
 factor=1.045+.075*math.exp(-((y-7.4)/.46)**2)
 return Vector((x*.115*factor,-(z-.45)*.115,(y-8.09)*.115))
eye_centres=[convert(sum((vs[i] for i in set(groups[n])),Vector())/len(set(groups[n]))) for n in ['joint-l-eye','joint-r-eye']]
vs=[convert(p) for p in vs]
keep=[i for i,f in enumerate(faces) if min(vs[j].z for j in f)>-.205]
ids=sorted(set(j for i in keep for j in faces[i]));remap={old:i for i,old in enumerate(ids)}
head=mesh('Garrick | anatomical face neck shoulders',[vs[i] for i in ids],[[remap[j] for j in faces[i]] for i in keep],skin,2)
uv=head.data.uv_layers.new(name='Original anatomical UV')
for p,fi in zip(head.data.polygons,keep):
 for li,ui in zip(p.loop_indices,faceuv[fi]):uv.data[li].uv=uvs[ui]
# Vertex-painted shading follows the anatomy continuously: no projected face or beard mask.
paint=head.data.color_attributes.new(name='Complexion',type='FLOAT_COLOR',domain='POINT')
def smooth(a,b,v):
 t=max(0,min(1,(v-a)/(b-a)));return t*t*(3-2*t)
for v in head.data.vertices:
 x,y,z=v.co;ax=abs(x);frontness=1-smooth(.001,.039,y)
 edge=-.080+smooth(.027,.082,ax)*.106
 density=(1-smooth(edge-.010,edge+.006,z))*smooth(-.141,-.119,z)*frontness
 mouth_clear=(1-smooth(.024,.035,ax))*smooth(-.083,-.073,z)*(1-smooth(-.047,-.037,z))
 density*=1-mouth_clear
 base=Vector((.39,.205,.128));dark=Vector((.056,.035,.025));col=base.lerp(dark,density*.93)
 liparea=(1-smooth(.022,.030,ax))*smooth(-.075,-.067,z)*(1-smooth(-.058,-.052,z))*smooth(.115,.129,-y)
 col=col.lerp(Vector((.31,.117,.084)),liparea*.65)
 under=math.exp(-((ax-.037)/.023)**2-((z+.010)/.009)**2)*frontness
 col*=1-under*.17
 paint.data[v.index].color=(*col,1)
attr=skin.node_tree.nodes.new('ShaderNodeVertexColor');attr.layer_name='Complexion';skin.node_tree.links.new(attr.outputs['Color'],skin.node_tree.nodes['Principled BSDF'].inputs['Base Color'])
# Reuse the previously generated albedo for painterly surface detail, while eyes,
# nose, lips and ears now have anatomical geometry. Keep projection provisional.
albedo=bpy.data.images.load(str(OUT.parent/'v02/Garrick_Face_Albedo.png'));albedo.pack()
uvpaint=head.data.uv_layers.new(name='Provisional albedo registration')
anchors=[(-.139,0),(-.118,.075),(-.085,.284),(-.050,.416),(0,.645),(.022,.73),(.067,.84),(.13,1)]
def imagev(z):
 for (a,va),(b,vb) in zip(anchors,anchors[1:]):
  if z<=b:return va+(vb-va)*(z-a)/(b-a)
 return 1
for p in head.data.polygons:
 for li in p.loop_indices:
  c=head.data.vertices[head.data.loops[li].vertex_index].co
  uvpaint.data[li].uv=(.5+c.x/.208,imagev(c.z))
mask=head.data.color_attributes.new(name='Albedo blend',type='FLOAT_COLOR',domain='POINT')
for v in head.data.vertices:
 x,y,z=v.co
 f=(1-smooth(-.032,.009,y))*smooth(-.146,-.120,z)*(1-smooth(.078,.092,abs(x)))
 mask.data[v.index].color=(f,f,f,1)
n=skin.node_tree.nodes;l=skin.node_tree.links
uvn=n.new('ShaderNodeUVMap');uvn.uv_map=uvpaint.name
tex=n.new('ShaderNodeTexImage');tex.image=albedo;tex.extension='EXTEND';l.new(uvn.outputs[0],tex.inputs['Vector'])
maskn=n.new('ShaderNodeVertexColor');maskn.layer_name=mask.name
mix=n.new('ShaderNodeMixRGB');l.new(maskn.outputs['Color'],mix.inputs[0]);l.new(attr.outputs['Color'],mix.inputs[1]);l.new(tex.outputs['Color'],mix.inputs[2]);l.new(mix.outputs[0],n['Principled BSDF'].inputs['Base Color'])
bpy.context.view_layer.update()
tree=BVHTree.FromObject(head,bpy.context.evaluated_depsgraph_get())
def front(x,z,offset=0):
 hit=tree.ray_cast(Vector((x,-.5,z)),Vector((0,1,0)))
 return Vector((x,(hit[0].y if hit[0] else 1)-offset,z))
def lock(name,pts,width,ma,depth=.28):
 # Flat tapered hair clumps with a central crest, not cylindrical strands.
 pts=list(pts)
 if any(word in name.lower() for word in ['wave','lock']):
  near=tree.find_nearest(Vector(pts[0]))
  if near[0]:pts[0]=near[0]-near[1]*.001
 dense=[]
 for k in range(len(pts)-1):
  a=Vector(pts[max(0,k-1)]);b=Vector(pts[k]);c=Vector(pts[k+1]);d=Vector(pts[min(len(pts)-1,k+2)])
  for j in range(8):
   t=j/8;dense.append(.5*((2*b)+(-a+c)*t+(2*a-5*b+4*c-d)*t*t+(-a+3*b-3*c+d)*t*t*t))
 dense.append(Vector(pts[-1]));v=[];f=[]
 for k,p in enumerate(dense):
  t=k/(len(dense)-1);tangent=(dense[min(k+1,len(dense)-1)]-dense[max(0,k-1)]).normalized();normal=Vector((p.x,p.y,p.z-.03)).normalized();right=tangent.cross(normal).normalized()
  if right.length<.1:right=Vector((1,0,0))
  normal=right.cross(tangent).normalized();w=width*(.35+.8*math.sin(math.pi*t*.88))*(1-t)**.5
  if k==len(dense)-1:w=.0001
  for q in [-1,-.5,0,.5,1]:v.append(p+right*w*q+normal*(1-q*q)*width*depth)
 for k in range(len(dense)-1):
  for j in range(4):a=k*5+j;f.append((a,a+1,a+6,a+5))
 o=mesh(name,v,f,ma)
 uv=o.data.uv_layers.new(name='Strand direction')
 for poly in o.data.polygons:
  for li in poly.loop_indices:
   vi=o.data.loops[li].vertex_index;uv.data[li].uv=((vi%5)/4,(vi//5)/(len(dense)-1))
 s=o.modifiers.new('Thin hair surface','SOLIDIFY');s.thickness=.0005;return o
# Actual eyeballs in the eye sockets: pupils remain geometric in every view.
for c in eye_centres:
 c.y-=.002
 ball('Eyeball',c,(.0138,.0138,.0138),white)
 ball('Hazel iris',c+Vector((-.0004,-.0134,0)),(.0057,.0012,.0057),iris)
 ball('Pupil',c+Vector((-.0004,-.0144,0)),(.0024,.00055,.0030),black)
 # Brows sit on the actual supraorbital ridge.
 s=1 if c.x>0 else -1
 pts=[front(s*x,z,.0007) for x,z in [(.013,.012),(.024,.018),(.04,.023),(.056,.020)]]
 # Anatomical ridge plus painted brow; no duplicate solid brow over the albedo.
 for j in range(12):
  x=s*(.016+j*.003);z=.018+.007*math.sin(j/11*math.pi)
# Fine broken beard edge, individually follows face rather than masking mouth.
for s in [-1,1]:
 for row in range(19):
  z=-.12+row*.006
  for col in range(18):
   x=s*(.008+col*.0047+random.uniform(-.0015,.0015));edge=-.071+min(abs(x)/.082,1)*.095
   if z>edge or (abs(x)<.033 and z>-.075):continue
   p=front(x,z,.0022)
   if p.y>-.014:continue
   length=random.uniform(.004,.010);q=front(x+s*.001,z-length*.5,.003);r=front(x+s*.002,z-length,.002)
   if q.y>.1 or r.y>.1:continue
   if abs(x)>.075 and random.random()<.18:lock('Beard edge tuft',[p,q,r],random.uniform(.00025,.0005),beard,.04)
# Scalp cap is extracted from the head itself; no exposed smooth helmet at the hairline.
hf=[]
for p in head.data.polygons:
 c=p.center
 if (c.z>.063+(.029 if c.y<-.012 else 0)) or (c.y>.029 and c.z>-.038) or (abs(c.x)>.078 and c.z>.029):hf.append(list(p.vertices))
used=sorted(set(j for f in hf for j in f));mp={j:i for i,j in enumerate(used)}
cap=mesh('Scalp under hair',[(head.data.vertices[j].co+head.data.vertices[j].normal*.0013) for j in used],[[mp[j] for j in f] for f in hf],hair,1)
# Keep only the rear underlayer, where a filled scalp is needed behind the locks.
bm=bmesh.new();bm.from_mesh(cap.data)
bmesh.ops.delete(bm,geom=[f for f in bm.faces if f.calc_center_median().y<.015],context='FACES')
bm.to_mesh(cap.data);bm.free()
# Short overlapping waves with distinct direction and pointed silhouettes.
# Randomized roots and ends break the parallel comb-over of the rejected study.
for i in range(92):
 t=random.random();yy=-.051+t*.159;rootx=random.uniform(.014,.045);rootz=.114-.040*t
 lift=random.uniform(.005,.022);endx=random.uniform(-.098,-.071);endz=random.uniform(.034,.083)-t*.045
 pts=[(rootx,yy,rootz),(rootx-.037,yy-.012,.131-t*.032+lift),(-.058,yy-.019,.115-t*.045), (endx,yy+.003,endz+.017),(endx-random.uniform(.004,.016),yy+random.uniform(-.01,.022),endz)]
 lock('Tousled left layered lock',pts,random.uniform(.004,.009),hairlight if i%9==0 else hair,.1)
for i in range(55):
 t=random.random();yy=-.037+t*.15
 pts=[(.024,yy,.115-t*.035),(.062,yy-.005,.127-t*.035+random.uniform(0,.012)),(.087,yy+.003,.094-t*.04),(.098,yy+.013,.051-t*.036),(.105+random.uniform(-.01,.009),yy+.005,.025-t*.04)]
 lock('Tousled right lock',pts,random.uniform(.004,.008),hairlight if i%10==0 else hair,.12)
for s in [-1,1]:
 for i in range(56):
  t=random.random();yy=.013+t*.095;zz=.07-random.random()*.045
  pts=[(s*.071,yy,zz),(s*.090,yy+.008,zz-.015),(s*.098,yy+.013,zz-.029),(s*.099,yy+.023,zz-.045),(s*(.103+random.uniform(-.011,.006)),yy+.006,zz-.062)]
  lock('Broken side waves',pts,random.uniform(.004,.008),hairlight if i%12==0 else hair,.1)
for i in range(95):
 x=random.uniform(-.078,.078);z=random.uniform(.079,.12);side=1 if x>0 else -1
 pts=[]
 for k,(yy,zz) in enumerate([(.054,z),(.093,z-.020),(.110,z-.055),(.109,z-.095),(.11,z-.122)]):
  seed=Vector((x+side*.005*math.sin(k*1.7),yy,zz));near=tree.find_nearest(seed)
  p=near[0]+near[1]*random.uniform(.003,.010) if near[0] else seed
  if k==4:p.z-=random.uniform(.006,.018);p.y+=.003
  pts.append(p)
 lock('Back layered wave',pts,random.uniform(.004,.008),hairlight if i%12==0 else hair,.1)
# Directional painted color breakup keeps wide hair pieces from reading as plastic strips.
for ma in [hair,hairlight]:
 n=ma.node_tree.nodes;l=ma.node_tree.links;uv=n.new('ShaderNodeTexCoord');mult=n.new('ShaderNodeVectorMath');mult.operation='MULTIPLY';mult.inputs[1].default_value=(35,1.2,1);l.new(uv.outputs['UV'],mult.inputs[0])
 noise=n.new('ShaderNodeTexNoise');noise.inputs['Scale'].default_value=3;noise.inputs['Detail'].default_value=2;l.new(mult.outputs[0],noise.inputs['Vector'])
 ramp=n.new('ShaderNodeValToRGB');col=ma.diffuse_color[:3];ramp.color_ramp.elements[0].position=.22;ramp.color_ramp.elements[0].color=(*(c*.4 for c in col),1);ramp.color_ramp.elements[1].position=.8;ramp.color_ramp.elements[1].color=(*(c*1.8 for c in col),1);l.new(noise.outputs['Fac'],ramp.inputs[0]);l.new(ramp.outputs[0],n['Principled BSDF'].inputs['Base Color'])
# A few foreground unruly strands crossing the temples, not a symmetric wig.
for s,off in [(-1,0),(-1,.012),(1,.005)]:
 pts=[(s*.056,-.060+off,.13),(s*.078,-.085+off,.098),(s*.069,-.099+off,.062),(s*.078,-.092+off,.035)]
 lock('Loose temple lock',pts,.006,hair,.2)
# Clean the open neck into a level presentation cut.
bm=bmesh.new();bm.from_mesh(head.data)
bmesh.ops.bisect_plane(bm,geom=list(bm.verts)+list(bm.edges)+list(bm.faces),dist=.00001,plane_co=(0,0,-.175),plane_no=(0,0,1),clear_inner=True,clear_outer=False)
edges=[e for e in bm.edges if e.is_boundary and all(abs(v.co.z+.175)<.0001 for v in e.verts)]
if edges:bmesh.ops.holes_fill(bm,edges=edges,sides=0)
bm.to_mesh(head.data);bm.free()
# Pack supplied reference images. No new image generation is used in this revision.
for i,p in enumerate((OUT.parent/'References').glob('*.png')):
 im=bpy.data.images.load(str(p));im.pack();o=bpy.data.objects.new('REFERENCE '+p.stem,None);sc.collection.objects.link(o);o.empty_display_type='IMAGE';o.data=im;o.empty_display_size=.7;o.location=(1+i,0,0);o.rotation_euler=(math.pi/2,0,0);o.hide_render=True
def aim(o,target):o.rotation_euler=(Vector(target)-o.location).to_track_quat('-Z','Y').to_euler()
w=bpy.data.worlds.new('Neutral charcoal');sc.world=w;w.use_nodes=True;w.node_tree.nodes['Background'].inputs[0].default_value=(.025,.030,.035,1);w.node_tree.nodes['Background'].inputs[1].default_value=.35
for name,pos,power,size,col in [('Key',(-.7,-1,.8),65,.65,(1,.87,.73)),('Fill',(.8,-.7,.3),25,.8,(.72,.82,1)),('Rim',(.4,.6,.7),100,.5,(1,.73,.50))]:
 d=bpy.data.lights.new(name,'AREA');d.energy=power*.4;d.shape='DISK';d.size=size;d.color=col;o=bpy.data.objects.new(name,d);sc.collection.objects.link(o);o.location=pos;aim(o,(0,0,-.05))
d=bpy.data.cameras.new('Portrait camera');cam=bpy.data.objects.new('Portrait camera',d);sc.collection.objects.link(cam);sc.camera=cam;d.type='ORTHO'
sc.render.engine='CYCLES';sc.cycles.samples=48;sc.cycles.use_denoising=True;sc.view_settings.view_transform='AgX';sc.render.resolution_x=1050;sc.render.resolution_y=1200;sc.render.resolution_percentage=100
sc['status']='v03 head foundation study; original face geometry replaced with adapted CC0 anatomical base. Not rigged or integrated into Unity.'
sc['base_source']='MakeHuman CC0 graphical assets; see Source/LICENSE.md and SOURCE.md'
for name,loc in [('three-quarter',(.42,-1.5,.12)),('front',(0,-1.5,.04)),('profile',(1.5,-.10,.02))]:
 cam.location=loc;aim(cam,(0,0,-.032));d.ortho_scale=.41;sc.render.filepath=str(OUT/(name+'.png'));bpy.ops.render.render(write_still=True)
cam.location=(.42,-1.5,.12);aim(cam,(0,0,-.032));d.ortho_scale=.41
bpy.ops.wm.save_as_mainfile(filepath=str(OUT/'Garrick_Head_Foundation_v03.blend'))
(OUT/'verification.json').write_text(json.dumps({'blender':bpy.app.version_string,'base_vertices':len(head.data.vertices),'base_faces':len(head.data.polygons),'anatomical_base':'MakeHuman CC0','morph_weights':weights,'render_source':'actual Blender Cycles geometry','face_texture_projection':'provisional UV registration of existing generated v02 albedo; geometric eyeballs and features','rigged':False,'unity_installed':False},indent=2))
