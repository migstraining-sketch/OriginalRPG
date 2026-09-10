# World / Biomes / Exploration

## Status / authority

**Status: LOCKED / CURRENT for the coordinated opening Unity pass.**

This document is the continuity authority for the physical and ecological identity of explorable places: terrain, vegetation, water, fauna, monsters, natural resources, environmental discoveries, and the relationship between geography and what the player can plausibly find.

Central Brain remains final design authority.

This document does not override dedicated authorities for Hunting, Potion Making, Cooking, travel, combat, dialogue, or progression. If an ecological decision would require one of those systems to make a new mechanical decision, keep that dependency explicit rather than silently deciding it here.

**Do not modify Unity from this subchat.** Central Brain is coordinating one implementation pass. The World / Biomes design gate is complete for that pass.

# Locked biome / location doctrine

**Biomes establish the natural possibility space for terrain, vegetation, water, wildlife, hostile creatures, resources, and discoveries. Individual locations then express a specific version of that biome according to local conditions, history, inhabitants, and current world state.**

A biome is not a loot table and a location is not a quest container.

The locked practical hierarchy is:

**Regional geography → biome identity → local conditions → authored location → current world state**

Examples:
- damp shaded woodland stone can plausibly support Silvermoss;
- sunny woodland clearings can support plants that do not thrive beneath dense canopy;
- rotting fallen timber can support fungi and insects unlike dry exposed stone;
- stream margins can support different animal signs and vegetation than upland trails;
- creatures can leave, relocate, die, be frightened away, or change behavior, changing what the player can later find there.

The intended transferable player knowledge is:

> **“I know the kind of place where this grows.”**

rather than:

> **“I know which glowing node respawns every five minutes.”**

Entering a different biome should change at least some of what the player expects to see, hear, notice, gather, track, and encounter. Identity should come from coherent combinations of terrain, vegetation, moisture/climate, ambient life, wildlife behavior, hostile creatures, anomalies where appropriate, resources, landmarks, human use, and unusual local features.

Do not copy another game's zones, level bands, gathering-node loops, recolored enemy ladders, or quest-hub structure. The useful broad inspiration from zone-based RPGs is only that geography should create recognizable expectations.

## Location-first exploration rule

An explorable location must contain enough ordinary life, structure, and optional interest to remain legible as a **place** even if its current quest objective were temporarily removed.

A small location can accomplish this with a few strong ecological relationships, ambient biodiversity, readable landmarks, and optional discoveries. It does not need large content volume.

Use the project-wide intrinsic-fun test:

> **If XP, coins, quest progress, and loot rarity were hidden, would wandering here once still offer something worth noticing, learning, choosing, or experiencing?**

# Locked environmental resource-placement doctrine

Environmental association is the primary gathering-placement rule.

Resources should appear according to understandable physical/ecological associations before abstract distribution quotas.

For authored gathering content, define:
1. **Association** — damp shaded stone, rotting hardwood, disturbed soil, sunlit clearing, stream bank, burrow edge, etc.
2. **Environmental language** — moisture, shade, bark state, soil, nearby water, canopy openness, animal activity, etc.
3. **Readability** — how conspicuous the useful object should be at ordinary play distance.
4. **Knowledge state** — what the player can notice versus what they can currently identify or understand.

## Readability without node-glow

World-first placement must not become pixel hunting.

Use distinctive silhouette, coherent clustering, local contrast, motion/sound for living things, environmental composition that draws the eye, and subtle nearby contextual interaction assistance where needed.

Do not use glowing outlines on every resource, sparkle carpets, detective vision as the primary gathering grammar, or important interactables visually indistinguishable from dressing.

A novice should be able to notice something interesting. An experienced player should increasingly be able to deliberately search the correct kind of micro-environment.

# Locked discovery / identification principle

Use the conceptual sequence:

**Notice → Recognize → Identify → Understand use**

These are not mandatory literal stats, ranks, UI stages, or four interactions.

The locked smallest useful rule is:

**The player may notice something before knowing exactly what it is or what it does. Record only what the player can reasonably know, then allow believable knowledge sources to refine that understanding later.**

Knowledge may come from direct observation, Marlow or another expert, Sylvie for culinary materials, Hunting knowledge for creature signs/materials, books/notes/maps, experimentation, or later recipes/techniques.

Example:
- first: **Pale shelf fungus**;
- later: proper identification;
- later still: a credible use, once such a use actually exists.

Avoid unidentified-item bureaucracy:
- no `??? Item #47` inventory swamp;
- no mandatory appraisal vendor for every herb;
- no withholding facts the character legitimately knows;
- no fake tooltip promising recipes/effects that have not been designed.

