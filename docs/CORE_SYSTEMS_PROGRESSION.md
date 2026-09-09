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

## MVP combat refinement package — PROPOSED, NOT LOCKED

Latest Unity playtesting exposed that the grid currently exists without demanding enough positional thought in the first battle. The following package is the Core Systems subchat's recommended smallest refinement. Central Brain must approve it before implementation.

### Design target

The first 30 minutes should not require a large ability bar. A new player should understand the universal turn through:

**Move (3, splittable) + one Primary Action**

Primary Actions remain:
- Attack
- Defend
- Item
- Dash
- one weapon-specific Signature technique when available
- contextual environment interaction when authored

Do **not** add a universal Shove for the opening prototype. Displacement is more valuable as a weapon/enemy identity and contextual-environment tool than as a button every character automatically owns.

### Proposed starter weapon signatures

The signatures are alternate tactical actions, not separate cooldown/resource systems. Each spends the normal Primary Action and should normally trade raw damage or positional safety for geometry.

#### Sword — Lunge

**Proposal:** target an enemy exactly 2 hexes away in a straight hex direction. Move 1 hex toward it into the intervening open hex, then strike for reduced damage (prototype target: **4** before Armor).

Purpose:
- sword users can affect a fight from two hexes away without becoming true ranged fighters
- converts position into offense by committing the player closer to danger
- creates a meaningful choice between spending ordinary Movement first for a full 6-damage Attack or using Lunge to preserve Movement for post-attack repositioning
- keeps Sword the flexible/mobile baseline rather than merely "range 1"

Restrictions:
- intervening hex must be walkable/unoccupied
- destination must leave the target adjacent
- straight hex direction only
- no jumping through Blocking/Difficult terrain for free; movement into Difficult terrain should consume/obey normal movement logic if retained after implementation testing

#### Spear — Drive

**Proposal:** attack a target 1–2 hexes away in a straight line for reduced damage (prototype target: **4**) and push it **1 hex directly away** if the destination is open.

Purpose:
- makes Spear about controlling spacing rather than merely having Sword+1 range
- can push enemies out of adjacency, toward/away from terrain, off ideal approach routes, or out of a chokepoint
- creates a tradeoff between full 6-damage basic thrust and lower-damage battlefield control

Restrictions:
- straight-line target geometry remains mandatory
- push fails harmlessly if destination is Blocking/outside grid/occupied; damage still applies
- no collision bonus or hazard damage is required for MVP unless a specific environment later earns it

#### Bow — Quick Shot

**Proposal:** Bow basic Attack remains **5 damage, range 2–4, LOS, no adjacent basic attack**. Quick Shot is an emergency close-range technique at **range 1–2** for reduced damage (prototype target: **3**) with LOS.

Purpose:
- preserves Bow's preferred 2–4 range while preventing adjacency from becoming a dead UI state
- the Bow user can choose a weak shot while pressured, or spend Movement/Dash/Switch Weapon to restore favorable distance
- keeps true ranged identity because full damage and widest targeting still require spacing

This is intentionally less powerful than granting a free retreat or normal-damage adjacent shot. The positional problem should remain real.

### Why no universal Shove yet

A universal Shove is a viable future addition but is not recommended for the first prototype because:
- it increases every character's universal action vocabulary
- it makes Spear displacement less distinctive
- it risks turning many encounters into "walk adjacent and push" instead of weapon-specific geometry
- authored environment interactions can still provide occasional universal positional tricks without creating another permanent button

Revisit only if playtesting shows Sword/Bow users lack enough ways to interact with battlefield geometry.

### Proposed clean Bow validity rule

Bow validity must be determined entirely in combat/hex coordinates, never by camera angle or screen-space click direction.

A Bow basic attack is valid if and only if:
1. attacker and target occupy valid combat hexes
2. hex distance is **2, 3, or 4**
3. target is not on a Blocking hex
4. line of sight between the two hex centers is clear under the grid LOS rule
5. the Primary Action is still available

No requirement exists for attacker/target to share a straight hex axis. Bow should attack any valid hex within radius 2–4 with clear LOS.

Quick Shot, if approved, uses the same rule except allowed distance is **1–2** and damage is reduced.

The current code-level `HexGrid.CanAttack` already expresses range + geometry + LOS independently of camera angle. Reported angle-specific failures should therefore be treated as implementation/input/target-selection defects unless further testing finds a reproducible grid-coordinate rule violation.

### Reusable MVP enemy positional behaviors

Do not make every creature unique through a bespoke subsystem. Opening enemies should be assembled from a small behavioral vocabulary:

1. **Pursue** — route toward an attack position. Baseline behavior, not sufficient by itself for most encounters.
2. **Committed Lunge/Pounce** — telegraph a destination/target hex, then leap/move to that locked location on the enemy phase. If the player leaves, the creature still commits and can lose tempo.
3. **Charge/Rush** — telegraphed straight-line lane with collision/blocking implications. Mossback remains the flagship opening version.
4. **Skirmish/Retreat** — ranged/mobile enemy attempts to restore preferred distance when pressured instead of standing adjacent and trading attacks.
5. **Area/Line Threat** — telegraph a small line, cone-like hex cluster, or local zone that makes standing still costly.
6. **Seek Favored Terrain** — creature tries to reach a terrain type that improves its movement/positioning, e.g. future Brookmaw-water behavior.

Not all six need implementation in the first woodland slice. For MVP opening proof, **Pursue + Pounce**, **Charge**, and eventually **Skirmish** are enough to demonstrate the vocabulary.

### Proposed first woodland battle redesign

