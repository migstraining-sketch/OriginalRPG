# Central Brain Handoff

## Identity

This is an **original fantasy RPG**. It is inspired by the charm and tactile simplicity of early AdventureQuest-style games, the permanent progression/self-direction of Old School RuneScape, and lessons from World of Warcraft/MMO design, but it is **not** a recreation of AdventureQuest, BattleOn, or any Artix Entertainment game. Artix-owned characters, locations, chronology, dialogue, and lore are inspiration/research only and are not canon.

Development stack is now **Unity + Blender**. Unity is being used for gameplay/prototyping. Blender will be used for original production assets once layouts, scale, interactions, and gameplay are stable enough that we are not polishing geometry we may immediately throw away.

## Current opening spine

**Character Creation → Garrick's Inn → Garrick/Marlow introduction → Marlow's sick troll problem → woodland expedition → Potion Making → Garrick opens contract board → player chooses any one of three Hunting contracts → Hunting unlocks → return with edible material → Garrick sends player to Sylvie → Sylvie demonstrates Cooking → player accepts pantry arrangement → Cooking unlocks → optional first supervised Cooking attempt.**

Target standard-path timing: about **27–30 minutes**, with healthy variance of roughly **23–35 minutes**.

## Opening philosophy

- Player gets control immediately in Garrick's Inn.
- Freedom is real, but the world reacts logically.
- Contextual reactions replace arbitrary invisible tutorial walls.
- Systems are taught through situations and characters, not giant tutorial overlays.
- Only one new mental model should be actively taught at a time.
- The player may sequence-break, refuse content, behave violently, or leave. The game should remember and react where practical.
- Starting weak matters because later progression needs context.
- No giant world-map dump at minute one. Geography should become meaningful gradually.
- The inn should become the player's first home base through relationships and use, not because the UI declares it a hub.

## IMPORTANT CURRENT DEVELOPMENT FRONTIER — READ BEFORE DOING NEW DESIGN

A playable Unity prototype now exists and runs. It proves that the project can support the basic technical skeleton, including exploration/interactions, inventory/equipment, connected encounter spaces, and the tactical hex-combat foundation.

**The currently implemented opening is NOT canonical yet.**

A recent implementation pass attempted to move the prototype toward the designed Garrick/Marlow opening. The structure is getting closer, but the latest playtest found a major problem: **the dialogue itself does not feel like our game or our characters yet.**

Specific problems observed:

- Garrick/Marlow dialogue is not the intended authored dialogue.
- Player dialogue choices feel artificial and wrong.
- The implementation frequently offers choices equivalent to **Listen / Leave** rather than meaningful player responses.
- The Marlow/player conversation in the laboratory is substantially wrong in dialogue, pacing, options, and overall feel.
- Garrick is **not** incorrectly present in the laboratory. The lab scene is player + Marlow as intended. The problem is the conversation itself.
- The technical/structural implementation should therefore not be mistaken for approved narrative content.

### Immediate rule

**Do NOT send Work another broad implementation pass yet.**

**Do NOT proceed deeper into implementing the woodland quest merely because the prototype can support it.**

The next task is to author the canonical playable dialogue for the opening ourselves, then give implementation a much more exact script/state specification instead of asking it to improvise from character summaries.

### Immediate next design sequence

Write/refine the actual playable dialogue for:

**player enters Garrick's Inn → reactive first contact with Garrick → Marlow/Bottle-Brain incident → Garrick volunteers the newcomer → Marlow invites player downstairs → laboratory exploration → sick troll reveal → Marlow conversation → woodland job offer**

Begin with the normal/default route where the player voluntarily approaches Garrick, then build the contextual alternate entrances around that canonical conversation.

### Dialogue-system principle to use going forward

**Continue is not a roleplaying choice.**

If the player has nothing meaningful to choose, normal confirm/continue input should simply advance the conversation. Do not manufacture a choice menu containing things such as `Listen` merely to make the player click a response.

Dialogue choices should appear when the player has an actual:

- response
- question
- decision
- attitude
- roleplaying expression
- meaningful refusal/acceptance

