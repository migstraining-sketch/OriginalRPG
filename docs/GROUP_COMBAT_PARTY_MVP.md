# Group Combat / Party MVP Design Proposal

## Status / authority

**PROPOSED FOR CENTRAL BRAIN REVIEW. NOT YET LOCKED.**

This document answers the current Core Systems question: how the existing tactical combat can grow from one player-controlled combatant vs one enemy into a clean **1–3 allied combatants vs 1–6 enemies** model, while allowing NPC allies to be directly controlled or AI-controlled and without making future human co-op require a tactical rewrite.

This document does **not** design networking, matchmaking, servers, PvP, raids, guilds, companion romance/approval, permanent-death narrative systems, advanced AI scripting, or a final endgame party system.

Do **not** modify Unity from this document until Central Brain approves the package.

## Design goal

Preserve the existing combat grammar at the **combatant** level:

**up to 3 Movement, splittable + one Primary Action per activation**

Attack, Signature, Defend, Item, Dash, and contextual interactions remain the core vocabulary. Group combat should gain depth primarily because several bodies, ranges, lanes, intents, and terrain relationships coexist on the same grid, not because every party member receives a giant hotbar.

The architecture should stop assuming:

**one Player object vs one Enemy object**

and instead think in terms of:

**Combatant + Side + Controller + activation state**.

## 1. Recommended group-combat turn grammar

### Alternating unit activations

Recommended smallest extension:

1. A **round** begins with every living combatant Ready.
2. The allied side receives the first activation in normal encounters unless an authored ambush says otherwise.
3. On an allied activation, one Ready allied combatant activates.
4. On an enemy activation, one Ready enemy activates.
5. Sides alternate activations while both have Ready combatants.
6. A combatant that activates becomes Spent for the round.
7. If one side runs out of Ready combatants first, the other side resolves its remaining Ready combatants, then a new round begins.
8. No combatant activates twice in one round unless a future explicit ability says otherwise.

For the player character or a directly controlled companion, one activation uses the existing grammar:

**Move up to that unit's Movement → one Primary Action → finish remaining Movement if any → End Activation**

Movement is tracked **per combatant**, not as a shared party pool.

Defend is also **unit-specific**. It protects only the combatant that spent its action to Defend, using the existing damage rule unless later revised.

### Why this model

It is a small conceptual extension of the current alternating Player/Enemy structure rather than a replacement with individual initiative stats or action points.

It also prevents the common party-combat problem where the player moves all three allies, deletes half the enemy force before it responds, then watches six enemies act in a long uninterrupted block.

With unequal sides, some consecutive enemy activations can occur after all allies have acted. That is acceptable for larger dangerous groups provided enemy actions are fast and readable. If playtesting makes a 3-enemy tail feel too passive, the first fallback should be encounter composition/AI pacing before inventing a more complicated initiative system.

### Allied activation choice

When more than one directly controlled ally is Ready, **the player chooses which Ready ally activates next** on the allied side's activation.

This is tactically valuable without adding a new stat:
- move the Spear ally first to Drive an enemy into a better lane;
- activate the Bow user after LOS opens;
- let a threatened ally Defend before committing another unit elsewhere.

AI-controlled allies may choose themselves when the allied activation belongs to them, as described below.

### Enemy activation choice

The game chooses one Ready enemy to activate using simple AI priorities. It should favor a readable, tactically sensible order rather than deliberately optimizing like a chess engine.

Important telegraphed commitments remain commitments. An enemy preparing Pounce/Charge should not silently abandon that behavior merely because another unit moved unless its documented behavior allows it.

## 2. Direct control vs AI control

Each active NPC companion has a **control preference**:

- **Direct**: the player chooses movement/action for that companion when it activates.
- **Independent**: competent default AI controls that companion.

Exact player-facing labels remain UI authority territory.

### Recommended setting behavior

- Store a **persistent default preference per companion**.
- Allow the player to change that preference **before combat / while arranging the active party**.
- Do **not** allow free mid-combat switching in MVP. That creates fiddly optimization and ambiguous ownership.
- A future explicit command/control mechanic could revisit mid-battle handoff, but it is unnecessary for the opening.

### AI-controlled companion simplicity

MVP companion AI gets no Dragon Age-style tactics editor.

Use competent default behavior based on the companion's actual combat kit and geometry:
- avoid clearly telegraphed danger where practical;
- seek valid attack positions;
- preserve preferred range when appropriate;
- use its Signature when the positional benefit is meaningful;
- avoid obviously blocking an ally's only route/LOS when an equally good alternative exists;
- Defend when trapped under a severe readable threat and no better action exists.

