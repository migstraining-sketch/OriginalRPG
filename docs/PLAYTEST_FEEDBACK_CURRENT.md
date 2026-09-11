# Current Human Playtest Feedback — Coordinated Opening Slice

## Status

**ACTIVE HUMAN PLAYTEST FEEDBACK / IMPLEMENTATION FOLLOW-UP.**

This document records issues observed by the project owner while manually playing draft PR #4 (`implementation/coordinated-opening-slice`). It is intentionally allowed to contain confirmed bugs, locked experience goals, and proposed implementation directions together, provided each is labeled clearly.

It does not replace upstream design authority. Where an issue is already governed by a locked document, implementation should follow that authority.

Draft implementation under test:
- PR #4 — `Implement coordinated opening slice, Blender inn and playtest fixes`
- branch: `implementation/coordinated-opening-slice`

## 1. Garrick fair bout incorrectly reuses theft context

### Observed

After receiving Marlow's quest, the player inspected Garrick's Hunting board and selected the legitimate option equivalent to:

> **Let me prove it. A fair bout.**

After beating Garrick, Garrick still used theft-specific dialogue equivalent to:

> **Still can't let you steal from me.**

The player had not been stealing.

### Locked fix direction

The **board-proof fair bout** and an actual **theft/provocation fight** must be distinct encounter/conversation contexts.

Suggested implementation state names are illustrative only:
- `BoardProof`
- `Theft`

Board-proof win:
- may unlock the Hunting board through the existing sequence-break rule;
- Garrick acknowledges the fair victory;
- does not set criminal/theft meaning;
- never plays theft accusation dialogue.

Board-proof loss:
- does not create crime state;
- leaves recovery available through normal Basic Rest.

The theft-specific line remains valid only when the player actually stole/provoked Garrick in the relevant authored context.

## 2. Garrick is too easy to Bow-kite

### Observed

The project owner beat Garrick with Bow by moving away and repeatedly attacking.

### Locked experience goal

**Garrick must remain beatable, but defeating him early should be highly unlikely.**

The solution should first address the tactical exploit rather than merely inflating HP.

### Proposed implementation direction

Garrick should be able to answer ranged kiting through some combination of:
- competent gap closing / Dash use;
- sensible Defend use while closing;
- a modest short-to-medium ranged counter during the fair bout.

Working proposed ranged action:

**Practice Throw** — Garrick throws a blunt wooden practice baton/club from the bout area.

Desired properties:
- weaker than Garrick's melee attack;
- enough reach that a Bow user cannot win indefinitely by walking backward;
- visually consistent with a fair practice bout rather than a fantasy projectile power.

The exact action name, range, damage, and AI weighting remain implementation/tuning choices unless separately locked.

Do not make Garrick unbeatable. Rare legitimate player victories should remain possible.

## 3. Player character model/equipment did not carry into combat

### Observed

The player's exploration representation did not transfer cleanly into at least one battle sequence.

### Implementation requirement

The authoritative player appearance should persist across exploration and combat.

Preserve where supported:
- player model/appearance;
- equipped weapon;
- relevant equipment visuals.

Do not silently substitute a generic combat placeholder.

This requirement also aligns with `PLAYER_CHARACTER_VISUAL_DIRECTION.md`, even though the final player visual reference is still proposed rather than locked.

## 4. Committed pursuing creature can be casually outrun

### Observed

During the Woodland/Mooncalf expedition, the player could simply outrun a creature that was meant to attack/pursue them.

### Locked experience goal

When an authored creature has **committed to confrontation**, the encounter must be physically capable of reaching that confrontation.

Do not solve this through obvious teleporting.

Use appropriate combinations of:
- creature movement speed;
- authored pursuit/stalking routes;
- interception/reveal positions;
- confrontation boundaries;
- species-appropriate behavior.

Legitimate retreat/disengagement rules from upstream authority still apply. The goal is to prevent the absurd state where a supposedly committed predator/abnormal pursuer jogs harmlessly behind the player forever.

Both the first Woodland predator and opening Mossback pursuit should be checked for this failure mode.

## 5. First Woodland combat still lacks the approved stalking buildup

### Observed

The currently playable PR #4 still presents the first Woodland attacker too abruptly.

### Why this happened

This is a known integration gap rather than forgotten design. PR #4 was implemented before the newer creature/encounter authority was integrated. The PR itself records the Gloam Lynx stalking sequence as a later integration step.

Current creature authority lives on:
- branch: `design/creatures-monsters-encounters`
- locked commit: `73151b5c4f358aec4582868f27c616cdeaaef533`
- file: `docs/CREATURES_MONSTERS_ENCOUNTERS.md`

### Locked encounter direction

The first Woodland predator is the **Gloam Lynx**, a normal native mythical predator rather than an anomaly.

Required opening flow:

**quiet trailhead → approximately two readable stalking cues → physical stalking → confrontation pocket → committed Pounce**

Possible cues include:
- Ashwing birds scattering;
- brush movement;
- partial silhouette/tail glimpse;
- ordinary eye-shine;
- prey remains / scratches where appropriate.

Do not use:
- clue counters;
- a Hunting tutorial;
- quest markers pointing to the animal;
- a free unavoidable opening hit.

The player may retreat and leave before confrontation. If the player continues deeper while actively stalked, the predator eventually produces a real confrontation through physical movement/authored routes rather than one disposable trigger volume.

## 6. Roaming encounter doctrine to preserve during expansion

Locked creature doctrine on the creature design branch:

> **Randomize ecology, not battles.**

Future local encounter variation should use physical creature activity pockets, plausible habitat, small authored routes, species behavior, and empty/quiet states rather than invisible step-count random battles.

Combat should emerge because a creature physically exists and player/creature behavior causes confrontation.

Current opening Woodland roster authority:
- **Gloam Lynx** — normal solitary predator;
- **Mooncalf / Mooncow / Moonbull** — peaceful/potentially defensive herd;
- **Mossback** — normally ordinary species, anomalous opening individual;
- **Rootmuzzle** — harmless small burrowing forager;
- **Ashwing Thrush** — ambient harmless bird.

Future-only directions:
- Fernhorn Roe;
- Runnelback.

## Next human-playtest priority

After the focused fixes above, rerun the opening from Garrick/Marlow through Woodland and verify at minimum:
- fair board bout never produces theft dialogue;
- Garrick can pressure Bow users without becoming literally unbeatable;
- player model/equipped weapon persist into combat;
- first predator provides readable stalking buildup;
- retreat before Gloam Lynx confrontation works;
- continuing deeper produces reliable confrontation;
- committed predator/Mossback pursuit cannot be casually outrun forever.

Continue recording human-feel problems here even when automated assertions pass. Automated validation is useful evidence, but does not prove dialogue tone, pacing, readable pursuit, or encounter feel.
