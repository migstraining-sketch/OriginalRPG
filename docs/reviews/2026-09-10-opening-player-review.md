> Historical assessment. The [coordinated package review](2026-09-10-coordinated-package-review.md) supersedes this as the current review. Several concerns below were subsequently resolved; do not treat them as current defects without checking the owning authority.

# WoodlandSpine opening: player experience review

Date: 2026-09-10. Lens: [Asmongold-informed review standard](../PLAYER_EXPERIENCE_REVIEW_LENS.md). All verdicts and scores here are the reviewer's judgments, not Asmongold's statements or ratings.

**Overall: 7/10 for the opening's design direction; provisionally 4/10 for its implemented experience readiness.** The strongest ideas are creature behavior, terrain tactics, distinct weapons, and becoming useful to an inhabited inn. The largest gap is that several interactions prescribe steps instead of letting the player discover or perform something interesting. The prototype is worth continuing, but it has not earned more systems or polished production art yet.

These are holistic review judgments, not arithmetic averages or a consumer release review. The implementation score reflects observed gaps; a new natural playthrough is still required to establish enjoyment and pacing. Graybox visuals are assessed separately from the sound decision to use placeholders.

## Evidence and revision boundaries

- Reviewed main **f15fa40**, including dialogue revision **3052271** and the room-storage decision that arrived during this review. The playable code remains the **72d6411** implementation. Pulling design documents did not update the executable.
- Read the separate **design/regional-map-mvp** branch at **ee14f79** for travel and Mooncalf revisions. Those are branch documents, not merged main or implemented gameplay. Their self-described approval is recorded without silently promoting or merging them.
- Read core rules, opening flow, character, contract and profession authorities, implementation and validation notes; inspected opening, lab, hunting, cooking, potion, timer and reward code, plus saved combat/lab screenshots.
- Existing validation records 4,463 editor checks and 290 scripted opening assertions. No new Unity run or human playtest was performed for this review. Saved screenshots establish layout only; they do not prove mouse feel, audio quality, performance or dialogue pacing.
- Existing authority documents sometimes contain stale implementation observations. For example, name entry and hearing/acceptance separation are already implemented, while the newly rejected lab FAQ and return-report order remain in code. Judge each claim against the implementation rather than treating every old problem list as current.

## System scorecard

Design = promise of the current direction. Build = what the inspected prototype currently supports. Scores are provisional. NR = insufficient evidence or intentionally absent.