Do not require the player to configure aggression sliders, target priorities, healing thresholds, formations, or scripts in the opening.

### Who chooses which AI ally goes next?

Recommended MVP: when the allied side receives an activation, the player may select any Ready **directly controlled** ally. If the player has left companions Independent, the game may select a sensible Ready Independent companion when no directly controlled choice is selected / when the player advances the allied activation.

For the first teaching encounter with one NPC ally, this is trivial and avoids a new command layer.

## 3. Party-state model

Use three distinct concepts.

### Recruited roster

Characters who have agreed to adventure with the player and are available for party selection when fiction/state permits.

Being recruited does **not** mean physically present everywhere.

### Active party

The player character plus up to **2 selected companions** currently traveling/adventuring with the player.

Only active-party members are eligible to enter ordinary combat with the player unless an encounter explicitly introduces a local temporary ally already present in the scene.

### Combat participants

The subset of characters physically present in the encounter and placed on the combat grid when combat begins.

Normally:

**active party members present at the location → combat participants**.

Local NPCs already at the scene may also become combat participants without being recruited.

This prevents the inventory-menu summoning problem. A recruited companion sitting back at Garrick's Inn cannot materialize in Reedwater Paddies because combat started.

## 4. Exploration representation of active companions

Recommended approach: **physical presence with loose-follow abstraction**.

Active companions are genuinely traveling with the player, but they do not need to mirror every footstep in a rigid follower train.

### Regional travel

When the party uses the Regional Map, active companions travel as part of the same journey. At destination arrival they appear naturally near the player at the authored arrival area.

### Local exploration

In open local spaces, companions visibly follow at a loose distance and choose nearby sensible positions.

In cramped interiors, narrow paths, interaction-heavy spaces, or camera compositions where literal follower pathing would create clutter, representation may abstract slightly:
- companion catches up after the player clears a doorway;
- waits just outside a tiny private interaction space;
- uses nearby authored idle/stand positions;
- reappears naturally when the party reaches the next meaningful local space.

The fiction remains that they are accompanying the player. The abstraction is presentation/pathing convenience, not teleporting an absent roster member into danger.

### Combat start

When combat begins, physically present active companions snap/resolve to the nearest sensible valid starting hex around their actual local positions, using the same principle already used to project combat over the real environment.

Do not spawn them from an invisible party menu at arbitrary tactical positions.

## 5. Encounter-size vocabulary and grid implications

Recommended design vocabulary, not hard difficulty tiers:

- **Duel / single threat:** 1 ally vs 1 enemy or a similarly focused encounter.
- **Small skirmish:** roughly 1–2 allies vs 2–3 enemies.
- **Party encounter:** roughly 2–3 allies vs 3–4 enemies.
- **Large dangerous group:** up to the intended opening architecture ceiling of roughly 3 allies vs 5–6 enemies.

These are encounter-building labels, not UI categories.

### Grid size

Do not globally enlarge every combat grid merely because 9 combatants are possible.

The existing small-grid philosophy remains. Encounter grids should be authored/generated large enough to provide:
- legal starting separation;
- maneuver lanes;
- room around Blocking terrain;
- viable Bow sightlines and minimum range;
- enough cells that allies/enemies do not begin as a packed blob.

A larger party encounter may need a modestly wider local grid than a duel, but oversized empty battlefields would dilute the positional game and increase walking turns.

### Occupancy

One combatant per hex. Units block movement through their occupied hex unless a future explicit movement rule says otherwise.

Because there are no universal opportunity attacks, body positioning matters through **space, routes, LOS, attack geometry, and protecting access**, not invisible disengagement punishment.

Do not add universal allied/enemy body phasing merely to make pathfinding easier.

## 6. Multi-enemy intent and readability

Every important enemy commitment still needs readable intent, but six enemies cannot each cover the screen in giant labels.

Recommended hierarchy:

### On-grid

Use compact visual intent language:
- small intent icon over/near each enemy;
- threatened hex/lane highlight only when relevant;
- selected/hovered enemy gets the clearest full telegraph;
- overlapping threatened hexes should combine visually rather than stacking opaque effects.

### Detail on focus

Hover/select/focus an enemy to expose concise intent text such as:
- Pounce → marked landing hex
- Rush → marked lane
- Attack → target/range if committed
- Retreat / Seek Water → behavioral movement intent

