# Garrick v03: anatomical head foundation study

Owner requested another attempt after rejecting the clay-like v01/v02 results. This is a **head-only modeling experiment, not approved character art**, and is not installed in Unity.

Open `Garrick_Head_Foundation_v03.blend` in Blender 5.2. The three PNGs are actual Cycles renders of that file: `three-quarter.png`, `front.png`, and `profile.png`.

## Changed foundation

- Replaced the custom rounded head with a connected anatomical mesh adapted from MakeHuman's CC0 graphical assets. Original mesh UVs are preserved alongside the provisional albedo registration.
- Applied adult male, age, muscularity, jaw-width and head-shape morphs. The exact study weights are recorded in `verification.json`; they do not change character canon.
- Added separate eyeballs, irises and pupils inside anatomical eye sockets. Nose, lips, ears and jaw have real surface structure visible in profile.
- Built overlapping hair pieces and sparse beard-edge geometry, with directional hair color variation.
- Reused the **generated painted face texture from v02** for surface detail. It is packed into this file. This remains provisional front projection blended into vertex color, not a finished production texture atlas. No new image generation was used for v03.
- Packed the two owner-supplied reference sheets. Those references still govern likeness and style.

## Honest limits

The head is a stronger anatomical starting point, but it is not a finished likeness or a demonstrated match to the reference's art quality. The hair remains too strip-like; surface projection at the temples/profile needs proper texture painting. Proportions, expression, skin treatment and beard silhouette still need art review. No polished outfit, complete body, animation rig, optimized game export or Unity replacement is delivered in this revision.

The model is still assembled and modified with Blender Python, now using an authored anatomical base. Do not describe it as a hand-sculpted production character or claim artistic acceptance from technical validation.

`build_bust.py` rebuilds this study and its renders. It overwrites generated v03 outputs: save any manual edits under a new filename first. No installation of MakeHuman or Blender add-ons is required.

Validation is limited to native file reopen, geometry/image presence and actual multi-angle rendering. These checks establish file integrity, not visual approval.

See `SOURCE.md` and `Source/LICENSE.ASSETS.md` for the anatomical asset provenance.
