# Historical result notice

This report describes an earlier pass. The superseded PLAYTEST-MARLOW guide has been deleted; use [PLAYTEST-OPENING](../PLAYTEST-OPENING.md) for the existing prototype route. References to that earlier guide below are historical evidence, not current instructions.

# Marlow opening validation — 2026-09-08

**Actual Unity Editor import and Windows player build: PASS**, Unity 6000.5.6f1. The earlier license restriction was resolved by running the build with normal licensing-service access. No final C# compiler diagnostics or serialization warnings remained.

- **169 combat assertions passed in Unity.** The previous damage/geometry/movement/phase rules remain covered.
- **31 opening assertions passed in Unity.** Includes gated gathering, no duplicate collection, survival/report gating, supervised errors, correct sequence and heat/stirs, single consumption, single potion reward, treatment/unlocks and neutral selection cancellation.
- **Runtime player: PASS, `RUNTIME_SMOKE_SUCCESS`.** Actual scene bootstrap and ScriptableObject deserialization succeeded. Contextual inn objects, lab invitation/entry, notes/specimens/equipment/troll, and expedition acceptance worked. Every registered active prompt had at least one clear capsule-sized standing position within interaction distance.
- CharacterController-driven physical inn exit and southbound return completed. The test initially failed because it competed with the normal movement driver at an uncontrolled background frame rate; the final test uses one driver at 60 fps. No bypass teleport replaced these door-route checks.
- Original Sword/Spear/Bow each defeated wildlife and Mossback with legal model commands in the Unity player. The first Mossback case entered through the sight → give space → pursuit → combat code, rather than a direct encounter call.
- Bloodleaf harvest removed only the useful tip visual; Silvermoss collection succeeded. Peaceful Mooncalf milk and frightened-Mooncalf/covered-sample routes both succeeded.
- Move/Attack/Item selection canceled without spending anything. Cancel after committed movement/Defend did not restore budgets.
- Mossback debrief, supervised preparation, deliberate wrong preparation, pause/resume, final ingredient consumption, troll treatment, remaining potion, recipe/profession unlock and Garrick/board posting stub all passed.
- Scene-only captures `Lab-before.png`, `Lab-recovery.png`, and `Mossback.png` were inspected. Pale-to-green recovery and the charge lane are visible.

**Limits:** the runtime test drives public interaction/combat commands, teleports between some distant checks, and tests prompt standing space rather than every possible walking route. It checks the inn doorway with actual movement. It does not click through the Unity Editor UI or constitute a human end-to-end playtest. Control comfort, ingredient discoverability, pacing, all approach positions and HUD readability at the user's chosen resolution still need the ordered personal test in `PLAYTEST-MARLOW.md`.

No known failing automated checks remain. Progress is intentionally session-only. The Mooncalf alternate route is frighten + fresh field sample; attacking/killing it is not implemented. Future Hunting entries and further potion batches are placeholders.
