"""Actual Blender inn context, not a Unity screenshot."""
import bpy
from pathlib import Path
from mathutils import Vector
OUT=Path(__file__).resolve().parent
bpy.ops.wm.open_mainfile(filepath=str(OUT/'Workwear_Foundation.blend'))
scene=bpy.context.scene
a=bpy.data.objects['Garrick | shared humanoid rig'];a.location=(0,3.22,0)
b=bpy.data.objects['Fitting test | shared humanoid rig'];b.location=(-1.25,1.1,0);b.rotation_euler.z=-.30
bpy.data.objects['Studio ground'].hide_render=True
with bpy.data.libraries.load(str(OUT.parents[1]/'Inn/GarricksInn_Visual_v02.blend'),link=False) as (src,dst):
    dst.objects=[n for n in src.objects if not any(n.startswith(p) for p in ['Garrick','Upper','Upstairs','Guest','Player personal','Bedside'])]
for o in dst.objects:
    if o and o.type in {'MESH','CURVE'} and o.location.z<3.5:
        scene.collection.objects.link(o);o.hide_render=False
cam=scene.camera
cam.location=(4,-6,5.0);target=Vector((-.5,2,1.04));cam.rotation_euler=(target-cam.location).to_track_quat('-Z','Y').to_euler();cam.data.ortho_scale=5.1
for o in scene.objects:
    if o.type=='LIGHT':o.location.y+=2.5
scene.render.resolution_x=1400;scene.render.resolution_y=1000
scene.render.filepath=str(OUT/'inn-foundation.png')
bpy.ops.wm.save_as_mainfile(filepath=str(OUT/'Inn_Foundation_Context.blend'))
bpy.ops.render.render(write_still=True)
print('INN_FOUNDATION_RENDERED')
