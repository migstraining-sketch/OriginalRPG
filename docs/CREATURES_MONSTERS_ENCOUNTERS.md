# Creatures / Monsters / Encounters

## Status / authority

**LOCKED / CURRENT CREATURE AND ENCOUNTER AUTHORITY FOR THE OPENING WOODLAND**, with clearly marked future/deferred items remaining uncommitted.

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

**Do not modify Unity or Blender from this document alone.** Central Brain must separately authorize implementation.

---

# 1. Core encounter doctrine

## Randomize ecology, not battles — LOCKED

Project-wide encounter principle:

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

The exact population-selection algorithm is an implementation detail, not creature canon.

---

# 2. Creature taxonomy

Preserve the existing five ecology roles:

1. **Ambient harmless wildlife**
2. **Interactable harmless wildlife**
3. **Potentially defensive wildlife**
4. **Predators / hostile wildlife**
5. **Monsters / anomalous creatures**

These are behavior/ecology categories, not XP or difficulty tiers.

## Mythical does not mean anomalous — LOCKED

A creature may have unusual anatomy, coloration, horns, fur structures, or other fantasy-world traits and still be completely ordinary native wildlife.

A creature becomes anomalous when its nature, presence, or current behavior meaningfully violates what the world has established as normal for that creature or place.

The opening communicates this through play:

**Gloam Lynx: fantasy wildlife can simply be dangerous.**

**Mooncalf herd: fantasy wildlife can be peaceful and become defensive when given reason.**

**Opening Mossback: something familiar is behaving wrong.**

Do not explain this philosophy through a taxonomy tutorial.

### Mossback clarification — LOCKED

**Mossbacks are not inherently an anomalous species.**

Marlow's knowledge establishes a normal behavioral baseline: Mossbacks are expected to be docile if given adequate space.

The **opening Mossback individual is anomalous** because it continues aggressive pursuit when a normal Mossback would be expected to disengage if given space.

Do not explain the cause yet. No visible corruption marker is added.

---

# 3. First Woodland predator: Gloam Lynx

## APPROVED AND LOCKED

**Name:** Gloam Lynx

The Gloam Lynx is the opening Woodland's first ordinary hostile predator.

It is:
- normal native mythical wildlife;
- solitary;
- large-cat / cougar-scale;
- dangerous because it is a predator;
- not corrupted;
- not controlled or summoned by an antagonist;
- not connected to the Mossback anomaly;
- not connected to a quest reagent.

Its identity deliberately establishes that not everything attacking the player is evil, corrupted, or part of the larger plot.

### Ecology role

**Predator / hostile wildlife.**

### Approximate size

Cougar-scale to slightly larger: a powerful medium-to-large quadruped rather than a boss-sized monster.

Exact model measurements remain visual-development tuning.

### Habitat association

Mixed-canopy interior, broken sightlines, fallen timber, rocky/rooted rises, and edge routes that let it observe prey before committing.

It should plausibly hunt medium woodland animals and use cover rather than standing in an open clearing waiting for the player.

### Normal behavior

- solitary;
- rests or patrols within a hunting range;
- uses cover and lateral movement;
- watches before attacking;
- may shadow a potential target for a short distance;
- commits once it decides the target is viable prey or a territorial threat.

Exact in-combat defeat, disengagement, and Flee resolution remain owned by Combat authority.

### Physical direction — LOCKED

Preserve:
- low stalking body;
- powerful spring-loaded hindquarters;
- strong forepaws;
- tufted ears;
- expressive ears, tail, shoulders, and spine;
- dark mottled woodland coat.

A subtle horn/brow feature may be explored only if it preserves the feline silhouette. Avoid large horns or ornament that pushes the animal toward boss-monster language.

Avoid:
- glowing runes;
- magical particle effects;
- spellcasting;
- breath weapons;
- demonic/corruption motifs;
- giant raid-boss scale.

Ordinary low-light eye-shine is acceptable if it reads as animal eye-shine rather than supernatural glowing eyes.

### Pounce readability — LOCKED DIRECTION

The creature's body must communicate the existing committed Pounce before UI reinforcement is considered.

Physical tell:

**body/head lower → hindquarters compress/load → shoulders settle → ears/tail/posture change → launch**

Combat UI may reinforce the threat according to Combat/UI authority, but it should not invent a tell the creature's animation fails to communicate.

Exact timing, threatened geometry, damage, and resolution remain Combat authority.

Once Pounce is committed, the existing combat commitment rule remains untouched.

---

# 4. First Marlow Woodland encounter

