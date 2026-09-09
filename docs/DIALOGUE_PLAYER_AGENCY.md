# Dialogue & Player Agency

## Status / authority
This document is the continuity home for the **Dialogue & Player Agency** sub-chat. Exact prose marked **PROPOSED** is not canon until Central Brain approves it. Locked character/story/system facts remain authoritative in their existing documents.

## Ownership / scope
Owns playable NPC dialogue, player responses, pacing/sequencing, conversational state, branching/convergence, interruption/resumption, acceptance/refusal, optional questions, player attitude/expression, NPC reactions to player behavior, and deciding when a choice is justified.

Does **not** independently change quest chronology, combat, Hunting/Cooking/Potion mechanics, Inn layout, character canon, or major story canon. Flag upstream contradictions for Central Brain.

# Core doctrine

## Continue is not a roleplaying choice
When the player has nothing meaningful to decide or express, normal confirm advances the conversation. Do not manufacture `Listen / Continue / Okay / Leave` choices unless they represent real decisions.

Every response must pass:
1. Does it naturally respond to what was just said?
2. Could the player actually know, feel, ask, or decide this now?
3. Does it meaningfully express attitude, request information, make a decision, or alter conversational/state direction?

## Player-intent rule
**NPCs may not advance the player's commitment state before the player does.** Distinguish being present, hearing an offer, showing interest, agreeing to hear details, and accepting a job.

NPCs may propose, infer cautiously, challenge, tempt, ask, or react. They may not decide that the player wants the job, agrees to help, trusts someone, believes something, intends to enter somewhere, is heroic, or knows unseen information.

## Information order
A line may rely only on information already established for its listener. Before each beat track what Garrick knows, Marlow knows, the player knows, what the player expressed, what was offered, and what was accepted/refused.

Setup precedes payoff. **"Might've found you another pair of legs"** must land only after the player understands Marlow needs something collected, cannot leave, and the watch has not supplied help.

## Questions and attitude
Questions are knowledge-gated. Optional questions may deepen character/world knowledge without becoming mandatory exposition gates. Sympathy, skepticism, curiosity, mercenary interest, humor, or rudeness do not automatically change quest commitment. Asking **"What does it pay?"** or **"What do you need?"** is not acceptance.

## Acceptance/refusal
For Marlow's opening job, acceptance occurs only when the player explicitly agrees to gather the ingredients after learning the job. Going downstairs means **agreeing to hear/see the problem**, not agreeing to solve it.

Refusal is real. Marlow does not beg and Garrick does not shame. Reconsideration remains possible while the established time window remains open; existing troll-death/Marlow-departure canon remains unchanged.

## Interruption/resumption
Do not replay completed introductions or re-explain learned information unless asked. Preserve work interest, payment questions, willingness merely to hear Marlow out, acceptance/refusal, and first-contact discoveries. Resume from the last coherent conversational state, not a generic root.

# Character voice guardrails

## Garrick
Gruff, practical, observant, dryly funny, casually intimidating, protective beneath the surface. Speaks economically. Notices behavior instead of narrating mechanics. Can needle Marlow because of established familiarity. Does not gush over the player or call them capable before evidence exists. Can offer opportunity without endorsing a stranger. Not a tutorial narrator. Humor should emerge from situations rather than saturate every line.

## Marlow
Gentle, slightly awkward, curious, creature-focused, experienced, competent. Careful about creatures/evidence. Social hesitation is not incompetence. Does not expose a vulnerable patient casually, call the player an expedition partner before agreement, or beg when refused. Practical questions including payment do not offend him. His laboratory should recontextualize him as experienced and serious.

## Garrick + Marlow
Brotherly history is subtext expressed through shorthand, timing, annoyance, trust, and practical care. Never explain that they are "like brothers" early.

# Known Unity dialogue problems
1. After **"[NAME]. Right."**, current `InnConversation` offers **"Thanks."** It has no conversational cause.
2. **"Might've found you another pair of legs"** currently arrives before the player knows what needs collecting or why Marlow cannot go.
3. **"If you're actually considering this..."** currently invents player interest.
4. Several mandatory one-option responses merely advance text, including current uses of **"Where?"**, **"Mine?"**, **"That's a troll."**, **"You jumped in after a troll?"**, **"He doesn't look the right colour."**, **"And you stay here with him."**, and the final briefing recap. Optional versions may be valid; mandatory Continue-buttons are not.
5. First-lab dialogue is currently a nearly linear question ladder rather than a conversation with genuine information choices.
6. `MarlowOpening.Say` falls back to **Step away** when no choice exists. Presentation should eventually support ordinary Continue independently from roleplaying choices.
7. Current `InnConversation` refusal uses the same `depart` staging as acceptance, risking Marlow leading the player downstairs after refusal. Implementation must separate refusal from willingness to hear him out.

