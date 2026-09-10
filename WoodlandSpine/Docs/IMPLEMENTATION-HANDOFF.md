# Implementation handoff — baseline and pending design

Status refreshed 2026-09-10. Latest gameplay commit: `72d6411`. **Gameplay changes are on hold for review.** New group combat, travel, milk failure, storage, Inn layout, partner and dialogue/UI revisions are design work awaiting implementation. See the [current review](../../docs/reviews/2026-09-10-coordinated-package-review.md).

The repository's root docs remain the design authority. This folder imports the existing WoodlandSpine Unity prototype and its accumulated opening-flow implementation; it does not replace or rewrite those documents.

## Included

Character entry and exploration; contextual Garrick/Marlow dialogue and supplied dialogue authority; a physical basement staircase and first troll/lab visit; weapon loan and inventory; connected woodland and deterministic hex battles; Mossback lane telegraph, obstacles and stagger; ingredient outcomes and first potion preparation; three hunting contracts with lethal/nonlethal outcomes; Sylvie's kitchen introduction and cooking; minimal shop and paid room entry. All visual geometry and audio are placeholders.

Assets, scene, .meta identifiers, Packages, ProjectSettings, validation sources, build tools and playtest notes are versioned. Library, local settings, logs, generated assemblies and Windows player output are excluded. Open this repository's WoodlandSpine folder in Unity Hub to develop here. The earlier outputs copy is the import source, not a second repository to maintain.

## Combat targeting correction

The camera previously rendered enemies behind the action panel while world input rejected clicks there. Combat now uses a camera viewport between the header and action panel, fitted to all battlefield cells and actor height. Exploration restores the full viewport. UI clicks remain isolated from world clicks. Selecting Attack also offers an explicit enemy confirmation button and Enter shortcut; the same range, sight, phase and Primary Action rules apply.

## Provisional and incomplete

The existing 45-minute illness timer is superseded by the new authored-state direction and must be replaced in the authorized coordinated pass. Room price and small coin rewards remain prototype assumptions. No save/load exists; new persistent-storage design is not proof of disk persistence. Kitchen and room doors use same-scene transfers; the lab uses real stairs. Crime/guards, room rest, final art, voiced dialogue and later progression are incomplete. [Validation status](../Validation/STATUS.md) records the earlier build. Automated callback tests do not establish dialogue quality or replace manual mouse/pacing acceptance.

Next step: resolve the current review's design dependencies before authorizing code. Subsequent playtesting must include natural dialogue, group targeting and the real success/failure routes, not only scripted callbacks.

The follow-up wide/short Game-view fix caps combat viewport aspect at 1.8 and compacts the combat header. This prevents adjacent prototype sites from appearing at the sides. Validation/STATUS.md records the focused camera regression and reviewed screenshot.
