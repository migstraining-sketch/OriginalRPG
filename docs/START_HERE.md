# START HERE

This repository is the continuity authority for the original RPG project.

## Read order for a new Central Brain / implementation chat

1. `README.md`
2. `docs/CENTRAL_BRAIN_HANDOFF.md`
3. `docs/PROJECT_STATE.json`
4. `docs/DESIGN_PILLARS.md`
5. `docs/DESIGN_GUARDRAILS.md`
6. `docs/CORE_SYSTEMS_PROGRESSION.md`
7. `docs/GROUP_COMBAT_PARTY_MVP.md`
8. `docs/CHARACTERS.md`
9. `docs/OPENING_FLOW.md`
10. `docs/OPENING_MOONCALF_MILK_FAILURE.md`
11. `docs/TRAVEL_WORLD_MAP_MVP.md`
12. `docs/WORLD_BIOMES_EXPLORATION.md`
13. `docs/DIALOGUE_PLAYER_AGENCY.md`
14. `docs/INN_STARTING_AREA.md`
15. `docs/INN_SPATIAL_BLOCKOUT.md`
16. `docs/CONTRACTS.md`
17. `docs/SYSTEMS_HUNTING.md`
18. `docs/HUNTING_PARTNERS.md`
19. `docs/SYSTEMS_POTION_MAKING.md`
20. `docs/SYSTEMS_COOKING.md`
21. `docs/UI_HUD_PLAYER_INFORMATION.md`
22. `docs/COORDINATED_OPENING_DEVELOPMENT_SLICE.md`
23. `docs/PLAYER_CHARACTER_VISUAL_DIRECTION.md`
24. `docs/PLAYTEST_FEEDBACK_CURRENT.md`
25. `docs/UNRESOLVED.md`

Also read the current opening-creature authority on branch `design/creatures-monsters-encounters`, commit `73151b5c4f358aec4582868f27c616cdeaaef533`, file `docs/CREATURES_MONSTERS_ENCOUNTERS.md`, until that approved authority is integrated into `main`.

## Working rule

Implementation of the **Coordinated Opening Development Slice** has been separately authorized and currently lives in **draft PR #4** on branch `implementation/coordinated-opening-slice`.

Current mode is **human acceptance / focused iteration**, not another broad design freeze and not an automatic merge. Do not merge PR #4 merely because automated checks pass. Human playtest feedback in `docs/PLAYTEST_FEEDBACK_CURRENT.md` is active implementation input and should be checked against the owning authorities before fixes are made.

New ideas may be documented before they are final, but their status must be explicit: **LOCKED**, **PROPOSED**, **FUTURE**, or **PLAYTEST FEEDBACK**. Do not silently promote a proposed visual/mechanic into canon just because Unity can see the document.

Current review baseline: [coordinated package assessment](reviews/2026-09-10-coordinated-package-review.md). Earlier reviews are dated evidence, not a current defect list.

Treat this repository as the source of truth for locked decisions. If a future chat finds a contradiction, do not silently rewrite upstream canon. Flag it, propose a fix, and update the relevant continuity files after Central Brain approval.

`docs/DIALOGUE_PLAYER_AGENCY.md` is the continuity authority for playable dialogue logic, player-response quality, conversational state, branching/convergence, and player commitment/agency. Proposed exact prose in that document does not become canon until Central Brain approves it.

`docs/OPENING_MOONCALF_MILK_FAILURE.md` is the opening-only authority for detecting genuine Mooncalf Milk reagent impossibility, clearing impossible objectives, returning to Marlow, preserving the missed Potion Making consequence, and converging the failed Marlow route back into Garrick's board/Hunting progression.

`docs/TRAVEL_WORLD_MAP_MVP.md` is the opening-only authority for the regional travel layer connecting Garrick's Inn, the Woodland, and starter Hunting destinations. For the opening MVP, Garrick's **front door is the sole Regional Map exit** and regional return arrives through/at the front entrance; the back door remains local Inn/property space with exact future use unresolved.

`docs/WORLD_BIOMES_EXPLORATION.md` is the locked continuity authority for biome identity, local ecology, environmental resource placement, exploration discoveries, renewable/finite natural-source philosophy, and the opening Woodland ecology package.

The approved opening-creature branch adds the locked doctrine **Randomize ecology, not battles**, the Gloam Lynx first-predator identity/stalking sequence, creature activity pockets, persistence classes, Rootmuzzle/Ashwing identities, and the clarification that the opening Mossback is an anomalous **individual**, not necessarily an anomalous species.

`docs/UI_HUD_PLAYER_INFORMATION.md` is the Central Brain-approved presentation/information-architecture authority for the coordinated UI pass. Its three-layer architecture and Esc/back doctrine are locked; exact screen composition remains Unity/playtest tuning.

`docs/GROUP_COMBAT_PARTY_MVP.md` is the locked authority for party/group-combat architecture, distributed activations, Active Party vs Recruited Roster vs Combat Participants, controller ownership, and opening defeat policy.

`docs/HUNTING_PARTNERS.md` is the locked authority for starter-Hunt local partner structure. Ilyra “Ily” Fen is implementation-ready for `Mud in the Moonrice`; Sable Venn and Nessa Vale are locked at character direction only until their contracts receive later implementation passes.

