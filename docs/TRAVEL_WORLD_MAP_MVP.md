# MVP Regional Travel / World Map

## Status / authority

This document defines the **opening-only MVP regional travel layer** approved in direction by Central Brain after live Unity playtesting exposed that the Woodland felt physically attached to Garrick's Inn.

This is not a full overworld/endgame travel design. It exists to give the opening's meaningful locations spatial identity while respecting player time.

**Doctrine:** Use the world map to compress meaningful travel, not erase geography.

Do not modify Unity from this document alone. Central Brain is coordinating dialogue, combat, Hunting, and travel into one implementation pass.

## Approved MVP direction

Central Brain has approved the following opening travel package:
- actual visual regional map rather than a destination list
- stable relative geography
- current-location indication
- knowledge-gated destinations
- Woodland becomes known through Marlow's accepted job
- Hunting destinations become selectable when their respective contract is accepted
- remote locations return through the same regional-map layer
- short travel/route presentation
- sensible local arrival points rather than spawning beside objectives
- no Inn → Woodland backyard portal
- no forcing unrelated destinations through the Woodland
- architecture remains compatible with future non-quest discovery
- room-rental behavior remains protected
- qualitative visual distance for MVP rather than exact travel-minute labels
- roughly 1–2 seconds of route/travel presentation as the current prototype target, subject to playtesting
- **Garrick's Inn front door is the opening MVP's regional-travel exit**
- **the Inn back door is not a Regional Map portal; it remains local Inn/property space reserved for future use**

## Problem being solved

The Inn, Woodland, Reedwater Paddies, Venn Homestead, and Vale Watermill must not behave like rooms directly attached to one another.

The player should understand that:
- these places occupy different positions in one region
- leaving one meaningful location means traveling somewhere else
- some places are farther/differently situated than others
- the player only knows destinations they have plausibly learned about

At the same time, the game should not require minutes of empty road traversal merely to prove distance exists.

## Opening travel grammar

Meaningful regional travel uses:

**Current location → Leave/Travel boundary → Regional Map → select known destination → short route/travel transition → destination arrival**

For Garrick's Inn specifically, the physical grammar is:

**Garrick's Inn front door → Regional Map → known regional destination**

Return travel uses:

**Regional Map → Garrick's Inn → arrive through/at the front entrance**

Opening examples:

**Garrick's Inn front door → Regional Map → Woodland**

**Woodland → Regional Map → Garrick's Inn front entrance**

After starter contracts become known:

**Garrick's Inn front door → Regional Map → Reedwater Paddies / Venn Homestead / Vale Watermill**

Remote destinations return through the same regional layer rather than pretending they physically border the Inn or each other.

## Smallest MVP presentation

Use an **actual visual regional map**, not a vertical destination list.

The map should show only the local opening region and only enough geography to orient the player:
- a simple terrain silhouette/background such as woodland mass, river, fields, and roads/tracks
- Garrick's Inn marker
- markers for destinations the player currently knows
- a clear current-location treatment
- lightweight route lines or implied paths between major positions where useful
- relative placement that remains stable between visits

Do not fill the map with decorative future-location icons, question marks, towers, collectibles, or unavailable-content silhouettes.

### Recommended opening layout

Exact art is downstream, but spatial relationships should be authored and stable rather than randomized.

Suggested opening-region composition:
- **Garrick's Inn:** central-ish safe/home anchor near a road junction
- **Woodland:** visibly separated from the Inn, beyond a stretch of road/trail, preferably toward one edge of the regional map
- **Reedwater Paddies:** agricultural lowland in a different direction from the Woodland
- **Venn Homestead:** rural homestead along another branch/edge of settled land
- **Vale Watermill:** positioned on the visible river/watercourse, spatially distinct from both farms and Woodland

The exact marker positions/art remain implementation-art/layout decisions for Central Brain/Unity review. The continuity requirement is that destinations occupy visibly distinct, stable positions and do not read as adjacent rooms.

## Destination knowledge and discovery

A destination has at least two separate concepts:

1. **Exists in world data**
2. **Known to the player / selectable on the regional map**

Do not equate them.

Opening discovery rules:
- Garrick's Inn is known from the beginning because the player is there.
- Woodland becomes known/selectable when the player accepts Marlow's gathering job and receives enough information to travel there.
- Reedwater Paddies becomes known/selectable when the player accepts **Mud in the Moonrice**.
- Venn Homestead becomes known/selectable when the player accepts **Three Missing by Morning**.
- Vale Watermill becomes known/selectable when the player accepts **When the Wheel Stopped**.

Reading a board posting may establish that a place/name exists, but **accepting the contract is the opening MVP point at which the destination becomes travel-selectable**, because the player then receives enough practical location information to go there.

### Future-proofing for systemic discovery

The architecture must not permanently require `quest accepted → icon appears` for every future destination.

