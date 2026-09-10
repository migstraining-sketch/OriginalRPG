# WoodlandSpine — Coordinated Opening Development Slice

Playable Unity prototype for **Marlow → Woodland → Mud in the Moonrice + Ily**, now using the owner-requested Blender inn study. This is not the complete three-route opening. The repository's design documents remain authority; prototype tuning and prose do not establish new canon.

## Open and play

1. Add this `WoodlandSpine` folder in Unity Hub with **Unity 6000.5.6f1**.
2. Open `Assets/Scenes/Opening.unity`, then press Play. An empty Untitled scene contains no game.
3. Start inside Garrick's Inn with immediate control. Speak to Garrick; enter your name when he asks. Follow Marlow only if you choose to hear him out.

If assets/scene need regeneration, use **Woodland → Generate initial scene and data** (or run `Tools/Build-Prototype.ps1` from this folder). The build tool validates rules, creates missing data assets and writes `Builds/Windows/WoodlandSpine.exe`. Quit that executable before rebuilding. Builds and Unity caches are not committed.

## Controls

- WASD / arrows: move relative to the camera in exploration. E: nearby interaction.
- Q/R or middle drag: rotate camera. Wheel: zoom. Shift + middle drag: exploration pan. Home: reset. Combat keeps the full board in view.
- I or top tabs: Player Panel (Inventory, Equipment, Character, Techniques, Journal).
- Dialogue: click an authored response; Space/Enter advances ordinary Continue. Name entry has its own confirmation. Esc never hides or answers dialogue.
- Combat: M Move, 1 Attack, 2 Defend, 3 Item, 4 Dash, 5 weapon technique. Select a target or destination by clicking; Enter confirms applicable actions. Space ends the current unit's activation.
- Click a Ready ally to assign the next allied slot. Direct allies accept your orders; Independent allies resolve immediately. Controller preference is set outside combat.
- Esc / right-click cancels uncommitted combat selection, closes ordinary panels, or cancels a pending storage quantity. Spent movement/actions and completed transfers remain committed.
- Walk through the front entrance or outdoor road boundary to open the Regional Map (E also works nearby). Select a known destination, then commit travel. After Stay here, step back before crossing again. Back/service door is local property only.

## What connects in this build

The revised inn introduction leads to Marlow's small, forward-moving lab conversation and explicit job acceptance. The clean field flask supports peaceful milk collection from a living nursing Mooncow. Bloodleaf, Silvermoss, wildlife and Mossback occupy the woodland. Five optional discoveries add observations to Knowledge without inventory items or completion counters.

Returning ingredients prioritizes treatment, then supervised Potion Making and the troll. Destroying the viable nursing source before collection immediately replaces the impossible objective with Return to Marlow. Reporting that failure closes treatment without Potion Making and still opens Garrick's board. Hostility by itself does not fail the job.

All three canonical Hunt postings are visible. Only **Mud in the Moonrice** is selectable in this development slice; the others do not reveal Regional Map destinations. Reedwater supports flexible clues, early wet-margin preparation, witnessed feeding redirection (Manage), or combat followed by explicit manual Harvest (Cull). Valid completion unlocks Hunting. Ily is physically present, can fight Direct or Independent, and is optionally recruited afterward. Toma's provisions/payment lead back to Garrick and Sylvie's existing Cooking lesson.

The imported Blender inn includes baked wood/stone/textile materials, occupied common-room seating, accessible board, physical basement stairs, kitchen, two connected upstairs flights and four modest guest rooms. The camera sections the upper floor and front/east walls for visibility. Rental remains paid once. The room chest transfers real carried inventory and equipment. Storage persists across regional visits **within the running game**; this prototype has no disk save/load yet. Free Basic Rest at the common-room hearth restores the player and present companions without requiring rental. Blender is not required to play or open the Unity project; the source model and export instructions are in `../ArtSource/Inn`.

## Combat

Combat stays on a hex grid over its encounter space. Units own HP, equipment, movement, Primary Action, controller, Defend and committed intent. Ordinary participant limits are 1–3 allies and 1–6 enemies. Frozen round schedules distribute enemies across allied slots; removed units skip slots until the next round rebuild. Defend lasts until that unit's next activation.

Fixed damage, weapon geometry, terrain, LOS, signatures and committed Pounce/Rush/Charge share model rules with highlights, hover reasons and click acceptance. Every committed threat remains visible even when another enemy is selected. The camera reserves actual space for the compact tray so the battlefield is not hidden behind its border. After victory or successful Flee, a defeated companion returns at 1 HP and needs Basic Rest before another fight.

Tune weapons and creature assets in `Assets/Resources`; `SliceRules` links them and exposes starting values including Ily HP. New creature defaults are prototype tuning, not an expansion of design canon.

## Verification and limits

Latest owner playtest fixes: [walking exits, camera controls, resting Mossback and troll reaction](Docs/PLAYTEST-FEEDBACK-FIXES.md). First editable [Garrick Blender study](../ArtSource/Garrick/README.md) is available separately; it is not final or integrated NPC art.

See [validation status](Validation/STATUS.md), [manual playtest](PLAYTEST-OPENING.md), and [implementation handoff](Docs/IMPLEMENTATION-HANDOFF.md). Automated checks use isolated runtime fixtures and do not measure a natural first playthrough or establish dialogue pacing/fun. The old 20–30 minute estimate is guidance only.

The inn is a visual-study integration, with baked albedo and real-time lighting rather than the exact offline Cycles appearance. Characters, the laboratory, outdoor art, animation and interface styling remain prototypes. No final character art, networking, later two Hunt routes or save system is included. The 3v6 stress fixture uses test actors; it is not an extra authored encounter. Inventory/storage contents reset when the application closes or Play mode restarts.
