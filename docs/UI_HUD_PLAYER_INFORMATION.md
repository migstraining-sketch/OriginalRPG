# UI / HUD / Player Information — MVP Architecture

## Status / authority

This is the continuity authority for **UI / HUD / Player Information**. Central Brain has approved the information architecture and UI doctrines recorded below. Do not modify Unity from this document alone; the coordinated implementation pass owns implementation.

This document owns presentation and information architecture, not combat mechanics, Hunting/Cooking/Potion rules, dialogue content, travel rules, inventory capacity, or progression.

Exact pixel positions, dimensions, icon art, fonts, panel animation, and exact right-vs-bottom placement remain Unity/playtest tuning. The **information architecture is locked; exact screen composition is not**.

## Existing authority preserved

The UI must keep the **world first and interface second**, teach one mental model at a time, use consistent navigation, avoid clue counters/solution arrows, keep one general non-punitive inventory, expose only real equipment slots, preserve the combat grid as the tactical surface, support lightweight Hunt Notes, keep dialogue grounded in the scene, treat the Regional Map as a larger visual travel interface, keep storage physically tied to the rented room, and reveal systems progressively.

RuneScape is useful here for **predictable homes for important systems**, not its art, chat box, permanent screen allocation, exact tabs, or proportions.

# Current Unity behavior / audit

`SliceHUD` is functional `OnGUI` scaffolding. It currently combines a wide top information/objective box, text-only HP, Armor and weapon text, one long `Equipment & inventory` scroll panel, a nearly full-width bottom combat panel, a growing/scrolling dialogue overlay, interaction prompts, and separate board/Brewing/Cooking screens.

Current input includes `I` inventory, `E` interact, combat `M` and `1`–`5`, Enter/Space, mouse grid targeting, and Esc/right-click cancellation. Legacy dialogue behavior allowed Esc to close or suspend dialogue in ways that are no longer approved.

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

# Central Brain-approved MVP architecture

## Core doctrine — LOCKED

**Persistent HUD shows information needed for immediate decisions. A compact predictable player-system strip opens deeper information. Large contextual interfaces appear only when the activity genuinely needs the space.**

Use three layers:

1. **Persistent HUD** — glanceable moment-to-moment state.
2. **Player Panel** — compact expandable/collapsible Inventory, Equipment, Character, Techniques and Journal.
3. **Context Interfaces** — combat expansion, dialogue, room storage, contract board, Cooking/Potion interactions and Regional Map.

The player must always know which layer owns input.

## Persistent HUD / HP / future MP / ammo — LOCKED

Keep the exploration HUD edge-anchored and compact. Show HP meter + current/max number, a hidden-until-needed secondary-resource slot, nearby interaction prompt, one compact current-objective reminder when useful, and a small player-system tab/button strip.

Do not permanently show Armor as a second bar, weapon prose, material totals, empty MP, irrelevant ammo, profession notices, clue counters, or an unearned minimap.

Use a persistent visual HP bar plus precise text such as `24 / 30`; depletion must be readable beyond color alone. **Armor remains a compact mitigation value rather than a second HP bar.** Reserve layout capability for MP but render nothing until a meaningful magic/MP system actually exists.

When an equipped weapon genuinely consumes ammunition or another combat resource, show a compact contextual readout such as `[arrow] 17`. Hide it for non-resource weapons and before that resource system exists. Low/empty states need more than color.

### Active objective reminder — LOCKED

The active objective reminder is **visible by default + collapsible**.

It must remain a **short current intention**, not a checklist.

Good:

`Investigate the damage at Reedwater Paddies.`

Bad:

- `Talk to Toma 0/1`
- `Find clues 2/3`
- `Track Reedback`
- `Solve problem`
- `Return to Toma`

Detailed memory, observations, inferences and task history belong in Journal.

## Player-system navigation — LOCKED

The mature player-tab set is:

- **Inventory**
- **Equipment**
- **Character**
- **Techniques**
- **Journal**

**Journal is the home for quests, remembered observations, Hunting notes/inferences, and similar player knowledge.** Do not create separate permanent Quest and Hunting tabs merely because those systems exist.

Tabs appear when meaningful rather than presenting locked silhouettes. `I` should open Inventory; `J` is a strong Journal candidate; Character may use `C` after conflict audit. Clicking the active tab closes it. Final default shortcuts beyond established anchors remain implementation/input tuning.

## Inventory — LOCKED DIRECTION

Inventory answers **what am I carrying, how many, and what can I do with it?** Use one general inventory with readable item icon/name, stack counts, selected-item details and contextual actions. Broad filters such as All / Equipment / Consumables / Materials / Key & Misc are enough if item volume earns them.

Do not add restrictive weight/capacity to make storage useful. Quest reagents remain normal items; Journal explains why they matter.

## Equipment — LOCKED DIRECTION

Equipment answers **what am I using, and what changes if I equip this?** MVP exposes only **Weapon** and **Body Armor**. A character silhouette is optional, but no empty MMO paper doll.

