# MVP Hunting System

The Hunting MVP exists to support the three starter contracts without designing the entire late-game profession.

## Identity

**Hunting is not just killing animals. It is noticing signs, understanding creatures, making a decision, and dealing with the result.**

Core verbs:

**Inspect → Interpret → Follow → Act → Harvest**

Not every hunt needs every step, but this is the shared grammar.

## Starter-Hunt local partner integration — LOCKED

Each starter Hunting contract has its own physically present local potential companion. Detailed authority lives in `HUNTING_PARTNERS.md`; group-combat rules remain owned by `GROUP_COMBAT_PARTY_MVP.md`.

Locked Hunting-facing rules:
- all three starter Hunts remain equal choices;
- whichever Hunt is chosen first may naturally provide the player's first potential-companion exposure;
- local partners do not solve investigation/inference for the player;
- combat is not forced merely to demonstrate party mechanics;
- do not inflate wildlife HP or add artificial extra enemies for tutorial symmetry;
- Cull and Manage remain legitimate;
- if combat occurs, a local partner uses normal Combatant / Side / Controller rules;
- recruitment is optional and conversational, never automatic from contract completion;
- **“Stay on my lead.”** maps to Direct and **“Use your judgment.”** maps to Independent, with small voice polish permitted later;
- if unexpected combat begins before the arrangement is discussed, a physically present local partner defaults to Independent for that encounter;
- do not interrupt an already-started fight with controller setup.

**Character-first guardrail:** the partners' weapons are tactical identities, not their personalities. Hunting content must not reduce Ily, Sable, or Nessa to weapon tutorials wearing NPC faces.

Current scope:
- **Ilyra “Ily” Fen + Mud in the Moonrice:** full integration locked/current for the coordinated Unity pass;
- **Sable Venn:** character-level direction locked, detailed Three Missing by Morning integration deferred;
- **Nessa Vale:** character-level direction locked, detailed When the Wheel Stopped integration deferred.

The local-partner layer must fit around the approved Hunting grammar rather than rewrite it.

## Inspect

The player interacts with clues physically present in the world:
- footprints
- broken stalks
- feathers
- damaged roof/thatching
- claw marks
- disturbed mud
- chewed wood
- nesting material
- eggshells
- droppings
- fur
- clogged channels
- blood spots

Relevant clues get a **subtle nearby interaction indicator**. No giant neon outlines and no detective-vision trail visible from across the map.

## Interpret

Inspecting clues builds observations. Early on, the game should not automatically reveal the full solution after one clue.

Example:
- deep broad tracks
- rice crushed rather than cleanly eaten
- mudgrubs exposed underneath

Possible inference:
**The animal may be digging through the crop to reach the grubs beneath it.**

Use **Inference** rather than always calling it a final Conclusion so later content can support revised interpretations.

## Hunt Notes

Use a lightweight Hunting observation log so players do not have to remember every clue manually.

Example:

### Reedwater Paddies
- Deep broad tracks
- Rice crushed, little eaten
- Mudgrubs exposed beneath damaged stalks

**Inference:** Something is feeding on the mudgrubs beneath the crop.

The log should support the world rather than replace looking at the world.

## Required vs optional clues

**Optional clues can be missed. Required understanding cannot be permanently bricked by one missed pixel.**

Use multiple clues pointing to the same required inference. A first contract might require enough evidence such as 2 of 4 relevant clues, while extra clues reveal preparation advantages, lore, optional rewards, or more precise understanding.

## Tracking

Tracking uses short chains of **2–4 meaningful signs**.

A sign should do at least one useful thing:
- change direction
- reveal behavior
- warn of danger
- reveal preparation information
- expose an optional opportunity

If a sign only says "walk another fifteen meters," remove it.

Avoid repetitive footprint → footprint → footprint sequences.

## Resolution language

Internal design categories:
- **Cull**: resolve by killing threatening creature(s)
- **Manage**: resolve without exterminating them by redirecting, deterring, relocating, removing access to food, moving habitat, blocking entry, etc.

These are **design categories**, not necessarily literal player-facing buttons.

Player-facing choices should be concrete, such as:
- Attack the Reedback
- Clear the old irrigation trench

Non-lethal solutions arise from understanding creature behavior, not arbitrary morality prompts.

Not every future contract needs both options.

## Pre-unlock behavior

The starter contracts unlock Hunting, so the player must already be able to perform basic inspection, interpretation, and simple harvesting before Hunting Level 1 exists.