Do not require opening a separate menu to understand the battlefield.

### Intent certainty

A telegraphed committed action should remain reliable according to that enemy's rules. A general intent such as Pursue can be less exact.

### Enemy turn speed

Ordinary AI activations should be brisk:
- path/movement animation can be accelerated while remaining readable;
- simple enemies should decide immediately;
- do not pause several seconds between each of six enemies;
- special telegraphed actions deserve more visual weight than ordinary reposition/attacks.

The goal is to read **what happened and why** without watching a tiny wildlife committee deliberate for a minute.

## 7. Targeting and tactical group play

All attacks/actions target specific combatants or hexes according to their geometry. Multi-enemy combat must not retain a hidden single `enemy` target assumption.

Group combat should make existing rules more expressive:
- allies can occupy lanes and force enemies to route around them;
- Spear Drive can reposition one threat to protect a Bow user or open a route;
- Sword Lunge can exploit a two-hex opening without consuming the whole approach;
- Bow positioning depends on LOS and keeping enemies out of range 1 pressure;
- Blocking terrain can split sightlines and movement routes;
- enemies can pressure different allies instead of dogpiling the player by default;
- focus fire is allowed but should emerge from target choice, not require a new Focus Fire command in MVP.

Do not add universal attacks of opportunity or flanking solely because parties now exist.

## 8. Win, defeat, and downed-unit recommendation

### Win

For ordinary lethal combat, win when all hostile combat participants are **defeated, fled, surrendered, or otherwise no longer contesting the encounter** according to authored behavior.

This is broader than `enemyHP == 0` and supports wildlife fleeing/non-lethal encounter conclusions later.

### Defeated combatants

A combatant at 0 HP becomes **Defeated** and no longer:
- occupies an active turn;
- contributes actions/intents;
- blocks victory resolution as an active threat.

Exact body/collision treatment can be implementation-specific for MVP so long as a defeated unit does not create a permanent pathing deadlock.

### Player-character defeat

**Central Brain decision required.** Do not silently lock whether the entire battle immediately ends when the player character reaches 0 HP while an allied companion remains standing.

Two viable models:
- **Player-down = battle loss:** simplest and preserves player-character centrality.
- **Party can continue:** directly controlled/AI companions may finish the fight and potentially rescue/recover the player afterward. This better supports future co-op/party ownership but implies additional post-defeat/recovery rules.

Core Systems recommendation for long-term architecture: do **not** hard-code combat termination to `PlayerHP <= 0`. Represent the player character as a combatant that can become Defeated, then let encounter rules decide whether that causes loss. For the opening implementation, Central Brain may still choose player-down = loss without making the engine assumption permanent.

### Companion defeat / death

Do **not** introduce permanent companion death for MVP.

A companion reduced to 0 HP is Defeated for the encounter. What recovery/injury state follows combat remains unresolved and should be designed later with Rest/health/narrative consequences.

No revive action is required for the first group-combat implementation unless Central Brain separately approves one.

## 9. Future co-op architectural guardrails

No networking is designed here. The tactical model should simply avoid assumptions that make future co-op unnecessarily painful.

### Combatant/controller ownership

Each combatant should conceptually have:
- combatant identity/data;
- side/team;
- current HP/Armor/movement/action state;
- position;
- controller ownership/type;
- legal actions from its equipment/techniques;
- AI behavior only when its controller is AI.

Controller can conceptually be:
- local direct player input;
- AI;
- future external/human controller.

Do not make weapon legality, damage, movement, intent, or turn rules depend on `this is the one Player object` where the same rule belongs to any combatant.

### Avoid now

- global singleton state such as one `playerCell` and one `enemyCell` as the permanent combat model;
- one hard-coded `enemyHP` target;
- UI actions that always mutate the player character instead of the currently controlled combatant;
- enemy AI that can only target the player character;
- battle end logic that assumes exactly one enemy exists;
- companion logic that exists only as decorative helper damage outside the normal activation system.

This is architecture guidance, not a request to build online systems.

## 10. Natural opening introduction

### Mooncalf herd: first proof of multiple enemies

The Woodland already provides a natural pre-recruitment multi-enemy case if the player threatens/attacks the juvenile Mooncalf and protective adults respond.

Use this as a **systemic consequence**, not a formal party tutorial.

Requirements:
- several hostile adults can enter one encounter;
- each has its own HP/position/activation/intent;
- player targets individual enemies normally;
- the encounter remains readable even if