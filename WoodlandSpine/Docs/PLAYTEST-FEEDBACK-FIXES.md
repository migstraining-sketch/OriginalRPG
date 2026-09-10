# Walking exits, camera control and first reactions — 2026-09-10

Owner playtest follow-up on the coordinated opening development slice. Current GitHub main was fetched and checked at `7aed489`; the new character briefs inform Blender work. A final refresh also found the separately published `design/creatures-monsters-encounters` branch at `73151b5`. Its new `docs/CREATURES_MONSTERS_ENCOUNTERS.md` was read, but is not on main or merged into this branch. This note describes implementation/staging choices, not new upstream canon.

The new creature package names Gloam Lynx, Rootmuzzle and Ashwing Thrush and specifies physically connected stalking with two cues before the first confrontation. That broader encounter pass is **not implemented here**: the first hostile creature still uses the retained placeholder encounter. The package preserves the normally docile Mossback baseline and unexplained abnormal pursuit, consistent with this feedback fix. Recommended next gameplay step: implement the approved lynx stalking encounter and current creature identities without adding later wildlife population refresh rules.

## Play

Use `OriginalRPG/WoodlandSpine/Builds/Windows/WoodlandSpine.exe`, or open this repository's `WoodlandSpine/Assets/Scenes/Opening.unity` in Unity 6000.5.6f1. The older sibling `outputs/WoodlandSpine` copy is not updated. Restart the executable or Play mode to load these fixes; current sessions do not hot-update.

Walk out through the inn's front entrance or south along the Woodland trail. Crossing the road now opens the Regional Map automatically. E at the road remains supported. Select the known destination and confirm Travel. Stay here/Esc keeps the actor on the floor: step back into the location before crossing again. The inn's weapon loan still intervenes when needed. The rear kitchen/service door remains local-only.

The original return bug was a small E-only interaction with no crossing behavior. It let players continue past the marker. RegionalTravel now owns crossing/clamping/re-entry state, and Woodland banks close the gaps beside the southern road. Arrival positions sit inside each threshold so travel does not immediately reopen the map.

## Camera

- Q / R, or middle-button drag: rotate.
- Mouse wheel: zoom. Combat permits zooming out and returning to the fitted board, preserving all playable hexes above the action tray.
- Shift + middle-button drag: pan during exploration.
- Home: reset rotation, pan and zoom.
- WASD/arrows move relative to the camera in exploration.

Camera input pauses during dialogue and modal panels. Room/lab/region transitions clear the pan offset. Combat uses a centered frame, so exploration panning cannot hide tactical hexes. Rotation recalculates the fit using the actual camera viewport; picking continues to use that camera.

## Mossback and troll

Mossback now visibly rests off the main trail with a slow breathing motion. Visiting the pasture then returning toward its clearing wakes it, regardless of reagent completion. Approaching within three metres can wake it on the outward trip, including while unarmed. The observation offers space, then the awakened creature pursues. The old AllGathered gate is removed. Resting explains the outward-trip posture, not the later abnormal aggression; no corruption cause or visual was added. Retreat remains possible and a failed milk route is not required to kill it before returning.

The first troll scene keeps two forward branches: the calm question about illness, and “That's a troll. You brought him into the inn?” The latter leads to the existing river rescue and locked “He was drowning.” line. Surprise is optional and does not accept Marlow's job or assert that the troll is a pet.

## Blender

`ArtSource/Garrick` contains an editable outfit/proportion study and three actual rendered views. Its face is provisional because the approved sheet image was unavailable in repository/accessible conversation attachments. It is not final art, rigged, or substituted for the existing Unity NPC. See its README for the next modeling step.

## Validation

See `Validation/STATUS.md` and `Validation/FeedbackEvidence` for this build's results. The feedback scenarios exercise actual CharacterController movement through exits, cancellation/re-entry, return selection, early/return Mossback activation, exploration camera adjustments and combat viewport/picking at four rotations. They are scripted checks, not a human first-play or native mouse/keyboard replay. World captures exclude IMGUI.
