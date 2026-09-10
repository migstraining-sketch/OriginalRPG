# Garrick's Inn — Population & Spatial Blockout Authority

## Status / scope

This is the narrow opening-MVP authority for Inn population, architecture, circulation, sightlines, and greybox proportions. It supplements `INN_STARTING_AREA.md`; it does not replace its room-rental, storage, Sylvie, or relationship-state authority. Chronology remains in `OPENING_FLOW.md`, dialogue/agency in `DIALOGUE_PLAYER_AGENCY.md`, travel in `TRAVEL_WORLD_MAP_MVP.md`, and UI/context interfaces in `UI_HUD_PLAYER_INFORMATION.md`.

**Do not modify Unity or Blender from this document alone.** Central Brain is assembling the coordinated pass.

Doctrine: **the Inn is a believable occupied building first and a menu hub second. The player enters a place already in motion.**

# 1. Opening population

Target **6 ambient/minor patrons** in the opening common room, with roughly **5–9** as a playtest range rather than a hard count. This is in addition to Garrick, Marlow, and Sylvie.

### Major NPCs
- Garrick: behind bar; service/social anchor.
- Marlow: opening table, then basement according to chronology.
- Sylvie: working in kitchen; mostly unseen before proper introduction.

### Minor authored NPCs
Use **0–2 named/repeatable locals or travelers** for MVP if useful. They may have short interactions or recur later, but should not automatically become quest givers. Exact identities remain open.

### Ambient patrons
Most occupants are lightweight population: eat, drink, talk, rest, look toward disturbances, and optionally enter/leave later if inexpensive. No full dialogue trees, schedules, procedural conversations, per-patron reputation, or day/night simulation.

Recommended opening composition:
- two locals sharing a hearth-side table
- one traveler alone with visible gear at an outer-wall table
- one patron actively eating near the hearth/service half
- one occupied bar stool away from Garrick's main interaction point
- one additional patron at a partially occupied communal table or second stool

Keep at least one usable table and several empty seats. Busy must retain negative space.

# 2. Ground-floor footprint and plan

Greybox target: approximately **18 m wide × 14 m deep** enclosed footprint before wall thickness. Allow roughly ±10–15% during camera/walkability playtesting. The common room uses about two-thirds of the floor; kitchen/service/vertical circulation uses most of the remainder.

Organize the building as:
1. front/public arrival band
2. common/service band
3. rear/service/vertical-circulation band

```text
                         REAR / LOCAL PROPERTY
┌──────────────────────────────────────────────────────────────┐
│ BACK DOOR       KITCHEN / SYLVIE               STAIRS UP    │
│ local only    ┌──────────────────┐            ┌───────────┐  │
│               │ service / prep   │            │ to rooms  │  │
│               └───────┬──────────┘            └─────┬─────┘  │
│                       │ kitchen door/pass            │        │
│ ┌──────────┐   ╔══════╧════════════════════╗   clear landing │
│ │BASEMENT ↓│   ║       GARRICK'S BAR       ║                 │
│ └────┬─────┘   ║       G ●                 ║                 │
│      │          ╚═══════════════════════════╝                 │
│ Marlow ●M        CONTRACT BOARD behind/near bar               │
│ table                                                        │
│                                                              │
│ ┌───────────┐   ┌─────────────┐       ┌─────────────────┐   │
│ │ FIREPLACE │   │ locals x2   │       │ traveler + gear │   │
│ │ diner     │   │ small table │       │ edge table      │   │
│ └───────────┘   └─────────────┘       └─────────────────┘   │
│                                                              │
│ partial communal table        CENTRAL / ARRIVAL CLEAR SPACE   │
│                                                              │
│ WINDOWS                         PLAYER START ●                │
│                           ┌──── FRONT DOOR ────┐              │
└───────────────────────────┴────────────────────┴──────────────┘
                         FRONT / REGIONAL TRAVEL
```

ASCII is relational authority, not final wall geometry/art.

# 3. Front entrance and player start

The **front door remains the sole opening-MVP Regional Map exit** and regional return point.

After Character Creation place the player roughly **2–3 m inside** the door with about a **3 m × 3 m arrival pocket**. They can rotate, step aside, approach the room, or immediately leave without colliding with furniture.

The initial view should catch Garrick/bar as the strongest social anchor, a glimpse of the board, some occupied common-room life, Marlow off-axis, and the upstairs direction. Do not spawn the player staring squarely into Garrick like a dialogue rail.

# 4. Bar, board, and Garrick sightlines

Recommended bar footprint: **5.5–6.5 m long × 1.0–1.2 m deep**, with **1.2–1.5 m** staff clearance behind it and **2.0–2.5 m** public clearway in front.

Place it in the rear-middle/right half of the common room. One or two stools may be occupied, but Garrick's main player interaction point stays clear.

Mount the contract board behind Garrick or slightly over his shoulder. It is visible as a promise of work but accessed from the public/bar side; the player never needs to walk behind the counter.

