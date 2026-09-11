# Illustrated Garrick: playable visual trial

2026-09-10. The owner approved testing the illustrated character direction in the actual inn after rejecting the procedural Blender character studies. This adds one static painted Garrick behind the existing bar. Dialogue, story state, interaction location, collision and all other NPCs retain their existing behavior. This does not establish a final character pipeline or change gameplay authority.

## Play this version

Launch `WoodlandSpine/Builds/Windows/WoodlandSpine.exe` from the **OriginalRPG repository**. If an older game is already running, close it first. For Unity, open the repository's `WoodlandSpine` project with Unity **6000.5.6f1**, open `Assets/Scenes/Opening.unity`, and press Play. The older workspace `outputs/WoodlandSpine` folder is not this project.

- Walk to the public side of Garrick's bar with WASD; use E to talk as before.
- Scroll to zoom closer. Hold Shift and drag the middle mouse button to pan toward the bar.
- Q/R or middle mouse drag rotates the camera. Home restores the original framing.
- F8 switches between illustrated Garrick and the old figure for comparison.

The inn remains the current Blender environment study. The player and other NPCs still use placeholders. Garrick is a single upright image that faces camera yaw: moving behind him does not reveal a newly painted rear view. There is no idle animation, lip sync or character shadow in this trial. The illustration's baked lighting is not dynamically relit. Judge whether the illustrated direction reads well in play before extending it to more characters.

## What changed

- `InnModelImporter` now isolates only the old Garrick figure's uniquely named material into a separate renderer. Furniture and the source inn files are preserved.
- `IllustratedInnActor` installs the art at the original bar position, follows the camera, and supports the reversible comparison toggle. It adds no collider.
- `IllustratedCharacterAssets` preserves the source image's proportions and prepares a material that survives a Windows build.
- `IllustratedCharacter.shader` removes the green background and uses depth testing so the counter can cover Garrick's lower body. The unmodified source PNG is opaque RGB; transparency occurs in Unity, not in the source file.
- `InnVisual` and `SliceGame` connect the actor to the existing inn and camera. Leaving the inn hides it with the rest of the region.
- `SliceCamera` permits closer exploration zoom. Combat retains its existing minimum full-board framing.

Source image and exact generation prompts: [art-source notes](../../ArtSource/Garrick/Illustrated-Cutout-v01/README.md). The rejected checkerboard-background generation is not installed. No Blender model is being presented as this illustration.

## Verification

The final build and built-player checks are recorded in [Garrick art evidence](../Validation/GarrickArtEvidence/README.md). Tests cover ordinary public-side interaction, camera rotation and pan, original-figure comparison, actual GPU key removal and foreground occlusion, and travel away from and back to the inn. The inn regression retains traversal, room access and storage coverage.

The evidence images are actual Unity world renders. They omit the immediate-mode UI and are not a human mouse/keyboard playtest. The close-up labeled diagnostic uses a test camera; the gameplay zoom image uses the normal camera's zoom and pan controls. Natural appearance, pacing and comfort still need the owner's playtest.

Rebuild with `Tools/Build-Prototype.ps1`. Run the built player with `--coordinated-smoke --case garrick-art` for the visual checks, or `--coordinated-smoke --case inn` for the inn regression. Normal launches do not execute test fixtures. Build outputs and raw logs remain local and are excluded from Git.