If something has no current or near-future reason to become inventory, prefer an observation, landmark, creature behavior, or environmental curiosity.

## Resource-purpose discipline

Do not invent a material catalogue simply to make biomes appear deep.

New resources belong to one of three categories:
1. **Known-use resource** — already supports an existing system or authored requirement.
2. **Near-future hook** — has a believable broad use category reasonably expected later, without prematurely locking recipe/formula/economy.
3. **World curiosity** — primarily enriches ecology, observation, or place and may never become an inventory item.

Use category 3 sparingly as inventory. When uncertain, prefer **interesting thing in the world** over **new stackable item**.

# Locked renewable / finite resource philosophy

Physical source and world state should explain persistence and loss wherever practical.

## Renewable biological sources

Living populations or organisms may remain renewable when the source plausibly survives and recovers. Examples include a living nursing Mooncow, a living berry bramble, or a carefully harvested perennial plant.

Do not define detailed production timers for MVP. Renewable does not mean that an exact pickup magically reappears because a cooldown elapsed.

## Harvestable plants and fungi

Harvest useful portions where sensible rather than automatically destroying the source. Bloodleaf remains the opening precedent.

A source may be sustainably gathered, locally exhausted for the current visit, destroyed by player/world action, or altered by later world state. Exact revisit/replenishment timing remains deferred.

## Creature-derived resources

A living source, shed material, nest product, harvested carcass, and behavioral interaction are not interchangeable.

If the player kills, drives away, relocates, or protects a creature population, future access should reflect that outcome where practical.

The Mooncalf herd is the opening reference case: preserving a viable nursing Mooncow can preserve Mooncalf Milk access; destroying the viable source can destroy that resource opportunity. The world does not manufacture a replacement merely because the quest expects one.

## Minerals and stone

Local mineral/stone extraction should generally read as finite unless later world logic supports renewed access through a mine, quarry, trade network, geological event, or another credible source.

Do not make mineral-node respawn treadmills the default doctrine.

## One-off discoveries

Unique observations, landmarks, abandoned objects, unusual formations, and authored world details may be one-off discoveries. They do not need to regenerate as reward containers.

# Locked creature-ecology roles

Not every living thing is combat content.

Use five broad roles:

1. **Ambient harmless wildlife** — birds, insects, distant small animals, pond life, etc. Most are not targetable, harvestable, or combat-capable.
2. **Interactable harmless wildlife** — selectively authored animals that can be observed, approached, followed, disturbed, fed, or otherwise interacted with without combat being the point.
3. **Potentially defensive wildlife** — normally non-hostile creatures that may flee, warn, posture, protect young, defend territory, or fight if pressed. Mooncalf adults are the opening precedent.
4. **Predators / hostile wildlife** — creatures that plausibly stalk, ambush, contest territory/food, or attack. The first ordinary Woodland combat creature belongs here and supports committed Pounce.
5. **Monsters / anomalous creatures** — creatures whose nature or current behavior exceeds ordinary wildlife expectations. Mossback currently occupies this role through unexplained abnormal pursuit/aggression.

Creature placement should consider food, water, shelter, young/nesting, trails/ranges, signs, defensive behavior, and predator/prey pressure where useful.

This does not require a full ecology simulator. Consistent authored behavior and physical signs are enough when they create rules the player can learn.

# Environmental production tiers

Use three production tiers:

## 1. Environmental dressing

Supports silhouette, mood, terrain readability, and biodiversity without demanding interaction. Examples: most trees, leaf litter, distant birds, ordinary stones, common grass, small insects.

## 2. Recognizable ecological feature

A repeated visual/environmental element the player can learn to associate with conditions or opportunities, even when it is not a pickup. Examples: damp shaded boulders, rotting hardwood, sunny bramble clearings, stream margins, burrows.

## 3. Interactable / gatherable resource

A deliberately readable object or source with a current reason for player interaction.

Do not promote every recognizable feature into an inventory node.

# Locked exploration-reward philosophy

Exploration rewards may be:
- useful material;
- food;
- knowledge or identification;
- environmental shortcut;
- creature observation;
- small story/world detail;
- unusual location or formation;
- improved understanding of where a resource/creature can be found;
- a changed future world interaction.

They do not always need to be XP, coins, equipment, quest progress, or rarity-tier loot.

A discovery succeeds if it leaves the player with something memorable, useful, learned, or changed.

Do not scatter rewards uniformly every fixed number of metres. Quiet stretches can establish place and anticipation.

# Relationship to the Regional Map

The Regional Map handles meaningful travel **between locations**. It is not a biome-resource menu.

