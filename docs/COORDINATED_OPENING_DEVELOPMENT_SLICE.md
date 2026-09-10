# Coordinated Opening Development Slice

## Status / authority

This document defines the **development/playtest scope** for the upcoming coordinated Unity opening slice. It does not rewrite the canonical opening or make one starter Hunting contract narratively mandatory.

**Do not modify Unity from this document alone.** Central Brain is coordinating the implementation pass separately.

Preferred scope language:
- **Coordinated Opening Development Slice**
- **Marlow / Woodland / Mud + Ily development slice**

Do not describe this build as the complete opening, complete first 30 minutes, all starter Hunts implemented, or full companion onboarding.

## Canonical starter-Hunt rule remains unchanged

The three starter Hunting contracts remain equal canonical choices:
- `Mud in the Moonrice`
- `Three Missing by Morning`
- `When the Wheel Stopped`

None is canonically mandatory or narratively privileged.

Whichever starter Hunt the player chooses first may eventually provide their first potential-companion exposure.

`Mud in the Moonrice` is the current implementation target because it is the only starter Hunt presently approved to full implementation depth under the new Hunting, group-combat, and companion standards. This is a production/test-scope fact, not a story rule.

Canonical contract authority remains in `CONTRACTS.md`. Shared/local-partner authority remains in `HUNTING_PARTNERS.md`.

## Current coordinated development-slice target

The playable Hunting target for this development slice is:

**Mud in the Moonrice + Ilyra “Ily” Fen**

The broader slice may include substantial approved opening content around it, including:
- Garrick/Marlow opening dialogue revisions
- Marlow's laboratory/troll sequence
- Woodland ingredient gathering and Mooncalf herd behavior
- Mooncalf Milk success/failure continuation
- approved opening combat refinements
- Regional Map/front-door travel grammar
- room-rental/storage behavior where separately approved
- `Mud in the Moonrice`
- Ily's local-partner/group-combat integration
- Hunting unlock on valid Mud completion
- Garrick → Sylvie handoff where reached

This does **not** mean the two other starter Hunt routes are implementation-complete.

## Contract-board behavior in this development/test build

Show all three canonical postings on Garrick's contract board so the intended equal-choice board presentation can be tested.

### Mud in the Moonrice
- visible
- selectable
- playable
- accepting it reveals **Reedwater Paddies** through the normal Regional Map discovery rule

### Three Missing by Morning
- visible
- **not selectable/playable in this development slice**
- clearly identified as unavailable in the current playtest/development build

### When the Wheel Stopped
- visible
- **not selectable/playable in this development slice**
- clearly identified as unavailable in the current playtest/development build

The unavailable indication is **development scaffolding only**.

It is not:
- canon dialogue
- Garrick refusing the job
- a fictional road closure/client absence
- evidence that the contract was already taken
- evidence that Mud is more important in-world
- permission to remove or hide the other two postings

Do not invent an in-world excuse. The build should honestly communicate that those routes are not included in the current playtest slice.

## Regional Map behavior in this development build

Development-unavailable contracts do **not** reveal their destinations.

Therefore:
- visible `Three Missing by Morning` does **not** reveal Venn Homestead
- visible `When the Wheel Stopped` does **not** reveal Vale Watermill
- accepting playable `Mud in the Moonrice` reveals Reedwater Paddies normally

When the deferred contracts become properly playable later, their existing canonical rules apply:
- accept `Three Missing by Morning` → Venn Homestead becomes known/selectable
- accept `When the Wheel Stopped` → Vale Watermill becomes known/selectable

The development scaffold must not alter `TRAVEL_WORLD_MAP_MVP.md`'s underlying knowledge-gated discovery doctrine.

## Partner implementation scope

For this slice:
- **Ilyra “Ily” Fen** is implementation-ready with `Mud in the Moonrice`.
- **Sable Venn** remains canonical at character level, but detailed `Three Missing by Morning` integration is deferred.
- **Nessa Vale** remains canonical at character level, but detailed `When the Wheel Stopped` integration is deferred.

Do not claim this slice completes companion onboarding for all starter choices.

## Pacing rule for this development slice

The older approximately **20–30 / 27–30 minute** opening estimate remains useful only as historical pacing guidance.

It is **not a hard acceptance criterion** for the coordinated development slice.

After implementation, measure a natural first playthrough and evaluate whether pacing feels healthy.

Do not:
- cut meaningful exploration to hit a stopwatch
- remove dialogue choices merely to shorten runtime
- collapse combat decisions or world interactions for an arbitrary duration target
- deliberately pad traversal/dialogue to preserve the old estimate

The intended standard is **natural pacing and clarity**, not compliance with an old minute budget.

If `OPENING_FLOW.md` contains approximate minute labels, read them as sequencing/pacing guidance rather than hard implementation deadlines or completion criteria.

## What this slice proves

The purpose of this slice is to test whether the interconnected opening systems work together coherently:

**Inn/social opening → Marlow/woodland → regional travel → combat/gathering → Potion Making success or failure continuation → Garrick board → Mud investigation → Ily/group-combat behavior where relevant → Hunting progression → downstream Sylvie handoff where reached**

It is a systems-integration proof slice, not a declaration that every canonical branch has shipped.

## Future full-opening completion requirement

Before the project can later claim the full three-choice starter-Hunt opening is complete, all of the following remain required:
- `Three Missing by Morning` receives its implementation-depth design pass
- Sable Venn's integration is completed
- `When the Wheel Stopped` receives its implementation-depth design pass
- Nessa Vale's integration is completed
- all three first-contract choices receive implementation/acceptance/playtest coverage

These requirements do not block the current Mud-focused development slice.

## Acceptance / scope checks

The coordinated development build should satisfy:
- all three canonical postings are visible
- Mud is selectable/playable
- Three Missing is visibly development-unavailable and cannot be selected
- When the Wheel Stopped is visibly development-unavailable and cannot be selected
- development-unavailable state uses no in-world fictional excuse
- accepting Mud reveals Reedwater Paddies normally
- unavailable postings do not reveal Venn Homestead or Vale Watermill
- Ily is the only starter-Hunt partner implemented to full depth in this slice
- Sable/Nessa remain canonical rather than deleted/replaced
- documentation/build language does not call Mud the mandatory first Hunt
- documentation/build language does not claim all three starter Hunts are implemented
- old opening minute estimates are treated as pacing guidance, not pass/fail timing criteria
- no Unity modification is authorized by this document itself
