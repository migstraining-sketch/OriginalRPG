# Dialogue & Player Agency

## Status / authority
This is the continuity home for the **Dialogue & Player Agency** sub-chat.

Central Brain has approved the normal-route opening structure and supplied live-playtest revisions through the final pre-implementation clarification pass. Exact lines explicitly marked **LOCKED** below are canonical dialogue. Lines marked **VOICE POLISH OPEN** preserve approved intent/order but may still receive minor wording polish before Unity implementation.

**Do not modify Unity yet.** Central Brain is collecting changes for one coordinated implementation pass.

Existing character, chronology, Inn, combat, Hunting, Cooking, Potion, and UI authorities remain upstream. Dialogue work does not silently rewrite them.

## Ownership / scope
Owns playable NPC dialogue, player responses, pacing/sequencing, conversational state, branching/convergence, interruption/resumption, acceptance/refusal, optional questions, attitude/expression, NPC reactions to player behavior, and deciding when a dialogue choice is justified.

Does **not** independently change quest chronology, combat, Hunting/Cooking/Potion mechanics, Inn layout, character canon, major story canon, or general UI architecture. Flag upstream contradictions for Central Brain.

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

## Information-order rule
A line may rely only on information already established. Before each beat track what Garrick knows, Marlow knows, the player knows, what the player expressed, what was offered, and what was accepted/refused.

Setup precedes payoff. **"Might've found you another pair of legs"** lands only after the player knows Marlow lost his usable sample, needs fresh replacement material, cannot leave someone/something, already requested help, and the Watch has not supplied it.

## Optional questions are not an encyclopedia
Optional questions should deepen character, clarify genuine ambiguity, or express meaningful player attitude. They should not exist merely because an NPC possesses information.

A menu with more buttons is not automatically more agentic. Prefer a small number of strong conversational directions over a root menu that behaves like an FAQ.

When the player asks a meaningful question, allow that answer to become a short conversational branch or follow-up before naturally converging. **Do not immediately bounce back to the same large root question menu after every answer.**

## Dialogue establishes motive, stakes, and objective, not the walkthrough
Dialogue should tell the player **why the task matters** and **what they are trying to accomplish**. It should not pre-solve exploration.

For the woodland job, the player needs to understand:
- Marlow needs Bloodleaf, Silvermoss, and Mooncalf Milk;
- those materials are in the Woodland;
- he cannot leave the troll;
- the materials may let him stabilize/treat the troll.

Marlow should not explain where every ingredient is, exactly how each interaction works, or how every creature behaves before the player explores. Optional clarification such as **"Mooncalf milk?"** may be worthwhile because it is naturally unusual, but Marlow is not a walkthrough narrator.

## Player/world truth is not automatically NPC knowledge
**Save-state truth is not automatically NPC knowledge.**

NPC knowledge must come from something the NPC witnessed, already knew, reasonably inferred from available evidence, or was actually told.

Example: `mossbackEncountered` is player/world history. `marlowKnowsMossbackIncident` is NPC knowledge. Never automatically synchronize them.

## Questions, attitude, acceptance
Questions are knowledge-gated. Sympathy, skepticism, curiosity, mercenary interest, humor, praise, disgust, or rudeness do not automatically change commitment.

Asking **"Does it pay?"** is not acceptance. Going downstairs is **willingness to hear/see the situation**, not quest acceptance.

The actual woodland quest is accepted only at the explicit final commitment gate after the troll has been revealed and the ingredients have been explained.

## The game does not author the player's emotion for them
NPC competence, beauty, horror, danger, humor, or quality may be strongly established by the world without forcing the player character to express the designer's preferred emotional reaction.

This applies directly to Sylvie's tasting scene. Her food may clearly be excellent, but the player must have at least one natural response path that does not force exaggerated praise, comedic disbelief, amazement, or another personality choice the player did not make.