| Area | Design / Build | Verdict and reason |
|---|---:|---|
| Original identity | 8 / 6 | **Keep.** Local wildlife problems, a working inn, and tactics in the environment form a coherent identity. Strengthen these through play rather than adding familiar RPG features by habit. |
| Character entry and controller | 7 / 5 | **Keep simple; test feel.** Coat choice and in-conversation name entry fit the opening. Camera/movement responsiveness was not measured this turn. Expanded character customization can wait. |
| First minutes and pacing | 6 / 4 | **Change/test.** Control arrives early, but many conversational beats precede the first tactical encounter. Documented timings are targets, not measured first-player results. The hook need not be combat, but it must be felt. |
| Inn layout and social boundaries | 8 / 6 | **Keep.** Bar, private kitchen, stairs, board and lab communicate functions. Contextual objections beat a silent locked door. Repeated permission checks could still make a home feel like an administration office. |
| Garrick | 8 / 6 | **Keep.** Practical protection and the weapon loan earn interest. Do not make every line a punchline or every useful service require proving yourself again. |
| Marlow and troll reveal | 8 / 4 | **Change presentation.** The caring naturalist and visibly ill patient are strong. Current Questions() repeatedly returns to an FAQ; the rescue answer is attached to an ambiguous illness-sounding question. Latest main correctly rejects this. |
| Player dialogue agency | 8 / 5 | **Keep the state separation; change the menus.** Name, hearing, invitation and acceptance are distinct now. More buttons are not inherently more freedom. Preserve attitude and optional curiosity without interrogating every subject. |
| Sylvie and kitchen relationship | 8 / 5 | **Keep.** Expertise expressed through food and changing access is good progression. Repeated dismissal could become tiresome; trust should visibly change her behavior. |
| Exploration and ingredients | 7 / 4 | **Change.** Observation has potential, but the current briefing specifies locations and the exact peaceful approach. Keep useful identification information while leaving something to infer. Test for unreadable clue hunting as well as over-explanation. |
| Regional travel | 7 / NR | **Support with a reservation.** Stable geography and short routes are sensible. The branch's early-exit map can offer only Return, and acceptance unlocks destinations. That risks making refusal freedom nominal. See tradeoff below. |
| Weapon loan and equipment | 8 / 7 | **Keep.** A reversible first choice, two slots, compressed values and stored purchases are appropriate. Each weapon must support the whole opening without forced switching. |
| Sword / Lunge | 8 / 7 | **Keep.** Full-damage adjacency versus reduced-damage approach creates a understandable commitment decision. Test whether Lunge is chosen for a reason rather than simply whenever lit up. |
| Spear / Drive | 8 / 7 | **Keep; balance-test.** Spacing plus displacement creates a useful terrain identity. Equal basic damage and extra reach could dominate Sword if mobility has too little value. |
| Bow / Quick Shot | 8 / 6 | **Keep; mouse-test.** Ranged geometry plus weaker adjacent defense is coherent. Prior targeting failures were experience-breaking; code tests and an Enter fallback do not replace clicking every visible target reliably. |
| Core hex turns and damage | 8 / 7 | **Keep.** Splittable movement, one action, fixed damage and explicit intent make decisions legible. Complexity should come from situations. Do not add timing inputs just to resemble a different successful game. |
| Defend, Dash, Item and switching | 7 / 6 | **Keep; audit usage.** They should each solve a real situation. A permanently inferior action is clutter. Fixed mitigation allows useful previews; switching consuming the action has a clear cost. |
| First creature / Pounce | 8 / 6 | **Keep; test teaching.** A committed landing hex rewards a deliberate sidestep. Check whether two fast hits end the encounter before a newcomer sees the lesson; do not inflate HP automatically. |
| Mossback / Charge / obstacles | 9 / 7 | **Best mechanic; prioritize.** Baiting a committed charge into a tree creates a memorable success from understanding. It needs an unmistakable windup, impact and stagger, followed by a different situation to prove learning. |
| Hunting investigation | 8 / 3 | **Change urgently.** Current AllEvidence requires every one of four signs, then client approval and trail interaction before resolution. This is an enforced checklist despite the authority allowing redundant evidence. |
| Three contract identities | 8 / 5 | **Keep the distinctions.** Reedback feeding, Nightquill's elevated route and Brookmaw nursery ask different questions. Current contracts share a highly similar gate sequence and generic enemy stats. Their actual play must differ. |
| Nonlethal resolutions and harvest | 8 / 5 | **Keep; test incentives.** Altering habitat is more interesting than a morality button. Comparable rewards protect choice, but the peaceful route must not become the automatic cheapest click. Persistent creatures/wheel changes help. |
| Mooncalf interaction | 7 / 4 | **Revise carefully.** Current authored instructions reveal the solution. The branch's nursing herd and supplied flask improve causality. Three-actor defensive combat is a concrete capability expansion, not a tiny dialogue change. |
| Return and treatment order | 8 / 3 | **Change urgently.** Current Report() requires the Mossback interview before Workbench() permits treatment. Marlow also knows the incident without being told. Treat the patient first; make the report player-initiated under the latest authority. |
| Potion Making | 7 / 4 | **Keep one consequential introduction; simplify repetition.** Saving the troll and gaining usable healing matter. Exact prescribed preparation actions and three required stirs are a weak repeatable loop unless control/observation becomes meaningful. |
| Cooking and Well Fed | 7 / 3 | **Prove a benefit before expanding.** The intended sensory interaction is promising. Current cooking follows enum stages and numeric heat bands; Well Fed is a flag displayed in the HUD with no consuming gameplay effect found. |
| Economy, rooms and rewards | 7 / 4 | **Tune around useful choices.** Current 10 starting coins plus 8 payment reaches the 18-coin room. Armor 2 costs 16 and has a combat effect; room rental currently offers access without implemented rest benefit. This is not yet an interesting power-versus-comfort tradeoff. |
| Newly approved room storage | 8 / NR | **Support.** A persistent physical chest gives the room a concrete home-base purpose. The new main explicitly forbids restricting the backpack to force rental and protects stored items if expiry is ever added. Persistence and transfers are still unimplemented; organization must justify the trip upstairs. |
| Permanent progression | 8 / 5 | **Keep the principle.** Learned capabilities and changed relationships can matter. More unlock labels will not substitute for revisitable uses. Long-term permanence is not proven by this small slice. |
| Refusal, crime and consequences | 7 / 4 | **Keep agency; expose causal consequences.** Leaving/refusing must remain usable choices. Garrick's generated opponent uses 30 HP and 5 damage, which does not by itself substantiate the intended overwhelming difficulty. Tune actual encounters, not claims. |
| Illness clock | 4 / 2 | **Challenge the present implementation.** A 45-minute provisional timer starts at invitation and ticks in dialogue/combat as well as exploration. It can punish the curiosity the opening invites. Consequences need legible causality and fair timing. |
| HUD, targeting and camera | 7 / 5 | **Test before decoration.** Safe combat framing improved the old overlap. The saved 1280×800 view still has substantial panel area, small actors and multiple competing highlights. Make the selected action and impending danger immediately distinguishable. |
| Art and audio | 8 / 2 visual; NR audio | **Keep placeholders as the process.** Current geometry is not yet an emotionally persuasive troll or intimidating charge. Readable silhouettes, facing/poses, impacts and useful sound cues should precede custom asset production. Audio was not listened to here. |
| Save, recovery, accessibility, performance | NR / incomplete | **Add essential test support; measure.** No save/load exists. For repeated opening tests, checkpoint/restart support becomes valuable. Verify text size, non-color telegraphs, remapping needs and representative performance; no fabricated scores. |
| Monetization and later systems | NR / absent | **Defer.** No basis to rate an unchosen sales model or imaginary endgame. No Smithing expansion, profession trees or larger map until the opening earns them. |

