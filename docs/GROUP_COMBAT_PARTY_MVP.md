# Group Combat / Party MVP

## Status / authority

**LOCKED / CURRENT MVP DIRECTION, with explicitly noted unresolved future systems.**

This document defines how the existing tactical combat grows into **1–3 allied Combat Participants vs roughly 1–6 enemies**, with NPC allies optionally player-controlled, while preserving possible future human co-op compatibility.

It does **not** design networking, PvP, raids, matchmaking, companion romance/approval, permanent death, revive systems, or advanced AI scripting.

**Do not modify Unity from this document alone.** Central Brain owns the coordinated implementation pass.

## Core architectural direction

Preserve the existing per-unit grammar:

**up to 3 Movement, splittable + one Primary Action per activation**.

Attack, Signature, Defend, Item, Dash, and contextual interactions remain the core verbs. Group depth should come from bodies, lanes, ranges, LOS, terrain, targeting, readable intent, and coordinated geometry rather than a larger hotbar.

Model:

**Combatant + Side + Controller + activation state**.

Each Combatant owns its own identity/data, Side, controller type, HP, Armor, position, Movement, Primary Action state, Defend state, equipment/techniques, and Ready/Spent/Defeated state.

Do not preserve a permanent one-player/one-enemy architecture.

---

## 1. Locked group-combat turn grammar

Use **distributed unit activations inside rounds**. Numerical superiority remains meaningful, but excess enemy activations are distributed through the round rather than dumped into a large end-of-round tail.

### Round start

Build the round schedule from Combatants who are **Ready and present at the start of that round**.

Every scheduled Combatant may activate at most once that round unless a future explicit rule says otherwise.

Allies receive the first activation in a normal encounter unless an authored ambush or encounter rule says otherwise.

Each activation gives that Combatant its own:
- up to **3 Movement**;
- one **Primary Action**;
- split movement before/after the Primary Action where legal.

After activation, that Combatant becomes **Spent** for the round.

No Speed or initiative stat is introduced for MVP.

### Distributed numerical-advantage scheduler

Let:
- `A` = number of Ready allied Combatants at round start;
- `E` = number of Ready enemy Combatants at round start.

If `E > A`:
- base enemy bucket = `floor(E / A)`;
- remainder = `E mod A`;
- the first `remainder` enemy buckets receive one additional activation;
- schedule one allied activation, then that enemy bucket, repeating until the round schedule is complete.

Examples:

**3 allies vs 6 enemies**

`A → E → E → A → E → E → A → E → E`

**3 allies vs 5 enemies**

`A → E → E → A → E → E → A → E`

**2 allies vs 5 enemies**

`A → E → E → E → A → E → E`

If side sizes are equal, alternate normally beginning with Allies:

`A → E → A → E ...`

If allies outnumber enemies, alternate while enemies remain, then resolve the remaining allied activations. An allied tail is player-directed and preserves the advantage of superior allied numbers.

At extreme disparity such as 1v6, consecutive enemy activations are mathematically unavoidable if every enemy acts once. Such encounters should be uncommon and ordinary AI activations must remain brisk.

### Casualties / fleeing during a round — LOCKED

The schedule is **not rebuilt during the round**.

If a scheduled Combatant becomes **Defeated, flees, surrenders, or otherwise leaves the encounter** before its scheduled activation:

**skip that activation.**

Do not rebucket, reorder, or rebuild the remaining round.

At the start of the next round, construct a fresh schedule normally from the Combatants who are then present and Ready.

This prevents removing one Combatant from mysteriously changing everyone else's already-established activation order.

### Allied activation slots and mixed controllers — LOCKED

Allied activation slots belong to the **Side**, not to controller type.

When an allied activation slot arrives and multiple allied Combatants are Ready, the player chooses **which Ready allied Combatant takes that slot**.

Then:
- if the chosen Combatant is **Direct**, the player controls its movement and action;
- if the chosen Combatant is **Independent**, its AI immediately resolves that Combatant's activation.

Direct and Independent companions do **not** receive separate initiative systems or separate activation tracks.

This allows tactical sequencing without Speed stats: for example, choose an Independent Spear ally first so its AI can Drive a threat, then use a later allied slot for a Direct Bow user with newly opened LOS.

### Enemy activation order

Enemy order inside each enemy bucket is chosen by deterministic/simple encounter AI.

Requirements:
- ordinary enemies decide quickly;
- no visible AI deliberation pauses;
- no hidden initiative stat;
- equivalent tie-breaking should be deterministic/reproducible for testing;
- committed telegraphs obey the commitment rule below.

### Committed enemy telegraphs — LOCKED