## Refusal is real
Marlow does not beg. Garrick does not shame. Refusing to hear Marlow out and refusing the woodland job are different states. Reconsideration remains possible while established canon permits.

# Dialogue Esc / disengagement doctrine — LOCKED

**When a meaningful dialogue decision is waiting, Esc does not hide the conversation and does not choose a response.**

The choices remain visible.

Esc must never silently mean:
- Yes
- No
- Refuse
- Leave
- Accept
- End conversation

If the fiction allows the player to disengage, provide an explicit neutral authored response such as:
- `I'll think about it.`
- `Not right now.`
- `I should go.`
- `End conversation.`

Exact wording must match the character and situation.

If the player selects that explicit neutral exit, preserve unresolved commitment state where appropriate so they may return later.

If the player cannot reasonably leave because of an authored situation, do not invent a fake neutral exit. Esc simply does not dismiss the decision.

**The previous state where dialogue became hidden while movement remained locked and a Resume Conversation prompt was required is not approved and must not exist in the coordinated implementation.**

Ordinary dialogue remains non-modal where practical: keep the lower-third/lower-side presentation, characters visible, Continue visually distinct from meaningful responses, and meaningful branches moving conversationally rather than behaving like FAQ roots.

## Interruption/resumption
Do not replay completed introductions or re-explain learned information unless asked. Preserve work interest, payment questions, willingness merely to hear Marlow out, acceptance/refusal, troll reveal, first-contact discoveries, and what each NPC actually knows.

A legitimate authored exit may pause an unresolved conversation and preserve its state for later. Resume from the last coherent conversational state rather than a generic root node.

# Voice guardrails

**Garrick:** Gruff, practical, observant, dryly funny, protective beneath the surface. Speaks economically. Notices behavior instead of narrating mechanics. Does not gush over the player or call them capable before evidence exists. Not a tutorial narrator. Humor needs contrast.

**Marlow:** Gentle, slightly awkward, curious, creature-focused, experienced, competent. Social hesitation is not incompetence. Does not expose a vulnerable patient casually, call the player an expedition partner before agreement, or beg when refused. Practical questions including payment do not offend him. The lab should recontextualize him as serious and experienced.

**Sylvie:** Exceptionally skilled, confident, exacting, territorial about her kitchen, practical about ingredients, and unimpressed by performative praise. Her competence can be obvious without forcing the player to praise her.

**Garrick + Marlow:** Brotherly history is subtext through shorthand, timing, annoyance, trust, and practical care. Do not explain that they are "like brothers."

# Required conversation state
Exact code representation is downstream, but implementation must distinguish at minimum:

- `playerNameKnown`: false / true
- `priorWorkInterest`: false / true
- `roomsKnown`
- `boardKnown`
- `merchandiseKnown`
- `basementDiscoveredOrTried`
- `returnedAfterImmediateLeave`
- theft/aggression history where relevant
- `marlowInterest`: unknown / willingToHear / refusedToHear
- `marlowJob`: notOffered / offered / accepted / refused
- `paymentAsked`
- `paymentKnown`
- `trollRevealed`
- learned-information flags only where later dialogue depends on them
- `mossbackEncountered`: player/world history
- `marlowKnowsMossbackIncident`: NPC knowledge
- optional structured Mossback-report details Marlow actually learned
- resumable unresolved-dialogue state after an explicit authored neutral exit

Never collapse `willingToHear` and `accepted`.

# CENTRAL BRAIN-APPROVED normal opening spine

## Beat 1 — Garrick first contact — LOCKED
**Garrick:** "New face."

Continue.

**Garrick:** "Garrick. I own the place."

Continue.

**Garrick:** "You got a name?"

The name-entry field appears here. The player's name is not assumed beforehand.

Player enters: **[NAME]**

**Garrick:** "[NAME]. Right."

**No response menu.** Dialogue closes and player control returns. Give the scene a short natural breathing window before Marlow's accident.

Then:

**CRASH.**