## Authored encounter, behavior-driven confrontation — LOCKED

The opening sequence is:

**quiet trailhead → stalking cues → physical stalking → confrontation → committed Pounce**

The encounter is authored, but combat is **not** launched by an arbitrary invisible tutorial gate.

The fiction is doing the forcing: a hungry solitary predator has decided the lone traveler is worth following.

## Beat 1 — quiet trailhead

The player arrives at the Woodland trailhead and receives the already-approved calm orientation stretch.

Ambient birds/insects and ordinary woodland sound establish a baseline before any threat cue.

No combat UI appears merely because the player entered the location.

## Beat 2 — first readable cue

After the player moves deeper into plausible hunting territory, one environmental cue suggests something substantial moved nearby.

Valid cues include:
- brush movement behind cover;
- a branch settling after weight left it;
- a short rustle that stops when the player turns;
- nearby birds abruptly scattering from one side of the route;
- other equivalent physical evidence.

## Beat 3 — confirmation without a checklist

A second cue confirms the first was not merely ambience.

Possible cues include:
- the lynx's silhouette crossing a distant gap;
- ear tufts or tail vanishing behind a trunk;
- ordinary eye-shine through brush;
- old prey remains;
- territorial scratches;
- another physically plausible sign.

**Guarantee approximately two readable stalking cues before the normal confrontation.** A possible additional cue may appear depending on route and player attention.

The cues do not need to be clicked.

Do not use:
- `Investigate 0/3` or another clue counter;
- Hunting UI/tutorial progression;
- quest markers pointing at the predator;
- a scripted horror-chase sequence.

## Beat 4 — physical stalking

The Gloam Lynx uses a few small authored stalking routes around the player-facing route rather than teleporting between cue points.

It may:
- pause behind cover when watched;
- circle toward another authored observation point;
- remain outside immediate melee range;
- use another physically connected stalking route when player movement changes.

This is exploration behavior, not a combat Pounce commitment.

The player should increasingly understand:

> **Something is following me.**

## Beat 5 — agency before confrontation

Before the Gloam Lynx commits to confrontation, the player may **retreat and leave the Woodland**.

That is a valid exercise of agency. The game does not place an invisible wall behind the player or instantly teleport the lynx into combat to prevent departure.

However, if the player **continues deeper through the area while the Gloam Lynx remains actively committed to stalking them**, the predator eventually confronts them and ordinary combat begins.

For this opening MVP, the player does **not** permanently bypass the entire first-predator encounter merely by walking around one trigger volume or taking a few steps around the expected confrontation spot.

The encounter's persistence comes from the predator continuing to stalk through authored world routes, not from a tutorial volume silently firing somewhere else.

## Beat 6 — confrontation pocket

The stalking paths naturally lead toward one of a small number of spaces suitable for the existing projected hex combat.

The lynx physically enters or reveals itself at the edge of that space. It does not appear directly in combat formation from nothing.

If the player aggressively approaches the animal earlier and produces a physically valid confrontation, combat may begin from sensible current positions according to Combat authority rather than teleporting both actors into a preferred arena.

## Beat 7 — combat begins before unavoidable damage

The stalking sequence does **not** grant the lynx a free unavoidable attack.

When the predator commits to confrontation, combat begins with the creature physically present and its dangerous intent readable.

Existing Combat authority then owns:
- start-state handling;
- turn/activation logic;
- Pounce commitment;
- movement;
- damage;
- counterplay;
- ordinary Flee rules where legal.

This preserves a reliable first Pounce lesson without presenting the fight as “tutorial battle required.”

---

# 5. Lightweight roaming / variable-encounter doctrine

## Creature activity pockets — LOCKED MVP ARCHITECTURE

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

### Presence selection

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

The exact selection algorithm and weighting are implementation details.

### Small behavior state machines

Ordinary creature behavior only needs a compact set of states appropriate to that species, for example:

**Rest → Feed/Forage → Travel → Watch/React → Flee / Defend / Stalk / Confront**

Not every species needs every state.

The goal is believable authored behavior, not simulation for simulation's sake.

### Physical movement rule

Creatures should enter and leave through understandable world space.

When a creature needs to disappear for performance or state cleanup, prefer doing so after it:
- reaches cover;
- exits through an authored boundary;
- moves sufficiently far out of player view;
- or the player leaves/reloads the location under appropriate state rules.

Avoid animals vanishing in clear view merely because an AI timer expired.

### Combat transition rule

A roaming creature does not spawn directly into combat formation.