From Garrick's normal position he should directly or nearly directly see:
- front entrance
- central aisle
- board approach
- upstairs approach
- kitchen doorway
- merchandise/bar interaction zone
- basement-door approach

No supernatural awareness through walls.

Kitchen traffic passes his service-side peripheral view. The upstairs stair begins in the rear-right public area before disappearing upward. The basement door sits across the central aisle on the rear-left/service edge. If later dressing partly occludes that door, opening sound plus Marlow's opening presence can support the reaction. Opening merchandise remains at/behind the bar so ordinary theft is directly witnessed.

# 5. Marlow opening placement

Marlow uses a small **2-person table about 4–6 m from Garrick**, left/rear of the common room and near, but not blocking, the basement route.

The table supports meal, drink, notebook, satchel, harmless specimens, and the breakable sample. It is close enough for the Garrick/Marlow exchange, far enough that Marlow has his own space, and off-axis from player spawn.

Do not place ambient patron bodies directly across the important Garrick↔Marlow opening composition. Marlow's crash should be physically readable as a disturbance in the room.

# 6. Hearth and seating

Place the main fireplace on the **left exterior wall**, diagonal/opposite the bar, creating a second social pole.

Hearth zone target: **3.5–4.5 m wide** including safe furniture clearance. Its chimney can rise along the exterior wall through the upper floor rather than cutting through central guest space.

Use loose seating islands, not a cafeteria grid.

Typical footprints:
- 2-person table: **0.8–1.0 m** square/diameter
- 4-person/common table: **1.2–1.6 m × 0.8–1.0 m**
- chair clearance: about **0.9 m minimum**, preferably **1.2 m+** on player-facing routes

# 7. Kitchen, service, and back door

Sylvie's kitchen occupies the **rear-left/center service side directly adjacent to the bar**.

Required relationship:
- normal doorway from common/service side that reads socially private
- efficient plate/food path to bar/common room
- optional small pass/shelf between kitchen and bar service side
- service-side relationship to the back door

The **back door sits on the rear wall near the kitchen/service zone**. It remains local Inn/property space only and is **not** a Regional Map portal. No final function is assigned by this pass.

Kitchen oven/hearth/vent should use the rear/service wall or a flue that can rise without colliding with the upstairs central hall. It need not share the common-room chimney.

Preserve: **“My inn. Her kitchen.”**

# 8. Basement access

Use a real closed door to a descending stair, not an exposed dungeon opening. Put it on the rear-left/service edge near Marlow's opening area.

Recommended stair clear width: **1.1–1.3 m**; stair run/landing footprint roughly **3.5–4.5 m** depending on turn. It can descend beneath the common/service footprint toward the established lab.

Furniture and patron anchors should naturally avoid this door, making it read as private/resident access. This pass does not redesign Marlow's lab.

# 9. Upstairs / rentable rooms

Use a compact landing + central hall with **four modest guest rooms**. This is enough for a believable inn without hotel-management scope.

```text
                           REAR
┌──────────────────────────────────────────────────┐
│ ROOM 3              │ HALL │          ROOM 4    │
│─────────────────────┤      ├────────────────────│
│ ROOM 1              │      │ PLAYER ROOM / 2    │
│                     │      │ bed + storage chest│
│─────────────────────┤LANDING├──────────┬─────────│
│                     │       │ STAIRS ↓ │         │
└─────────────────────┴───────┴──────────┴─────────┘
                           FRONT
```

Which numbered room becomes the player's can change. The requirement is a stable physical rented room containing the already-approved **bed + persistent storage chest**.

Room interior target: **3.5–4.5 m × 4–5 m**. Hall: **1.4–1.8 m** clear. Landing: approximately **2.5 m × 2.5 m minimum**.

Do not add decorating, housing construction, storage upgrades, property ownership, or companions living there.

# 10. Circulation and bottlenecks

Preserve a **front door → central aisle → bar/rear** circulation spine about **2.0–2.5 m wide**. Branches reach hearth, bar/board, stairs, kitchen, and basement.

No furniture/patron marker should permanently reduce a primary route below about **1.2 m**; major routes should usually remain **1.8 m+**.

Avoid:
- table blocking spawn
- stools forming a wall across the bar
- stairs emptying into a dining chair
- Marlow blocking basement access
- kitchen door opening into the bar interaction queue
- basement door hidden behind Marlow's table
- patrons idling in the central aisle

# 11. Patron placement zones

**A — Hearth locals:** two locals at a small table near fireplace.

**B — Traveler edge:** one traveler + pack at exterior wall/window.

**C — Eating patron:** one diner near hearth/service half, outside central aisle; visually proves Sylvie has customers.

**D — Bar regular:** one occupied end stool, never Garrick's primary interaction position.

**E — Flexible/partial table:** sixth patron or deliberately empty/partial communal table for density tuning.

All anchors stay outside door swings, stair landings, board interaction space, Marlow's focal staging line, and the arrival pocket.

