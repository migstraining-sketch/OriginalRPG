# Dialogue & Player Agency

## Status / authority
This is the continuity home for the **Dialogue & Player Agency** sub-chat.

Central Brain has now approved the **normal-route opening conversation structure with revisions**. Exact lines explicitly marked **LOCKED** below are canonical dialogue. Lines marked **VOICE POLISH OPEN** preserve approved intent/order but may still receive minor wording polish before Unity implementation. Unity is **not** to be modified yet.

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

## Questions, attitude, acceptance
Questions are knowledge-gated. Optional questions may deepen character/world knowledge without becoming mandatory exposition gates. Sympathy, skepticism, curiosity, mercenary interest, humor, or rudeness do not automatically change commitment.

Asking **"Does it pay?"** is not acceptance. Going downstairs is **willingness to hear/see the situation**, not quest acceptance.

The actual woodland quest is accepted only at the explicit final commitment gate after the troll has been revealed and the ingredients have been explained.

## Refusal is real
Marlow does not beg. Garrick does not shame. Refusing to hear Marlow out and refusing the woodland job are different states. Reconsideration remains possible while established canon permits.

## Interruption/resumption
Do not replay completed introductions or re-explain learned information unless asked. Preserve work interest, payment questions, willingness merely to hear Marlow out, acceptance/refusal, troll reveal, and first-contact discoveries. Resume from the last coherent state.

# Voice guardrails

**Garrick:** Gruff, practical, observant, dryly funny, protective beneath the surface. Speaks economically. Notices behavior instead of narrating mechanics. Does not gush over the player or call them capable before evidence exists. Not a tutorial narrator. Humor needs contrast.

**Marlow:** Gentle, slightly awkward, curious, creature-focused, experienced, competent. Social hesitation is not incompetence. Does not expose a vulnerable patient casually, call the player an expedition partner before agreement, or beg when refused. Practical questions including payment do not offend him. The lab should recontextualize him as serious and experienced.

**Together:** Brotherly history is subtext through shorthand, timing, annoyance, trust, and practical care. Do not explain that they are "like brothers."

# Known Unity dialogue problems
1. Current `InnConversation` assumes the configured player name before Garrick asks for it. The approved flow requires name entry at **"You got a name?"**
2. After **"[NAME]. Right."**, current `InnConversation` offers **"Thanks."** It has no conversational cause.
3. The current prototype chains Marlow's accident immediately after the name exchange. Approved pacing returns control after **"[NAME]. Right."** so the crash happens physically in the inn after a short breathing space.
4. **"Might've found you another pair of legs"** currently arrives before the player understands what needs collecting or why Marlow cannot go.
5. **"If you're actually considering this..."** invents player interest.
6. Several mandatory one-option responses merely advance text, including current uses of **"Where?"**, **"Mine?"**, **"That's a troll."**, **"You jumped in after a troll?"**, **"He doesn't look the right colour."**, **"And you stay here with him."**, and the final briefing recap.
7. First-lab dialogue is a nearly linear question ladder rather than genuine information/attitude choices.
8. The player can currently encounter unrelated generic Marlow dialogue before discovering the troll. Approved behavior requires Marlow to guide the player to the intended reveal if spoken to first.
9. `MarlowOpening.Say` falls back to **Step away** when no choice exists. Presentation should eventually support ordinary Continue independently from roleplaying choices.
10. Current `InnConversation` refusal shares departure staging with acceptance, risking Marlow leading the player downstairs after refusal.

These observations do **not** authorize Unity changes yet.

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
- learned information flags for woodland source, ingredient list, Marlow's inability to leave, Watch backlog, illness symptoms, rescue history, and any other question-gated knowledge used later

Never collapse `willingToHear` and `accepted`.

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

After **"[NAME]. Right."**, dialogue closes and player control returns. The scene receives a short natural breathing window so Marlow's accident feels like an event in the physical inn, not the next exposed dialogue node.

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

