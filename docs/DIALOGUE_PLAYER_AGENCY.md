# Dialogue & Player Agency

## Status / authority
This is the continuity home for the **Dialogue & Player Agency** sub-chat.

Central Brain has approved the normal-route opening structure with revisions and has now supplied additional live Unity playtest feedback. Exact lines explicitly marked **LOCKED** below are canonical dialogue. Lines marked **VOICE POLISH OPEN** preserve approved intent/order but may still receive minor wording polish before Unity implementation.

**Do not modify Unity yet.** Central Brain is collecting changes for one coordinated implementation pass.

Existing character, chronology, Inn, combat, Hunting, Cooking, and Potion authorities remain upstream. Dialogue work does not silently rewrite them.

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
- Marlow needs Bloodleaf, Silvermoss, and Mooncalf Milk
- those materials are in the woodland
- he cannot leave the troll
- the materials may let him stabilize/treat the troll

Marlow should not give a giant briefing that explains where every ingredient is, exactly how each interaction works, or how every creature behaves before the player has explored anything.

Trust the player to discover. Optional clarification such as **"Mooncalf milk?"** may be worthwhile because it is naturally unusual, but Marlow is not a walkthrough narrator.

## Player/world truth is not automatically NPC knowledge
**Save-state truth is not automatically NPC knowledge.**

NPC knowledge must come from something the NPC:
- witnessed
- already knew
- reasonably inferred from available evidence
- was actually told

This applies to all conversations, not only the opening.

Example: the player can encounter a Mossback in the woodland while Marlow remains unaware. Track the encounter separately from whether Marlow has been told.

## Questions, attitude, acceptance
Questions are knowledge-gated. Sympathy, skepticism, curiosity, mercenary interest, humor, or rudeness do not automatically change commitment.

Asking **"Does it pay?"** is not acceptance. Going downstairs is **willingness to hear/see the situation**, not quest acceptance.

The actual woodland quest is accepted only at the explicit final commitment gate after the troll has been revealed and the ingredients have been explained.

## Refusal is real
Marlow does not beg. Garrick does not shame. Refusing to hear Marlow out and refusing the woodland job are different states. Reconsideration remains possible while established canon permits.

## Interruption/resumption
Do not replay completed introductions or re-explain learned information unless asked. Preserve work interest, payment questions, willingness merely to hear Marlow out, acceptance/refusal, troll reveal, first-contact discoveries, and what each NPC actually knows.

Resume from the last coherent conversational state rather than a generic root node.

# Voice guardrails

**Garrick:** Gruff, practical, observant, dryly funny, protective beneath the surface. Speaks economically. Notices behavior instead of narrating mechanics. Does not gush over the player or call them capable before evidence exists. Not a tutorial narrator. Humor needs contrast.

**Marlow:** Gentle, slightly awkward, curious, creature-focused, experienced, competent. Social hesitation is not incompetence. Does not expose a vulnerable patient casually, call the player an expedition partner before agreement, or beg when refused. Practical questions including payment do not offend him. The lab should recontextualize him as serious and experienced.

**Together:** Brotherly history is subtext through shorthand, timing, annoyance, trust, and practical care. Do not explain that they are "like brothers."

# Implementation status and remaining dialogue revision

Status cleanup, 2026-09-10: the earlier gameplay pass at `72d6411` addressed name entry at Garrick's question, the causeless Thanks reply, post-name free control before the crash, problem setup before the work handoff, separate interest/acceptance/refusal states, Marlow-first troll guidance and ordinary Continue. These remain regression requirements, not a list of unimplemented defects. See [implementation evidence](../WoodlandSpine/Docs/DIALOGUE-AGENCY-IMPLEMENTATION.md). Human pacing/voice acceptance is still not established.

The newer lab/return revisions remain pending:

1. Replace the troll FAQ/root-menu bouncing with contextual questions and forward convergence.
2. Distinguish illness questions from the rescue story; do not force the rejected **"Is he dangerous?"** answer.
3. Reduce the woodland briefing's tendency to pre-solve ingredient discovery.
4. Simplify the return objective and prioritize the sick troll/treatment over the Mossback discussion.
5. Keep Marlow's knowledge separate from encounter/world flags; he learns the incident through player disclosure.
6. Preserve the existing meaningful-choice/Continue distinctions while implementing the newer UI suspend/back rules.

The detailed approved/proposed dialogue below remains authoritative according to its stated approval status. These observations do **not** authorize Unity changes.

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
- `mossbackReportDetailsKnownByMarlow`: optional structured flags if implementation needs to remember what the player actually reported

Never collapse `willingToHear` and `accepted`.

Never automatically synchronize `mossbackEncountered` and `marlowKnowsMossbackIncident`.

# CENTRAL BRAIN-APPROVED normal opening spine

## Beat 1 — Garrick first contact

### LOCKED exact sequence
**Garrick:** "New face."

Continue.

**Garrick:** "Garrick. I own the place."

Continue.

**Garrick:** "You got a name?"

### Name entry behavior — LOCKED
The player name is **not assumed before this point**. The name-entry field appears when Garrick asks. Player types/confirms the name.

Example:

Player enters: **Migs**

**Garrick:** "Migs. Right."

### Response behavior — LOCKED
**No response menu.** Remove current **"Thanks/Thank you"**.

After **"[NAME]. Right."**, dialogue closes and player control returns. Give the scene a short natural breathing window so Marlow's accident feels like an event in the physical inn.

Then:

**CRASH.**

Garrick turns toward Marlow.

**Garrick:** "Bottle-Brain."

## Beat 2 — Establish Marlow's problem before Garrick volunteers the player

### LOCKED exact sequence
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

Minor Marlow wording may receive **VOICE POLISH**, but the information order and Garrick/Marlow logic are locked.

## Beat 3 — Player chooses whether to hear Marlow out

### LOCKED prompt
**Marlow:** "Would you be willing to hear me out?"

### LOCKED meaningful responses
- **"What do you need?"**
- **"Does it pay?"**
- **"Sure."**
- **"No."**

Do **not** offer **"What's wrong with him?"** here. The player only knows Marlow said **"I can't leave him."**

### What do you need?
**VOICE POLISH OPEN, intent locked:**

**Marlow:** "Several things from the woodland. It'll make more sense if you see why I can't go myself."

Do not list the ingredients yet.

### Does it pay?
Marlow is not offended. Asking does not accept the job. Exact amount remains economy-dependent. A brief Garrick/Marlow exchange ensuring fair pay is permitted.

### Sure.
Meaning is explicitly **I am willing to hear you out**, not **I accept the woodland job**.

### No.
Set `marlowInterest = refusedToHear`.

**VOICE POLISH OPEN:**

**Marlow:** "All right."

Garrick does not guilt or shame the player. Conversation closes. Marlow does not head downstairs expecting the player to follow.

### Interested-route convergence
For any non-refusal route:

**VOICE POLISH OPEN:**

**Marlow:** "If you're willing, come downstairs. You can see the situation before you decide anything."

Responses:
- **"All right. Show me."** → `marlowInterest = willingToHear`
- **"No."** → `marlowInterest = refusedToHear`

Going downstairs is not quest acceptance.

## Beat 4 — Invitation, Garrick warning, laboratory entry

### LOCKED
**Marlow:** "My laboratory's downstairs."

If `basementDiscoveredOrTried == true`, optional contextual response:
- **"The locked room?"**

**Marlow:** "Yes. Different circumstances now."

Do not show this response if the player never discovered the basement door.

Marlow begins heading downstairs.

**Garrick:** "Bottle-Brain."

Marlow looks back. Garrick looks toward the player.

**Garrick:** "You holler if they try anything funny."

### VOICE POLISH OPEN
Possible tail:

**Marlow:** "I'm sure that won't be necessary."

