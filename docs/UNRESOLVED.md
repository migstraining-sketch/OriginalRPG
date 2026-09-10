# Unresolved / Deferred Design

These items are intentionally **not** finalized. Future chats should not invent answers and silently treat them as canon.

## Immediate next frontier

The owner approved implementation after final blocker review. The current frontier is reviewing and human-playtesting the **Marlow → Woodland → Mud in the Moonrice + Ily** implementation branch. See [validation status](../WoodlandSpine/Validation/STATUS.md). Automated success is not acceptance of dialogue staging, UI readability, fun or natural pacing. No complete three-route opening is claimed.
Combat regressions to preserve through implementation review include:
- Sword Lunge
- Spear Drive
- Bow Quick Shot
- Bow targeting/LOS correctness in every hex direction
- first woodland committed Pounce
- Mossback obstacle/lane baiting
- combat clarity and cancel/back behavior

The first 1–2 hours after Cooking unlock remain deliberately deferred until the opening's combat/dialogue experience is proven fun and coherent.

Questions for that later phase still include:
- What does the player naturally do after the opening ends?
- How do the remaining two starter contracts fit into early play?
- When is the first room likely to be affordable?
- How useful are Garrick's starter weapons/armor after the combat refinement?
- How often should the player return to Marlow/Sylvie?
- Does the early game need another local destination or can the inn + nearby contract areas sustain the first hour or two?

## Basic Rest / Rest Quality

### Basic Rest — resolved and locked for MVP

A minimal recovery action now exists and is **not** part of the unresolved Rest Quality system.

**Basic Rest is available for free through Garrick's Inn hearth/common-room rest point and does not require renting a room.**

Basic Rest:
- restores the player to full HP;
- restores present companions to full HP;
- clears the state preventing previously Defeated companions from entering another combat;
- costs nothing;
- requires returning to Garrick's Inn.

A companion who was Defeated and then reaches the post-encounter state at 1 HP after **victory or successful Flee** remains combat-ineligible until Basic Rest.

This resolves the immediate companion-recovery dependency. Do not re-open Basic Rest merely because deeper rest design remains unfinished.

### Rest Quality — still unresolved

Concept exists but exact mechanics do not.

Potential quality ladder mentioned:
**floor < sleeping bag < protected camp < proper bed**

Do not freeze yet:
- Rest Quality bonuses/effects;
- whether Well Fed interacts with Rest Quality;
- room-specific bonuses;
- camp-rest rules;
- fatigue/hunger systems;
- time-management/rest-frequency mechanics;
- paid or premium recovery layers.

The rented room remains valuable through its private bed/rest location, persistent storage, home continuity, and potential future Rest Quality mechanics. It is **not** required for ordinary MVP recovery.

## Well Fed

Locked:
- food does not directly heal
- intro meals all give the same starter value
- Well Fed is sustained preparation/recovery support

Unresolved:
- exact effect
- duration
- stacking
- whether tied directly to Rest Quality

## Potion Making

Locked successful opening: Health Potion learned through Marlow. The reagent-impossibility route does not unlock Potion Making; see `OPENING_MOONCALF_MILK_FAILURE.md`.

Unresolved:
- exact skill/XP curve
- future Mana Potion
- Strength Potion
- recipe requirements
- discovery model
- advanced apparatus
- ingredient contribution ratios
- advanced quality

## Hunting

Locked MVP grammar and starter contracts.

Unresolved:
- XP/rank curve
- tools/equipment
- traps
- bait
- selective harvesting
- trophy systems
- rare/legendary hunts
- specialization
- spoilage/preservation mechanics
- advanced tracking UI

## Cooking

Locked MVP interaction grammar and Sylvie opening.

Unresolved:
- XP/rank curve
- recipe discovery beyond opening
- exact food effects
- advanced cookware
- substitutions
- advanced quality
- specializations
- tools
- restaurant/inn service systems

## Smithing

Confirmed as a future skill/system, but not designed yet.

Early concept:
- could focus first on upgrading/maintaining armor and weapons
- may later include smith-only gear
- throwing knives were discussed as a possible ranged-support product

Do not freeze formulas, materials, progression, or NPC yet.

## Taming

Only teased by peaceful Mooncalf interaction in Marlow's quest.

Do not unlock or explain it in the opening.

Unresolved:
- whether it becomes a formal skill
- creature eligibility
- relationship to pets/companions/mounts
- progression

## Combat

The opening MVP combat model now has a locked/current refinement direction in `CORE_SYSTEMS_PROGRESSION.md` and group-combat authority in `GROUP_COMBAT_PARTY_MVP.md`.

Locked/current opening direction includes:
- small tactical hex grids projected over the exploration environment
- fixed prototype damage and compressed Armor
- Sword, Spear, and Bow geometry identities
- exactly one opening Signature technique per starter weapon
- Sword Lunge
- Spear Drive
- Bow Quick Shot at range 1 only
- no universal Shove for MVP
- readable enemy intent/telegraphs
- first woodland committed Pounce
- Mossback straight-line Charge with deliberate obstacle/lane interaction
- behavioral weaknesses over arbitrary tooltip weaknesses where practical
- teach → mastery → remix enemy evolution
- permanent learned weapon techniques as the preferred progression direction
- group combat supports roughly 1–3 allies vs 1–6 enemies under the locked distributed activation grammar
- player-down = battle loss for opening encounters, while remaining an encounter rule rather than universal engine truth
- defeated companions recover to 1 HP after victory or successful Flee and require Basic Rest before re-entering combat
- free Basic Rest at Garrick's Inn clears that combat-ineligibility state and restores present party HP

Still unresolved:
- final long-term combat stats/formulas
- final technique acquisition/progression model
- weapon mastery structure
- ability-slot limits
- respec rules
- critical hits
- elemental resistances
- armor penetration
- advanced status effects
- cover/high-ground bonuses
- advanced initiative beyond the locked MVP group scheduler
- shield mechanics
- magic/ranged/melee long-term relationships
- exact encounter tuning after Unity playtests
- long-term injury/revival/recovery systems beyond Basic Rest

Do not inherit BattleOn formulas by default.

## Character creation

The larger final character-creation system is not yet frozen for this original version. The opening currently assumes character creation exists and ends with loading directly into Garrick's Inn.

## World / town

Current release focus is Garrick's Inn and nearby local adventure areas.

Opening regional travel, front-door access and knowledge gates are already defined in `TRAVEL_WORLD_MAP_MVP.md`. Inn population, footprint and circulation are defined in `INN_SPATIAL_BLOCKOUT.md`. These are not open design choices merely because final art and wider geography remain deferred.

Unresolved:
- town name
- kingdom/region names
- full world map
- travel model beyond nearby areas
- wider settlements/factions
- main antagonist identity

## Mossback anomaly / larger story

Locked:
- normally docile Mossback attacks during Marlow expedition
- no visible corruption/villain marker
- Marlow says behavior is strange
- this is a microscopic early thread toward a larger antagonist story

Unresolved:
- cause
- antagonist
- timeline
- escalation pattern

Do not prematurely reveal or over-signpost it.

## Garrick / Marlow / Sylvie dialogue

Representative key lines and structure exist, but most exact canonical opening dialogue is still being authored/refined. Preserve personality, state memory, and meaningful-choice rules before implementation improvisation.

## Economy

Unresolved:
- exact starting coins
- room price
- contract coin rewards
- gear prices
- material values
- contribution ratios

Locked directional rule:
The cheapest room is around **5–10 coins beyond starting wealth**, and Marlow's reward should move the player meaningfully toward it without making Hunting mandatory if exploration already supplied enough value.