At this point the player knows enough for Garrick's solution to be intelligible: Marlow lost the last usable sample, needs fresh replacement material, cannot leave someone/something, already sought outside help, and the Watch has not supplied it.

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

The minor Marlow wording in this section may receive **VOICE POLISH**, but the information order and Garrick/Marlow logic are locked.

## Beat 3 — Player chooses whether to hear Marlow out

Marlow now addresses the player directly.

### LOCKED prompt
**Marlow:** "Would you be willing to hear me out?"

### LOCKED meaningful responses
- **"What do you need?"**
- **"Does it pay?"**
- **"Sure."**
- **"No."**

Do **not** offer **"What's wrong with him?"** here. The player only knows Marlow said **"I can't leave him."** They have not yet seen a sick creature.

### Branch: What do you need?
**VOICE POLISH OPEN, intent locked:** Marlow establishes only that he needs several things from the woodland and that seeing the situation downstairs will explain why he cannot retrieve them himself. Do not list Bloodleaf/Silvermoss/Mooncalf Milk yet.

Recommended implementation wording:

**Marlow:** "Several things from the woodland. It'll make more sense if you see why I can't go myself."

This sets `marlowInterest = willingToHear` only when the player later agrees to go downstairs. Asking the question itself is interest, not commitment.

### Branch: Does it pay?
Marlow is not offended. Asking does not accept the job. Exact amount remains economy-dependent.

**VOICE POLISH OPEN:** a brief Garrick/Marlow exchange ensuring fair pay is permitted, but the implementation must not invent a final number until economy authority supplies it.

### Branch: Sure.
Meaning is explicitly **I am willing to hear you out**, not **I accept the woodland job**.

### Branch: No.
Set `marlowInterest = refusedToHear`.

**VOICE POLISH OPEN:**

**Marlow:** "All right."

Garrick does not guilt or shame the player. Conversation closes naturally. Marlow does not head downstairs expecting the player to follow.

### Interested-route convergence
For any non-refusal route, Marlow eventually states the approved intent:

**VOICE POLISH OPEN:**

**Marlow:** "If you're willing, come downstairs. You can see the situation before you decide anything."

Meaningful responses:
- **"All right. Show me."** → `marlowInterest = willingToHear`
- **"No."** → `marlowInterest = refusedToHear`

Going downstairs is not quest acceptance.

## Beat 4 — Invitation, Garrick warning, laboratory entry

### LOCKED
**Marlow:** "My laboratory's downstairs."

If `basementDiscoveredOrTried == true`, optional contextual response becomes available:
- **"The locked room?"**

**Marlow:** "Yes. Different circumstances now."

Do not show that response if the player never discovered the basement door.

Marlow begins heading downstairs.

**Garrick:** "Bottle-Brain."

Marlow looks back. Garrick looks toward the player.

**Garrick:** "You holler if they try anything funny."

### VOICE POLISH OPEN
Possible Marlow/Garrick tail:

**Marlow:** "I'm sure that won't be necessary."

**Garrick:** "Didn't say it would."

No mandatory player response such as **"Mine?"**

## Beat 5 — Laboratory behavior and reveal fallback
Once downstairs, **return control to the player immediately**. Do not launch another long conversation.

The lab should naturally expose expedition gear, notes, specimens, creature sketches, plants, alchemy equipment, habitats/enclosures, and evidence that Marlow is experienced and competent.

### Critical interaction rule — LOCKED
The player must not be required to guess that they need to inspect the troll before talking to Marlow.

Before the troll reveal:
- if player explores, allow it
- if player discovers/approaches the troll, trigger reveal
- if player talks to Marlow first, Marlow naturally shows them the troll

Recommended exact staging:

**Marlow:** "Come here. I'll show you."

Marlow physically leads or turns the player's attention toward the troll.

Then:

**Marlow:** "This is why I can't leave."

Do not produce unrelated generic Marlow dialogue in this pre-reveal state.

General rule: **When an NPC explicitly brings the player somewhere to show them something, talking to that NPC before inspecting the intended object should naturally lead the player to it rather than requiring a hidden interaction order.**

