# Current validation status

Latest pass: continuity audit against main fc69ea8 and read-only review of the Opening Questline branch.

- Unity 6000.5.6f1 Windows build: PASS (1,992 editor assertions).
- Full built-player opening regression: **287 assertions**, including real shop purchase callbacks, no auto-equipping or duplicate coat charges, and all five Sylvie ingredient introductions without unreported resolution inference.
- That runtime pass precedes the final text-only correction to Garrick's departure line and inventory scrolling; the final Unity build includes both.
- Prior focused camera regression: 772 assertions across three screen sizes, with the wide Game-view screenshot visually reviewed. Camera code was unchanged by this pass.

Read Docs/CONTINUITY-AUDIT.md for matches, concrete remaining implementation gaps and unresolved authority conflicts. No claim of full Cooking/Hunting compliance or narrative acceptance is made. Automated tests do not replace a human mouse/dialogue/pacing playthrough.

