# Shared workwear foundation — first reuse proof

Owner authorized a Garrick-plus-second-character foundation test. This is an editable **Blender proof**, not final approved character art or Unity integration. Current `origin/main` was checked at `7aed489`; `docs/GARRICK_VISUAL_APPROVED_REFERENCE.md` remains appearance authority. The second actor is an unnamed fitting mannequin, not a new canonical NPC.

## Inspect

Open `Workwear_Foundation.blend` in Blender 5.2.1. Two named collections contain the independently positioned actors. Both use the same 65-bone skeleton definition, the same animation action, and linked clothing/body mesh data. Material overrides and root proportions distinguish the fitting test. Modifying a shared mesh changes both actors; make a single-user copy before unique sculpting.

- `shared-foundation.png`: actual 3D render of both assemblies.
- `garrick-layered.png`, `garrick-age.png`: workwear and aging review.
- `garrick-reach-fit.png`: the actual reaching pose used for a limited fitting check.
- `inn-foundation.png`: offline Blender view using the existing inn geometry; not a game screenshot.
- Frames **1–60**: breathing idle; **61–120**: reaching fit test. Space plays the timeline when the cursor is over Blender's timeline or 3D viewport.

## Reusable parts created

Separate vest front/back/side/shoulder panels; folded shirt collar and plain front facing; removable apron bib, skirt and shoulder straps; work belt. Existing licensed sleeves/hands, trousers, boots, head, eyes and hair are retained and adapted. Tunic toggles, shoulder tabs and tall boot cuff decoration were removed. New garment weights are transferred from the source body cloth; accessories follow the appropriate actor rig.

Garrick has a salt-and-pepper beard/temple treatment and restrained under-eye coloring. This is still a stylized fitting model, with younger facial structure and tidier hair than the approved reference. The sleeves, collar, apron drape, facial identity and close-up finish still need art refinement. Do not label the likeness final.

This proves reuse within **one masculine base and two proportion profiles**. It does not establish a complete cast library, female bodies, different head topology, walking/combat/face animation, extreme body-shape fitting or production cloth simulation. The second actor has the same underlying face geometry. It is a reuse test, not proof of two distinct finished identities.

Objects are marked as Blender assets and have `foundation_module` properties. For a complete actor, append the named actor collection from this blend, retaining the rig, meshes and action together. Individual clothing objects require the matching rig and rest pose. The second skeleton is a linked definition but has its own pose and transform.

## Reproduce / validate

Run these scripts with Blender's background `--python` option, in order:

1. `build_foundation.py`: opens the earlier candidate read-only and writes this folder's generated blend and review renders.
2. `verify_foundation.py`: reloads the saved blend and checks linked reuse, materials, normalized garment weights, texture packing, rig ownership and evaluated clothing motion.
3. `render_inn.py`: produces the context image and a local, ignored context blend.

Running generation overwrites this folder's generated outputs. Save manual refinements under a new filename first. A process exit code alone is not sufficient: look for `FOUNDATION_BUILD_COMPLETE`, `FOUNDATION_VERIFIED` and `INN_FOUNDATION_RENDERED` in logs. `verification.json` records completed checks and explicit limitations. Automated structural checks do not establish visual approval or full animation quality.

Custom materials currently use Blender nodes/vertex colors; they need texture baking or corresponding engine materials before export. No Unity gameplay or scene files were changed for this proof.

## Sources and next transfer test

Adapted from the existing [CC0 Quaternius fitting candidate](../../Garrick/Rigged-v01/README.md). Its original source files, license documents and source manifest remain under that folder. Existing inn source remains unchanged.

The owner subsequently offered to handle Character Creator 5 exports. That enables a separate face/body quality comparison. A CC5 skeleton is not assumed compatible with this Quaternius rig: importing, fitting and retargeting must be tested. Preserve this working proof until the replacement passes those checks. See [one-model export checklist](CC5_EXPORT_CHECKLIST.md).
