# Garrick's Inn — Population & Spatial Blockout Authority

## Status / scope

This document is the narrow spatial authority for the opening-MVP population, architectural relationships, circulation, sightlines, and greybox proportions of Garrick's Inn.

It supplements `INN_STARTING_AREA.md`; it does not replace that document's room-rental, Sylvie, storage, or relationship-state authority. Opening chronology remains in `OPENING_FLOW.md`, dialogue/agency remains in `DIALOGUE_PLAYER_AGENCY.md`, Regional Map behavior remains in `TRAVEL_WORLD_MAP_MVP.md`, and HUD/context-interface behavior remains in `UI_HUD_PLAYER_INFORMATION.md`.

**Do not modify Unity or Blender from this document alone.** Central Brain is assembling a coordinated implementation pass.

Core doctrine: **the Inn is a believable occupied building first and a menu hub second.** The player enters a place already in motion.

# Opening population recommendation

## Visual/playtest target

Target **6 ambient/minor patrons in the common room at opening**, with a healthy playtest range of roughly **5–9**. This is not a hard simulation count.

This count is in addition to the major opening characters physically relevant to the Inn:
- Garrick behind the bar
- Marlow at his opening table
- Sylvie working in the kitchen, mostly unseen until her later introduction

The room should read as successful and inhabited without becoming a human obstacle course.

## Population tiers

### Major NPCs
- **Garrick** — bar/service anchor, landlord, merchant, board gatekeeper
- **Marlow** — opening common-room diner/researcher, then moves to basement lab according to chronology
- **Sylvie** — kitchen authority; heard/smelled/implied before proper introduction rather than added to the opening conversation pile

### Minor authored NPCs
For MVP, **0–2 named/repeatable minor patrons are enough**. They may have one or two contextual interactions or recur later, but they should not automatically carry quests.

Preferred eventual use: one recognizable local regular and/or one repeat traveler can help the Inn develop memory without requiring a cast explosion. Their identities are not locked by this spatial pass.

### Ambient patrons
Most occupants are lightweight world population. They eat, drink, talk, rest, look toward disturbances, and occasionally enter/leave if inexpensive to support later.

They do not need dialogue trees, quest markers, schedules, procedural conversations, individual reputation systems, or day/night simulation.

# Recommended opening patron composition

Use this as a blockout/staging target, not immutable casting:

1. **Two locals sharing a small table** in the hearth/social half of the room.
2. **One traveler sitting alone with gear** at an outer-wall table, visually distinct from the local cluster.
3. **One patron eating near the hearth** where Sylvie's food visibly has a customer.
4. **One occupied bar stool** close enough to make the bar feel used but not directly in front of Garrick's primary interaction position.
5. **One additional patron** at a partially occupied communal table or second bar stool depending on camera/readability.

Keep at least one visibly usable table and several empty seats. Empty seats are useful negative space and imply capacity rather than abandonment.

# Ground-floor spatial plan

## Overall footprint

Greybox target: approximately **18 m wide × 14 m deep** for the enclosed ground-floor footprint, before wall thickness. Gameplay-friendly adjustment of roughly ±10–15% is expected after walkability/camera testing.

The common room should occupy roughly **two-thirds of the ground floor**. Kitchen/service/private circulation uses most of the remaining third.

The building is organized around three bands:

1. **Front/public arrival band** — front entrance, arrival breathing space, first tables.
2. **Common/service band** — common seating, hearth, Marlow table, bar, contract board.
3. **Rear/service/vertical circulation band** — kitchen, upstairs stair, basement access, back/local-property door.

## Recommended top-down relationship

This ASCII is relational authority, not final art or exact wall geometry.

```text
                              REAR / LOCAL PROPERTY SIDE
┌────────────────────────────────────────────────────────────────────┐
│  BACK DOOR          KITCHEN / SYLVIE              STAIRS UP       │
│  local only     ┌──────────────────────┐        ┌─────────────┐    │
│      ║          │ prep/service space   │        │ up to rooms │    │
│      ║          │                      │        └──────┬──────┘    │
│      ║          └───────┬──────────────┘               │           │
│                         │ kitchen door / pass           │           │
│  ┌──────────┐    ╔══════╧══════════════════════╗   ┌────▼─────┐    │
│  │ BASEMENT │    ║          GARRICK'S BAR      ║   │ landing/ │    │
│  │ DOOR ↓   │    ║   G ●                      ║   │ clearway │    │
│  └────┬─────┘    ╚═════════════════════════════╝   └──────────┘    │
│       │             CONTRACT BOARD behind/near bar                  │
│       │                                                             │
│  Marlow table ●M              CENTRAL CLEAR AISLE                   │
│  meal + notes                                                       │
│                                                                     │
│  ┌──────────────┐       ┌──────────────┐      ┌────────────────┐   │
│  │ HEARTH       │       │ local table  │      │ traveler table │   │
│  │ chair/table  │       │ 2 patrons    │      │ 1 patron+gear  │   │
│  └──────────────┘       └──────────────┘      └────────────────┘   │
│                                                                     │
│       partial/communal table              arrival breathing space   │
│                                                                     │
│ WINDOWS                     PLAYER START ●                          │
│                         ┌──── FRONT DOOR ────┐                      │
└─────────────────────────┴────────────────────┴──────────────────────┘
                              FRONT / REGIONAL TRAVEL
```

