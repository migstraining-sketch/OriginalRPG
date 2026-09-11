"""Saved-file checks for reuse, normalized skinning, textures and live deformation."""
import bpy,json,math
from pathlib import Path
OUT=Path(__file__).resolve().parent
bpy.ops.wm.open_mainfile(filepath=str(OUT/'Workwear_Foundation.blend'))
scene=bpy.context.scene
a=bpy.data.objects['Garrick | shared humanoid rig']
b=bpy.data.objects['Fitting test | shared humanoid rig']
checks=[]
def check(value,label):
    checks.append({'pass':bool(value),'check':label})
    if not value:raise RuntimeError(label)
check(len([o for o in scene.objects if o.type=='ARMATURE'])==2,'two independently positioned assembled characters')
check(a.data==b.data and len(a.data.bones)==65,'same 65-bone skeleton definition')
check(a.animation_data.action==b.animation_data.action,'same action datablock drives both actors')
am=[o for o in a.children if o.type=='MESH'];bm=[o for o in b.children if o.type=='MESH']
shared=[o for o in bm if any(o.data==x.data for x in am)]
check(len(shared)==len(bm),'every second-actor mesh reuses first-actor geometry')
check(len(shared)>=15,'reuse includes a substantial modular assembly')
check(all(all(s.material is not None for s in o.material_slots) for o in am+bm),'no missing material slots on either actor')
for rig,meshes in [(a,am),(b,bm)]:
    check(all(any(m.type=='ARMATURE' and m.object==rig for m in o.modifiers) for o in meshes),'all meshes deform with their own actor: '+rig.name)
    check(all(math.isfinite(c) for o in meshes for v in o.data.vertices for c in v.co),'finite geometry: '+rig.name)
    for o in meshes:
        if not o.name.startswith(('Vest |','Apron |','Shirt | folded','Fit test | Vest')):continue
        check(all(abs(sum(g.weight for g in v.groups)-1)<.001 for v in o.data.vertices),'normalized garment weights: '+o.name)
        check(all(g.name in rig.data.bones for g in o.vertex_groups),'valid garment bone names: '+o.name)
for img in bpy.data.images:
    if img.source=='FILE':
        if not img.has_data:img.reload()
        _=img.pixels[0] if len(img.pixels) else None
check(all(img.has_data and img.packed_file for img in bpy.data.images if img.source=='FILE'),'all image textures packed and readable')
vest=bpy.data.objects['Vest | left front']
def snapshot(frame):
    scene.frame_set(frame);bpy.context.view_layer.update()
    obj=vest.evaluated_get(bpy.context.evaluated_depsgraph_get())
    return [obj.matrix_world@v.co for v in obj.data.vertices]
idle=snapshot(1);reach=snapshot(91)
check(max((x-y).length for x,y in zip(idle,reach))>.001,'clothing vertices actually move in the reaching fit test')
scene.frame_set(1)
report={'checks':checks,'shared_meshes':len(shared),'first_actor_meshes':len(am),'second_actor_meshes':len(bm),'shared_skeleton_bones':len(a.data.bones),'shared_action':a.animation_data.action.name,'unity_integrated':False,'visual_approval':False,'limits':['One masculine base and two proportion profiles, not a complete cast library.','Reaching fit test and idle only; no walking, combat or facial animation acceptance.','Procedural materials require baking or engine material recreation.']}
(OUT/'verification.json').write_text(json.dumps(report,indent=2))
print('FOUNDATION_VERIFIED',len(checks),'checks;',len(shared),'shared mesh datablocks')