**Garrick:** "Didn't say it would."

No mandatory player response such as **"Mine?"**

## Beat 5 — Laboratory behavior and reveal fallback
Once downstairs, **return control to the player immediately**.

The lab should expose expedition gear, notes, specimens, creature sketches, plants, alchemy equipment, habitats/enclosures, and evidence that Marlow is experienced and competent.

### Critical interaction rule — LOCKED
Before the troll reveal:
- if player explores, allow it
- if player discovers/approaches the troll, trigger reveal
- if player talks to Marlow first, Marlow naturally shows them the troll

Recommended staging:

**Marlow:** "Come here. I'll show you."

Marlow physically leads or turns the player's attention toward the troll.

Then:

**Marlow:** "This is why I can't leave."

Do not produce unrelated generic Marlow dialogue in this pre-reveal state.

General rule: **When an NPC explicitly brings the player somewhere to show them something, talking to that NPC before inspecting the intended object should naturally lead the player to it rather than requiring a hidden interaction order.**

# REVISED Beat 6 — Troll reveal

Once the pale baby troll is actually visible:

### LOCKED
**Marlow:** "This is why I can't leave."

Allow the visual reveal to breathe.

**Marlow:** "He should be green."

Do not immediately expose a four-button FAQ menu.

### Initial question structure — APPROVED DIRECTION
Use a very small number of meaningful directions, preferably:
- **"What's wrong with him?"**
- **"Why is he down here?"**

Exact wording may receive minor polish, but preserve the reduced information load and avoid ambiguous **"What happened to him?"** at this moment.

### Branch A — What's wrong with him?
Marlow does not know the underlying cause.

**VOICE POLISH OPEN, intent locked:**

**Marlow:** "I don't know."

He then explains only the observed symptoms:
- he stopped eating
- his colour faded
- his regeneration is slowing

Recommended wording:

**Marlow:** "I don't know. He stopped eating first. Then the colour began to fade. His regeneration's slowing too."

This uncertainty demonstrates competence because Marlow does not pretend to possess a diagnosis he does not have.

Allow a short natural follow-up or Continue into the job setup. Do not bounce immediately back to a large question root.

### Branch B — Why is he down here?
Marlow answers the immediate question by establishing that he found/rescued the troll during flooding.

**VOICE POLISH OPEN:**

**Marlow:** "I found him in the river during the floods. He was trapped and couldn't get clear."

Natural optional follow-up:

**Player:** "You pulled a troll out of the river?"

**Marlow:** "He was drowning."

**"He was drowning." is LOCKED.** Keep the beat simple. No heroic speech.

This branch then converges naturally into the illness/treatment setup without returning to a giant question menu.

### Removed from the initial troll menu
- **"Is he dangerous?"** is removed unless a future scene creates a genuinely characterful reason to ask it.
- The rejected line **"Usually? Potentially. Right now, I'm more worried about him."** must not be implemented.
- **"You keep a troll down here?"** is superseded by the clearer **"Why is he down here?"** direction for this scene.
- Ambiguous **"What happened to him?"** is not used for the rescue-history branch here.

# REVISED Beat 7 — Woodland job setup and offer
Only after the player understands the troll situation does Marlow explain the actual task.

### Purpose first
**LOCKED intent:**

**Marlow:** "I think I can stabilize him."

**Marlow:** "But what I had left isn't enough. I need fresh material."

If the player already learned the material comes from the woodland, a contextual response such as **"From the woodland?"** may appear. Do not offer it otherwise.

Then Marlow gives the objective:

**Marlow:** "Bloodleaf. Silvermoss. Mooncalf Milk."

**VOICE POLISH OPEN** on exact punctuation/phrasing, but the ingredient list itself is locked by upstream canon.

### Keep optional clarification small
A natural reaction such as:
- **"Mooncalf milk?"**

may be offered because the item is unusual.

