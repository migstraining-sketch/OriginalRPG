# Creatures / Monsters / Encounters

## Status / authority

**PROPOSED CREATURE / ENCOUNTER DESIGN PASS — AWAITING CENTRAL BRAIN APPROVAL.**

Central Brain remains final design authority.

This document owns creature and monster identity, ecology-facing behavior, roaming/presence logic, encounter setup and motivation, persistence/disappearance rules, physical transition from exploration into combat, and creature visual-development requirements.

It does **not** rewrite:
- combat-system rules or tuning;
- Hunting-system rules;
- quest canon;
- character canon;
- biome authority.

Upstream authorities remain:
- `START_HERE.md`
- `WORLD_BIOMES_EXPLORATION.md`
- `CORE_SYSTEMS_PROGRESSION.md`
- `GROUP_COMBAT_PARTY_MVP.md`
- `SYSTEMS_HUNTING.md`
- `OPENING_FLOW.md`
- `OPENING_MOONCALF_MILK_FAILURE.md`
- `HUNTING_PARTNERS.md`
- `DESIGN_GUARDRAILS.md`
- `COORDINATED_OPENING_DEVELOPMENT_SLICE.md`

**Do not modify Unity or Blender from this document.**

---

# 1. Core encounter doctrine

## Randomize ecology, not battles

The recommended project-wide encounter principle is:

> **Randomize ecology, not battles.**

The game should not primarily roll an invisible battle and then manufacture enemies around the player.

Instead, explorable locations contain believable **creature activity pockets** tied to habitat, routes, shelter, food, water, nesting/young, territory, and current world state.

On a visit, a pocket may contain:
- a plausible creature;
- a small plausible group;
- ambient life only;
- evidence that an animal recently passed through;
- or nothing dangerous at all.

Creatures physically exist in exploration before combat. The player may see, hear, approach, avoid, disturb, follow, frighten, threaten, or be noticed by them. Combat begins when creature behavior and player behavior actually produce a confrontation.

**“Nothing dangerous is here this visit” is a valid ecological result.**

This is intentionally lighter than a full ecosystem simulator. Small authored state machines, routes, habitat gates, and controlled variation are sufficient.

---

# 2. Creature taxonomy

Preserve the existing five ecology roles:

1. **Ambient harmless wildlife**
2. **Interactable harmless wildlife**
3. **Potentially defensive wildlife**
4. **Predators / hostile wildlife**
5. **Monsters / anomalous creatures**

These are behavior/ecology categories, not XP or difficulty tiers.

## Mythical does not mean anomalous

A creature may have unusual anatomy, coloration, horns, fur structures, or other fantasy-world traits and still be completely ordinary native wildlife.

A creature becomes anomalous when its nature, presence, or current behavior meaningfully violates what the world has established as normal for that creature or place.

This distinction is important in the opening:

**ordinary dangerous predator → peaceful/defensive Mooncalf herd → familiar creature behaving abnormally**

The player should learn this contrast through play rather than a taxonomy tutorial.

### Mossback clarification

The **Mossback species itself should not automatically be treated as an anomaly species.** Existing authority establishes that Mossbacks are normally docile if given space.

The **opening Mossback individual/encounter is anomalous** because it continues unexplained pursuit and aggression after ordinary explanations should have ended the confrontation.

No visible corruption marker is added.

---

# 3. First Woodland predator recommendation

## Recommend approval: Gloam Lynx

**Recommendation:** keep the working name **Gloam Lynx** and approve it as the opening Woodland's first ordinary hostile predator.

The concept already solves several needs at once:
- its large-cat anatomy naturally supports committed Pounce;
- stalking behavior gives the first fight a physical narrative reason;
- a solitary predator avoids turning the opening into an enemy pack tutorial;
- it can feel mythical without reading as magical corruption;
- its behavior creates a clean contrast with the Mooncalf herd and anomalous Mossback later.

### Ecology role

**Predator / hostile wildlife.**

The Gloam Lynx is a normal native solitary predator of the Woodland. It is not corrupted, controlled, summoned, or connected to the larger antagonist plot.

