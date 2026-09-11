import bpy,json,math
from pathlib import Path
from mathutils import Vector
out=Path(__file__).resolve().parent
bpy.ops.wm.open_mainfile(filepath=str(out/'Garrick_Rigged_Candidate.blend'))
rig=bpy.data.objects['Garrick — deformation skeleton'];meshes=[o for o in rig.children if o.type=='MESH']
checks=[]
def check(value,label):
 checks.append({'pass':bool(value),'check':label})
 if not value:raise RuntimeError(label)
check(len(rig.data.bones)==65,'single retained 65-bone character skeleton')
check(len([o for o in bpy.context.scene.objects if o.type=='ARMATURE'])==1,'no duplicate imported rigs')
check(all(math.isfinite(c) for o in meshes for v in o.data.vertices for c in v.co),'finite character geometry')
check(all(len(o.data.vertices)>0 for o in meshes),'no empty character meshes')
check(all(m.object==rig for o in meshes for m in o.modifiers if m.type=='ARMATURE'),'all skinning modifiers target the retained rig')
for img in bpy.data.images:
 if img.source=='FILE':
  img.reload()
  if len(img.pixels):sample=img.pixels[0]
check(all(img.has_data for img in bpy.data.images if img.source=='FILE'),'all referenced image textures load from saved project')
bpy.context.scene.frame_set(1);bpy.context.view_layer.update();first=rig.pose.bones['Head'].matrix.translation.copy()
bpy.context.scene.frame_set(31);bpy.context.view_layer.update();last=rig.pose.bones['Head'].matrix.translation.copy()
check((first-last).length>.001,'breathing action deforms the live skeleton')
bpy.context.scene.frame_set(1)
for o in meshes:o.data.calc_loop_triangles()
bpy.ops.file.pack_all()
bpy.ops.wm.save_as_mainfile(filepath=str(out/'Garrick_Rigged_Candidate.blend'))
(out/'verification.json').write_text(json.dumps({'checks':checks,'mesh_objects':len(meshes),'triangles':sum(len(o.data.loop_triangles) for o in meshes),'bones':len(rig.data.bones),'textures_packed':True,'unity_integrated':False,'visual_approval':False},indent=2))
print('GARRICK_CANDIDATE_VERIFIED',len(checks))