# 12. Minimal ambient behavior

Reusable MVP set:
- quiet seated conversation
- eating
- drinking
- solo/resting
- glance/turn toward loud disturbances
- brief murmur/laugh/cheer when contextually appropriate
- optional stand/leave later if cheap

Do not implement schedules, procedural conversations, full day/night turnover, or per-patron reputation.

During important dialogue the Inn does **not** freeze. Nearby chatter can dip modestly; patrons retain small idle motions; avoid launching loud ambient barks over focal lines; Marlow's crash earns brief looks before patrons resume.

# 13. Greybox-ready measurements

These are gameplay-friendly starting values, not historical engineering requirements:

- ground footprint: **~18 × 14 m**
- common-room clear ceiling: **~3.2–3.6 m**
- upstairs guest-room ceiling: **~2.6–3.0 m**
- exterior/main door clear width: **~1.2–1.4 m**
- ordinary interior door: **~0.9–1.0 m**
- major circulation: **~1.8–2.5 m**
- secondary circulation: **~1.2–1.5 m**
- bar: **~5.5–6.5 × 1.0–1.2 m**
- behind-bar staff clearance: **~1.2–1.5 m**
- stair clear width: **~1.1–1.3 m**
- upstairs hall: **~1.4–1.8 m**
- guest room: **~3.5–4.5 × 4–5 m**
- small table: **~0.8–1.0 m**
- common table: **~1.2–1.6 × 0.8–1.0 m**

Wall thickness, beams, windows, door swings, and stair headroom should be represented in the visual plan/Blender greybox enough to prevent the building reading as disconnected rectangles, but final architectural art is not locked.

# 14. Architectural plausibility notes

- Common-room fireplace sits on an exterior wall with a plausible vertical stack.
- Kitchen heat/venting uses rear/service wall or separate flue.
- Upstairs stair and hall occupy the rear-right/central footprint rather than teleporting between floors.
- Basement stair descends under the service/common footprint without requiring impossible overlap.
- Front windows can serve common-room public walls; avoid placing major windows where kitchen/service/privacy logic would make them awkward.
- Back door aligns with service circulation, not the Regional Map.
- Furniture leaves door swings and circulation usable.

Do not lock final materials, textures, furniture models, lighting, palette, or architectural ornament in this pass.

# 15. Existing character visual-reference audit

Repository review for this pass found **character role/personality/behavior notes** in `docs/CHARACTERS.md`, but no authoritative appearance specification there for Garrick, Sylvie, Marlow, or Ily.

The repository tree/search reviewed for this task did **not surface committed portrait/reference-image files for Garrick or Sylvie**. Existing Unity content contains prototype character representations/scripts, but those are implementation scaffolding and must not be promoted into final visual canon.

Current repo-supported visual information:
- **Garrick:** rugged/large/strong character direction only; no locked appearance sheet found.
- **Marlow:** personality/role/opening staging only; no locked appearance sheet found.
- **Sylvie:** personality/role/culinary authority only; no locked appearance sheet found.
- **Ily:** role/personality/profession + Spear tactical identity; no locked appearance sheet found.

Central Brain believes Garrick/Sylvie image references may exist from earlier development outside the currently surfaced repository material. **Recover/confirm those references rather than inventing replacements.** Do not redesign appearances in this pass.

# 16. Locked/proposed/unresolved distinction

## Locked upstream rules preserved
- immediate player autonomy
- kitchen social gate
- basement/Marlow chronology
- board gate
- room-rental flow and persistent chest benefit
- front door = sole MVP Regional Map exit/return
- back door = local Inn/property future-use only
- Sylvie remains unseen/unintroduced until appropriate chronology

## Proposed blockout behavior for Central Brain approval
- 6 opening patrons, 5–9 playtest range
- ~18 × 14 m footprint
- rear-middle/right bar
- left-wall hearth
- rear-left/center kitchen + service back door
- rear-left private basement access
- rear-right upstairs access
- four upstairs guest rooms
- dimensions and patron zones above

## Central Brain decisions still required
1. Approve or revise the proposed ground-floor orientation/proportions.
2. Approve the **6-patron target** and 5–9 tuning range.
3. Decide whether MVP needs any named minor authored patron now, or ambient-only is sufficient.
4. Confirm four upstairs guest rooms as enough for blockout, or request another modest count.
5. Recover/confirm Garrick and Sylvie visual references believed to exist outside the surfaced repo; confirm whether Marlow/Ily already have external references.
6. Final exact room assignment/numbering remains presentation-level unless implementation needs a stable identifier.

# 17. Production readiness

After Central Brain approves the proposed items above, this layout is specific enough for the next pipeline:

**design authority → top-down visual plan → Blender greybox → Unity walkable blockout → proportion/camera playtest → later environment art**

The greybox builder should not need to invent the building's core footprint, circulation, service relationships, sightlines, stair placement, patron zones, or approximate scale.