# UI / HUD / Player Information — MVP Architecture

## Status / authority

This document is the proposed continuity home for **UI / HUD / Player Information**. Central Brain remains final design authority. Until Central Brain approves the recommendations below, treat them as implementation-ready proposals rather than locked mechanics.

This document owns presentation and information architecture: how the player sees moment-to-moment state, opens deeper player systems, reads combat legality, navigates panels, and returns to play. It does **not** redesign combat, Hunting, Cooking, Potion Making, dialogue content, regional travel rules, inventory capacity, or progression.

**Do not modify Unity from this document alone.** Central Brain is coordinating a larger implementation pass.

## Repository authority this proposal preserves

Current upstream rules require the UI to:

- keep the **world first and interface second**;
- teach one mental model at a time and avoid menu/tutorial avalanches;
- use consistent UI language and predictable close/back behavior;
- avoid replacing Hunting with detective vision, counters, or solution arrows;
- keep one general inventory for equipment, materials, ingredients, potions, and adventure items, with sensible stacking and no restrictive opening carrying capacity;
- expose only the opening equipment slots actually supported: **Weapon** and **Body Armor**;
- communicate compressed combat values clearly, including current HP, Armor, weapon geometry, Primary Action state, enemy intent, range, and LOS;
- preserve the combat grid as the main tactical reading surface;
- support lightweight Hunt Notes that remember observations/inferences without solving the hunt;
- preserve dialogue momentum and avoid giant overlays/FAQ-menu behavior;
- treat the Regional Map as a real visual travel interface, not a destination-list tab;
- keep room storage physically tied to the rented room and avoid making the backpack artificially unpleasant;
- reveal systems progressively when the player actually gains a reason to use them.

The useful RuneScape reference is therefore **predictable homes for important player systems**, not its art, chat box, permanent screen allocation, exact tab count, or proportions.

# Current Unity UI audit

The current prototype is functional scaffolding, not a coherent final shell.

## What exists now

`SliceHUD` uses Unity immediate-mode `OnGUI` and scales a 1280×800 reference layout. It currently combines most presentation responsibilities in one script:

- a top information box showing player name, text-only `HP current/max`, Armor, equipped weapon, objective, and combat phase/resource text;
- a large centered **Equipment & inventory** panel opened with `I`;
- a bottom combat box containing enemy intent, combat log, action buttons, selection hints, and cancel instructions;
- a centered/lower dialogue box that dynamically grows and can become scrollable;
- separate large interfaces for the contract board, brewing, and Cooking;
- exploration interaction prompts at the bottom.

Input currently supports:

- `I` inventory;
- `E` interact;
- combat `M`, `1`–`5`, Enter and Space shortcuts;
- Esc/right-click cancellation in combat;
- Esc closing dialogue and other modes;
- mouse selection on the combat grid.

## Current implementation problems relevant to the coordinated pass

