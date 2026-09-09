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

### Intrinsic-fun test — locked Core Systems doctrine

Progression and rewards should **reinforce enjoyable activities, not compensate for boring ones**.

When evaluating a core activity, temporarily imagine its XP, currency, and reward numbers are hidden and ask:

> **Would performing this once still be interesting?**

If the answer is no, increasing rewards is not the preferred fix. Improve the activity itself first where practical.

This does not mean rewards are unimportant. Rewards should strengthen motivation around an activity whose moment-to-moment decisions, discovery, execution, or consequences already have value.

### Systemic-discovery principle — locked Core Systems doctrine

Prefer a few **understandable, consistent interacting rules** over bespoke scripted answers.

Players should be allowed to combine systems and discover valid solutions for themselves where practical. Do not immediately tutorialize every useful interaction the systems permit.

In combat this can eventually include combinations among:
- displacement
- enemy telegraphs
- terrain
- line of sight
- creature behavior
- contextual environmental interactions

This principle does **not** authorize a universal physics sandbox or uncontrolled MVP scope. Start with a few strong rules, test whether their interactions are fun, and expand only when playtesting earns more complexity.

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

## MVP combat refinement package — LOCKED / CURRENT MVP DIRECTION

Central Brain approved the opening combat-refinement package with one Bow revision. Do not redesign these rules casually during implementation; implementation discoveries should be reported back to Core Systems/Central Brain.

### Design target

The first 30 minutes should not require a large ability bar. A new player should understand the universal turn through:

**Move (3, splittable) + one Primary Action**

Primary Actions remain:
- Attack
- Defend
- Item
- Dash
- one weapon-specific Signature technique
- contextual environment interaction when authored

There is **no universal Shove** for MVP. Displacement is more valuable as a weapon/enemy identity and contextual-environment tool than as a button every character automatically owns.

### Locked starter weapon signatures

Each starter weapon receives exactly one opening Signature technique. Signatures spend the normal Primary Action and do not introduce a separate cooldown/resource system.

#### Sword — Lunge

Target an enemy exactly **2 hexes away in a straight hex direction**. Move 1 hex toward it into the intervening open hex, then strike for reduced damage; prototype target is **4 Damage before Armor**.

Purpose:
- lets Sword affect a fight from two hexes away without becoming a true ranged weapon
- converts position into offense while committing the player closer to danger
- creates a choice between spending ordinary Movement first for a full 6-damage Attack or using Lunge to preserve more Movement for post-attack repositioning
- keeps Sword the flexible/mobile baseline rather than merely "range 1"

Restrictions:
- intervening hex must be walkable/unoccupied
- destination must leave the target adjacent
- straight hex direction only
- Lunge does not jump through Blocking terrain or erase terrain costs

Exact implementation treatment of entering Difficult terrain should preserve the normal movement rules rather than granting free traversal.

#### Spear — Drive

Attack a target **1–2 hexes away in a straight line** for reduced damage; prototype target is **4 Damage before Armor**, then push it **1 hex directly away** if the destination is open.

Purpose:
- makes Spear about controlling spacing rather than merely having Sword+1 range
- can push enemies out of adjacency, toward/away from terrain, off ideal approach routes, or out of chokepoints
- creates a tradeoff between the full 6-damage basic thrust and lower-damage battlefield control

Restrictions:
- straight-line target geometry remains mandatory
- push fails harmlessly if the destination is Blocking, outside the grid, or occupied; damage still applies
- no universal collision bonus or hazard damage is part of MVP

#### Bow — Quick Shot

Bow basic Attack remains:
- **5 Damage before Armor**
- **range 2–4**
- LOS required
- cannot basic-attack adjacent targets

Quick Shot is the Bow's emergency pressured-state Signature:
- **range 1 only**
- **3 Damage before Armor**
- LOS required
- spends the Primary Action
- no automatic movement or reposition

Spatial identity:
- **range 1:** pressured emergency fallback
- **range 2–4:** preferred firing envelope
- **range 5+:** reposition required

Quick Shot deliberately does not solve the Bow's spacing problem while dealing full damage. **Disengaging Shot is not part of the opening MVP.**

### Clean Bow validity rule — locked design authority

Bow validity must be determined entirely in combat/hex coordinates, never by camera angle or screen-space click direction.

A Bow basic attack is valid if and only if:
1. attacker and target occupy valid combat hexes
2. hex distance is **2, 3, or 4**
3. target is not on a Blocking hex
4. line of sight between the two hex centers is clear under the grid LOS rule
5. the Primary Action is still available

No requirement exists for attacker/target to share a straight hex axis. Bow should attack any valid hex within radius 2–4 with clear LOS.

