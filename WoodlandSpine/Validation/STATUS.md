# Current validation status

Latest pass: wide/short Unity Game-view framing regression.

- Unity 6000.5.6f1 Windows build: PASS.
- Editor assertions: **1,992** (456 existing rules/dialogue assertions plus 1,536 viewport assertions, now including the reported 1100x510 Game-view proportions).
- Focused built-player camera assertions: **772** across 1100x510, 1280x800 and 1920x1080. Checks actual actor projection, exclusion of neighboring sites, camera aspect and restoration of the exploration viewport.
- Full-frame screenshot Combat-framing-1100x510.png visually reviewed: battle and HUD visible, neighboring sites excluded. Other captured sizes are available alongside it.
- Previous full opening suite: **265** assertions; not rerun for this camera-only correction. It covered Attack confirmation and the opening gameplay route.

The original HUD fix allowed the camera's horizontal field to grow with a very wide, short Game view. The viewport now has a maximum 1.8 aspect ratio, centered over a solid background; the combat header is shorter and hides the exploration objective while fighting. Enemies remain clear of the action controls. Camera aspect is set explicitly and reset on return to exploration.

Run the built player with --combat-camera-smoke for the focused test, or --opening-smoke for the full opening route. Automated checks do not replace human mouse testing or dialogue/pacing acceptance. Prior validation reports describe historical passes.
