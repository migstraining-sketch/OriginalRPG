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
9. `docs/DIALOGUE_PLAYER_AGENCY.md`
10. `docs/INN_STARTING_AREA.md`
11. `docs/CONTRACTS.md`
12. `docs/SYSTEMS_HUNTING.md`
13. `docs/SYSTEMS_COOKING.md`
14. `docs/UNRESOLVED.md`

## Working rule

Treat this repository as the source of truth for locked decisions. If a future chat finds a contradiction, do not silently rewrite upstream canon. Flag it, propose a fix, and update the relevant continuity files after Central Brain approval.

`docs/DIALOGUE_PLAYER_AGENCY.md` is the continuity authority for playable dialogue logic, player-response quality, conversational state, branching/convergence, and player commitment/agency. Proposed exact prose in that document does not become canon until Central Brain approves it.

## Project correction

This is an **original RPG**, not an AdventureQuest/BattleOn reconstruction and not an October 15, 2002 chronology project. AdventureQuest, BattleOn, RuneScape, World of Warcraft, and other referenced games are research/inspiration only.

## Current design frontier

The opening is designed through:

**Character Creation → Garrick's Inn → Marlow's opening quest → Potion Making → first Hunting contract → Hunting → Sylvie → Cooking unlock.**

A playable Unity prototype exists, but implementation does not promote itself to canon.

The current priority is to **implement and playtest the approved MVP combat-refinement package** in `docs/CORE_SYSTEMS_PROGRESSION.md`, especially:

- Sword Lunge, Spear Drive, and Bow Quick Shot
- the Bow targeting/LOS bug
- the first woodland creature's committed Pounce
- Mossback obstacle/lane baiting
- combat clarity and cancel/back behavior

In parallel, the Garrick/Marlow opening dialogue is under active authored review in `docs/DIALOGUE_PLAYER_AGENCY.md`. Do not implement proposed exact dialogue from that document until Central Brain approves it.

**Do not move on to designing the first 1–2 hours after Cooking unlock yet.** Finish refining and validating the opening's combat/dialogue experience first.