## Beat 6 — Troll reveal
Once the pale baby troll is actually visible:

### LOCKED
**Marlow:** "This is why I can't leave."

Allow the visual reveal to breathe.

**Marlow:** "He should be green."

Now patient questions are logically available.

### Approved optional question territory
- **"What's wrong with him?"**
- **"What happened to him?"**
- **"You keep a troll down here?"**
- **"Is he dangerous?"**

These are optional information/attitude branches that reconverge naturally. The player is not required to exhaust them.

### What's wrong with him?
Core answer is **LOCKED IN INTENT** and may receive minor wording polish:

**Marlow:** "I don't know."

Then establish observable symptoms:
- stopped eating
- colour faded
- regeneration slowing

Marlow's uncertainty is deliberate competence, not ignorance-as-comedy.

Recommended wording:

**Marlow:** "I don't know. He stopped eating first. Then the colour began to fade. His regeneration's slowing too."

### What happened to him?
Preserve established rescue core. Marlow found him in the river/flooding.

Eventually exact line:

**Marlow:** "He was drowning."

Keep it simple. No heroic monologue.

### You keep a troll down here?
**VOICE POLISH OPEN:** Marlow answers matter-of-factly and may naturally bridge into the rescue history. Do not turn it into defensive exposition.

### Is he dangerous?
Approved intent:

**Marlow:** "Usually? Potentially."

He looks at the sick troll.

**Marlow:** "Right now, I'm more worried about him."

Exact wording may receive minor voice polish while preserving the idea that trolls are not unrealistically harmless.

## Beat 7 — Actual woodland job setup and offer
Only after the player understands the troll situation does Marlow explain the actual task.

### LOCKED intent / wording currently approved
**Marlow:** "I think I can stabilize him."

**Marlow:** "But what I had left isn't enough. I need fresh material."

If the player already learned the material comes from the woodland, optional contextual response may appear:
- **"From the woodland?"**

Do not offer it otherwise.

Then Marlow establishes the required ingredients:
- Bloodleaf
- Silvermoss
- Mooncalf Milk

### Approved optional questions
- **"Where do I find them?"**
- **"Mooncalf milk?"**
- **"What am I supposed to do with them?"**
- **"And you can't go because of him?"**
- **"What are you paying?"** only if payment has not already been discussed

These are optional information questions, not one-option Continue gates.

Once the player has enough information:

**Marlow:** "If you bring them back, I can prepare the treatment."

**Marlow:** "Will you help me?"

This is the actual quest commitment gate.

### LOCKED commitment responses
- **"I'll get them."** → `marlowJob = accepted`
- **"No."** → `marlowJob = refused`

Earlier curiosity, sympathy, payment questions, lab entry, and willingness to hear Marlow out must never set acceptance.

### Acceptance
On acceptance:

**Marlow:** "Thank you."

Then provide only the practical information needed to attempt the woodland section: Bloodleaf appearance/harvest, Silvermoss environment/harvest, peaceful Mooncalf approach, Mossbacks normally docile if given space, and Garrick weapon reminder if still appropriate.

After the briefing, normal Continue returns control. No fake **"Got it"** or forced recap button.

### Refusal
On refusal:

**Marlow:** "All right."

No begging. No Garrick guilt trip. No immediate forced reversal. Existing reconsideration window and downstream troll/Marlow consequences remain unchanged.

# Contextual first-contact variants around the approved spine
These variants determine how the player reaches Beat 1/Beat 2 without continuity amnesia. They do not replace the approved normal spine.

## Normal bar approach
Use Beat 1 exactly as written. Name entry occurs at Garrick's question. After **"[NAME]. Right."**, control returns. Marlow's crash happens after the breathing window.

## Kitchen before introduction
Garrick reacts to the trespass first:

**Garrick:** "Oi."

**Garrick:** "Don't even know your name and you're already trying to get into my kitchen?"

Approved response territory remains contextual:
- **"Just looking around."**
- **"Your kitchen?"**
- **"Who's going to stop me?"**

