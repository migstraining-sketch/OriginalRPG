# Starter Hunting Contracts

Completing **any one** of these starter contracts formally unlocks Hunting. The other two remain available.

All three are local wildlife problems, not generic kill quests. Each should take roughly 5–8 minutes on first play and teach a different Hunting idea.

## 1. Mud in the Moonrice

**Client:** Toma Reed  
**Location:** Reedwater Paddies  
**Creature:** Reedback

### Board problem
Something repeatedly enters Toma's rice paddies, breaks fencing, flattens shoots, and destroys crop.

### Apparent explanation
A large animal is eating the rice.

### Actual explanation
The Reedback is primarily digging for **mudgrubs** beneath the flooded soil. It damages the rice while feeding, but the rice itself is not its main target.

### Locked contract outcomes
**Lethal:** Hunt/kill the Reedback, then manually Harvest it for **Fresh Reedback Haunch**.  
**Non-lethal:** Redirect the Reedback to a safer mudgrub feeding area away from the paddies. Toma rewards the player with **Preserved Reedback Cut** from an earlier legitimate regional cull.

Both are valid first-Hunting outcomes. Neither is the secret morally correct route and neither should receive a clearly superior tutorial reward.

### Implementation-ready investigation
Use **three primary evidence pieces** in and around the damaged paddy. They may be inspected in any order.

1. **Churned feeding patch** — a shallow muddy hollow among damaged rice. Several shoots are pushed aside or uprooted, but there is little clean bite damage. Inspecting it records that the crop appears to have been displaced while something dug underneath it.
2. **Exposed mudgrubs** — pale mudgrubs wriggle in freshly turned mud at the edge of one damaged patch. Inspecting them records that the digging exposed abundant prey/food beneath the rice bed.
3. **Broad Reedback tracks / entry route** — deep paired tracks enter through a damaged low section of bund/fencing, cross the worked mud, then angle away toward wetter ground. Inspecting them records the animal's size and gives the first readable outbound direction.

Do not add a field of clue nodes. Subtle nearby interaction assistance is enough.

### Interpretation logic
The required understanding is not tied to one magic clue.

- Any **two** primary evidence pieces are enough to record the working Inference: **the animal is digging through the crop to reach food beneath the mud.**
- Inspecting the exposed mudgrubs plus either other clue sharpens that observation to explicitly connect the feeding behavior to **mudgrubs**.
- All three clues provide the clearest read but are not mandatory for progression.

The game should phrase this as concise player observation/inference rather than an answer popup. Example internal note:

> Rice crushed and pushed aside, not cleanly eaten. Mudgrubs are exposed where the soil was turned. The animal is probably digging for the grubs beneath the crop.

A player who misses evidence may still locate and attack the Reedback. Missing the inference mainly means the non-lethal solution is less obvious/not yet enabled by understanding.

### Tracking sequence
From the paddies to the encounter area, use **three short tracking beats** rather than a direct objective arrow:

1. the broad outbound tracks leave the damaged paddy through the broken/low entry route;
2. a rubbed muddy flank mark on reeds/brush confirms the same large animal turned toward the wet margin;
3. fresh digging and newly exposed mudgrubs near the encounter clearing indicate the animal is close and still feeding.

Each beat should be readable from the previous area with ordinary environmental composition plus subtle contextual interaction help at close range. If the player wanders away, the last confirmed sign remains available in Hunt Notes/objective language so they can retrace rather than fail.

The player may physically find the encounter area without inspecting all three beats. Tracking is intended to teach reading a trail, not enforce invisible gates.

### Reedback encounter behavior
The Reedback is ordinary wildlife, not a bespoke boss.

Recommended MVP behavior:
- prefers staying near soft/wet ground while not engaged;
- begins the encounter feeding/digging rather than waiting in combat stance;
- if attacked, uses existing **Pursue** behavior plus a short readable **Rush** when it has a clear lane;
- Rush is a simple straight-line committed movement/attack using existing combat vocabulary, not a new system;
- after a Rush or when forced into close pressure, it returns to ordinary adjacent attack/pursuit behavior.

Keep durability in ordinary early-creature territory and tune during Unity implementation. Do not make it a Mossback-scale miniboss.

### Non-lethal redirection
The alternate feeding location is a muddy wet margin a short distance away from the rice beds. It already contains mudgrubs, but the surface is too compacted/shallowly crusted for easy feeding compared with the cultivated paddy.

Once the player has correctly inferred the mudgrub behavior, enable the contextual solution:

1. **Open/loosen the wet feeding patch** at one authored interaction point, exposing mudgrubs.
2. **Create a readable connection away from the crop** by opening the small muddy runnel/soft path between the Reedback's current feeding area and the alternate patch.

This is one compact environmental manipulation sequence, not a bait inventory or trapping system. The exposed grubs and disturbed mud provide the cause-and-effect cue.

When the Reedback detects the newly accessible feeding patch, it moves to investigate/feed there. The player then observes it settle into repeated digging away from the rice. That behavioral change, not merely the animal walking offscreen, marks the non-lethal wildlife problem as resolved.

### Temporary scare-off is not completion
Simply approaching, chasing, damaging, or frightening the Reedback until it runs away does **not** complete the contract. If the underlying feeding cause remains, the animal can circle back/leave new return signs after a short local reset. The objective remains to deal with the crop problem.

### Toma Reed state
Toma is practical and knows the damage pattern, not the ecological answer.

At first contact he can establish:
- something large keeps coming through the paddy;
- rice is being flattened/uprooted;
- he has seen enough of the animal to identify it as a Reedback if needed, but has not carefully studied why it is digging;
- simply chasing it off has not solved the problem.

Toma should **not** tell the player that mudgrubs are the answer or narrate the Hunting loop.

On lethal resolution, Toma accepts that the immediate crop threat is gone and acknowledges the completed work without moral celebration.

On non-lethal resolution, Toma recognizes that the animal is now feeding away from the crop and is relieved the practical problem is solved. He does not frame the player as morally superior.

For the non-lethal ingredient handoff, Toma gives the player **Preserved Reedback Cut** from an earlier lawful regional cull/food supply, preserving the established believable source.

### Completion and opening handoff
The contract wildlife problem becomes complete only when either:
- the Reedback is dead **and the player has manually harvested the Fresh Reedback Haunch**, or
- the Reedback has been meaningfully redirected and observed feeding at the alternate patch.

Completing either route formally unlocks **Hunting**. Entering the area, inspecting one clue, attacking the Reedback, or killing unrelated wildlife does not.

After resolution, the player leaves Reedwater Paddies through its travel boundary, returns through the Regional Map to Garrick's Inn, reports the work, and carries the route-appropriate edible ingredient into the existing Garrick → Sylvie handoff. Do not redesign Sylvie's sequence here.

### Sequence-break rules
- **Find Reedback early:** allowed. Player may observe or attack it before finishing investigation.
- **Attack immediately:** allowed. This commits to a possible lethal fight but does not auto-complete the contract.
- **Find mudgrubs before Toma:** allowed. Preserve the observation so later investigation can use it.
- **Solve with partial clues:** allowed once enough evidence supports the mudgrub inference.
- **Discover redirect, then kill:** allowed. Final route becomes lethal and requires Harvest.
- **Scare it away temporarily:** not completion; cause remains unresolved.
- **Reach alternate feeding patch early:** world interaction may be visible, but the meaningful redirect action should require the player-character to have enough evidence to understand why exposing mudgrubs there matters. Do not present a glowing 'Solve Quest' interaction without that knowledge.

### Contract-specific Unity acceptance tests
- Accepting the contract can reveal/make Reedwater Paddies selectable through the regional travel layer; arrival spawn is at the farm approach, not beside the first clue or Reedback.
- All three primary clues are interactable in flexible order.
- No single missed clue permanently blocks required progression.
- Any two appropriate clues can produce the basic feeding inference; exposed mudgrubs plus supporting evidence produces the explicit mudgrub inference.
- Player can reach/fight the Reedback without completing every clue/tracking beat.
- Tracking uses three readable signs and no giant direct-to-creature objective arrow.
- Scaring the Reedback away without solving the feeding cause does not complete the contract.
- Redirect resolution requires an alternate mudgrub feeding patch and results in observable feeding behavior away from the crop.
- Killing the Reedback does not complete the lethal route until manual Harvest produces Fresh Reedback Haunch.
- Non-lethal resolution gives Preserved Reedback Cut from Toma's existing food supply/earlier lawful cull, not from the living animal.
- Hunting unlocks once, after either valid wildlife-problem resolution, and not on entry/inspection/first attack.
- Both ingredient outcomes remain valid inputs to the existing Sylvie introduction.

### Persistent consequence
Paddies recover. If redirected, the same Reedback may later be seen feeding safely in the alternate wetland.

### Core Hunting lesson
**Read tracks and feeding signs. Understand what the animal is actually doing.**

