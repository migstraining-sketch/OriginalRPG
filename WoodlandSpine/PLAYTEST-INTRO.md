# Reactive inn/lab opening — current personal playtest

This is the current narrative target: **spawn → reactive introduction → broken sample incident → invitation → lab exploration → sick troll → accept/refuse the expedition**. Stop at acceptance for this pass. The older woodland/potion content remains engineering scaffolding, not an approved continuation of this opening.

Open the existing project in Unity **6000.5.6f1**, open **Assets/Scenes/Opening.unity**, and press **Play**. You may also run **Builds/Windows/WoodlandSpine.exe** beside its data folder. Choose your name and coat. You should immediately control the character, with no instruction to seek a quest NPC and no floating inn/lab destination labels.

## First-action branches — use a fresh Play session for each

1. **Garrick:** approach the bar and speak to the innkeeper. He asks your name. Continue through the conversation.
2. **Kitchen:** try the door with a serving hatch on the back wall, to the right of the front exit. Garrick reacts to the intrusion, then asks your name. The kitchen remains private. Repeating the interaction after stepping away resumes the introduction.
3. **Upstairs:** approach the stairs on the right. The rooms-cost-money reaction leads into the introduction; the later welcome must not explain the rooms again.
4. **Basement:** try the floor hatch beside Marlow's table. Marlow calls Garrick over. You cannot enter yet. This leads into the shared introduction.
5. **Board:** walk around the left end of the bar to inspect the pinned work notices. Garrick explains why an unknown stranger cannot take dangerous contracts. His welcome must not point the board out again. Hunting stays locked.
6. **Merchandise:** inspect the rack near the bar. Looking is allowed. Ask about it to meet Garrick, or try taking a blade; that gives a warning without granting an item. No crime/economy system is implied.
7. **Marlow first:** speak to the diner beside the basement. He is eating, with food and a mug on the table. Garrick joins the awkward exchange and introduces himself.
8. **Idle/sit:** remain still, optionally using the bench's sit/stand interaction. Nothing should interrupt after five seconds. After approximately **70 seconds of continuous stillness**, Garrick acknowledges you. Movement resets the idle wait. Moving also ends the placeholder seated pose.
9. **Leave first:** walk through the front opening between the two back-wall sections. You can actually leave while unarmed. Garrick makes one passing remark without stopping you. Walk back in; he remembers it in the subsequent introduction.

Esc/right-click can close a conversation. Returning to an ordinary inn interaction should resume its current beat, preserving the original first-action context. Try interrupting the sample incident and returning: the vial must not break twice.

## Shared introduction and lab

1. Continue the shared introduction. Watch the corked transport sample fall from Marlow's lunch table and break. It is his last usable sample; there are no upstairs experiments.
2. Hear the watch-request exchange, Garrick's “another pair of legs,” Marlow's objection and Garrick's “Nope.” The player is available, not endorsed as a hero.
3. Marlow invites you downstairs **before** Garrick's warning. The hatch visibly opens. Finish the exchange, or step away after the invitation and enter; Garrick's warning can then be a passing remark.
4. Enter the lab. Movement returns immediately. There is a short **four-second free-exploration grace period** before the troll/job conversation is available; nothing automatically starts afterward.
5. Inspect a few objects: precise observations/sketches, field equipment, specimens, small habitats and fermentation notes. The lab should communicate experience, travel and expertise. The cask notes explain the ale/space arrangement without spelling out the friendship.
6. Inspect the pale baby troll. Follow the short conversation: “He was drowning”; its failing appetite/regeneration; why Marlow cannot leave it alone.
7. Ask what he needs. The job offer now names Bloodleaf, Silvermoss and Mooncalf Milk. Field advice explains identification, habitat and peaceful cooperation.
8. **Refuse once.** You should regain control, have no active gathering objective and be able to leave the lab. You may return and reconsider. The refusal is recorded for future consequences, but no death timer/departure system runs.
9. **Accept.** The only new objective should be “HELP MARLOW PREPARE A TREATMENT” with the three ingredients. Stop the narrative playtest here and report how the introduction felt.

## Controls / retained combat check

WASD/arrows: move. **E:** nearby interaction. **I:** inventory. **Esc/right-click:** close/back or cancel combat selection.

For the retained combat system: **M** selects Move, **1** Attack, **2** Defend, **3** Item, **4** Dash. Selection alone spends nothing. Click a hex/target/item to commit; use **Confirm / Enter** for Dash or Defend. **Space** ends the turn. Cancel after hovering/selecting: budgets must remain unchanged. Cancel after a confirmed action: spent costs must stay spent.

## What to report

- Any branch that fails to converge or forgets what you already did.
- A missed/obscured incident, unreadable destination, or inaccessible prompt.
- Dialogue that feels repetitive, too instructional, or unlike Garrick/Marlow.
- Whether the quiet start, incident and lab reveal feel like a coherent first several minutes. The intended 5–8 minute pace still needs a human reading/walking test.

Remaining placeholders: all art/poses/UI, silent vial break, explicit stair transfer into the lab, seated capsule pose, no shop/rest/crime systems, no saved progress between Play sessions, and only a hook for later refusal consequences. The next step is a focused blocking/dialogue revision from this playtest, before approving or expanding the woodland continuation.