These observations do **not** authorize Unity changes yet.

# Conversation-state model
Recommended conceptual state; exact code representation is downstream.

Player knowledge may track: Garrick identity; Marlow identity; basement ownership; urgent patient; need for field ingredients; why Marlow cannot leave; watch backlog; patient being a troll; illness signs; ingredient list; payment if stated.

Preserve existing first-contact discoveries separately: rooms, board/work, merchandise, basement attempt, immediate leave/return, theft/aggression.

Player commitment should distinguish:
- `workInterest`: unknown / expressed
- `marlowInterest`: unknown / willingToHear / refusedToHear
- `marlowJob`: notOffered / offered / accepted / refused
- `askedPayment`

Do not collapse `willingToHear` and `accepted` into one boolean.

Basement is private before invitation. Invitation grants social access to follow Marlow down. Job acceptance is not required merely to enter on this first invitation.

# Existing locked opening facts preserved
- immediate control in Garrick's Inn and reactive first-contact entrances
- Garrick learns player name
- Marlow breaks the last usable research sample
- town-watch request is backlogged behind urgent human problems
- Garrick eventually says **"Might've found you another pair of legs."**
- Marlow: **"You don't know them."** Garrick: **"Nope."**
- Marlow invites player downstairs before Garrick's warning
- lab returns control for environmental exploration
- baby troll was rescued from drowning; **"He was drowning."** remains preferred
- troll should be green; is pale, not eating, and losing regeneration
- Marlow will not leave it unattended
- Bloodleaf, Silvermoss, Mooncalf Milk are required
- genuine refusal and established downstream consequences remain

# PROPOSED canonical playable dialogue/state specification
**PROPOSED FOR CENTRAL BRAIN REVIEW. Exact prose is not canon yet.** Normal route below; contextual first-contact entrances remain and converge while remembering discoveries.

## Beat 1 — Normal first contact
**Before:** nobody has player commitment. Player knows only observable inn information.

**Garrick:** "New face."

Continue.

**Garrick:** "Garrick. I own the place. You got a name?"

**Player:** "[NAME]."

**Garrick:** "[NAME]. Right."

**No response menu.** If earlier behavior created business, Garrick responds to it. Otherwise allow a natural pause before Marlow's incident rather than listing inn features.

**After:** Garrick/player know each other's names as applicable. No work interest inferred.

## Beat 2 — Accident establishes the problem before the solution
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

**After:** player understands urgent patient + fresh material needed + Marlow cannot leave + watch has not supplied help. The pair-of-legs joke now has setup.

## Beat 3 — Garrick notices the stranger
Garrick looks at player, then Marlow.

**Garrick:** "Might've found you another pair of legs."

**Marlow:** "You don't know them."

**Garrick:** "Nope."

If work interest was previously expressed:

**Garrick:** "But they're looking for work."

Otherwise:

**Garrick:** "But they're standing right here."

**Marlow:** "That's a very low bar for an expedition partner."

**Garrick:** "Good thing you're not taking them on an expedition."

**Garrick:** "You need something fetched. Ask 'em."

Marlow now addresses the player rather than the NPCs continuing to decide around them.

**Marlow:** "Would you be willing to hear me out?"

Meaningful responses:
- **"What needs fetching?"** → practical interest; willing to hear, not accepted
- **"Does it pay?"** → transactional interest; payment may be stated; not accepted
- **"What's wrong with your patient?"** → concern/curiosity; not accepted
- **"Not interested."** → genuine refusal to hear offer

For practical interest:

**Marlow:** "A few things from the woodland. I can explain what, but you should see why I can't go myself first."

For patient question:

**Marlow:** "That's easier to show than explain. He's downstairs."

For payment: use approved amount once finalized. Asking does not accept.

Then on non-refusal routes:

**Marlow:** "If you're willing, come downstairs. You can see the situation before you decide anything."

Responses:
- **"All right. Show me."** → `willingToHear`; invitation granted
- **"No."** → refused to hear; remains upstairs

## Beat 4 — Invitation and warning
**Marlow:** "My laboratory is downstairs."

If basement tried earlier, optional contextual response **"The locked room?"** then:

**Marlow:** "Yes. Different circumstances now."

Otherwise no menu is required.

Marlow gathers surviving material/notes.

**Garrick:** "Bottle-Brain."

Marlow looks back.

**Garrick:** "They try anything funny down there, holler."

Garrick looks at player.

**Garrick:** "I'll hear you."

This preserves Garrick's protective warning without requiring the current forced **"Mine?"** response. Central Brain may retain a stronger boot line if desired.

Marlow unlocks the basement and physically leads. Player follows under normal control.