A destination can later become known through any believable knowledge source, including:
- physical exploration
- following a road or landmark
- NPC directions
- rumors
- purchased/found maps
- books or notes
- observing a landmark from another location
- other systemic discoveries

For MVP, only the explicit opening sources above need implementation.

## Unknown destinations

Do **not** display every future location as a greyed-out icon.

If the player does not know a place exists, it should generally not appear as a destination marker at all.

The background geography may naturally imply that the region continues beyond the known markers. This preserves mystery without UI clutter.

## Entering the regional map

### Garrick's Inn

For the opening MVP, **the front door is the Inn's sole regional-travel exit**.

Using/leaving through the front door opens the Regional Map rather than loading the Woodland or another regional destination directly.

Do not choose whichever doorway is most technically convenient as the travel portal. The front entrance is the physical connection between Garrick's Inn and regional travel, reinforcing the Inn as a believable building with stable spatial logic.

### Back door — local space only

The Inn back door is **not** a Regional Map exit for MVP and must not become a second map portal.

It belongs to Garrick's Inn/local property and is reserved for future local content. Exact future use is intentionally unresolved.

Potential eventual uses may include an inn yard, deliveries, refuse/storage, stable, kitchen/service access, garden, alley, or another local-property function, but **none of these are selected or implemented by this travel package**.

Preserve only the rule:

**front door = regional travel**

**back door = local Inn space / future use**

Do not create a mandatory Inn-yard scene solely to justify regional travel. A later immediate exterior/grounds area remains compatible with the architecture but is not required for this pass.

### Remote locations

Each meaningful destination needs an obvious local **Leave / Return to road / Travel onward** boundary or interaction.

Using it opens the regional map with that destination marked as the current location.

Do not require the player to walk through the Woodland to reach Reedwater Paddies merely because the Woodland is currently the only implemented exterior scene.

## Returning and cancellation

Opening the regional map is not itself a commitment to travel.

The player should be able to cancel/back out and remain at the location they were leaving, unless the local fiction explicitly moved them past a one-way boundary.

Once a destination is selected and travel is committed, the arrival occurs after the lightweight travel transition.

When Garrick's Inn is selected as the return destination, arrival should stage the player **through or immediately at the front entrance**, not at the back door, kitchen, basement, or an arbitrary interior spawn point.

## Communicating distance and geography

For MVP, distance is communicated **qualitatively rather than with exact travel minutes**.

Use:
- stable visual spacing between markers
- visible roads/trails/river relationships where useful
- short vs longer route tracing
- arrival framing/ambient change

Avoid exact labels such as `12 minutes away` until the game has an authoritative time-of-day/travel-time system.

The map should make it immediately obvious that the Woodland is not Garrick's backyard and that the Paddies, Homestead, and Watermill occupy different parts of the region.

## Lightweight travel transition

Selecting a destination should create a brief sense of movement without becoming a cutscene tax.

Approved current prototype target:
1. Player selects destination marker.
2. Selected route/path subtly highlights or traces from current location toward destination.
3. A small traveler/token marker moves partway or fully along the route, or the route draw itself implies progress.
4. Very short fade/ambient transition.
5. Destination loads with a brief arrival framing beat before full control.

Target feel: **roughly one or two seconds of travel presentation plus ordinary loading**, subject to playtesting.

Possible ambient layer:
- woodland route: birds/wind/leaf ambience
- paddies: insects/water/field ambience
- homestead: rural animal/yard ambience
- watermill: river/wheel ambience

Do not add random encounters, travel events, stamina, survival meters, mounts, day/night simulation, or travel currencies for this MVP.

The exact token animation and audio implementation are downstream presentation choices. The required effect is simply that destination selection reads as **travel occurred**, not **Scene B loaded**.

## Arrival behavior

Arrival should place the player at a sensible entrance edge/road/trailhead for that location rather than in the middle of its objective.

Examples:
- Garrick's Inn: through/at the front entrance
- Woodland: trailhead/entry path with enough quiet space before the first combat or ingredient interaction
- Reedwater Paddies: farm approach, not directly beside the Reedback clue
- Venn Homestead: property approach, allowing the player to meet Mara and inspect the scene
- Vale Watermill: path/river approach where the stopped wheel is readable before investigation begins

This preserves each destination's local geography and lets the adventure breathe after regional travel.

## Opening chronology integration

### Marlow job

After Marlow's woodland job is explicitly accepted:
- Woodland becomes known/selectable.
- Objective should communicate traveling to the Woodland, not imply the Inn door is the quest entrance.
- Marlow provides/lends the clean field container needed for the Mooncalf Milk reagent before departure.
- Player leaves Garrick's Inn through the **front door**.
- Front door opens the Regional Map.
- Player selects Woodland.
- Brief route/travel presentation.
- Player arrives at Woodland trailhead.

