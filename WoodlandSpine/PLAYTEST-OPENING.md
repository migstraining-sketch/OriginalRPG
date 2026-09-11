# Coordinated slice manual playtest

Scope: **Marlow → Woodland → Mud in the Moonrice + Ily**, not all three starter routes. Use a fresh Play session for each incompatible branch. Unity: open `Assets/Scenes/Opening.unity`. Windows: launch the generated `Builds/Windows/WoodlandSpine.exe`.

## Natural first playthrough

Record elapsed time from control to the Sylvie handoff without racing or consulting the route below. Note where dialogue implies knowledge/commitment you did not supply, where the next intention is unclear, and where travel feels physically disconnected. Do not enforce an old 20–30 minute target.

## Successful treatment route

1. Walk around the occupied common room. Inspect kitchen, upstairs, basement and board before permission. Talk to Garrick and enter a name only when asked.
2. Let the interruption finish. Hearing Marlow out and following downstairs must not accept his job. Walk down the actual basement steps. Speaking to Marlow before inspecting the troll must lead into the reveal.
3. At the two initial lab questions, press Esc. Both choices remain visible. Continue toward the job; choose a refusal/neutral exit in one run and return. Explicitly accept before collecting ingredients.
4. Choose a loan weapon and use the front-door Regional Map. Cancel before commitment once. Travel to Woodland; nearby inn/farm scenes must not appear around the combat view.
5. Gather Bloodleaf and Silvermoss, inspect optional discoveries, collect milk peacefully with the lent flask. No Sunberry inventory item, XP gate, counters or destination markers should appear.
6. Fight wildlife and Mossback. With Bow, compare adjacent, range 2–4, obstructed and out-of-range positions. Hover reasons, attack highlights and accepted clicks should agree. Bait Mossback into the tree; its committed lane must remain fixed.
7. Return via the trailhead/map. Marlow prioritizes ingredients/treatment. Prepare the potion and treat the troll. Discuss Mossback afterward only if you choose to mention it.

## Genuine milk failure

In a fresh accepted-job run, provoke the juvenile. The nursing adult and protector join one battle. Flee while the nursing source survives: hostility alone must not fail treatment. In a separate run, destroy the nursing source before obtaining milk. The objective must immediately become Return to Marlow. Report the loss: no replacement source/pail, no Potion Making unlock, and Garrick's board remains available. Depart and return to test the later authored absence without a countdown.

## Mud and Ily

1. Check all three board postings. Only Mud is available for this build; other two do not add destinations. Accept Mud and travel to Reedwater Paddies.
2. Find the actual target early in one run. Inspect any two of the three clues in another. No fixed four-clue checklist or client-supplied inference is required.
3. Manage: open the crusted feeding patch and reconnect the runnel, even before clues. Stay close enough to witness the Reedback redirect and feed away from the rice. Preparing alone is not completion.
4. Cull: try Ily Direct in one run and Independent in another. Select who uses each allied slot. Kill the Reedback, then deliberately Harvest it. Killing alone must not unlock Hunting or finish the job.
5. Receive Toma's route-appropriate provisions/payment once. Invite Ily explicitly, or decline; recruited roster and active traveling companion are distinct. Return to the Inn, speak to Garrick and enter Sylvie's kitchen.

## Rooms, recovery and combat controls

- Before rental, reduce player/present companion HP and use the hearth's free Basic Rest. No coins should be charged. Defeated companions after victory AND Flee should be stable at 1 HP and unable to join another encounter until Rest.
- Rent once, enter the upper floor, walk through your room's doorway and open the chest. Other rooms are occupied. Store/withdraw stacks and equipment, including equipped gear. Esc cancels an uncommitted quantity before closing; completed transfers persist across travel. No duplicate equipment or phantom carried items.
- During battle, attack enemies near the bottom and sides. The tray must not conceal valid battlefield targets. Cancel a selection after moving: movement stays spent. Select another enemy while threats are committed: all threats stay shown. Check Defend expires on its owner's next activation.
- Use Inventory, Equipment, Character, Techniques and Journal; collapse the intention reminder. MP/ammo stays absent when irrelevant. Back door never opens the Regional Map.

## Automated reproduction

`Tools/Build-Prototype.ps1` runs the editor rules and creates the Windows player. Run that executable with `--coordinated-smoke --case CASE -batchmode -nographics -logFile LOG`, where CASE is `success`, `failure`, `manage`, `direct`, `independent` or `stress`. For world-render captures replace headless flags with `-force-d3d11 -screen-width 1280 -screen-height 800 -screen-fullscreen 0`. Optional `--capture-ui` attempts a full framebuffer capture; host capture support varies.

These flags opt into scripted test fixtures and quit afterward. Ordinary launch never runs them. Results go under `Validation/Coordinated/CASE`. The stress case tests 3v6/3v5, not new story content. Earlier smoke flags route to the current runner. Automated pass results do not replace the natural UI/dialogue/timing checks above.
