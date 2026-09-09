# Current validation status

Latest pass: combat HUD targeting correction and complete GitHub source import.

- Unity 6000.5.6f1 Windows build: PASS.
- Editor assertions: **1,731** (456 existing rules/dialogue checks and 1,275 viewport checks across five screen shapes).
- Built-player assertions: **265**. Includes actual camera projection of every battlefield cell and actor height in all six weapon/woodland fights; explicit Attack confirmation, fixed damage, invalid range and repeat-confirmation protection; retained opening, lab traversal, potion, contracts and kitchen routes.
- Imported repository C# compilation against installed Unity assemblies: PASS.

The runtime suite drives callbacks and selected physical walking routes. It does not replace human mouse testing, dialogue quality or pacing acceptance. Read Docs/IMPLEMENTATION-HANDOFF.md for the import and targeting change. DIALOGUE-CORRECTION.md and other results record historical passes. Docs/OPENING-DIALOGUE-AUTHORITY.md preserves the supplied script.