## Highest-value changes

**1. Fix causal dialogue, not just prose.** The latest main revision is directionally right: shrink the lab root menu, let questions develop briefly, remove the rejected danger answer, and separate the player's encounter history from Marlow's knowledge. Ingredients arriving during a medical crisis should lead toward treatment, not a compulsory field interview. Preserve unasked topics and voluntary reporting. This recommendation agrees with new authority; it does not claim that authority is already implemented.

**2. Remove the mandatory four-clue checklist.** OpeningProgress.HuntProgress.Interpret() currently rejects anything short of AllEvidence; FullOpening also gates progress on that state. A minimal experiment is sufficient evidence from relevant redundant clues, with additional observations providing advantage or richer understanding. Two arbitrary clicks are not automatically a correct inference: test the actual clue combinations. Let the world and the animal confirm a hypothesis, rather than making the client grade every deduction. The next player should be able to explain the habitat solution before selecting it.

**3. Make the opening's promise arrive earlier.** The documented normal route places woodland combat around minutes 9–14, after several social/lab beats. That is a risk for the impatient player this lens represents, not proof that all openings require immediate combat. Keep the lab's emotional purpose. Measure how long before players make an interesting choice and whether they want to continue; remove redundant advances before cutting character context. A useful experiment is an engaging choice in the first few minutes and a brisk optional-question route to the expedition. Those are review targets, not new canon timings.

**4. Reconsider the clock before polishing its warning.** Current TickIllness() is called every frame except Defeated, with invitation as its start condition. Dialogue and tactical thinking count. Recommend testing an explicit, legible form of deterioration that does not tax reading or paused decisions; deliberate departures or meaningful world events could be candidates. Exact rules require a design decision. Do not silently delete the established possibility of failure. Do not keep an opaque clock merely because failure is supposed to matter.