1. **HP is text, not glanceable state.** `HP 30/30` is embedded in a sentence with name, Armor, and weapon.
2. **The top strip is overloaded.** Objective text and combat state occupy a broad box across most of the screen rather than a compact persistent HUD.
3. **Inventory and equipment are fused into one long scrolling list.** Provisions, unlock notices, quest reagents, armor, weapons, healing items, coins, and profession state share one panel.
4. **System state leaks into inventory.** `Hunting learned`, `Cooking learned`, Potion Making unlock text, and opening reagent progress are presented as inventory information.
5. **There is no stable player-system navigation shell.** `I` opens one bespoke panel; other deeper information has no predictable home.
6. **Combat actions consume nearly the full bottom width.** This works as debug scaffolding but competes with the grid and will scale poorly as learned techniques grow.
7. **Disabled combat actions mostly communicate state through disabled buttons.** The player needs a nearby reason when an action is invalid because of range, LOS, adjacency, spent Primary Action, blocked Lunge destination, etc.
8. **Combat targeting copy is partly implementation-specific and brittle.** For example, generic `red range` wording cannot by itself communicate Bow's radius + LOS legality or signature-specific geometry.
9. **Dialogue can grow into a large scrolling overlay.** That reinforces the menu-dimension feeling that current dialogue revisions are trying to remove.
10. **Esc currently closes dialogue outright.** That may be appropriate for leaving an interruptible conversation, but the presentation/input contract needs to distinguish `back/cancel current UI layer` from a consequential `leave conversation` action so Esc never produces a surprising roleplaying decision.
11. **Objective presentation can become checklist-like.** Current Marlow objective text includes ingredient checkmarks. This is useful prototype feedback but should not become the universal quest/Hunting language.
12. **The prototype has no room-storage transfer UI or Regional Map shell yet.** Both need to fit the same navigation/state rules without becoming tabs inside the compact player panel.
13. **Current `OnGUI` architecture is monolithic.** The coordinated pass should separate HUD state, player-system panels, combat presentation, dialogue presentation, storage transfer, and map presentation even if final Unity UI technology is decided downstream.
14. **Bow targeting trust is already a priority upstream bug.** UI highlights, hover feedback, and accepted clicks must all agree with the combat-coordinate legality rule.

# Recommended MVP information architecture

## Core doctrine

**Persistent HUD shows information needed for immediate decisions. A compact, predictable player-system strip opens deeper information. Large contextual interfaces appear only when the activity genuinely needs the space.**

This is a refinement, not a rejection, of Central Brain's proposed doctrine.

Three presentation layers:

1. **Persistent HUD** — glanceable state that remains useful while moving/fighting.
2. **Player panel** — compact expandable/collapsible panel for Inventory, Equipment, Character, Techniques, and Journal/Hunt Notes.
3. **Context interfaces** — combat action expansion, dialogue, physical storage transfer, Cooking/Potion interactions, contract board, and Regional Map.

The player should always know which layer currently owns input.

# 1. Persistent exploration HUD

Keep the exploration HUD small and anchored to screen edges, leaving the center and most of the world unobstructed.

Recommended MVP persistent elements:

- **HP meter** with current/max numbers;
- contextual secondary resource slot, hidden unless a real system uses it;
- small interaction prompt when a nearby object/NPC is actionable;
- compact current-objective reminder only when there is useful active direction;
- compact player-system tab/button strip.

Do **not** permanently show:

- Armor as a giant meter;
- equipped weapon description;
- all currencies/material counts;
- empty MP;
- ammo for a weapon/system that does not use ammo;
- profession unlock notices;
- clue counters;
- a minimap merely because other RPGs have one.

Armor and equipped weapon can be visible in Character/Equipment and may surface contextually in combat. The character model should also communicate equipped gear when art supports it.

## Objective reminder

Use one short current intention, not a quest checklist wall. Examples of appropriate granularity:

- `Travel to the Woodland.`
- `Marlow needs Bloodleaf, Silvermoss, and Mooncalf Milk.`
- `Return to Marlow.`
- `Investigate the damage at Reedwater Paddies.`

The detailed remembered evidence belongs in Journal/Hunt Notes.

Allow the reminder to collapse/hide without erasing the underlying journal entry.

# 2. HP presentation

Use a persistent visual HP bar plus optional numeric text such as **24 / 30**.

Requirements:

- readable at a glance without parsing a sentence;
- depletion represented by bar length/fill, not color alone;
- current/max numbers available by default for tactical precision;
- damage/healing feedback should briefly animate/change the meter without blocking control;
- same visual language in exploration and combat so entering combat does not require relearning health presentation.

Do not make Armor a second health bar. Armor is a mitigation property and should read as a compact value/icon where relevant.

# 3. Future MP accommodation

Reserve a **secondary-resource slot** adjacent to/below HP in the HUD layout, but do not render it until the player has a meaningful MP-using capability.

When magic becomes real, MP can occupy that slot with the same broad visual grammar as HP while remaining visually distinguishable by label/icon/shape as well as color.

