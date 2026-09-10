# Starter Hunting Local Partners

## Status / authority

**LOCKED / CURRENT DIRECTION.**

Central Brain has approved the shared starter-Hunt partner doctrine, the character-level identities for all three starter partners, and the full **Mud in the Moonrice + Ilyra Fen** implementation specification for the upcoming coordinated Unity pass.

Detailed implementation for Sable Venn / **Three Missing by Morning** and Nessa Vale / **When the Wheel Stopped** remains deliberately deferred until those contracts receive their own implementation passes.

Upstream authorities remain:
- `GROUP_COMBAT_PARTY_MVP.md` for Combatant / Side / Controller, Direct vs Independent control, activation scheduling, Active Party vs local Combat Participants, defeat rules, and physical-presence doctrine;
- `CORE_SYSTEMS_PROGRESSION.md` for combat geometry and weapon signatures;
- `SYSTEMS_HUNTING.md` for **Inspect → Interpret → Follow → Act → Harvest**;
- `CONTRACTS.md` for each starter Hunt's wildlife problem, clues, inference, tracking, resolution, reward, and persistent consequence;
- `DIALOGUE_PLAYER_AGENCY.md` for conversational agency and information-order rules.

**Do not modify Unity from this document alone.** Central Brain is coordinating one implementation pass.

## Locked shared local-partner doctrine

Each starter Hunting contract has one local NPC who can work beside the player during that contract and may become recruitable afterward.

All three starter Hunts remain equal choices. Whichever Hunt the player chooses first may naturally become the player's first exposure to a potential companion. **Mud in the Moonrice is the first Unity implementation target, not the mandatory party-onboarding route.**

Locked rules:
- the NPC is physically present for a believable local reason before party mechanics are needed;
- the NPC does not identify the Hunt's hidden cause, point out every clue, or solve the investigation for the player;
- the player still owns the core Hunting problem and may sequence-break it normally;
- the NPC can react to discoveries without becoming an answer dispenser;
- combat is not forced merely to demonstrate an ally;
- no creature HP inflation, duplicate target, or artificial extra wildlife is added solely for tutorial symmetry;
- if combat happens, the NPC uses ordinary **Combatant / Side / Controller** rules rather than helper damage outside the turn system;
- if the Hunt resolves peacefully, the NPC remains physically useful through ordinary local work, environmental manipulation, observation, or confirmation;
- Cull and Manage remain legitimate practical routes rather than companion-approval morality tests;
- recruitment is optional and conversational, not an automatic consequence of `contractComplete`;
- recruitment does not automatically place the NPC into the Active Party; normal Recruited Roster / Active Party rules apply afterward.

## Character-first weapon guardrail

**The starter partners' weapons are tactical identities, not their personalities.**

Do not reduce Ily to “the Spear companion,” Sable to “the Bow companion,” or Nessa to “the Sword companion.” Their professions, relationships, motives, worldview, habits, obligations, interests, flaws, and later development must remain capable of carrying them outside combat.

Weapon geometry is one tactical expression of a character, not the character's reason to exist.

## Locked first ally-control choice

Before a plausible confrontation, when the local partner has agreed to accompany the player into the risky part of the Hunt, use one compact character-grounded choice:

- **“Stay on my lead.”** → Direct
- **“Use your judgment.”** → Independent

Exact wording may receive small character-voice polish later without changing the meaning.

Do not dump initiative, Ready/Spent, activation buckets, AI scripting, or controller theory into this exchange. Minimal contextual UI may clarify `Direct` / `Independent` on first exposure if needed.

No free mid-combat control switching is added.

### Unexpected combat before the choice

If combat begins unexpectedly before this arrangement is discussed, a physically present local partner defaults to **Independent** for that encounter when they have a believable reason to intervene.

Do not interrupt already-started combat with controller setup.

### If combat never happens

The local partner remains physically present and useful on a peaceful Manage route. The control choice does not need an artificial combat payoff.

### Local partner vs recruited companion

The contract-time control choice governs a local Combat Participant. It does not mean the NPC is already recruited. If recruitment later succeeds, persistent companion control preference follows the normal party authority.

---

# 1. Mud in the Moonrice — Ilyra “Ily” Fen

## Authority status

**APPROVED AND LOCKED FOR THE COORDINATED UNITY PASS.**

The detailed Mud + Ily integration below is current implementation authority. It must fit around the already-approved Hunt rather than rewriting it.

