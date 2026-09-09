# Dialogue & Player Agency

## Status / authority

This document is the continuity home for the **Dialogue & Player Agency** sub-chat.

It governs dialogue quality, conversational logic, player-expression opportunities, and conversation state. Exact prose marked **PROPOSED** is not canon until Central Brain approves it. Locked character/story/system facts remain authoritative in their existing documents.

## Ownership / scope

This sub-chat owns:
- playable NPC dialogue
- player response options
- conversation pacing and sequencing
- conversational state
- branching, convergence, interruption, and resumption
- meaningful acceptance/refusal
- questions and optional information
- player attitude/expression
- NPC reactions to player behavior
- preventing NPCs from assuming player intentions
- character-voice consistency inside playable conversations
- deciding whether a moment needs a dialogue choice or ordinary Continue input

It does **not** independently change:
- quest chronology
- combat rules
- Hunting, Cooking, or Potion Making mechanics
- Inn layout
- character canon
- major story canon

If dialogue exposes a contradiction in one of those authorities, flag it for Central Brain rather than repairing upstream canon silently.

---

# Core dialogue doctrine

## Continue is not a roleplaying choice

When the player has nothing meaningful to decide or express, normal confirm input advances the conversation.

Do not manufacture choices such as:
- Listen
- Continue
- Okay
- Leave
- Look

unless that action is a genuine decision in the specific context.

A player response earns a choice slot only if it passes all three tests:

1. **Natural response:** does this naturally respond to what was just said?
2. **Knowledge/state validity:** could the player actually know, feel, ask, or decide this now?
3. **Expressive/state value:** does it meaningfully express attitude, request information, make a decision, or alter conversational/state direction enough to justify a choice?

If not, use Continue or environmental control instead.

## Player-intent rule

**NPCs may not advance the player's commitment state before the player does.**

NPCs may propose, infer cautiously, challenge, tempt, ask, or react. They may not decide that the player:
- wants a job
- agrees to help
- trusts someone
- is interested
- believes a claim
- intends to enter somewhere
- is heroic/altruistic
- knows information not yet learned

The conversation must distinguish **being present**, **hearing an offer**, **showing interest**, **agreeing to hear details**, and **accepting a job**.

## Information-order rule

A line may rely only on information already established for its listener.

Before each beat, ask:
- What does Garrick know?
- What does Marlow know?
- What does the player know?
- What has the player actually expressed?
- What has been offered?
- What has been accepted/refused?
- What questions are now logically available?

Setup must precede payoff. In particular, Garrick's **"Might've found you another pair of legs"** only makes sense after the player understands that Marlow needs something collected, cannot currently leave, and the watch has not supplied help.

## Questions are knowledge-gated

A question should appear because the preceding conversation created it. Do not offer a question about a fact the player has not encountered or a conclusion they could not reasonably draw.

Optional questions can deepen character/world knowledge without becoming mandatory exposition gates.

## Attitude is not commitment

A player may be sympathetic, skeptical, curious, mercenary, amused, or rude without that automatically accepting/refusing a quest.

Likewise, asking **"What does it pay?"** is not acceptance. Asking **"What do you need?"** is interest, not commitment.

## Acceptance must be explicit when stakes matter

For Marlow's opening job, acceptance occurs only when the player explicitly agrees to gather the ingredients after learning what the job is.

Going downstairs means **agreeing to hear/see the problem**, not agreeing to solve it.

## Refusal must be real

If the player refuses Marlow, Marlow does not beg and Garrick does not shame them. The player can reconsider while the established time window remains open. If they do not, the existing troll-death/Marlow-departure consequence remains authoritative.

## Interruption and resumption

Conversation state must survive ordinary interruption.

On resume:
- do not replay already completed introductions
- do not re-explain information the player already learned unless they ask for a reminder
- preserve whether the player showed interest, asked about pay, agreed only to hear Marlow out, accepted, or refused
- preserve contextual first-contact discoveries such as rooms, board, merchandise, basement ownership, or Marlow's identity

NPCs should resume from the last coherent conversational state, not from a generic root node.

---

# Character voice guardrails

## Garrick

Gruff, practical, observant, dryly funny, casually intimidating, protective beneath the surface.

He:
- speaks economically
- notices behavior rather than narrating mechanics
- can needle Marlow because their familiarity is established through behavior
- does not gush over the player
- does not call the player capable before evidence exists
- can offer an opportunity without endorsing the stranger
- should not become a tutorial narrator listing inn features

Dry humor works best when it emerges from the situation. Do not turn every Garrick line into a punchline.

## Marlow

Gentle, slightly awkward, curious, creature-focused, experienced, competent.

