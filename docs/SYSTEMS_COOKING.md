# MVP Cooking System

Cooking must support Sylvie's five possible first demonstrations using one reusable grammar. Do not turn each recipe into its own minigame.

## Core grammar

**Inspect → Prepare → Set Up → Cook → Read → Remove → Finish**

Recipes may skip irrelevant steps.

The core design rule is:

> **Recipes change what the player should notice and do, not the controls they use.**

## Four fundamental player actions

### 1. Click / interact
Used for discrete actions:
- inspect ingredient
- pick up ingredient
- choose cookware
- place food
- turn/flip
- remove from heat
- begin resting
- apply finish

### 2. Hold and move
Used for preparation:
- slicing
- scoring
- trimming
- mixing/folding
- drying

This should be forgiving. The system should care about broad intent, direction, repetition, and stopping at an appropriate state, not pixel-perfect mouse dexterity.

### 3. Adjust heat
Continuous simple control:

**Low ←→ Medium ←→ High**

Exact temperatures are unnecessary for MVP.

### 4. Observe and react
Player reads:
- color
- texture
- bubbling
- steam
- sizzling
- firmness
- rendered fat
- browning
- movement

The core skill is recognizing what is happening rather than memorizing exact timers.

## Contextual choices vs direct control

### Contextual decisions
Only plausible options appear at natural points:
- inspect state
- choose preparation action
- choose available cookware
- season/taste/balance/rest where relevant

Avoid giant culinary dropdowns.

### Direct control
Player executes the chosen action:
- movement during preparation
- heat control
- stirring/folding/turning
- removal timing

Cooking asks both:
**What should I do?** and **Can I execute it appropriately?**

## Preparation Surface

Ingredients occupy a simple interaction area with contextual preparation zones.

The same hold-and-move grammar supports:

### Slicing
Direction and spacing matter broadly.
- Fresh Reedback: cut across grain
- Preserved Reedback: thinner cuts

### Scoring
Trace shallow paths through skin/fat without deeply cutting flesh. Do not simulate three-axis knife physics.

### Trimming
Follow visually distinguishable boundaries to remove unwanted anatomy.

### Drying
Move cloth/pad across wet surfaces until moisture state changes.

### Mixing / folding
Bowl becomes the preparation surface. Broad motions alter texture/uniformity.

### Beginner prep states
Internal states may be:
- Incomplete
- Appropriate
- Overworked

These are cause/effect states, not loot rarity tiers.

## Cookware and method

Cookware is physically/contextually selected from what is available.

For the opening, likely only pan/skillet plus prep bowl are necessary.

The player should not select abstract buttons such as SEAR / RENDER / SCRAMBLE.

Instead:

**ingredient preparation + cookware + heat + handling = cooking method**

Examples:
- Fresh Reedback + hot pan + leave it alone = sear
- Preserved Reedback + moderate heat = render/crisp
- Eggs + low heat + active folding = soft eggs
- Brookmaw + scored skin + moderate heat then increase = render/crisp

## Heat communication

Heat control is simple Low–Medium–High with continuous movement between broad regions.

Cookware/food should communicate heat first through physical cues:
- shimmer
- smoke onset
- fat/butter behavior
- sizzle intensity

Tiny accessibility UI may assist, but food/cookware remain the primary interface.

Important information must never rely on color alone.

## Doneness/timing

No giant perfect-zone countdown bars.

Use overlapping cues:

### Visual
- browning
- opacity
- firmness
- moisture
- rendered fat
- setting edges
- crisping/blistering
- steam

### Audio
- wet violent hiss
- drier sharper sizzle
- steady rendering crackle
- aggressive popping/smoke cue when overheated

### Animation
- egg folds/set
- meat surface tightens
- Brookmaw skin curls/crisps

The aspiration is that experienced players can cook with minimal UI.

## Intervention

Actions such as Turn, Stir/Fold, Remove become available when physically sensible, but the game should not say **FLIP NOW FOR PERFECT RESULT**.

Player chooses timing from cues.

## Residual heat