Weapon details expose decision-relevant damage, range/geometry, Signature technique and LOS/adjacency restrictions. Body Armor exposes Armor and only future properties that actually matter. Comparisons emphasize meaningful changes, not stat soup.

## Character / Stats — LOCKED DIRECTION

Keep a small decision-facing summary: name, HP, Armor, equipped weapon summary, and Movement where useful. Show profession/progression state only when it has meaningful inspectable information.

Do not invent Strength/Dexterity/Intelligence or dozens of derived numbers to fill space. Test every exposed statistic with: **what decision changes because the player can see this?**

## Techniques — LOCKED

Use **Techniques** as the home for learned weapon techniques unless Central Brain later adopts broader terminology. Each learned technique shows weapon association, concise behavior, range/geometry, damage/effect, Primary Action or other real cost, and special restrictions.

Show learned techniques, not future locked silhouettes. Technique acquisition/loadout rules remain upstream-unresolved.

## Journal / Hunt Notes — LOCKED

Journal exists to **remember, not solve**. An active entry can contain motive/objective, location/client/source, discovered observations, supported player-character **Inference**, and last useful trail information.

For Hunting, never turn this into `2/3 CLUES FOUND`, an inference progress bar, automatic creature identification, a giant next-track arrow, or Good/Evil route labels.

Example:

**Reedwater Paddies**
- Deep broad tracks in the damaged rows.
- Rice crushed, but little eaten.
- Mudgrubs exposed beneath disturbed stalks.

**Inference:** Something is digging through the crop for the grubs beneath it.

For tracking resilience, `The last clear sign led toward the wet eastern margin` is appropriate memory support. `GO HERE →` is not.

## Combat actions — LOCKED

Combat keeps the persistent HP/resource HUD and expands the lower edge into a compact **combat command tray**. Preserve the upstream action set: Move, Attack, equipped Signature, Defend, Item, Dash, contextual authored interaction when present, End Turn, and Flee only when legal.

Selecting an action expands a small detail/hint region. The Signature button uses the actual technique name. Keyboard hints remain visible but secondary. The grid remains the primary tactical surface. Do not build a ten-slot MMO hotbar for an opening character with one Signature.

## Combat targeting / range / LOS / intent — LOCKED

The UI must give one trustworthy answer to **can I do this, where, and why?**

- valid target/reachable hexes: clear positive state;
- hovered target/hex: stronger focus;
- enemy threatened/telegraphed space: visually distinct from player-valid space;
- invalid hover: concise first relevant blocker;
- important states use shape/border/pattern/icon as well as color.

Approved useful blocker messages include:

- `Too close for Bow attack.`
- `Line of sight blocked.`
- `Out of range.`
- `Primary Action already spent.`
- `Lunge path blocked.`

Use the most relevant concise explanation rather than dumping an internal legality trace.

### Damage preview — LOCKED

A concise deterministic damage preview is approved where useful, **provided the UI reads the authoritative combat calculation rather than duplicating combat math**.

UI must not maintain a second copy of damage/Armor formulas that can drift away from combat rules.

Enemy intent should live primarily on/near the enemy and threatened grid space, with concise tray reinforcement. Telegraph danger, not the solution.

### Multi-enemy telegraph visibility — LOCKED / REGRESSION PROTECTION

**A dangerous committed enemy telegraph remains visible regardless of which enemy is currently focused or selected.**

Focus may reveal fuller detail for one enemy, but it must never hide another enemy's already-committed attack, threatened lane/area, target marker, or other essential danger cue.

This is a regression-protection requirement for the coordinated pass.

# Dialogue presentation — LOCKED DIRECTION

Keep characters visually present. Prefer a **lower-third/lower-side dialogue presentation** rather than a large centered modal when composition allows. Show speaker + readable text.

**Ordinary Continue must be visually distinct from meaningful player responses.** Choices appear only when authored dialogue actually has choices.

Avoid scrolling for normal dialogue. If text routinely needs scrolling, fix layout/copy rather than normalizing scroll boxes.

Meaningful branches should have conversational momentum before convergence rather than immediately reopening large FAQ-style root menus.

## Dialogue Esc / exit behavior — LOCKED

**When a meaningful dialogue decision is waiting, Esc does not hide the conversation and does not choose a response.**

The choices remain visible.

Esc must never silently mean:
- Yes
- No
- Refuse
- Leave
- Accept
- End conversation

If the fiction allows the player to disengage, provide an explicit neutral authored response such as `I'll think about it.`, `Not right now.`, `I should go.`, or `End conversation.` Exact wording belongs to the dialogue authority and must fit the context.

If the player selects that explicit exit, preserve unresolved commitment state where appropriate so the conversation can be resumed later.

If the player cannot reasonably leave because of an authored situation, do not invent a fake neutral exit. Esc simply does not dismiss the decision.

### Explicitly rejected legacy state