Once inside a location, local terrain and ecology carry its identity.

For Woodland MVP:
- arrive at a sensible trailhead;
- include a short calm arrival stretch;
- provide enough local branching/space to wander from the obvious ingredient route;
- keep an understandable return/travel boundary;
- do not expose resource icons on the Regional Map.

A known location can remain worth revisiting because player knowledge, tools, recipes, Hunting ability, NPC needs, or world state changed, not because a map icon reports available nodes.

# Locked future biome / location design checklist

Use this internally when designing future locations. It is not player-facing UI.

## A. Regional context
- relationship to settlements, roads, rivers, coastline, elevation, and neighboring terrain;
- why this terrain/climate belongs here.

## B. Terrain and movement
- ground types;
- slopes/elevation;
- obstacles;
- traversal features;
- combat-relevant terrain where appropriate.

## C. Water / climate / exposure
- moisture and drainage;
- shade/sun;
- wind and temperature character;
- streams, ponds, marsh, coast, etc.

## D. Vegetation structure
- dominant canopy/large forms;
- understory and ground layer;
- dead/decaying vegetation;
- recognizable micro-habitats.

## E. Ambient life
- insects;
- birds;
- small animals;
- distant signs/sounds.

## F. Interactable and defensive wildlife
- what lives here;
- what it eats and uses for shelter;
- what it does when approached, threatened, or observed.

## G. Predators / hostile wildlife
- why they are here;
- defining behavior/tell;
- signs they leave.

## H. Monsters / anomalies
- whether they belong here at all;
- what separates them from normal ecology;
- whether presence is common, rare, unexplained, seasonal, or historically caused.

## I. Natural resources
For each interactable resource define:
- ecological association;
- purpose category;
- renewable/finite expectation;
- visual readability;
- consuming-system dependency.

## J. Landmarks / local discoveries
- formations;
- unusual trees/rocks/water features;
- ruins/human traces where appropriate;
- shortcuts;
- vistas;
- small environmental stories.

## K. Human use and pressure
- roads/tracks;
- hunting/logging/gathering;
- farming edges;
- settlement influence;
- abandoned use.

## L. Current world state
- what changed because of quests, creature outcomes, events, NPC action, or player action.

## M. Player-learning test
After several visits, what should the player be able to predict about this biome without following a marker?

If the answer is only “what enemies are my level,” the biome identity is too thin.

# Opening Woodland ecology package — LOCKED / CURRENT

Use a **temperate mixed woodland** with enough moisture variation to support the already-established quest resources and creature encounters.

The Woodland should read as one coherent small location with several overlapping micro-habitats rather than a corridor containing three ingredient stations.

## Locked micro-habitat package

The coordinated Unity pass should support:
- **trailhead / drier entry path** — calm arrival and orientation;
- **mixed-canopy interior** — dominant ordinary woodland space;
- **sunlit or broken-canopy clearing** — supports different low vegetation and Sunberry;
- **damp shaded stone pocket** — natural Silvermoss association;
- **fallen/rotting timber pocket** — fungi/insect activity and the pale shelf fungus observation;
- **shallow stream, wet runnel, or stream margin** — moisture cue, animal activity, and orientation;
- **Mooncalf grazing/family area** — enough open ground and cover to read herd relationships;
- **Mossback combat space** — trees/rocks/lanes support the existing Charge interaction.

These may overlap spatially. Do not build eight disconnected rooms merely because eight micro-habitats are listed.

## Minimum visual vegetation palette

Do not create ten tree species or tree crafting stats.

Use three recognizable tree/large-plant families:
- dominant broad-canopy hardwood;
- pale-barked lighter understory/younger tree;
- occasional resinous evergreen.

Support them with inexpensive dressing such as ferns, brambles near light gaps, moss on damp surfaces, fungi on dead wood, leaf litter/fallen branches, and reeds/sedges near wet margins.

These are ecological visual families, not an inventory catalogue.

## Existing required content retained

The Woodland still contains:
- **Bloodleaf** — recognized from Marlow's description; useful portions harvested without automatically destroying the whole plant;
- **Silvermoss** — associated with damp shaded stone;
- **Mooncalf Milk** — obtained from a living nursing Mooncow with a suitable field flask;
- **ordinary hostile Woodland wildlife** — first committed Pounce combat lesson;
- **Mooncalf family/herd** — behavioral encounter, peaceful by default and defensive if threatened;
- **Mossback** — normally expected to be docile if given space, but this individual continues unexplained pursuit/aggression.

The optional ecology must not become a scavenger checklist or delay the urgent opening quest for players following the direct route.

# Five optional Woodland discoveries — LOCKED / CURRENT

