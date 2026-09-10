# World / Biomes / Exploration

## Status / authority

This document is the continuity home for the physical and ecological identity of explorable places: terrain, vegetation, water, fauna, monsters, natural resources, environmental discoveries, and the relationship between geography and what the player can plausibly find.

Central Brain remains final design authority.

This document does not override dedicated authorities for Hunting, Potion Making, Cooking, travel, combat, dialogue, or progression. If an ecological proposal would require one of those systems to make a new mechanical decision, keep that dependency explicit rather than silently deciding it here.

Do **not** modify Unity from this document alone. Central Brain is coordinating one opening implementation pass.

## Core biome / location doctrine

**Biomes establish the natural possibility space for terrain, vegetation, water, wildlife, hostile creatures, resources, and discoveries. Individual locations then express a specific version of that biome according to their local conditions, history, inhabitants, and current world state.**

A biome is not a loot table and a location is not a quest container.

The practical hierarchy is:

**Regional geography → biome identity → local conditions → authored location → current world state**

Examples:
- A damp shaded woodland rock face can plausibly support Silvermoss.
- A sunny woodland clearing can support plants that would not thrive beneath dense canopy.
- Rotting fallen timber can support different fungi/insects than dry exposed stone.
- A stream margin can support different animal signs and vegetation than an upland trail.
- A creature can leave, relocate, die, be frightened away, or change behavior, changing what the player can later find there.

This doctrine should let a player gradually build transferable knowledge such as:

> **“I know the kind of place where this grows.”**

rather than:

> **“I know which glowing node respawns every five minutes.”**

### What entering a different biome should change

A different geographic area should change at least some of what the player expects to see, hear, notice, gather, track, and encounter.

That identity should come from coherent combinations of:
- ground/terrain structure
- dominant vegetation forms
- moisture/water/climate cues
- ambient life
- interactable wildlife
- predators/hostile wildlife
- monsters/anomalies where appropriate
- resource associations
- landmarks and signs of human use
- unusual local features

Do not imitate another game's zones, level bands, gathering-node loops, recolored enemy ladders, or quest-hub structure. The useful inspiration is simply that geography should create recognizable expectations.

## Location-first exploration rule

An explorable location must contain enough ordinary life, structure, and optional interest to remain legible as a **place** even if its current quest objective were temporarily removed.

This does not mean every area needs a large content budget. A small location can feel convincing through a few strong ecological relationships, ambient biodiversity, local landmarks, and optional discoveries.

Use the project-wide intrinsic-fun test:

> **If XP, coins, quest progress, and loot rarity were hidden, would wandering here once still offer something worth noticing, learning, choosing, or experiencing?**

## Environmental resource placement

Resources should be placed according to understandable environmental associations first, not abstract distribution quotas.

For authored gathering content, define:
1. **What it is associated with.** Example: damp shaded stone, rotting hardwood, disturbed soil, sunlit clearing, stream bank, burrow edge.
2. **What visual language supports that association.** Moisture, shade, bark state, soil color/texture, nearby water, canopy openness, animal activity, etc.
3. **How conspicuous the useful object should be at ordinary play distance.**
4. **Whether the player needs prior knowledge to recognize its identity/use, as distinct from simply noticing that it is interesting.**

### Readability without glowing-node design

World-first placement does not justify pixel hunting.

Use combinations of:
- distinctive silhouette
- coherent clustering
- local contrast against its substrate
- small movement or sound for living things
- environmental composition that draws the eye
- subtle nearby contextual interaction assistance
- stronger assistance after relevant knowledge/progression where another system explicitly supports it

Do not:
- outline every useful plant from across the map
- cover the ground in sparkles
- use detective vision as the primary gathering grammar
- make important objects visually indistinguishable from dressing

The ideal result is that a novice can reasonably notice something interesting, while an experienced player can deliberately search the right kind of micro-environment.

## Smallest useful discovery / identification rule

Use the conceptual sequence:

**Notice → Recognize → Identify → Understand use**

These are **not mandatory literal stats, ranks, UI stages, or four clicks**.

The smallest useful rule is:

**The game may let the player notice and collect an unfamiliar natural object before they know its exact name or use. Record only what the player can currently justify knowing, then allow believable knowledge sources to refine that record later.**

