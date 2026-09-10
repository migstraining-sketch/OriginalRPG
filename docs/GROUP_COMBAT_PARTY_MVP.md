# Group Combat / Party MVP Design Proposal

## Status / authority

**PROPOSED FOR CENTRAL BRAIN REVIEW. NOT YET LOCKED.**

This document answers how the existing tactical combat can grow into **1–3 allied combatants vs 1–6 enemies**, with NPC allies optionally player-controlled, while preserving possible future human co-op compatibility. It does not design networking, PvP, raids, matchmaking, companion romance/approval, permanent death, or advanced AI scripting.

**Do not modify Unity from this document until Central Brain approves the package.**

## Core architectural direction

Preserve the existing per-unit grammar:

**up to 3 Movement, splittable + one Primary Action per activation**.

Attack, Signature, Defend, Item, Dash, and contextual interactions remain the core verbs. Group depth should come from bodies, lanes, ranges, LOS, terrain, targeting, and coordinated geometry rather than a larger hotbar.

Stop assuming **one Player object vs one Enemy object**. Model **Combatant + Side + Controller + activation state**.

## 1. Recommended turn grammar

Use **alternating unit activations** inside a round.

1. Every living combatant begins the round Ready.
2. Allies receive the first activation in a normal encounter unless an authored ambush says otherwise.
3. One Ready allied combatant activates, then one Ready enemy combatant activates.
4. The activated unit becomes Spent for that round.
5. Continue alternating while both sides have Ready units.
6. If one side runs out first, the other side resolves its remaining Ready units, then the next round begins.
7. No unit activates twice in one round unless a future explicit rule says otherwise.

Each unit tracks its own Movement, Primary Action, Defend state, position, HP, and activation state. Split movement remains per unit.

When several directly controlled allies are Ready, the player chooses which one activates on the allied activation. This creates tactics without an initiative stat: Spear can Drive first to open a lane, Bow can act after LOS opens, or a threatened ally can Defend before another unit commits.

Enemy activation order is chosen by simple readable AI. Important telegraphed commitments remain commitments unless that creature's documented behavior permits changing them.

### Pacing note

With 3 allies vs 6 enemies, the enemy side can have a short tail of consecutive activations after all allies are Spent. Accept this initially, but keep ordinary enemy decisions/animations brisk. If playtesting makes the tail tedious, first tune encounter composition and AI presentation before inventing a complicated initiative economy.

## 2. Direct control vs AI control

Each active NPC companion has a persistent default control preference:

- **Direct:** player chooses that companion's movement/action.
- **Independent:** competent default AI controls it.

Exact UI wording belongs to UI authority.

Recommended rules:
- preference is stored per companion;
- player can change it before combat / while arranging the active party;
- no free mid-combat control-mode switching in MVP;
- no tactics scripting menu, aggression sliders, formations, or behavior editor.

MVP companion AI should understand its actual geometry: avoid obvious telegraphs where practical, seek valid attacks, preserve preferred range, use its Signature when positionally useful, avoid obviously blocking an ally when an equivalent route exists, and Defend when trapped under severe readable threat with no better response.

## 3. Party-state model

Use three distinct concepts.

### Recruited roster
Characters who have agreed to adventure with the player and are available when fiction/state permits. Recruitment does not mean physical presence everywhere.

### Active party
Player character plus up to **2 selected companions** currently traveling/adventuring together.

### Combat participants
Characters physically present in the encounter and placed on the grid. Normally present active-party members enter combat; local NPCs already at the scene may also participate without being recruited.

A recruited NPC back at Garrick's Inn does not materialize into a fight at Reedwater Paddies.

## 4. Exploration representation

Recommend **physical presence with loose-follow abstraction**.

Active companions genuinely travel with the player. Regional-map travel carries the active party together and destination arrival places companions naturally nearby.

During local exploration they follow loosely rather than forming a rigid follower train. In cramped interiors, narrow paths, or camera-sensitive spaces, presentation may abstract slightly: companions catch up after doorways, wait just outside tiny private spaces, use nearby authored idle positions, or rejoin at the next meaningful local space. The fiction remains that they are accompanying the player.

When combat begins, physically present companions resolve to sensible nearby valid starting hexes based on their actual local positions. They are not summoned from a party menu.

## 5. Encounter-size vocabulary and grid implications

