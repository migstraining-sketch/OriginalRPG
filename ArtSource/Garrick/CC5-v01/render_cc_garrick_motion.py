import bpy
from pathlib import Path
ROOT=Path(__file__).resolve().parents[4]/'outputs/character-assets';OUT=ROOT/'Garrick-CC5-v01';frames=OUT/'motion-frames';frames.mkdir(exist_ok=True)
bpy.ops.wm.open_mainfile(filepath=str(OUT/'Garrick_CC5_Candidate.blend'))
scene=bpy.context.scene;scene.render.resolution_x=450;scene.render.resolution_y=600;scene.cycles.samples=8;scene.render.use_persistent_data=True
for index,frame in enumerate(range(1,121,5)):
    scene.frame_set(frame);scene.render.filepath=str(frames/('%03d.png'%index));bpy.ops.render.render(write_still=True)
print('CC_GARRICK_MOTION_FRAMES_COMPLETE',index+1)