Do not invent MP amount, regeneration, spell costs, or magic unlock rules here.

# 4. Contextual ammunition / combat resources

Use a small contextual resource readout near the HP/resource cluster or weapon/action area when the equipped weapon actually consumes that resource.

Example form:

`[arrow icon] 17`

Rules:

- hidden when the equipped weapon does not use ammunition;
- hidden before ammunition mechanically exists;
- count updates immediately when consumed/recovered;
- low/empty state uses icon/state/text treatment, not color alone;
- weapon-specific future resources may use the same contextual slot if they meet the same moment-to-moment relevance test.

Do not reserve permanent blank HUD real estate for hypothetical future resources.

# 5. Player-system tab/navigation structure

Recommended compact tab set once all relevant opening systems exist:

- **Inventory**
- **Equipment**
- **Character**
- **Techniques**
- **Journal**

`Journal` is preferred over a permanent `Quest` or `Hunting` tab because it can house objectives, remembered conversations/locations where later needed, and Hunt Notes without making every system demand its own top-level button. Within a hunt, the relevant entry can contain an **Observations / Inference / Trail** structure.

Progressive rule: tabs that have no meaningful content or player capability should not appear as rows of locked silhouettes. The strip grows as the player's actual information needs grow.

Recommended shortcuts for approval/testing:

- `I` → Inventory;
- `C` → Character (or another conflict-free key after Unity input audit);
- `J` → Journal;
- a single Techniques shortcut is optional for MVP because techniques are also reachable from the tab strip;
- clicking the same active tab closes the panel;
- Esc closes the current player panel and returns to gameplay.

Exact key bindings should be rebindable later; MVP needs consistency first.

# 6. Inventory

Inventory should answer: **What am I carrying, how many, and what can I use/manage?**

Recommended structure:

- clear item grid/list with readable icons + names/tooltips;
- sensible category filtering rather than separate inventories;
- stack counts on stackable materials/ingredients;
- coins/currency in a stable compact location;
- selected-item detail area for description and valid contextual actions;
- equipped items marked clearly but not duplicated into a separate fake inventory.

Useful MVP categories may be broad: **All / Equipment / Consumables / Materials / Key & Misc.** Do not over-categorize before item volume earns it.

Do not add restrictive weight/carry limits to justify storage. Storage is valuable for organization, stockpiling, and home-base preparation.

Opening quest reagents are normal items. The journal/objective can explain why they matter; inventory should not become the quest tracker.

# 7. Equipment

Equipment should answer: **What am I using, and what changes if I equip this?**

For MVP, show only supported functional slots:

- Weapon
- Body Armor

A compact equipment panel can show the character/model silhouette if useful, but do not build an MMO paper doll full of empty Head/Hands/Boots/Rings slots.

For a selected weapon show decision-relevant information:

- damage;
- basic attack range/geometry;
- signature technique name and concise effect;
- LOS/adjacency restrictions where relevant.

For body armor show Armor value and any future meaningful property only when such properties exist.

Comparison should emphasize changed meaningful values, not a wall of derived statistics.

# 8. Character / Stats

Character should expose a **small decision-facing summary**, not a spreadsheet.

Recommended MVP contents:

- name;
- max/current HP;
- Armor from current equipment;
- equipped weapon summary;
- current movement allowance where useful to teach the combat model;
- unlocked progression/profession identities only if they have meaningful player-facing state to inspect.

Do not invent Strength/Dexterity/Intelligence or dozens of derived ratings merely to fill the page. If a future statistic affects choices, add it when the mechanic exists and explain its effect in plain language.

A useful rule for every exposed number: **What decision can the player make differently because they can see this?** If there is no good answer, keep it out of the primary Character panel.

# 9. Abilities / learned weapon techniques

Use **Techniques** as the player-facing home for learned weapon techniques unless Central Brain later chooses a broader ability vocabulary.