After the reaction, converge into Garrick introducing himself, then **"You got a name?"** with the actual name-entry field. Set kitchen/private-boundary knowledge. Do not later explain the kitchen as brand-new information.

## Stairs/rooms before introduction
Garrick begins with:

**Garrick:** "Rooms aren't free."

Meaningful responses can include actual room-price inquiry, saying the player was only looking, or admitting they cannot afford it. If price is learned, set `roomsKnown` and do not repeat the same explanation later.

Converge into Garrick identifying himself and asking the player's name. If the player explicitly expresses a need for money/work during this branch, set `priorWorkInterest = true`; otherwise do not infer it merely because they are poor.

## Contract board before introduction
Reading is allowed. Attempting to take a posting brings Garrick in.

If the player asks what the board is, explain local paid problems briefly. If they try to claim a job, preserve Garrick's competence concern rather than tutorial gating.

Set `boardKnown = true`. Set `priorWorkInterest = true` only if the player actually expresses wanting work/taking a posting.

Later, when Garrick says **"Might've found you another pair of legs"**, he may use **"But they're looking for work"** only if that interest was truly expressed.

## Basement before invitation
First discovery establishes that the door is Marlow's laboratory and private. A repeated attempt can involve Garrick and naturally roll into introduction.

Set `basementDiscoveredOrTried = true`.

Later, after Marlow invites the player down, the optional **"The locked room?"** response becomes available. Do not show it otherwise.

## Merchandise before introduction
Looking is allowed. Taking without paying receives Garrick's warning. Asking whether it is for sale can naturally establish merchant context. Set `merchandiseKnown` only when the player actually learns that.

If theft de-escalates, converge into introduction without pretending the warning never happened. Aggressive/theft escalation remains governed by existing consequence canon.

## Marlow-first approach
Marlow may greet the stranger and Garrick can enter through the Bottle-Brain exchange already established by Inn continuity. If Marlow gives his own name before Garrick's shared conversation, set Marlow identity known and do not reintroduce him later as though they have never met.

Garrick still asks the player's name unless already learned through another valid route.

## Idle
Garrick may eventually acknowledge the stranger. This is not forced immediately. If the player engages, converge into his introduction and name-entry beat. If they remain seated, let them remain seated.

## Immediate leave / return
Leaving works. No invisible wall.

On return, Garrick may use a contextual acknowledgement such as **"Back already?"** before introducing himself if the player still does not know him. Set `returnedAfterImmediateLeave = true` and do not replay the original first-sighting bark unchanged if the return line already acknowledges history.

# Contextual shared-scene rules
1. **Name known:** Garrick asks the player's name only if it has not been entered/confirmed yet.
2. **Work interest:** use **"But they're looking for work"** only when the player actually expressed work interest. Otherwise use **"But they're standing right here."**
3. **Basement knowledge:** **"The locked room?"** exists only when that door was previously discovered/attempted.
4. **Payment:** if already discussed upstairs, do not offer **"What are you paying?"** again in the lab as though new. If amount was stated, preserve it.
5. **Woodland source:** **"From the woodland?"** exists only if Marlow already told the player the material comes from there.
6. **Troll questions:** no patient-specific question appears before the reveal.
7. **Refused to hear:** do not start the downstairs lead. Reconsideration must acknowledge the refusal.
8. **Willing to hear:** grants first-visit lab access but does not alter `marlowJob`.
9. **Refused job:** troll reveal and lab visit remain remembered. Reconsideration does not replay them.

# Interruption and resumption specification
Implementation must preserve the last coherent conversational checkpoint, not simply the last text node.

## Before name entry
If interrupted before the player confirms a name, Garrick may resume the introduction and ask again. Do not invent a name.

## After name entry / before crash
`playerNameKnown = true`. On re-engagement, Garrick does not ask again. The crash event remains pending if it has not occurred.

## During Marlow/Garrick problem setup
Completed lines/facts remain learned. Resume from the next coherent beat rather than replaying **"last usable sample"** or the Watch exchange from the top.

