# Group Combat / Party MVP

## Status / authority

**LOCKED / CURRENT MVP DIRECTION, with explicitly noted unresolved items.**

Central Brain approved this package with one mechanical revision to activation scheduling plus additional locks around active-party selection, opening defeat policy, and starter-Hunt companion structure.

This document defines how the existing tactical combat grows into **1–3 allied combatants vs roughly 1–6 enemies**, with NPC allies optionally player-controlled, while preserving possible future human co-op compatibility.

It does **not** design networking, PvP, raids, matchmaking, companion romance/approval, permanent death, or advanced AI scripting.

**Do not modify Unity from this document alone.** Central Brain is still coordinating the next implementation pass.

## Core architectural direction

Preserve the existing per-unit grammar:

**up to 3 Movement, splittable + one Primary Action per activation**.

Attack, Signature, Defend, Item, Dash, and contextual interactions remain the core verbs. Group depth should come from bodies, lanes, ranges, LOS, terrain, targeting, and coordinated geometry rather than a larger hotbar.

Stop assuming **one Player object vs one Enemy object**. Model:

**Combatant + Side + Controller + activation state**.

Each combatant owns its own HP, Armor, position, Movement, Primary Action state, Defend state, equipment/techniques, and Ready/Spent/Defeated state.

---

## 1. Locked group-combat turn grammar

Use **alternating unit activations inside a round**, with numerical advantage distributed rather than dumped into a large end-of-round enemy tail.

### Base rules

1. Every living combatant begins the round **Ready**.
2. Allies receive the first activation in a normal encounter unless an authored ambush/encounter rule says otherwise.
3. Every combatant activates at most once per round unless a future explicit rule says otherwise.
4. Activating a unit gives that unit its own **3 Movement + one Primary Action + split movement**.
5. After activation, that combatant becomes **Spent** for the round.
6. Defend remains unit-specific.
7. At the end of the schedule, all surviving combatants refresh to Ready and the next round begins.
8. No Speed/initiative stat is introduced for MVP.

### Distributed numerical-advantage schedule

When **enemies outnumber allies**, divide the enemy activations as evenly as possible into one deterministic enemy bucket after each allied activation.

Let:
- `A` = number of Ready allied combatants at round start
- `E` = number of Ready enemy combatants at round start

If `E > A`:
- base enemy bucket size = `floor(E / A)`
- remainder = `E mod A`
- the first `remainder` buckets receive one additional enemy activation
- schedule one allied activation, then that bucket of enemy activations, then the next allied activation, and so on

Examples:

**3 allies vs 6 enemies**

`A → E → E → A → E → E → A → E → E`

**3 allies vs 5 enemies**

`A → E → E → A → E → E → A → E`

**2 allies vs 5 enemies**

`A → E → E → E → A → E → E`

The bucket sizes differ by at most one, so excess enemy activations are distributed through the round rather than tail-loaded.

If side sizes are equal, use ordinary alternation starting with Allies:

`A → E → A → E ...`

If allies outnumber enemies, use ordinary alternation while enemies remain, then allow the remaining allied activations. The passive-watching problem Central Brain is solving is specifically large enemy tails; an allied tail remains player-directed and preserves the benefit of allied numerical superiority.

### Why this rule

- deterministic and easy to explain/debug;
- preserves numerical superiority;
- produces Central Brain's desired 3-v-6 cadence exactly;
- requires no Speed stat, initiative roll, or continuous turn gauge;
- keeps committed enemy telegraphs predictable because each enemy still receives at most one activation in a known round schedule;
- scales to the intended 1–3 allies vs 1–6 enemies without changing the unit action grammar.

At extreme disparity such as 1 ally vs 6 enemies, consecutive enemy activations are mathematically unavoidable if every enemy acts once and the player acts once. Such encounters should be uncommon and ordinary AI activations must remain brisk.

### Allied activation choice

When multiple directly controlled allied combatants are Ready, the player chooses **which Ready ally uses the next allied activation slot**.

