# Opening Mooncalf Milk Failure Path

## Status / authority

This document is the opening-only authority for the **Mooncalf Milk reagent-impossibility branch** and supplements `OPENING_FLOW.md`.

The rule below is **settled**. It exists so player freedom can create a real consequence without leaving the player trapped under an impossible objective or softlocking the wider RPG.

## Settled source rule

If the player genuinely destroys access to the currently available viable nursing Mooncow **before obtaining the required serving**, the game does **not** manufacture a replacement source.

Provoking or fighting the herd does **not automatically** fail Marlow's quest.

The relevant state is whether a viable source of the required reagent still physically exists and remains recoverably accessible.

### Quest can still continue

The quest remains viable if, for example:

- combat starts but a viable nursing Mooncow survives;
- an adult becomes hostile but milk can still later be obtained;
- the herd temporarily flees but can reasonably/recoverably return;
- the Mooncalf is threatened but the required nursing-Mooncow source remains viable and accessible.

Do not use `combatStarted`, `mooncalfAttacked`, `adultHostile`, or temporary flight by themselves as reagent-failure flags.

### Reagent becomes genuinely impossible

Trigger the failure branch only once authored world state establishes that the required serving can no longer be obtained from the currently available source.

Examples include:

- the viable nursing Mooncow is killed before milk is obtained;
- the viable nursing herd is permanently driven away or made unrecoverable before milk is obtained;
- another authored world-state change genuinely destroys access to the only currently available viable source.

In those states:

- no backup bottle appears;
- no replacement herd spawns for quest convenience;
- no hidden merchant suddenly stocks the reagent;
- no invisible quest correction grants Mooncalf Milk;
- no dead juvenile or dead adult magically produces the same renewable reagent outcome.

The loss of the reagent source is a physical world consequence of the player's actions.

## Immediate objective handling

Once reagent impossibility is known, **remove `Obtain Mooncalf Milk` as an active objective immediately**.

Do not leave the player following an impossible collection instruction.

The current player intention becomes semantically equivalent to:

**Return to Marlow.**

Journal/knowledge state should preserve the truth of what happened, for example:

**The required Mooncalf Milk can no longer be obtained from the herd. Marlow should know.**

Exact player-facing prose belongs to Dialogue/UI polish. Do not use a giant `QUEST FAILED` popup as the primary communication.

The objective-state transition should be deterministic from world state; it does not require waiting for Marlow, a timer, or a reload before the impossible collection instruction is cleared.

## Return to Marlow

The player returns normally through established geography:

**Woodland → Regional Map → Garrick's Inn front entrance → Marlow's lab**

Marlow does not magically know how the source was lost before the player returns/tells him or the missing reagent becomes evident through conversation.

The failure conversation prioritizes:

**player returned without a viable reagent → Marlow understands the treatment cannot be completed → immediate emotional/world consequence**

Do not interrupt this with Mossback discussion.

Exact dialogue and knowledge-state wording remain owned by `DIALOGUE_PLAYER_AGENCY.md`.

## Treatment quest closure

Once Marlow understands that the required reagent is permanently unavailable:

- Marlow's opening treatment quest becomes **impossible to complete**;
- close/remove its active treatment objective;
- journal/history may retain the event as remembered failure/world state;
- the game must not continue directing the player toward an unobtainable ingredient.

This is a real failure state, not a temporarily paused success route.

## Potion Making consequence

Do **not** compensate for the failure by unlocking Potion Making.

If Marlow's treatment cannot be completed:

- the normal opening Potion Making interaction does not occur;
- **Potion Making does not unlock**;
- **Health Potion recipe is not learned through this opening route**.

Existing authority that Marlow later leaves for a significant period and his related content becomes unavailable remains intact.

The wider RPG continuing does not mean every missed system is immediately restored.

## Failure → board / Hunting convergence

The player's wider opening progression continues immediately after the Marlow failure conversation resolves enough that Garrick understands the situation.

Locked convergence:

**Marlow treatment failure → Garrick's contract board becomes available → player chooses one starter Hunting contract → accepted contract reveals its destination → Regional Map travel → Hunting progression continues**

The player does **not** need to:

- defeat Garrick;
- find a replacement Mooncalf Milk source;
- wait for a hidden illness/departure timer;
- reload;
- complete Potion Making.

This failure branch is an explicit alternate way to reach Garrick's normal opening board-available state.

Garrick can recognize that Marlow's immediate job is over/failed and move the player toward ordinary local contract work through appropriate dialogue/state. Exact wording is downstream.

### Hunting destination rules remain unchanged

Marlow's failure does not reveal Hunting locations by itself.

The board becomes available first. Then existing travel rules apply:

- accept `Mud in the Moonrice` → Reedwater Paddies becomes known/selectable;
- accept `Three Missing by Morning` → Venn Homestead becomes known/selectable;
- accept `When the Wheel Stopped` → Vale Watermill becomes known/selectable.

No Garrick fight or travel exception is required.

## Marlow / troll aftermath

Do **not** require a hidden real-time illness countdown while the player wanders around.

The immediate failure conversation establishes that the treatment cannot be completed. Longer consequences such as troll deterioration/death and Marlow's extended departure should advance through clear authored/world-state transitions.

For MVP, a deterministic later progression point such as a later departure/return transition or another clear opening milestone is sufficient.

Do not fully design Marlow's long absence here.

The important requirement is that the player is **not left waiting for failure consequences to trigger before being allowed to continue playing**.

Garrick's later established serious response remains available when Marlow actually departs:

> **"Marlow left."**

## Successful route remains unchanged

None of this alters normal success:

**ingredients obtained → return → Marlow relief/hope → Potion Making/treatment → troll stabilizes → opening continues**

This failure branch exists only when the required reagent genuinely becomes unavailable before collection.

## Acceptance tests

Implementation must explicitly satisfy at least:

- attack herd, disengage, viable nursing Mooncow remains → quest still possible;
- temporary herd flight with recoverable access → quest still possible;
- nursing Mooncow killed before milk obtained → collection objective removed;
- player receives Return to Marlow intention rather than impossible collection instruction;
- returning without milk produces failure conversation/state;
- treatment quest closes as impossible;
- Potion Making does not unlock;
- Garrick's board becomes available through failure progression;
- player can accept a starter Hunt normally;
- accepted Hunt destination becomes selectable through Regional Map normally;
- no replacement herd, bottle, merchant, or hidden source appears;
- no Garrick fight is required;
- no hidden timer is required for Hunting progression;
- successful Marlow route remains unchanged.

## Design principle

This preserves the project's agency doctrine:

**The player has freedom to act, and the world has freedom to respond.**

The game should neither disable aggression to protect the quest nor secretly repair the consequence afterward.

At the same time, consequence remains proportional: failing one character's urgent problem changes that character's story and access, not the existence of the rest of the game.