He:
- is careful about creatures and evidence
- can be socially hesitant without being incompetent
- does not casually expose a vulnerable patient to a stranger without a reason
- does not treat the player as an expedition partner before they agree to anything
- does not beg when refused
- can answer practical questions, including payment, without taking offense
- becomes visibly more authoritative in his laboratory through behavior, knowledge, and environment

His broken sample is an unfortunate accident under pressure, not proof that he is a bumbling scientist.

## Garrick + Marlow

Their old, brotherly relationship should be inferred through shorthand, timing, annoyance, trust, and practical care.

Avoid exposition such as "we're like brothers." Garrick knowing what Marlow's understatement means, Marlow tolerating Bottle-Brain, and Garrick quietly pushing him toward a solution communicate more.

---

# Known Unity dialogue problems

Current prototype review confirms several dialogue-state defects.

1. After **"[NAME]. Right."**, the current `InnConversation` offers **"Thanks."** This fails the natural-response test. Garrick has done nothing that calls for gratitude.
2. The prototype reaches **"Might've found you another pair of legs"** immediately after the town-watch exchange, before the player knows what Marlow needs collected or why Marlow cannot leave. The intended joke therefore arrives before its setup.
3. Marlow currently says **"If you're actually considering this..."** before the player has necessarily expressed interest. This advances player intent without permission.
4. The prototype contains several interaction choices whose only function is advancing text, including lines such as **"Where?"**, **"Mine?"**, **"That's a troll."**, **"You jumped in after a troll?"**, **"He doesn't look the right colour."**, **"And you stay here with him."**, and the final briefing recap. Some may be valid as optional expressions, but they should not be mandatory one-option Continue buttons.
5. The first-lab sequence currently forces a nearly linear question ladder. The lab should first return control, then use a smaller number of genuine questions/reactions around the troll rather than requiring the player to speak every transition.
6. `MarlowOpening.Say` falls back to a **Step away** choice when no authored choice exists. That UI convention conflicts with the doctrine when stepping away is not a meaningful decision. Dialogue presentation should eventually support ordinary Continue separately from roleplaying choices.
7. The current refusal path in `InnConversation` calls the same departure staging used by acceptance, which risks Marlow physically leading the player downstairs after refusal. Implementation must eventually separate **refused** from **agreed to hear Marlow out**.

These are implementation observations, not authorization to change Unity yet.

---

# Opening conversation state model

Recommended conceptual state variables. Exact code representation is downstream.

## Knowledge flags

Player-facing knowledge:
- `knowsGarrickName`
- `knowsMarlowName`
- `knowsBasementIsMarlowLab`
- `knowsMarlowHasUrgentPatient`
- `knowsMarlowNeedsFieldIngredients`
- `knowsMarlowCannotLeavePatient`
- `knowsWatchBacklogged`
- `knowsPatientIsTroll`
- `knowsTrollIllnessSigns`
- `knowsIngredientList`
- `knowsPayment` if payment has been stated

Existing first-contact discoveries should remain separate:
- rooms known
- board/work known
- merchandise known
- basement tried
- returned after immediately leaving
- theft/aggression history

## Player-expression / commitment flags

- `workInterest`: unknown / expressed / declined generally
- `marlowInterest`: unknown / willingToHear / refusedToHear
- `marlowJob`: notOffered / offered / accepted / refused
- `askedPayment`
- optional attitude memories only where an NPC response later benefits from remembering them

Do not collapse `willingToHear` and `accepted` into one boolean.

## Access state

- basement private before Marlow's invitation
- invitation grants social access to follow Marlow down
- accepting the woodland job is **not** required merely to enter the lab on this first invitation

---

# Existing locked opening facts preserved

The following are existing continuity and are **not being redesigned here**:

- player gets control immediately in Garrick's Inn
- reactive first-contact entrances converge naturally
- Garrick learns the player's name
- Marlow's last usable research sample breaks during/around the opening interaction
- Marlow has a town-watch request that is backlogged behind urgent human problems
- Garrick eventually says **"Might've found you another pair of legs."**
- Marlow objects: **"You don't know them."** Garrick answers **"Nope."**
- Marlow invites the player downstairs before Garrick's boot warning
- control returns in the laboratory for environmental exploration
- Marlow rescued a baby troll from drowning
- **"He was drowning."** remains the preferred core explanation
- troll should be green, has gone pale, stopped eating, and lost regeneration
- Marlow refuses to leave it unattended while it deteriorates
- required woodland ingredients are Bloodleaf, Silvermoss, and Mooncalf Milk
- the player may genuinely refuse
- refusal can eventually lead to troll death and Marlow's temporary departure under existing canon

---

# PROPOSED canonical playable dialogue/state specification

**Status: PROPOSED FOR CENTRAL BRAIN REVIEW. Exact prose below is not canon yet.**

This specification covers the normal route. Existing contextual first-contact entrances remain valid and should converge while remembering what the player already discovered.