### Approximate size

Cougar-scale to slightly larger: a powerful medium-to-large quadruped rather than a boss-sized monster.

Exact model measurements remain visual-development tuning.

### Habitat association

Mixed-canopy interior, broken sightlines, fallen timber, rocky/rooted rises, and edge routes that let it observe prey before committing.

It should plausibly hunt medium woodland animals and use cover rather than standing in open clearings waiting for a player.

### Normal behavior

- solitary;
- rests or patrols within a hunting range;
- uses cover and lateral movement;
- watches before attacking;
- may shadow a potential target for a short distance;
- commits once it decides the target is viable prey or a territorial threat;
- does not fight to the death merely because it exists, unless combat/outcome authority later requires that for the authored opening encounter.

### Physical direction

Keep:
- low stalking body;
- powerful spring-loaded hindquarters;
- strong forepaws;
- long tufted ears;
- expressive ears, tail, shoulders, and spine;
- dark mottled woodland coat.

Optional fantasy anatomy should stay subtle. Small backward brow spurs or keratin/bony brow ridges may be explored during visual development, but **large horns should be avoided** because they weaken the predator silhouette and push the animal toward boss-monster language.

Avoid:
- glowing runes;
- magical particle effects;
- spellcasting;
- breath weapons;
- demonic/corruption motifs;
- giant raid-boss scale.

Ordinary low-light eye-shine is acceptable if it reads as animal eye-shine rather than supernatural glowing eyes.

### Pounce readability

The creature's body must communicate the existing committed Pounce before UI reinforcement is considered.

Recommended physical tell:

**head/body lower → shoulders settle → hindquarters compress/load → ears flatten or angle back → tail stiffens or changes rhythm → launch**

The exact timing, threatened geometry, damage, and resolution remain Combat authority.

Once Pounce is committed, the existing combat commitment rule remains untouched.

---

# 4. Opening stalking-to-combat sequence

The first Woodland fight should no longer be:

**enter Woodland → unexplained enemy combat**

Recommended authored sequence:

## Beat 1 — calm trailhead

The player arrives at the Woodland trailhead and receives the already-approved quiet orientation stretch.

Ambient birds/insects and ordinary woodland sound establish a baseline before any threat cue.

No combat UI appears.

## Beat 2 — first disturbance

After the player moves deeper into mixed canopy, one environmental cue suggests something large moved nearby.

Good candidates:
- brush movement behind cover;
- a branch settling after weight left it;
- a short rustle that stops when the player turns;
- nearby birds abruptly scattering from one side of the route.

Do not guarantee every cue in one playthrough.

## Beat 3 — confirmation without a checklist

A second cue confirms that the first was not merely ambience.

Possible authored variants:
- the lynx's silhouette crosses a distant gap;
- ear tufts or tail vanish behind a trunk;
- ordinary eye-shine catches briefly through brush;
- a recent scrape or prey remnant is visible if the player happens to look near the route.

The player receives no `Investigate 0/3` objective and no Hunting unlock/tutorial.

**Recommended cue budget:** surface at least two readable stalking cues before the normal confrontation, with an optional third depending on route and attention. The player does not need to click them.

## Beat 4 — the predator shadows the route

The Gloam Lynx uses one of a few small authored stalking paths around the player-facing route rather than teleporting between cue points.

It may:
- pause behind cover when watched;
- circle to another authored observation point;
- remain outside immediate melee range;
- abandon one approach route and use another if the player moves unexpectedly.

This is exploration behavior, not a combat Pounce commitment.

The player should increasingly understand:

> **Something is following me.**

## Beat 5 — confrontation pocket

The stalking paths should naturally terminate near one of a small number of spaces that are already suitable for the existing projected hex combat.

The lynx physically enters or reveals itself at the edge of that space. It does not pop into formation from nothing.

If the player spots and aggressively approaches the animal earlier, combat may begin from the physically valid current area/positions rather than teleporting both actors back to a preferred arena.

If the player has not forced an earlier confrontation, the lynx eventually decides the lone traveler is viable prey or a territorial threat and commits to the encounter.

