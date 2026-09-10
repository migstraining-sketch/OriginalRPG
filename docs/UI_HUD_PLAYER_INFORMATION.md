# UI / HUD / Player Information — MVP Architecture

## Status / authority

Proposed continuity home for **UI / HUD / Player Information**. Central Brain remains final authority. These are implementation-ready recommendations until approved. Do not modify Unity from this document alone; the coordinated pass owns implementation.

This document owns presentation and information architecture, not combat mechanics, Hunting/Cooking/Potion rules, dialogue content, travel rules, inventory capacity, or progression.

## Existing authority preserved

The UI must keep the **world first and interface second**, teach one mental model at a time, use consistent navigation, avoid clue counters/solution arrows, keep one general non-punitive inventory, expose only real equipment slots, preserve the combat grid as the tactical surface, support lightweight Hunt Notes, keep dialogue grounded in the scene, treat the Regional Map as a larger visual travel interface, keep storage physically tied to the rented room, and reveal systems progressively.

RuneScape is useful here for **predictable homes for important systems**, not its art, chat box, permanent screen allocation, exact tabs, or proportions.

# Current Unity behavior / audit

`SliceHUD` is functional `OnGUI` scaffolding. It currently combines a wide top information/objective box, text-only HP, Armor and weapon text, one long `Equipment & inventory` scroll panel, a nearly full-width bottom combat panel, a growing/scrolling dialogue overlay, interaction prompts, and separate board/Brewing/Cooking screens.

Current input includes `I` inventory, `E` interact, combat `M` and `1`–`5`, Enter/Space, mouse grid targeting, and Esc/right-click cancellation. `SliceGame` currently lets Esc close dialogue outright.

### Problems found

1. HP is embedded as `HP current/max` text rather than a glanceable meter.
2. The wide top strip mixes immediate state, equipment and objectives.
3. Inventory, equipment, provisions, quest reagents and profession unlock notices share one scrolling panel.
4. There is no stable deeper-system navigation shell.
5. Combat buttons consume too much width and will scale poorly as techniques grow.
6. Disabled actions do not consistently explain **why** they are unavailable.
7. Generic range copy cannot fully communicate Bow radius + LOS or signature geometry.
8. Dialogue can become a large scrolling overlay that obscures the physical scene.
9. Esc needs a deliberate contract so `back` never accidentally becomes a meaningful roleplaying choice.
10. Current Marlow objective checkmarks are useful debug feedback but should not become the universal quest/Hunting grammar.
11. Storage transfer and Regional Map presentation do not yet exist in the current shell.
12. The monolithic `SliceHUD` should be separated by presentation responsibility during the coordinated pass, regardless of final Unity UI technology.
13. Bow visual/input legality must be repaired so highlights, hover feedback and accepted clicks agree with combat-coordinate rules.

# Recommended MVP architecture

## Core doctrine

**Persistent HUD shows information needed for immediate decisions. A compact predictable player-system strip opens deeper information. Large contextual interfaces appear only when the activity genuinely needs the space.**

Use three layers:

1. **Persistent HUD** — glanceable moment-to-moment state.
2. **Player panel** — compact expandable/collapsible Inventory, Equipment, Character, Techniques and Journal.
3. **Context interfaces** — combat expansion, dialogue, room storage, contract board, Cooking/Potion interactions and Regional Map.

The player must always know which layer owns input.

## 1–4. Persistent HUD, HP, future MP, ammo

Keep the exploration HUD edge-anchored and compact. Show HP meter + current/max number, a hidden-until-needed secondary-resource slot, nearby interaction prompt, one compact current-objective reminder when useful, and a small player-system tab/button strip.

Do not permanently show Armor as a second bar, weapon prose, material totals, empty MP, irrelevant ammo, profession notices, clue counters, or an unearned minimap.

Use a visual HP bar plus precise text such as `24 / 30`; depletion must be readable beyond color alone. Armor remains a compact mitigation value. Reserve layout capability for MP but render nothing until meaningful MP mechanics exist.

When an equipped weapon actually consumes ammunition, show a compact contextual readout such as `[arrow] 17`. Hide it for non-ammo weapons and before ammo exists. Low/empty states need more than color.

The objective reminder states intention, not checklist progress: `Travel to the Woodland`, `Return to Marlow`, or `Investigate the damage at Reedwater Paddies`. Detailed memory belongs in Journal.

