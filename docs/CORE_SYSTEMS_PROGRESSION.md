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

**Defend duration is now locked:** Defend begins immediately when used and lasts until the start of that Combatant's next activation, when it expires before the new action. It does not expire merely because the global round changed.

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

Committed telegraphs are now explicit: once Pounce, Rush, Charge, or another committed action locks a target/lane/destination/order, it does not silently adapt because another unit acted first unless that creature has an explicit adaptation rule. General intent such as Pursue may remain reactive.

## Group combat / party expansion — LOCKED / CURRENT DIRECTION

Detailed authority:

**`docs/GROUP_COMBAT_PARTY_MVP.md`**

Central Brain considers the group-combat grammar complete enough for the coordinated Unity pass.

Locked direction:
- architecture supports roughly **1–3 allies vs 1–6 enemies**, with six enemies an upper target rather than default encounter size
- ordinary MVP allied Combat Participants are capped at **3 total**, including player, Active Party companions, and temporary/local helpers
- player character + up to **2 active companions**
- model **Combatant + Side + Controller + activation state**, not one-player/one-enemy assumptions
- distinguish **Recruited Roster / Active Party / Combat Participants**
- companions are selected into the **Active Party before travel/encounters**, not summoned when combat starts
- a recruited companion must be part of the current Active Party and physically present to enter ordinary remote combat
- active companions use physical presence with **loose-follow abstraction** during exploration
- companions may use **Direct** or **Independent AI** control chosen before combat; no free mid-combat mode switching for MVP
- every unit retains its own **3 Movement + one Primary Action + split movement**
- no universal opportunity attacks or flanking are added for group combat
- compact per-enemy intent + on-grid threat shapes + focused detail preserve readability
- Mooncalf herd is the first natural multi-enemy combat proof if provoked
- no permanent companion death or revive subsystem for MVP
- future co-op compatibility is architectural only; no networking design

### Distributed activation scheduler — locked

At round start, build the schedule from Combatants who are **Ready and present at that round start**.

When enemies outnumber allies, divide enemy activations into deterministic buckets after allied activation slots.

If `A` is allied Ready count and `E` is enemy Ready count with `E > A`:
- base bucket = `floor(E / A)`
- remainder = `E mod A`
- the first `remainder` buckets receive one extra enemy activation
- schedule one Ally activation, then that enemy bucket, repeating until all scheduled units have had their slot

Examples:
- **3v6:** `A → E → E → A → E → E → A → E → E`
- **3v5:** `A → E → E → A → E → E → A → E`
- **2v5:** `A → E → E → E → A → E → E`

Equal sides alternate normally starting with Allies. If allies outnumber enemies, alternate while enemies remain, then resolve remaining allied activations. No Speed/initiative stat is added.

### Casualties / fleeing inside a round — locked

If a scheduled Combatant becomes **Defeated, flees, surrenders, or otherwise leaves the encounter before its slot**, skip that activation.

Do **not** rebucket or rebuild the remainder of the current round.

Recalculate the next schedule at the start of the following round from then-present Ready Combatants.

### Mixed-controller allied slots — locked

Allied activation slots belong to the **Side**, not controller type.

When multiple allied Combatants are Ready, the player chooses which Ready ally takes the next allied slot. If that Combatant is **Direct**, the player resolves it; if **Independent**, its AI immediately resolves it.

There is no separate initiative track for AI companions.

### Opening defeat / companion recovery — locked

For MVP opening encounters:

**player character Defeated → battle lost.**

This remains an encounter-level rule, not a universal engine-level truth.

A companion Defeated during combat is out for that encounter. After victory, the companion recovers to a stable **1 HP** but is **ineligible for further combat until the party Rests**. They may continue traveling/remaining physically present narratively unless authored content says otherwise.

No revive items, injury tables, permanent companion death, bleed-out timers, or unconscious-body management are part of MVP.

### Starter Hunt companion direction — locked

Each of the **three starter Hunting contracts** should eventually contain its own local NPC character who can work alongside the player during that contract and potentially become recruitable afterward.

Because all three contracts remain equal choices, whichever one the player selects first can organically introduce the first potential companion. `Mud in the Moonrice` is **not** mandatory party onboarding.

These local NPCs should:
- be physically present for believable reasons
- contribute without solving the Hunt for the player
- support lethal and non-lethal routes where reasonable
- not force combat merely to demonstrate party mechanics
- potentially demonstrate different combat styles/weapon geometry
- become recruitable through character/narrative logic rather than as an automatic tutorial reward

The 3-allied-participant cap still applies. A local helper does not become a fourth normal Combat Participant if player + 2 Active Companions are already participating.

Exact identities, personalities, recruitment conditions, and kits belong to Hunting/character authority.

### Coordinated Unity test priority — recommendation

Implementation should prove smaller cases before stress testing the architectural ceiling:

1. **1v1 regression**
2. **1v2 / Mooncalf-style** multi-enemy targeting
3. **2v1 with Ily/local partner**
4. **2v2**
5. **3v3**
6. **3v5 / 3v6** scheduling/readability stress test

This is a testing recommendation, not an authored encounter-progression rule.

**Do not modify Unity from this direction until Central Brain issues/executes the coordinated implementation specification.**

## Dedicated opening-system authorities

- Hunting: `SYSTEMS_HUNTING.md` — **Inspect → Interpret → Follow → Act → Harvest**
- Cooking: `SYSTEMS_COOKING.md` — **Inspect → Prepare → Set Up → Cook → Read → Remove → Finish**
- Potion Making: `SYSTEMS_POTION_MAKING.md`
- Regional travel: `TRAVEL_WORLD_MAP_MVP.md`
- Group combat / party: `GROUP_COMBAT_PARTY_MVP.md`

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
- advanced initiative/reaction economies beyond the locked distributed MVP scheduler
- shields, durability, enchantments, rarity/set systems, encumbrance
- deep Smithing progression
- final Well Fed / Rest Quality formulas beyond the companion combat-eligibility consequence tied to Rest
- exact economy/reward values
- full loot/trading economy
- exact companion identities, kits, recruitment conditions, or technique-acquisition rules
- long-term injury/revival/permanent-death systems
- larger allied encounter rules beyond the ordinary MVP cap of 3 Combat Participants
- networking/co-op implementation

## Working rule

When a system becomes substantial enough, use a dedicated authority rather than allowing Core Systems to become a monolithic design dump. Core Systems remains the cross-system spine and decision ledger.