This preserves useful sequencing without initiative stats. Example: Spear can Drive first to open a lane, then Bow can exploit LOS on a later allied slot.

Independent-AI companions use the same allied slots and Ready/Spent rules. Exact arbitration when both Direct and Independent companions are Ready can be implementation-simple: a Directly controlled ally choice should not require a separate initiative subsystem.

### Enemy activation order

Enemy order inside each enemy bucket is chosen by deterministic/simple encounter AI.

Requirements:
- committed telegraphed actions remain reliable according to their creature rules;
- ordinary enemies decide quickly;
- AI should not spend visible time "thinking" between activations;
- no hidden initiative stat is needed.

Exact tie-breaking among equivalent enemies can remain an implementation detail so long as it is deterministic/reproducible for testing.

---

## 2. Direct control vs Independent AI

Each active NPC companion has a persistent default control preference:

- **Direct:** player chooses that companion's movement/action.
- **Independent:** competent default AI controls it.

Exact UI wording belongs to UI authority.

Locked rules:
- preference is stored per companion;
- player can change it before combat / while arranging the active party;
- no free mid-combat control-mode switching in MVP;
- no tactics scripting menu, aggression sliders, formations, or behavior editor.

MVP companion AI should understand its actual geometry: avoid obvious telegraphs where practical, seek valid attacks, preserve preferred range, use its Signature when positionally useful, avoid obviously blocking an ally when an equivalent route exists, and Defend when trapped under severe readable threat with no better response.

---

## 3. Party-state model

Use three distinct concepts.

### Recruited Roster

Characters who have agreed to adventure with the player and are available when fiction/state permits. Recruitment does not mean physical presence everywhere.

### Active Party

Player character plus up to **2 selected companions** currently traveling/adventuring together.

### Combat Participants

Characters physically present in the encounter and placed on the grid. Normally present Active Party members enter combat; local NPCs already at the scene may also participate without being recruited.

A recruited NPC back at Garrick's Inn does not materialize into a fight at Reedwater Paddies.

### Active-party selection before travel/encounters — locked

Companions are selected **before travel/encounters**, not summoned when combat begins.

Conceptual flow:

**Recruited Roster → choose up to 2 Active Companions → travel together → physically present companions enter combat**

The exact party-selection UI remains downstream. It may live in the player/party interface or be integrated into travel preparation, but combat itself does not pull absent roster members from an invisible menu.

---

## 4. Exploration representation

Use **physical presence with loose-follow abstraction**.

Active companions genuinely travel with the player. Regional-map travel carries the Active Party together and destination arrival places companions naturally nearby.

During local exploration they follow loosely rather than forming a rigid follower train. In cramped interiors, narrow paths, or camera-sensitive spaces, presentation may abstract slightly: companions catch up after doorways, wait just outside tiny/private spaces, use nearby authored idle positions, or rejoin at the next meaningful local space.

The fiction remains that they are accompanying the player.

When combat begins, physically present companions resolve to sensible nearby valid starting hexes based on their actual local positions. They are not summoned from a party menu.

---

## 5. Encounter-size vocabulary and grid implications

Useful authoring vocabulary:
- **Duel / single threat:** focused 1-enemy encounter.
- **Small skirmish:** roughly 1–2 allies vs 2–3 enemies.
- **Party encounter:** roughly 2–3 allies vs 3–4 enemies.
- **Large dangerous group:** up to the intended architectural target of roughly 3 allies vs 5–6 enemies.

These are design labels, not UI tiers.

The architecture supports roughly **1–3 allies vs 1–6 enemies**, but six enemies are not the default encounter size.

Do not enlarge every grid globally. Larger encounters may use a modestly wider local grid, but still need meaningful density, legal starting separation, maneuver lanes, Blocking terrain, Bow sightlines/minimum range, and enough space to avoid spawning as a packed blob.

One combatant occupies one hex. Units block movement through occupied hexes unless a future explicit rule says otherwise. No universal opportunity attacks or flanking are added merely because parties exist.

---

## 6. Multi-enemy intent and readability

