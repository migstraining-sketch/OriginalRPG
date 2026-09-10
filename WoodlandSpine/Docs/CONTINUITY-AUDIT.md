# Continuity audit — 2026-09-08

**Historical audit:** the conflicts and next-step recommendations below describe that date, not current authority. Use [START_HERE](../../docs/START_HERE.md) and the [current review](../../docs/reviews/2026-09-10-coordinated-package-review.md). Retained for implementation-gap and source provenance.

## Authority and scope

Compared Unity source with main at fc69ea8: Central Brain handoff, project state, design pillars, Core Systems, characters, opening flow, inn, contracts, Hunting, Cooking, Potion Making and unresolved design register. Also read docs/OPENING_QUESTLINE_ROLE.md on origin/design/opening-questline-role. That branch was reviewed as proposed continuity, not merged or promoted to main.

This is a source/state audit with regression tests, not narrative approval or a timed human playthrough. Root design documents were preserved. The prior user-supplied exact opening script remains in OPENING-DIALOGUE-AUTHORITY.md.

## Matches and corrections

| Area | Result and evidence |
|---|---|
| Original identity | Placeholder original characters/world; no borrowed franchise assets or chronology. |
| Combat | CombatModel, HexGrid and data assets implement deterministic damage, 30 HP, equipment Armor, 3 Movement plus Primary Action, split movement, weapon geometry, LOS, difficult terrain and Mossback charge/stagger. Action cancellation and safe camera framing remain. |
| Equipment | Weapon/Body slots, compressed values and unrestricted inventory match. Fixed shop auto-equipping: purchases now enter carried equipment. Inventory offers explicit body equip outside combat, preserving the starting coat. Initial loan still equips intentionally. |
| Inn / lab structure | Immediate control, contextual contacts, invitation before warning, physical stairs, free lab exploration, Marlow relocation and troll decision exist. Exact prose/pacing remains subject to the user's approval; code coverage does not approve it. |
| Potion opening | Three named ingredients, supervised preparation, first dose to troll, recovery, remaining potion and recipe unlock exist. Repeat lab use consumes contributions rather than coins. Physical manipulation is still simplified UI. |
| Contracts | Three equal postings, clients, both resolution routes, the five edible outcomes, first-completion Hunting unlock and remaining postings exist. |
| Sylvie ingredient recognition | Fixed generic cut/tail descriptions using the five supplied species/condition lines from INN_STARTING_AREA. Exact player entrance is now “Garrick sent me.” Eggs do not reveal the Nightquill outcome. |
| Cooking unlock | Demonstration, tasting exchange, request to learn, pantry agreement, acceptance unlock and optional practice exist. Well Fed is identical and does not heal. The mechanical implementation is incomplete, as detailed below. |
| Rooms | Initial 10 coins / room 18 satisfies the 5–10 coin gap; renting is available when affordable. Prices remain prototype values; no Rest Quality system is claimed. |

## Remaining implementation gaps — not marked complete

1. **Cooking interaction is the largest mismatch.** CookingSession advances through buttons and numeric heat thresholds. It groups shed tail with preserved meat, lacks separate grain/scoring/drying operations, pan/food cues, actual residual heat and resting, and forgiving edible/ruined outcomes. Sylvie does not physically execute a demonstration while the player observes. Five distinct introductions do not mean five correctly implemented preparations. Implement this as one dedicated pass after the opening dialogue is approved, using SYSTEMS_COOKING's shared controls.
2. **Hunting observation and tracking are simplified.** HuntProgress requires all four clues, with no redundant sufficient-evidence route; no persistent readable Hunt Notes; following is one authored interaction rather than 2–4 meaningful signs. Brookmaw shed-tail material is awarded on resolution rather than physically recovered as part of investigation. Current location labels are directional stand-ins for the named locations. These are fidelity gaps, not finished Hunting MVP compliance.
3. **Garrick's sequence-break fight is too easy and too generic.** It currently uses 30 HP / 5 damage and an outdoor practice bout. Core Systems calls for an overwhelmingly difficult new-character opponent using normal rules. Exact kit/tuning is deferred; no new combat kit was invented in this audit.
4. **Departure wording corrected.** Garrick now says only “Marlow left.” as specified. The provisional illness timer itself has not been redesigned.
5. **Persistent world means session-only today.** No save/load survives quitting. The target duration has not been verified by a human playthrough. Room/kitchen doors still transfer within the scene. Body/weapon art, lab gestures and NPC execution remain placeholders.
6. **Sylvie's outcome conversation is partial.** Ingredient recognition and shared tasting/unlock beats exist; optional player-reported lethal/nonlethal conversations and beginner supervision are not fully authored into play. Do not infer outcomes from eggs or add unapproved Nightquill loot.

## Conflicts that need Central Brain reconciliation

- CENTRAL_BRAIN_HANDOFF explicitly supersedes the old frontier and says to author/approve opening dialogue before deeper work. START_HERE, PROJECT_STATE.json and UNRESOLVED still point to the next 1–2 hours. Follow the explicit handoff correction; do not expand later content based on the stale entries.
- Core Systems and Central Brain call the first weapon's narrative source unresolved. The user explicitly instructed this prototype to use Garrick's loan, so it remains implemented. Decide whether to promote that loan or replace it in canonical dialogue; the inventory system cannot settle it.
- Central Brain says exact opening dialogue is not yet approved, while the earlier supplied script drove a corrective implementation. Keep authored-source provenance and user playtest approval separate. No declaration that the narrative is now correct is warranted.
- INN_STARTING_AREA's Duskhen passage says “asks how” but explains a joke about a yes/no question. When implementing that optional branch, the actual player line needs to be a yes/no question for “Yes.” to make sense.
- START_HERE does not list INN_STARTING_AREA or SYSTEMS_POTION_MAKING in its main read order. They still matter as dedicated authorities. Add cross-links through the design owners rather than treating omission as deprecation.

## Next smallest production step

Resolve/approve the default Garrick-to-first-lab dialogue against the supplied script, then implement and playtest its wording and staging only. After that, a dedicated Cooking mechanics pass and Hunting observation pass are needed; the current technical skeleton should not be presented as full design compliance.


## Follow-up: 2026-09-09

Main baec2e2 reconciles the frontier conflicts listed above and makes the approved combat refinement the current priority. See COMBAT-REFINEMENT.md for the new implementation. The earlier audit remains historical evidence; its non-combat implementation gaps still apply.