Preserve without alteration:
- three primary clues;
- flexible clue order;
- two-clue inference resilience;
- explicit mudgrub inference from mudgrubs + supporting evidence;
- three-beat tracking trail;
- no giant direct-to-Reedback objective arrow;
- early Reedback discovery/attack;
- Reedback Pursue + short committed Rush;
- manual Harvest;
- wet-margin redirect;
- early physical interaction with the alternate feeding area;
- temporary scare-off ≠ completion;
- behavioral Manage completion.

## Identity

**Name:** Ilyra Fen  
**Usual name:** Ily  
**Opening role:** seasonal irrigation hand / ditch-mender around Reedwater  
**Weapon:** practical field spear  
**Combat identity:** reach, spacing, lane control, normal Spear + Drive geometry

Ily takes seasonal farm and water-control work across the lowlands. She is not permanently bound to Toma's farm, but she has repaired Reedwater's channels before and knows the local irrigation hardware.

## Personality

Dry, practical, alert, and mildly competitive about physical work. Ily dislikes sloppy fixes and empty boasting but is patient with genuine uncertainty. She prefers the smallest change that actually solves a recurring physical problem.

She does not romanticize Hunting. A Reedback damaging crops is a real problem. Killing it can be reasonable. Redirecting it can also be reasonable if the change actually lasts. Her standard is whether the solution works, not whether it earns moral purity points.

Character texture:
- notices craftsmanship, failed repairs, useful terrain, and whether someone pulls their weight;
- tests practical ideas when doing so is safe;
- skeptical of people who talk bigger than they act;
- quietly enjoys watching water, ground, or machinery behave correctly after a repair.

Her Spear supports her tactical identity, but **it does not define her personality**.

## Why she is at Reedwater

Toma asked Ily to inspect and patch the repeatedly damaged low bund / irrigation edge because Reedback traffic and churned water are beginning to damage water control as well as the rice.

She is already working near the paddy edge when the player arrives, with tools and her field spear nearby. Her presence makes sense even if party mechanics did not exist.

## Relationship to Toma and the problem

Ily and Toma know each other through seasonal farm work. There is mutual respect with ordinary friction: Toma wants the crop protected quickly; Ily is unwilling to promise that repairing the same break again will matter if nobody understands why the animal keeps returning.

She knows:
- which channel edges have been damaged;
- which bund section has been repaired before;
- that simply chasing the animal away has not lasted;
- how to work the authored wet-margin/runnel terrain safely.

She does **not** initially know:
- that mudgrubs are driving the Reedback's behavior;
- the full three-clue inference;
- exactly where the Reedback currently is;
- whether Cull or Manage is the better solution.

She must not pre-solve the Hunt.

## Arrival and first contact

Arrival remains at the sensible farm approach from regional travel.

Toma remains the client and establishes the crop problem. Ily is visible nearby doing real work rather than standing beside him as a companion-selection object.

The player may speak to Toma first, speak to Ily first, inspect evidence first, or move deeper into the paddies and sequence-break toward the Reedback.

If spoken to first, Ily can establish only that she is repairing damage that keeps recurring. She may observe that another patch will not explain why the animal keeps choosing this field. She does not identify mudgrubs or reveal the solution.

## Relationship to the approved clue structure

The existing clues remain:
1. churned feeding patch;
2. exposed mudgrubs;
3. broad Reedback tracks / entry route.

Flexible order and two-clue resilience remain unchanged.

Ily's role is **reactive, not revelatory**:
- after the player inspects churned rice, she may note that it does not resemble clean grazing;
- after the player discovers mudgrubs, she may react to how many are in the turned soil without completing the inference;
- at the entry route, she may distinguish fresh traffic from an older repair, while the tracks still carry the Hunting information.

If the player largely ignores Ily, the Hunt remains fully solvable.

## Following the Reedback

The approved three tracking beats remain unchanged:
1. outbound broad tracks through the low/broken route;
2. muddy flank rub on reeds/brush;
3. fresh digging / newly exposed mudgrubs near the encounter area.

Once there is a practical reason to head after the animal, Ily follows loosely. She waits while the player inspects, catches up naturally on narrow paths, and may confirm a sign after the player finds it. She does not announce the next clue or draw a path to the Reedback.

If the player reaches the Reedback early, Ily may catch up where physically plausible rather than forcing a return to the intended trail.

## First control-mode exchange

Before the final wet-margin / confrontation area, if combat has not already started, Ily can ask whether the player wants her following their calls or handling herself if the Reedback turns on them.

Locked semantic responses:
- **“Stay on my lead.”** → Direct
- **“Use your judgment.”** → Independent

