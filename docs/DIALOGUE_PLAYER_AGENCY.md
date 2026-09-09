# Dialogue & Player Agency

## Status / authority
This is the continuity home for the **Dialogue & Player Agency** sub-chat. Exact prose marked **PROPOSED** is not canon until Central Brain approves it. Existing character, chronology, and system authorities remain upstream.

## Ownership / scope
Owns playable NPC dialogue, player responses, pacing/sequencing, conversational state, branching/convergence, interruption/resumption, acceptance/refusal, optional questions, attitude/expression, NPC reactions to player behavior, and deciding when a dialogue choice is justified.

Does **not** independently change quest chronology, combat, Hunting/Cooking/Potion mechanics, Inn layout, character canon, or major story canon. Flag upstream contradictions for Central Brain.

# Dialogue doctrine

## Continue is not a roleplaying choice
When the player has nothing meaningful to decide or express, normal confirm advances the conversation. Do not manufacture `Listen / Continue / Okay / Leave` menus.

Every response must pass:
1. Does it naturally respond to what was just said?
2. Could the player actually know, feel, ask, or decide this now?
3. Does it meaningfully express attitude, request information, make a decision, or alter conversational/state direction?

## Player-intent rule
**NPCs may not advance the player's commitment state before the player does.** Distinguish being present, hearing an offer, showing interest, agreeing to hear details, and accepting a job.

NPCs may propose, infer cautiously, challenge, tempt, ask, or react. They may not decide that the player wants work, agrees to help, trusts someone, believes something, intends to enter somewhere, is heroic, or knows unseen information.

## Information order
A line may rely only on information already established. Before each beat track what Garrick knows, Marlow knows, the player knows, what the player expressed, what was offered, and what was accepted/refused.

Setup precedes payoff. **"Might've found you another pair of legs"** must land only after the player understands Marlow needs something collected, cannot leave, and the watch has not supplied help.

## Questions, attitude, acceptance
Questions are knowledge-gated. Optional questions may deepen character/world knowledge without becoming mandatory exposition gates. Sympathy, skepticism, curiosity, mercenary interest, humor, or rudeness do not automatically change commitment. Asking **"What does it pay?"** is not acceptance.

For Marlow's job, acceptance occurs only when the player explicitly agrees to gather the ingredients after learning the job. Going downstairs means **agreeing to hear/see the problem**, not agreeing to solve it.

Refusal is real. Marlow does not beg and Garrick does not shame. Reconsideration remains possible while established canon permits.

## Interruption/resumption
Do not replay completed introductions or re-explain learned information unless asked. Preserve work interest, payment questions, willingness merely to hear Marlow out, acceptance/refusal, and first-contact discoveries. Resume from the last coherent state.

# Voice guardrails

**Garrick:** Gruff, practical, observant, dryly funny, protective beneath the surface. Speaks economically. Notices behavior instead of narrating mechanics. Does not gush over the player or call them capable before evidence exists. Not a tutorial narrator. Humor needs contrast.

**Marlow:** Gentle, slightly awkward, curious, creature-focused, experienced, competent. Social hesitation is not incompetence. Does not expose a vulnerable patient casually, call the player an expedition partner before agreement, or beg when refused. Practical questions including payment do not offend him. The lab should recontextualize him as serious and experienced.

**Together:** Brotherly history is subtext through shorthand, timing, annoyance, trust, and practical care. Do not explain that they are "like brothers."

# Known Unity dialogue problems
1. After **"[NAME]. Right."**, current `InnConversation` offers **"Thanks."** It has no conversational cause.
2. **"Might've found you another pair of legs"** arrives before the player understands what needs collecting or why Marlow cannot go.
3. **"If you're actually considering this..."** invents player interest.
4. Several mandatory one-option responses merely advance text, including current uses of **"Where?"**, **"Mine?"**, **"That's a troll."**, **"You jumped in after a troll?"**, **"He doesn't look the right colour."**, **"And you stay here with him."**, and the final briefing recap.
5. First-lab dialogue is a nearly linear question ladder rather than genuine information/attitude choices.
6. `MarlowOpening.Say` falls back to **Step away** when no choice exists. Presentation should eventually support ordinary Continue independently from roleplaying choices.
7. Current `InnConversation` refusal uses the same departure staging as acceptance, risking Marlow leading the player downstairs after refusal.

These observations do **not** authorize Unity changes yet.

# Conversation-state rules
Conceptual state only; exact code representation is downstream.

Track player knowledge where relevant: Garrick identity; Marlow identity; basement ownership; urgent patient; need for field ingredients; why Marlow cannot leave; watch backlog; patient being a troll; illness signs; ingredient list; payment if stated.

Preserve first-contact discoveries separately: rooms, board/work, merchandise, basement attempt, immediate leave/return, theft/aggression.

