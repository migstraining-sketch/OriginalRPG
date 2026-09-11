# Garrick's Inn source model

`GarricksInn_Visual_v02.blend` is the editable Blender 5.2.1 environment study requested by the owner. It is now the source for the playable inn. The separate original visual-study output is preserved unchanged.

The current model includes placeholder people, not approved final Garrick/Marlow/Sylvie/Ily character art. Latest upstream reviewed for integration: `7aed489` (new Garrick/Ily visual reference documents; inn spatial authority unchanged). Owner authorization to integrate this model supersedes the older documentation-only coding hold.

The 2026-09-10 [illustrated Garrick trial](../../WoodlandSpine/Docs/GARRICK-ILLUSTRATED-TRIAL.md) separates the source figure's uniquely named material into a `GarrickProxy` renderer during Unity import. The runtime can hide that figure and display the new art, or restore it with F8. The source Blender model, exported binary, furniture and collision remain unchanged.

To re-export, open this file with Blender in background mode and run `WoodlandSpine/Tools/Export-Inn.py`. It evaluates the geometry, bakes procedural albedo into texture atlases, and writes `WoodlandSpine/Assets/Art/Inn`. It never overwrites the source blend. Unity's small local `InnModelImporter` imports those files without a Blender installation or third-party plugin on the player's machine.

The export opens the basement enclosure behind its door, separates doors/stairs/floors for gameplay visibility, and omits the static Marlow figure/sample in favor of existing moving gameplay props. Vertex coordinates and triangle winding are converted explicitly from Blender Z-up to Unity Y-up at metre scale. Collision uses structural meshes and selected furniture; tiny cups, textiles and ornaments do not obstruct walking.

Unity capsule checks exposed two furniture bottlenecks in the visual study. The game export moves the weapon display forward on the public side of the bar and moves Marlow's spare chair to the west side of his table. This opens the kitchen/basement approach while retaining both fixtures and the original footprint. Rendered geometry and collision move together; the source study remains preserved.

Runtime anchors, lighting, cutaway visibility and door states live in `InnLayout` / `InnVisual`. The upper floor and the front/east walls use a camera cutaway; their floor/wall collision remains active. The retained laboratory connects below the model's basement landing. Materials are baked albedo with real-time Unity lighting, so the player view is not identical to the offline Cycles illustration.