No systems lecture accompanies the exchange.

If the player starts combat before this conversation and Ily is physically present, she enters as **Independent** for that encounter.

## Combat style and tactical geometry

Ily uses the established Spear rules:
- basic thrust at range 1–2 in a straight hex line;
- **Drive** for reduced damage plus a one-hex push when legal.

What her presence can naturally demonstrate:
- an ally can threaten from behind or beside the player rather than crowding the same adjacent cell;
- reach creates lane and sequencing choices;
- Drive can restore spacing after a Rush or alter a vulnerable lane;
- allied positioning matters even in a 2-v-1.

Do **not** increase Reedback durability to guarantee Ily showcase turns. A short 2-v-1 is acceptable.

## Locked Independent AI behavior

If Independent, Ily should:
- seek a legal range-2 straight-line thrust when useful rather than crowding adjacent by default;
- avoid standing in a clearly telegraphed Rush lane where a reasonable alternative exists;
- use Drive when its push creates meaningful spacing, protects a route, or prevents immediate close pressure;
- prefer the full-damage basic attack when the push has no meaningful positional value;
- avoid blocking the player's only clear approach/retreat lane when equivalent positions exist;
- Defend if trapped under severe readable threat with no better attack/reposition.

She receives no bespoke Hunt-only combat powers.

## Cull route participation

If the player attacks or combat otherwise occurs, Ily joins when physically present and circumstances justify it.

Her reaction is practical. She does not shame the player for killing the Reedback and does not celebrate the death as sport.

After victory:
- Ily does not auto-loot;
- Ily does not perform Harvest for the player;
- the player must still manually use **Harvest Reedback**;
- manual Harvest still produces **Fresh Reedback Haunch** and gates lethal completion.

If the player first discovers the Manage possibility and later kills the Reedback, the contract follows the normal Cull completion rule.

## Manage route participation

The approved wet-margin solution remains unchanged and physically available before formal inference where the authored world state supports it.

Player-facing ownership remains:
- the player notices/chooses to loosen the alternate wet feeding patch;
- the player notices/chooses to open the small muddy runnel/soft connection;
- the player may do these early before formal inference;
- the Reedback must later settle into feeding there for Manage completion.

Ily may physically assist **after the player initiates the work** through believable irrigation labor, such as bracing a channel board, holding a tool/edge while the runnel is opened, tamping the crop-side break, or helping stabilize the new water/mud path while they observe.

These are cooperative staging actions, not extra puzzle steps and not requirements for the solution to exist.

If the player prepared the alternate patch before speaking to Ily, she recognizes the work already done and helps only with what remains. She never resets the sequence.

When the Reedback settles into the alternate patch, Ily may quietly confirm the practical result, but the **animal's behavior** remains the authoritative completion evidence.

## Temporary scare-off

Ily does not misread fleeing as success. If the Reedback is only frightened away while the feeding cause remains, she treats the job as unfinished.

## Locked sequence-break support

Support at minimum:
- player investigates before speaking to Ily;
- player finds Reedback early;
- player starts combat before controller choice → Ily defaults Independent;
- player prepares wet margin early;
- player completes most/all Manage setup alone;
- player kills after preparing redirect → Cull + manual Harvest;
- player scares Reedback away → still incomplete;
- player largely ignores Ily → Hunting progression still works.

## Resolution reactions

### Cull

Matter-of-fact acceptance that a real farm problem was resolved. Clean Harvest/non-waste may earn character respect, but Cull itself is not morally penalized.

### Manage

Ily is personally interested because changing the conditions resembles good field work. This is professional satisfaction, not moral approval. Manage does not receive superior rewards or recruitment eligibility.

## Recruitment

Both Cull and Manage can support later recruitment.

Recruitment requires an actual relationship/conversation beat and does **not** automatically follow `contractComplete`.

Because Ily is a seasonal worker rather than permanently tied to Reedwater, future travel is plausible once the immediate water-control work is stabilized. A player may later ask whether she takes work beyond field jobs / whether she would travel with them. Ily can decide based on the relationship and what she observed during the job.

If the player never engages that possibility, no recruitment popup appears.

Successful recruitment places Ily in the **Recruited Roster** according to normal party authority. It does not silently make her an Active Companion.

## Mud-specific implementation state

In addition to the existing Hunting state, implementation must be able to distinguish at least:
- whether Ily has been met;
- whether she is currently cooperating with the player locally;
- her Direct / Independent encounter preference if discussed;
- whether she is physically present when combat begins;
- whether she witnessed the Cull or Manage outcome where dialogue depends on it;
- whether a recruitment conversation has become contextually available;
- whether recruitment has actually succeeded.