`docs/COORDINATED_OPENING_DEVELOPMENT_SLICE.md` defines **development/playtest scope**, not canon. It preserves all three starter Hunts as equal canonical choices while limiting the current coordinated build to a fully playable **Mud in the Moonrice + Ilyra “Ily” Fen** Hunting route.

`docs/PLAYER_CHARACTER_VISUAL_DIRECTION.md` is a **proposed** visual direction for the player avatar, not yet an approved character reference. It exists so art/implementation can align around a grounded young traveler direction while exact face/model details remain open.

`docs/PLAYTEST_FEEDBACK_CURRENT.md` is the active manual-playtest issue log for draft PR #4. It currently covers Garrick fair-bout/theft-state leakage, Bow kiting against Garrick, player-model continuity into combat, committed-pursuit reliability, and the not-yet-integrated Gloam Lynx stalking buildup.

## Project correction

This is an **original RPG**, not an AdventureQuest/BattleOn reconstruction. AdventureQuest, BattleOn, RuneScape, World of Warcraft, Supernatural, and other referenced media are inspiration/reference only, not canon to copy.

## Current design / implementation frontier

The canonical opening is designed through:

**Character Creation → Garrick's Inn → Marlow's opening quest → Regional Map → Woodland → Potion Making on success OR Marlow treatment-failure branch → first Hunting contract → Regional Map → contract destination → Hunting → Sylvie → Cooking unlock.**

All three starter Hunting contracts remain equal canonical choices:
- `Mud in the Moonrice`
- `Three Missing by Morning`
- `When the Wheel Stopped`

None is canonically mandatory.

The current development build remains the **Coordinated Opening Development Slice** / **Marlow / Woodland / Mud + Ily development slice**.

Its fully playable Hunting target is:

**Mud in the Moonrice + Ilyra “Ily” Fen**

For this development/test build:
- all three canonical postings remain visible on Garrick's board;
- `Mud in the Moonrice` is selectable/playable and reveals Reedwater Paddies on acceptance;
- `Three Missing by Morning` is visibly unavailable for the current playtest and is not selectable;
- `When the Wheel Stopped` is visibly unavailable for the current playtest and is not selectable;
- the unavailable state is development scaffolding only, with no invented in-world excuse;
- unavailable postings do not reveal Venn Homestead or Vale Watermill.

Draft PR #4 now implements a large portion of this slice, but implementation does not promote itself to canon and is still under human playtest.

### First human playtest feedback now active

The owner immediately found several issues worth fixing before merge:
- legitimate board-proof fight with Garrick incorrectly fell into theft-flavored win dialogue;
- Bow kiting made Garrick too easy despite the intent that early victory be possible but highly unlikely;
- player character representation did not carry cleanly into at least one combat sequence;
- a committed Woodland pursuer could be casually outrun;
- the first Woodland attacker still lacked the later-approved stalking buildup.

Read `docs/PLAYTEST_FEEDBACK_CURRENT.md` before the next PR #4 implementation patch.

### Opening Woodland creature direction

Approved creature authority on `design/creatures-monsters-encounters` establishes:
- **Gloam Lynx** as the first normal native mythical predator;
- roughly two readable stalking cues before ordinary confrontation;
- quiet trailhead → stalking → physical reveal/confrontation → committed Pounce;
- player may retreat before confrontation, but continuing deeper while actively stalked eventually produces confrontation;
- no free unavoidable opening hit;
- **Randomize ecology, not battles** for later roaming variation;
- current roster: Gloam Lynx, Mooncalf family, opening Mossback, Rootmuzzle, Ashwing Thrush;
- future-only directions: Fernhorn Roe and Runnelback.

### Player character visual direction

A proposed player-avatar direction is now recorded for future art/model continuity: grounded young male traveler, roughly 24–26, lean athletic build, dark-brown medium messy hair, warm amber/brown eyes, lightly tanned skin direction, blue-grey/slate travel layers, cream/grey undershirt, leather utility/satchel elements, dark trousers, practical boots, and no chosen-one/armor-heavy visual language.

This remains **proposed**, not a locked character reference. The implementation requirement that the player model/equipped weapon persist coherently into combat is active regardless of final art.

### Pacing clarification

Older approximately **20–30 / 27–30 minute** opening estimates are historical pacing guidance only. They are **not a hard acceptance criterion** for the coordinated development slice.

After implementation, measure a natural first playthrough. Do not cut meaningful exploration, dialogue choices, combat decisions, or world interaction merely to force the slice under an old stopwatch target, and do not deliberately pad it either.

Before the project can later claim the full three-choice starter-Hunt opening is complete, `Three Missing by Morning` + Sable and `When the Wheel Stopped` + Nessa still need implementation-depth passes and all three first-contract choices need acceptance/playtest coverage.

**Do not move on to designing the first 1–2 hours after Cooking unlock yet.** Finish refining and validating the opening experience first.

## Ongoing player experience review

Read `PLAYER_EXPERIENCE_REVIEW_LENS.md` when evaluating new design or implementation decisions. It defines evidence-based advisory criticism, not impersonation or a replacement for locked canon. Recommendations should be reported for discussion rather than silently implemented as design changes.
