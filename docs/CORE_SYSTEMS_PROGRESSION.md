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

It does **not** override Central Brain or the dedicated continuity authorities for specific opening content. Detailed Hunting mechanics belong in `SYSTEMS_HUNTING.md`; detailed Cooking mechanics belong in `SYSTEMS_COOKING.md`; opening Potion Making belongs in `SYSTEMS_POTION_MAKING.md`; Inn presentation/dialogue belongs in `INN_STARTING_AREA.md`; chronology belongs in `OPENING_FLOW.md`.

If Core Systems design discovers a contradiction with locked upstream canon, flag it for Central Brain instead of silently rewriting it.

## Project-wide systems philosophy

Locked/high-level direction:
- Progression should feel permanent and meaningful.
- Old content should remain useful whenever practical.
- Avoid constantly invalidating equipment, materials, and zones.
- Avoid seasonal-reset/borrowed-power dependence.
- Avoid currency bloat and reward-screen clutter.
- New/returning players should not be dumped directly into endgame complexity.
- Faster progression should not make slower progression meaningless.
- Give players freedom without making them directionless.
- Exploration, geography, travel, gathering, and preparation should matter.
- Player actions should connect to understandable outputs.
- Grind is acceptable when the goal and reward remain clear.
- Failure should provide learning/progress where appropriate rather than existing only as punishment.
- Systems should be understandable without requiring a wiki open constantly.
- Long-term replayability should come from interconnected systems and durable progression rather than an endless gear treadmill.

## Locked MVP tactical-combat direction

Central Brain has accepted the current opening prototype direction:
- small turn-based hex grids are projected over the real exploration environment
- base player Movement: **3 hexes**
- one **Primary Action** per player turn
- movement may be split before/after the action for the prototype
- alternating Player Phase → Enemy Phase
- no universal opportunity attacks
- no universal facing/flanking system for MVP
- terrain begins with Open, Difficult, and Blocking
- Difficult terrain costs 2 Movement
- important enemy actions use readable intent/telegraphs
- exploration position influences combat starting position
- normal valid attacks do not use a generic random miss chance
- weapon identity begins with geometry/range rather than only damage

The current Mossback prototype identity is a readable straight-line Charge that can be redirected into blocking terrain to create a tactical opening.

## Locked MVP damage / defense model

Prototype formula:

**Damage Taken = max(1, Attack Damage - Armor)**

While Defending:

**Damage Taken = max(1, ceil((Attack Damage - Armor) × 0.5))**

Current prototype assumptions:
- fixed damage rather than random damage ranges
- player starts at **30 HP / 1 Armor**
- player has 0 inherent Armor; worn body equipment supplies Armor
- starter Sword: **6 Damage**
- HP carries most encounter durability; Armor stays compressed and meaningful
- minimum damage floor is 1 for a valid damaging hit unless a future explicit rule says otherwise
- shields are deliberately deferred so they can later receive tactical identity instead of being reduced to passive +Armor

Prototype encounter targets currently used by the Unity slice:
- ordinary woodland creature: about **10 HP / 0 Armor / 4 Damage**
- Mossback: about **34 HP / 1 Armor / 6 Slam**, with Charge as the more dangerous telegraphed attack

Garrick remains a sequence-break opponent who should be overwhelmingly difficult for a new character while still obeying normal combat rules rather than story immunity. Exact final Garrick combat values/kit remain subject to encounter tuning.

## Locked MVP equipment / loadout direction

Functional opening slots:
- **Weapon**
- **Body Armor**

Current weapon prototypes:
- **Sword:** 6 Damage, adjacent range; flexible baseline
- **Spear:** 6 Damage, range 1–2 in a straight hex line; spacing identity
- **Bow:** 5 Damage, range 2–4, line of sight, cannot basic-attack adjacent targets; distance/sightline identity

Other locked/current rules:
- one equipped weapon at a time
- switching outside combat is free
- switching during combat consumes the Primary Action
- starting padded/travel garment supplies Armor 1
- Garrick can offer a meaningful Armor 2 early upgrade
- one general inventory holds equipment, materials, ingredients, potions, and adventure items
- sensible materials stack
- no restrictive carrying capacity for the MVP opening
- buying adds an item to inventory rather than auto-equipping it
- visible body armor/weapons should reflect equipment when production art supports it
- no item-level/gear-score ladder in MVP
- no rarity-color treadmill in MVP
- early gear should include sidegrades as well as occasional true upgrades
- compressed values should keep +1 Damage / +1 Armor meaningful

