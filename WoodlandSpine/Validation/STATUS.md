# Current validation status

**Latest visual trial: illustrated Garrick, 2026-09-10.** [Play instructions](../Docs/GARRICK-ILLUSTRATED-TRIAL.md). Final Windows build passed **4,668 editor assertions** with no C# compiler warnings/errors or shader errors. Final built-player runs: **garrick-art 31**, **inn 160**, all passed with empty runtime error transcripts. [Art evidence and build hashes](GarrickArtEvidence/README.md). Verified GPU background removal, counter occlusion, camera following/pan, retained bar interaction, region roundtrip visibility and inn traversal/rooms/storage. This is a static visual trial; natural appearance and usability remain subject to owner playtesting. Other runtime suites below are historical and were not rerun for this art change.

**Latest follow-up: walking exits, camera controls and creature/dialogue reactions, 2026-09-10.** [Change notes](../Docs/PLAYTEST-FEEDBACK-FIXES.md). Windows build passed with no C# compiler warnings/errors and **4,668 editor assertions**. Latest player runs: feedback 537, feedback-early 536, inn 159, success 130, failure 18, Manage 27 — **1,407 checks, all passed**, empty runtime error transcripts. These exercise real controller road crossings, cancellation/re-entry, actual return travel, both Mossback wake paths, rotated combat fitting/picking, and retained opening/rooms/failure/Mud flows. [Current evidence](FeedbackEvidence/README.md). Direct/Independent/group-stress tests below are older evidence; no scheduler rules changed in this follow-up.

Prior follow-up: [playable Blender inn integration](../Docs/INN-MODEL-INTEGRATION.md). Its 4,664 editor assertions and environment evidence remain historical; the latest runs above supersede its overlapping cases.

**2026-09-10 — Coordinated Opening Development Slice / Marlow → Woodland → Mud in the Moonrice + Ily.** Implementation branch: `implementation/coordinated-opening-slice`. No main merge. This supersedes the prior dialogue-only status; older reports remain historical evidence.

## Build and editor checks

Unity **6000.5.6f1**, Windows x64: **PASS** (`SLICE_BUILD_SUCCESS`). Final compilation reports no C# warnings or errors. Build command: `Tools/Build-Prototype.ps1` from `WoodlandSpine`.

| Suite | Assertions |
| --- | ---: |
| Deterministic rules | 169 |
| Opening state | 35 |
| Reactive introduction | 34 |
| Retained opening/profession rules | 99 |
| Dialogue/agency | 133 |
| Combat viewport | 1,536 |
| Combat refinements / signatures / intent | 2,460 |
| Group combat | 153 |
| Coordinated travel, herd, Mud, storage and Esc | 49 |
| **Total** | **4,668** |

Group schedule cases include 1v1, 1v2, 2v1, 2v2, 3v3, 2v5, 3v5 and 3v6. Tests cover caps, casualty skip without rebucketing, Direct/Independent slots, Defend across rounds, occupancy, recovery eligibility, and Bow legality/reason agreement. Existing refinement tests retain geometry, LOS, Pounce/Charge/signature/cancel checks. Schedule checks are not equivalent to human playtesting every group size.

## Built-player scenarios

| Case | Checks | Scope |
| --- | ---: | --- |
| success | 110 | Name/intro, physical basement and rented-room walking, lab agency/Esc, peaceful milk, actual wildlife/Mossback victory, treatment, board, free Rest, rental, storage/travel roundtrip |
| failure | 18 | Three-member herd combat; source-loss objective; Flee; failure report without Potion Making; board and Reedwater continuation |
| manage | 27 | Both terrain preparations before inference, observed redirected feeding, provisions, recruitment and kitchen handoff |
| direct | 29 | Ily Direct + Hero ordinary-rule combat; kill alone insufficient; Harvest, reward, recruit, return |
| independent | 29 | Ily Independent combat through same scheduler; Harvest and handoff |
| stress | 35 | 3v6 participant setup, six individually picked enemies at four camera angles, frozen schedule then 3v5 next round, recovery |
| **Total** | **248** | **All passed** |

Scenarios are opt-in (`--coordinated-smoke --case CASE`); normal launch is untouched. They invoke authored callbacks for some interactions, use fixtures for exceptional states (including destruction of the nursing source), and explicitly advance combat in tests. They are not a human input replay. Combat completion uses ordinary HP, movement, actions and consumables. Separate rule checks cover aggression without source loss and all two-clue combinations.

Runtime result/check transcripts are copied into `CoordinatedEvidence`; raw captures and host logs remain local under ignored `Coordinated/` and `*.log`. A physical traversal test found a chest blocking the rented doorway; its position was corrected and the walk/store/travel/withdraw route passed afterward.

## Evidence limits / known limitations

- [World renders](CoordinatedEvidence/README.md) show real runtime placeholder geometry. They exclude IMGUI. Full-frame capture produced black frames, and native window capture was unavailable on this host. No visual acceptance of dialogue, map, storage or HUD is claimed.
- Natural first-play timing, dialogue pacing, subjective combat feel and UI readability in the user's normal window remain **unverified**. Use `../PLAYTEST-OPENING.md` for these checks. Historical 20–30 minute targets are not acceptance thresholds.
- Chest contents, party and progression persist within the running session, not across application/Play-mode restarts. There is no disk save/load implementation.
- Ily's exploration follow motion is a simple prototype follower; complex obstacle navigation and final animation are not validated.
- No material design blocker or deliberate gameplay-authority deviation was needed. No known failing automated check remains. This does not establish a complete opening, three implemented Hunt routes, final art or guaranteed enjoyment.

Final generated executable SHA-256: `95D0C3362277726ADCE36509ACE867138EDB472DA0D02DB64EB4BEEAFF0C29BC`. Unity build outputs are local and excluded from Git.
