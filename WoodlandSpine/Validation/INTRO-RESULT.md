# Historical result notice

This report describes an earlier pass. The superseded PLAYTEST-INTRO guide has been deleted; use [PLAYTEST-OPENING](../PLAYTEST-OPENING.md) for the existing prototype route. References to that earlier guide below are historical evidence, not current instructions.

# Reactive introduction validation — 2026-09-08

## Automated checks actually performed

- **Unity 6000.5.6f1 import and Windows development build: PASS.** No C# compiler diagnostics remained.
- **236 assertions passed in the Unity Editor:** 169 combat/rule checks, 33 existing opening-state checks and 34 reactive-context checks. Supplementary standalone source compilation and model simulations also passed.
- **256 assertions passed in the built player's `--intro-smoke` test.** These include direct Garrick, kitchen, rooms, basement, board, Marlow-first, merchandise looking/taking, seated idle, leaving and return. The test physically walks CharacterController routes to the six principal first-interaction prompts. Leave/return runs with the normal game update active and no weapon equipped.
- Branches converge through the same graph, retain interrupted context, suppress repeated room/board explanations and do not give the ingredient list in the inn. Invitation precedes warning; the basement remains private beforehand. Resuming the incident does not drop another sample.
- Laboratory entry returns exploration control with no forced conversation. The short free-exploration interval, inspect interactions, troll reveal, refusal/free exit, reconsideration and acceptance/objective were exercised.
- Move, Attack, Item, Dash and Defend selection cancellation passed. Confirmed Dash and movement remain spent after cancel.
- The **existing full-loop runtime regression also passed** after adapting its initial dialogue path. Sword/Spear/Bow, gathering, brewing and later unlock scaffolding continue to work. This is engineering regression coverage, not approval or expansion of that later narrative.

## Presentation and test limits

The inn, incident and lab camera renders were inspected. Inn/lab floating destination labels are absent; lunch, stock, board, stairs, kitchen hatch, lab equipment and the pale troll are visible. Camera staging was refined so the entrance and incident sit clear of the lower dialogue area.

The test runner uses public gameplay commands for conversations and combat. It does not perform a human mouse/keyboard playthrough or validate the emotional pacing. Idle threshold is tested with **simulated elapsed time**, not a person waiting 70 seconds. Scene captures do not include the rendered HUD; text fit was reviewed from layout/source, and human readability remains a playtest item. Earlier hidden-window screenshot capture failed, so the final test writes direct camera renders instead.

No known failing functional checks remain. The intended 5–8 minute experience still needs the ordered human test in `PLAYTEST-INTRO.md`.

## Deliberate placeholders / deviations

- Vial drop/break is visual and silent; no audio or final character animation was added.
- Sitting is a simple placeholder pose. Basement traversal remains the existing explicit same-scene stair transfer.
- Refusal records state/history for future consequences; there is no troll-death timer or Marlow departure.
- The existing later woodland/potion content is retained as non-canonical engineering scaffolding. This pass authors only through accepting/refusing the expedition.
- No final art, Blender assets, full economy, crime, rooms/rest or town systems were added. Progress remains session-only.
