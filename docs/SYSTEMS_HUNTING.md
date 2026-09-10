# MVP Hunting System

The Hunting MVP exists to support the three starter contracts without designing the entire late-game profession.

## Identity

**Hunting is not just killing animals. It is noticing signs, understanding creatures, making a decision, and dealing with the result.**

Core verbs:

**Inspect → Interpret → Follow → Act → Harvest**

Not every hunt needs every step, but this is the shared grammar.

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

`CONTRACTS.md` owns the contract-specific fiction, clue placement, Toma state, exact route outcomes, and acceptance tests. This section defines the Hunting-system state grammar that the first implementation must prove.

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

## Knowledge-gated contextual actions

World-first does not mean every authored solution interaction should be active before the player-character understands it.

For the first hunt, the alternate wet feeding patch may physically exist from arrival, but its quest-solving interaction should become meaningful only after the player has enough evidence to connect disturbed mud, exposed mudgrubs, and Reedback feeding behavior.

This is not an invisible morality gate. It represents the player-character knowing **why** loosening/exposing that patch could redirect the animal.

Do not label the interaction `Non-lethal solution` or `Spare Reedback`.

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
- discovering its behavior directly can count as supporting evidence where the authored scene clearly communicates the same fact.

Do not silently teleport or despawn the Reedback merely to restore the intended clue order.

## Temporary displacement vs resolved behavior

The first hunt must establish a reusable distinction:

**An animal leaving the immediate encounter area is not automatically a solved wildlife problem.**

A Reedback that is merely frightened off still has the same food incentive and may return. Management resolution requires a durable enough behavioral/environmental change for the contract's scope: the alternate feeding patch is prepared and the animal is observed feeding there away from the crop.

This distinction should later support deterrence/relocation contracts without treating any fleeing AI state as success.

## Lethal completion and Harvest

Death and Harvest are separate states.

For Mud in the Moonrice:
1. Reedback death resolves the living threat but does not finish the complete Hunting grammar.
2. The carcass remains contextually interactable.
3. Manual Harvest produces the guaranteed **Fresh Reedback Haunch**.
4. Only then is the lethal route's wildlife/Hunting task considered complete for opening progression.

Do not auto-transfer the haunch on the death frame.

## Non-lethal ingredient rule

A Manage route does not need to extract an edible product from the living target.

For Mud in the Moonrice, the established **Preserved Reedback Cut** comes from Toma's earlier legitimate regional cull/food supply after the player resolves the present animal/crop problem non-lethally.

This is a contract-specific reward source, not a universal rule that peaceful hunts always generate preserved meat.

## Hunting unlock condition

Formal Hunting unlock happens only after a complete wildlife problem has been resolved through a valid contract route.

For the first implemented contract:
- **Cull path:** Reedback dead + required manual Harvest completed.
- **Manage path:** alternate feeding area successfully prepared + Reedback observed settling into feeding behavior away from the crop.

Do not unlock Hunting on:
- contract acceptance;
- area entry;
- first clue;
- reaching an inference;
- first attack;
- Reedback death before Harvest;
- temporarily scaring the animal away;
- killing unrelated wildlife.

The unlock presentation should remain modest and integrated with existing opening feedback rather than a giant profession-graduation screen.

## Contract travel boundary

Hunting does not own regional travel, but the first playable contract must be compatible with the approved regional-map authority:

**Garrick's Inn → Regional Map → Reedwater Paddies** after accepting the contract, and **Reedwater Paddies → Regional Map → Garrick's Inn** after or during the visit.

Arrival should begin at a sensible farm approach so the player has room to meet Toma/read the damaged paddies before stepping onto the first evidence. Hunting logic must not assume the player spawned beside a clue or creature.

## Intrinsic-fun acceptance check

Before increasing coin/XP/reward values, playtest the hunt with reward numbers mentally hidden.

The contract succeeds as a Hunting proof if the player can enjoyably:
- notice a small readable set of signs;
- form a cause-and-effect hypothesis;
- follow a short trail without pixel hunting;
- choose between a direct hunt and an environmentally grounded management solution;
- see the paddies/problem state meaningfully change;
- manually deal with the result.

If that loop is weak, fix the investigation, tracking, encounter, or resolution. Do not compensate with larger rewards.

## Approval boundary for this proof

Already locked upstream:
- Hunting identity and five-verb grammar;
- evidence should live in the world with subtle assistance;
- no single mandatory clue should brick the investigation;
- short 2–4-sign tracking chains;
- Cull/Manage are internal categories rather than morality buttons;
- manual Harvest;
- no randomized starter material tiers;
- completing a real starter contract unlocks Hunting;
- Mud in the Moonrice premise and both ingredient outcomes.

New implementation detail proposed for Central Brain/playtest approval:
- exact evidence threshold and explicit mudgrub-knowledge threshold;
- state separation listed above;
- early physical access to the Reedback regardless of investigation completion;
- knowledge-gated contextual activation of the authored redirect interaction;
- redirection success requiring observed stable feeding at the alternate patch rather than simple flee state.

These recommendations deliberately avoid adding traps, bait inventories, procedural tracking, ecology simulation, or a new Hunting-only UI framework.

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
