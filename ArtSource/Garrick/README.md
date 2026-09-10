# Garrick — first Blender outfit and proportion study

**Owner rejected the v01, v02 and v03 previews on 2026-09-10.** These files are preserved as earlier studies, not approved art. The latest attempt is the [v04 hair groom and head proportion experiment](v04/README.md). It is also unfinished and awaiting visual judgment. Do not install these studies in Unity as accepted character art.

Owner-requested Blender work, 2026-09-10. This is an **editable blockout**, not final character art or a replacement for the approved character sheet. It is not installed in Unity.

Authority checked: `origin/main` at `7aed489`, especially [Garrick's approved modeling brief](https://github.com/migstraining-sketch/OriginalRPG/blob/7aed4898e90c4c659ea655b29923fae5a0b7fbe0/docs/GARRICK_VISUAL_APPROVED_REFERENCE.md). The Character Visual Development conversation also records the approved four characters and planned shared scale lineup. Its accessible history did not supply the actual approved sheet image. The face/hair forms here are provisional study geometry; no exact likeness match or final scale approval is claimed.

Open `Garrick_Outfit_Study_v01.blend` in Blender 5.2. The character collection contains separate editable clothing, body, hair, beard and proprietor props; the presentation collection contains only lights, camera and floor. Front is -Y, up is +Z, units are metres. The working 1.90 m target sits within the approved 1.88–1.93 m envelope and does not lock a canonical height.

The study emphasizes a substantial core, rolled light sleeves, dark vest, oxblood apron, cloth, large forearms/hands and heavy boots. A few keys establish the proprietor role. No armor, long coat, weapon rig or adventurer pouches were added.

`three-quarter.png`, `front.png` and `back.png` are actual Cycles renders of the blend geometry. `verification.json` records object/triangle counts. `build_study.py` regenerates the study and renders using Blender's Python API; running it overwrites these generated study outputs, so save manual refinements under a new filename first.

Remaining work: compare directly against the approved face/hair sheet and four-character lineup, refine continuous anatomy/cloth topology, hands and face, then UVs, material wear, rigging and an inn work idle. The current rounded forms are blockout geometry, not the final stylized dark-fantasy finish.