Possible knowledge sources include:
- direct observation
- Marlow or another knowledgeable NPC
- Sylvie for culinary materials
- Hunting knowledge for creature signs/materials
- books/notes/maps
- experimentation
- later learned recipes or techniques

Example progression:
- initial interaction: **Pale shelf fungus**
- later identification: its proper name
- later system knowledge: a credible broad or exact use, when that use actually exists

Avoid unidentified-item bureaucracy:
- no permanent `??? Item #47` inventory swamp
- no mandatory appraisal vendor for every herb
- no withholding basic obvious facts such as “edible berry” when the character has legitimately learned them
- no fake mechanical tooltip promising recipes/effects that have not been designed

If an unfamiliar object has no current or near-future reason to become inventory, prefer recording an observation or leaving it as an environmental curiosity rather than giving the player a developer-promise item.

## Resource-purpose discipline

Do not create material catalogues to make a biome look deep.

A new interactable resource should fit one of these categories:

1. **Known-use resource** — already supports an existing system or authored requirement.
2. **Near-future hook** — has a believable broad use category that the project reasonably expects to support, without prematurely locking a recipe/formula/economy.
3. **World curiosity** — exists mainly to enrich ecology, observation, or place and may never become an inventory item.

Use category 3 sparingly as an inventory object. World curiosities are usually stronger as observations, landmarks, creature behavior, or inspectable environmental details.

When uncertain, prefer **interesting thing in the world** over **new stackable item**.

## Renewable / finite resource philosophy

World state should explain persistence and loss wherever practical.

### Renewable biological sources

Living populations or organisms may remain renewable when the source plausibly survives and recovers.

Examples:
- a living nursing Mooncow can remain a future source of Mooncalf Milk
- a berry-bearing shrub can fruit again
- a carefully harvested perennial plant can regrow

Do not define detailed production timers for MVP. The important rule is that renewable does not mean “the exact pickup magically reappears because the cooldown elapsed.”

### Harvestable plants and fungi

Harvest only the useful portion where that makes sense. The opening Bloodleaf precedent remains good: gathering does not automatically require uprooting/destroying the entire plant.

A plant/fungus can be:
- sustainably gathered and therefore remain a plausible future source
- exhausted locally for the current visit
- destroyed by player/world action
- altered by later world state

Exact revisit/replenishment timing is deferred.

### Creature-derived resources

Creature resources follow creature state.

A living source, shed material, nest product, harvested carcass, or behavioral interaction should not be treated as interchangeable.

If the player kills, drives away, relocates, or protects a creature population, future access should reflect that outcome where practical.

The Mooncalf herd is the opening reference case: violence can physically destroy the renewable milk opportunity rather than being corrected by an invisible replacement herd.

### Minerals and stone

Mineral/stone extraction should usually read as materially finite at a local authored site unless later world logic supports renewed access through a mine, quarry, trade network, geological event, or other credible source.

Do not implement mineral-node respawn treadmills as default doctrine.

### One-off discoveries

Unique observations, hidden landmarks, abandoned objects, unusual formations, and authored story details may be one-off discoveries.

They do not need to regenerate simply because other gatherables can.

## Ambient vs interactable ecology

Not every living thing is content the player must click.

Use five broad ecological roles:

### Ambient harmless wildlife

Birds, insects, distant small animals, pond life, etc. Their primary job is making the place feel inhabited.

Most should not be targetable, harvestable, or combat-capable.

### Interactable harmless wildlife

Creatures the player can meaningfully observe, approach, feed, disturb, follow, or otherwise interact with without combat being the point.

Use selectively when the behavior itself is worth the interaction.

### Potentially defensive wildlife

Normally non-hostile animals that may flee, warn, posture, protect young, defend territory, or fight if pressed.

Mooncalf adults are the opening precedent.

### Predators / hostile wildlife

Animals that can plausibly stalk, ambush, contest territory/food, or attack. Hostility should still arise from behavior rather than “red name means evil.”

The first ordinary Woodland combat creature belongs here and should support the approved Pounce lesson.

### Monsters / anomalous creatures

Creatures whose nature or current behavior exceeds ordinary wildlife expectations.