**Garrick:** "Bottle-Brain."

## Beat 2 — Establish Marlow's problem before Garrick volunteers the player — LOCKED
**Marlow:** "I'm fine."

**Garrick:** "Wasn't asking."

**Marlow:** "...That's unfortunate."

**Garrick:** "How unfortunate?"

**Marlow:** "That was the last usable sample I had."

**Garrick:** "Can you make another?"

**Marlow:** "If I had fresh material."

**Garrick:** "Then go get it."

**Marlow:** "I can't leave him."

**Garrick:** "Thought you already put in a request."

**Marlow:** "I did."

**Garrick:** "Watch still sitting on it?"

**Marlow:** "They have more urgent matters."

Garrick notices the player.

**Garrick:** "Might've found you another pair of legs."

**Marlow:** "You don't know them."

**Garrick:** "Nope."

If `priorWorkInterest == true`:

**Garrick:** "But they're looking for work."

Otherwise:

**Garrick:** "But they're standing right here."

Then:

**Marlow:** "That's not usually how I choose expedition partners."

**Garrick:** "Good thing you're not going."

**Marlow:** "That's rather the problem."

**Garrick:** "Then ask 'em."

Minor Marlow wording may still receive voice polish, but information order is locked.

## Beat 3 — Hear-Marlow decision
**Marlow:** "Would you be willing to hear me out?"

Meaningful responses:
- **"What do you need?"**
- **"Does it pay?"**
- **"Sure."**
- **"No."**
- when appropriate, an authored neutral defer such as **"Not right now."** may be added if Central Brain wants a non-refusal exit distinct from No

Esc does nothing to this decision and does not hide it.

### What do you need?
**VOICE POLISH OPEN:**

**Marlow:** "Several things from the woodland. It'll make more sense if you see why I can't go myself."

Do not list the ingredients yet.

### Does it pay?
Marlow is not offended. Asking does not accept the job. Exact amount remains economy-dependent.

### Sure.
Means **I am willing to hear you out**, not **I accept the woodland job**.

### No.
Set `marlowInterest = refusedToHear`.

**Marlow:** "All right."

Conversation closes naturally. Marlow does not lead the player downstairs.

### Interested-route convergence
**VOICE POLISH OPEN:**

**Marlow:** "If you're willing, come downstairs. You can see the situation before you decide anything."

Responses:
- **"All right. Show me."** → `marlowInterest = willingToHear`
- **"No."** → `marlowInterest = refusedToHear`
- an authored neutral defer may be used if the fiction supports postponing without refusal

Esc remains inert while this choice is waiting.

## Beat 4 — Invitation, Garrick warning, laboratory entry
**Marlow:** "My laboratory's downstairs."

If `basementDiscoveredOrTried == true`, optional contextual response:
- **"The locked room?"**

**Marlow:** "Yes. Different circumstances now."

Marlow heads downstairs.

**Garrick:** "Bottle-Brain."

**Garrick:** "You holler if they try anything funny."

**VOICE POLISH OPEN:**

**Marlow:** "I'm sure that won't be necessary."

**Garrick:** "Didn't say it would."

No mandatory player response such as **"Mine?"**

## Beat 5 — Laboratory behavior and reveal fallback
Return control to the player immediately.

Before troll reveal:
- player may explore;
- approaching/discovering troll triggers reveal;
- talking to Marlow first causes Marlow to show the troll naturally.

**VOICE POLISH OPEN:**

**Marlow:** "Come here. I'll show you."

Then:

**Marlow:** "This is why I can't leave."

Do not produce unrelated generic Marlow dialogue in this pre-reveal state.

## Beat 6 — Troll reveal
**Marlow:** "This is why I can't leave."

Allow the visual reveal to breathe.

**Marlow:** "He should be green."

Use a very small number of meaningful directions, preferably:
- **"What's wrong with him?"**
- **"Why is he down here?"**