## Beat 5 — First laboratory exploration
**State:** player agreed only to hear Marlow out. No woodland job has been offered yet. Marlow knows the player is a stranger Garrick opportunistically suggested.

On arrival, **return control**. Do not immediately start a dialogue tree.

Marlow can give one spatial line without a menu:

**Marlow:** "Mind the cases."

Meaningful environmental inspections remain available: expedition gear, field notes/specimens, habitats, fermentation setup. They should establish competence through evidence.

Optional Marlow conversation about the ale arrangement can remain discoverable through inspecting fermentation equipment. It is flavor, not a gate.

After a short exploration window or if the player approaches the sick creature, the troll makes a weak sound. Marlow immediately crosses to it. This behavior draws attention without a quest marker.

## Beat 6 — Sick troll reveal
Marlow kneels beside the pale troll, checking it before speaking.

**Marlow:** "Easy. I'm here."

He notices the player nearby.

**Marlow:** "This is why I can't leave."

Pause/Continue.

**Marlow:** "He should be green. He stopped eating first. Then the colour began to go. His regeneration's slowing too."

Marlow indicates a scratch/wound that should have healed.

Now genuine questions/reactions are available because the player has seen the patient and symptoms:
- **"Why do you have a troll?"**
- **"Do you know what's wrong with him?"**
- **"Is he dangerous?"**
- **"What were you trying to make?"**

These are optional information branches. The player does not need to click all of them to progress.

### Why do you have a troll?
**Marlow:** "I found him during the floods, trapped under a bridge."

Optional follow-up:
- **"You rescued a troll?"**

**Marlow:** "He was drowning."

No further defense is needed.

### Do you know what's wrong?
**Marlow:** "Not exactly. I've been recording the changes. I think I can treat the symptoms and restore his regenerative response."

### Is he dangerous?
Marlow answers according to locked creature canon without pretending a sick wild creature is harmless. Exact risk wording is **UNRESOLVED** pending Central Brain if troll temperament has not been formally defined.

### What were you trying to make?
**Marlow:** "A treatment. The sample upstairs was part of it. I need fresh ingredients to finish the preparation."

After one question or ordinary Continue, Marlow naturally reaches the offer setup.

## Beat 7 — Why Marlow needs the player
If the player has not already asked why Marlow cannot gather material:

**Marlow:** "Normally I'd collect everything myself. I know the woodland. But his condition can change quickly, and I won't leave him unattended."

If watch backlog was already heard upstairs, do not explain it again. If this route somehow skipped it, Marlow may explain the pending request here.

Then:

**Marlow:** "What I need is nearby: Bloodleaf, Silvermoss, and Mooncalf Milk."

He gives the minimal practical description needed to make the decision intelligible, not the full tutorial briefing yet.

**Marlow:** "If you bring them back, I can stay here and finish the treatment."

Only now is the woodland job actually offered.

## Beat 8 — Explicit woodland job decision
**Marlow:** "Will you help?"

Proposed responses:
- **"I'll get what you need."** → ACCEPT
- **"What does it pay?"** → information only; return to decision
- **"Tell me what I'd be walking into."** → asks for woodland risk/context; return to decision
- **"I can't do this."** → REFUSE

If payment was already asked upstairs, do not offer the same question as though it is new. If exact payment was already stated, a mercenary attitude option could instead be **"And the payment's still [amount]?"** only if useful.

### Accept
Set `marlowJob = accepted` and only now set the actual quest/job accepted state.

Marlow's response should be restrained:

**Marlow:** "Thank you. Then I'll show you exactly what to look for."

He provides the existing practical field briefing: Bloodleaf red veins/useful tips/leave stem rooted; Silvermoss on damp shaded stone/leave most; peaceful Mooncalf approach; Mossbacks normally docile if given space. Garrick weapon reminder can remain.

After briefing, **no mandatory recap response** such as "Bloodleaf, Silvermoss, milk. I'll be back." Normal Continue returns control. The objective may now record the accepted task.

### Refuse
Set `marlowJob = refused`.

**Marlow:** "I understand. Thank you for hearing me out."

Return control. No forced lead, no guilt line, no quest acceptance. Reconsideration remains available while canon permits.

## Beat 9 — Reconsideration
If player later speaks to Marlow before consequence resolution:

**Marlow:** "Have you reconsidered?"

Responses:
- **"Tell me what you need again."** → reminder, not acceptance
- **"I'll help."** → acceptance, then briefing
- **"No."** → close naturally

Do not replay troll reveal or pretend the prior refusal never occurred.

# Branch/convergence rules

## First-contact convergence
Kitchen, rooms, basement, board, merchandise, Marlow-first, idle, leave/return, and non-terminal theft branches may converge into the shared accident, but preserve discovered facts and attitudes. Do not re-ask questions already answered.

## Work-interest branch
If