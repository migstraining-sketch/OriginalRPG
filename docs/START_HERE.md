# START HERE

This repository is the continuity authority for the original RPG project.

## Read order for a new Central Brain chat

1. `README.md`
2. `docs/CENTRAL_BRAIN_HANDOFF.md`
3. `docs/PROJECT_STATE.json`
4. `docs/DESIGN_PILLARS.md`
5. `docs/DESIGN_GUARDRAILS.md`
6. `docs/CORE_SYSTEMS_PROGRESSION.md`
7. `docs/CHARACTERS.md`
8. `docs/OPENING_FLOW.md`
9. `docs/TRAVEL_WORLD_MAP_MVP.md`
10. `docs/DIALOGUE_PLAYER_AGENCY.md`
11. `docs/INN_STARTING_AREA.md`
12. `docs/CONTRACTS.md`
13. `docs/SYSTEMS_HUNTING.md`
14. `docs/SYSTEMS_COOKING.md`
15. `docs/UI_HUD_PLAYER_INFORMATION.md`
16. `docs/UNRESOLVED.md`

## Working rule

Treat this repository as the source of truth for locked decisions. If a future chat finds a contradiction, do not silently rewrite upstream canon. Flag it, propose a fix, and update the relevant continuity files after Central Brain approval.

`docs/DIALOGUE_PLAYER_AGENCY.md` is the continuity authority for playable dialogue logic, player-response quality, conversational state, branching/convergence, and player commitment/agency. Proposed exact prose in that document does not become canon until Central Brain approves it.

`docs/TRAVEL_WORLD_MAP_MVP.md` is the opening-only authority for the regional travel layer connecting Garrick's Inn, the Woodland, and starter Hunting destinations. It is intentionally not a full endgame/overworld travel design.

`docs/UI_HUD_PLAYER_INFORMATION.md` is the proposed presentation/information-architecture authority for the coordinated UI pass. Central Brain approval is still required for the recommendations explicitly listed there before Unity implementation.

## Project correction

This is an **original RPG**, not an AdventureQuest/BattleOn reconstruction and not an October 15, 2002 chronology project. AdventureQuest, BattleOn, RuneScape, World of Warcraft, and other referenced games are research/inspiration only.

## Current design frontier

The opening is designed through:

**Character Creation → Garrick's Inn → Marlow's opening quest → Regional Map → Woodland → Potion Making → first Hunting contract → Regional Map → contract destination → Hunting → Sylvie → Cooking unlock.**

A playable Unity prototype exists, but implementation does not promote itself to canon.

Central Brain is assembling one coordinated opening implementation/playtest pass covering:

- approved dialogue revisions
- approved MVP combat refinements
- regional-map/travel layer
- Mooncalf herd/container/failure-path behavior
- rented-room persistent storage
- at least one fully playable starter Hunting contract
- coherent HUD/player-information presentation after UI architecture approval

Do **not** modify Unity piecemeal from individual design documents before that coordinated pass.

The travel layer must preserve geography without manufacturing empty traversal: meaningful destinations use a visual regional map, only plausibly known locations become selectable, and short route/travel presentation communicates that travel occurred.

Room-rental behavior is currently working well in playtest. Preserve that behavior while adding the approved persistent personal storage benefit from `INN_STARTING_AREA.md`.

**Do not move on to designing the first 1–2 hours after Cooking unlock yet.** Finish refining and validating the opening experience first.

## Ongoing player experience review

The project owner requested an Asmongold-informed review lens on 2026-09-10. Read `PLAYER_EXPERIENCE_REVIEW_LENS.md` when evaluating new design or implementation decisions. It defines evidence-based advisory criticism, not impersonation or a replacement for locked canon. The initial scored assessment is `reviews/2026-09-10-opening-player-review.md`; it distinguishes current code, newer design revisions and untested experience. Recommendations should be reported for discussion rather than silently implemented as design changes.