Quick Shot uses the same legality principles except its distance must be **exactly 1** and it deals 3 Damage before Armor.

The current code-level `HexGrid.CanAttack` already expresses range + geometry + LOS independently of camera angle. Reported angle-specific failures are therefore a priority implementation/input/target-selection bug unless testing uncovers a separate reproducible grid-coordinate defect.

### Reusable MVP enemy positional behaviors

Opening enemies should be assembled from a small reusable behavioral vocabulary rather than every creature receiving a bespoke subsystem:

1. **Pursue** — route toward an attack position. Baseline behavior, not sufficient by itself for most encounters.
2. **Committed Lunge/Pounce** — telegraph a destination/target hex, then leap/move to that locked location on the enemy phase. If the player leaves, the creature still commits and can lose tempo.
3. **Charge/Rush** — telegraphed straight-line lane with collision/blocking implications. Mossback is the flagship opening version.
4. **Skirmish/Retreat** — ranged/mobile enemy attempts to restore preferred distance when pressured instead of standing adjacent and trading attacks.
5. **Area/Line Threat** — telegraph a small line, cone-like hex cluster, or local zone that makes standing still costly.
6. **Seek Favored Terrain** — creature tries to reach a terrain type that improves its positioning, e.g. future Brookmaw-water behavior.

Not all six need implementation in the first woodland slice. For the opening proof, **Pursue + Pounce** and **Charge** are required; Skirmish/other verbs can enter when an encounter needs them.

### First woodland battle — locked direction

The first ordinary woodland fight should remain short, roughly the existing **10 HP / 0 Armor** durability target, but it must not simply approach and Swipe every phase.

Current encounter direction:
- small natural clearing with one Blocking object such as a tree/rock/log and a small patch of Difficult brush/mud
- creature begins about 3 hexes away rather than already adjacent
- its distinctive behavior is a readable **committed Pounce**
- Pounce locks onto the player's current hex, or a clearly marked landing/attack hex, one phase before resolving
- if the player remains, Pounce hits and ends in close pressure
- if the player leaves the marked hex, the creature still commits to the marked landing hex and ends its action there, creating a positional opening

The encounter is **not** a one-answer tutorial puzzle. Different valid answers may include:
- move away and punish the committed landing
- Defend and intentionally absorb the attack
- Sword Lunge when the new spacing supports it
- maintain Spear reach or use Drive to restore spacing
- reposition Bow to preserve its 2–4 firing envelope; use Quick Shot only if caught adjacent
- exploit Blocking terrain when the layout naturally allows it

The lesson is:

> **Enemy intent marks dangerous space; movement changes what happens.**

### Mossback refinement — locked direction

Preserve the established straight-line telegraphed Charge.

The battlefield must deliberately support the mechanic rather than relying on accidental alignment:
- at least **two meaningful Blocking objects/routes**
- at least one open lane where Charge is genuinely dangerous
- obstacles positioned so the player can deliberately bait a collision by moving across/around a lane
- enough open cells around obstacles that all three starter weapon geometries remain usable
- Difficult terrain may shape routes but should not become the main gimmick

The goal is for baiting and redirecting Charge to feel intentional and satisfying.

Do **not** let player displacement trivially cancel a locked Charge simply by pushing Mossback one hex after telegraph unless later playtesting explicitly earns that interaction. The Charge should remain a meaningful commitment to read and exploit.

### Terrain/environment — locked opening direction

Keep the universal terrain categories:
- Open
- Difficult
- Blocking

Do not add a universal Hazard category solely to rescue the first encounter. Movement cost, routing, LOS, Pounce/Charge telegraphs, and Spear displacement should already create tactical value.

Environmental interactions remain **authored/contextual**, not a universal physics system.

Blocking terrain should do multiple jobs where sensible:
- block movement
- break Bow/ranged LOS
- interrupt Mossback Charge
- create chokepoints/routes

### Combat progression direction — locked principle, unresolved implementation

The preferred long-term direction is **permanent learned weapon techniques** rather than a flood of temporary/replacement abilities.

Locked direction:
- each opening weapon begins with Basic Attack plus one Signature technique
- later combat growth should add meaningful geometry, timing, setup, movement, control, or other new tactical verbs before relying on numerical inflation
- horizontal tactical progression is preferred before vertical stat inflation
- avoid filler progression such as tiny +2% damage nodes where a meaningful new capability would better serve the system

Still unresolved:
- exactly how techniques are learned
- whether use, trainers, discoveries, quests/contracts, milestones, or a hybrid grants them
- weapon mastery structure
- ability-slot limits
- respec rules
- final unlock pacing

### Enemy-evolution doctrine — locked

Use **teach → mastery → remix**.