Commitment must distinguish:
- `workInterest`: unknown / expressed
- `marlowInterest`: unknown / willingToHear / refusedToHear
- `marlowJob`: notOffered / offered / accepted / refused
- `askedPayment`

Never collapse `willingToHear` and `accepted`. Basement is private before invitation. Invitation grants access to hear Marlow out; job acceptance is not required merely to enter.

# Existing locked opening facts preserved
Immediate control and reactive first contacts; Garrick learns player name; last usable sample breaks; watch request is backlogged; **"Might've found you another pair of legs" / "You don't know them" / "Nope"** remain established; Marlow invites before Garrick warning; lab returns control; troll was rescued from drowning and **"He was drowning"** remains preferred; troll should be green but is pale, not eating, and losing regeneration; Marlow will not leave it; Bloodleaf, Silvermoss, Mooncalf Milk are required; genuine refusal and existing downstream consequences remain.

# PROPOSED opening playable dialogue/state specification
**FOR CENTRAL BRAIN REVIEW. Exact prose below is not canon yet.** Normal route shown. Contextual first-contact entrances still converge while remembering discoveries.

## Beat 1 — First contact
**Garrick:** "New face."

Continue.

**Garrick:** "Garrick. I own the place. You got a name?"

**Player:** "[NAME]."

**Garrick:** "[NAME]. Right."

**No response menu.** If prior behavior created business, Garrick responds to it. Otherwise allow a natural pause before the incident. No work interest is inferred.

## Beat 2 — Establish Marlow's problem before Garrick's solution
**CRASH.**

**Garrick:** "Bottle-Brain."

**Marlow:** "...That's unfortunate."

**Garrick:** "How unfortunate?"

**Marlow:** "That was the last usable sample I had."

**Garrick:** "For the little one?"

**Marlow:** "Yes."

**Garrick:** "Can you make another?"

**Marlow:** "If I had fresh material."

**Garrick:** "Then go get it."

**Marlow:** "I can't leave him alone like this."

Continue.

**Garrick:** "Thought you already put in a request."

**Marlow:** "I did."

**Garrick:** "Watch still sitting on it?"

**Marlow:** "They have more urgent matters."

Now the player understands: urgent patient, fresh material required, Marlow cannot leave, watch has not supplied help.

## Beat 3 — Garrick notices the stranger
Garrick looks at player, then Marlow.

**Garrick:** "Might've found you another pair of legs."

**Marlow:** "You don't know them."

**Garrick:** "Nope."

If work interest was expressed: **"But they're looking for work."** Otherwise: **"But they're standing right here."**

**Marlow:** "That's a very low bar for an expedition partner."

**Garrick:** "Good thing you're not taking them on an expedition."

**Garrick:** "You need something fetched. Ask 'em."

Marlow finally addresses the player:

**Marlow:** "Would you be willing to hear me out?"

Meaningful responses:
- **"What needs fetching?"** → practical interest; not acceptance
- **"Does it pay?"** → transactional interest; not acceptance
- **"What's wrong with your patient?"** → concern/curiosity; not acceptance
- **"Not interested."** → genuine refusal to hear offer

For practical interest: **Marlow:** "A few things from the woodland. I can explain what, but you should see why I can't go myself first."

For patient question: **Marlow:** "That's easier to show than explain. He's downstairs."

Payment uses the approved amount once finalized. Asking never accepts.

Then on non-refusal routes:

**Marlow:** "If you're willing, come downstairs. You can see the situation before you decide anything."

Responses:
- **"All right. Show me."** → willingToHear; invitation granted
- **"No."** → refusedToHear; remains upstairs

## Beat 4 — Invitation and warning
**Marlow:** "My laboratory is downstairs."

If basement was tried earlier, optional **"The locked room?"** → **Marlow:** "Yes. Different circumstances now."** Otherwise no menu.

Marlow gathers surviving material/notes.

**Garrick:** "Bottle-Brain."

**Garrick:** "They try anything funny down there, holler."

Garrick looks at player.

**Garrick:** "I'll hear you."

This preserves protection without requiring the forced **"Mine?"** joke. Central Brain may retain the stronger boot threat if preferred.

Marlow unlocks the basement and physically leads. Player follows under normal control.

## Beat 5 — Laboratory exploration
Player has agreed only to hear Marlow out. No woodland job has been offered.

Return control immediately. Marlow may say **"Mind the cases."** without a response menu. Expedition gear, notes/specimens, habitats, and fermentation setup establish competence environmentally. Ale-arrangement dialogue remains optional flavor.

After a short exploration window or if player approaches the sick creature, the troll makes a weak sound. Marlow immediately crosses to it. No quest marker is needed.

## Beat 6 — Sick troll reveal
Marlow kneels beside it.