## Beat 1 — Normal first contact

### State before

Garrick knows: an unknown traveler/new face has entered.

Marlow knows: little or nothing about the player; he is on a break while worried about his patient.

Player knows: only what they can observe in the inn.

Player commitment: none.

### Proposed dialogue

**Garrick:** "New face."

Continue.

**Garrick:** "Garrick. I own the place. You got a name?"

This is a genuine prompt requiring the player's configured name, not a roleplaying menu.

**Player:** "[NAME]."

**Garrick:** "[NAME]. Right."

### Player response

**No response menu here.** Garrick's acknowledgment does not require thanks or another manufactured reply.

If there is relevant first-contact business, Garrick responds to it. Otherwise the scene gets a small natural pause before the Marlow incident rather than forcing Garrick to list services.

### State after

Player knows Garrick's name/ownership. Garrick knows player's name. No work interest has been inferred.

---

## Beat 2 — Marlow's accident establishes urgency, not yet the solution

**CRASH.**

Garrick looks over.

**Garrick:** "Bottle-Brain."

**Marlow:** "...That's unfortunate."

**Garrick:** "How unfortunate?"

**Marlow:** "That was the last usable sample I had."

**Garrick:** "For the little one?"

**Marlow:** "Yes."

This small addition is important. It tells the player the broken sample concerns someone/something Garrick already knows about without prematurely revealing the troll.

**Garrick:** "Can you make another?"

**Marlow:** "If I had fresh material."

**Garrick:** "Then go get it."

**Marlow:** "I can't leave him alone like this."

Now the player understands the essential constraint: Marlow needs material from elsewhere and cannot leave his patient.

Continue.

**Garrick:** "Thought you already put in a request."

**Marlow:** "I did."

**Garrick:** "Watch still sitting on it?"

**Marlow:** "They have more urgent matters."

### State after

Player now knows:
- Marlow has an urgent patient/problem
- the broken sample was part of that problem
- fresh material must be collected
- Marlow cannot currently leave
- the watch has not supplied help

The logical runway for **another pair of legs** now exists.

---

## Beat 3 — Garrick notices the available stranger

Garrick looks at the player, then Marlow.

**Garrick:** "Might've found you another pair of legs."

**Marlow:** "You don't know them."

**Garrick:** "Nope."

If the player previously expressed interest in work:

**Garrick:** "But they're looking for work."

If they did not:

**Garrick:** "But they are standing right here."

**Marlow:** "That is a very low bar for an expedition partner."

**Garrick:** "Good thing you're not taking them on an expedition."

Garrick looks to Marlow rather than deciding for the player.

**Garrick:** "You need something fetched. Ask 'em."

This replaces the current sequence where Garrick and Marlow discuss the player at length before anyone actually asks what the player thinks.

### Meaningful player-expression point

Marlow addresses the player.

**Marlow:** "Would you be willing to hear me out?"

Proposed responses:
- **"What needs fetching?"** → expresses practical interest; sets `marlowInterest = willingToHear`
- **"Does it pay?"** → expresses transactional interest; does **not** accept; Marlow gives/defers exact approved payment
- **"What's wrong with your patient?"** → expresses concern/curiosity; Marlow says it is easier to show them and asks whether they will come downstairs
- **"Not interested."** → genuine refusal to hear the offer; no basement lead

If player asks what needs fetching:

**Marlow:** "A few things from the woodland. I can explain what, but you should see why I can't go myself first."

If player asks about the patient:

**Marlow:** "That's easier to show than explain. He's downstairs."

If player asks about payment:

Marlow states the approved amount when finalized. Asking about money must never count as acceptance.

Then, for any non-refusal route:

**Marlow:** "If you're willing, come downstairs. You can see the situation before you decide anything."

Proposed responses:
- **"All right. Show me."** → `marlowInterest = willingToHear`; basement invitation granted
- **"No."** → refusal to hear; remains upstairs

This explicitly protects agency: following Marlow means **I will hear/see the proposal**, not **I accept the quest**.

---

## Beat 4 — Basement invitation and Garrick warning

Only after the player agrees to hear Marlow out does Marlow grant access.

**Marlow:** "My laboratory is downstairs."

If basement was tried earlier, optional contextual response:
- **"The locked room?"**

**Marlow:** "Yes. Different circumstances now."

If basement was not tried, no response menu is needed.

Marlow starts gathering the surviving material/notes.

**Garrick:** "Bottle-Brain."

Marlow looks back.

**Garrick:** "They try anything funny down there, holler."

Garrick looks at player.

**Garrick:** "I'll hear you."

This keeps Garrick protective without requiring the current forced **"Mine?"** joke. A stronger boot threat can remain an alternate if Central Brain prefers it, but it should not require a fake player question to land.

Marlow unlocks the basement and physically leads. Player follows under normal control.