Do not expose a large FAQ menu.

### What's wrong with him?
**VOICE POLISH OPEN, intent locked:**

**Marlow:** "I don't know. He stopped eating first. Then the colour began to fade. His regeneration's slowing too."

Marlow's uncertainty demonstrates competence. Short follow-up or Continue moves forward. Do not bounce back to a root menu.

### Why is he down here?
**VOICE POLISH OPEN:**

**Marlow:** "I found him in the river during the floods. He was trapped and couldn't get clear."

Optional natural follow-up:

**Player:** "You pulled a troll out of the river?"

**Marlow:** "He was drowning."

**"He was drowning." is LOCKED.**

Removed from this initial menu:
- **"Is he dangerous?"**
- rejected **"Usually? Potentially..."** answer
- **"You keep a troll down here?"**
- ambiguous **"What happened to him?"** rescue-history wording

## Beat 7 — Woodland job setup and offer
Only after the player understands the troll situation:

**Marlow:** "I think I can stabilize him."

**Marlow:** "But what I had left isn't enough. I need fresh material."

Then the objective materials:

**Marlow:** "Bloodleaf. Silvermoss. Mooncalf Milk."

Keep optional clarification small. **"Mooncalf milk?"** is a legitimate unusual-item reaction. Do not add a walkthrough menu.

**Marlow:** "If you bring them back, I can prepare the treatment."

**Marlow:** "Will you help me?"

Commitment responses:
- **"I'll get them."** → `marlowJob = accepted`
- **"No."** → `marlowJob = refused`
- if the fiction permits postponement without refusal, add an explicit neutral authored defer such as **"I'll think about it."** and leave `marlowJob` unresolved

Esc does not hide this decision or select any branch.

On acceptance:

**Marlow:** "Thank you."

Provide only minimal orientation necessary to begin. No fake **"Got it"** button afterward.

On refusal:

**Marlow:** "All right."

No begging or guilt trip.

# Objective language
The accepted woodland objective should remain concise.

Return objective should be the semantic equivalent of:

**"Bring the ingredients back to Marlow."**

or:

**"Return to Marlow."**

Do not instruct the player to report "observations."

# Return-to-Marlow sequence

## Priority rule — LOCKED
**player returns → Marlow realizes ingredients were obtained → relief/hope → Potion Making → troll stabilizes → immediate crisis eases → optional Mossback report becomes appropriate**

The Mossback anomaly must not steal priority from the sick troll.

## Ingredients first
**VOICE POLISH OPEN, emotional direction locked:**

**Marlow:** "You found them."

He checks the materials.

**Marlow:** "All three?"

**Marlow:** "Good. Good."

He looks toward the troll.

Possible continuation:

**Marlow:** "We can do this."

Marlow's restraint cracks slightly because there is finally hope. Do not make him bubbly or melodramatic.

Move promptly into Potion Making. After treatment, the troll's colour begins returning and it becomes hungry again. Only then is there room for lower-priority woodland anomalies.

# Player-initiated Mossback report
`mossbackEncountered` records world/player history. `marlowKnowsMossbackIncident` records NPC knowledge.

After stabilization, if the player chooses to report it:

**VOICE POLISH OPEN:**

**Player:** "Something happened in the woods."

**Marlow:** "What happened?"

**Player:** "A Mossback attacked me."

Only then set `marlowKnowsMossbackIncident = true`.

Marlow may ask a small number of natural questions motivated by prior answers, not an interrogation checklist. Relevant concepts include whether it was cornered, whether young were nearby, whether the player threatened it, whether it had room to leave, and whether it kept pursuing.

When ordinary explanations fail:

**Marlow:** "That's strange."

If the player never reports it, Marlow remains unaware.

# Contextual first-contact variants
Previously approved contextual entrances remain valid and converge into the normal spine while preserving state.

