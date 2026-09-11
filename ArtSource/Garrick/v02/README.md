# Garrick reference-led Blender study v02

Replaces the owner-rejected v01 as the current **work in progress**. It has not been promoted to approved/final character art or installed in Unity.

The owner supplied two actual images on 2026-09-10: the opening cast lineup and Garrick's detailed reference sheet. Both originals are preserved in `../References` and packed into this blend as viewport reference images. They supplement the approved written brief; the generic v01 silhouette is no longer the modeling basis. The existing inn appearance reference remains the environment style target, not a request to remodel the inn in this pass.

## Open and inspect

Open `Garrick_Reference_Study_v02.blend` in Blender 5.2.1. The file includes editable meshes, curves, materials, packed images, a metre-scale model root, the supplied reference boards, and studio lights/camera. Working model height is 1.90 m, inside the existing approved range, not a newly locked exact cast height.

`three-quarter.png`, `portrait.png`, `front.png` and `back.png` are actual Cycles renders of the saved model. The face texture was generated from the supplied Garrick sheet using the built-in image tool and is applied to the Blender head; it is not a stand-in illustration pasted over these renders. See [texture provenance and full prompt](TEXTURE-PROMPT.md).

## Changed from v01

- Human-length limbs and narrower continuous shoulders/forearms instead of stacked ball shapes.
- Defined face planes with a painted mature face, close beard and separate swept, tapered hair geometry.
- Open linen collar, fitted dark waistcoat, dark leather apron, restrained wear, cream towel with faded oxblood bands, and keys.
- Laced leather boots with cuffs, welt, toe seam and buckles; gathered dark trousers.
- Individual finger geometry in a neutral modeling stance.
- Packed references and reproducible Blender source, with v01 preserved separately.

## Remaining production work

This is still a surface/style study. Face likeness and final art-style acceptance require the owner's review. Hair, hands, clothing deformation and material wear still need an artist's refinement. The head uses provisional front-projection UVs with painted eyes and beard; it is not a finished facial animation mesh. The model is unrigged, unretopologized, and not a Unity-ready replacement. Rigging, animation, final UVs/texture baking and game-distance checks remain before integration.

`build_garrick.py` regenerates this study and all four renders using the saved face texture. It overwrites v02 generated outputs. Save manual sculpting under a new version before rerunning it. No gameplay or inn model files were changed for this revision.