**5. Prove one repeatable preparation activity.** The first brew has an excellent consequence: a creature improves and the player obtains a usable potion. The recipe procedure itself currently has little freedom. Repeating the same prescribed clicks does not demonstrate mastery. Cooking has an additional problem: the resulting status has no practical effect. Select one small useful Well Fed behavior through the existing authority, then test whether players voluntarily prepare again. Avoid building the entire sensory Cooking specification before this benefit and one interaction are convincing.

**6. Make tactical success obvious.** Keep Mossback's locked lane and terrain interaction. A player should see the charge intent, choose a route, watch the obstacle stop the beast, and recognize the lost enemy tempo. Improve placeholder motion, sound and selected-target feedback where they communicate that chain. The user's previous inability to click a target is the sort of concrete failure that outweighs otherwise strong rules. This review does not assert it still occurs after the fix; it remains a required natural mouse test.

## Tradeoffs worth challenging rather than rubber-stamping

- **Regional map:** The branch solves compressed geography cheaply. But an early exit that opens a map with nowhere to go satisfies the button-level definition of leaving while giving no playable alternative. Accept this as an honest MVP limit or later authorize a small meaningful outlet. Do not advertise unrestricted exploration, and do not solve it by adding a continent.
- **Herd and flask:** A supplied flask avoids a needless shopping trip; visible adults make defensive behavior understandable. A renewable nursing animal gives future visits a reason. Losing that resource through aggression can be a fair consequence if danger and acquisition logic are clear. Define what remains playable if the player destroys the only source; a meaningful failed quest differs from a silently stuck objective. Do not invent a replacement milk drop to erase the consequence.
- **Reward equivalence:** Keep lethal and nonlethal total value broadly comparable, but measure risk, time and resource costs. Fifteen coins for both outcomes does not guarantee equally attractive routes. Avoid making one option a strict improvement in every respect.
- **Armor versus room:** Current values make both individually affordable after Marlow, but the coat affects combat while the implemented room is mostly a space. New main now gives the room an approved persistent-storage purpose: support that small addition, retain optional rental and the unrestricted backpack, and verify stored equipment/materials survive travel and reload. Do not imply a nonexistent recovery benefit. Because inventory is already unrestricted, organization and a sense of ownership must provide the value; do not manufacture inconvenience to make the chest necessary.
- **Mystery:** Keep the unexplained Mossback behavior. An observable exception to established animal behavior is a stronger seed than an immediate evil glow or villain explanation. First ensure players actually understand normal behavior and notice the exception.

## Keep, remove, add, defer

**Keep:** Original setting; inn as home; restrained character humor; explicit commitment; compact equipment; fixed combat; three weapon identities; Pounce; Charge/tree/stagger; practical wildlife solutions; useful crafting outputs; comparable route value.

**Remove or replace:** Mandatory exhaustive clue collection; lab FAQ bouncing; compulsory Mossback report before treatment; NPC knowledge copied from world flags; repeated instructions that explain every discovery; purely ceremonial advances where there is no action or expression.

**Add first:** Better behavioral feedback for one tactical victory; sufficient-evidence Hunting; coherent conversation branches; a testable practical food benefit after design approval; checkpoint/restart support if repeated opening tests are costing time.

**Defer:** Final Blender art, expanded character creator, Smithing, more profession layers, additional regions, full herd simulation, large reward catalogues and elaborate UI styling.

## Smallest useful next production step

Implement the coordinated approved lab/return dialogue corrections, then run a fresh-player session through the first woodland return. Keep a separate short Hunting test for the evidence-gate change. Do not combine this with a large art pass.

Record time to first self-directed decision, first encounter and treatment; where players try to act but cannot; whether they can explain an enemy intent; whether the weapon choice feels useful; and what they believe Marlow knows. Ask an open question afterward: what would they choose to do next? Test a brisk player and a curious reader, not only someone following the intended path.

For Hunting, let a tester find an informative subset of clues and attempt a valid solution. For the timer, leave dialogue/inventory open and verify the chosen policy. For combat, click the actual actors near every edge and obstacle, then test a charge bait without coaching. For crafting, offer a second use voluntarily and observe whether its benefit justifies its interaction cost.

A higher review score should come from these observed improvements. Existing automated passes remain valuable regression evidence, but they cannot award the game a fun score.