For each learned technique show:

- associated weapon/type;
- concise behavior;
- range/target geometry;
- damage/effect;
- Primary Action cost or other real cost/restriction;
- clear special conditions.

Opening example information should make Sword Lunge, Spear Drive, and Bow Quick Shot understandable without requiring combat trial-and-error to learn their legality.

Do not fill the panel with unknown locked silhouettes. Show what the player has learned.

Long-term slot/loadout rules remain unresolved upstream and are not decided here.

# 10. Journal / Quest / Hunt Notes

The Journal should help the player **remember**, not solve.

Recommended hierarchy:

**Active thread / contract**

- short objective/motive;
- location/client/source where useful;
- discovered observations in the order or grouping that best preserves meaning;
- player-character **Inference** only after the game state supports it;
- last useful trail information where tracking resilience requires memory;
- resolved threads retained in a quieter completed/history area when continuity needs them.

For Hunting, avoid:

- `2/3 CLUES FOUND`;
- progress bars toward an inference;
- automatically naming the creature before evidence supports it;
- giant map arrows to the next track;
- labeling actions as Good/Evil or Lethal/Non-lethal solutions.

Good Hunt Notes example:

**Reedwater Paddies**

- Deep broad tracks in the damaged rows.
- Rice crushed, but little of it eaten.
- Mudgrubs exposed beneath disturbed stalks.

**Inference:** Something is digging through the crop for the grubs beneath it.

If the player wanders off a tracking chain, a note such as `The last clear sign led toward the wet eastern margin` is appropriate memory support. `GO HERE →` is not.

# 11. Combat action presentation

When combat starts, keep the persistent HP/resource HUD but let the lower edge **expand into a combat command tray** rather than replacing the whole screen.

MVP command set remains upstream-authoritative:

- Move
- Attack
- weapon Signature
- Defend
- Item
- Dash
- contextual authored interaction when present
- End Turn
- Flee only when actually legal

Recommended presentation:

- one compact horizontal/curved tray or clustered command row near the bottom;
- selected action expands a small detail/hint region rather than opening a giant new panel;
- signature button uses the actual equipped technique name/icon where practical;
- keyboard hints remain visible but secondary;
- grid remains the dominant central visual.

Do not design a ten-slot MMO hotbar for an opening character with one Signature technique.

# 12. Combat targeting / range / LOS feedback

Combat UI must create one trustworthy answer to **Can I do this, where, and why?**

For the selected action:

- valid target/reachable hexes receive a clear positive state;
- threatened/telegraphed enemy space remains visually distinct from player-valid target space;
- hovered hex/target gets a stronger focus state;
- invalid hover gives a concise reason when useful;
- do not rely on red/green/blue alone: use border/pattern/icon/shape changes as secondary cues.

Example invalid reasons:

- `Too close for Bow basic attack.`
- `Out of range.`
- `Line of sight blocked.`
- `Primary Action already spent.`
- `Lunge path is blocked.`
- `No open hex to push into.` for the push component, while preserving that Spear Drive damage may still be legal under upstream rules.

The UI should explain the **first relevant blocker**, not dump a rules engine trace.

### Predictive information

Because combat uses fixed damage and predictable Armor math, hovering/targeting may show expected damage where it improves decisions. Do not hide deterministic information merely to manufacture uncertainty.

### Enemy intent

Intent belongs near the enemy and/or threatened grid space first, with a compact textual reinforcement in the combat tray.

Examples:

- marked Pounce landing hex + enemy tell;
- marked Mossback Charge lane + concise `Preparing to charge` reinforcement.

Telegraph danger, not the solution. Never write `Move behind the tree to stagger Mossback.`

# 13. Dialogue presentation

Dialogue should feel anchored to characters standing in the world.

Recommended MVP direction:

- lower-third or lower-side dialogue panel rather than a large centered modal whenever scene composition allows;
- speaker name + readable text with generous line length limits;
- ordinary Continue