Mossback currently occupies this space through unexplained abnormal pursuit/aggression. Do not overpopulate the opening with anomalies or explain the larger mystery prematurely.

### Ecology must support Hunting later

Creature placement should consider:
- food
- water
- shelter
- nesting/young
- trails/ranges
- signs left behind
- defensive behavior
- predator/prey pressure where useful

Not every creature requires a simulation. Authored behavior and signs are sufficient when they create consistent rules the player can learn.

## Environmental dressing vs ecological feature vs interactable resource

Use three production tiers:

### 1. Environmental dressing

Supports silhouette, mood, terrain readability, and biodiversity without interaction.

Examples: most trees, leaf litter, distant birds, ordinary stones, common grass, small insects.

### 2. Recognizable ecological feature

A repeated visual/environmental element the player can learn to associate with conditions or opportunities, even when it is not itself a pickup.

Examples: damp shaded boulder clusters, rotting fallen hardwood, sunny bramble clearings, stream margins, animal burrows.

These features are the grammar that makes resource placement understandable.

### 3. Interactable / gatherable resource

A deliberately readable object or source with a current reason for player interaction.

Do not promote every recognizable feature into an inventory node.

## Exploration reward philosophy

Exploration rewards may be:
- useful material
- food
- knowledge/identification
- an environmental shortcut
- creature observation
- small story/world detail
- unusual location or formation
- improved understanding of where a resource/creature can be found
- a changed future world interaction

They do **not** always need to be:
- XP
- coins
- equipment
- quest progress
- rarity-tier loot

A discovery is successful if it leaves the player with something memorable, useful, learned, or changed.

Do not scatter rewards uniformly every fixed number of meters. Some stretches should simply establish place and anticipation.

## Relationship to the Regional Map

The Regional Map handles meaningful travel **between locations**. It must not become a biome-resource menu.

Once the player enters a location, local terrain and ecology should carry its identity.

For Woodland MVP:
- arrive at a sensible trailhead
- include a short calm arrival stretch
- provide enough local branching/space to wander away from the obvious ingredient route
- keep one clear return/travel boundary
- do not expose resource icons on the Regional Map

A known location can remain worth revisiting because the player's knowledge, tools, recipes, Hunting skill, NPC needs, or world state changed, not because a map icon says `3/7 nodes available`.

## Future biome / location design template

Use this as an internal design checklist, not player-facing UI.

### A. Regional context
- Where is the biome/location relative to settlements, roads, rivers, coastlines, elevation, and neighboring terrain?
- Why does this climate/terrain exist here?

### B. Terrain and movement
- Ground types
- slopes/elevation
- obstacles
- traversal features
- combat-relevant terrain where appropriate

### C. Water / climate / exposure
- Moisture
- drainage
- shade/sun
- wind
- temperature character
- streams/ponds/marsh/coast/etc.

### D. Vegetation structure
- Dominant canopy/large forms
- understory
- ground layer
- dead/decaying vegetation
- recognizable micro-habitats

### E. Ambient life
- Insects
- birds
- small animals
- distant signs/sounds

### F. Interactable and defensive wildlife
- What lives here?
- What does it eat/use for shelter?
- What does it do when approached, threatened, or observed?

### G. Predators / hostile wildlife
- Why are they here?
- What behavior/tell defines them?
- What signs can they leave?

### H. Monsters / anomalies
- Are they appropriate here at all?
- What makes them different from ordinary ecology?
- Is their presence common, rare, unexplained, seasonal, or caused by local history?

### I. Natural resources
For each interactable resource:
- ecological association
- purpose category (known-use / near-future hook / world curiosity)
- renewable/finite expectation
- visual readability
- relevant consuming-system dependency

### J. Landmarks / local discoveries
- natural formations
- unusual trees/rocks/water features
- ruins/human traces where appropriate
- shortcuts
- vistas
- small environmental stories

### K. Human use and pressure
- Roads/tracks
- hunting
- logging
- farming edges
- gathering
- settlement influence
- abandoned use

### L. Current world state
- What has changed here because of quests, creature outcomes, weather/events, NPC actions, or player action?

### M. Player-learning test
After several visits, what should a player be able to predict about this biome without following a marker?

If the answer is only “what enemies are my level,” the biome identity is too thin.

# Opening Woodland ecology package