Useful authoring vocabulary:
- **Duel / single threat:** focused 1-enemy encounter.
- **Small skirmish:** roughly 1–2 allies vs 2–3 enemies.
- **Party encounter:** roughly 2–3 allies vs 3–4 enemies.
- **Large dangerous group:** up to the intended architectural target of roughly 3 allies vs 5–6 enemies.

These are design labels, not UI tiers.

Do not enlarge every grid globally. Larger encounters may use a modestly wider local grid, but still need meaningful density, legal starting separation, maneuver lanes, Blocking terrain, Bow sightlines/minimum range, and enough space to avoid spawning as a packed blob.

One combatant occupies one hex. Units block movement through occupied hexes unless a future explicit rule says otherwise. No universal opportunity attacks or flanking are added merely because parties exist.

## 6. Multi-enemy intent/readability

Six enemies cannot each own a giant text banner.

Use layered information:
- compact intent icon near each enemy;
- threatened hex/lane highlights only when relevant;
- selected/hovered enemy exposes concise full intent;
- overlapping threat areas combine visually rather than stacking opaque effects;
- committed Pounce/Charge telegraphs remain reliable; broad Pursue intent can remain less exact.

Ordinary AI turns should resolve briskly. Simple enemies decide immediately; ordinary movement/attacks should not pause theatrically; signature telegraphs get more visual weight. The player must understand **what happened and why** without watching six slow AI turns.

UI authority must eventually support: current active unit, Ready/Spent state where needed, ally HP/status, enemy intent, target selection, direct-vs-AI companion distinction, and turn/activation readability while keeping the grid primary.

## 7. Tactical group play

Multi-target combat should make existing rules more expressive:
- bodies shape routes and lanes;
- allies can protect access to vulnerable ranged units through positioning;
- Spear Drive can move a threat away from a Bow user or open a route;
- Sword Lunge can exploit two-hex openings;
- Bow depends on LOS and avoiding range-1 pressure;
- Blocking terrain splits sightlines and creates bottlenecks;
- enemies can pressure different allies instead of always dogpiling the player;
- focus fire emerges from ordinary target choice rather than requiring a new command.

Actions target a specific combatant or hex according to geometry. The combat model must not retain a hidden single-enemy target assumption.

## 8. Win / defeat recommendation

Ordinary lethal combat ends in victory when no hostile combat participant still contests the encounter: enemies may be defeated, fled, surrendered, or otherwise removed by authored rules.

At 0 HP, a combatant becomes **Defeated** and stops receiving activations/intents. Defeated units must not create permanent pathing deadlocks.

Do **not** add permanent companion death or a revive subsystem for MVP. A defeated companion is out for the encounter; post-battle recovery/injury consequences remain unresolved.

### Central Brain decision required: player-character defeat

Do not hard-code the engine to `player HP <= 0 means combat system ends forever`.

Two valid opening policies remain:
- **Player-down = battle loss**, simplest and player-centric.
- **Party may continue**, allowing surviving companions to finish the encounter, which fits party/co-op architecture better but requires recovery consequences.

Core Systems recommendation: architect the player as a normal combatant who can become Defeated, then let encounter rules decide whether that state causes loss. Central Brain can still choose player-down = loss for the opening without baking that assumption into the combat model.

## 9. Future co-op architectural guardrails

No networking design is authorized. Future-proof only the tactical ownership model.

Each combatant should conceptually own:
- identity/data;
- side/team;
- HP/Armor;
- Movement/Primary Action/activation state;
- position;
- equipment/techniques and legal actions;
- controller ownership/type;
- AI behavior only when controller is AI.

Controller can conceptually be local direct input, AI, or a future external/human controller.

Avoid permanent architecture built around one `playerCell`, one `enemyCell`, one `enemyHP`, UI actions that always mutate the player character, enemy AI that can only target the player, battle-end logic that assumes one enemy, or companions implemented as decorative bonus damage outside the normal activation system.

## 10. Opening integration

### Mooncalf herd: multi-enemy proof

If the player attacks/threatens the juvenile Mooncalf, protective adults already have a world-consistent reason to defend it. This should be the first **systemic proof that one encounter can contain multiple enemies**, not a formal tutorial.

Requirements:
- multiple adults can enter one encounter;
- each has independent HP, position, activation, and intent;
- player targets individual enemies normally;
- win/loss handles several hostiles;
- camera/grid remains readable;
- do not inflate Mooncalf-family durability just to make it a boss fight.