Teach an understandable behavior, let the player become competent against it, then later challenge that familiarity without making the original learning worthless.

Examples of direction, not locked future enemies:
- early enemy teaches committed Pounce; later related enemy may redirect slightly after takeoff
- Mossback teaches heavy straight-line Charge; later charger may break light cover while retaining a readable commitment

Old enemies should often become easier because the player has gained knowledge, techniques, and tactical competence, not because every old creature silently scales to preserve identical difficulty forever.

### Behavioral weaknesses — locked doctrine

Prefer weaknesses arising from **how enemies behave** over arbitrary tooltip-only weaknesses where practical.

Examples:
- Pounce commits to a landing location
- Mossback commits to a charge lane
- future Nightquill behavior may depend on a clean glide lane
- future Brookmaw behavior may favor water

The player should often discover these relationships through cause and effect.

### Telegraph danger, not the solution — locked doctrine

Important enemy intent must be clear enough for informed decisions, but the game should not automatically tutorialize the answer.

Good:
- **Mossback is preparing to charge through these hexes.**

Too explicit:
- **Move behind the tree so Mossback crashes and becomes Staggered.**

Threat information should be clear. Discovering how to exploit that information is part of play.

### Multiple valid answers — locked doctrine

Combat encounters should generally avoid collapsing into scripted command sequences. A state may have a strongest response, but the systems should support multiple understandable valid answers where practical.

This is especially important for tutorial encounters: teach rules and consequences, not one memorized solution.

### Combat clarity — locked doctrine

Because the MVP uses predictable hits, fixed prototype damage, telegraphed intent, and grid legality, visual/input feedback must agree with the underlying rules.

The camera, highlights, LOS display, selected action, and target acceptance should never create conflicting interpretations of what is legal.

The Bow targeting inconsistency directly undermines tactical trust and is therefore a **priority Unity bug** before/alongside implementation of the approved refinement package.

### Enemy-design template — locked Core Systems tool

When defining an opening enemy, document at minimum:
- **Behavior:** what positional pattern it tries to execute
- **Tell:** what the player can read before the important action resolves
- **Exploitable Rule:** the discoverable limitation/weakness in that behavior
- **Later Remix Potential:** one way future content could challenge mastery without discarding the original lesson

Example:

**Mossback**
- Behavior: straight-line Charge
- Tell: lowers head + charge lane appears
- Exploitable Rule: cannot redirect once committed; large Blocking terrain stops it
- Later Remix Potential: tougher charger could break light cover but still cannot freely turn mid-charge

This template guides encounter design without requiring every creature to become a giant bespoke subsystem.

### Locked combat doctrine summary

> **Give the player a small number of meaningful verbs, give enemies readable but exploitable behavior, let terrain and geometry change the answer, teach through cause and effect, and make progression expand what the player can do rather than merely inflate numbers.**

## Alternatives worth considering — NOT MVP LOCKS

These remain possible future tests if playtesting shows a locked signature or rule underperforms:
- Sword mobile slash that attacks adjacent then grants/forces a 1-hex reposition
- Spear **Brace/Set Spear** preparing a lane attack against an approaching enemy
- Bow **Disengaging Shot** as a later technique, not an opening Signature
- universal Shove only if future testing shows the universal kit truly needs it
- first-enemy short line/cone telegraph only if committed Pounce fails in playtest

## Deferred combat ideas

Do not add for this refinement pass:
- large ability bars
- cooldown/resource rotations
- universal reactions/opportunity attacks
- universal flanking/facing
- cover percentages
- formal hazard-damage system
- knockback collision damage
- high-ground bonuses
- advanced status system
- combo trees
- weapon mastery trees
- ammo economy
- crit/accuracy layers

## Unity bugs / implementation issues

Known/reported for the later implementation specification:
- **Bow targeting appears angle/distance dependent in play despite design intending any radius-2–4 hex with clear LOS. Treat as a priority bug.** Combat validity must be based on grid coordinates, not camera angle/click vector.
- Validate screen click → world point → `HexGrid.At` target conversion near hex borders and at different camera angles.
- Validate that highlighted Bow-valid cells and actual `CanAttack` acceptance use the same source of truth. A cell must never render as attackable and then reject the attack unless state changed.
- Expand automated Bow checks to **all hex directions and off-axis radius-2/3/4 targets**, especially cases reproducing the playtest bug.
- Add/verify Quick Shot legality tests for exactly range 1, 3 Damage before Armor, LOS, and no reposition.
- Selected-but-uncommitted combat actions need the previously identified cancel/back path; new Signature targeting inherits that rule.

**Do not implement this package in Unity until Central Brain sends the implementation specification.**

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