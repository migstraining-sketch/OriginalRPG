# Illustrated Garrick evidence — 2026-09-10

Final Windows x64 build: **PASS**, Unity 6000.5.6f1. All **4,668 editor assertions** passed; no C# compiler warnings/errors or shader errors were reported. Final built-player runs: **garrick-art 31 checks**, **inn 160 checks**, both PASS with empty runtime error transcripts. These cases were rerun on the final build. `verification.json` records hashes of the Unity launcher, game assembly and unmodified source artwork. The launcher hash alone does not identify gameplay changes.

- [Normal inn framing](01-inn-world.png)
- [Normal gameplay camera, close zoom and pan toward the bar](03-gameplay-close-zoom-world.png)
- [Diagnostic bar close-up](03-bar-close-up-world.png)
- [Original figure comparison from the same diagnostic camera](04-original-comparison-world.png)

These are actual Unity world renders, not generated mockups. They exclude IMGUI. The diagnostic camera provides a closer composition for checking depth and edges; it is not the default gameplay view. Camera pan and zoom can reproduce the separate gameplay close view using ordinary controls.

The visual case verifies source proportions, single active Garrick visual, no new collider, retained public-side interaction, camera yaw following, close pan framing, F8-equivalent comparison state, actual GPU key removal with no leaked green pixels, foreground depth occlusion, and Inn/Woodland roundtrip visibility. The inn case retains room/access/storage and physical traversal checks. Tests call some gameplay methods directly and do not constitute human visual acceptance or a full mouse/keyboard replay.

The previous full opening/group-combat evidence remains historical; those runtime cases were not rerun for this isolated art change. See [play instructions and limitations](../../Docs/GARRICK-ILLUSTRATED-TRIAL.md).