If an enemy has committed to a telegraphed action such as **Pounce, Rush, or Charge**, that commitment does **not silently change target, lane, destination, or scheduled ordering** because another unit acted first.

Only a creature-specific rule that explicitly permits adaptation may alter a committed action.

General/non-committed intent such as **Pursue** may remain reactive according to that creature's ordinary behavior.

This preserves tactical trust: a committed telegraph is information the player can plan around.

---

## 2. Defend — locked duration rule

Defend remains unit-specific and uses the existing damage model.

**Defend begins immediately when used and lasts until the start of that Combatant's next activation.**

At the start of that unit's next activation, Defend expires **before** it takes its new movement/action.

Do not expire Defend at the global round boundary.

This means Defend behaves consistently even when activation spacing changes because of uneven side sizes.

---

## 3. Direct control vs Independent AI

Each active NPC companion has a persistent default control preference:

- **Direct:** player chooses that companion's movement/action when selected for an allied activation slot.
- **Independent:** competent default AI resolves that companion's activation when selected for an allied activation slot.

Exact UI wording belongs to UI authority.

Locked rules:
- preference is stored per companion;
- player may change it before combat / while arranging the Active Party;
- no free mid-combat control-mode switching in MVP;
- no tactics scripting menu, aggression sliders, formations, or behavior editor;
- both controller modes use the same Side activation schedule.

MVP companion AI should understand its real geometry: seek valid attacks, preserve preferred range, avoid obvious telegraphs where practical, use its Signature when positionally useful, avoid blocking allies when an equivalent route exists, and Defend when trapped under severe readable threat with no better response.

---

## 4. Party-state model

Use three distinct concepts.

### Recruited Roster

Characters who have agreed to adventure with the player and are available when fiction/state permits. Recruitment does not mean physical presence everywhere.

### Active Party

Player character plus up to **2 selected companions** currently traveling/adventuring together.

### Combat Participants

Characters physically present in the encounter and placed on the grid. Normally present Active Party members enter combat. A local NPC already at the scene may also participate only if the allied participant cap permits it.

A recruited NPC back at Garrick's Inn does not materialize into a fight at Reedwater Paddies.

### Active-party selection before travel/encounters — LOCKED

Companions are selected **before travel/encounters**, not summoned when combat starts.

Conceptual flow:

**Recruited Roster → choose up to 2 Active Companions → travel together → physically present companions enter combat**

The exact party-selection UI remains downstream. It may live in the player/party interface or travel preparation, but combat itself cannot summon absent roster members.

### Allied Combat Participant cap — LOCKED

Ordinary MVP combat supports a maximum of:

**3 allied Combat Participants total.**

This total includes all allied units regardless of origin:
- player character;
- Active Party companions;
- temporary/local NPC helpers.

Therefore a full Active Party of **player + 2 companions** does **not** gain a fourth normal activation because a local helper is present.

If a local NPC is present while the allied cap is already full, that NPC may still contribute through authored/contextual non-combat involvement, but does not become an additional normal Combat Participant.

Larger allied encounters are deferred.

---

## 5. Exploration representation

Use **physical presence with loose-follow abstraction**.

Active companions genuinely travel with the player. Regional-map travel carries the Active Party together and destination arrival places companions naturally nearby.

During local exploration they follow loosely rather than forming a rigid follower train. In cramped interiors, narrow paths, or camera-sensitive spaces, presentation may abstract slightly: companions can catch up after doorways, wait outside tiny/private spaces, use nearby authored idle positions, or rejoin at the next meaningful local space.

The fiction remains that they are accompanying the player.

When combat begins, physically present eligible participants resolve to sensible nearby valid starting hexes based on their actual local positions. They are not summoned from a party menu.

---

## 6. Encounter size and grid implications

Useful authoring vocabulary:
- **Duel / single threat:** focused 1-enemy encounter;
- **Small skirmish:** roughly 1–2 allies vs 2–3 enemies;
- **Party encounter:** roughly 2–3 allies vs 3–4 enemies;
- **Large dangerous group:** up to roughly 3 allies vs 5–6 enemies.

These are design labels, not UI tiers.

The architecture supports roughly **1–3 allies vs 1–6 enemies**, but six enemies are not the default.

Do not enlarge every grid globally. Larger encounters may use a modestly wider local grid, but still need meaningful density, legal starting separation, maneuver lanes, Blocking terrain, Bow sightlines/minimum range, and enough room to avoid spawning as a packed blob.

One active Combatant occupies one hex. Units block movement through occupied hexes unless a future explicit rule says otherwise. No universal opportunity attacks or flanking are added merely because parties exist.

---

## 7. Multi-enemy intent and readability

Every important enemy commitment needs readable intent, but six enemies cannot each own a giant text banner.

