# Core Systems & Progression

## Ownership / scope

This document is the continuity home for the **Core Systems & Progression** sub-chat.

This sub-chat owns cross-system design such as:
- combat rules and prototype combat math
- equipment/loadout structure
- progression philosophy and skill structures
- economy direction and anti-inflation rules
- relationships between Hunting, Alchemy/Potion Making, Cooking, Smithing, gear, gathering, and exploration
- crafting-system interfaces where more than one profession is affected
- long-term anti-obsolescence rules
- reward structure and systemic progression loops
- identifying when a proposed mechanic belongs in a narrower dedicated system document

It does **not** override Central Brain or dedicated continuity authorities. If Core Systems design discovers a contradiction with locked upstream canon, flag it for Central Brain instead of silently rewriting it.

## Project-wide systems philosophy

Locked/high-level direction includes permanent/meaningful progression, preserving old-content usefulness, avoiding currency/reward clutter and stat inflation, making exploration/preparation matter, and favoring understandable systemic interactions over isolated bars or content treadmills.

### Intrinsic-fun test — locked

Progression/rewards should reinforce enjoyable activities, not compensate for boring ones. Ask whether performing the activity once would still be interesting if XP/reward numbers were temporarily hidden.

### Systemic-discovery principle — locked

Prefer a few understandable, consistent interacting rules over bespoke scripted answers. Allow unexpected-but-valid combinations where practical without building a universal physics sandbox or tutorializing every useful interaction.

## Locked MVP tactical combat

- small turn-based hex grids projected over the real exploration environment
- base Movement **3**
- one Primary Action
- split movement before/after the action
- current single-combat prototype alternates player/enemy phases
- no universal opportunity attacks or universal facing/flanking
- Open / Difficult / Blocking terrain; Difficult costs 2 Movement
- readable enemy intent/telegraphs
- exploration position influences combat start
- normal valid attacks do not use generic random miss chance
- weapon identity begins with geometry/range

### Damage / defense

**Damage Taken = max(1, Attack Damage - Armor)**

While Defending:

**Damage Taken = max(1, ceil((Attack Damage - Armor) × 0.5))**

Prototype: player 30 HP / Armor 1; fixed damage; starter Sword 6. Shields remain deferred.

### Equipment / weapon signatures

Functional opening slots: Weapon + Body Armor. Player has 0 innate Armor; worn body gear supplies it.

- **Sword:** 6 Damage adjacent. **Lunge:** target exactly 2 straight, move into intervening legal hex, 4 Damage before Armor.
- **Spear:** 6 Damage, range 1–2 straight. **Drive:** 4 Damage before Armor + push target 1 hex directly away if legal.
- **Bow:** 5 Damage, range 2–4, LOS. **Quick Shot:** range 1 only, 3 Damage before Armor, LOS, Primary Action, no automatic movement.

No universal Shove for MVP. One equipped weapon at a time; switching in combat spends Primary Action.

### Enemy / encounter doctrine

- first woodland creature uses committed telegraphed Pounce
- Mossback uses readable straight-line Charge and an arena deliberately supporting obstacle/lane baiting
- prefer behavioral weaknesses over arbitrary tooltip weaknesses
- telegraph danger without automatically telegraphing the solution
- support multiple valid encounter answers
- use teach → mastery → remix enemy evolution
- enemy-design tool: **Behavior / Tell / Exploitable Rule / Later Remix Potential**
- combat progression should add permanent tactical verbs horizontally before numerical inflation; exact technique acquisition remains unresolved
- Bow targeting inconsistency is a priority Unity implementation bug

## Group combat / party expansion — PROPOSED, NOT LOCKED

Central Brain has requested a design pass for future-compatible **1–3 allies vs 1–6 enemies**, optional NPC direct/AI control, and eventual co-op compatibility without networking design.

The detailed proposal now lives in:

**`docs/GROUP_COMBAT_PARTY_MVP.md`**

It is deliberately separate because party activation, roster/active-party state, companion world presence, multi-enemy intent, defeat policy, and controller ownership are substantial enough to deserve a narrow authority rather than turning this file into a monolith.

Key recommendations awaiting Central Brain approval:
- alternating **unit activations** inside rounds rather than all allies then all enemies
- player chooses which Ready directly controlled ally activates on allied activations
- every combatant retains its own Movement + Primary Action + split movement
- companion control preference: Direct or Independent AI, chosen before combat; no free mid-combat mode switching for MVP
- distinguish **Recruited roster / Active party / Combat participants**
- active companions are physically present but use loose-follow abstraction during exploration
- architecture uses **Combatant / Side / Controller**, avoiding permanent one-player/one-enemy assumptions
- compact per-enemy intent plus on-grid threat shapes and focus detail
- no new universal opportunity attacks/flanking
- no permanent companion death/revive subsystem for MVP
- Mooncalf herd can naturally prove multi-enemy combat if provoked
- Mud in the Moonrice may introduce a physically present temporary NPC ally, but must not inflate Reedback or force combat merely to justify party onboarding
- player-character-down defeat policy still requires Central Brain decision; underlying architecture should not hard-code `player HP <= 0` as the only possible combat end condition

**Do not modify Unity from the group-combat proposal until Central Brain approves it.**

## Dedicated opening-system authorities

- Hunting: `SYSTEMS_HUNTING.md` — **Inspect → Interpret → Follow → Act → Harvest**
- Cooking: `SYSTEMS_COOKING.md` — **Inspect → Prepare → Set Up → Cook → Read → Remove → Finish**
- Potion Making: `SYSTEMS_POTION_MAKING.md`
- Regional travel: `TRAVEL_WORLD_MAP_MVP.md`
- Detailed group combat proposal: `GROUP_COMBAT_PARTY_MVP.md`

## Cross-system proposals still not locked

Working direction remains:
- Hunting = knowledge/acquisition
- Alchemy = adaptation/situational solutions
- Cooking = sustained preparation
- Smithing = permanent equipment investment

Preferred loop:

**Explore → Hunt/Gather → Recover → Process/Craft → Prepare → Harder Adventures → New Knowledge/Materials**

Anti-obsolescence proposals remain: important materials should have multiple uses; advanced recipes may continue using common materials with specialized catalysts; old creatures can gain new harvest relevance; progression should unlock capabilities more often than tiny percentage bonuses; recipes/techniques can come from world discovery, NPC knowledge, contracts, books, experimentation, or research.

## Deferred / unresolved

Do not silently decide:
- profession XP/rank/mastery structures
- whether players can master every profession
- exact long-term combat stats/formulas
- crits, elemental resistance, penetration, advanced statuses
- cover/high-ground bonuses
- advanced initiative/reaction economies
- shields, durability, enchantments, rarity/set systems, encumbrance
- deep Smithing progression
- final Well Fed / Rest Quality formulas
- exact economy/reward values
- full loot/trading economy
- group-combat items explicitly deferred in `GROUP_COMBAT_PARTY_MVP.md`

## Working rule

When a system becomes substantial enough, use a dedicated authority rather than allowing Core Systems to become a monolithic design dump. Core Systems remains the cross-system spine and decision ledger.
