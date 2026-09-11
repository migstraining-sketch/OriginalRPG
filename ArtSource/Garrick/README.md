# Garrick — illustrated trial and historical Blender studies

**Latest fitting pass: [CC5-based Garrick candidate](CC5-v01/README.md).** The owner supplied a Character Creator export and authorized adapting it. Body, workwear and facial controls are integrated in a local Blender candidate with inn and movement previews. Source export and CC-derived binaries remain local; reproducible fitting scripts and validation are tracked. Visual approval and Unity replacement are still pending.

**Current follow-up: [shared workwear foundation](../Characters/Foundation-v01/README.md).** Owner requested reusable foundations after accepting the improved rigged body direction. This Blender proof adds separate clothing layers and a second unnamed assembly sharing meshes, skeleton and animation. It remains a visual/fitting candidate, not accepted final art or Unity integration. The owner has also offered to export a Character Creator 5 base for a subsequent comparison.

**Latest attempt: [rigged 3D candidate](Rigged-v01/README.md).** The owner rejected the cutout's in-world appearance and requested another attempt using an existing character base. The new candidate adapts licensed Quaternius body/outfit/hair geometry, retains a humanoid skeleton, and includes actual Blender renders in isolation and beside the inn bar. It is not yet approved or installed in Unity. The older illustrated artwork remains a style reference; its flat in-game presentation is not an accepted final character solution.

**Owner rejected the v01 through v04 Blender previews on 2026-09-10.** The owner clarified that the target is illustrated/animated fantasy, not photorealism, and allowed a 2D-looking approach. The owner then approved trying the [illustrated direction preview](Illustrated-v01/README.md) inside the inn. The latest work is the [playable illustrated cutout trial](Illustrated-Cutout-v01/README.md). It is a generated 2D image rendered within Unity, not a Blender model or finished animated character. The earlier Blender files below are preserved as rejected studies. Do not install them in Unity as accepted character art.

## Historical Blender v01 study

Owner-requested Blender work, 2026-09-10. This is an **editable blockout**, not final character art or a replacement for the approved character sheet. It is not installed in Unity.

Authority checked: `origin/main` at `7aed489`, especially [Garrick's approved modeling brief](https://github.com/migstraining-sketch/OriginalRPG/blob/7aed4898e90c4c659ea655b29923fae5a0b7fbe0/docs/GARRICK_VISUAL_APPROVED_REFERENCE.md). The Character Visual Development conversation also records the approved four characters and planned shared scale lineup. Its accessible history did not supply the actual approved sheet image. The face/hair forms here are provisional study geometry; no exact likeness match or final scale approval is claimed.

Open `Garrick_Outfit_Study_v01.blend` in Blender 5.2. The character collection contains separate editable clothing, body, hair, beard and proprietor props; the presentation collection contains only lights, camera and floor. Front is -Y, up is +Z, units are metres. The working 1.90 m target sits within the approved 1.88–1.93 m envelope and does not lock a canonical height.

The study emphasizes a substantial core, rolled light sleeves, dark vest, oxblood apron, cloth, large forearms/hands and heavy boots. A few keys establish the proprietor role. No armor, long coat, weapon rig or adventurer pouches were added.

`three-quarter.png`, `front.png` and `back.png` are actual Cycles renders of the blend geometry. `verification.json` records object/triangle counts. `build_study.py` regenerates the study and renders using Blender's Python API; running it overwrites these generated study outputs, so save manual refinements under a new filename first.

Remaining work: compare directly against the approved face/hair sheet and four-character lineup, refine continuous anatomy/cloth topology, hands and face, then UVs, material wear, rigging and an inn work idle. The current rounded forms are blockout geometry, not the final stylized dark-fantasy finish.