Implement exactly these five optional authored pieces for the coordinated pass. None is required for Marlow's treatment.

## 1. Sunberry bramble — observation / environmental discovery only

**Implementation scope: does NOT enter Inventory in this pass.**

**Purpose category:** near-future culinary hook, deliberately not materialized as a carried item until Cooking gives it a present use.

**Placement:** broken-canopy/sunny clearing edge rather than deep shade.

**Interaction:** inspect/observe one readable bramble and its fruit. Do not scatter repeated berry nodes across the map.

**Current payoff:** an edible-looking natural discovery and a learned environmental association: Sunberry brambles favor light gaps.

**Do not invent yet:** exact Cooking recipe, mechanical food effect, Well Fed interaction, sale price, Cooking XP, stack/economy rules, or exact replenishment timing. Cooking authority owns future culinary use.

The living bramble remains a plausible renewable biological source for later use, but no gathering/respawn behavior is required in this coordinated pass.

## 2. Pale shelf fungus — observation / unknown discovery only

**Implementation scope: does NOT enter Inventory in this pass.**

**Purpose category:** near-future naturalist/alchemical hook with deliberately incomplete identification.

**Placement:** damp rotting fallen hardwood.

**Presentation:** **Pale shelf fungus** or similarly descriptive wording, without rarity/effect tooltip.

**Interaction:** inspect/observe only for this coordinated pass.

**Current payoff:** proves the Notice → Recognize → Identify → Understand use principle without creating an unidentified inventory object.

**Do not invent yet:** proper species name, exact effect, recipe, sell value, rarity, or a promise that Marlow identifies it immediately.

## 3. Evergreen resin seep — inspectable observation only

**Implementation scope: does NOT enter Inventory in this pass.**

**Purpose category:** near-future broad utility hook.

**Placement:** one authored visibly damaged resinous evergreen.

**Interaction:** inspect the sticky natural resin and its damaged-tree source. Do not make every evergreen a gathering node.

**Current payoff:** teaches that mundane natural materials can be worth noticing and that resources arise from particular physical conditions rather than generic node categories.

Possible future utility categories such as adhesive, fire-starting, repair/crafting, treatment preparation, or alchemy are not approved recipes or systems.

**Do not invent yet:** recipes, crafting system, market value, profession ownership, or replenishment timing.

## 4. Small forager burrow — observation / behavior only

**Implementation scope: no Inventory item and no required combat.**

**Purpose category:** ecological/world curiosity.

**Placement:** soft soil near roots or a wet margin, with readable signs of recent feeding.

**Interaction:** patient observation can reveal a small harmless woodland forager briefly emerging, feeding, or retreating. Rushing close may simply cause it to hide.

**Current payoff:** creature observation and ecological learning. It demonstrates that not every animal attacks, drops loot, or exists for a quest.

No Hunting XP, capture system, pet hook, or loot is required.

## 5. Split old tree above a spring seep — landmark / environmental discovery only

**Implementation scope: no Inventory item.**

**Purpose category:** world curiosity and local landmark.

**Placement:** a short optional branch away from the direct ingredient route.

**Feature:** an old storm- or lightning-split tree whose exposed roots frame a small spring/seep, with visibly/audibly different moss, insect, or bird activity from the main trail.

**Interaction:** simple inspect/observe. It may help local orientation.

**Current payoff:** an unusual location worth finding for its own sake, reinforcing that moisture and old wood change what lives nearby.

Do not add a secret chest, permanent-stat shrine, quest teaser, or guaranteed rare material in this pass.

# Optional-content placement rule

Distribute the five optional discoveries so wandering is rewarded without turning the Woodland into a pinata.

- direct path may visually hint at optional branches;
- no optional interaction blocks ingredient progression;
- at least one should sit near a useful ecological micro-habitat the player already cares about;
- at least one should require a small deliberate detour;
- none should require pixel-perfect scanning;
- no map markers or `0/5 discoveries` counter.

A direct player can complete Marlow's expedition cleanly. A curious player should return feeling the Woodland contained more than Marlow happened to need.

# Woodland local-layout principles for the coordinated Unity pass

Preserve local exploration rather than converting the Woodland into a narrow sequence.

Minimum spatial behavior:
- trailhead provides a calm orientation beat before danger;
- route has at least one modest branch, loop, or side pocket;
- Bloodleaf, Silvermoss, herd, and Mossback are not four beads on one straight line;
- ecological features aid navigation through clearing light, wet margin, fallen timber, herd space, and split-tree spring;
- later content may be physically reachable early where reasonable rather than spawned solely by quest counters;
- return/travel boundary remains understandable.

