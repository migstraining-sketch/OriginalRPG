# START HERE

This repository is the continuity authority for the original RPG project.

## Read order for a new Central Brain chat

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
23. `docs/UNRESOLVED.md`

## Working rule

**Owner-approved coordinated implementation, 2026-09-10.** The final blocker review passed and the owner separately authorized the **Marlow → Woodland → Mud in the Moonrice + Ily** slice. Implementation is on `implementation/coordinated-opening-slice` for PR review; do not merge directly to main. See [current implementation validation](../WoodlandSpine/Validation/STATUS.md). Natural dialogue/UI/pacing acceptance is not yet established. Review-first development still applies to future design packages.

The [earlier package assessment](reviews/2026-09-10-coordinated-package-review.md) is dated evidence, not a current unresolved blocker list. Prototype implementation does not establish new canon.
Treat this repository as the source of truth for locked decisions. If a future chat finds a contradiction, do not silently rewrite upstream canon. Flag it, propose a fix, and update the relevant continuity files after Central Brain approval.

`docs/DIALOGUE_PLAYER_AGENCY.md` is the continuity authority for playable dialogue logic, player-response quality, conversational state, branching/convergence, and player commitment/agency. Proposed exact prose in that document does not become canon until Central Brain approves it.

`docs/OPENING_MOONCALF_MILK_FAILURE.md` is the opening-only authority for detecting genuine Mooncalf Milk reagent impossibility, clearing impossible objectives, returning to Marlow, preserving the missed Potion Making consequence, and converging the failed Marlow route back into Garrick's board/Hunting progression.

`docs/TRAVEL_WORLD_MAP_MVP.md` is the opening-only authority for the regional travel layer connecting Garrick's Inn, the Woodland, and starter Hunting destinations. It is intentionally not a full endgame/overworld travel design. For the opening MVP, Garrick's **front door is the sole Regional Map exit** and regional return arrives through/at the front entrance; the back door remains local Inn/property space with exact future use unresolved.

`docs/WORLD_BIOMES_EXPLORATION.md` is the locked continuity authority for biome identity, local ecology, environmental resource placement, exploration discoveries, renewable/finite natural-source philosophy, and the opening Woodland ecology package. Its coordinated-pass Woodland scope is current authority and does not authorize Unity changes on its own.

`docs/UI_HUD_PLAYER_INFORMATION.md` is the Central Brain-approved presentation/information-architecture authority for the coordinated UI pass. Its three-layer architecture and Esc/back doctrine are locked; exact screen composition remains Unity/playtest tuning.

`docs/GROUP_COMBAT_PARTY_MVP.md` is the locked authority for party/group-combat architecture, distributed activations, Active Party vs Recruited Roster vs Combat Participants, controller ownership, and opening defeat policy.

`docs/HUNTING_PARTNERS.md` is the locked authority for starter-Hunt local partner structure. Ilyra “Ily” Fen is implementation-ready for `Mud in the Moonrice`; Sable Venn and Nessa Vale are locked at character direction only until their contracts receive later implementation passes.

`docs/COORDINATED_OPENING_DEVELOPMENT_SLICE.md` defines **development/playtest scope**, not canon. It preserves all three starter Hunts as equal canonical choices while limiting the upcoming coordinated development build to a fully playable **Mud in the Moonrice + Ilyra “Ily” Fen** Hunting route. The other two postings remain visible as development-unavailable scaffolding and do not reveal their destinations in this slice.

## Project correction

This is an **original RPG**, not an AdventureQuest/BattleOn reconstruction and not an October 15, 2002 chronology project. AdventureQuest, BattleOn, RuneScape, World of Warcraft, and other referenced games are research/inspiration only.

## Current design frontier

The canonical opening is designed through:

**Character Creation → Garrick's Inn → Marlow's opening quest → Regional Map → Woodland → Potion Making on success OR Marlow treatment-failure branch → first Hunting contract → Regional Map → contract destination → Hunting → Sylvie → Cooking unlock.**

All three starter Hunting contracts remain equal canonical choices:
- `Mud in the Moonrice`
- `Three Missing by Morning`
- `When the Wheel Stopped`

None is canonically mandatory.

The next coordinated implementation/playtest target is narrower and should be described as the **Coordinated Opening Development Slice** or **Marlow / Woodland / Mud + Ily development slice**.

Its fully playable Hunting target is:

**Mud in the Moonrice + Ilyra “Ily” Fen**

For this development/test build:
- all three canonical postings remain visible on Garrick's board;
- `Mud in the Moonrice` is selectable/playable and reveals Reedwater Paddies on acceptance;
- `Three Missing by Morning` is visibly unavailable for the current playtest and is not selectable;
- `When the Wheel Stopped` is visibly unavailable for the current playtest and is not selectable;
- the unavailable state is development scaffolding only, with no invented in-world excuse;
- unavailable postings do not reveal Venn Homestead or Vale Watermill.

The development slice may also include approved dialogue revisions, combat refinements, group-combat/party architecture, regional-map/front-door travel, Mooncalf herd/container/failure behavior, rented-room storage, HUD/player-information work, and Woodland ecology additions as separately authorized.

A playable Unity prototype exists, but implementation does not promote itself to canon.

Do **not** modify Unity piecemeal from individual design documents before Central Brain authorizes the coordinated pass.

The travel layer must preserve geography without manufacturing empty traversal: meaningful destinations use a visual regional map, only plausibly known locations become selectable, and short route/travel presentation communicates that travel occurred.

Room-rental behavior is currently working well in playtest. Preserve that behavior while adding the approved persistent personal storage benefit from `INN_STARTING_AREA.md`.

### Pacing clarification

Older approximately **20–30 / 27–30 minute** opening estimates are historical pacing guidance only. They are **not a hard acceptance criterion** for the coordinated development slice.

After implementation, measure a natural first playthrough. Do not cut meaningful exploration, dialogue choices, combat decisions, or world interaction merely to force the slice under an old stopwatch target, and do not deliberately pad it either.

Before the project can later claim the full three-choice starter-Hunt opening is complete, `Three Missing by Morning` + Sable and `When the Wheel Stopped` + Nessa still need implementation-depth passes and all three first-contract choices need acceptance/playtest coverage.

**Do not move on to designing the first 1–2 hours after Cooking unlock yet.** Finish refining and validating the opening experience first.

## Ongoing player experience review

The project owner requested an Asmongold-informed review lens on 2026-09-10. Read `PLAYER_EXPERIENCE_REVIEW_LENS.md` when evaluating new design or implementation decisions. It defines evidence-based advisory criticism, not impersonation or a replacement for locked canon. The initial scored assessment is `reviews/2026-09-10-opening-player-review.md`; it distinguishes current code, newer design revisions and untested experience. Recommendations should be reported for discussion rather than silently implemented as design changes.