The first ordinary woodland fight should remain short, roughly the existing **10 HP / 0 Armor** durability target, but the enemy should not simply approach and Swipe every phase.

Recommended concept:
- small natural clearing with one Blocking object (tree/rock/log) and a small patch of Difficult brush/mud
- creature begins about 3 hexes away rather than already adjacent
- its first distinctive behavior is a readable **Pounce**
- Pounce locks onto the player's current hex (or a clearly marked adjacent landing/attack hex) one phase before resolving
- if the player remains, Pounce deals the creature's normal/heavier damage and ends in close pressure
- if the player steps off the marked hex, the creature still commits to the marked landing hex and ends its action there, giving the player a clean positional opening

This is not intended as a puzzle with one correct answer. Different weapons solve it differently:
- Sword can step aside, then use full Attack or Lunge depending on new spacing
- Spear can keep the landing hex at reach 2 or use Drive to restore spacing
- Bow can relocate to maintain 2–4 range and punish the committed landing; if caught, Quick Shot is a weak fallback

The encounter should naturally teach **"enemy intent marks space; moving changes what happens"** before Mossback escalates that idea into a lethal charge lane.

### Mossback refinement

Keep the established straight-line telegraphed Charge.

Battlefield requirements for the next prototype pass:
- at least **two meaningful Blocking objects/routes**, not one decorative tree placed far from the action
- at least one open lane where Charge is genuinely dangerous
- obstacles positioned so the player can deliberately move across a lane and bait a collision rather than waiting for lucky alignment
- enough open cells around obstacles that all three weapon geometries remain usable
- Difficult terrain may shape routes but should not become the main gimmick

With proposed signatures:
- Sword Lunge can punish from 2 away while preserving movement needed to leave a future charge lane
- Spear Drive can alter spacing before a charge setup without canceling Mossback's identity
- Bow can exploit long sightlines but must keep moving when Mossback closes

Do **not** let player displacement trivially cancel a locked Charge merely by pushing Mossback one hex after telegraph unless implementation testing deliberately proves that interaction is fun. The Charge should remain a commitment the player reads and exploits, not a state deleted by one cheap control action.

### Terrain/environment recommendation

Keep the locked universal terrain categories:
- Open
- Difficult
- Blocking

Do not add a universal Hazard terrain category solely to make the first fight interesting. The grid can already become tactical through movement cost, routing, LOS, pounce/charge telegraphs, and Spear displacement.

For opening MVP, environmental interaction should remain **authored/contextual**, not a universal physics system. Examples worth testing later include dropping/kicking a loose object or opening/closing a route, but no such interaction is required for the first woodland proof if the enemy behavior itself creates positional decisions.

Blocking terrain should do double duty where sensible:
- block movement
- break Bow/ranged LOS
- interrupt Mossback Charge
- create chokepoints/routes

### Combat progression direction — proposal only

Additional combat depth should preferably arrive as **permanent learned techniques** rather than a flood of replacement abilities.

Early direction worth testing later:
- each weapon family begins with Basic Attack plus one clear signature technique
- later progression can permanently unlock a small number of additional weapon techniques through use, trainers, discoveries, quests/contracts, or meaningful milestones
- techniques should add new geometry/timing/positioning options before simply becoming stronger versions of existing attacks
- avoid early cooldown bars, rotating proc systems, and disposable borrowed-power combat kits

Do not build weapon mastery trees, ability-slot limits, respec systems, or final unlock schedules yet.

### Alternatives worth considering

These are not the recommended first prototype but remain viable tests if the package above underperforms:
- Sword signature as a **mobile slash** that attacks adjacent then grants/forces a 1-hex reposition instead of Lunge
- Spear signature as **Brace/Set Spear**, preparing a lane attack against the next enemy entering reach, instead of Drive
- Bow signature as a **Disengaging Shot** that attacks at reduced damage and moves the player 1 hex, instead of Quick Shot
- universal Shove as a fifth core action if weapon-specific displacement proves too restrictive
- a first-enemy short line/cone telegraph instead of Pounce

### Deferred combat ideas

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

### Unity bugs / implementation issues

Known/reported for review before implementation specification:
- **Bow targeting appears angle/distance dependent in play despite design intending any radius-2–4 hex with clear LOS.** Treat as a bug. Combat validity must be based on grid coordinates, not camera angle/click vector.
- Validate screen click → world point → `HexGrid.At` target conversion near hex borders and at different camera angles.
- Validate that highlighted Bow-valid cells and actual `CanAttack` acceptance use the same source of truth. A cell must never render as attackable and then reject the attack unless state changed.
- Existing automated checks cover Bow minimum/maximum range and LOS in at least some axial cases, but should be expanded to **all hex directions and off-axis radius-2/3/4 targets**, especially cases that reproduce the playtest bug.
- Selected-but-uncommitted combat actions already need a cancel/back path per prior playtest notes; any new Signature targeting should inherit the same cancel behavior.

### Questions requiring Central Brain approval

Before implementation, Central Brain should explicitly approve/reject:
1. Whether each starter weapon receives exactly **one Signature technique** in the opening.
2. Sword **Lunge**: range-2 straight commit + reduced damage.
3. Spear **Drive**: reduced-damage thrust + 1-hex push.
4. Bow **Quick Shot**: reduced-damage range-1–2 fallback while normal Bow remains 2–4.
5. No universal Shove for MVP.
6. First woodland creature receiving a telegraphed committed **Pounce** so movement matters immediately.
7. Mossback arena requirement for deliberate obstacle/lane baiting rather than relying on incidental alignment.
8. Permanent learned weapon techniques as the preferred future combat-progression direction.

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
