# Implementation handoff — 2026-09-08

The repository's root docs remain the design authority. This folder imports the existing WoodlandSpine Unity prototype and its accumulated opening-flow implementation; it does not replace or rewrite those documents.

## Included

Character entry and exploration; contextual Garrick/Marlow dialogue and supplied dialogue authority; a physical basement staircase and first troll/lab visit; weapon loan and inventory; connected woodland and deterministic hex battles; Mossback lane telegraph, obstacles and stagger; ingredient outcomes and first potion preparation; three hunting contracts with lethal/nonlethal outcomes; Sylvie's kitchen introduction and cooking; minimal shop and paid room entry. All visual geometry and audio are placeholders.

Assets, scene, .meta identifiers, Packages, ProjectSettings, validation sources, build tools and playtest notes are versioned. Library, local settings, logs, generated assemblies and Windows player output are excluded. Open this repository's WoodlandSpine folder in Unity Hub to develop here. The earlier outputs copy is the import source, not a second repository to maintain.

## Combat targeting correction

The camera previously rendered enemies behind the action panel while world input rejected clicks there. Combat now uses a camera viewport between the header and action panel, fitted to all battlefield cells and actor height. Exploration restores the full viewport. UI clicks remain isolated from world clicks. Selecting Attack also offers an explicit enemy confirmation button and Enter shortcut; the same range, sight, phase and Primary Action rules apply.

## Provisional and incomplete

The 45-minute illness timer, room price and small coin rewards are tuning assumptions. No save/load exists. Kitchen and room doors use same-scene transfers; the lab uses real stairs. Crime/guards, room rest, final art, voiced dialogue and later progression are incomplete. Existing historical validation notes describe earlier passes; STATUS.md identifies the current pass. Automated callback tests do not establish that the dialogue feels right or replace manual mouse/pacing acceptance.

Next smallest step: a human playthrough of the supplied Garrick/Marlow branches and first lab visit, plus edge-of-battlefield mouse targeting, before custom Blender assets.

The follow-up wide/short Game-view fix caps combat viewport aspect at 1.8 and compacts the combat header. This prevents adjacent prototype sites from appearing at the sides. Validation/STATUS.md records the focused camera regression and reviewed screenshot.