### First Hunting contract: allied-combat introduction

`Mud in the Moonrice` should remain fundamentally a Hunting problem about Reedback feeding behavior. Do not enlarge the Reedback into a boss simply because a local NPC ally exists.

Recommended integration:
- a local NPC is physically present/introduced through the contract and can assist during the dangerous confrontation;
- the NPC is a **temporary local ally first**, not automatically a permanent recruit;
- if the player chooses/causes a lethal confrontation, the fight can naturally demonstrate **player + 1 NPC ally vs Reedback** if playtesting shows the ordinary Reedback remains tactically interesting with two allies;
- if two allies trivialize one ordinary Reedback, preserve the contract and teach group combat through a small contextual complication only if ecology/story already justifies it. Do **not** manufacture extra Reedbacks or boss HP merely for tutorial symmetry;
- the non-lethal redirect route must remain fully valid and should not require a combat tutorial;
- after working together, the NPC may later offer/accept recruitment through narrative authority. Core Systems does not invent identity/personality here.

### Recommendation on whether Mud in the Moonrice is the right first ally fight

It is acceptable **if the ally is framed as practical local help and the Reedback encounter remains short**. Its strength is timing: Hunting already brings the player into a field problem and a local helper can be physically present without magical summoning.

However, group combat must not distort the contract. If playtesting shows 2-v-1 Reedback is trivial or tonally silly, use the contract to introduce the **temporary ally / active-participant concept** without forcing combat, and let a later starter hunt provide the first true allied skirmish. Preserve equal starter-contract choice rather than making one contract secretly mandatory party onboarding.

## 11. Specific follow-up requirements for Hunting

After Central Brain approves the Core Systems package, Hunting should decide only contract-facing details:
- where/how the temporary local helper is introduced physically;
- why they are present and willing to assist;
- whether they participate in Cull, Manage, or both without stealing player agency;
- what combat kit/weapon geometry they demonstrate;
- whether the player chooses Direct vs Independent control for this first ally or whether the first encounter defaults to one mode for teaching clarity;
- how they react to early sequence breaks, immediate attack, Reedback Rush, and non-lethal redirection;
- how recruitment becomes available afterward without making the other two starter contracts inferior;
- how the helper remains physically consistent through regional arrival/local exploration/departure.

Hunting must **not** rewrite the approved clue/inference/tracking/redirection structure merely to make room for a companion tutorial.

## 12. Decisions needed before Unity implementation

Central Brain should approve/reject these before the coordinated pass:

1. **Alternating unit activations** as the group-combat extension of current alternating sides.
2. Allied side may choose which Ready directly controlled ally activates next.
3. One activation retains that unit's **3 Movement + one Primary Action + split movement**.
4. Direct/Independent companion control preference is persistent and chosen before combat; no free mid-combat switching in MVP.
5. Recruited roster / Active party / Combat participants are distinct states.
6. Active companions use **physical presence + loose-follow abstraction** during exploration.
7. Up to 2 active companions is the intended party target; roughly 5–6 enemies is an architectural ceiling to support, not an encounter default.
8. Multi-enemy intent uses compact per-enemy icons + on-grid threat shapes + full detail on focus.
9. No universal opportunity attacks/flanking added for group combat.
10. No permanent companion death or revive subsystem in MVP.
11. Combat architecture uses **Combatant / Side / Controller**, not one-player/one-enemy assumptions.
12. Mooncalf herd can serve as the first systemic multi-enemy proof if provoked.
13. Mud in the Moonrice may introduce a physically present temporary NPC ally, but must not inflate Reedback or force combat; if 2-v-1 is weak in playtest, preserve the contract and move the first true allied skirmish later.
14. Decide opening defeat policy when the player character falls while an ally remains, while keeping the underlying architecture encounter-rule-driven.

## Explicitly deferred

Do not design/implement yet:
- networking, matchmaking, servers, lobbies, online persistence;
- PvP, raids, guilds, party finder;
- more than 2 companions / six-player parties;
- companion romance/approval systems;
- permanent companion death;
- revive/injury system;
- companion gear progression;
- advanced companion tactics editor;
- individual initiative/speed stats;
- reaction/reserve-action economy;
- universal opportunity attacks/flanking;
- formation editor;
- final recruit roster or companion identities.