When confrontation occurs:
1. the creature already exists in exploration;
2. the player and creature have physical world positions;
3. the projected combat space uses sensible nearby valid starting positions according to Combat authority;
4. other physically present eligible participants follow existing group-combat rules.

No absent companion or enemy materializes merely because combat started.

### Variation without battle roulette

Useful variables include:
- presence/absence;
- small count variation where ecologically plausible;
- current activity;
- route/anchor choice;
- which side of a pocket the creature enters from;
- whether it notices the player first;
- current authored world state.

Do not use this doctrine to randomize combat stats, damage, initiative, loot quality, or other systems owned elsewhere.

### Quiet visits are intentional

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

## A. Ordinary renewable wildlife presence — LOCKED

Examples: ordinary small animals, grazers, and later Gloam Lynx presence on non-story visits.

Rule:
- if killed, frightened away, or driven from its pocket, that individual/group remains gone for the current visit;
- later ecologically plausible presence may return after a **meaningful refresh**;
- a later visit does not need to reproduce the same individual, count, or activity;
- immediate behind-the-player respawn is forbidden.

### Immediate re-entry anti-reroll rule — LOCKED PRINCIPLE

Leaving and immediately stepping back into a location does **not** automatically reroll the area's wildlife population.

Preserve appropriate location/world-state continuity until a meaningful refresh condition occurs.

**Exact meaningful-refresh timing remains deferred.**

## B. Authored persistent creature groups — LOCKED

Example: **Mooncalf herd**.

These use explicit world-state rules rather than ordinary visit randomization.

The Mooncalf herd must preserve the existing nursing-Mooncow reagent logic and failure authority. It cannot be randomized out of existence merely because a location revisit rolled `empty`.

Temporary flight is not the same as permanent loss. If the herd can recoverably return, existing Mooncalf Milk logic remains viable.

## C. Unique / anomalous encounters — LOCKED

Example: **opening Mossback individual**.

Unique/anomalous encounters use explicit authored state.

They are not ordinary renewable combat spawns and should not casually respawn after defeat or disappear because a general wildlife table rerolled.

---

# 7. Opening Woodland creature roster

The opening Woodland remains a small coherent ecological package, not an enemy catalogue.

## Current opening package

### 1. Gloam Lynx — APPROVED / LOCKED CURRENT PREDATOR

- **Ecology role:** Predator / hostile wildlife
- **Approximate size:** Cougar-scale to slightly larger
- **Habitat:** Mixed-canopy interior, fallen timber, rooted/rocky cover, woodland edges
- **Normal behavior:** Solitary stalking predator; watches, circles, then commits when prey/threat assessment favors attack
- **Player interactions:** Hear/spot it, notice signs, retreat, approach, disturb, be stalked, fight
- **Combat:** Yes in the authored first Marlow Woodland encounter if the player continues deeper while actively stalked; future presence may vary ecologically
- **Memorable visual trait:** Tufted ears plus a low dark mottled silhouette with visibly powerful hindquarters
- **Scope:** **Current opening Woodland**

### 2. Mooncalf / Mooncow / Moonbull family — LOCKED CURRENT WILDLIFE

- **Ecology role:** Potentially defensive wildlife
- **Approximate size:** Juvenile plus large ungulate-like adults; exact 3D scale remains visual tuning
- **Habitat:** Woodland grazing/family area with open ground, cover, and access to forage/water
- **Normal behavior:** Graze, nurse, remain socially aware; adults protect the juvenile if seriously threatened
- **Player interactions:** Observe family behavior, identify nursing relationship, collect Mooncalf Milk from viable nursing Mooncow with field flask, provoke or threaten
- **Combat:** Possible if player aggression/serious threat causes adults to defend the calf
- **Memorable visual trait:** Visual design must clearly distinguish juvenile, nursing Mooncow, and protective adult roles without relying entirely on UI labels
- **Scope:** **Existing locked current Woodland content**

### 3. Mossback — LOCKED CURRENT ANOMALOUS INDIVIDUAL

- **Ecology role:** Species baseline is ordinary wildlife; opening individual functions as Monster / anomalous encounter because of abnormal behavior
- **Approximate size:** Large, heavy quadruped; exact species scale remains visual-development work
- **Habitat:** Woodland interior/clearing routes with trees, rocks, and lanes that can support the existing Charge interaction
- **Normal behavior:** Mossbacks are expected to be docile if given adequate space
- **Opening abnormal behavior:** This individual continues aggressive pursuit when ordinary behavior should have disengaged
- **Player interactions:** Give space, observe that pursuit continues abnormally, fight if confrontation persists, optionally report incident to Marlow later
- **Combat:** Yes on the continuing successful opening route under existing authority
- **Memorable visual trait:** Broad moss-draped or moss-like dorsal mass should communicate weight and straight-line momentum; final anatomy is not settled here
- **Scope:** **Existing locked current Woodland content**

