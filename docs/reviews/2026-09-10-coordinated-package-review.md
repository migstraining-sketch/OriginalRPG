# Coordinated opening package — review for Central Brain

Reviewed 2026-09-10 against GitHub main through `c7a8773`, including the coordinated revisions merged at `f30189d`. This supersedes the earlier opening review as the current assessment. It is a document/source review, not a new Unity playtest. Gameplay remains unchanged.

## Verdict

**Keep the direction. Resolve the small cross-system gaps below before issuing the implementation packet.** My design-potential rating is **8/10**. I am substantially happier with this revision, but I cannot honestly call documents a 10/10 game or guarantee Asmongold's reaction. This uses the project's [evidence-informed review lens](../PLAYER_EXPERIENCE_REVIEW_LENS.md), not impersonation.

The strongest idea is still a believable place whose creatures, people and terrain give the player useful information. The risk is that the opening becomes a long sequence of introductions, explanations and preparation before the player gets enough freedom to use what they learned.

The current owner instruction is review first, no gameplay coding. This review does not promote its own recommendations to canon.

## Improvements that resolve the previous objections

| Area | Current verdict | What improved |
|---|---|---|
| Group combat | Keep | Round schedules freeze; casualties/fleeing skip slots; mixed Direct/Independent allies share side slots; Defend lasts until that unit's next activation; the ordinary allied cap includes helpers. These close real implementation ambiguities. |
| Tactical information | Keep | Committed lanes/targets do not silently change. Dangerous committed telegraphs remain visible when another enemy is focused. The player can trust the board. |
| Milk-source failure | Keep | Actual source loss clears the impossible objective, leads back to Marlow, closes treatment, and opens Hunting without a Garrick fight or hidden wait. No replacement bottle erases the consequence. Temporary aggression/flight alone does not fail the quest. |
| Storage | Keep | Transfers commit when completed; Back cancels only unfinished selection. A physical chest adds a home-base benefit without deliberately crippling the backpack. |
| Woodland discoveries | Keep, test presentation | Sunberry no longer becomes an item with no current use. All five optional additions are observations/environmental discoveries. Keep them incidental, with no five-item completion expectation. |
| Inn layout | Keep | Public board and weapon display, six ambient patrons, a clear arrival pocket, service circulation and separate social anchors solve concrete problems. The building can feel occupied without six extra dialogue trees. |
| Character writing | Keep direction, test delivery | The quieter troll reveal, contextual questions and treatment-first return fit sincerity and earned character appeal. Good staging and editing still matter more than counting branches. |

Do not reopen the solved scheduling, transfer or milk-softlock objections just because an older review lists them.

## Decisions still needed before coding

### 1. Companion recovery needs an actual Rest action

**Dependency to resolve.** [Group combat](../GROUP_COMBAT_PARTY_MVP.md) returns a defeated companion to stable 1 HP after victory but forbids further combat until the party Rests. [Unresolved design](../UNRESOLVED.md) still leaves Rest mechanics open; the existing prototype does not implement resting. The rules specify the restriction without specifying an available way to clear it.

Keep a consequence for defeat, but approve a minimal recovery interaction now: where it is available, what it restores, whether it costs anything, and whether a companion can recover before the player can afford a room. Do not silently introduce a paid-room requirement or design a full Rest Quality/injury system. Also specify the companion's recovery state when the encounter ends by player Flee rather than victory.

**Proof required later:** defeat a companion, win, return with insufficient room money, use the approved recovery route, and take that companion into the next fight. Repeat with Flee. Recovery must be understandable without reading developer notes.

### 2. Esc suspension is explicit, but still feels like a trap

**Experience recommendation, not a rules contradiction.** [UI authority](../UI_HUD_PLAYER_INFORMATION.md) says Esc can collapse dialogue while movement stays locked until Resume or an authored Leave action. This avoids accidentally refusing a job, but hiding the conversation while leaving the player immobilized is a poor default. It adds another click without restoring useful control.

My preference: at a genuine decision, keep the options visible and provide an explicit, neutral **End conversation / I'll think about it** option where the fiction allows it. Preserve unresolved commitment state when the player returns. If Central Brain retains collapse/suspend, show an unmistakable resume prompt and make the still-active conversation obvious; verify that every such state has a clear exit. Do not turn Esc into acceptance/refusal.