Not every spoken line needs a branching choice. Choices should earn their place.

Examples around the sick troll might eventually include genuine questions/reactions such as what happened to it, whether it is dangerous, why Marlow keeps it beneath an inn, or whether the player is willing to help. Exact canonical wording is still to be authored and should not be invented by implementation before Central Brain approves it.

## Reactive first-contact structure

The player begins with autonomy. Different first behaviors should trigger contextual reactions and then converge naturally into Garrick's introduction rather than becoming endlessly repeatable locked-door barks.

Supported conceptual entrances include:

- Approach Garrick normally.
- Try the kitchen: Garrick objects because an unknown stranger is entering a private working kitchen, then begins the introduction.
- Try upstairs: Garrick explains rooms are not free; this can expose the existence of paid rooms/work and become the introduction.
- Try Marlow's basement before invitation: Marlow notices from his table and calls Garrick's attention to the stranger trying his laboratory door.
- Inspect the contract board: player can understand that work exists, but Garrick will not let an unknown stranger take dangerous local jobs merely to discover what eats them.
- Inspect Garrick's merchandise: looking is allowed; deliberate theft can trigger warning/escalation hooks.
- Talk to Marlow first: allowed; Garrick notices the stranger already talking to Bottle-Brain and joins naturally.
- Sit/idle: allowed; Garrick can eventually acknowledge the stranger without an immediate forced tutorial trigger.
- Leave: allowed. No invisible wall. Garrick may make one contextual remark, but the player can genuinely exit and the opening waits for their return.

Immediate dialogue should remember what the player already discovered. If the stairs caused first contact, Garrick should not immediately explain rooms again as though nothing happened. If the board caused it, he should not point it out as a brand-new discovery.

## Locked main characters

### Garrick

Rugged, large, strong innkeeper/bartender. Gruff, practical, funny without trying too hard, observant, protective beneath the rough exterior. Inspiration reference: Bobby Singer archetype only. Garrick sells basic weapons/armor, rents rooms, and controls the local contract board. He calls Marlow an affectionate insult such as **Bottle-Brain**.

Garrick is the opening's first social anchor, **not a tutorial narrator**.

### Marlow

Experienced expeditionary alchemist/naturalist/healer. Gentle, awkward, curious, creature-focused, competent in his field. Inspiration reference: Newt Scamander-like energy only. He is not a bumbling scientist.

At the opening he is upstairs at a table because he is eating/taking a break, not conducting experiments in the common room. He lives/works in the basement lab. He accidentally created excellent ale while researching fermentation; he supplies Garrick, who supplies his home/lab. Their relationship is effectively brotherly but not stated outright.

The first lab visit should recontextualize him. Upstairs he can initially read as Garrick's odd Bottle-Brain friend. Downstairs, the expedition gear, notes, specimens, alchemy equipment, creature care, and accumulated field evidence should make it clear that he is experienced and serious about his work.

### Sylvie

Exceptional chef, intensely confident, perfectionist, territorial about her kitchen, hates wasting edible ingredients, feeds hungry people whether they can pay or not. Inspiration reference: Sanji-like culinary intensity only. She recognizes edible species and practical ingredient condition with extraordinary skill, but is not clairvoyant.

## Garrick → Marlow handoff

During/around Garrick's opening interaction, Marlow accidentally breaks/drops the **last usable sample** connected to his urgent research. The incident matters rather than existing only as slapstick.

Established rhythm/concepts include:

> **Garrick:** "Thought you already put in a request."
>
> **Marlow:** "I did."
>
> **Garrick:** "Watch still sitting on it?"
>
> **Marlow:** "They have more urgent matters."

Garrick notices the newcomer and essentially volunteers the only available pair of legs:

> **Garrick:** "Might've found you another pair of legs."

Marlow correctly objects because they are strangers:

> **Marlow:** "You don't know them."
>
> **Garrick:** "Nope."

The joke is that Garrick is giving the player almost no endorsement. They are simply here, mobile, potentially looking for work, and currently Marlow's most realistic option.