## Front entrance and player start

The **front door is the sole opening-MVP Regional Map travel exit**. Regional return arrives through/at this same entrance.

After Character Creation, place the player approximately **2–3 m inside the front door**, not touching the threshold and not directly in front of an NPC. Give a roughly **3 m × 3 m arrival pocket** so the player can rotate the camera, step aside, or immediately leave without colliding with furniture.

Default first view should catch:
- Garrick/bar as the strongest social anchor
- enough of the contract board to promise work without reading it from spawn
- part of the occupied common room
- Marlow off-axis rather than directly in the player's face
- the stair direction as a visible promise of upstairs space

Do not spawn the player staring squarely into Garrick as if placed on a dialogue rail.

# Bar / board / service relationship

## Garrick's bar

Recommended footprint: approximately **5.5–6.5 m long × 1.0–1.2 m deep**, with roughly **1.2–1.5 m working clearance** behind it.

Place the bar in the **rear-middle/right half** of the common room rather than flat against a distant wall. This makes Garrick a visual anchor and gives him believable command of public circulation.

Maintain a **2.0–2.5 m public clearway** in front of the bar so shopping, dialogue, stools, and through-traffic do not collide.

One or two stools may be occupied, but Garrick's main player interaction point must remain unobstructed.

## Contract board

Mount the board on the wall directly behind Garrick or slightly over his shoulder, visually obvious from the common room but physically accessed through the bar interaction zone.

The player should not need to walk behind the counter to use it.

Garrick can therefore see and address anyone attempting to claim work, and the board naturally reads as something administered informally through him rather than a detached quest kiosk.

# Garrick sightline logic

From his normal working position Garrick should have direct or near-direct view of:
- front entrance / new arrivals
- central common-room aisle
- contract-board approach
- main stair approach
- kitchen doorway
- merchandise/bar interaction zone
- basement-door approach

Do not give him supernatural awareness through walls.

### Kitchen
The kitchen door sits adjacent to/behind the bar service side. A stranger heading for it must pass through Garrick's visual field or obvious peripheral space.

### Upstairs
The stair begins near the rear-right side of the public room, visible diagonally from behind the bar. The player does not disappear behind a wall before Garrick has a chance to react.

### Basement
Place Marlow's basement door/stair entrance on the rear-left/service-side edge of the common room, still visible across the central aisle from Garrick. If later wall dressing partly occludes it, the door itself should be audible and Marlow is present during the opening to notice the attempt.

### Theft/provocation
Merchandise is behind/at the bar, so Garrick directly witnesses ordinary theft attempts. Do not place stealable opening merchandise around blind corners solely to create a trap.

# Marlow opening placement

Place Marlow at a **small 2-person table roughly 4–6 m from Garrick**, toward the left/rear side of the common room near the route to the basement but not directly beside the basement door.

This gives him:
- conversational shouting distance to Garrick
- a believable reason to later move toward the lab
- enough separation that he is not part of Garrick's personal space
- enough table surface for meal, drink, notebook, satchel, harmless specimen clutter, and the breakable sample
- a clear physical stage for the CRASH to draw Garrick/player attention

Do not put ambient patrons directly between Garrick and Marlow during the opening exchange. Their sightline does not need to be mathematically empty, but focal staging should remain visually legible.

# Hearth and common seating

Place the main fireplace on the **left exterior wall**, opposite/diagonal from the bar. This creates a warm social pole rather than stacking every point of interest on the service wall.

Recommended hearth zone: approximately **3.5–4.5 m wide**, including safe furniture clearance.

The chimney stack can rise along this exterior wall through the upper floor without cutting through the center of guest rooms.

Common seating should form loose islands around the hearth and room edges rather than a cafeteria grid.

Typical table footprint:
- small 2-person: **0.8–1.0 m square/diameter**
- common 4-person: **1.2–1.6 m long × 0.8–1.0 m deep**
- preserve roughly **0.9 m minimum chair clearance**, preferably **1.2 m+** on player-facing circulation sides

# Kitchen / bar / back-door relationship

Sylvie's kitchen occupies the **rear-left/center service side**, directly adjacent to the bar.

Required relationships:
- a normal kitchen doorway connects to the common/service side but reads as staff/private territory
- food can reach Garrick/common room without crossing the entire public floor
- a small pass/shelf/opening between kitchen and bar service side is allowed and recommended for believable plate movement, but it does **not** make the kitchen publicly accessible
- the kitchen shares a service-side relationship with the back/local-property door