The following behavior is **not approved** and must be removed in the coordinated implementation:

**dialogue hidden + movement still locked + Resume Conversation prompt required**

Dialogue may be non-modal in presentation, but a meaningful pending choice remains visibly present until the player selects an authored response or the fiction supplies another legitimate transition.

## Room-storage transfer — LOCKED

The physical rented-room chest opens a two-pane interface:

**Carried Inventory ↔ Room Storage**

Use the same item language as Inventory, support direct transfer and sensible stack/quantity handling, and keep access physically tied to the room chest. No global-bank access, needless confirmations, or transfer tax. Storage is valuable through organization and stockpiling, not a crippled backpack.

**Completed transfers commit when the transfer action completes.** Esc/back cancels only an unfinished quantity selection or other uncommitted transfer sub-action; it does not reverse items already moved. Once no unfinished sub-action remains, Esc/back closes the storage interface back to the room.

## Regional Map — LOCKED

Regional Map is **not a Player Panel tab**. It remains a larger contextual travel interface entered through the appropriate physical Leave/Travel boundary. Preserve stable geography, known destinations, current-location treatment, route presentation and cancel-before-commit behavior from `TRAVEL_WORLD_MAP_MVP.md`.

The player-system shell must not become a magical travel button that bypasses physical travel boundaries.

# Global Esc / Back doctrine — LOCKED

**Esc/back may cancel or close the current uncommitted UI layer, but it must never silently perform a consequential in-world or roleplaying choice.**

Distinguish the cases explicitly:

- **ordinary UI layer** → Esc/back can close the layer or cancel an unfinished sub-action if nothing consequential has committed;
- **combat targeting / selected-but-uncommitted action** → cancel selection/targeting;
- **Inventory** → close Inventory;
- **Storage quantity/sub-action** → cancel only that unfinished sub-action; completed transfers remain committed;
- **Storage with no unfinished sub-action** → close Storage;
- **Regional Map before travel commitment** → cancel/return;
- **dialogue with a meaningful choice waiting** → Esc does not hide, close, choose, refuse, accept, or otherwise resolve the conversation; choices remain visible;
- **dialogue where fiction permits disengagement** → player must select the explicit authored neutral exit to leave/defer;
- **dialogue where fiction does not permit a reasonable exit** → do not invent one; Esc remains inert for the pending decision.

Committed travel, inventory transfers, combat actions, and world-state changes remain committed. Back does not rewind world state.

## Progressive UI unlocking — LOCKED

The shell grows with actual capability: no MP before magic, no ammo before ammo, no giant empty Hunting dashboard, no future-technique silhouettes, and no locked-tab graveyards. When a system becomes relevant, reveal its home with a modest one-time cue rather than a tutorial avalanche.

## Screen-space philosophy — LOCKED

Adopt RuneScape's **predictability**, reject its permanently large viewport tax. Persistent HUD/tab strip stay small. Deeper panels expand only when opened. Combat expands contextually. Dialogue preserves the scene. Regional Map/storage/crafting may use more space because the player deliberately entered those activities.

The information architecture above is locked. Exact pixel positions, dimensions, icon art, fonts, panel animation, and exact right-vs-bottom placement remain Unity/playtest tuning.

## Accessibility/readability baseline — LOCKED DIRECTION

Support UI scale, readable text, comfortable hit targets, strong hover/selected/disabled distinctions, non-color-only state cues, and consistent focus/back behavior. Do not require tiny pixel-perfect clicking.

# Must resolve before / during coordinated implementation

- Fix/verify Bow targeting so code legality, grid highlight, hover explanation and click acceptance are identical.
- Implement the shared UI input/focus/back state contract above before adding Map, storage and new panels.
- Keep gameplay state separate from presentation so Journal/HUD reads state without becoming quest logic.
- Split monolithic `SliceHUD` responsibility; do not let debug `OnGUI` become permanent architecture by inertia.
- Ensure dialogue exposes ordinary Continue separately from authored choices.
- Ensure meaningful dialogue choices stay visible on Esc and no hidden-dialogue/frozen-player/Resume Conversation state survives.
- Ensure explicit authored dialogue exits preserve unresolved commitment state when appropriate.
- Ensure Hunting exposes observations/inferences/trail memory as data the Journal can present without inventing checklist progression.
- Ensure storage uses the same inventory item model, the chest's physical interaction remains the access gate, and completed transfers are not undone by Back.
- Ensure damage preview consumes the same authoritative calculation/result used by combat resolution rather than reimplementing formulas in UI code.
- Ensure dangerous committed enemy telegraphs remain visible even when focus moves elsewhere.

# Explicitly unresolved / deferred

This document does not decide magic mechanics, ammo economy, technique acquisition/loadouts, final stat system, controller UI, full accessibility suite, minimap, endgame map hierarchy, crafting recipe library, inventory capacity beyond the existing non-punitive guardrail, final shortcut map, or exact visual art/style/layout composition.