Do not automatically add a full menu containing **Where do I find them? / What do I do with them? / How do I get each one? / What creatures are there?**

Marlow may provide only enough orientation to make the objective actionable, for example that the needed materials are in the woodland. Do not pre-solve the encounters.

### Actual commitment gate
**Marlow:** "If you bring them back, I can prepare the treatment."

**Marlow:** "Will you help me?"

### LOCKED commitment responses
- **"I'll get them."** → `marlowJob = accepted`
- **"No."** → `marlowJob = refused`

Do not interpret earlier curiosity, sympathy, payment questions, entering the lab, or agreeing to hear Marlow out as acceptance.

### Acceptance
On acceptance, Marlow may genuinely say:

**Marlow:** "Thank you."

Then give only minimal practical orientation actually required to begin. No giant acquisition walkthrough. No fake **"Got it"** response afterward. Normal Continue returns control.

### Refusal
**Marlow:** "All right."

No begging. No Garrick guilt trip. Existing reconsideration window and downstream consequences remain unchanged.

# Objective-language recommendation

## Active woodland objective
Keep the accepted objective centered on what the player must find. Exact UI wording belongs to implementation, but do not add unnecessary explanatory clutter.

## Return objective — APPROVED DIRECTION
Replace wording equivalent to:

**"Bring the ingredients and your observations back to Marlow in the lab."**

with something simple such as:

**"Bring the ingredients back to Marlow."**

or:

**"Return to Marlow."**

Do **not** tell the player they have "observations" to report. Whether they report anything beyond the ingredients is their conversational choice.

# REVISED return-to-Marlow sequence

## Priority rule
The return must preserve Marlow's emotional priority:

**player returns → Marlow realizes ingredients were obtained → relief/hope → Potion Making → troll stabilizes → immediate crisis eases → optional Mossback report becomes appropriate**

The Mossback anomaly must not steal priority from the sick troll.

## Return Beat A — Ingredients first
When the player returns with the required materials, Marlow's first concern is whether the treatment can now be made.

### VOICE POLISH OPEN, approved emotional direction
**Marlow:** "You found them."

He checks the materials.

**Marlow:** "All three?"

He confirms Bloodleaf, Silvermoss, and Mooncalf Milk.

**Marlow:** "Good. Good."

Marlow looks toward the troll.

Possible continuation:

**Marlow:** "We can do this."

The exact prose remains open to minor voice polish. The emotional function is locked: Marlow's usual restraint cracks slightly because there is finally hope for the creature he refused to leave.

Do not make him bubbly, melodramatic, or celebratory. Do not interrupt this moment with a Mossback interrogation or a large dialogue menu.

## Return Beat B — Move promptly into Potion Making
After checking the ingredients, Marlow transitions directly into preparing the treatment with the player.

Exact Potion Making mechanics remain owned by the Potion/system authority. Dialogue should support urgency and supervision without re-teaching the woodland.

## Return Beat C — Troll stabilizes
After the treatment is successfully prepared, the first dose goes to the troll under existing canon.

The troll's colour begins returning and, most importantly, it becomes hungry again. The immediate crisis visibly eases.

Only after this point is there conversational room for lower-priority woodland anomalies.

# Player-initiated Mossback report

## Knowledge state — LOCKED principle
`mossbackEncountered` records player/world history.

`marlowKnowsMossbackIncident` records whether Marlow has actually learned about it.

These are separate.

Marlow does **not** automatically know the encounter happened simply because it exists in save-state.

## When the report can happen
After the troll is stabilized and immediate treatment dialogue has settled, create a natural optional player opportunity to raise the incident.

Possible player line:

**VOICE POLISH OPEN:**

**Player:** "Something happened in the woods."

**Marlow:** "What happened?"

**Player:** "A Mossback attacked me."

At this point set `marlowKnowsMossbackIncident = true`.

Only now may Marlow investigate the report.