**Marlow:** "Easy. I'm here."

Then to player:

**Marlow:** "This is why I can't leave."

Continue.

**Marlow:** "He should be green. He stopped eating first. Then the colour began to go. His regeneration's slowing too."

He indicates a wound that should have healed.

Now genuine optional questions exist:
- **"Why do you have a troll?"**
- **"Do you know what's wrong with him?"**
- **"Is he dangerous?"**
- **"What were you trying to make?"**

Player need not exhaust them.

**Why troll:** Marlow explains he found him trapped during floods. Optional follow-up **"You rescued a troll?"** → **Marlow:** "He was drowning."**

**Cause:** **Marlow:** "Not exactly. I've been recording the changes. I think I can treat the symptoms and restore his regenerative response."**

**Danger:** exact answer is **UNRESOLVED** until troll temperament/risk is confirmed by Central Brain.

**Treatment:** **Marlow:** "The sample upstairs was part of it. I need fresh ingredients to finish the preparation."**

## Beat 7 — Job setup
If not already established:

**Marlow:** "Normally I'd collect everything myself. I know the woodland. But his condition can change quickly, and I won't leave him unattended."

Do not repeat the watch explanation if heard upstairs.

**Marlow:** "What I need is nearby: Bloodleaf, Silvermoss, and Mooncalf Milk."

**Marlow:** "If you bring them back, I can stay here and finish the treatment."

Only now is the woodland job actually offered.

## Beat 8 — Explicit decision
**Marlow:** "Will you help?"

Responses:
- **"I'll get what you need."** → ACCEPT
- **"What does it pay?"** → information only, then return to decision; omit if already answered
- **"Tell me what I'd be walking into."** → risk/context, then return to decision
- **"I can't do this."** → REFUSE

### Accept
Only now set actual quest/job accepted state.

**Marlow:** "Thank you. Then I'll show you exactly what to look for."

Give existing practical field briefing: Bloodleaf red veins/useful tips/leave stem; Silvermoss damp shaded stone/leave most; peaceful Mooncalf approach; Mossbacks normally docile if given space; Garrick weapon reminder if still appropriate.

After briefing, normal Continue returns control. Do **not** require a recap choice such as **"Bloodleaf, Silvermoss, milk. I'll be back."**

### Refuse
**Marlow:** "I understand. Thank you for hearing me out."

Return control. No lead, guilt, or quest acceptance. Existing consequence clock remains upstream canon.

## Beat 9 — Reconsideration
If player later speaks to Marlow in time:

**Marlow:** "Have you reconsidered?"

Responses:
- **"Tell me what you need again."** → reminder, not acceptance
- **"I'll help."** → acceptance + briefing
- **"No."** → close naturally

Do not replay the reveal or erase prior refusal.

# Branch / convergence rules
- Kitchen, rooms, basement, board, merchandise, Marlow-first, idle, leave/return, and non-terminal theft entrances may converge into the shared incident while preserving learned facts.
- If player expressed work interest, Garrick may truthfully reference it. If not, he may only note the player's availability/presence.
- Asking pay or details sets information/interest state, never quest acceptance.
- Agreeing downstairs grants invitation/access only.
- Refusing upstairs must not trigger Marlow's downstairs lead.
- Refusing the actual job leaves the lab visit as something that happened and can be remembered.
- If conversation is interrupted, resume at the coherent current beat without replaying name/sample/reveal sequences.

# Player-response rules for implementation
1. Prefer 2–4 meaningful responses when a decision/expression point exists.
2. Do not display a one-option menu merely to continue.
3. Do not offer mutually identical responses wearing different prose.
4. Questions may branch for information and reconverge without changing commitment.
5. Choices should describe what the player means, not hidden mechanics.
6. NPC follow-ups must reflect actual prior player behavior/state.
7. Do not force the player character to make observations the environment can communicate on its own.

# Approved dialogue
No new exact prose from this document is approved yet. Existing locked lines remain authoritative where identified above. Move Central-Brain-approved exact dialogue into this section after review.

# Unresolved dialogue questions for Central Brain
1. Approve/revise the proposed pre-legs setup: **little one → fresh material → Marlow cannot leave → watch backlog → pair of legs**.
2. Should Garrick's stronger **big boot coming for their face** warning remain, be shortened, or use the proposed quieter **"I'll hear you"** version?
3. Confirm troll temperament/risk so Marlow can answer **"Is he dangerous?"** accurately.
4. Exact Marlow quest payment remains unresolved upstream; dialogue must not invent the number.
5. Decide whether **"That's a very low bar for an expedition partner"** fits Marlow's voice or should be softened further.
6. After Central Brain approves the conversation, Unity needs an exact implementation spec that separates Continue input from response-choice UI and fixes refusal/downstairs staging. Do not implement before that approval.