Completing the first contract formalizes demonstrated fundamentals into the Hunting progression system.

Do not show something absurd like "You can now inspect footprints" after the player just spent a contract doing so.

## Harvesting

Appropriate killed creatures remain contextually interactable for a simple manual Harvest action.

Example:

> **Harvest Reedback**

Short animation, then guaranteed sensible yield such as:
- Reedback Haunch
- Thick Hide (if appropriate to current content)

For MVP, do **not** build a carcass inventory/micromanagement screen.

No Head / Left Horn / Right Horn / Liver / Toe spreadsheet.

One Harvest interaction is enough.

## Material quality

Do not introduce randomized Common/Rare/Epic meat tiers in the starter experience.

If material conditions are introduced later, they should come from understandable state/cause:
- Fresh
- Preserved
- Damaged
- Pristine

Possible future causes include harvesting method, damage type, tool, timing, or preservation, but starter Cooking ingredients remain guaranteed and reliable.

## Initial Hunting progression direction

After unlock, Hunting should eventually improve three things:

### Detection
Relevant signs become slightly easier to notice at sensible ranges, without glowing through walls.

### Interpretation
The player extracts more information from the same clues.

Progression example:
- early: Large claw marks.
- later: Spacing suggests the creature was running.
- advanced: Marks belong to a juvenile Marshclaw.

### Harvesting
Old creatures can reveal additional useful parts as knowledge increases.

Example progression:
- early: Meat
- later: Meat + Hide
- advanced: Meat + Hide + Scent Gland

This helps preserve old creatures/content as economically relevant.

## What should reward Hunting progress

Favor meaningful activities:
- completing Hunting contracts
- discovering new creature signs
- identifying behavior
- successful tracking
- harvesting new materials
- resolving wildlife problems
- documenting new species knowledge

Avoid mindless +1 Hunting XP for endlessly clicking corpses.

Exact XP/rank structure is deliberately unresolved.

## Failure rules

Starter Hunting should teach observation, not punish curiosity harshly.

- Lose a fight → normal combat failure rules.
- Miss a tracking step → retrace signs.
- Scare an animal away accidentally → signs can locate it again where logical.
- Miss optional clues → lose information/advantages, not entire quest.

# First playable Hunting proof: Mud in the Moonrice

`CONTRACTS.md` owns the contract-specific fiction, clue placement, Toma state, exact route outcomes, and acceptance tests. `HUNTING_PARTNERS.md` owns the now-locked Ilyra Fen integration. This section defines the Hunting-system state grammar that the first implementation must prove.

## Required state separation

The prototype must not collapse the whole contract into one linear quest-stage integer if doing so would force sequence order. Exact code representation is downstream, but gameplay state must distinguish at least:

- contract accepted / not accepted
- individual evidence inspected
- current inference strength/knowledge
- individual tracking signs discovered
- Reedback encountered
- Reedback alive/dead
- Reedback temporarily scared away vs meaningfully redirected
- alternate feeding patch prepared
- redirect behavior confirmed
- carcass harvested
- route ingredient obtained
- wildlife problem resolved
- Hunting formally unlocked
- Toma outcome reported where later dialogue depends on it

These flags may be represented however Unity prefers, but they must support reasonable out-of-order play.

## Investigation threshold rule

The first contract proves the general rule that **required understanding is evidence-threshold based, not single-clue based**.

For Mud in the Moonrice, the implementation target is:
- three meaningful primary clues;
- any two can support the basic feeding-behavior inference;
- exposed mudgrubs plus another supporting clue can produce the explicit mudgrub inference needed to make the environmental redirection solution understandable.

The threshold exists to prevent one missed clue from bricking the hunt. It should not be presented to the player as `2/3 clues found` unless later UI testing proves that necessary. Prefer Hunt Notes/observations that update naturally.

## Systemic discovery and authored world interactions

Knowledge must not act as an invisible permission gate for a physically sensible authored world interaction.

For Mud in the Moonrice, the alternate wet feeding patch exists from the beginning. If the world design supports loosening that patch or opening its small runnel/soft connection, the player may perform those actions whenever the current physical state makes them sensible, including before the formal mudgrub inference has fired.

Inference still has substantial value. It may:
- update Hunt Notes;
- produce sharper player-character observations;
- contextualize what the wet-margin interactions might accomplish;
- make subtle interaction assistance clearer;
- support prediction that a newly exposed mudgrub site may draw the Reedback away from the crop;
- support later reporting/dialogue where relevant.

