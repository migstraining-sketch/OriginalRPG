import bpy,json,math
from pathlib import Path
ROOT=Path(__file__).resolve().parents[4]/'outputs/character-assets';OUT=ROOT/'Garrick-CC5-v01'
bpy.ops.wm.open_mainfile(filepath=str(OUT/'Garrick_CC5_Candidate.blend'))
rig=bpy.data.objects['Garrick | CC humanoid'];body=bpy.data.objects['CC_Base_Body'];beard=bpy.data.objects['Garrick | face-following beard'];scene=bpy.context.scene
checks=[]
def check(value,label):
    checks.append({'pass':bool(value),'check':label})
    if not value:raise RuntimeError(label)
meshes=[o for o in rig.children if o.type=='MESH' and not o.hide_render]
check(len([o for o in scene.objects if o.type=='ARMATURE'])==1,'single CC skeleton; source fitting skeleton removed')
check(len(rig.data.bones)==101,'all 101 imported bones retained')
check(len(body.data.shape_keys.key_blocks)==161,'all 160 facial shapes retained alongside Basis')
check(len(beard.data.shape_keys.key_blocks)==161,'new beard inherits all facial shapes')
check(all(any(m.type=='ARMATURE' and m.object==rig for m in o.modifiers) for o in meshes),'all visible character meshes bound to CC skeleton')
check(all(math.isfinite(c) for o in meshes for v in o.data.vertices for c in v.co),'finite character geometry')
for o in meshes:
    if o==body:continue
    check(all(abs(sum(g.weight for g in v.groups)-1)<.02 for v in o.data.vertices),'normalized weights: '+o.name)
for img in bpy.data.images:
    if img.source=='FILE':
        _=img.pixels[0] if len(img.pixels) else None
check(all(img.packed_file and img.has_data for img in bpy.data.images if img.source=='FILE'),'saved blend reloads packed image textures')
def positions(obj):
    bpy.context.view_layer.update();o=obj.evaluated_get(bpy.context.evaluated_depsgraph_get());return [o.matrix_world@v.co for v in o.data.vertices]
scene.frame_set(1);vest=next(o for o in meshes if o.name.startswith('Vest | left front'));sleeve=next(o for o in meshes if o.name.startswith('Shirt | rolled sleeves'))
rest=positions(sleeve);scene.frame_set(91);moved=positions(sleeve)
check(max((a-b).length for a,b in zip(rest,moved))>.05,'fitted sleeve deforms in reaching pose')
scene.frame_set(1)
key=body.data.shape_keys.key_blocks['V_Open'];rest=positions(beard);key.value=.55;moved=positions(beard)
check(abs(beard.data.shape_keys.key_blocks['V_Open'].value-.55)<.001,'beard expression driver follows body')
check(max((a-b).length for a,b in zip(rest,moved))>.001,'beard geometry deforms with mouth shape')
key.value=0
for o in meshes:o.data.calc_loop_triangles()
report={'checks':checks,'bones':len(rig.data.bones),'mesh_objects':len(meshes),'triangles':sum(len(o.data.loop_triangles) for o in meshes),'facial_shapes':160,'unity_integrated':False,'visual_approval':False,'validation_scope':'Saved-file structural checks and limited idle/reach/mouth fit tests, not full animation QA.'}
(OUT/'verification.json').write_text(json.dumps(report,indent=2))
print('CC_GARRICK_VERIFIED',len(checks))
