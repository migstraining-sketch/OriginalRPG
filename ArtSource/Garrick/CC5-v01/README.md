# Garrick — Character Creator base fitting candidate

Owner authorized proceeding on 2026-09-11 after successfully exporting Kevin from Character Creator 5. This is a **local Blender appearance/fitting candidate**, not approved final Garrick and not a Unity replacement. Current `origin/main` was checked; the approved `GARRICK_VISUAL_APPROVED_REFERENCE.md` remains the appearance authority. The owner's production request authorizes this modeling pass.

## What changed

- Adapted the imported Kevin body toward a broader working torso/core, with restrained facial-plane and age-color treatment.
- Preserved the CC skeleton's 101 bones and all 160 body facial shapes (plus Basis).
- Reused the workwear foundation's separate shirt/collar, vest, apron, belt, towel, trousers, boots and keys. Fitted the sleeves from the earlier T-pose to the CC rest pose and transferred weights onto the CC skeleton.
- Kept the CC face/hands. Removed old-base hands from the clothing assembly. A reversible body mask hides skin beneath the clothes; it does not delete the source body topology.
- Adapted the licensed clumped hair and created a beard surface from the CC face. Its facial shapes are driven from the body, including the tested mouth-opening shape.
- Authored a 120-frame idle/reach fitting action and rendered the candidate with the existing inn geometry.

## Local deliverables

Outputs are deliberately outside this repository, under the workspace's `outputs/character-assets/Garrick-CC5-v01/` directory:

- `Garrick_CC5_Candidate.blend`: editable character, packed textures, skeleton, facial shapes and fit-test action.
- `Garrick_Inn_Context.blend`: existing inn source appended around the character for review.
- `garrick-full.png`, `garrick-face.png`, `garrick-reach.png`: actual Blender renders.
- `garrick-inn.png`: actual Blender context render, not a Unity screenshot.
- `garrick-motion.mp4`: low-resolution sampled idle/reach review, when the encoding step below has completed.
- `verification.json`: saved-model structural checks; a copy accompanies these scripts.

Open the candidate in Blender 5.2.1. Frames 1–60 cover the idle and 61–120 the reach test. The masked body and retained facial controls remain editable. Save manual refinements under a new filename before regeneration.

## Reproduce

Prerequisite: the local, validated `outputs/character-assets/CC5-Import-v01/Kevin_Blender_Test.blend` from the owner's export. The original export remains at `C:/Users/Miguel/OneDrive/Documents/CC5-Base-Test`; it is not overwritten. No CC5-derived meshes, packed textures, FBX keys or binaries are uploaded by this change. Quaternius source geometry and its CC0 licenses remain in `../Rigged-v01/Source`.

Run each script with Blender's background `--python` option:

1. `build_cc_garrick.py`
2. `verify_cc_garrick.py`
3. `render_cc_garrick_inn.py`
4. `render_cc_garrick_motion.py`

The scripts locate local output folders relative to the repository's parent workspace. Encode the resulting numbered PNG sequence at 6 fps using FFmpeg, with H.264/yuv420p and an MP4 container. Geometry and textures stay local; these scripts, notes and validation results are backed up in Git.

Look for `CC_GARRICK_BUILD_COMPLETE`, `CC_GARRICK_VERIFIED`, `CC_GARRICK_INN_COMPLETE`, and `CC_GARRICK_MOTION_FRAMES_COMPLETE`. Blender's process exit code alone is not sufficient to establish success.

## Verification and remaining limits

51 checks passed on the saved candidate: one retained CC rig, facial shape retention, own-rig bindings, garment weights, packed textures, evaluated sleeve movement and beard expression following. These checks do not establish complete animation coverage or artistic approval.

The cuff/neck visibility gaps from the first fitting render were corrected. The character has been viewed at rest and in the reaching pose. Full walking, combat, facial expression range, cloth intersections in extreme motion, collision and Unity materials/animation are not validated. Native CC shader effects are approximated with Blender materials rather than reproduced completely.

Visual limitations remain: hair silhouette is still tidier than the reference; beard boundary and collar/shoulder seams need refinement; the age and stylization require owner review. The clothing is a fitted prototype, not a finished production wardrobe. Do not describe this as a likeness-approved or game-ready final character.