## 5. Player-system navigation

Recommended mature opening tab set:

- **Inventory**
- **Equipment**
- **Character**
- **Techniques**
- **Journal**

`Journal` is preferred over separate permanent Quest and Hunting tabs. Hunt Notes live inside relevant entries. Tabs appear when meaningful rather than presenting locked silhouettes. `I` should open Inventory; `J` is a strong Journal candidate; Character may use `C` after conflict audit. Clicking the active tab closes it. Esc closes the current player panel and returns to play.

## 6. Inventory

Inventory answers **what am I carrying, how many, and what can I do with it?** Use one general inventory with readable item icon/name, stack counts, selected-item details and contextual actions. Broad filters such as All / Equipment / Consumables / Materials / Key & Misc are enough if item volume earns them.

Do not add restrictive weight/capacity to make storage useful. Quest reagents remain normal items; Journal explains why they matter.

## 7. Equipment

Equipment answers **what am I using, and what changes if I equip this?** MVP exposes only **Weapon** and **Body Armor**. A character silhouette is optional, but no empty MMO paper doll.

Weapon details expose decision-relevant damage, range/geometry, Signature technique and LOS/adjacency restrictions. Body Armor exposes Armor and only future properties that actually matter. Comparisons emphasize meaningful changes, not stat soup.

## 8. Character / Stats

Keep a small decision-facing summary: name, HP, Armor, equipped weapon summary, and Movement where useful. Show profession/progression state only when it has meaningful inspectable information.

Do not invent Strength/Dexterity/Intelligence or dozens of derived numbers to fill space. Test every exposed statistic with: **what decision changes because the player can see this?**

## 9. Techniques

Use **Techniques** as the home for learned weapon techniques unless Central Brain later adopts broader terminology. Each learned technique shows weapon association, concise behavior, range/geometry, damage/effect, Primary Action or other real cost, and special restrictions.

Show learned techniques, not future locked silhouettes. Technique acquisition/loadout rules remain upstream-unresolved.

## 10. Journal / Hunt Notes

Journal exists to **remember, not solve**. An active entry can contain motive/objective, location/client/source, discovered observations, supported player-character **Inference**, and last useful trail information.

For Hunting, never turn this into `2/3 CLUES FOUND`, an inference progress bar, automatic creature identification, a giant next-track arrow, or Good/Evil route labels.

Example:

**Reedwater Paddies**
- Deep broad tracks in the damaged rows.
- Rice crushed, but little eaten.
- Mudgrubs exposed beneath disturbed stalks.

**Inference:** Something is digging through the crop for the grubs beneath it.

For tracking resilience, `The last clear sign led toward the wet eastern margin` is appropriate memory support. `GO HERE →` is not.

## 11. Combat actions

Combat keeps the persistent HP/resource HUD and expands the lower edge into a compact **combat command tray**. Preserve the upstream action set: Move, Attack, equipped Signature, Defend, Item, Dash, contextual authored interaction when present, End Turn, and Flee only when legal.

Selecting an action expands a small detail/hint region. The Signature button uses the actual technique name. Keyboard hints remain visible but secondary. Do not build a ten-slot MMO hotbar for an opening character with one Signature.

## 12. Combat targeting / range / LOS / intent

The UI must give one trustworthy answer to **can I do this, where, and why?**

- valid target/reachable hexes: clear positive state;
- hovered target/hex: stronger focus;
- enemy threatened/telegraphed space: visually distinct from player-valid space;
- invalid hover: concise first relevant blocker;
- important states use shape/border/pattern/icon as well as color.

Useful blockers include `Too close for Bow basic attack`, `Out of range`, `Line of sight blocked`, `Primary Action already spent`, and `Lunge path is blocked`.

Because damage is deterministic, hover/targeting may show expected damage when useful. Enemy intent should live primarily on/near the enemy and threatened grid space, with concise tray reinforcement. Telegraph danger, not the solution.

## 13. Dialogue presentation

Keep characters visually present. Prefer a lower-third/lower-side panel rather than a large centered modal when composition allows. Show speaker + readable text; ordinary Continue is visually different from meaningful player responses. Choices appear only when authored dialogue actually has choices.