But inference must not change the underlying laws of the authored world solely by toggling interaction availability.

This does **not** create a universal Dig action for every muddy surface. The interactions remain authored where the environment physically supports them. The rule is simply: **knowledge can reveal purpose; it does not turn physics on.**

Do not label the interactions `Non-lethal solution` or `Spare Reedback`.

## Tracking resilience

Tracking signs should be world objects/areas with persistent discovery state where practical.

If the player leaves the intended route:
- do not fail the quest;
- keep the last confirmed sign available;
- Hunt Notes/objective language may remind them of the last useful direction without drawing a giant arrow to the animal;
- physically reaching the encounter by exploration remains valid.

A discovered later sign may implicitly satisfy earlier route progression where necessary. Do not force the player to walk backward solely to click an intermediate track they already bypassed.

## Encounter entry and sequence freedom

The Reedback exists as part of the location rather than spawning only when an investigation counter reaches 100%.

Therefore:
- a player may encounter it early;
- a player may attack immediately;
- a player may observe it feeding before understanding every clue;
- discovering its behavior directly can count as supporting evidence where the authored scene clearly communicates the same fact;
- a player may prepare part or all of the alternate feeding area early through physically valid authored interactions.

Do not silently teleport or despawn the Reedback merely to restore the intended clue order.

## Early-preparation discovery path

The following sequence is explicitly valid:

**player prepares alternate feeding area early → later Reedback uses it → player/world recognizes durable behavioral redirection → Manage resolution completes**

If this occurs, do not require the player to backtrack and click two clues merely to validate a solution they already discovered through world interaction and observation.

The inference system can update retroactively or through the observed cause-and-effect where useful.

## Temporary displacement vs resolved behavior

**An animal leaving the immediate encounter area is not automatically a solved wildlife problem.**

A Reedback that is merely frightened off still has the same food incentive and may return. Management resolution requires a durable enough behavioral/environmental change for the contract's scope: the alternate feeding patch is prepared and the animal is observed feeding there away from the crop.

## Lethal completion and Harvest

Death and Harvest are separate states.

For Mud in the Moonrice:
1. Reedback death resolves the living threat but does not finish the complete Hunting grammar.
2. The carcass remains contextually interactable.
3. Manual Harvest produces the guaranteed **Fresh Reedback Haunch**.
4. Only then is the lethal route's wildlife/Hunting task considered complete for opening progression.

Do not auto-transfer the haunch on the death frame. Ily does not perform this Harvest for the player.

## Non-lethal ingredient rule

For Mud in the Moonrice, the established **Preserved Reedback Cut** comes from Toma's earlier legitimate regional cull/food supply after the player resolves the present animal/crop problem non-lethally.

This is a contract-specific reward source, not a universal rule that peaceful hunts always generate preserved meat.

## Hunting unlock condition

Formal Hunting unlock happens only after a complete wildlife problem has been resolved through a valid contract route.

For the first implemented contract:
- **Cull path:** Reedback dead + required manual Harvest completed.
- **Manage path:** alternate feeding area successfully prepared + Reedback observed settling into feeding behavior away from the crop.

Do not unlock Hunting on contract acceptance, area entry, first clue, reaching an inference, first attack, Reedback death before Harvest, temporary scare-off, or unrelated wildlife kills.

## Intrinsic-fun acceptance check

The contract succeeds as a Hunting proof if the player can enjoyably notice signs, form a hypothesis, track the creature, choose a direct or environmentally grounded solution, discover valid world interactions, see the problem state change, and deal with the result. Ily may enrich this experience as a character and local collaborator, but the Hunt must still function if the player largely ignores her.

## Current approval boundary

The Mud Hunting implementation package and the detailed Ily integration are both **approved/current for the coordinated Unity pass**.

No new Hunting subsystem is created by the partner layer. Preserve the existing hunt first; layer local character presence and ordinary group-combat participation around it.

## Deferred

Do not define yet:
- exact Hunting XP/rank curve
- Hunting tools/equipment slots
- selective carcass harvesting
- traps
- bait crafting
- trophy rarity
- breeding
- legendary hunts
- specialization trees
- trading economy
- spoilage simulation
- advanced tracking UI
- detailed Sable/Nightquill integration
- detailed Nessa/Brookmaw integration
