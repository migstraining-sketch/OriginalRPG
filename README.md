# OriginalRPG

Design continuity repository for an original fantasy RPG inspired by the charm, self-direction, permanence, and systemic depth of older RPGs and MMOs.

## Important project rule

This is an **original game**. AdventureQuest, BattleOn, RuneScape, World of Warcraft, Supernatural, One Piece, Fantastic Beasts, and other referenced works are inspirations/research only. Their characters, locations, chronology, names, lore, and proprietary content are **not canon** for this project.

Start with [`docs/START_HERE.md`](docs/START_HERE.md).

## Playable Unity prototype

The existing placeholder prototype is in [`WoodlandSpine/`](WoodlandSpine/README.md). It does **not** implement the newest coordinated design package. Add that folder in Unity Hub using Unity **6000.5.6f1**, open `Assets/Scenes/Opening.unity`, then press Play. An empty Untitled scene will not run the game.

See the [opening playtest guide](WoodlandSpine/PLAYTEST-OPENING.md) and [implementation handoff](WoodlandSpine/Docs/IMPLEMENTATION-HANDOFF.md). Source, scene, tunable assets, project settings and build tools are included. Unity generates its caches locally; Windows builds are generated with `WoodlandSpine/Tools/Build-Prototype.ps1` and are not stored in Git.

The design documents above remain the continuity authority. Prototype defaults and unfinished behavior do not promote themselves to locked canon.

## Current workflow: review before gameplay coding

The owner requested review-first development on 2026-09-10. Review each updated design package for Central Brain before implementing it. Gameplay coding is currently on hold; a document being marked locked does not by itself lift that hold. See the [current package review](docs/reviews/2026-09-10-coordinated-package-review.md) for recommendations awaiting decisions.
