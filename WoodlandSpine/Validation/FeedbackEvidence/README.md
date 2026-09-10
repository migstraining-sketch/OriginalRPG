# Owner playtest follow-up evidence — 2026-09-10

Final Unity 6000.5.6f1 Windows build: passed, no C# compiler warnings/errors. Editor validation: 4,668 assertions. Built-player scenarios: feedback 537, feedback-early 536, inn 159, success 130, failure 18, manage 27. All exited 0 with empty error transcripts. Check/result transcripts are copied here from the opt-in runtime scenarios.

Assembly-CSharp.dll SHA-256:
`7E33A657CE61C810F59021DFB4F1A39EAB6D042721F17679A6F6837E8B44E4D1`

The feedback scenarios walk a real CharacterController across each exit without invoking E or Open, cancel and re-enter the inn exit, verify the rest of the south wall is not a travel point, and commit Inn return through the physically opened Woodland map. Test fixtures place the actor for isolated staging checks. Mossback is checked both on return without reagents and at close range before pasture/wildlife victory while unarmed. Camera adjustments exercise the same controller method as input; every combat hex remains in the game viewport and the actual enemy can be picked at 0/90/180/270 degrees.

`resting-mossback-world.png` and `rotatable-combat-world.png` are real offscreen game-camera renders. They **exclude IMGUI** and do not prove map/HUD/dialogue visual acceptance. None of these runs is a native input replay or human pacing review. Use the updated playable build for that acceptance.
