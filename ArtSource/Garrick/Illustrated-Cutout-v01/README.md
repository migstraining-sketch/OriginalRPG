# Garrick illustrated cutout: playable visual trial

Owner approved the illustrated look and explicitly authorized testing a 2D cutout inside the inn on 2026-09-10. This is an isolated visual implementation, not a change to the entire game's art/animation pipeline.

The source asset is `WoodlandSpine/Assets/Art/Characters/Garrick/Garrick-Keyed.png`, generated with the built-in image tool. See [PROMPTS.md](PROMPTS.md) for exact prompts and the rejected fake-transparency attempt. The selected RGB source uses a green key background, removed by `Woodland/Illustrated Character` while rendering. No painted checkerboard is used in the game.

Runtime installation is in `InnVisual` / `IllustratedInnActor`. The inn importer separates the old figure's uniquely named `Garrick scale silhouette` material from the combined Common mesh. Only that figure's triangles are relocated into a separate renderer. Its original material and positions remain available for comparison. Furniture, collision, dialogue, interaction anchors, inventory and story rules are untouched.

The new artwork stands at Garrick's original location behind the bar. It faces camera yaw and uses normal depth testing, so the counter and other solid geometry can hide it. **F8** toggles the illustration and the original scale figure. Existing Q/R and middle-drag rotation, wheel zoom and Home reset remain available.

Limits: one static pose and one painted viewing direction; turning the camera does not reveal a true rear view. No idle animation, lip sync or final sprite animation is provided. NPCs other than Garrick remain placeholders. The source PNG is not transparent outside Unity's keyed material. This is a visual trial to judge the approved illustrated look in context.

The opt-in built-player `--coordinated-smoke --case garrick-art` scenario checks public-side interaction, camera following, comparison toggle, real GPU background removal/depth occlusion, and Inn/Woodland visibility. Rendered evidence and execution status are documented in `WoodlandSpine/Docs/GARRICK-ILLUSTRATED-TRIAL.md` after verification.
