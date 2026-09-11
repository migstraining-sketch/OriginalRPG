"""Reference correction and strand grooming study; builds on the v03 anatomical mesh."""
import bpy, math, random, json
from pathlib import Path
from mathutils import Vector
from mathutils.bvhtree import BVHTree
random.seed(461)
OUT=Path(__file__).resolve().parent
bpy.ops.wm.open_mainfile(filepath=str(OUT.parent/'v03/Garrick_Head_Foundation_v03.blend'))
sc=bpy.context.scene
head=bpy.data.objects['Garrick | anatomical face neck shoulders']
# Remove only the previous study's generated hair components from this new file.
prefixes=('Tousled','Broken side','Back layered','Loose temple','Scalp under','Beard edge','Brow hair','Heavy expressive')
for o in list(bpy.data.objects):
 if o.name.startswith(prefixes):bpy.data.objects.remove(o,do_unlink=True)
def smooth(a,b,x):
 t=max(0,min(1,(x-a)/(b-a)));return t*t*(3-2*t)
for v in head.data.vertices:
 p=v.co
 # Bring forehead/crown down without moving the eye sockets or nose.
 if p.z>.026:p.z=.026+(p.z-.026)*.76
 # More substantial lower jaw, less taper from cheek to chin.
 jaw=math.exp(-((p.z+.10)/.033)**2)
 p.x*=1+.052*jaw
 # Subtle eye-opening refinement preserves the original connected eyelid loops.
 eye=math.exp(-((abs(p.x)-.035)/.017)**2-((p.z-.002)/.010)**2)
 if p.y<-.068 and p.z>0:p.z+=.0014*eye
 # Reduce the triangular point at the lower nose tip.
 nose=math.exp(-(p.x/.019)**2-((p.z+.048)/.014)**2)
 if p.y<-.11:p.y+=.0025*nose
head.data.update()
skin=head.data.materials[0];paint=head.data.color_attributes['Complexion'];mask=head.data.color_attributes['Albedo blend']
for v in head.data.vertices:
 x,y,z=v.co;ax=abs(x)
 # Fade the face image before the temple/ear, preventing stretched painted ears.
 theta=abs(math.atan2(x,-y+.015))
 f=(1-smooth(.55,.95,theta))*(1-smooth(-.034,.0,y))*smooth(-.145,-.12,z)*(1-smooth(.055,.079,z))
 mask.data[v.index].color=(f,f,f,1)
 # The side complexion/beard is painted directly on the mesh, independent of front projection.
 side=1-smooth(-.017,.02,y)
 edge=-.074+min(ax/.086,1)*.080
 density=(1-smooth(edge-.012,edge+.008,z))*smooth(-.15,-.116,z)*side
 mouth=(1-smooth(.026,.037,ax))*smooth(-.099,-.088,z)*(1-smooth(-.063,-.052,z))
 density*=1-mouth
 col=Vector((.36,.205,.14)).lerp(Vector((.048,.034,.027)),density*.88)
 # Rear scalp darkens under the groom without a separate jagged cap shell.
 scalp_edge=.064 if y<-.025 else (.025 if y<.02 else -.044)
 ear=ax>.081 and -.05<z<.029 and y<.034
 scalp=smooth(scalp_edge-.008,scalp_edge+.007,z)*(0 if ear else 1)
 col=col.lerp(Vector((.027,.018,.014)),scalp)
 paint.data[v.index].color=(*col,1)
sp=skin.node_tree.nodes.get('Principled BSDF');sp.inputs['Roughness'].default_value=.68;sp.inputs['Specular IOR Level'].default_value=.22
for o in bpy.data.objects:
 if o.name.startswith('Hazel iris'):o.scale.x*=.84;o.scale.z*=.84
 if o.name.startswith('Pupil'):o.scale.x*=.8;o.scale.z*=.8
for name,col in [('Eyes | ivory',(.38,.345,.28)),('Eyes | grey hazel',(.11,.105,.072))]:
 m=bpy.data.materials.get(name)
 if m:m.node_tree.nodes['Principled BSDF'].inputs['Base Color'].default_value=(*col,1)