Marlow should invite the player downstairs **before** Garrick gives the warning about hollering if the player tries anything funny. The invitation is what changes the basement from private space to accessible space.

## First laboratory visit

Once downstairs, **return control to the player before forcing another long dialogue**. Let them look around and inspect a handful of meaningful objects. Do not turn the lab into twenty glowing inspect nodes.

Environmental storytelling should establish:

- expedition equipment
- handwritten notes
- creature sketches
- specimens
- alchemy equipment
- plants
- animal habitats/enclosures
- evidence of extensive travel and actual expertise

The ale arrangement can be established naturally: Marlow accidentally created spectacular ale during unrelated fermentation research. He barely/does not drink it. Garrick gets the ale; Marlow gets the basement/home/workspace and room for his creatures. Officially it is a trade. The emotional brotherhood remains subtext.

## Marlow opening quest

Marlow rescued a baby troll from drowning. It is normally green but has become pale, stopped eating, and lost much of its regeneration. Marlow's town-watch request is backlogged because human emergencies take priority. He refuses to leave the sick creature unattended.

The simple core of why he rescued it remains:

> **Marlow:** "He was drowning."

Required woodland ingredients:

- Bloodleaf
- Silvermoss
- Mooncalf Milk

Mooncalf cooperation quietly seeds future Taming without unlocking it. The player may still frighten, attack, or kill it if they insist.

A normally docile **Mossback** persistently attacks the player despite appropriate behavior. No corruption effect or villain clue appears. Marlow later only says this is strange and worth investigating. This is the first microscopic thread toward a future larger antagonist storyline.

Back in the lab, the player physically prepares the treatment:

**prepare Bloodleaf → prepare Silvermoss → measure Mooncalf Milk → combine in sequence → control heat/stirring → finish Health Potion.**

First dose goes to the troll. Its color begins to return and, most importantly, it becomes hungry again.

**Potion Making unlocked. Health Potion recipe learned.**

If the player refuses Marlow and does not reconsider before the relevant time window, the troll dies. Marlow leaves on an extended expedition and his content becomes unavailable for a significant period. Garrick says simply:

> **"Marlow left."**

The exact timer/implementation remains downstream unless already deliberately implemented.

## Hunting

Three starter contracts remain available. Completing any one formally unlocks **Hunting**. Hunting's MVP identity is:

**Inspect → Interpret → Follow → Act → Harvest**

Hunting is about understanding and resolving wildlife problems, not merely killing animals.

Starter contracts:

- **Mud in the Moonrice**: Reedback digging for mudgrubs in Toma Reed's paddies. Kill or redirect. Ingredient: Fresh Reedback Haunch or Preserved Reedback Cut.
- **Three Missing by Morning**: Nightquill enters Mara Venn's Duskhen coop from an elevated glide route. Hunt or deter/secure route. Ingredient: Fresh Duskhen Eggs.
- **When the Wheel Stopped**: Brookmaws established a nursery around Oren Vale's mill channel. Kill or relocate by restoring a safer side-channel. Ingredient: Fresh Brookmaw Tail or naturally shed Brookmaw Tail found in nursery debris.

## Cooking

After the first contract, Garrick sends the player into Sylvie's kitchen with the established beat:

> **Garrick:** "Take it through to her. I'd call her out, but I've survived this long by remembering one thing."
>
> **Garrick:** "My inn. Her kitchen."

First entrance:

> **Sylvie:** "Out."
>
> **Player:** "Garrick sent me."

She notices the ingredient and performs a master demonstration through the same Cooking system the player later uses.

Cooking grammar:

**Inspect → Prepare → Set Up → Cook → Read → Remove → Finish**

Five intro states teach different principles:

- Fresh Reedback: structure/grain, searing, resting
- Preserved Reedback: condition, salt/moisture, rendering/crisping
- Duskhen Eggs: gentle heat, movement, residual heat
- Fresh Brookmaw Tail: anatomy, scoring, rendering, heat changes
- Shed Brookmaw Tail: source/handling, cleaning/drying, then render/crisp