Every important enemy commitment still needs readable intent, but six enemies cannot each cover the screen in giant labels.

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
- do not pause several seconds between each enemy;
- special telegraphed actions deserve more visual weight than ordinary reposition/attacks.

The goal is to read **what happened and why** without watching a tiny wildlife committee deliberate for a minute.

UI authority must eventually support: current active unit, Ready/Spent state where needed, ally HP/status, enemy intent, target selection, Direct-vs-Independent companion distinction, and activation readability while keeping the grid primary.

---

## 7. Targeting and tactical group play

All attacks/actions target specific combatants or hexes according to their geometry. Multi-enemy combat must not retain a hidden single `enemy` target assumption.

Group combat should make existing rules more expressive:
- allies can occupy lanes and force enemies to route around them;
- Spear Drive can reposition one threat to protect a Bow user or open a route;
- Sword Lunge can exploit a two-hex opening without consuming the whole approach;
- Bow positioning depends on LOS and keeping enemies out of range-1 pressure;
- Blocking terrain can split sightlines and movement routes;
- enemies can pressure different allies instead of dogpiling the player by default;
- focus fire is allowed but should emerge from target choice, not require a new Focus Fire command in MVP.

Do not add universal attacks of opportunity or flanking solely because parties now exist.

---

## 8. Win, defeat, and downed-unit rules

### Win

For ordinary lethal combat, win when all hostile combat participants are **Defeated, fled, surrendered, or otherwise no longer contesting the encounter** according to authored behavior.

This is broader than `enemyHP == 0` and supports wildlife fleeing/non-lethal encounter conclusions later.

### Defeated combatants

A combatant at 0 HP becomes **Defeated** and no longer:
- receives an activation;
- contributes actions/intents;
- blocks victory resolution as an active threat.

Exact body/collision treatment can be implementation-specific for MVP so long as a defeated unit does not create a permanent pathing deadlock.

### Opening player-character defeat — locked

For MVP opening encounters:

**Player character Defeated → battle lost.**

However, this is an **encounter rule**, not a universal engine truth.

The player character is architecturally a Combatant capable of entering Defeated state. Encounter rules determine whether that state ends battle. Do **not** hard-code combat termination directly to `playerHP <= 0` as a permanent system assumption.

This preserves future options such as surviving companions, human co-op partners, revival mechanics, or special encounter rules without redesigning combat ownership.

### Companion defeat / death

Do **not** introduce permanent companion death for MVP.

A companion reduced to 0 HP is Defeated for the encounter. What recovery/injury state follows combat remains unresolved and should be designed later with Rest/health/narrative consequences.

No revive action is required for the first group-combat implementation.

---

## 9. Future co-op architectural guardrails

No networking is designed here. The tactical model should simply avoid assumptions that make future co-op unnecessarily painful.

### Combatant/controller ownership

Each combatant should conceptually have:
- combatant identity/data;
- side/team;
- current HP/Armor/movement/action state;
- position;
- controller ownership/type;
- legal actions from equipment/techniques;
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

---

## 10. Natural opening introduction

### Mooncalf herd: first proof of multiple enemies

The Woodland already provides a natural pre-recruitment multi-enemy case if the player threatens/attacks the juvenile Mooncalf and protective adults respond.

Use this as a **systemic consequence**, not a formal party tutorial.

Requirements:
- several hostile adults can enter one encounter;
- each has its own HP/position/activation/intent;
- player targets individual enemies normally;
- the encounter remains readable even with overlapping threat information;
- do not inflate Mooncalf-family durability just to turn the consequence into a boss fight.

### Starter Hunting contracts: potential companion introductions — locked direction

**Each of the three starter Hunting contracts should eventually contain its own local NPC character who can work alongside the player during that contract and potentially become recruitable afterward.**

Because all three starter contracts are equal choices, whichever contract the player chooses first can organically introduce the player's first potential companion.

This explicitly avoids making `Mud in the Moonrice` secretly mandatory party onboarding.