## Woodland identity for the coordinated Unity pass

Use a **temperate mixed woodland** with enough moisture variation to support the already-locked quest resources and creature encounters.

The opening Woodland should read as one coherent small location containing several micro-habitats rather than a corridor with three ingredient stations.

### Terrain / local structure

Minimum authored environmental structure:
- **trailhead / drier entry path** — quiet arrival and orientation
- **mixed-canopy interior** — dominant ordinary woodland space
- **sunlit or broken-canopy clearing** — supports different low vegetation and one optional food find
- **damp shaded stone pocket** — natural Silvermoss association
- **fallen/rotting timber pocket** — fungi/insect activity and optional discovery
- **shallow stream or wet runnel / stream margin** — moisture cue, animal activity, and local orientation
- **Mooncalf grazing/family area** — enough open ground and cover to read the herd's relationships
- **Mossback combat space** — trees/rocks/lanes deliberately support the existing telegraphed Charge interaction

These can overlap spatially. MVP does not require eight disconnected rooms.

### Minimum visual vegetation palette

Do not create ten tree species or crafting stats.

Use three recognizable tree/large-plant families for visual ecology:
- **dominant broad-canopy hardwood** — majority of mature canopy/dressing
- **pale-barked lighter understory/younger tree** — visual break and edge/clearing readability
- **occasional resinous evergreen** — minority accent, useful for drier ground and the resin interaction below

Add inexpensive dressing such as:
- ferns/low shade plants
- brambles/berry-like shrubs near light gaps
- moss on damp surfaces
- fungi on dead wood
- leaf litter/fallen branches
- reeds/sedges at the wet margin

These are ecological art families, not an inventory catalogue.

### Existing required content retained

The Woodland still contains:
- **Bloodleaf** — recognized from Marlow's description; harvested without automatically destroying the whole plant
- **Silvermoss** — associated with damp shaded stone
- **Mooncalf Milk** — obtained from a living nursing Mooncow with a suitable field flask
- **ordinary hostile Woodland wildlife** — first committed Pounce combat lesson
- **Mooncalf family/herd** — behavioral encounter, peaceful by default, defensive if threatened
- **Mossback** — normally expected to be docile if given space, but this individual continues unexplained pursuit/aggression

The optional ecology below must not turn these into a scavenger checklist or delay the urgent opening quest for players following the direct route.

## Exact optional Woodland discoveries/interactions recommended for the upcoming Unity pass

Implement **five** optional pieces. None is required for Marlow's treatment.

### 1. Sunberry bramble — optional edible find

**Purpose category:** Near-future culinary hook; immediately understandable as food only if the interaction/character knowledge can justify that safely.

**Placement:** Broken-canopy/sunny clearing edge rather than deep shade.

**Interaction:** One readable bramble/berry cluster can be inspected and, if known safe, gathered in a small amount. Do not create repeated berry nodes across the map.

**Current payoff:** A modest edible provision / culinary ingredient candidate and a learned association: these brambles favor light gaps.

**Do not lock yet:** exact Cooking recipe, Well Fed value, sale price, respawn interval, or Cooking XP.

If Central Brain does not want a new edible inventory item before Cooking can meaningfully consume it, downgrade this to an inspectable edible observation and leave gathering deferred.

### 2. Pale shelf fungus on fallen hardwood — unknown-first discovery

**Purpose category:** Near-future alchemical/naturalist hook, with identification deliberately incomplete.

**Placement:** Rotting fallen hardwood in a damp interior pocket.

**Initial presentation:** **Pale shelf fungus** or similarly descriptive wording, not a rarity banner or effect tooltip.

**Interaction:** Player may inspect it. Gathering a small specimen is allowed only if the inventory can represent a descriptive unknown cleanly without creating unidentified-item clutter.

**Current payoff:** Proves the discovery principle. Marlow can later be a believable identification source, but no exact recipe/effect is promised in this pass.

**Do not lock yet:** proper species name, exact alchemical effect, recipe, sell value, rarity, or whether Marlow identifies it immediately after the opening crisis.

### 3. Evergreen resin seep — mundane useful natural material

**Purpose category:** Near-future broad utility hook.

**Placement:** On the occasional resinous evergreen, preferably where bark is visibly damaged.