bpy.context.view_layer.update();deps=bpy.context.evaluated_depsgraph_get();tree=BVHTree.FromObject(head,deps)
ev=head.evaluated_get(deps);surface=ev.to_mesh();surface.calc_loop_triangles()
triangles=[];beardtris=[]
for t in surface.loop_triangles:
 pts=[surface.vertices[i].co.copy() for i in t.vertices];c=sum(pts,Vector())/3
 ear=abs(c.x)>.081 and -.052<c.z<.033 and c.y<.037
 if c.z>(.057 if c.y<-.023 else (.020 if c.y<.02 else -.048)) and c.z<.115 and not ear:
  triangles.append(pts)
 x=abs(c.x);edge=-.071+min(x/.083,1)*.082
 if -.135<c.z<edge and c.y<.005 and not ear and not (x<.033 and -.094<c.z<-.058):beardtris.append(pts)
ev.to_mesh_clear()
def sampled(tris):
 # Sampling by area prevents tiny ear polygons receiving most of the groom.
 weights=areaweights[id(tris)]
 a,b,c=random.choices(tris,cum_weights=weights,k=1)[0];u=random.random();v=random.random()
 if u+v>1:u=1-u;v=1-v
 return a+(b-a)*u+(c-a)*v
areaweights={}
for tris in [triangles,beardtris]:
 cumulative=[];total=0
 for a,b,c in tris:total+=(b-a).cross(c-a).length*.5;cumulative.append(total)
 areaweights[id(tris)]=cumulative
def material(name,color,rough=.7):
 m=bpy.data.materials.new(name);m.diffuse_color=(*color,1);m.use_nodes=True
 p=m.node_tree.nodes.get('Principled BSDF');p.inputs['Base Color'].default_value=(*color,1);p.inputs['Roughness'].default_value=rough;p.inputs['Specular IOR Level'].default_value=.3
 return m
hairmats=[material('Groom | deep brown',(.023,.012,.007),.6),material('Groom | chestnut',(.046,.024,.012),.63),material('Groom | warm strands',(.069,.038,.020),.67),material('Groom | sparse silver',(.16,.135,.105),.75)]
groom=bpy.data.collections.new('GARRICK v04 | editable strand groom');sc.collection.children.link(groom)
def curve_object(name,ma,radius):
 d=bpy.data.curves.new(name,'CURVE');d.dimensions='3D';d.resolution_u=1;d.bevel_depth=radius;d.bevel_resolution=0;d.resolution_u=1
 o=bpy.data.objects.new(name,d);groom.objects.link(o);d.materials.append(ma);return o
hairgroups=[curve_object('Wavy groom '+str(i),ma,.000105) for i,ma in enumerate(hairmats)]
beardgroups=[curve_object('Beard groom '+str(i),ma,.000075) for i,ma in enumerate(hairmats)]
def add_strand(obj,pts,scale=1):
 s=obj.data.splines.new('POLY');s.points.add(len(pts)-1)
 for j,(p,co) in enumerate(zip(s.points,pts)):
  p.co=(*co,1);p.radius=scale*(1-j/(len(pts)-.5))**.65
def projection(p,offset):
 hit=tree.find_nearest(p)
 return hit[0]+hit[1]*offset if hit[0] is not None else p
guides=[]
for k in range(520):
 root=sampled(triangles);side=-1 if root.x<.025 else 1
 if root.y<-.022:direction=Vector((side*.067,.032,-.024))
 elif root.z>.065:direction=Vector((side*.035,.061,-.036))
 else:direction=Vector((side*.004,.024,-.064))
 length=random.uniform(.70,1.22);lift=random.uniform(.009,.020);phase=random.random()*math.tau
 guide=[]
 for j in range(20):
  t=j/19;p=root+direction*(t*length)
  p=projection(p,.0003+lift*math.sin(math.pi*t*.93))
  # Wavy variation is strongest away from the root, giving soft, broken ends.
  p+=Vector((.004*math.sin(t*math.tau*1.15+phase),.002*math.sin(t*math.tau+phase),.002*math.sin(t*math.tau*1.8+phase)))*math.sin(math.pi*t/2)
  if j==19:p+=direction.normalized()*random.uniform(.001,.005)
  guide.append(p)
 guides.append(guide)
 for follower in range(32):
  offset=Vector((random.gauss(0,.0018),random.gauss(0,.0018),random.gauss(0,.0011)))
  pts=[]
  for j,p in enumerate(guide):
   t=j/19;co=p+offset*(.8+.5*math.sin(math.pi*t))
   co+=Vector((math.sin(t*15+phase)*.00025,math.cos(t*17+phase)*.00025,0))*t
   if j==0:co=projection(co,.0001)
   pts.append(co)
  r=random.random();index=0 if r<.72 else (1 if r<.94 else (2 if r<.998 else 3))
  add_strand(hairgroups[index],pts,random.uniform(.65,1.2))