The back door should sit on the **rear wall near the kitchen/service zone**. It is not a Regional Map portal and has no final MVP destination/function. Its placement simply leaves a plausible future connection to local Inn/property space.

For architectural plausibility, the kitchen cooking hearth/oven/vent should use the rear/service wall or a flue that can rise without intersecting the main upstairs corridor. It does not need to share the exact chimney with the common-room fireplace.

Preserve: **“My inn. Her kitchen.”**

# Basement access

Use a real door leading to a descending stair, not an exposed fantasy-dungeon hole.

Recommended basement stair footprint: roughly **1.1–1.3 m clear width × 3.5–4.5 m run** including landing/turn as needed.

The entrance belongs on the rear-left/service side near Marlow's opening area. It should read as **private/staff/resident access**, not guest circulation.

A closed door and the way furniture avoids it are enough to communicate that ordinary patrons do not wander downstairs.

The stair can descend beneath the common-room/service footprint and then connect to Marlow's established lab. This pass does not redesign the lab.

# Upstairs / rentable rooms

## Simple MVP plan

Use a compact landing and central hall with **four modest guest rooms**. This is enough to make the Inn credible without becoming hotel management.

Suggested upper footprint remains approximately aligned with the **18 m × 14 m** ground floor, though roof shape/wall thickness may reduce usable floor area.

```text
                         REAR
┌───────────────────────────────────────────────────────┐
│   ROOM 3                │ hall │          ROOM 4      │
│                         │      │                      │
│─────────────────────────┤      ├──────────────────────│
│   ROOM 1                │      │     PLAYER ROOM / 2  │
│                         │      │   bed + storage chest│
│─────────────────────────┤      ├───────────────┬──────│
│                         │LANDING│   STAIRS ↓    │      │
└─────────────────────────┴───────┴───────────────┴──────┘
                         FRONT
```

Room numbering/which physical room becomes the player's is presentation-level and may change. The requirement is that the rented room has a stable physical identity and contains the already-approved bed + persistent storage chest.

Recommended room interior target: roughly **3.5–4.5 m × 4–5 m** each, enough for bed, chest, small table/stand, door swing, and comfortable player navigation.

Hall clear width: **1.4–1.8 m**.

Landing clear area: approximately **2.5 m × 2.5 m** minimum.

Do not add decorating, housing construction, storage upgrades, companions, or property systems.

# Circulation authority

Preserve a strong **front-door → central aisle → bar/rear circulation** spine approximately **2.0–2.5 m wide**.

Branch routes from that spine lead to:
- hearth/common seating
- bar/board
- stairs up
- kitchen door
- basement door

No table, patron idle marker, stool, or decorative prop should permanently reduce a primary route below roughly **1.2 m** clear width. Major routes should usually remain **1.8 m+**.

Avoid these bottlenecks:
- front-door spawn blocked by a table
- stools forming a wall across the bar
- stairs emptying directly into a dining chair
- Marlow's chair blocking basement access
- kitchen door opening into the bar queue
- basement door hidden behind Marlow's table
- patrons idling in the central aisle

# Patron placement zones

## Zone A — Hearth locals
Two locals at a small table/chairs near the hearth. They visually establish the Inn as a neighborhood social space.

## Zone B — Traveler edge table
One traveler with a pack/gear near an exterior wall/window, away from the hearth cluster. This visually establishes regional traffic.

## Zone C — Eating patron
One diner within plausible service distance of the bar/kitchen but outside the central aisle. Their plate is useful environmental proof of Sylvie's work before her introduction.

## Zone D — Bar regular
One occupied stool toward an end of the bar, never at Garrick's primary interaction position.

## Zone E — Flexible/partial table
One additional ambient patron or empty/partially occupied communal table. Use this zone to tune room density after camera/playtest review.

Patron navigation/idle anchors must stay outside door swings, stair landings, board interaction space, Marlow's opening staging line, and the front arrival pocket.

# Minimal ambient behavior package

MVP patrons need only a small reusable vocabulary:
- seated idle conversation loop
- eating loop
- drinking loop
- quiet solo/resting loop
- glance/turn toward loud disturbances such as Marlow's crash
- short murmur/laugh/cheer reaction where contextually appropriate
- optional stand/leave behavior later if cheap to implement

Do not implement schedules, procedural conversations, full day/night turnover, per-patron reputation, or dozens of named states.

## Focal-dialogue readability

The Inn should not freeze when important dialogue begins.

Instead:
- nearby ambient speech drops modestly in volume/intensity
- patrons continue small idle motions
- avoid starting a loud ambient bark during Garrick/Marlow/Sylvie key lines
- camera/composition should keep Garrick and Marlow visually separated from patron silhouettes during their opening exchange
- Marlow's crash may trigger brief looks, then patrons return to what they were doing

The room remains alive while