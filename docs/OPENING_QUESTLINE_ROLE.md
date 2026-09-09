# Opening Questline Role

## Scope

This design role owns the **playable first ~20–30 minutes** of the original RPG, from character creation through the first Cooking unlock.

The role is responsible for authoring and maintaining continuity for:

- reactive first contact inside Garrick's Inn
- Garrick's default and contextual introduction branches
- Garrick/Marlow opening dialogue
- Bottle-Brain incident and Marlow's invitation downstairs
- first laboratory visit
- Marlow's sick troll problem
- woodland expedition and first Health Potion
- Potion Making unlock
- transition back to Garrick and room/work hooks
- first Hunting-board choice and starter contract pacing
- Hunting unlock
- Garrick-to-Sylvie handoff
- Sylvie's first Cooking introduction only insofar as it completes the opening flow

This role does **not** redesign downstream profession systems, late-game progression, broader world chronology, or the first 1–2 hours after the opening unless Central Brain explicitly extends the scope.

## Continuity authority

Read and preserve these before changing the opening:

1. `CENTRAL_BRAIN_HANDOFF.md`
2. `OPENING_FLOW.md`
3. `CONTRACTS.md`
4. `SYSTEMS_POTION_MAKING.md`
5. `SYSTEMS_HUNTING.md`
6. `SYSTEMS_COOKING.md`
7. `CHARACTERS.md`
8. `UNRESOLVED.md`

Do not silently promote prototype behavior to canon. The Unity prototype is implementation evidence, not design authority.

## Current locked opening spine

**Character Creation → Garrick's Inn → Garrick/Marlow introduction → Marlow's sick troll problem → woodland expedition → Potion Making → Garrick opens contract board → player chooses any one of three Hunting contracts → Hunting unlocks → return with edible material → Garrick sends player to Sylvie → Sylvie demonstrates Cooking → player accepts pantry arrangement → Cooking unlocks → optional first supervised Cooking attempt.**

Target standard-path timing is roughly **27–30 minutes**, with healthy variance around **23–35 minutes**.

## Opening design law

**Freedom of action, not freedom from consequence/context.**

The player receives control immediately. The world reacts logically to what they try before talking to Garrick. Contextual reactions should converge naturally into the opening rather than behaving like invisible tutorial walls.

Examples:

- kitchen first: Garrick stops a stranger from entering the working kitchen
- upstairs first: Garrick establishes that rooms cost money
- basement first: Marlow notices the stranger trying his laboratory door and calls Garrick's attention to it
- board first: the player may inspect work, but Garrick will not send an unproven stranger out to discover what eats them
- merchandise first: looking is allowed; deliberate theft can escalate
- Marlow first: Garrick notices the new face already talking to Bottle-Brain
- idle/sit: allowed; Garrick eventually acknowledges them naturally
- leave: allowed; Garrick may comment, but the player can genuinely exit

Immediate dialogue should remember what the player already discovered.

## Garrick / Marlow opening dialogue frontier

Central Brain has explicitly identified the **canonical playable dialogue** as the immediate design frontier. Implementation should not improvise from character summaries.

The default route should be authored first, then the contextual first-contact branches should converge into the shared scene without manufacturing fake choices.

### Dialogue-system rule

**Continue is not a roleplaying choice.**

Only present a dialogue choice when the player has an actual response, question, decision, attitude, refusal, or roleplaying expression. Do not offer filler options such as `Listen` merely to force a click.

## Shared Garrick → Marlow scene

The working approved structure is:

1. Garrick naturally engages the newcomer and learns the player's name.
2. Garrick establishes only inn information the player has not already discovered.
3. Marlow accidentally breaks the **last usable sample** tied to urgent research.
4. Garrick recognizes the problem and asks about the watch request.
5. Garrick notices the player and offers Marlow the only available "pair of legs" without pretending to trust or endorse them.
6. Marlow objects because they are strangers.
7. Garrick's attitude is essentially: the player may not be much, but they are what Marlow has and time is running out.
8. Marlow decides that if the player is considering the work, they should first see what he is working on.
9. **Marlow invites the player downstairs before Garrick gives the boot-to-the-face warning.**
10. Player may genuinely refuse.

Preferred established rhythm:

> **Garrick:** "Thought you already put in a request."
>
> **Marlow:** "I did."
>
> **Garrick:** "Watch still sitting on it?"
>
> **Marlow:** "They have more urgent matters."

Then:

> **Garrick:** "Might've found you another pair of legs."
>
> **Marlow:** "You don't know them."
>
> **Garrick:** "Nope."

Possible continuation when the player has expressed interest in work:

> **Garrick:** "But they're looking for work."

Otherwise Garrick may simply point out that the newcomer is the person currently available.

Marlow may object that this is not how he chooses expedition partners; Garrick can answer that Marlow is not the one going.

Garrick's endorsement must remain deliberately weak. A representative later beat is:

> **Garrick:** "Look, they're not much."
>
> **Garrick:** "But they're what you've got."

When Marlow hesitates:

> **Garrick:** "Bottle-Brain, you're running out of time."

Marlow then establishes the lab invitation:

> **Marlow:** "If you're actually considering this... you should see what I'm working on first."
>
> **Player:** "Where?"
>
> **Marlow:** "Downstairs. My laboratory."