## Beat 6 — combat begins before unavoidable damage

The first encounter should **not** use the stalking sequence as permission for a free unavoidable opening hit.

When the creature transitions into a clearly committed attack, combat begins with the creature visible and its dangerous intent readable. Existing Combat authority then owns turn order, start-state handling, Pounce commitment, movement, damage, and counterplay.

### Retreat / avoidance question

The broader ecology doctrine supports avoidance, but the opening still needs to prove the committed Pounce encounter.

Central Brain should decide whether the first-visit Gloam Lynx can be **fully avoided** by retreating or taking another route, or whether the initial Marlow expedition authors a confrontation somewhere on the required deeper route.

Regardless of that decision:
- do not teleport the lynx behind the player;
- do not respawn it instantly after disengagement;
- do not make it cross obviously impossible space to force combat.

---

# 5. Lightweight roaming / variable-encounter doctrine

## Creature activity pockets

Each local exploration map may contain a small number of authored creature activity pockets.

A pocket may define:
- habitat tags/requirements;
- plausible creature families;
- authored patrol/forage/rest routes;
- feeding, drinking, nesting, lookout, or shelter anchors;
- physical entry/exit points;
- nearby combat-capable spaces where relevant;
- world-state conditions that enable or suppress presence.

These are **not visible glowing spawn nodes**.

## Presence selection

On location entry or another meaningful world refresh, each eligible pocket chooses from a small authored state set rather than a giant random table.

Examples:
- empty;
- ambient-only;
- animal resting;
- animal feeding;
- animal moving between anchors;
- predator watching/stalking;
- recent signs but animal already gone.

Story-critical groups do not use this random presence layer unless their owning authority explicitly permits it.

## Small behavior state machines

Ordinary creature behavior only needs a compact set of states appropriate to that species, for example:

**Rest → Feed/Forage → Travel → Watch/React → Flee / Defend / Stalk / Confront**

Not every species needs every state.

The goal is believable authored behavior, not simulation for simulation's sake.

## Physical movement rule

Creatures should enter and leave through understandable world space.

When a creature needs to disappear for performance or state cleanup, prefer doing so after it:
- reaches cover;
- exits through an authored boundary;
- moves sufficiently far out of player view;
- or the player leaves/reloads the location.

Avoid animals vanishing in clear view merely because an AI timer expired.

## Combat transition rule

A roaming creature does not spawn directly into combat formation.

When confrontation occurs:
1. the creature already exists in exploration;
2. the player and creature have physical world positions;
3. the projected combat space uses sensible nearby valid starting positions according to Combat authority;
4. other physically present eligible participants follow existing group-combat rules.

No absent companion or enemy materializes just because combat started.

## Variation without battle roulette

Useful variables include:
- presence/absence;
- small count variation where ecologically plausible;
- current activity;
- route/anchor choice;
- which side of a pocket the creature enters from;
- whether it notices the player first;
- current authored world state.

Do not use this doctrine to randomize combat stats, damage, initiative, loot quality, or other systems owned elsewhere.

## Quiet visits are intentional

Not every return trip needs a fight.

A player may revisit the Woodland and find:
- harmless life;
- a predator's signs but no predator;
- a grazer that flees before contact;
- an empty hunting pocket;
- a different ordinary creature using the same habitat.

This is a feature, not failed content delivery.

---

# 6. Persistence / disappearance classes

## A. Ordinary renewable wildlife presence

Examples: ordinary small animals, grazers, and later Gloam Lynx presence on non-story visits.

Recommended rule:
- if killed, frightened away, or driven from its pocket, that individual/group remains gone for the current visit;
- later ecologically plausible presence may return after a **meaningful refresh**;
- a later visit does not need to reproduce the same individual, count, or activity;
- immediate behind-the-player respawn is forbidden.

Exact repopulation timing is deferred.

**Recommended minimum anti-reroll rule:** leaving and immediately stepping back into a location should not automatically reroll all wildlife. Preserve a visit/world-state cache until a meaningful refresh condition occurs.

The exact definition of that refresh condition requires later Central Brain / implementation approval.