- **Kitchen first:** Garrick stops the stranger. Kitchen discovery does not imply work interest.
- **Rooms first:** preserve `roomsKnown`; work interest changes only if actually expressed.
- **Board first:** reading establishes `boardKnown`, not commitment. Explicit work-seeking can set `priorWorkInterest = true`.
- **Basement first:** establishes `basementDiscoveredOrTried = true`; later **"The locked room?"** exists only if learned.
- **Merchandise/theft first:** looking is not stealing; warnings/aggression persist.
- **Marlow first:** preserve prior encounter/name knowledge so later conversation does not re-introduce him falsely.
- **Idle:** allowed; completed name exchange is not replayed.
- **Leave/return:** leaving is real; return resumes from earliest unresolved state rather than restarting.

# Interruption / resumption implementation rules
1. Every major beat has resumable state separate from presentation node.
2. Once `playerNameKnown == true`, Garrick does not re-ask the player's name without a narrative reason.
3. Sample break never replays after firing.
4. A meaningful pending decision stays visible when Esc is pressed.
5. Esc never changes commitment state.
6. A legitimate authored neutral exit may end the current presentation while preserving unresolved state for later.
7. Do not create hidden-dialogue + movement-locked + Resume Conversation state.
8. `marlowInterest = refusedToHear` never triggers downstairs staging.
9. `marlowInterest = willingToHear` grants invitation/access, not woodland acceptance.
10. Once `trollRevealed == true`, pre-reveal guidance does not replay.
11. Troll branches converge forward rather than reopening a persistent root FAQ.
12. `marlowJob = accepted` is the ordinary state that starts the woodland task.
13. Returning with ingredients prioritizes treatment before optional anomaly reporting.
14. `marlowKnowsMossbackIncident` changes only through a valid in-world information path.

# Sylvie tasting-response agency — LOCKED DIRECTION
After Sylvie's first demonstration, the player tastes the dish. The food may be presented as clearly excellent, but the player must not be forced into the previously preferred amazement script.

The old universal forced exchange:

**Player:** "...What did you do to this?"

**Sylvie:** "Cooked it."

**Player:** "No. Seriously."

**Sylvie:** "So am I."

is no longer a mandatory universal player reaction.

Implementation should present a small set of natural reactions only if the moment warrants player expression. At least one path must be emotionally neutral/non-performative, for example:
- **"Can you teach me to do that?"** → direct interest in learning
- **"What did you do differently?"** → practical curiosity
- **"That's good."** → restrained positive reaction
- **Continue / finish tasting** where no explicit response is needed before the next conversational beat

Exact final response set remains **VOICE POLISH OPEN**, but exaggerated praise/comedic disbelief must not be the only route.

Sylvie's expertise remains intact; player agency changes, not food quality.

# Duskhen wording correction — LOCKED EDITORIAL RULE
The mismatch where narration says the player **asks how Sylvie can tell** and Sylvie replies **"Yes."** is invalid.

Use a real yes/no setup if preserving the joke:

**Player:** "You can tell they're fresh just by looking?"

**Sylvie:** "Yes."

This is the preferred corrected example because the answer now actually answers the question. If implementation instead uses an open **"How can you tell?"**, Sylvie must give an actual explanatory answer rather than **"Yes."**

