# Garrick rigged character candidate

Owner requested another attempt on 2026-09-10 after rejecting the flat cutout and declining a tool that required manual operation. This is an actual editable 3D fitting study made from licensed character geometry. It is **not approved final Garrick**, and it has **not replaced the Unity character**.

## Open / inspect

Open `Garrick_Rigged_Candidate.blend` in Blender 5.2.1. It includes a 65-bone humanoid skeleton and a restrained 60-frame breathing loop at 30 fps. The neutral standing pose lowers the source T-pose arms. Textures are packed into the final blend by `verify_candidate.py`.

- `three-quarter.png`, `front.png`, `portrait.png`: actual Cycles renders of this geometry.
- `bar-context.png`: offline Blender context render beside furniture from the existing inn source. This is not a screenshot of the Unity game or evidence of Unity integration.
- `verification.json`: structural/texture/rig checks. These do not certify animation quality or likeness.

The base silhouette, face, hands, cloth folds, footwear and animation skeleton come from **Quaternius**. The free pack provides the Superhero male body; this study retains its head/neck and uses the compatible peasant outfit's rig and clothed body. It widens the torso/core, adjusts the palette, retains the source hair/beard geometry, adds an apron, cloth towel and small key details, and authors a breathing action. The vest is a clean procedural material region on the existing cloth mesh, not a finished separate garment. It will need baking or separate garment work before engine export.

Remaining visual issues: the face and tidy parted hair still read younger/more groomed than approved Garrick; the beard is too regular; sleeve trim and collar retain the source peasant design; apron/towel weights and prop attachments need work for reaching/walking animations. The roughly 1.9 m working height and heavier core are fitting choices, not new canon. No final likeness, polished animation, optimized engine export or full game-ready delivery is claimed.

## Sources / license

- [Universal Base Characters](https://quaternius.com/packs/universalbasecharacters.html), free Standard edition downloaded from the creator's [itch.io page](https://quaternius.itch.io/universal-base-characters).
- [Modular Character Outfits — Fantasy](https://quaternius.itch.io/modular-character-outfits-fantasy), free Standard edition.
- Both packs are supplied by the creator under **CC0 1.0**. Original license files are retained in `Source/BASE-LICENSE.txt` and `Source/OUTFIT-LICENSE.txt`. No paid assets were purchased.
- `Source/manifest.json` identifies the imported model files and original hashes. Source dependencies are copied unchanged except that two missing image-name aliases in the base glTF are supplied from the pack's identical corresponding normal maps (`_png.png` aliases). The light skin texture is another supplied source texture, not a generated face image.
- Inn context uses this repository's `ArtSource/Inn/GarricksInn_Visual_v02.blend` without overwriting it.

Authority read: `origin/main` Garrick modeling brief at `7aed489`; owner instructions authorize this separate candidate and maintain the illustrated/stylized target. Gameplay, dialogue and the Unity scene are unchanged.

## Reproduce

Run Blender in background mode with `--python build_candidate.py`, then `--python verify_candidate.py`, then `--python render_bar.py`, using each script's absolute path. The scripts regenerate only this candidate's outputs. Save manual edits as a different blend before rerunning. The generated bar-context blend and Blender backups remain local; the small source model, script and resulting context image are sufficient to reproduce it.