The exact narrative source of the player's first weapon remains unresolved and belongs with opening/economy decisions rather than being silently dictated by the equipment system.

## Dedicated opening-system authorities

### Hunting

Detailed MVP authority: `SYSTEMS_HUNTING.md`

Locked identity:

**Inspect → Interpret → Follow → Act → Harvest**

Hunting is about understanding and resolving wildlife problems, not only killing creatures.

### Cooking

Detailed MVP authority: `SYSTEMS_COOKING.md`

Locked opening grammar:

**Inspect → Prepare → Set Up → Cook → Read → Remove → Finish**

Food does not provide immediate/reactive healing. The five Sylvie introductory dishes have mechanically equivalent starter value through the placeholder **Well Fed** state.

### Potion Making / Alchemy

Opening authority: `SYSTEMS_POTION_MAKING.md`

Potion Making is introduced early through Marlow and is the current home for immediate/reactive healing through Health Potions. Full Alchemy progression is not yet designed.

### Smithing

Confirmed future system, not yet fully designed.

Do not freeze deep Smithing mechanics, materials, upgrade formulas, durability, repair loops, or NPC structure until the opening loop and equipment behavior are tested.

## Current cross-system proposals — NOT LOCKED

These are working Core Systems proposals from the design chat. They should not be promoted to canon without Central Brain approval.

### Profession identities

- **Hunting:** knowledge + acquisition. Understand creatures/environments and recover useful materials.
- **Alchemy:** adaptation + situational solutions. Prepare consumable answers to specific problems.
- **Cooking:** sustained preparation. Longer-duration readiness/recovery support rather than immediate healing.
- **Smithing:** permanent equipment investment. Gear should be maintained, modified, specialized, or improved rather than constantly discarded.

### Interconnection direction

Preferred broad loop:

**Explore → Hunt/Gather → Recover Materials → Process/Craft → Prepare → Tackle Harder Adventures → Discover New Knowledge/Materials**

Hunting/exploration should be major input sources, while Alchemy, Cooking, and Smithing transform those inputs into different kinds of player power.

Possible cross-links include:
- Hunting → Alchemy: glands, venom, discovered plants/reagents
- Hunting → Cooking: meat, eggs, mushrooms, edible creature materials
- Hunting → Smithing: hides, bone, claws, monster materials, access to resource sites
- Alchemy → Hunting: antidotes, lures, scent masking, oils
- Alchemy → Smithing: tempering agents, solvents, weapon treatments
- Cooking → Hunting: sustained expedition preparation/recovery
- Smithing → Hunting: weapons, harvesting tools, traps/equipment if later approved
- Smithing → Alchemy/Cooking: apparatus/cookware upgrades where useful

Not every link needs equal weight. Avoid dependency spaghetti.

### Anti-obsolescence proposals

- Important materials should usually have more than one meaningful use.
- Advanced recipes may continue using common early materials alongside specialized later catalysts instead of replacing every old resource tier.
- Old creatures can gain new harvest relevance as Hunting knowledge improves.
- Progression should unlock new capabilities/techniques more often than merely adding invisible percentage bonuses.
- Recipes/techniques should often be discovered through the world, experimentation, NPC knowledge, contracts, books, or creature research rather than only purchased from a trainer.
- Avoid long chains of intermediate processing; most useful items should require no more than roughly 1–2 processing stages unless a specific design earns more complexity.

These are directionally aligned with the project's permanence philosophy but remain proposals until Central Brain explicitly locks them.

## Deferred / unresolved Core Systems questions

Do not silently answer these yet:
- exact profession XP/rank/mastery structure
- whether professions use levels, mastery trees, technique unlocks, recipe proficiency, or a hybrid
- whether players can eventually master every profession
- exact long-term combat stats/formulas
- critical hits
- elemental resistances
- armor penetration
- advanced status effects
- cover/high-ground bonuses
- advanced initiative
- shield mechanics
- durability/repair model
- equipment rarity tiers
- enchantments/sockets/set bonuses
- encumbrance/weight limits
- deep Smithing progression
- final Food/Well Fed effects
- Rest Quality formulas
- exact economy/prices/reward values
- full loot/trading economy

## Working rule

When a system becomes specific enough to deserve its own continuity authority, create/update a dedicated system document rather than allowing this file to become a monolithic design dump. Core Systems should remain the cross-system spine and decision ledger.