# Approved exact dialogue
Locked exact lines include:
- **Garrick:** "New face."
- **Garrick:** "Garrick. I own the place."
- **Garrick:** "You got a name?"
- **Garrick:** "[NAME]. Right."
- **Garrick:** "Bottle-Brain."
- **Marlow:** "I'm fine."
- **Garrick:** "Wasn't asking."
- **Marlow:** "...That's unfortunate."
- **Garrick:** "How unfortunate?"
- **Marlow:** "That was the last usable sample I had."
- **Garrick:** "Can you make another?"
- **Marlow:** "If I had fresh material."
- **Garrick:** "Then go get it."
- **Marlow:** "I can't leave him."
- **Garrick:** "Thought you already put in a request."
- **Marlow:** "I did."
- **Garrick:** "Watch still sitting on it?"
- **Marlow:** "They have more urgent matters."
- **Garrick:** "Might've found you another pair of legs."
- **Marlow:** "You don't know them."
- **Garrick:** "Nope."
- contextual **"But they're looking for work." / "But they're standing right here."**
- **Marlow:** "That's not usually how I choose expedition partners."
- **Garrick:** "Good thing you're not going."
- **Marlow:** "That's rather the problem."
- **Garrick:** "Then ask 'em."
- **Marlow:** "Would you be willing to hear me out?"
- response set **"What do you need?" / "Does it pay?" / "Sure." / "No."**
- **Marlow:** "My laboratory's downstairs."
- contextual **"The locked room?"**
- **Marlow:** "Yes. Different circumstances now."
- **Garrick:** "You holler if they try anything funny."
- **Marlow:** "This is why I can't leave."
- **Marlow:** "He should be green."
- **Marlow:** "He was drowning."
- ingredient identities **Bloodleaf / Silvermoss / Mooncalf Milk**
- **Marlow:** "If you bring them back, I can prepare the treatment."
- **Marlow:** "Will you help me?"
- commitment responses **"I'll get them." / "No."**
- refusal **Marlow:** "All right."
- anomaly conclusion **Marlow:** "That's strange."

# Voice-polish-open prose still requiring Central Brain approval
1. Marlow's exact short line after **"What do you need?"**
2. Marlow's exact interested-route invitation wording
3. optional Garrick/Marlow tail after the downstairs warning
4. Marlow's exact pre-reveal **"Come here. I'll show you."** wording
5. final exact troll question labels, currently preferred **"What's wrong with him?" / "Why is he down here?"**
6. exact illness-symptom sentence
7. rescue setup before locked **"He was drowning."**
8. exact stabilization/fresh-material phrasing
9. exact minimal ingredient-list delivery and any **"Mooncalf milk?"** exchange
10. exact neutral defer wording at decisions where Central Brain wants postponement distinct from refusal
11. return-reaction wording **"You found them. / All three? / Good. Good. / We can do this."**
12. exact Mossback report opener/follow-up chain
13. exact return objective wording between **"Bring the ingredients back to Marlow."** and **"Return to Marlow."**
14. final Sylvie tasting response set, provided at least one non-performative path remains

# Unity implementation questions still requiring answers
1. Exact name-entry presentation/validation and whether Esc may cancel the **text-entry UI** before confirmation without acting as a dialogue response.
2. Exact physical trigger/timing for the post-name crash so it breathes without feeling timer-scripted and never replays.
3. Dialogue UI must support ordinary Continue separately from authored choices.
4. Meaningful-choice state must ignore Esc and keep choices visible.
5. Explicit neutral authored exits require a clean state transition that preserves unresolved commitment when appropriate.
6. No suspended hidden-dialogue/Resume Conversation mode may remain from the prior UI proposal.
7. Optional-question branches must converge forward without reopening FAQ roots.
8. Exact troll reveal trigger precedence among approach, direct interaction, exploration time, and talking to Marlow.
9. Exact minimal woodland orientation after the anti-walkthrough revision.
10. Choose final exact return objective text.
11. Exact behavior on partial-ingredient return.
12. Decide whether optional Mossback report is offered immediately after recovery dialogue or through a later manual talk interaction.
13. Persist world encounter knowledge separately from NPC knowledge across save/load.
14. Sylvie tasting UI must permit a non-amazed/non-praising path without weakening presentation of her skill.
15. Duskhen freshness joke should use the corrected yes/no setup or an actual explanatory answer.

# Final standard
**Natural conversation + correct information order + meaningful player expression + characters who remember what has actually happened.**

Additional live-playtest standards:

**The player should feel like they are talking to people, not querying databases.**

**The interface must never convert Back/Esc into a hidden roleplaying decision.**