### On-grid

Use compact visual intent language:
- small intent icon over/near each enemy;
- threatened hex/lane highlight only where relevant;
- selected/hovered enemy gets the clearest full telegraph;
- overlapping threat areas combine visually rather than stacking opaque effects.

### Focus detail

Hover/select/focus can expose concise intent such as:
- Pounce → marked landing hex;
- Rush → marked lane;
- Attack → target/range if committed;
- Retreat / Seek Water → behavioral movement intent.

Do not require opening a separate menu to understand the battlefield.

Committed intent must remain reliable under the commitment rule above. General Pursue-like intent can remain reactive.

### Enemy turn speed

Ordinary AI activations should be brisk. Simple units decide immediately; ordinary movement/attacks should not pause theatrically; special telegraphed actions receive more visual weight.

The player should understand **what happened and why** without watching a wildlife committee hold six consecutive meetings.

UI authority must support current active unit, Ready/Spent state where needed, ally HP/status, enemy intent, targeting, Direct-vs-Independent distinction, and activation readability while keeping the grid primary.

---

## 8. Targeting and tactical group play

All attacks/actions target specific Combatants or hexes according to their geometry. Multi-enemy combat must not retain a hidden single-enemy target assumption.

Group combat should make existing rules more expressive:
- bodies shape routes and lanes;
- allies can protect access to vulnerable ranged units through positioning;
- Spear Drive can move a threat away from a Bow user or open a route;
- Sword Lunge can exploit two-hex openings;
- Bow depends on LOS and avoiding range-1 pressure;
- Blocking terrain splits sightlines and creates bottlenecks;
- enemies can pressure different allies instead of always dogpiling the player;
- focus fire emerges from target choice rather than requiring a new command.

Do not add universal opportunity attacks or flanking solely because parties exist.

---

## 9. Win, defeat, and companion recovery

### Win

For ordinary lethal combat, win when all hostile Combat Participants are **Defeated, fled, surrendered, or otherwise no longer contesting the encounter** according to authored behavior.

This is broader than `enemyHP == 0` and supports wildlife fleeing/non-lethal conclusions later.

### Defeated Combatants

At 0 HP, a Combatant becomes **Defeated** and no longer:
- receives an activation;
- contributes actions/intents;
- blocks victory resolution as an active threat.

If its scheduled activation has not yet occurred this round, that activation is skipped under the locked casualty rule.

Exact body/collision treatment can be implementation-specific for MVP so long as Defeated units do not create permanent pathing deadlocks.

### Opening player-character defeat — LOCKED

For MVP opening encounters:

**Player character Defeated → battle lost.**

This is an encounter rule, not a universal engine truth.

The player character is architecturally a Combatant capable of entering Defeated state. Encounter rules decide whether that state ends battle. Do **not** hard-code combat termination directly to `playerHP <= 0` as a permanent system assumption.

### Defeated companion after victory — LOCKED MVP recovery rule

A companion Defeated during combat remains out for that encounter.

After the encounter ends in victory:
- that companion recovers to a stable **1 HP**;
- they **cannot participate in another combat until the party Rests**;
- they may remain physically present and continue traveling narratively unless an authored scene says otherwise.

This is an MVP consequence, not the final injury/recovery system.

Do **not** add:
- revive items;
- injury tables;
- permanent companion death;
- bleed-out timers;
- unconscious-body management.

The exact later Rest/injury/revival model remains deferred.

---

## 10. Future co-op architectural guardrails

No networking is designed here. Future-proof only tactical ownership.

Each Combatant should conceptually own:
- identity/data;
- Side/team;
- HP/Armor;
- Movement/Primary Action/activation state;
- Defend state;
- position;
- equipment/techniques and legal actions;
- controller ownership/type;
- AI behavior only when controller is AI.

Controller can conceptually be local direct input, AI, or a future external/human controller.

Avoid permanent architecture built around:
- one `playerCell` and one `enemyCell`;
- one hard-coded `enemyHP`;
- UI actions that always mutate the player character rather than the selected/active Combatant;
- enemy AI that can only target the player character;
- battle-end logic that assumes one enemy;
- companion logic implemented as decorative bonus damage outside normal activation rules;
- engine-level assumption that player defeat always universally ends combat.

This is architecture guidance, not networking design.

---

## 11. Natural opening introduction

### Mooncalf herd: first multi-enemy proof

If the player attacks/threatens the juvenile Mooncalf, protective adults already have a world-consistent reason to defend it. This is the first **systemic proof that one encounter can contain multiple enemies**, not a formal party tutorial.