If the player previously tried the lab door, Marlow can acknowledge that the circumstances are now different.

Only after the invitation exists does Garrick warn:

> **Garrick:** "They try anything funny down there, you holler."

He makes clear the threatened large boot is intended for the player.

Garrick then frames the opportunity simply as work rather than assigning a tutorial quest.

Player responses can include real questions/decisions such as:

- what does Marlow need?
- how much does it pay?
- agree to hear him out
- refuse

Marlow is not offended by asking about payment.

## First laboratory visit

The player physically follows Marlow downstairs. Do not teleport them into a dialogue scene.

After entering, **return control** so the player can inspect a small number of meaningful environmental details before the next conversation.

The lab should recontextualize Marlow as an experienced expeditionary alchemist/naturalist rather than Garrick's odd friend upstairs.

Environmental evidence includes:

- expedition equipment
- handwritten notes
- creature sketches
- specimens
- alchemy equipment
- plants
- habitats/enclosures
- evidence of extensive travel

Marlow remains protective of the lab. Invitation does not mean unrestricted permission to rummage through everything.

### Why Marlow lives beneath the inn

Marlow accidentally created exceptional ale while experimenting with fermentation for another purpose. He does not really drink it.

He supplies Garrick's ale; Garrick supplies his home/lab and room for his creatures.

Officially this is a trade. Their brother-like relationship remains subtext.

## Sick troll

Marlow rescued a baby troll from drowning during an expedition.

Preferred core explanation:

> **Marlow:** "He was drowning."

The troll should normally be green but has become pale, stopped eating, and lost much of its natural regeneration.

Marlow is a capable traveler who would normally gather his own materials. He will not leave the troll unattended in this condition and does not trust someone else to care properly for his creatures.

His town-watch request is delayed because human emergencies reasonably rank ahead of a sick pet by their standards.

The player learns the problem before making the genuine accept/refuse decision.

If the player refuses and does not reconsider before the established window expires, the troll dies. Marlow leaves for an extended expedition to understand/cure the disease and becomes unavailable for a significant period.

Garrick then says simply:

> **"Marlow left."**

Garrick normally calls him **Bottle-Brain**. Using Marlow signals that the situation is serious and that Garrick is deeply affected even if he will not say so directly.

## Woodland quest

Required ingredients:

- Bloodleaf
- Silvermoss
- Mooncalf Milk

The player travels physically to nearby woodland Marlow knows from his expeditions.

Mooncalf cooperation can quietly demonstrate that wildlife can sometimes be handled without violence, but **Taming does not unlock here**. The player may frighten, attack, chase, or kill the Mooncalf if they choose, and the world should respond logically.

Ordinary woodland wildlife provides likely first combat.

A normally docile **Mossback** later pursues and attacks despite the player giving it appropriate space. There is no visible corruption, villain mark, glowing evil object, or explanation.

Marlow later concludes only that the behavior is strange and worth investigating. This is a microscopic early thread of the future larger antagonist storyline, not a revealed villain plot.

## First Potion Making interaction

Back in the lab, the player physically performs:

**Prepare Bloodleaf → prepare Silvermoss → measure Mooncalf Milk → combine in correct sequence → control heat/stirring → finish experimental Health Potion.**

Marlow supervises heavily and prevents catastrophic first-attempt mistakes.

The first dose goes to the troll. Its color begins to return and, most importantly, its appetite returns.

> **Marlow:** "It worked."

The player receives the remaining Health Potion.

**Potion Making learned.**

**Health Potion recipe learned.**

Future lab use requires useful contribution such as herbs, specimens, creature feed, or laboratory supplies rather than a flat generic crafting fee. The relationship can become reciprocal over time.

## Hunting transition

After Marlow's job, Garrick now has evidence the player can travel, fight, and return alive, so he opens the Hunting board unless the player has already sequence-broken that restriction by defeating him earlier.

Room rental remains available on Day One. The cheapest room should cost roughly **5–10 coins more than starting wealth**. Marlow's payment moves a standard player closer; exploration may make it affordable already and should be allowed to matter.

Three starter contracts appear with equal weight:

- **Mud in the Moonrice**
- **Three Missing by Morning**
- **When the Wheel Stopped**

Any can be selected first. The other two remain afterward.

Completing any one unlocks **Hunting** using the established grammar:

**Inspect → Interpret → Follow → Act → Harvest**

## Garrick → Sylvie handoff

Any first Hunting contract guarantees an edible outcome suitable for Sylvie.

Established Garrick beat:

> **Garrick:** "Take it through to her. I'd call her out, but I've survived this long by remembering one thing."
>
> **Garrick:** "My inn. Her kitchen."

This is the first explicit permission to enter Sylvie's kitchen.

The Opening Questline role ends its mandatory progression responsibility at **Cooking unlock**, with the first personal supervised Cooking attempt optional.

## Open details this role may still refine

- exact canonical wording for the full default Garrick/Marlow route
- exact contextual wording for alternate first-contact branches
- exact identity/physical form of the broken research sample
- exact quest coin amounts and opening economy numbers
- exact baby troll name/visual design
- exact woodland map/layout and ordinary encounter roster
- exact timing window before the troll dies if Marlow is refused
- exact UI/state flags needed by implementation to remember reactive branches

Do not expand these into unrelated systems without Central Brain approval.