## B. Authored persistent creature groups

Example: **Mooncalf herd**.

These use explicit world-state rules rather than ordinary visit randomization.

The Mooncalf herd must preserve the existing nursing-Mooncow reagent logic and failure authority. It cannot be randomized out of existence merely because a location revisit rolled `empty`.

Temporary flight is not the same as permanent loss. If the herd can recoverably return, existing Mooncalf Milk logic remains viable.

## C. Unique / anomalous encounters

Example: **opening Mossback individual**.

Unique/anomalous encounters use explicit authored state.

They are not ordinary renewable combat spawns and should not casually respawn after defeat or disappear because a general wildlife table rerolled.

---

# 7. Proposed opening Woodland creature roster

The goal is a small coherent roster, not an enemy catalogue.

## 1. Gloam Lynx — proposed current-slice predator

- **Ecology role:** Predator / hostile wildlife
- **Approximate size:** Cougar-scale to slightly larger
- **Habitat:** Mixed-canopy interior, fallen timber, rooted/rocky cover, woodland edges
- **Normal behavior:** Solitary stalking predator; watches, circles, then commits when prey/threat assessment favors attack
- **Player interactions:** Hear/spot it, notice signs, retreat, approach, disturb, be stalked, potentially fight
- **Combat:** Likely/possible; intended first committed-Pounce teaching creature if approved
- **Memorable visual trait:** Long ear tufts plus a low dark mottled silhouette with visibly powerful hindquarters
- **Scope:** **Recommend current Woodland slice**, but identity/implementation awaits Central Brain approval

## 2. Mooncalf / Mooncow / Moonbull family — existing locked wildlife

- **Ecology role:** Potentially defensive wildlife
- **Approximate size:** Juvenile plus large ungulate-like adults; exact 3D scale remains visual tuning
- **Habitat:** Woodland grazing/family area with open ground, cover, and access to forage/water
- **Normal behavior:** Graze, nurse, remain socially aware; adults protect the juvenile if seriously threatened
- **Player interactions:** Observe family behavior, identify nursing relationship, collect Mooncalf Milk from viable nursing Mooncow with field flask, provoke or threaten
- **Combat:** Possible if player aggression/serious threat causes adults to defend the calf
- **Memorable visual trait:** Visual design must clearly distinguish juvenile, nursing Mooncow, and protective adult roles without relying entirely on UI labels
- **Scope:** **Existing locked current Woodland content**

## 3. Mossback — existing opening anomalous individual

- **Ecology role:** Species baseline is ordinary wildlife; opening individual functions as Monster / anomalous encounter because of abnormal behavior
- **Approximate size:** Large, heavy quadruped; exact species scale remains visual-development work
- **Habitat:** Woodland interior/clearing routes with trees, rocks, and lanes that can support existing Charge interaction
- **Normal behavior:** Mossbacks are expected to be docile if given adequate space
- **Player interactions:** Give space, observe that pursuit continues abnormally, fight if confrontation persists, optionally report incident to Marlow later
- **Combat:** Yes on the continuing successful opening route under existing authority
- **Memorable visual trait:** Broad moss-draped or moss-like dorsal mass should communicate weight and straight-line momentum; final anatomy is not settled here
- **Scope:** **Existing locked current Woodland content**

## 4. Rootmuzzle — proposed identity for the existing small-forager observation

- **Ecology role:** Interactable harmless wildlife
- **Approximate size:** Large shrew / small rabbit scale, roughly 25–35 cm body length
- **Habitat:** Soft soil near roots, fallen timber, or a wet-margin burrow
- **Normal behavior:** Briefly emerges to nose through leaf litter/soft soil for grubs and seeds; freezes at distant movement and retreats if approached quickly
- **Player interactions:** Patient observation, cautious approach, watch feeding behavior; rushing close causes it to hide
- **Combat:** No current combat role
- **Memorable visual trait:** Broad flexible digging snout and oversized soil-darkened forepaws
- **Scope:** **Candidate current-slice identity for the already-locked small harmless forager observation**; naming/appearance requires Central Brain approval and does not add a sixth optional discovery

