# Opening implementation — verification and limits

## Current build

Unity 6000.5.6f1 imported and built the Windows player successfully. The final source build passed **336 Editor assertions**: 169 combat rules, 34 opening rules, 34 reactive-context checks and 99 progression/cooking checks. The final built-player run passed **183 runtime assertions** (`FULL_OPENING_RUNTIME_SUCCESS`).

## What the runtime run actually exercises

- Immediate control and no initial objective.
- Real CharacterController walking to the relocated board; inspecting it without starting an intervention.
- Shared introduction, real NPC walk to the stairs, real player descent/ascent, layer/camera transition and permanent Marlow relocation.
- Optional ale branch, troll attention without taking control, ordered lab reveal, refusal and reconsideration.
- Actual front-door walkout and all three weapons against both original woodland combat models.
- Naturalist report, ingredient consumption, supervised first potion, troll treatment and board unlock.
- All three contract interaction graphs: Reedback lethal combat plus harvest, Nightquill nonlethal repair, Brookmaw nonlethal relocation. Client payments, separate food inventory and mill/roof consequences.
- Kitchen referral, Sylvie demonstration, Cooking acceptance, optional practice and paid room rental.
- Physical paths between the inn approach and all three client sites, with earlier quest state retained.

Editor tests additionally cover both lethal/nonlethal resolution state for all three jobs, duplicate-reward prevention, ordering gates, illness expiry and treatment protection, room affordability and wrong/correct cooking heat.

The run drives dialogue/gameplay callbacks directly. It walks selected routes with the real CharacterController and resolves actual deterministic combat, but does not click every UI button like a human or physically walk the entire quest in one uninterrupted natural playthrough. It does not establish a 20–35 minute reading pace or approve the writing.

## Problems found and fixed

- Lab cabinet overlapped the staircase landing: moved clear; descent/ascent now pass.
- Original board prompt occupied the new stairwell: moved beside the bar; physical approach now passes.
- Bow test strategy initially omitted the already-supported post-action movement: restored the existing split-movement strategy; all weapon encounters pass without changing damage values.
- Lab camera showed the surface world and cropped the room: added floor cutaway layers and authored lab framing.
- Multiple contract food rewards were sharing one count: separated provision counts by item.
- Fleeing Mossback formerly satisfied the story gate: it now preserves retry while requiring defeat to finish this route.

## Files and artifacts

See **CHANGED-FILES.txt** for the exact source and metadata changes against the pre-pass snapshot. New core files are FirstLabVisit.cs, OpeningProgress.cs (including CookingSession), HuntDefinition.cs, HuntingWorld.cs, FullOpening.cs, FullOpeningSmoke.cs, and Editor/FullOpeningValidation.cs. Existing scene generation, dialogue, HUD, controller, opening state and build validation are updated. No custom art assets or external game IP were imported.

README.md and PLAYTEST-OPENING.md are current. Earlier INTRO/MARLOW guides and validation reports are historical. The Windows build and source archive are refreshed.

Opening-connected-lab.png and Opening-kitchen.png are inspected direct camera renders, not HUD-inclusive screenshots. They demonstrate blockout framing only.

## Remaining scope limits

No known failing checks in the final build. Human acceptance of the dialogue, timing, all first-contact variations, HUD sizing on other displays and natural full-loop play remains pending. Garrick/crime and violent Mooncalf branches are implemented at prototype level but are not end-to-end runtime-covered by this test. Generic wildlife movement is reused for the contract fights; advanced creature behaviours remain absent.

Placeholder geometry, simple NPC movement/poses and a generated creature murmur; no final animation, voice acting or Blender assets. Kitchen/room doors still use same-scene transfers; the first lab descent is continuous physical movement. Illness defaults to 45 minutes after invitation and locks Marlow for the remainder of the session on expiry; that duration is a provisional tunable, not an approved permanent design value. No save/load, complete crime/legal system, rest-quality effects or mechanical Well Fed benefit. Progress and physical consequences last for the running session.

Next smallest production step: manually play invitation through the first lab decision and revise only lines, pauses or staging that fail the intended scene before art production.