# Short surface-following undercoat fills gaps between the larger wavy groups.
for i in range(10000):
 root=sampled(triangles);side=-1 if root.x<.025 else 1
 direction=Vector((side*.010,.009,-.007)) if root.y<.025 else Vector((side*.002,.006,-.013))
 pts=[]
 for j in range(7):
  t=j/6;pts.append(projection(root+direction*t,.0002+.002*math.sin(math.pi*t)))
 add_strand(hairgroups[0 if random.random()<.85 else 1],pts,random.uniform(.8,1.15))
# Dense short beard fibers over the actual jaw surface, plus longer chin stubble.
for i in range(7200):
 root=sampled(beardtris);hit=tree.find_nearest(root);normal=hit[1]
 length=random.uniform(.0015,.0045) if root.z>-.087 else random.uniform(.003,.008)
 direction=Vector((root.x*.10,-.0005,-1)).normalized()
 pts=[]
 for j in range(6):
  t=j/5;p=root+direction*length*t+normal*(.0001+.0012*math.sin(math.pi*t*.8));pts.append(p)
 r=random.random();index=0 if r<.80 else (1 if r<.95 else (2 if r<.982 else 3))
 add_strand(beardgroups[index],pts,random.uniform(.7,1.1))
# A fine moustache follows the upper lip rather than making a solid tube.
for s in [-1,1]:
 for i in range(450):
  x=s*random.uniform(.001,.034);z=random.uniform(-.065,-.060)-abs(x)*.20
  hit=tree.ray_cast(Vector((x,-.5,z)),Vector((0,1,0)))
  if hit[0] is None:continue
  root=hit[0];pts=[]
  for j in range(6):
   t=j/5;pts.append(root+Vector((s*.006*t,-.0008*math.sin(math.pi*t),-.0035*t)))
  add_strand(beardgroups[0 if random.random()<.85 else 1],pts,.8)
# Reduce the overlarge forehead from the earlier camera composition, not by cropping it away.
cam=sc.camera;cam.data.ortho_scale=.38
def aim(o,target):o.rotation_euler=(Vector(target)-o.location).to_track_quat('-Z','Y').to_euler()
sc.render.resolution_x=1000;sc.render.resolution_y=1100;sc.cycles.samples=40
sc['status']='v04 groom and proportion correction. Head-only art study, not approved or installed in Unity.'
sc['hair_method']='Actual editable 3D strands; no generated image used as a rendered model substitute.'
for name,pos in [('three-quarter',(.45,-1.5,.09)),('front',(0,-1.5,.035)),('profile',(1.5,-.08,.02))]:
 cam.location=pos;aim(cam,(0,0,-.035));sc.render.filepath=str(OUT/(name+'.png'));bpy.ops.render.render(write_still=True)
cam.location=(.45,-1.5,.09);aim(cam,(0,0,-.035))
bpy.ops.wm.save_as_mainfile(filepath=str(OUT/'Garrick_Head_Groom_v04.blend'))
(OUT/'verification.json').write_text(json.dumps({'blender':bpy.app.version_string,'base_version':'v03','head_vertices':len(head.data.vertices),'hair_strands':sum(len(o.data.splines) for o in hairgroups),'beard_strands':sum(len(o.data.splines) for o in beardgroups),'render_source':'actual Cycles scene','rigged':False,'unity_installed':False},indent=2))