## 5. Ashwing Thrush — proposed ambient bird family

- **Ecology role:** Ambient harmless wildlife
- **Approximate size:** Small woodland songbird
- **Habitat:** Canopy edges, understory branches, streamside shrubs, fallen timber pockets
- **Normal behavior:** Calls, hops/feeds, moves in loose pairs or tiny groups, scatters rapidly from larger approaching animals
- **Player interactions:** Primarily sight/sound observation; not a targetable actor for MVP
- **Combat:** No
- **Memorable visual trait:** Ash-grey upper wings with a warmer underside/tail flash visible when the bird bursts from cover
- **Scope:** **Recommend inexpensive current-slice ambient dressing**, especially because flock scatter can support the Gloam Lynx stalking language without a special effect

## 6. Fernhorn Roe — proposed future harmless browser

- **Ecology role:** Interactable harmless wildlife / visible browser
- **Approximate size:** Small deer/goat scale
- **Habitat:** Broken-canopy clearings, bramble edges, open woodland transitions
- **Normal behavior:** Browses low vegetation, keeps distance, bolts into cover if approached too aggressively, may stop farther away to watch
- **Player interactions:** Spot from distance, approach cautiously, watch browsing/flee behavior
- **Combat:** Not intended as normal combat content; defensive combat should not be added merely because it has an HP-capable model
- **Memorable visual trait:** Short flattened antler/keratin growths with a fern-frond silhouette rather than giant fantasy horns
- **Scope:** **Future dressing**, not required for the coordinated opening slice

## 7. Runnelback — proposed future wet-margin defensive animal

- **Ecology role:** Potentially defensive wildlife
- **Approximate size:** Medium dog / large badger scale
- **Habitat:** Shallow stream margins, root shelves, muddy banks, reeds/sedges
- **Normal behavior:** Feeds and rests near water, slips into the stream when given room, warns when cornered or approached near a resting site
- **Player interactions:** Observe at water, frighten it into retreat, potentially crowd/provoke it
- **Combat:** Possible in future if cornered/pressed; **not** intended as an opening combat requirement
- **Memorable visual trait:** Broad paddle-like tail and low overlapping dorsal plates that shed water, fantastical but still animal-functional
- **Scope:** **Future dressing / later revisit candidate**, not required for the coordinated opening slice

### Roster scope discipline

The current locked Woodland ecology package already specifies **exactly five optional authored discoveries** for the coordinated pass.

Therefore this roster must **not** silently add several new current-slice interactables.

Recommended current-slice treatment if Central Brain approves the identities:
- Gloam Lynx = fills the already-required unnamed first hostile wildlife role;
- Rootmuzzle = names/fleshes out the already-required small forager observation;
- Ashwing Thrush = inexpensive ambient biodiversity/danger cue, not a new collectible/checklist interaction;
- Mooncalf family and Mossback = existing required content;
- Fernhorn Roe and Runnelback = future dressing only.

---

# 8. Encounter variation examples for later Woodland revisits

These are examples of the doctrine, not locked encounter tables.

A mixed-canopy predator pocket might resolve as:
- empty but with old scratch/prey signs;
- Ashwing birds feeding normally;
- a Fernhorn crossing through and fleeing;
- a Gloam Lynx resting out of the obvious route;
- a Gloam Lynx actively stalking another animal and potentially noticing the player;
- no dangerous creature at all.

A wet-margin pocket might resolve as:
- insects/bird activity only;
- Rootmuzzle briefly foraging;
- Runnelback resting near the bank in a later build;
- recent tracks with the animal already gone.

The player should learn habitat associations without learning a slot-machine schedule.

---

# 9. Creature visual-development requirements

Final art/modeling is deliberately deferred until ecology and encounter identity are approved.

Project style target remains:

**stylized dark-fantasy anime key art translated into 3D**

Creature visual development should prioritize, in order:
1. readable silhouette;
2. anatomy that supports behavior;
3. body language readable at gameplay camera distance;
4. habitat-fit materials/colors;
5. one or two memorable fantasy traits;
6. only then secondary ornament.

