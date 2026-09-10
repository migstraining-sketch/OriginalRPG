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
22. `docs/UNRESOLVED.md`

## Working rule

**Owner instruction, 2026-09-10: review first; gameplay coding is on hold.** Read the updated GitHub package, provide an honest review for Central Brain, and resolve material concerns before a separately authorized implementation pass. Locked design status alone is not permission to start coding. Documentation cleanup may remove clearly superseded instructions, but reviewer recommendations must remain proposals until accepted. Do not claim a guaranteed 10/10 experience from documents.

Current review: [coordinated package assessment](reviews/2026-09-10-coordinated-package-review.md). Earlier reviews are dated evidence, not a current defect list.

Treat this repository as the source of truth for locked decisions. If a future chat finds a contradiction, do not silently rewrite upstream canon. Flag it, propose a fix, and update the relevant continuity files after Central Brain approval.

`docs/DIALOGUE_PLAYER_AGENCY.md` is the continuity authority for playable dialogue logic, player-response quality, conversational state, branching/convergence, and player commitment/agency. Proposed exact prose in that document does not become canon until Central Brain approves it.

`docs/OPENING_MOONCALF_MILK_FAILURE.md` is the opening-only authority for detecting genuine Mooncalf Milk reagent impossibility, clearing impossible objectives, returning to Marlow, preserving the missed Potion Making consequence, and converging the failed Marlow route back into Garrick's board/Hunting progression.

`docs/TRAVEL_WORLD_MAP_MVP.md` is the opening-only authority for the regional travel layer connecting Garrick's Inn, the Woodland, and starter Hunting destinations. It is intentionally not a full endgame/overworld travel design. For the opening MVP, Garrick's **front door is the sole Regional Map exit** and regional return arrives through/at the front entrance; the back door remains local Inn/property space with exact future use unresolved.

`docs/WORLD_BIOMES_EXPLORATION.md` is the locked continuity authority for biome identity, local ecology, environmental resource placement, exploration discoveries, renewable/finite natural-source philosophy, and the opening Woodland ecology package. Its coordinated-pass Woodland scope is current authority and does not authorize Unity changes on its own.

`docs/UI_HUD_PLAYER_INFORMATION.md` is the Central Brain-approved presentation/information-architecture authority for the coordinated UI pass. Its three-layer architecture and Esc/back doctrine are locked; exact screen composition remains Unity/playtest tuning.

`docs/GROUP_COMBAT_PARTY_MVP.md` is the locked authority for party/group-combat architecture, distributed activations, Active Party vs Recruited Roster vs Combat Participants, controller ownership, and opening defeat policy.

`docs/HUNTING_PARTNERS.md` is the locked authority for starter-Hunt local partner structure. Ilyra “Ily” Fen is implementation-ready for `Mud in the Moonrice`; Sable Venn and Nessa Vale are locked at character direction only until their contracts receive later implementation passes.

## Project correction

This is an **original RPG**, not an AdventureQuest/BattleOn reconstruction and not an October 15, 2002 chronology project. AdventureQuest, BattleOn, RuneScape, World of Warcraft, and other referenced games are research/inspiration only.

## Current design frontier

The opening is designed through:

**Character Creation → Garrick's Inn → Marlow's opening quest → Regional Map → Woodland → Potion Making on success OR Marlow treatment-failure branch → first Hunting contract → Regional Map → contract destination → Hunting → Sylvie → Cooking unlock.**

A playable Unity prototype exists, but implementation does not promote itself to canon.

Central Brain is assembling one coordinated opening implementation/playtest pass covering:

- approved dialogue revisions
- approved MVP combat refinements
- locked group-combat/party architecture
- regional-map/travel layer with front-door travel grammar
- Mooncalf herd/container/failure-path behavior
- rented-room persistent storage
- `Mud in the Moonrice` as the first fully playable starter Hunting contract
- Ilyra “Ily” Fen local-partner integration
- approved HUD/player-information architecture
- approved Woodland biome/exploration ecology additions

Do **not** modify Unity piecemeal from individual design documents before that coordinated pass.

The travel layer must preserve geography without manufacturing empty traversal: meaningful destinations use a visual regional map, only plausibly known locations become selectable, and short route/travel presentation communicates that travel occurred.

Room-rental behavior is currently working well in playtest. Preserve that behavior while adding the approved persistent personal storage benefit from `INN_STARTING_AREA.md`.

**Do not move on to designing the first 1–2 hours after Cooking unlock yet.** Finish refining and validating the opening experience first.

## Ongoing player experience review

The project owner requested an Asmongold-informed review lens on 2026-09-10. Read `PLAYER_EXPERIENCE_REVIEW_LENS.md` when evaluating new design or implementation decisions. It defines evidence-based advisory criticism, not impersonation or a replacement for locked canon. The initial scored assessment is `reviews/2026-09-10-opening-player-review.md`; it distinguishes current code, newer design revisions and untested experience. Recommendations should be reported for discussion rather than silently implemented as design changes.
