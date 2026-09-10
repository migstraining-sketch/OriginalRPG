# Anatomical asset provenance

Provider: MakeHuman Community, https://github.com/makehumancommunity/makehuman

Retrieved 2026-09-10. Index commit: `a8bc2d54ff0ac92e78ff71431b1023eda42bf482`.

The base mesh and target files are **CC0 1.0 Universal graphical assets**, as stated in their embedded headers and the repository's `LICENSE.md`, section C. The application source has a different license; this study does not copy or depend on MakeHuman program code. Full graphical asset license: `Source/LICENSE.ASSETS.md`.

Repository paths used by `build_bust.py`:

- `makehuman/data/3dobjs/base.obj`
- `makehuman/data/targets/macrodetails/caucasian-male-young.target`
- `makehuman/data/targets/macrodetails/caucasian-male-old.target`
- `makehuman/data/targets/macrodetails/universal-male-young-maxmuscle-averageweight.target`
- `makehuman/data/targets/chin/chin-width-incr.target`
- `makehuman/data/targets/chin/chin-bones-incr.target`
- `makehuman/data/targets/head/head-square.target`

`source-checks.json` records downloaded asset hashes checked against the Git tree above.

Character-specific art direction comes from the owner-supplied Garrick and cast reference sheets in `../References`, not the base mesh. The existing generated face texture comes from `../v02/Garrick_Face_Albedo.png`; its generation prompt is recorded in `../v02/TEXTURE-PROMPT.md`. Copies of these images are packed inside the native Blender file.