Requirements:
- multiple adults can enter one encounter;
- each has independent HP, position, activation, and intent;
- player targets individual enemies normally;
- win/loss handles several hostiles;
- camera/grid remains readable;
- do not inflate Mooncalf-family durability just to manufacture a boss encounter.

### Starter Hunting contracts: potential companion introductions — LOCKED

**Each of the three starter Hunting contracts should eventually contain its own local NPC character who can work alongside the player during that contract and potentially become recruitable afterward.**

Because all three starter contracts remain equal choices, whichever one the player chooses first can organically introduce the player's first potential companion. `Mud in the Moonrice` is not mandatory party onboarding.

These local NPCs must:
- be physically present for believable reasons;
- contribute without solving the Hunt for the player;
- support lethal and non-lethal routes where reasonable;
- not force combat merely to demonstrate party mechanics;
- potentially demonstrate different combat styles/weapon geometry;
- become recruitable through character/narrative logic rather than as an automatic tutorial reward;
- remain characters first, not mechanically color-coded starter choices.

Peaceful Hunt resolution can still involve meaningful cooperation such as investigation, environmental manipulation, watching the creature, maintaining a route, or confirming behavior.

The allied participant cap still applies. A local helper does not become a fourth Combat Participant when the player already travels with two Active Companions.

Exact identities, personalities, recruitment conditions, and kits belong to Hunting/character authority.

---

## 12. Hunting / character follow-up requirements

For each starter contract, Hunting/character design owns:
- who the local potential partner is;
- why they are physically present and personally invested;
- what they contribute without solving clue/inference structure;
- whether they participate in Cull, Manage, or both;
- what combat style/weapon geometry they demonstrate if combat occurs;
- whether/when Direct vs Independent control is introduced;
- reactions to sequence breaks and unexpected choices;
- believable optional recruitment logic;
- physical continuity through arrival, exploration, resolution, and departure.

Hunting must not rewrite approved clue/inference/tracking/redirection structures merely to make room for a companion tutorial.

---

## 13. Coordinated Unity implementation / testing priority

Architecture should support roughly **1–3 allies vs 1–6 enemies**, but implementation should prove smaller cases first.

Recommended test order:

1. **1v1 regression** — existing single-combat behavior still works under generalized Combatant/Side/Controller architecture.
2. **1v2 / Mooncalf-style** — multiple enemy HP/targeting/activation/intent/win-state.
3. **2v1 with Ily/local partner** — allied selection, Direct/Independent control, participant placement, ally defeat handling.
4. **2v2** — distributed side slots, targeting, mixed intents, casualty skipping.
5. **3v3** — full ordinary allied cap, activation choice/readability.
6. **3v5 / 3v6 stress test** — bucket distribution, compact intent, camera/grid density, AI pacing.

This is a **testing recommendation**, not an authored encounter-progression rule.

Required behavioral tests should include:
- eliminating an enemy before its scheduled slot skips that slot but does not rebucket the current round;
- fleeing/surrender/removal behaves the same way;
- next-round schedule recalculates from currently present Ready Combatants;
- committed Pounce/Rush/Charge keeps its target/lane/destination/order unless creature rules explicitly permit adaptation;
- Defend persists across round boundaries when necessary and expires only at the start of that Combatant's next activation;
- Direct and Independent allies share the same allied activation slots;
- defeated companion returns at stable 1 HP after victory and is combat-ineligible until Rest;
- ordinary allied Combat Participants never exceed 3;
- full player + 2 companion party does not gain a fourth normal activation from a local helper.

---

## 14. UI requirements inherited by implementation

Do not redesign UI authority here. Group combat requires the UI layer to support:
- clearly identified currently active Combatant;
- ally HP/status;
- Ready/Spent state when needed for activation choice;
- compact per-enemy intent;
- threatened hex/lane overlays;
- selected/focused enemy detail;
- targeting among multiple enemies;
- Direct vs Independent companion distinction;
- readable side activation progression without turning the screen into a giant initiative spreadsheet.

The grid remains the primary tactical surface.

---

## Explicitly deferred

Do not design/implement yet:
- networking, matchmaking, servers, lobbies, online persistence;
- PvP, raids, guilds, party finder;
- more than 3 ordinary allied Combat Participants;
- companion romance/approval systems;
- permanent companion death;
- general revive items or revival subsystem;
- injury tables / bleed-out / unconscious-body management;
- companion gear progression unless separately approved;
- advanced companion tactics editor;
- individual Speed/initiative stats;
- reaction/reserve-action economy;
- universal opportunity attacks/flanking;
- formation editor;
- final recruit roster or final companion identities;
- larger allied battle rules;
- exact long-term post-defeat recovery model beyond the locked MVP 1-HP-until-Rest rule.