The Woodland itself contains the ingredient encounters and combat beats described in `OPENING_FLOW.md`; the regional-map layer should not attempt to encode those local interactions.

Return:
- player reaches Woodland leave/travel boundary
- opens Regional Map with Woodland as current location
- selects Garrick's Inn
- travel transition
- arrives through/at Garrick's Inn **front entrance**
- returns to Marlow physically in the lab

On return, the map/travel layer does not trigger or assume any Mossback conversation. Marlow's immediate priority is the troll treatment; NPC knowledge of the Mossback remains governed by dialogue/player-agency state.

### Hunting contracts

When Garrick opens the board, all three postings remain equal choices.

A contract destination becomes selectable **only after that contract is accepted**:
- Mud in the Moonrice → Reedwater Paddies
- Three Missing by Morning → Venn Homestead
- When the Wheel Stopped → Vale Watermill

The other two contract locations are not automatically revealed as selectable merely because their postings exist.

After contract acceptance:
- leave Garrick's Inn through the **front door**
- Regional Map
- select accepted contract destination

After contract resolution:
- leave destination through its travel boundary
- Regional Map
- select Garrick's Inn
- arrive through/at the **front entrance**
- return to Garrick with outcome/material

The other two starter contracts remain available and can reveal their destinations later when accepted.

## Interaction with immediate-leave freedom

The opening rule that the player may genuinely leave Garrick's Inn remains intact.

If the player uses the **front door** before learning any other destination, the Regional Map may contain only **Garrick's Inn as the known/current anchor** and no meaningful selectable destination yet.

For MVP, do not invent a fake destination merely to reward premature leaving.

The map can simply communicate that the player does not yet know where they are going and allow them to return/back out. If Central Brain later wants a small freely-known nearby destination, that is a separate content decision.

The back door must not be used as an alternate escape around this state; it remains a local Inn/property boundary reserved for future content.

This is preferable to turning the Woodland into the default exterior simply because it exists technically.

## Relationship to renewable Woodland resources

The regional map must allow the Woodland to remain a meaningful revisitable destination after Marlow's opening quest.

In particular, if the Mooncalf herd remains intact, it can continue to serve as a renewable source of the reagent traditionally called **Mooncalf Milk** when the player later returns with a suitable container.

Travel does not special-case this resource. The principle is simply:

**known Woodland → travel back through Regional Map → revisit living herd/resource site**

Exact milk replenishment, herd simulation, container capacity, and Potion Making economy remain outside this travel MVP.

## Room-rental protection

Room rental is explicitly outside this travel change.

Preserve current room behavior. Do not alter:
- room access logic
- pricing logic
- room interaction
- rest-related placeholders
- upstairs flow

unless a separate bug/contradiction is discovered and approved.

## Implementation boundaries for the coordinated Unity pass

Travel implementation should be able to represent at minimum:
- current regional location
- known destination set
- selectable/unselectable destination state
- stable marker position per destination
- route/relationship presentation
- open/cancel map behavior
- commit destination
- short travel transition
- destination arrival spawn/entry point
- Garrick's Inn **front door** as the sole opening-MVP regional-travel trigger
- Garrick's Inn **front entrance** as its regional return/arrival point

Opening destination IDs/concepts needed:
- Garrick's Inn
- Woodland
- Reedwater Paddies
- Venn Homestead
- Vale Watermill

Only Woodland and at least the first implemented Hunting destination need full playable scene content in the next pass; the travel architecture must nevertheless support all three starter contract destinations without special-case rewiring.

## Explicitly deferred

Do not design/implement yet:
- continental/world-scale maps
- nested province map hierarchy
- manual overworld walking
- random travel encounters
- road ambush systems
- mounts
- fast-travel currencies
- travel stamina
- survival meters
- waypoint towers
- discovery XP
- map-marker collectibles
- fog-of-war minigames
- procedural roads
- day/night travel simulation
- precise travel-time economy
- open-world discovery rules beyond keeping the architecture compatible
- animal ecology/breeding systems
- Mooncalf herd simulation
- milk-production/replenishment timers
- detailed fluid-volume simulation
- any specific back-door destination/function

## Approval status

Central Brain has approved the core regional-map direction and the following details for MVP:
1. qualitative visual distance rather than exact travel-minute labels
2. accepting each starter contract as the opening trigger that makes its specific destination selectable
3. no invented destination when a player leaves before knowing anywhere else; allow cancel/return
4. roughly 1–2 seconds of route trace/token movement plus fade/ambient change as the current prototype target, subject to playtesting
5. destination arrival at a sensible local entrance/trailhead rather than directly at the quest objective
6. **Garrick's Inn front door is the sole opening-MVP regional-travel exit**
7. **regional return to Garrick's Inn arrives through/at the front entrance**
8. **the back door is not a Regional Map portal and remains local Inn/property space reserved for future use**

No specific back-door function is chosen by this package.