Do not enlarge the map simply to increase walking time. Every branch must earn its footprint through ecology, choice, orientation, encounter setup, or discovery.

# Woodland renewable / state rules for implementation

Preserve these semantics even if simplified prototype internals still use reset/replenishment logic:

- **Bloodleaf:** harvesting useful portions does not automatically destroy the source plant. Future availability is plausible; exact timing remains deferred.
- **Silvermoss:** a gathered patch may be locally exhausted for the current visit; exact regrowth timing remains deferred.
- **Mooncalf Milk:** renewable only while an appropriate living nursing source remains or recoverably returns. Existing failure-path authority controls.
- **Sunberry:** observation only in this pass; the living bramble remains the physical future source, but no gathering/respawn behavior is required yet.
- **Pale shelf fungus:** observation only in this pass, so no gathering/respawn behavior is required.
- **Evergreen resin seep:** observation only in this pass, so no gathering/respawn behavior is required.
- **forager observation / split-tree landmark:** once noticed they remain knowledge/location discoveries, not regenerating reward containers.

Do not promote the prototype's existing per-patch cooldown implementation into canon merely because it already works technically.

# Ambient biodiversity budget

Ambient non-interactive life may be richer than the five optional interactions where inexpensive.

Good candidates include bird calls/silhouettes, insects around water or rotting wood, distant small-animal rustling, occasional moth/butterfly/beetle dressing, and ordinary leaves, needles, cones, stones, and fallen branches.

These should not all become targetable actors or collectible objects.

# Deliberately unresolved dependencies

## Cooking

Do not decide:
- a Sunberry recipe;
- exact Sunberry mechanical effect;
- whether raw gathered foods apply Well Fed;
- ingredient substitutions;
- Cooking XP from gathering.

Sunberry remains a visible near-future culinary hook, but it does not enter Inventory until Cooking gives it a real present use.

## Potion Making

Do not decide:
- pale fungus proper name/effect;
- resin recipes;
- potion recipe-discovery rules;
- ingredient potency/quality tiers;
- contribution values for Marlow's lab.

Potion Making currently has one authoritative learned recipe. Optional Woodland observations must not dump future recipes into the opening.

## Hunting

Do not decide:
- Hunting XP for ambient observations;
- whether Hunting progression formally identifies the small forager;
- advanced tracks/sign systems for this expedition;
- extra harvest parts from the first hostile creature, Mooncalves, or Mossback;
- bait, traps, or Taming systems.

The ecology creates a substrate future Hunting can use without turning Marlow's expedition into a second Hunting tutorial before Hunting formally unlocks.

## Economy / inventory

Do not decide:
- Sunberry sale price;
- stack limits;
- natural-resource rarity tiers;
- global respawn economics;
- encumbrance or spoilage.

For this coordinated pass, **none of the five optional Woodland discoveries enters Inventory**. Existing quest resources remain gatherable according to their owning authorities.

# Approval record

Central Brain approved this authority with the following locked/current decisions:
- biome hierarchy: **Regional geography → biome identity → local conditions → authored location → current world state**;
- environmental association as the core gathering-placement rule;
- lightweight **Notice → Recognize → Identify → Understand use** discovery principle;
- physical renewable/finite source logic;
- five creature-ecology roles;
- exploration rewards broader than XP/coins;
- future-biome design checklist;
- opening Woodland micro-habitat package;
- all five optional Woodland discoveries;
- coordinated-pass optional-discovery scope: **Sunberry, pale shelf fungus, evergreen resin seep, small forager burrow, and split old tree/spring seep are all observation/environment discoveries only; none enters Inventory in this pass.**

No further World / Biomes design approval is required before the coordinated opening Unity pass unless implementation exposes a contradiction with another authority.

# Coordinated Unity-pass implications

Central Brain's coordinated pass should account for:
- modest Woodland branching/side pockets rather than a straight quest corridor;
- ecological micro-habitat dressing that visually explains resource placement;
- exactly five optional authored discoveries/interactions;
- observation-only handling for Sunberry, fungus, resin, forager, and split-tree spring;
- no new optional inventory resource from this package;
- no discovery checklist, resource-marker carpet, or generalized gathering-system expansion;
- subtle nearby interaction assistance where needed for readability;
- preserving living-source/world-state consequences for Mooncalf Milk;
- not treating prototype gathering cooldowns as final design authority;
- leaving Cooking, Potion Making, Hunting, and economy specifics unresolved unless their owning authorities separately approve them.

The goal is not “more loot in the Woodland.” The goal is a durable world language:

**terrain and ecology tell the player what kinds of things might live or grow here, and attention can reveal more than the current quest asked for.**