**Interaction:** Inspect/take a small amount from **one authored seep**, not every evergreen in the forest.

**Current broad identity:** Sticky natural resin with plausible future utility in adhesives, fire-starting, treatment preparation, repair/crafting, or alchemy. Those uses are examples of category, not approved recipes.

**Current payoff:** Teaches that ordinary non-magical materials can be worth noticing and that resources arise from specific tree state rather than generic “wood node.”

**Do not lock yet:** recipes, crafting system, market value, renewable timing, or whether resin eventually belongs to a dedicated profession.

If inventory scope is too expensive, this can be an observation-only interaction for the Unity pass.

### 4. Burrow edge / small forager observation — wildlife interaction without combat

**Purpose category:** World curiosity / ecological knowledge; **no inventory item**.

**Placement:** Soft soil near roots or the wet margin, with a small burrow and signs of recent feeding.

**Interaction:** Patient approach/inspection reveals a small harmless woodland forager briefly emerging, feeding, or retreating. Rushing close can simply make it hide.

**Current payoff:** Creature observation and a tiny piece of ecological learning. It demonstrates that not every animal attacks, drops loot, or exists for a quest.

No Hunting XP, capture system, pet hook, or loot is required.

### 5. Split old tree above a spring seep — unusual place / environmental discovery

**Purpose category:** World curiosity; **no inventory item required**.

**Placement:** A short optional branch away from the direct ingredient route.

**Feature:** An old lightning- or storm-split tree whose exposed roots frame a small clear spring/seep, with different moss/insect/bird ambience from the main trail.

**Interaction:** Simple inspect/observe. It may also function as a modest visual landmark that helps orient the player locally.

**Current payoff:** An unusual location worth finding for its own sake, plus reinforcement that moisture and old wood change what lives nearby.

Do not turn it into a secret chest, permanent stat shrine, quest teaser, or guaranteed rare material in this pass.

## Optional-content placement rule

The five optional discoveries should be distributed so that wandering is rewarded without turning the Woodland into a pinata.

Recommended composition:
- direct path exposes **visual hints** of at least two optional branches
- no optional interaction blocks ingredient progression
- at least one sits near a useful ecological micro-habitat the player already cares about, so discovery can happen organically
- at least one requires a small deliberate detour
- none requires pixel-perfect scanning
- no map markers or `0/5 discoveries` counter

A player who follows only Marlow's objectives can complete the expedition cleanly. A player who pokes at the edges should come back feeling that the Woodland had more in it than Marlow happened to need.

## Woodland local-layout principles for Unity

The upcoming coordinated pass should preserve local exploration rather than converting the Woodland into a narrow sequence.

Minimum spatial behavior:
- trailhead gives a calm orientation beat before danger
- route has at least one modest branch/loop or side pocket before/around the objective cluster
- Bloodleaf, Silvermoss, herd, and Mossback are not staged as four beads on one straight line
- ecological features help navigation: clearing light, wet margin, distinctive fallen timber, herd meadow, split-tree spring
- player can physically reach later content early where reasonable; authored world state should not require quest-counter spawn magic
- return/travel boundary remains understandable

Do not enlarge the map merely to increase walking time. Every extra branch should earn its footprint through ecology, choice, orientation, encounter setup, or discovery.

## Woodland renewable / state rules for implementation

For the coordinated pass, preserve these semantics even if the prototype uses simplified internal reset/replenishment logic:

- **Bloodleaf:** harvesting useful portions does not automatically destroy the source plant. Future availability is plausible, exact timing deferred.
- **Silvermoss:** a gathered patch may be locally exhausted for the current visit; exact regrowth timing deferred.
- **Mooncalf Milk:** renewable only while an appropriate living nursing source remains/recoverably returns. Existing failure-path authority remains controlling.
- **Sunberry:** if implemented as gatherable, treat the living bramble as the source rather than destroying it for one harvest. Exact refortification/fruiting timing deferred.
- **Pale shelf fungus:** if gathered, current patch can be exhausted; do not promise respawn behavior yet.
- **Resin seep:** if gathered, source remains a damaged resinous tree; exact renewed availability deferred.
- **one-off observations/landmarks:** remain discovered/known and do not regenerate as reward containers.

