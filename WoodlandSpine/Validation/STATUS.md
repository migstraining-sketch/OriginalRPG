# Current validation status

Latest pass: approved combat refinement against main baec2e2.

- Unity 6000.5.6f1 Windows build: PASS.
- Editor checks: **4,453**, including **2,460** refinement cases covering Bow axes/off-axis radii and LOS, elevated-model and ground picking at four camera angles/two encounter elevations, near-border conversion, Signature geometry/damage/terrain/action budgets, Pounce commitment, and Mossback collision after Drive.
- Built-player camera/control checks: **781**. Signature selection, cancellation, confirmation and repeat-confirmation protection; three screen sizes and restoration of exploration framing.
- Full-frame 1100x510 screenshot visually reviewed: Signature controls and range, disabled out-of-range confirmation, Pounce marker, and cropped battlefield are visible.
- Full built-player opening regression: **286 checks passed** on the final combat build, including all three weapons against woodland wildlife and Mossback, plus the retained lab, potion, contracts, shop and kitchen routes.

See Docs/COMBAT-REFINEMENT.md for changes, tuning values, Lunge's documented terrain interpretation, enemy design template and remaining limitations. The previous continuity audit's non-combat gaps remain. Automated tests and screenshot checks do not establish combat fun, dialogue acceptance or a complete human mouse playthrough.