Food can keep changing briefly after removal.

This is mechanically real because Duskhen Eggs teach carryover cooking. Eggs visibly continue setting after removal.

Novices can receive explicit Sylvie warnings; later players read it themselves.

## Resting

For dishes requiring it, player moves food to a rest surface and chooses Rest.

No need for long real-world timers. Opening rest times should be compressed.

Readiness can be communicated by softened steam/sizzle and stabilized visible juices rather than countdowns.

## Finishing

Final contextual actions may include:
- season
- assess/taste
- balance
- plate

No elaborate plating minigame for MVP.

Preserved ingredients should teach that existing salt/moisture state changes finishing decisions.

## Beginner mistakes

Track understandable causes, not scores.

### Preparation errors
- slices too thick
- score too deep
- insufficient drying
- uneven mixture

### Heat errors
- too cool
- too hot
- heat change too late

### Timing errors
- turn early/late
- remove early/late

### Handling errors
- too much movement
- too little movement
- skipped rest
- over-seasoned preserved ingredient

Most mistakes should still create edible food.

## Opening result states

Internal broad states:
- **Properly Prepared**
- **Rough but Edible**
- **Ruined**

Ruined should require severe neglect, not one beginner mistake.

Do not display numeric scores, star ratings, rarity colors, or 72% Cooking Quality. Sylvie communicates cause/effect naturally when supervising.

Example:

> "Too early. Look at that side."

## Sylvie demonstration

The player is not passively watching a cutscene.

The normal Cooking interaction exists while Sylvie owns execution. Player can inspect what she is doing and observe the exact cues/actions they will later use.

At useful moments the game may invite observation, e.g. inspecting the pan to notice the sizzling has become sharper/drier, but do not turn this into a school quiz.

Sylvie uses **the same mechanics as the player**. Her mastery comes from knowledge and execution, not hidden NPC rules.

She:
- prepares efficiently
- identifies condition immediately
- chooses correct heat from the start
- anticipates doneness
- changes heat before problems occur
- understands carryover/resting
- seasons according to ingredient state
- wastes nothing

## Optional supervised first attempt

After Cooking unlocks, player may try immediately or leave.

During beginner supervision:
- margins are forgiving
- Sylvie warns before catastrophic mistakes
- cues can be slightly emphasized
- hints may appear after hesitation
- many errors remain correctable

## Five introductory applications

### Fresh Reedback Haunch
- inspect freshness/grain
- slice across grain
- season
- hot pan
- sear with minimal movement
- read browning/sizzle
- turn
- remove
- rest

**Lesson:** Structure matters.

### Preserved Reedback Cut
- inspect preserved/salted/reduced-moisture state
- thin slice
- restrained seasoning
- moderate heat
- render/crisp
- assess/balance finish

**Lesson:** Condition matters.

### Fresh Duskhen Eggs
- crack
- mix/fold
- gentle heat
- active movement
- read folds/setting
- remove slightly loose
- residual heat finishes

**Lesson:** Control matters.

### Fresh Brookmaw Tail
- inspect anatomy
- trim
- score skin
- moderate heat
- render skin-side
- read fat/skin changes
- raise heat to finish
- remove/crisp/plate

**Lesson:** Anatomy matters.

### Naturally Shed Brookmaw Tail
- inspect source/freshness
- clean
- trim
- dry thoroughly
- score
- render
- raise heat
- crisp

**Lesson:** Source and handling matter.

## First food benefit

All five introductory dishes must have **mechanically equivalent starter value**.

Eating one applies placeholder state:

**Well Fed**

Locked meaning:
- sustained preparation/recovery benefit
- never immediate healing

Exact mechanical effect is **not yet frozen** because Rest Quality remains unresolved.

## Deferred

Do not define yet:
- exact Cooking XP/ranks
- advanced recipe discovery
- meal duration categories
- stacking
- advanced cookware
- exact Well Fed numbers/effect
- ingredient substitutions
- advanced food quality ladders
- Cooking tools
- profession specializations
- restaurant management
- farming
- spoilage simulation