### 4. Rootmuzzle — APPROVED CURRENT FORAGER IDENTITY

- **Ecology role:** Interactable harmless wildlife
- **Approximate size:** Large shrew / small rabbit scale, roughly 25–35 cm body length
- **Habitat:** Soft soil near roots, fallen timber, or a wet-margin burrow
- **Normal behavior:** Briefly emerges to nose through leaf litter/soft soil for grubs and seeds; freezes at distant movement and retreats if approached quickly
- **Player interactions:** Patient observation, cautious approach, watch feeding behavior; rushing close causes it to hide
- **Combat:** No current combat role
- **Memorable visual trait:** Broad flexible digging snout and oversized soil-darkened forepaws
- **Scope:** **Current opening identity for the already-approved small harmless forager observation**

Rootmuzzle does **not** create a sixth optional Woodland discovery. It names and gives creature identity to the already-approved small-forager observation.

### 5. Ashwing Thrush — APPROVED CURRENT AMBIENT WILDLIFE

- **Ecology role:** Ambient harmless wildlife
- **Approximate size:** Small woodland songbird
- **Habitat:** Canopy edges, understory branches, streamside shrubs, fallen timber pockets
- **Normal behavior:** Calls, hops/feeds, moves in loose pairs or tiny groups, scatters rapidly from larger approaching animals
- **Player interactions:** Primarily sight/sound observation; not a targetable actor for MVP
- **Combat:** No
- **Memorable visual trait:** Ash-grey upper wings with a warmer underside/tail flash visible when the bird bursts from cover
- **Scope:** **Current inexpensive ambient Woodland dressing**

Ashwing Thrushes may support environmental stalking language, such as scattering in response to a nearby Gloam Lynx. They remain ambient life rather than a collectible/checklist interaction.

## Future roster directions only

The following names/directions are approved only as future Woodland roster possibilities. They are **not current-slice production requirements** and their exact appearance, mechanics, interaction depth, and combat behavior remain uncommitted until content actually needs them.

### Fernhorn Roe — FUTURE DIRECTION ONLY

Broad role: harmless visible Woodland browser associated with broken-canopy clearings and vegetation edges.

Do not further lock anatomy, behavior tree, interactions, materials, harvests, combat rules, or implementation scope yet.

### Runnelback — FUTURE DIRECTION ONLY

Broad role: wet-margin Woodland animal capable of supporting a future defensive-wildlife niche.

Do not further lock anatomy, behavior tree, interactions, materials, harvests, combat rules, or implementation scope yet.

### Current-slice roster discipline

The current package therefore consists of:
- **Gloam Lynx** — hostile predator;
- **Mooncalf / Mooncow / Moonbull** — peaceful/defensive herd;
- **Mossback** — normally ordinary species, anomalous opening individual;
- **Rootmuzzle** — harmless burrowing forager;
- **Ashwing Thrush** — ambient harmless bird.

This stays within existing Woodland scope:
- Gloam Lynx fills the already-required first hostile wildlife role;
- Rootmuzzle names the already-required small-forager observation;
- Ashwing Thrush is ambient biodiversity/danger language rather than a new optional discovery;
- Mooncalf family and Mossback are existing required content;
- Fernhorn Roe and Runnelback remain future-only.

---

# 8. Encounter variation examples for later Woodland revisits

These examples demonstrate the locked doctrine but are **not** locked encounter tables or population weights.

A mixed-canopy predator pocket might later resolve as:
- empty but with old scratch/prey signs;
- Ashwing birds feeding normally;
- harmless wildlife passing through;
- a Gloam Lynx resting out of the obvious route;
- a Gloam Lynx actively stalking another animal and potentially noticing the player;
- no dangerous creature at all.

A wet-margin pocket might later resolve as:
- insects/bird activity only;
- Rootmuzzle briefly foraging;
- recent tracks with the animal already gone;
- a future wet-margin species if/when one is actually implemented.

The player should learn habitat associations without learning a slot-machine schedule.

---

# 9. Creature visual-development requirements

Final art/modeling remains deferred until visual-development work is separately authorized.

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

For combat-capable creatures, concept/model sheets should eventually include behavioral poses, not just neutral turnarounds.

