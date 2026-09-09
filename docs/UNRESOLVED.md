# Unresolved / Deferred Design

These items are intentionally **not** finalized. Future chats should not invent answers and silently treat them as canon.

## Immediate next frontier

The current priority is to **implement and playtest the approved MVP combat-refinement package** while continuing authored refinement of the canonical Garrick/Marlow opening dialogue.

Immediate combat validation includes:
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

## Rest Quality

Concept exists but exact mechanics do not.

Potential quality ladder mentioned:
**floor < sleeping bag < protected camp < proper bed**

Do not freeze yet:
- what resting restores
- whether it restores HP/MP
- how Well Fed interacts with rest
- room bonuses
- frequency/limits
- camp rules

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

Locked opening: Health Potion learned through Marlow.

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

The opening MVP combat model now has a locked/current refinement direction in `CORE_SYSTEMS_PROGRESSION.md`.

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
- advanced initiative
- shield mechanics
- magic/ranged/melee long-term relationships
- exact encounter tuning after Unity playtests

Do not inherit BattleOn formulas by default.

## Character creation

The larger final character-creation system is not yet frozen for this original version. The opening currently assumes character creation exists and ends with loading directly into Garrick's Inn.

## World / town

Current release focus is Garrick's Inn and nearby local adventure areas.

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
