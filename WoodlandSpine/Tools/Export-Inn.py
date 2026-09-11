"""Export the approved Blender study into a baked, axis-explicit Unity asset.
Run Blender --background <v02.blend> --python Tools/Export-Inn.py.
The source .blend is never overwritten. No third party import package is needed.
"""
import bpy, os, json, struct, math, hashlib
from mathutils import Vector

OUT=os.path.abspath(os.path.join(os.path.dirname(__file__),'../Assets/Art/Inn'))
os.makedirs(OUT,exist_ok=True)
scene=bpy.context.scene
for c in bpy.data.collections: c.hide_viewport=False; c.hide_render=False
for lc in bpy.context.view_layer.layer_collection.children: lc.hide_viewport=False; lc.exclude=False
# The presentation had a closed wall behind its door leaf. Cut the real passage
# in the export copy so the open door connects to the authored basement stairs.
wall=bpy.data.objects.get('Basement stair enclosure')
bpy.ops.mesh.primitive_cube_add(size=1,location=(-7.5,3.55,1.15))
cutter=bpy.context.object;cutter.dimensions=(1.35,.8,2.5)
bpy.ops.object.transform_apply(location=False,rotation=False,scale=True)
mod=wall.modifiers.new('Gameplay doorway opening','BOOLEAN');mod.operation='DIFFERENCE';mod.object=cutter
bpy.context.view_layer.objects.active=wall;bpy.ops.object.modifier_apply(modifier=mod.name)
bpy.data.objects.remove(cutter,do_unlink=True)
# Walkability tuning discovered with Unity's actual character capsule: the
# original case pinched the bar-side service route; the spare chair pinched
# the kitchen/basement branch. Move geometry and collision together.
weapon_parts=['Public weapon case','Weapon rack backing','Representative weapon shaft','Plain steel weapon head','Sword guard','Representative shortbow','Bowstring']
for o in list(scene.objects):
    if any(o.name.startswith(n) for n in weapon_parts):o.location.y-=2.15
    if o.name.startswith('Chair /') and abs(o.location.x+5.1)<.4 and abs(o.location.y-2.65)<.4:
        d=o.location-Vector((-5.1,2.65,0));o.location=Vector((-6.25,1.9,0))+Vector((-d.y,d.x,d.z));o.rotation_euler.z+=math.pi/2
scene.render.engine='CYCLES';scene.cycles.samples=1
scene.render.bake.use_pass_direct=False;scene.render.bake.use_pass_indirect=False;scene.render.bake.use_pass_color=True
scene.render.bake.margin=8
scene.render.bake.target='IMAGE_TEXTURES'
deps=bpy.context.evaluated_depsgraph_get()
groups={}; collisions=[]; materials=[]; output=[]

def group(o):
    col=o.users_collection[0].name[:2] if o.users_collection else ''
    n=o.name
    if col in ['07','08']: return None
    if n.startswith('Marlow harmless sample'): return None
    if col=='04':
        if n.startswith('Marlow'): return None # Gameplay actor retains the existing moving proxy.
        if n.startswith('Sylvie'): return 'Kitchen'
        for i,prefix in enumerate(['Hearth local A','Hearth local B','Eating patron','Traveler','Bar regular','Communal patron']):
            if n.startswith(prefix): return 'Patron'+str(i)
        return 'Common'
    if n.startswith('Private basement door') and not any(s in n for s in ['jamb','lintel']): return 'BasementDoor'
    if n.startswith('Sylvie kitchen threshold') and not any(s in n for s in ['jamb','lintel']): return 'KitchenDoor'
    if col=='05' or o.location.z>3.64 and col in ['09','10']: return 'Upper'
    if col=='02': return 'Kitchen'
    if col=='06': return 'Cutaway'
    if n.startswith('Basement descending') or n.startswith('Basement bottom'): return 'BasementStairs'
    if col=='03': return 'UpperStairs'
    return 'Common'

# Coarse furniture shapes only; floor/stair/wall collision uses evaluated meshes.
solid_names=['footprint','foundation','floor board','floor beside','plaster wall','cutaway sill',
 'Front wall /','East full wall','Basement stair enclosure','Basement enclosed',
 'Kitchen back extension','Kitchen west wall','Kitchen front west','Kitchen front pass',
 'Service passage divider','Kitchen masonry range','Kitchen prep block','Bar / panelled',
 'Bar service cabinet','Public board end','Public weapon case','Weapon rack backing',
 'Hearth jamb','Hearth lintel','Table / joined','Chair / seat','Traveler pack','Marlow satchel',
 'Upstairs first flight','Upstairs second flight','Upstairs turning','Basement descending',
 'Basement bottom','Upper deck','Upper rear hall deck','Upper front deck','Upper landing',
 'Guest room exterior','Upper rear wall','Guest room partition','Guest hall wall','Guest bed frame',
 'Player personal storage chest','Guest travel chest','Bedside cabinet','Main oak post',
 'Rear room cross beam','Cask body','Hearth stone pad','Leather chair']