For the Gloam Lynx, future visual development needs:
- neutral walk/trot silhouette;
- low stalk;
- watch/freeze posture;
- warning/confrontation posture;
- Pounce load pose;
- launch silhouette;
- recovery/landing posture;
- retreat/flee posture if supported by Combat behavior.

For Mooncalf adults, future visual work should clearly communicate calm, alert, warning, protective interposition, and aggression without requiring a UI state label.

For Mossback, future visual work must make ordinary docility plausible **and** allow the abnormal pursuit/Charge encounter to feel disturbing because of behavior rather than corruption VFX.

---

# 10. Central Brain approval record

Central Brain reviewed this authority and approved the following:

- **Gloam Lynx** name and identity as the first ordinary Woodland predator;
- Gloam Lynx large-cat/cougar-scale anatomy direction and subtle-fantasy-trait guardrail;
- stalking sequence with approximately two guaranteed readable cues and possible additional route-dependent cue;
- body-language-first committed Pounce setup;
- first-encounter clarification: player may retreat/leave before confrontation, but continuing deeper while actively stalked eventually produces confrontation rather than permitting permanent bypass through trigger-volume avoidance;
- ordinary Flee rules after combat begins where legal;
- **Randomize ecology, not battles** as project encounter doctrine;
- activity pockets and lightweight authored behavior states for MVP architecture;
- ordinary renewable wildlife / authored persistent groups / unique-anomalous encounters as the three broad persistence classes;
- immediate location re-entry does not reroll wildlife population;
- **Rootmuzzle** as the identity of the existing harmless-forager observation;
- **Ashwing Thrush** as inexpensive ambient Woodland wildlife and stalking-language support;
- **Fernhorn Roe** and **Runnelback** as future roster directions only;
- Mossbacks are normally ordinary creatures, while the opening individual is behaviorally anomalous;
- opening ecological progression: dangerous normal wildlife → peaceful/defensive wildlife → familiar creature behaving wrong.

---

# 11. Remaining unresolved / deferred creature decisions

The following remain intentionally uncommitted:

- exact **meaningful-refresh timing/conditions** for ordinary wildlife repopulation;
- exact activity-pocket population-selection algorithm and weights;
- exact later-visit frequency/distribution of Gloam Lynx or other ordinary predators;
- final Gloam Lynx measurements and whether the subtle brow/horn feature survives visual development;
- final creature models, materials, animation timing, and audio language;
- detailed future Fernhorn Roe appearance, mechanics, interaction depth, combat behavior, and production scope;
- detailed future Runnelback appearance, mechanics, interaction depth, combat behavior, and production scope;
- broader Woodland roster expansion beyond what current content actually needs;
- cause of the opening Mossback's abnormal behavior and any larger-story escalation pattern.

Combat numbers, Pounce geometry/timing, Flee mechanics, Hunting interactions, quest consequences, and biome placement rules remain owned by their existing authorities rather than becoming unresolved decisions here.

---

# 12. Contradiction / authority review

## No material contradiction found

The approved package remains consistent with current repository authority:
- `WORLD_BIOMES_EXPLORATION.md` requires ordinary hostile Woodland wildlife supporting committed Pounce, a peaceful/defensive Mooncalf herd, and an abnormally aggressive Mossback;
- `OPENING_FLOW.md` requires a quiet Woodland arrival before danger and identifies the first ordinary hostile creature as the likely first combat;
- `CORE_SYSTEMS_PROGRESSION.md` and `GROUP_COMBAT_PARTY_MVP.md` own committed-Pounce combat behavior, ordinary Flee behavior, participant rules, and combat start-state mechanics;
- `SYSTEMS_HUNTING.md` supports creatures physically existing in locations instead of spawning only when progress counters are satisfied;
- `OPENING_MOONCALF_MILK_FAILURE.md` distinguishes temporary herd flight from genuine permanent loss;
- `DESIGN_GUARDRAILS.md` supports world-first information, physical consequences, player agency, and avoiding invisible battle/checklist machinery.

## Optional-discovery scope remains intact

`WORLD_BIOMES_EXPLORATION.md` limits the coordinated pass to its already-approved five optional authored Woodland discoveries.

This creature authority does not expand that count:
- Gloam Lynx fills an already-required hostile-wildlife slot;
- Rootmuzzle names the already-required harmless-forager observation;
- Ashwing Thrush remains ambient non-interactive dressing;
- Fernhorn Roe and Runnelback remain future-only.

## Implementation hold preserved

`START_HERE.md` states that gameplay coding is on hold pending separate authorization. This document is authority/documentation only and is **not permission to modify Unity or Blender**.