## Naturalist follow-up
Marlow may ask a small number of natural questions based on the report, such as whether:
- it was cornered
- young were nearby
- the player threatened/provoked it
- it had room to leave
- it continued pursuing after the player gave space

Do not turn this into an interrogation checklist. Use a short conversational chain where each answer motivates the next question.

When ordinary explanations fail, preserve the established understated conclusion:

**Marlow:** "That's strange."

He can state that he will investigate when he is able. Do not create a giant main-story reveal or dramatic villain clue.

## If the player never reports it
Marlow remains unaware.

No later Marlow dialogue may reference the Mossback incident unless he learns it through some other valid in-world source.

# Contextual first-contact variants
Previously approved contextual entrances remain valid and must converge into the normal spine while preserving state.

## Kitchen first
Garrick can stop the unknown stranger from entering the kitchen. If that interaction already established Garrick's identity or prompted name entry, do not replay those lines later. Kitchen discovery does not imply work interest.

## Stairs/rooms first
If the player learns rooms cost money or asks about price, preserve `roomsKnown`. If they express being broke or ask about work as a result, `priorWorkInterest` may become true only if their dialogue actually expressed that interest.

## Board first
Reading the board establishes `boardKnown`, not automatic commitment. Trying to take a posting can cause Garrick to block it because he does not know the player's capability. If the player explicitly asks for work, set `priorWorkInterest = true`.

## Basement first
Trying the locked basement establishes `basementDiscoveredOrTried = true` and that Marlow claims the laboratory. Later **"The locked room?"** is available only because the player actually knows this.

## Merchandise/theft first
Looking does not equal stealing. Theft warnings and aggression remain remembered. Garrick's later dialogue should not explain merchandise as new information if the player already interacted with it.

## Marlow first
If the player speaks with Marlow before Garrick, preserve that Marlow has already been encountered and possibly named. The shared scene should not introduce him as though they never met.

## Idle
Idling is allowed. Garrick may eventually acknowledge the player without forcing the whole opening. If the name exchange occurs, preserve it and do not repeat.

## Leave/return
Leaving is real. On return, Garrick may acknowledge that the player came back, then continue from the earliest unresolved conversational state rather than restarting the introduction.

# Interruption/resumption implementation rules

1. Every major beat should have a resumable state distinct from its presentation node.
2. Once `playerNameKnown == true`, Garrick never asks the player's name again unless a separate narrative reason exists.
3. Once the sample-break event has fired, it never replays.
4. Once Marlow has asked whether the player will hear him out, interruption resumes at that commitment state rather than re-running Garrick/Marlow setup.
5. `marlowInterest = refusedToHear` must not trigger downstairs staging.
6. `marlowInterest = willingToHear` grants invitation/access but not the woodland job.
7. Once `trollRevealed == true`, pre-reveal Marlow guidance does not replay.
8. Optional troll branches should remember which core information the player learned where needed, but the scene should converge forward rather than reopening a persistent root FAQ.
9. `marlowJob = accepted` is the only ordinary state that starts the woodland task.
10. Returning with ingredients prioritizes treatment before any optional anomaly report.
11. `marlowKnowsMossbackIncident` changes only through an in-world information path, normally the player telling him.
12. If the player never reports the incident, later conversations must respect that ignorance.

# Approved exact dialogue
The following exact lines are currently locked by Central Brain/upstream continuity:

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
- contextual Garrick: **"But they're looking for work."** / **"But they're standing right here."**
- **Marlow:** "That's not usually how I choose expedition partners."
- **Garrick:** "Good thing you're not going."
- **Marlow:** "That's rather the problem."
- **Garrick:** "Then ask 'em."
- **Marlow:** "Would you be willing to hear me out?"
- response set: **"What do you need?" / "Does it pay?" / "Sure." / "No."**
- **Marlow:** "My laboratory's downstairs."
- contextual player: **"The locked room?"**
- **Marlow:** "Yes. Different circumstances now."
- **Garrick:** "You holler if they try anything funny."
- **Marlow:** "This is why I can't leave."
- **Marlow:** "He should be green."
- **Marlow:** "He was drowning."
- ingredient identities: **Bloodleaf / Silvermoss / Mooncalf Milk**
- **Marlow:** "If you bring them back, I can prepare the treatment."
- **Marlow:** "Will you help me?"
- commitment responses: **"I'll get them." / "No."**
- refusal response: **Marlow:** "All right."
- anomaly conclusion after valid report/investigation: **Marlow:** "That's strange."

