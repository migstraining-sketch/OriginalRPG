# Garrick v04: hair groom and head proportion experiment

The owner rejected the v03 head as still visually wrong and asked to continue. This is another **unfinished head study**, not approved character art or a Unity-ready NPC.

Open `Garrick_Head_Groom_v04.blend` in Blender 5.2. `front.png`, `three-quarter.png` and `profile.png` are actual renders of this scene. No generated illustration is substituted for a model preview.

Changes from v03:

- Lowered the forehead/crown above the brow and broadened the lower jaw.
- Replaced the solid hair strips with editable 3D curve strands, guided into overlapping waves. Added a short undercoat for coverage.
- Added short beard fibers following the actual face surface.
- Reduced iris/pupil size and adjusted eye color.
- Restricted the existing painted face albedo before the sides/ears, blending to vertex-painted side complexion to reduce projection stretching.

The source remains the adapted CC0 MakeHuman anatomical mesh and the previously generated v02 face albedo. See `../v03/SOURCE.md`, `../v03/source-checks.json`, and `../v02/TEXTURE-PROMPT.md`. The owner-supplied character references and existing face albedo remain packed in this file. No new third-party assets or image generation were used for v04.

## Limits

This is a procedural grooming and proportion experiment. It is not a professionally sculpted likeness. The face still uses provisional texture projection; style, likeness, expression and hair arrangement need visual judgment. Curve-based strand grooming is not an optimized game hairstyle. It needs a later game-compatible hair solution before any Unity integration. No rig, outfit or full body is included here.

`refine_head.py` loads the preserved v03 file and writes v04 outputs. It does not alter v03 or Unity. Re-running it overwrites generated v04 outputs; save manual refinements under a new filename.

Technical validation checks native Blender reopen, packed images, geometry/groom presence and rendered files. Passing those checks does not mean the art meets the supplied reference.
