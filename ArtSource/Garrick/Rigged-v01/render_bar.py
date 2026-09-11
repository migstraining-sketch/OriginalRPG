"""Offline context render using the project's actual Blender inn furniture."""
import bpy,math,json
from pathlib import Path
from mathutils import Vector
out=Path(__file__).resolve().parent
bpy.ops.wm.open_mainfile(filepath=str(out/'Garrick_Rigged_Candidate.blend'))
scene=bpy.context.scene;rig=bpy.data.objects['Garrick — deformation skeleton'];rig.location.y=3.22
bpy.data.objects['Studio ground'].hide_render=True
path=out.parents[1]/'Inn/GarricksInn_Visual_v02.blend'
with bpy.data.libraries.load(str(path),link=False) as (src,dst):
 dst.objects=[name for name in src.objects if not any(name.startswith(prefix) for prefix in ['Garrick','Upper','Upstairs','Guest','Player personal','Bedside'])]
for o in dst.objects:
 if o and o.type in {'MESH','CURVE'} and o.location.z<3.5:scene.collection.objects.link(o);o.hide_render=False
cam=scene.camera;cam.location=(2.8,-2.6,3.0);target=Vector((0,3.1,1.08));cam.rotation_euler=(target-cam.location).to_track_quat('-Z','Y').to_euler();cam.data.ortho_scale=2.9
scene.render.resolution_x=1200;scene.render.resolution_y=900
scene.render.filepath=str(out/'bar-context.png')
for o in scene.objects:
 if o.type=='LIGHT':o.location.y+=3.22
bpy.ops.wm.save_as_mainfile(filepath=str(out/'Garrick_Bar_Context.blend'))
bpy.ops.render.render(write_still=True)