A normal mythical animal should look designed enough to belong in the setting without looking like a raid boss.

### Animation-first readability

For any combat-capable creature, concept/model sheets should eventually include behavioral poses, not just neutral turnarounds.

For the Gloam Lynx specifically, future visual development needs:
- neutral walk/trot silhouette;
- low stalk;
- watch/freeze posture;
- warning/confrontation posture;
- Pounce load pose;
- launch silhouette;
- recovery/landing posture;
- flee/disengage posture if supported.

For Mooncalf adults, future visual work should clearly communicate calm, alert, warning, protective interposition, and aggression without requiring a UI state label.

For Mossback, future visual work must make ordinary docility plausible **and** allow the abnormal pursuit/Charge encounter to feel disturbing because of behavior rather than corruption VFX.

---

# 10. Decisions requiring Central Brain approval

1. **Approve or reject `Gloam Lynx` as the canonical first ordinary Woodland predator name/identity.**
2. **Approve the first-predator anatomy guardrail:** large-cat/cougar scale, subtle fantasy traits, no overt magical/corruption language.
3. **Approve the stalking sequence as the replacement for immediate unexplained Woodland combat.**
4. **Decide whether the first-visit Gloam Lynx can be fully avoided/escaped before combat, or whether the initial Marlow route guarantees a confrontation somewhere deeper in the required Woodland route.**
5. **Approve `Randomize ecology, not battles` as the encounter doctrine.**
6. **Approve the activity-pocket + small authored state-machine model for roaming wildlife.**
7. **Approve the persistence split:** ordinary renewable presence vs authored persistent groups vs unique/anomalous encounters.
8. **Approve the anti-reroll direction that immediate location re-entry does not reroll all wildlife; exact meaningful-refresh timing can remain deferred.**
9. **Approve, revise, or reject `Rootmuzzle` as the identity of the already-locked small harmless forager observation.**
10. **Approve, revise, or reject `Ashwing Thrush` as inexpensive ambient Woodland life and a natural stalking cue.**
11. **Decide whether `Fernhorn Roe` and `Runnelback` are useful future Woodland species or should remain unnamed ecological placeholders.**
12. **Approve the Mossback taxonomy clarification:** ordinary species baseline, anomalous opening individual/behavior.

---

# 11. Contradiction / authority review

## No material contradiction found

The requested authority package is broadly consistent with this proposal:
- `WORLD_BIOMES_EXPLORATION.md` already requires ordinary hostile Woodland wildlife supporting committed Pounce, a peaceful/defensive Mooncalf herd, and an abnormally aggressive Mossback;
- `OPENING_FLOW.md` already requires a quiet Woodland arrival before danger and identifies the first ordinary hostile creature as the likely first combat;
- `CORE_SYSTEMS_PROGRESSION.md` and `GROUP_COMBAT_PARTY_MVP.md` already own the committed-Pounce combat rule and physical participant/start-position logic;
- `SYSTEMS_HUNTING.md` explicitly supports creatures physically existing in locations instead of spawning only when progress counters are satisfied;
- `OPENING_MOONCALF_MILK_FAILURE.md` already distinguishes temporary herd flight from genuine permanent loss;
- `DESIGN_GUARDRAILS.md` strongly supports world-first information, physical consequences, player agency, no invisible battle treadmill, and no tutorial-checklist version of the stalking sequence.

## Scope tension resolved conservatively

`WORLD_BIOMES_EXPLORATION.md` says the coordinated pass should implement **exactly five optional authored Woodland discoveries**. A larger creature roster could accidentally expand that scope.

This proposal avoids that contradiction by:
- using Gloam Lynx to fill an already-required hostile-wildlife slot;
- using Rootmuzzle only as a proposed identity for the already-required small-forager observation;
- using Ashwing Thrush as ambient non-interactive dressing;
- keeping Fernhorn Roe and Runnelback as future dressing rather than coordinated-slice requirements.

## Implementation hold preserved

`START_HERE.md` currently states that gameplay coding is on hold pending review/separate authorization. This document is documentation/design only and must not be treated as permission to modify Unity or Blender.