Exact variable names are downstream. Never collapse recruitment into `contractComplete == true`.

## Mud + Ily Unity acceptance tests

All existing Mud acceptance tests remain in force, plus:
- Ily is physically present at Reedwater for irrigation-repair reasons before party mechanics are needed;
- original clues, inference resilience, and tracking work with or without speaking to Ily;
- Ily never identifies mudgrubs as the answer for the player or points directly to the Reedback;
- before normal confrontation, Direct/Independent can be established through the approved compact exchange;
- early combat before that exchange defaults a present Ily to Independent without opening setup mid-fight;
- in combat, Ily is a normal allied Combatant using Spear/Drive and normal activation rules;
- Reedback HP/enemy count are not inflated for Ily;
- on Cull, player manual Harvest remains required;
- on Manage, Ily can assist after player initiation without gating the wet-margin interactions;
- early player preparation of the alternate site remains valid;
- temporary scare-off remains incomplete;
- stable Reedback feeding away from the rice remains required for Manage completion;
- Ily can remain physically present through a peaceful route without forcing combat;
- Hunt completion alone does not recruit Ily.

---

# 2. Three Missing by Morning — Sable Venn

## Authority status

**CHARACTER-LEVEL DIRECTION LOCKED. DETAILED CONTRACT INTEGRATION DEFERRED.**

Do not treat this section as a complete implementation spec for Three Missing by Morning.

## Locked identity

**Name:** Sable Venn  
**Relationship:** Mara Venn's adult niece  
**Opening role:** courier / temporary homestead hand  
**Weapon identity:** Bow, centered on range and line of sight

## Locked character direction

Quick-witted, restless, observant, and lightly irreverent under pressure. She dislikes letting an unseen threat choose the terms of an encounter.

Her courier work already takes her along regional roads, making later travel/recruitment plausible without requiring her to abruptly abandon a fixed life.

Cull and Manage remain practical alternatives rather than morality tests.

Her Bow is a tactical identity, **not her personality**. Future writing should develop her courier life, family relationship with Mara, habits, worldview, motives, obligations, and interests independently of ranged combat.

## Deferred to Three Missing implementation pass

Do not lock yet:
- exact first-contact staging;
- exact clue reactions;
- detailed Nightquill follow behavior;
- detailed Bow AI priorities in that encounter;
- exact peaceful participation;
- exact sequence-break responses;
- precise recruitment timing/conditions/dialogue.

The future implementation pass must preserve the existing Nightquill Hunt rather than redesigning it around Sable.

---

# 3. When the Wheel Stopped — Nessa Vale

## Authority status

**CHARACTER-LEVEL DIRECTION LOCKED. DETAILED CONTRACT INTEGRATION DEFERRED.**

Do not treat this section as a complete implementation spec for When the Wheel Stopped.

## Locked identity

**Name:** Nessa Vale  
**Relationship:** Oren Vale's younger sister  
**Opening role:** working mill hand / repairer  
**Weapon identity:** Sword, centered on flexible close movement + Lunge

## Locked character direction

Steady, wry, stubborn, and mechanically curious. Nessa tends to mentally disassemble a broken system while other people are still complaining about it and dislikes repairs that treat symptoms instead of causes.

Her connection to the mill is real. Future recruitment should feel like a meaningful personal choice rather than her abandoning her home and work because a quest ended.

Cull and Manage remain practical alternatives rather than morality tests.

Her Sword is a tactical identity, **not her personality**. Future writing should develop her relationship to Oren, mill work, local responsibilities, habits, worldview, ambitions, and conflicts independently of close-range combat.

## Deferred to When the Wheel Stopped implementation pass

Do not lock yet:
- exact first-contact staging;
- exact Brookmaw clue reactions;
- detailed Sword/Lunge AI priorities in that encounter;
- exact relocation assistance;
- exact sequence-break responses;
- precise recruitment timing/conditions/dialogue.

The future implementation pass must preserve the existing Brookmaw Hunt rather than redesigning it around Nessa.

---

# Design gate status

**Starter-Hunt partner design gate: COMPLETE for the upcoming coordinated Unity pass.**

Current implementation authority for that pass:
- shared local-partner/control/recruitment doctrine: locked;
- Ilyra Fen + Mud in the Moonrice integration: locked/current;
- Sable Venn character-level concept: locked, detailed Hunt integration deferred;
- Nessa Vale character-level concept: locked, detailed Hunt integration deferred.