Avoid scrolling for normal dialogue. If text routinely needs scrolling, fix layout/copy rather than normalizing scroll boxes.

Esc/back must not silently choose refusal, rudeness, acceptance or another consequential response. If leaving a conversation is allowed, it should be explicit/predictable; dialogue authority owns whether leaving is valid at a given state.

## 14. Room-storage transfer

The physical chest opens a two-pane interface:

**Carried Inventory ↔ Room Storage**

Use the same item language as Inventory, support direct transfer and sensible stack/quantity handling, and let Esc close back to the room. No global-bank access, needless confirmations, or transfer tax. Storage is valuable through organization and stockpiling, not a crippled backpack.

## 15. Regional Map

Regional Map is **not** a right-side player tab. It is a larger contextual travel interface entered through a physical Leave/Travel boundary. Preserve stable geography, known destinations, current-location treatment, route presentation and cancel-before-commit behavior from `TRAVEL_WORLD_MAP_MVP.md`.

The player-system shell must not become a magical travel button that bypasses physical travel boundaries.

## 16. Mouse + keyboard / input ownership

Use a consistent input-state stack:

- Exploration: WASD + E; player tabs available.
- Player panel: UI owns input; movement suppressed; Esc closes panel.
- Dialogue: dialogue owns input; Continue key advances ordinary Continue only; Esc follows approved leave/back behavior.
- Combat: shortcuts select; mouse targets grid; Enter confirms where appropriate; Esc/right-click cancels the **current uncommitted selection first**.
- Storage/Map/context interface: interface owns input; Esc backs out one layer when cancellation is legal.

Committed movement/actions remain spent. Back never rewinds game state.

## 17. Progressive unlocks

The shell grows with actual capability: no MP before magic, no ammo before ammo, no giant empty Hunting dashboard, no future-technique silhouettes. When a system becomes relevant, reveal its home with a modest one-time cue rather than a tutorial avalanche.

## 18. Screen-space philosophy

Adopt RuneScape's **predictability**, reject its permanently large viewport tax. Persistent HUD/tab strip stay small. Deeper panels expand only when opened. Combat expands contextually. Dialogue preserves the scene. Regional Map/storage/crafting may use more space because the player deliberately entered those activities.

## Accessibility/readability baseline

Support UI scale, readable text, comfortable hit targets, strong hover/selected/disabled distinctions, non-color-only state cues, and consistent focus/back behavior. Do not require tiny pixel-perfect clicking.

# Central Brain approvals required before Unity pass

1. Three-layer architecture: Persistent HUD + Player Panel + Context Interfaces.
2. Tab set: Inventory / Equipment / Character / Techniques / Journal.
3. **Journal** as combined quest-memory/Hunt Notes home rather than permanent Hunting-only tab.
4. HP bar + number and hidden-until-real MP slot.
5. Contextual ammo/resource slot.
6. Compact bottom combat tray.
7. Invalid-target reason feedback and deterministic damage-preview direction.
8. Lower-third/lower-side dialogue direction and rule that Esc cannot silently make a consequential dialogue choice.
9. Two-pane physical room-storage transfer UI.
10. Progressive reveal rather than locked silhouettes/empty gauges.
11. Whether compact objective reminder is visible by default, player-collapsible, or both.
12. Final default shortcuts after conflict/accessibility audit; `I` and `J` are recommended anchors.

# Must resolve before coordinated implementation

- Fix/verify Bow targeting so code legality, grid highlight, hover explanation and click acceptance are identical.
- Define a shared UI input/focus/back state contract before adding Map, storage and new panels.
- Keep gameplay state separate from presentation so Journal/HUD reads state without becoming quest logic.
- Split monolithic `SliceHUD` responsibility; do not let debug `OnGUI` become permanent architecture by inertia.
- Ensure dialogue exposes ordinary Continue separately from authored choices.
- Ensure Hunting exposes observations/inferences/trail memory as data the Journal can present without inventing checklist progression.
- Ensure storage uses the same inventory item model and the chest's physical interaction remains the access gate.

# Explicitly unresolved / deferred

This document does not decide magic mechanics, ammo economy, technique acquisition/loadouts, final stat system, controller UI, full accessibility suite, minimap, endgame map hierarchy, crafting recipe library, inventory capacity beyond the existing non-punitive guardrail, or visual art style/iconography.
