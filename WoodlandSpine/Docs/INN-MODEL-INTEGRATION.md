# Playable Blender inn integration — 2026-09-10

The owner-requested `GarricksInn_Visual_v02.blend` is integrated into the existing Opening scene and Windows player. It replaces the old common-room, kitchen and guest-room primitives. The laboratory and outdoor locations retain their existing prototype art.

## What changed

- Source: `ArtSource/Inn/GarricksInn_Visual_v02.blend` at repository root, preserved unchanged. SHA-256: `AADBA0EF030228D2C1A9C2712F888C7CF504E2F432DF484869E0ABCF825A1628`.
- `Tools/Export-Inn.py` evaluates Blender geometry, preserves source Generated coordinates for albedo baking, exports texture atlases and writes explicit Unity-axis geometry. `Assets/Editor/InnModelImporter.cs` creates the model, Standard materials and combined static collision. Neither Unity users nor players need Blender installed.
- `Assets/Art/Inn` contains the self-contained imported geometry and baked textures. Fourteen source render groups replace thousands of separately editable Blender pieces. Static structural/furniture collision is combined by zone. Small ornaments do not obstruct the controller.
- `InnLayout` centralizes door, room, sample, travel and stair positions. `InnVisual` binds the imported geometry to existing interactions, door permissions, six ambient patrons, cutaway layers and warm lighting. `SliceRules.innModel` references the imported asset directly.
- `WorldBuilder`, `HuntingWorld` and `CoordinatedSlice` no longer generate duplicate inn furniture/rooms. Existing dialogue, inventory, contracts and combat remain their own state owners.
- `FirstLabVisit` follows the new basement route. The retained lab sits below its actual landing. Upstairs has two walkable connected flights. Kitchen entry and the paid-room shortcut retain their existing interaction behavior; both stairways can also be walked physically.
- `RegionalTravel`, `FullOpening`, `SampleDrop` and `SliceCamera` use the new geography. The front-door loan interruption occurs near the actual exit. Storage contents still survive regional travel within the current session. Camera framing keeps the inn/rooms in view; upper floor and front/east walls use a cutaway while collision stays active.

The exported study needed three geometry adaptations discovered during integration: an actual passage behind the basement door, the weapon display shifted forward on the public side of the bar, and Marlow's spare chair moved to his table's west side. Those furniture moves opened the kitchen/basement approach. Geometry and collision agree; the original study remains untouched.

## Validation

Unity 6000.5.6f1 Windows development build: PASS, no C# compiler warnings or errors. Existing editor suites: **4,664 assertions passed**.

Built-player checks: **inn 159**, **success 130**, **manage 27**, all passed. The inn case verifies baked texture references, six patrons, actual CharacterController travel to the public bar/board, both upstairs flights, bedroom/storage access, return downstairs, kitchen access, the physically locked basement leaf, and the merchandise approach. The success case follows Marlow downstairs and back, completes wildlife/Mossback combat and treatment, travels between regions, rents a room, and transfers storage across a regional round trip. Manage covers Mud's nonlethal resolution, Ily recruitment, return to Garrick and kitchen/Sylvie access.

Ground-floor test routing searches collision for a walkable path; the actual character controller traverses it. It does not add pathfinding or automated player movement to the game. Scripted interactions call authored callbacks. These checks do not establish human dialogue pacing or replay every mouse/keyboard input.

Current player assembly SHA-256: `AFD69FBA4BB647A7125F28E01334418896EC078FF5A050DC21F397E4F2593A59`.

Evidence: `Validation/InnModelEvidence`. PNGs are actual player-camera world renders, excluding IMGUI. No full-window HUD capture or final-art parity with the original reference collage is claimed.

## Playtest

Launch `Builds/Windows/WoodlandSpine.exe`, or open `Assets/Scenes/Opening.unity` with Unity 6000.5.6f1 and press Play. Begin a new session. WASD/arrows move; E interacts; I opens the player panel. The same opening and Mud/Ily gameplay remains available.

Prioritize walking through the inn, following Marlow down/up, checking the upstairs camera/room chest, and returning from Reedwater to Sylvie. Character models and laboratory/outdoor art remain placeholders. Albedo is baked from Blender; illumination is real-time Unity lighting, so the result differs from the offline Cycles previews. Progress/storage still reset when the application closes; this pass adds no save system.

Authority refreshed from GitHub main `7aed489`: new Garrick/Ily character reference briefs do not change inn layout/gameplay. Those final character models are outside this environment integration. Source documents retain their authority hierarchy. Work remains on `implementation/coordinated-opening-slice`; no main merge is performed.
