import bpy
from pathlib import Path
from mathutils import Vector
ROOT=Path(__file__).resolve().parents[4]/'outputs/character-assets';OUT=ROOT/'Garrick-CC5-v01'
bpy.ops.wm.open_mainfile(filepath=str(OUT/'Garrick_CC5_Candidate.blend'))
scene=bpy.context.scene;rig=bpy.data.objects['Garrick | CC humanoid'];rig.location+=(Vector((0,3.22,.025)))
bpy.data.objects['Studio ground'].hide_render=True
path=Path(__file__).resolve().parents[2]/'Inn/GarricksInn_Visual_v02.blend'
with bpy.data.libraries.load(str(path),link=False) as (src,dst):dst.objects=[n for n in src.objects if not any(n.startswith(p) for p in ['Garrick','Upper','Upstairs','Guest','Player personal','Bedside'])]
for o in dst.objects:
    if o and o.type in {'MESH','CURVE'} and o.location.z<3.5:scene.collection.objects.link(o);o.hide_render=False
for o in scene.objects:
    if o.type=='LIGHT':o.location.y+=3.22
cam=scene.camera;cam.location=(2.5,-3.8,3.2);cam.rotation_euler=(Vector((0,3.16,1.15))-cam.location).to_track_quat('-Z','Y').to_euler();cam.data.ortho_scale=3.0
scene.render.resolution_x=1300;scene.render.resolution_y=1000;scene.cycles.samples=32
bpy.ops.wm.save_as_mainfile(filepath=str(OUT/'Garrick_Inn_Context.blend'))
scene.render.filepath=str(OUT/'garrick-inn.png');bpy.ops.render.render(write_still=True)
print('CC_GARRICK_INN_COMPLETE')