## At hear-me-out decision
If no response was selected, return to **"Would you be willing to hear me out?"** and its four approved responses.

## After refusedToHear
Do not auto-resume the offer. Future Marlow interaction can use a short reconsideration opener while time remains.

## During downstairs lead
Do not replay the invitation/warning after access was granted. If player wanders away, the invitation remains valid unless upstream state deliberately revokes it.

## Pre-troll-reveal lab state
Talking to Marlow triggers the reveal guidance. Do not show unrelated generic lab dialogue.

## Post-reveal, pre-offer
Do not replay the visual reveal. Optional questions already answered should either disappear or become natural reminder variants where useful.

## At actual job decision
If interrupted before deciding, return to the real commitment gate **"Will you help me?"** after preserving any optional information already learned.

## After accepted
Do not ask for acceptance again. Allow reminders/briefing only.

## After refused
Do not erase refusal. Reconsideration should acknowledge it explicitly and offer a fresh accept/refuse decision without replaying the opening.

# Unity-ready dialogue/state specification boundaries
Once Central Brain gives final review, Unity should receive an implementation packet with:
- exact locked line order
- exact choice labels
- visibility conditions
- state writes for each choice
- state-dependent alternate lines
- Continue-vs-choice presentation rules
- interruption checkpoints
- physical staging triggers for crash, basement lead, and troll reveal
- explicit separation of `marlowInterest` and `marlowJob`

Unity must not improvise new dialogue, infer player interest from proximity, or convert ordinary Continue moments into fake roleplaying choices.

# Approved dialogue status

## LOCKED exact prose
- **"New face."**
- **"Garrick. I own the place."**
- **"You got a name?"**
- **"[NAME]. Right."**
- **"Bottle-Brain."** at the crash reaction
- **"I'm fine."**
- **"Wasn't asking."**
- **"...That's unfortunate."**
- **"How unfortunate?"**
- **"That was the last usable sample I had."**
- **"Can you make another?"**
- **"If I had fresh material."**
- **"Then go get it."**
- **"I can't leave him."**
- **"Thought you already put in a request."**
- **"I did."**
- **"Watch still sitting on it?"**
- **"They have more urgent matters."**
- **"Might've found you another pair of legs."**
- **"You don't know them."**
- **"Nope."**
- **"But they're looking for work."** / **"But they're standing right here."** under correct state
- **"That's not usually how I choose expedition partners."**
- **"Good thing you're not going."**
- **"That's rather the problem."**
- **"Then ask 'em."**
- **"Would you be willing to hear me out?"**
- response labels **"What do you need?" / "Does it pay?" / "Sure." / "No."**
- **"My laboratory's downstairs."**
- contextual **"The locked room?"**
- **"Yes. Different circumstances now."**
- **"You holler if they try anything funny."**
- **"This is why I can't leave."**
- **"He should be green."**
- **"He was drowning."**
- **"I think I can stabilize him."**
- **"But what I had left isn't enough. I need fresh material."**
- **"If you bring them back, I can prepare the treatment."**
- **"Will you help me?"**
- commitment labels **"I'll get them." / "No."**
- acceptance **"Thank you."**
- refusal **"All right."**

## VOICE POLISH OPEN
Minor connective Marlow wording, optional information answers, exact payment exchange, some contextual first-contact wording, and the optional Marlow/Garrick tail after the basement warning may still be polished while preserving approved logic and state.

# Remaining unresolved questions for Central Brain
1. Exact Marlow quest payment/economy value.
2. Whether the suggested **"Come here. I'll show you."** should be locked as the pre-reveal fallback line or polished further.
3. Whether the optional post-warning exchange **"I'm sure that won't be necessary." / "Didn't say it would."** is approved or should be omitted.
4. Exact optional answer wording for **"You keep a troll down here?"** and **"Is he dangerous?"** may receive final voice polish.
5. After Central Brain's final review, produce the exact Unity implementation packet. Do not modify Unity before that approval.
