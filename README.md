# OriginalRPG

Design continuity repository for an original fantasy RPG inspired by the charm, self-direction, permanence, and systemic depth of older RPGs and MMOs.

## Important project rule

This is an **original game**. AdventureQuest, BattleOn, RuneScape, World of Warcraft, Supernatural, One Piece, Fantastic Beasts, and other referenced works are inspirations/research only. Their characters, locations, chronology, names, lore, and proprietary content are **not canon** for this project.

Start with [`docs/START_HERE.md`](docs/START_HERE.md).

## Playable Unity prototype

The coordinated placeholder prototype is in [`WoodlandSpine/`](WoodlandSpine/README.md). This branch implements the approved **Marlow → Woodland → Mud in the Moonrice + Ily** development slice, not the complete three-route opening. Add that folder in Unity Hub using Unity **6000.5.6f1**, open `Assets/Scenes/Opening.unity`, then press Play. An empty Untitled scene will not run the game.

See the [opening playtest guide](WoodlandSpine/PLAYTEST-OPENING.md) and [implementation handoff](WoodlandSpine/Docs/IMPLEMENTATION-HANDOFF.md). Source, scene, tunable assets, project settings and build tools are included. Unity generates its caches locally; Windows builds are generated with `WoodlandSpine/Tools/Build-Prototype.ps1` and are not stored in Git.

The design documents above remain the continuity authority. Prototype defaults and unfinished behavior do not promote themselves to locked canon.

## Current workflow: approved implementation under review

The owner approved the coordinated implementation after final blocker review. Changes are on `implementation/coordinated-opening-slice`, for pull-request review rather than direct merge to main. See the [validation status](WoodlandSpine/Validation/STATUS.md). Human dialogue, UI and pacing acceptance is still required. Future design packages retain review-first development; this approval covers the named slice only.