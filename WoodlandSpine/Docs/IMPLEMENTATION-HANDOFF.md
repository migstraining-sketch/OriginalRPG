# Coordinated opening implementation handoff

Branch: `implementation/coordinated-opening-slice`. Gameplay authority reviewed at `631f087`; subsequent main `c2b845c` adds only `CHARACTER_VISUAL_DEVELOPMENT.md`, which does not authorize art production or alter gameplay. This branch does not implement the complete three-route opening.

## Ownership and state boundaries

| Area | Files / responsibility |
| --- | --- |
| Combat rules | `Combatant`, `CombatRound`, `CombatModel`, `HexGrid`: units, frozen scheduler, authoritative legality and deterministic effects |
| Combat presentation | `BattlePresentation`, `BattleHUD`, `CombatViewport`, `WorldBuilder.ShowGrid`: actor bindings, picking, labels, tray and highlights |
| Integration | `CoordinatedSlice`: routes explicit interactions, outcomes and objectives to independent state owners |
| Travel | `CoordinatedState.TravelKnowledge`, `RegionalTravel`: known destinations, front-door commitment, regional visibility, arrival |
| Opening | `InnConversation`, `ReactiveIntro`, `FirstLabVisit`, `MarlowOpening`, `OpeningState`: knowledge and explicit commitment, treatment priority |
| Herd | `MooncalfHerd`, `HerdState`: separately living members, reusable flask, viable source, failed-treatment continuation |
| Mud | `MudInTheMoonrice`, `MudInvestigation`: any two clues, early preparation, observed feeding, Cull then Harvest, reward |
| Ily | `CompanionPresence`: physical location, local presence, recruited roster vs active travel, controller preference, recovery |
| Player/context UI | `PlayerPanel`, `SliceHUD`, `RoomStorage`: state readers and explicit transfers; Journal owns notes |
| World blockout | `WorldBuilder`, `OpeningWorld`, `HuntingWorld`, `AmbientPatron`: common room, lab, upper rooms, kitchen, Woodland and Reedwater |
| Data | `SliceData`, `EnemyData`, `Resources/*.asset`: editable defaults including new herd/Reedback assets and Ily HP |
| Validation | editor `GroupCombatValidation` / `CoordinatedValidation` plus existing suites; opt-in player `CoordinatedSmoke` |

`SliceGame` remains the existing entry point and orchestrator. New UI does not own quest progress. Region groups preserve child active states; changing regions must not reactivate a dead creature or reveal a remote encounter. There are no frame-by-frame broad scene scans.

## Invariants to preserve

- `CombatModel.Hero` is the player. Legacy `playerHP/playerCell/inventory` accessors refer to the acting allied unit inside combat; use Hero for exploration synchronization. `Current` exposes the actual resolving unit for HUD labels.
- Multiple Ready allies choose the next allied slot; Independent control resolves through the same rules. Controller changes occur before combat. Local helpers count toward the ordinary three-allied-participant cap.
- Removing a participant skips its existing slot. The next round rebuilds buckets. Defend expires when its owner starts another activation. Focus never rewrites committed threats.
- Actor picking, grid highlighting, hover text and click acceptance call model legality. Empty tray margins never become a hidden world-click rejection area; the camera reserves the tray's footprint.
- `MarlowInterest` is not `MarlowJob`. Invitation, curiosity, entering the lab, ordinary Continue and Esc do not imply acceptance. Esc leaves decisions visible.
- Return with ingredients precedes optional Mossback discussion. No Mossback kill gate on treatment. No backup milk pail or failure timer. Source destruction before collection is genuinely final for that treatment attempt.
- The lent field flask is a real inventory entry. Milk is carried in that container; preparation returns the clean flask. The first loan happens once, including when the flask is stored.
- Mud preparation works without clue flags. Two clues can support inference, but do not compel Manage/Cull. Witnessed feeding or explicit Harvest determines valid completion. Killing after Manage invalidates that Manage result until Harvest. Rewards do not duplicate.
- Personal storage transfers real item references/counts. Equipped gear is explicitly unequipped on storing. Completed transfers survive panel cancellation and regional travel. Storage is session-persistent because the prototype has no disk save system.
- Free hearth Rest restores the player and present companions, including eligibility. No rental, price, quality, fatigue or timer is attached.

## Verification and outstanding acceptance

The build script regenerates missing assets, runs all editor suites, and produces the Windows executable. The opt-in runtime runner tests the integrated success route and independent failure/Manage/Cull/Ily/stress cases. It directly invokes some authored interactions and uses fixtures for exceptional states; it is not a full human input replay.

See `Validation/STATUS.md` for final counts and `PLAYTEST-OPENING.md` for manual acceptance. Camera render evidence shows actual runtime placeholder geometry, not the IMGUI interface. Host full-window capture returned black frames, so UI screenshot acceptance and natural pacing/timing remain unverified. The former chest obstruction was found by a CharacterController walking check and fixed before delivery.

No design contradiction or gameplay-rule exception was required. Remaining tuning includes camera/readability at the user's window size, dialogue staging, terrain spacing, Independent ally feel and natural first-play length. Do not promote prototype prose, creature tuning or placeholder silhouettes into canon. Next production step is a human playthrough of these routes, followed by targeted fixes; later Sable/Nessa routes and final Blender art remain separate work.
