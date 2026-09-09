# Approved combat refinement — 2026-09-09

Implemented against main baec2e2, including CORE_SYSTEMS_PROGRESSION's locked package, DESIGN_GUARDRAILS and the updated Central Brain frontier. The older sentence requesting a separate implementation specification remains in Core Systems; this pass follows the user's direct implementation request and the newer explicit START_HERE/Central Brain instruction to implement and playtest this package. No proposed alternatives or later systems were added.

## Player actions

Basic attacks retain their existing values and geometry. Key 5 selects the equipped weapon's Signature; click the enemy or press Enter to confirm. Esc/right-click cancels without spending anything. Each technique spends the normal Primary Action, with no cooldown or new resource.

- Sword Lunge: target exactly two hexes away on a straight axis, enter the free intervening hex and deal 4 before Armor. Open ground costs no Movement; Difficult terrain costs its full normal 2 Movement and insufficient remaining Movement rejects the action. This is the documented implementation choice for the spec's unresolved terrain treatment. No jump through blockers or occupied cells.
- Spear Drive: 4 before Armor at straight range 1–2, then push one hex directly away. Blocked/out-of-grid/occupied push destinations leave the enemy in place; damage still applies. No collision bonus.
- Bow Quick Shot: 3 before Armor at exactly range 1 with clear grid LOS; no movement. Basic Bow remains 5 at radius 2–4, including off-axis cells, with LOS.

Signature damage and enemy Pounce range/enablement are serialized inspector values. The build does not overwrite those tuning values.

## Targeting and presentation

Raised-model clicks now select that actor's actual combat hex before a ground projection. Ground clicks use the encounter plane's elevation, then HexGrid.At. Attack validity rejects off-grid and blocking cells. Camera angle is irrelevant to combat legality. Highlights and action confirmation share CombatModel.CanTarget; a red enemy highlight means the selected attack is legal. The existing safe viewport and Enter fallback remain.

The HUD shows one Signature for the equipped weapon. Pounce marks the committed landing hex in amber, including when occupied by the player. Charge retains its amber lane. Intent describes dangerous space rather than prescribing where to move.

## Enemy patterns

| Enemy | Behavior | Tell | Exploitable rule | Later remix potential (not implemented) |
|---|---|---|---|---|
| Woodland creature | Pursue until in range, commit to Pounce | Pounce intent and amber landing hex; starts about 3 hexes away | Landing does not follow the player. A miss ends the enemy action there. Defend mitigates a hit. Blocking terrain can interrupt it. | A later relative could gain limited redirection. |
| Mossback | Pursue and commit to straight Charge | Intent and amber lane | Cannot steer after windup; blocking trees stop it and cost the following enemy phase. | A later charger could break light cover. |

Pounce retains 10 HP / 0 Armor / 4 attack and has tunable range 3. On a hit it ends adjacent, avoiding unit overlap. On a miss it lands on the marked empty hex without a follow-up swipe.

The Mossback layout already contained three meaningful blocking trees and difficult patches. Validation now proves a collision lane for each obstacle. Drive preserves the locked lane; a displaced Mossback regains the original charge origin before continuing its committed charge. It does not silently cancel or retarget. Position changes are immediate placeholder movement, not finished animations.

## Validation and limits

Editor coverage includes all radius-2/3/4 Bow targets, six-direction Signature cases, mud budgets, blocked pushes, Pounce dodge/Defend, charge after displacement, and model/ground clicks from four camera angles at two encounter elevations, including near-border clicks. Built-player checks exercise Signature selection/cancel/confirmation and full-opening fights with all three weapons. See Validation/STATUS.md for actual results.

This pass does not approve dialogue, complete Cooking/Hunting fidelity, add mastery trees, introduce hazards, or implement save/load. The earlier continuity audit's combat row predates this package; its non-combat gaps remain. The next production step is a human playtest of the new positional choices and target acceptance, then targeted tuning based on observed problems.