### Approval status
The following were already locked before this implementation pass:
- client/location/creature
- Reedback is after mudgrubs rather than primarily eating rice
- lethal and non-lethal routes are both valid
- Fresh Reedback Haunch / Preserved Reedback Cut outcomes
- preserved cut comes from an earlier legitimate regional cull
- both routes feed Sylvie's Cooking introduction
- all three starter contracts remain equal canon choices

New implementation recommendations in this section:
- exact three-clue set and two-clue inference threshold
- exact three-beat tracking trail
- compact Reedback Pursue + readable short Rush combat identity
- authored alternate wet-margin feeding patch and two-interaction redirection sequence
- Toma's minimal knowledge/reaction state
- explicit sequence-break/completion tests

No new major system is required. These implementation recommendations may be tuned during Unity playtesting without changing the locked contract premise, provided Central Brain is informed of any material behavioral change.

---

## 2. Three Missing by Morning

**Client:** Mara Venn  
**Location:** Venn Homestead  
**Creatures:** Duskhen livestock + Nightquill predator

### Board problem
Three Duskhen have disappeared over several nights. Coop is shut. Little/no obvious blood outside. Ground signs are confusing.

### Apparent explanation
Some predator is somehow entering the coop from ground level.

### Actual explanation
A **Nightquill** uses an elevated glide route from an overhanging tree and enters through a loose roof vent. It barely touches the ground around the coop.

### Investigation language
Possible clues:
- intact coop door
- unusual quill/feather high in rafters
- disturbed roof material
- scratches near vent
- quills/claw marks on nearby tree
- elevated glide route

The player learns that tracks are not always footprints and that predator behavior must be read spatially.

### Resolution
**Lethal:** Hunt the Nightquill.  
**Non-lethal:** Secure the roof/access route and deter/remove the easy hunting path.

### Edible Sylvie outcome
Guaranteed: **Fresh Duskhen Eggs** from Mara as part of the reward.

The ingredient does not reveal whether the Nightquill was killed or deterred. If Sylvie reacts to that choice, the player must tell her what happened.

### Persistent consequence
Mara's flock remains safe and egg production returns to normal. A deterred Nightquill may later be observable deeper in the woods.

### Core Hunting lesson
**Deduction and predator behavior. Look above ground level and understand how the predator moves.**

---

## 3. When the Wheel Stopped

**Client:** Oren Vale  
**Location:** Vale Watermill  
**Creature:** Brookmaw

### Board problem
The mill wheel has repeatedly jammed despite Oren clearing the channel.

### Apparent explanation
"River vermin" keep dragging random debris into the wheel.

### Actual explanation
Brookmaws established a **nursery** in the protected mill channel because it is sheltered, slow-moving, warm, and rich in prey/grain runoff.

### Brookmaw biology
Brookmaws are amphibious fantasy wildlife with powerful paddle-shaped tails. They can naturally **shed and regrow their tails** after injury/territorial incidents.

### Investigation language
Possible clues:
- constructed nest debris rather than random driftwood
- gnawed/dragged branches
- aquatic tracks/disturbed sediment
- nursery material/young
- old blocked side-channel
- a naturally shed Brookmaw tail already caught in the nursery/grate debris

The shed tail should exist as part of the ecology/investigation, not appear only because the player chose a peaceful route.

### Resolution
**Lethal:** Fight/kill the Brookmaws and clear the nursery.  
**Non-lethal:** Restore a safer side-channel and relocate/redirect the nursery away from the wheel.

### Edible Sylvie outcome
- Kill route: **Fresh Brookmaw Tail**
- Relocation route: **Naturally Shed Brookmaw Tail** recovered during investigation

Sylvie verifies freshness/condition before using a shed tail.

### Persistent consequence
The mill wheel physically begins turning again and remains operational afterward. If relocated, Brookmaws can later be seen in the restored side-channel.

### Core Hunting lesson
**Read habitat, nesting behavior, and waterway signs. Sometimes solving the environment solves the animal problem.**

---

# Shared contract rules

- All three initial postings receive equal visual weight.
- No gold "main quest" contract.
- No ordering that implies one is the correct first choice.
- Each shows client, location, problem, and reward, not solution.
- Total expected early value should be broadly comparable.
- Each guarantees a distinct edible material path into Sylvie's introduction.
- Lethal and non-lethal resolutions should not create a superior/inferior tutorial reward path.
- Non-lethal choices should arise from understanding the situation, not GOOD/EVIL buttons.