Contract-facing requirements for all three local NPCs:
- physically present for believable local reasons;
- contribute without solving the Hunt for the player;
- support lethal and non-lethal routes where reasonable;
- do not force combat merely to demonstrate party mechanics;
- may demonstrate different combat styles/weapon geometry;
- recruitment is earned through character/narrative logic, not automatically granted as a tutorial reward;
- peaceful Hunt resolution can still involve meaningful physical cooperation such as investigating, manipulating the environment, watching the creature, maintaining a route, or helping confirm behavior.

These NPCs are **characters first**, not three mechanically color-coded starter choices. Exact identities, personalities, relationships, recruitment conditions, and kits belong in the upcoming Hunting/character design pass.

### Mud in the Moonrice specifically

The approved contract remains fundamentally a Hunting problem about Reedback feeding behavior. Do not enlarge the Reedback into a boss or add arbitrary extra enemies merely because a local partner exists.

A local NPC may assist during Cull, Manage, or both if the contract/character design makes that believable. If a 2-v-1 Reedback fight is tactically weak, that is not a reason to distort the ecology. The Hunt can introduce cooperation without forcing a group fight.

### Other starter Hunts

`Three Missing by Morning` and `When the Wheel Stopped` receive the same systemic opportunity: each has its own local potential partner. Hunting/character design should decide how those characters participate without stealing the player's investigation or turning every contract into the same companion tutorial wearing different scenery.

---

## 11. Specific follow-up requirements for Hunting / character design

Hunting and character design should now decide, for **each of the three starter contracts**:
- who the local potential partner is;
- why they are physically present and personally invested;
- what they can contribute without solving the clue/inference structure;
- whether they participate in Cull, Manage, or both;
- what combat style/weapon geometry they can demonstrate, if combat occurs;
- whether/when Direct vs Independent control is introduced to the player;
- how they react to sequence breaks and unexpected player choices;
- how recruitment becomes available afterward through believable character logic;
- how recruitment remains optional and does not make one starter contract mechanically mandatory;
- how they remain physically consistent through arrival, local exploration, resolution, and departure.

Hunting must **not** rewrite approved clue/inference/tracking/redirection structures merely to make room for a companion tutorial.

---

## 12. UI requirements to carry forward

Do not redesign UI authority here. Group combat requires the existing UI architecture to be able to represent:
- clearly active combatant;
- ally HP/status;
- Ready/Spent state where needed;
- compact enemy intent;
- threatened hexes/lanes;
- target selection among several enemies;
- Direct vs Independent control distinction for companions;
- enough activation-order information to understand the distributed round cadence without turning the HUD into a timeline spreadsheet.

Exact composition remains UI/playtest territory.

---

## 13. Before Unity implementation

The group-combat design direction is now sufficiently locked for implementation planning, but the coordinated Unity pass should not begin from this document alone.

Before implementation, the specification should explicitly carry:
- Combatant/Side/Controller/activation-state data model;
- Ready/Spent/Defeated per combatant;
- distributed enemy bucket scheduler;
- per-unit movement/action state;
- multi-target legality rather than one-enemy assumptions;
- encounter-level loss rule for player Defeat;
- active-party membership selected before travel/encounters;
- physically present local NPC combatants independent of recruitment;
- compact multi-enemy intent requirements;
- no networking implementation.

Exact local NPC identities are **not** required to refactor the combat architecture, but are required before implementing those characters into starter Hunting content.

---

## Explicitly deferred / unresolved

Do not design/implement yet:
- networking, matchmaking, servers, lobbies, online persistence;
- PvP, raids, guilds, party finder;
- more than 2 active companions / six-player parties;
- companion romance/approval systems;
- permanent companion death;
- revive/injury system;
- companion gear progression;
- advanced companion tactics editor;
- individual initiative/Speed stats;
- reaction/reserve-action economy;
- universal opportunity attacks/flanking;
- formation editor;
- final recruited roster or companion identities;
- exact technique acquisition/progression;
- exact post-battle recovery rules for Defeated companions.