**Proof required later:** a new player presses Esc at the job decision and can immediately explain what happened and how to continue or leave.

### 3. Define what the next build promises at the contract board

**Delivery decision.** [Partners](../HUNTING_PARTNERS.md) makes Ily/Mud implementation-ready while Sable/Nessa await their contract passes. All three contracts remain equal opening choices in canon. A build advertising three equally complete routes while only one meets the new standard would break that promise.

Approve a narrowly identified Mud-focused development test first, or include the other two contract passes before declaring the whole opening complete. Do not make Mud mandatory canon to hide unfinished work. Existing simplified routes are not proof of the new contract/partner design.

**Proof required later:** build notes say exactly which routes are under test; a complete-opening acceptance pass eventually includes each first-contract choice.

## Review concerns that do not need another design system

- **Pacing:** stop treating the old 27–30-minute target as a guarantee. The package adds map travel, group combat, companion decisions, ecology and storage alongside two profession introductions. Keep optional material optional and time a natural first playthrough. Do not cut meaningful choices just to meet the old estimate.
- **Player expression:** Sylvie's scripted tasting praise should not force the player to express amazement. Offer a natural response without weakening her competence. In the Duskhen passage, “asks how” followed by “Yes” is still a wording mismatch: use an actual yes/no player question or answer the question asked. This is a small editorial correction for the dialogue owner, not a new branch system.
- **Humor:** Garrick's dry warmth works when contrasted with Marlow's care and Sylvie's professional attention. If everybody answers everything with a comeback, their voices converge. Let the troll scene and the failed-treatment conversation land without a compulsory joke.
- **Inn plan:** the upstairs sketch shows its stair toward the front while the text requires rear-right/central continuity. Treat the sketch as relational, as specified; align the actual stair vertically in the first floor plan before producing geometry. No new architecture brief is needed.
- **Defeat and recovery cost:** losing Potion Making and losing a companion's combat eligibility can compound. Check that the failure route remains viable using the actual available healing/recovery and encounter tuning. Do not infer that “the board opens” alone proves the rest of the route is playable.
- **Exploration:** an inspectable berry that cannot yet be picked is acceptable as limited prototype scope, but do not present it with a Gather prompt or imply an immediate inventory reward. Let the environment carry some discoveries without constant popups.

## What I would not add

No more opening professions, named ambient patrons, companion approval meters, detailed injury systems, final art, lore explanations for Mossback, or quest-safe replacement milk. The package needs coherent delivery and player testing more than additional features.

## Suggested next step for Central Brain

Resolve the three decisions above in the owning authorities, then request another review of the changes. Once there are no material design blockers and the owner authorizes coding, implement in small verifiable steps under the coordinated plan. A document lock is not evidence that a scene is fun; a playtest may still overturn our expectations.

## Repository cleanup performed with this review

- Deleted `WoodlandSpine/PLAYTEST-INTRO.md` and `WoodlandSpine/PLAYTEST-MARLOW.md`: superseded playtest routes containing incorrect current instructions, including pre-name-entry setup, stopping at an old scope boundary, obsolete milk fallback and outdated action controls. Historical versions remain in Git; `PLAYTEST-OPENING.md` remains the baseline guide.
- Replaced duplicated/stale Central Brain handoff mechanics with current authority links; retained the equipment constraints that were not fully repeated in Core.
- Updated `PROJECT_STATE.json`, `START_HERE.md` and README status to distinguish existing implementation from newer design and preserve the owner's review-before-coding instruction.
- Removed stale behind-the-bar board placement and unresolved transfer-presentation claims from the Inn overview; current spatial/UI authorities already settle those.
- Consolidated already-addressed dialogue problem entries into a baseline/regression note while retaining the latest unresolved lab/return concerns.
- Marked old audits, source scripts and result reports as historical evidence where needed; corrected references to removed playtest guides. Preserved original supplied dialogue, test evidence, actual Unity files and useful previous reviews.

Validation for this cleanup: JSON parsing, Markdown link checks and Git whitespace/change-scope checks. No Unity tests or build were run because no gameplay source, assets or settings changed. Earlier automated passes are evidence for their recorded baseline only.
