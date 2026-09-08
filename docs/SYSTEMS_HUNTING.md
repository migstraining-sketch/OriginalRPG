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