Preferred tasting beat:

> **Player:** "...What did you do to this?"
>
> **Sylvie:** "Cooked it."
>
> **Player:** "No. Seriously."
>
> **Sylvie:** "So am I."

Player asks to learn. Sylvie requires enough raw ingredient to practice plus a pantry contribution. On acceptance, **Cooking unlocks**. First personal Cooking attempt is optional immediately afterward.

All intro dishes apply the same placeholder **Well Fed** state. Food does not directly heal. Exact Well Fed/Rest interaction remains unresolved.

## Tactical combat — locked MVP direction

Combat is turn-based on **small hex grids projected over the real exploration environment** rather than teleporting to generic arenas.

Locked/current MVP principles:

- Base player Movement: **3 hexes**.
- One **Primary Action** per player turn.
- Movement may be split before and after the Primary Action.
- Opening actions: Attack, Defend, Item, Dash, plus contextual environmental interactions.
- No universal opportunity attacks.
- No universal facing/flanking system for MVP.
- Alternating Player Phase → Enemy Phase.
- Important enemy actions use readable intent/telegraphs.
- Terrain begins with Open, Difficult, and Blocking; difficult terrain costs 2 Movement.
- Starting exploration position influences starting tactical position.
- Weapon identity should begin with geometry/range rather than only damage.
- Normal valid attacks do **not** use a generic random miss chance.
- Fixed damage for the prototype.
- Damage formula: **max(1, Attack Damage - Armor)**.
- Defend halves post-Armor damage, rounded upward.
- Starting player prototype: **30 HP / 1 Armor**.
- Starter Sword prototype: **6 Damage**.
- Mossback prototype identity: telegraphed straight-line Charge that can collide with obstacles and create an opening.

A concrete UX issue found during playtest: selected combat actions need a **cancel/back** before commitment. Right-click and/or Escape should cancel a selected but uncommitted action without spending the turn. This is not an undo system; committed/resolved actions remain committed.

## Equipment/loadout — locked MVP direction

- Functional opening slots: **Weapon + Body Armor** only.
- Player has 0 inherent Armor; worn body equipment provides Armor.
- Starting padded/travel garment provides **Armor 1**.
- Garrick can offer a meaningful **Armor 2** early upgrade.
- Prototype weapon families: Sword, Spear, Bow.
- Sword: 6 damage, adjacent.
- Spear: 6 damage, range 1–2 in a straight hex line.
- Bow: 5 damage, range 2–4, line of sight, cannot basic-attack adjacent targets.
- One equipped weapon at a time.
- Weapon switching outside combat is free; in combat it consumes the Primary Action.
- One general inventory for equipment/materials/ingredients/potions/adventure items.
- No restrictive carrying capacity for the MVP opening.
- No item level/gear score or rarity-color treadmill in MVP.
- Visible body armor and weapon should eventually reflect what is equipped.
- Early gear should include sidegrades and compressed stat growth so +1 Damage/+1 Armor remains meaningful.

The exact narrative source of the player's first weapon remains unresolved and should be chosen alongside the canonical opening dialogue/economy rather than silently decided by the inventory system.

## Rooms

Room renting is Day One content. Cheapest room should be roughly 5–10 coins beyond starting wealth. Marlow's reward moves a normal player closer; exploration can potentially make the room affordable earlier. Exact Rest Quality mechanics remain unresolved.

## Current production rule

Do not start serious Blender production merely because Unity is running. Use placeholder/blockout geometry until the inn layout, interaction positions, camera behavior, encounter spaces, and opening flow are stable. Then begin replacing approved blockouts with original Blender assets in deliberate passes.

## Next recommended task

**Do not plan the first 1–2 hours yet.** That was the previous frontier and is now superseded by playtest feedback.

The immediate next task is:

### Author the canonical opening dialogue and player-response structure

Start with the default Garrick approach and carry it through the Bottle-Brain incident and Marlow invitation. Then design the contextual first-contact variants. After that, author the first Marlow laboratory conversation through the sick troll reveal and woodland job offer.