# Voice-polish-open prose requiring Central Brain approval
The following lines/wordings are implementation-ready in intent but not declared immutable canon yet:

1. **Marlow:** "Several things from the woodland. It'll make more sense if you see why I can't go myself."
2. **Marlow:** "If you're willing, come downstairs. You can see the situation before you decide anything."
3. **Marlow:** "I'm sure that won't be necessary." / **Garrick:** "Didn't say it would."
4. **Marlow:** "Come here. I'll show you."
5. Initial troll question wording, currently preferred as **"What's wrong with him?" / "Why is he down here?"**
6. Illness answer wording, currently: **"I don't know. He stopped eating first. Then the colour began to fade. His regeneration's slowing too."**
7. Rescue setup before **"He was drowning."**
8. **Marlow:** "I think I can stabilize him." / **"But what I had left isn't enough. I need fresh material."** if Central Brain wants exact wording locked
9. Minimal ingredient-list delivery and any optional **"Mooncalf milk?"** exchange
10. Acceptance response **"Thank you."** and any minimal departure orientation
11. Return reaction wording: **"You found them." / "All three?" / "Good. Good." / "We can do this."**
12. Player-initiated Mossback report opener: **"Something happened in the woods." / "A Mossback attacked me."**
13. Exact naturalist follow-up sequence before **"That's strange."**
14. Exact simplified return objective, with current preferred options **"Bring the ingredients back to Marlow."** or **"Return to Marlow."**

# Unity implementation questions still requiring answers
1. **Name-entry UI:** exact interaction/presentation for the text field when Garrick asks the name, including validation and cancel/back behavior.
2. **Post-name breathing trigger:** how long/what physical trigger causes the sample crash after control returns, without feeling timer-scripted or allowing it to replay.
3. **Dialogue UI capability:** ensure ordinary Continue can exist without manufacturing a response button, while genuine choices use a separate menu.
4. **Optional-question flow:** implementation needs branch nodes that can converge forward without automatically reopening the same root menu.
5. **Troll reveal staging:** exact trigger precedence among approach radius, direct troll interaction, elapsed exploration, and talking to Marlow first.
6. **Minimal woodland orientation:** Central Brain should confirm whether Marlow says only that the ingredients are in the woodland, or whether one very light location hint per ingredient is allowed after the new anti-walkthrough revision.
7. **Objective wording:** choose final exact return text between **"Bring the ingredients back to Marlow."** and **"Return to Marlow."**
8. **Return ingredient validation:** exact behavior if the player returns with only some ingredients, or reaches Marlow before the full set is complete.
9. **Mossback report timing/UI:** decide whether the optional report appears automatically as a player response after recovery dialogue, or remains available through a later manual talk interaction.
10. **Partial Mossback disclosure:** if the player reports only that something strange happened but does not identify the Mossback immediately, determine whether Marlow asks one natural follow-up or the branch waits.
11. **State persistence:** implementation must persist `mossbackEncountered` separately from `marlowKnowsMossbackIncident` across saving/loading and later dialogue.
12. **Legacy dialogue cleanup:** coordinated Unity pass must remove/rework old root FAQ menus, Step-away fallbacks, automatic Mossback interrogation, and any code path that derives NPC knowledge directly from world encounter flags.

# Final standard
**Natural conversation + correct information order + meaningful player expression + characters who remember what has actually happened.**

Additional live-playtest standard:

**The player should feel like they are talking to people, not querying databases.**