for o in list(scene.objects):
    if o.type not in ['MESH','CURVE']: continue
    g=group(o)
    if g is None: continue
    ev=o.evaluated_get(deps);m=bpy.data.meshes.new_from_object(ev)
    if not m.vertices: bpy.data.meshes.remove(m);continue
    # Preserve each source object's Generated coordinates through joining/baking.
    attr=m.attributes.new('SourceGenerated','FLOAT_VECTOR','POINT')
    lo=Vector(tuple(min(v.co[i] for v in m.vertices) for i in range(3)))
    hi=Vector(tuple(max(v.co[i] for v in m.vertices) for i in range(3)))
    for v in m.vertices: attr.data[v.index].vector=tuple((v.co[i]-lo[i])/max(hi[i]-lo[i],.00001) for i in range(3))
    m.transform(o.matrix_world)
    cp=bpy.data.objects.new(o.name+' export',m);scene.collection.objects.link(cp)
    groups.setdefault(g,[]).append(cp)
    if any(s.lower() in o.name.lower() for s in solid_names):
        m.calc_loop_triangles()
        collisions.append((g,o.name,[(v.co.x,v.co.z,v.co.y) for v in m.vertices],[(t.vertices[0],t.vertices[2],t.vertices[1]) for t in m.loop_triangles]))
    o.hide_render=True;o.hide_set(True)

for g,obs in groups.items():
    bpy.ops.object.select_all(action='DESELECT')
    for o in obs:o.hide_set(False);o.select_set(True)
    bpy.context.view_layer.objects.active=obs[0];bpy.ops.object.join();o=bpy.context.object;o.name=g
    # Geometry joining reduces thousands of source pieces to a handful of renderers.
    bpy.ops.object.mode_set(mode='EDIT');bpy.ops.mesh.select_all(action='SELECT')
    bpy.ops.uv.smart_project(angle_limit=math.radians(66),island_margin=.004)
    bpy.ops.object.mode_set(mode='OBJECT')
    size=2048 if g in ['Common','Upper'] else 256 if g.startswith('Patron') else 1024
    img=bpy.data.images.new(g+' Albedo',width=size,height=size)
    local=[]
    for slot in o.material_slots:
        source=slot.material or bpy.data.materials['Warm lime plaster']
        mat=source.copy();slot.material=mat;mat.use_nodes=True
        nodes=mat.node_tree.nodes;links=mat.node_tree.links
        for node in list(nodes):
            if node.type=='TEX_COORD':
                a=nodes.new('ShaderNodeAttribute');a.attribute_name='SourceGenerated'
                for l in list(node.outputs['Generated'].links): links.new(a.outputs['Vector'],l.to_socket)
        tex=nodes.new('ShaderNodeTexImage');tex.image=img;nodes.active=tex
        p=next((n for n in nodes if n.type=='BSDF_PRINCIPLED'),None)
        emit=list(p.inputs['Emission Color'].default_value) if p else [0,0,0,1]
        strength=p.inputs['Emission Strength'].default_value if p else 0
        local.append(len(materials));materials.append(dict(name=mat.name,texture=g+'-Albedo.png',metal=p.inputs['Metallic'].default_value if p else 0,rough=p.inputs['Roughness'].default_value if p else .8,emission=[v*strength for v in emit[:3]]))
    print('BAKING',g,len(o.data.polygons),flush=True)
    bpy.ops.object.bake(type='DIFFUSE')
    img.filepath_raw=os.path.join(OUT,g+'-Albedo.png');img.file_format='PNG';img.save()
    m=o.data;m.calc_loop_triangles();uv=m.uv_layers.active.data
    verts=[];sub={}
    # Split corners retains Blender's weighted normals and the atlas UV seams.
    for t in m.loop_triangles:
        ix=[]
        for li in [t.loops[0],t.loops[2],t.loops[1]]:
            loop=m.loops[li];v=m.vertices[loop.vertex_index];n=m.corner_normals[li].vector
            ix.append(len(verts));verts.append((v.co.x,v.co.z,v.co.y,n.x,n.z,n.y,uv[li].uv.x,uv[li].uv.y))
        sub.setdefault(local[t.material_index],[]).extend(ix)
    output.append((g,verts,sub));o.hide_render=True

def string(f,s):
    b=s.encode('utf-8');f.write(struct.pack('<i',len(b)));f.write(b)
with open(os.path.join(OUT,'GarricksInn.innmodel'),'wb') as f:
    string(f,json.dumps(dict(version=1,source='GarricksInn_Visual_v02.blend',materials=materials)))
    f.write(struct.pack('<i',len(output)))
    for name,verts,sub in output:
        string(f,name);f.write(struct.pack('<i',len(verts)))
        for v in verts:f.write(struct.pack('<8f',*v))
        f.write(struct.pack('<i',len(sub)))
        for mat,indices in sub.items():f.write(struct.pack('<ii',mat,len(indices)));f.write(struct.pack('<%di'%len(indices),*indices))
    f.write(struct.pack('<i',len(collisions)))
    for g,name,verts,tris in collisions:
        string(f,g);string(f,name);f.write(struct.pack('<i',len(verts)))
        for v in verts:f.write(struct.pack('<3f',*v))
        indices=[v for t in tris for v in t];f.write(struct.pack('<i',len(indices)));f.write(struct.pack('<%di'%len(indices),*indices))
print('INN_EXPORT_COMPLETE',len(output),'render groups',len(collisions),'collision meshes',flush=True)
