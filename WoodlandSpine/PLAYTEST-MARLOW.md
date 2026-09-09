> Legacy engineering-scaffold guide. The current narrative playtest is PLAYTEST-INTRO.md; stop at expedition acceptance.

# Marlow opening — personal playtest

Open this existing project with Unity **6000.5.6f1**, open **Assets → Scenes → Opening**, then press **Play**. If Unity displays an empty sky, stop Play mode and open Opening rather than Untitled. Choose a name/coat and enter the inn. A fresh Play session starts a new opening; progress is retained during traversal and combat retries, but there is no disk save yet.

1. **Meet the innkeepers.** Speak to Garrick at the bar, then Marlow by the basement. Accept Marlow's invitation. He goes downstairs. Use the basement hatch beside his table.
2. **Explore the lab.** You should regain movement immediately. Inspect the dated notes, labelled specimens, expedition equipment and pale baby troll. Ask Marlow what he needs, and accept the ingredient expedition. Read his advice before leaving.
3. **Return upstairs.** Use the lab stairs. Ask Garrick for a loaner: Sword, Spear or Bow. Walk out through the inn's front doorway and follow the woodland trail. There should be a calm stretch before any hostile encounter.
4. **Bloodleaf.** Look left of the trail for red leaves. Inspect them and pinch off the useful tips. The rooted stem/lower leaves remain. Repeated interaction must not award extra samples.
5. **Ordinary wildlife.** Continue north into the first clearing. Test Move, Attack and Item selection, then cancel each with Esc/right-click. Nothing should be spent by selecting/canceling. Commit a move, cancel again, and verify that the movement remains spent. Defeat the creature; two valid Sword hits should suffice.
6. **Silvermoss.** Beyond that clearing, look left around damp, shaded stone. Take a small portion of the pale moss. No large glowing waypoint marks it.
7. **Mooncalf Milk.** The Mooncalf is to the right beyond the first clearing. Follow Marlow's advice: lower your weapon, stand sideways, wait, then collect a small measure. On another fresh run, try frightening her and inspect the covered field sample farther right. The fresh, labelled sample provides a route forward; Marlow notices the difference. Killing/Taming are not implemented.
8. **Mossback.** Continue toward the far clearing. When you spot it, choose to give it space. Observe it following you anyway. You can retreat from the clearing; approaching again leads to combat. The amber Charge lane locks before the next Enemy Phase. Step out of it, or position so a major tree stops the charge. A collision should cost Mossback its next recovery phase. Bow users should be able to reposition out of adjacent range. Defeat it for the main route; boundary Flee also records survival.
9. **Return to Marlow.** Walk south through the cleared woodland, enter the inn and use the basement hatch. Tell Marlow what happened. His questions cover being cornered, injury, young and provocation. Observations should remain uncertain where appropriate; no explanation for the unusual aggression is revealed.
10. **Make the experimental potion.** Interact with the workbench. Separate Bloodleaf tips, clean/bruise Silvermoss, adjust the milk measure to 1.00, combine leaf → moss → milk, set gentle heat within 0.35–0.55, stir three times, then decant. Try one wrong choice: Marlow should intervene without consuming the batch. Step away with Esc halfway through and reopen the bench; the stage must be retained.
11. **Treat the troll.** Give it the experimental potion. Watch its head lift toward the food and its color shift toward green over a few seconds. Speak with Marlow. Potion Making and the Health Potion recipe unlock; inventory gains one remaining Health Potion. Repeating the conversation/treatment must not duplicate rewards.
12. **Tell Garrick.** Go upstairs, tell him it worked, and inspect the board. The three titles should be selectable, each explaining that its contract content is not implemented.

## Controls

| Input | Action |
|---|---|
| WASD / arrows | Explore |
| E | Nearest interaction |
| I | Inventory, equipment, samples and unlock status |
| M | Select combat movement; click a blue hex |
| 1 | Select Attack; click the enemy in valid range |
| 2 | Commit Defend immediately |
| 3 | Select Item; choose a consumable to commit |
| 4 | Commit Dash immediately |
| Space | End turn |
| Esc / right-click | Cancel combat selection or close menu; never undo committed actions |
| Flee button | Spend Primary Action at a grid boundary |
| Potion sliders/buttons | Measure, combine, heat and stir; Esc preserves the current preparation |

## Targeted checks after the main route

- Retry a defeat: encounter HP/consumables restore to the checkpoint, while previously gathered ingredients and opening milestones remain.
- Return early with missing ingredients: Marlow should explain what is needed and allow you to leave again.
- Inspect the board before treating the troll: it stays locked.
- Test Spear and Bow on fresh runs. The loan system still retains previously tried weapons so equipment switching can be tested; switching during combat consumes the Primary Action.
- Pause the brew and visit another lab object. Returning must resume the same stage.

Everything remains placeholder: geometry, materials, creature poses, dialogue presentation, and mixing controls. The lab uses an explicit stair transition to a separate room in the same scene. The first supervised recipe is the only brewing content; no extra batches, full profession system, Hunting contracts, Cooking, Smithing, save/load or Blender assets are added.

After this playtest, the smallest next step is to fix any reported targeting, route or pacing issues in this opening before expanding content.