Do not promote the prototype's existing per-patch cooldown implementation into canon merely because it already works technically.

## Ambient biodiversity budget

Ambient non-interactive life may be richer than the five optional interactions when cheap to produce.

Good MVP candidates:
- bird calls / one or two visible bird silhouettes
- insects near water or rotting wood
- distant rustling small animal
- occasional butterfly/moth or beetle dressing
- frogs/pond insects only if the wet feature supports them
- leaves/needles/fallen cones/ordinary stones as dressing

These should not all become targetable actors or collectible objects.

## Deliberately unresolved dependencies

### Cooking

Do not decide:
- a Sunberry recipe
- whether raw gathered foods apply Well Fed
- ingredient substitutions
- exact food benefits
- Cooking XP from gathering

Sylvie may eventually recognize/use Woodland foods, but Cooking authority owns those mechanics.

### Potion Making

Do not decide:
- the pale fungus's exact name/effect
- resin recipes
- potion-recipe discovery rules
- ingredient potency/quality tiers
- contribution values for Marlow's lab

Potion Making currently has one authoritative learned recipe. Optional Woodland finds should not dump future recipes into the opening.

### Hunting

Do not decide:
- Hunting XP for ambient observations
- whether Hunting progression formally identifies the small forager
- advanced tracks/sign systems in the Woodland
- extra harvest parts from the first hostile creature, Mooncalves, or Mossback
- bait/traps/taming systems

The ecology should create a foundation that future Hunting can use without turning this opening expedition into a second Hunting tutorial before Hunting formally unlocks.

### Economy / inventory

Do not decide:
- sale prices for new natural materials
- stack limits
- resource rarity tiers
- global respawn economics
- encumbrance/spoilage

If the coordinated pass cannot represent unknown descriptive items cleanly, prefer observation-only implementations over creating placeholder economy data.

## Central Brain approval boundary

This document proposes the following decisions for Central Brain approval:

1. Lock the refined biome doctrine: **biome possibility space → local conditions/location expression → current world state**.
2. Lock environmental association as the primary gathering-placement rule, with readability assistance but no glowing-node/detective-vision default.
3. Lock the lightweight unknown-discovery rule: descriptive knowledge may precede exact identification/use without creating a formal unidentified-item bureaucracy.
4. Lock physical source/state as the preferred renewable/finite logic, while leaving detailed replenishment timing unresolved.
5. Lock the five-role creature ecology distinction: ambient harmless, interactable harmless, potentially defensive, predator/hostile wildlife, monster/anomaly.
6. Lock exploration rewards as broader than XP/coins/gear/quest progress.
7. Approve the internal future-biome checklist in this document.
8. Approve the opening Woodland as a temperate mixed woodland with recognizable dry trail, canopy, clearing, damp stone, fallen timber, wet margin, herd area, and Mossback combat micro-habitats.
9. Approve the five optional opening interactions for the coordinated Unity pass:
   - Sunberry bramble
   - pale shelf fungus on fallen hardwood
   - evergreen resin seep
   - harmless burrow/forager observation
   - split old tree + spring seep landmark
10. Confirm whether Sunberry, fungus, and resin should enter inventory in the upcoming pass or whether one or more should initially remain observation-only until consuming systems are ready.

Until Central Brain approves these items, treat them as implementation-ready proposals rather than locked canon.

## Coordinated Unity-pass implications

If approved, the upcoming Unity pass should account for:
- Woodland local layout with modest branching/side pockets rather than a straight quest corridor
- ecological micro-habitat dressing that visually explains resource placement
- exactly five optional authored discoveries/interactions, not a broad gathering-system expansion
- no quest-marker carpet or discovery checklist
- subtle nearby interaction assistance where needed for readability
- a lightweight observation/knowledge representation capable of descriptive unknowns, or observation-only fallback if inventory/UI work would expand scope
- preserving living-source/world-state consequences for Mooncalf Milk
- not treating prototype gatherable cooldowns as final design authority
- leaving Cooking/Potion/Hunting/economy specifics unresolved unless their owning authorities separately approve them

The goal is not “more loot in the Woodland.” The goal is that the Woodland begins teaching a durable world language: **terrain and ecology tell the player what kinds of things might live or grow here, and attention can reveal more than the current quest asked for.**
