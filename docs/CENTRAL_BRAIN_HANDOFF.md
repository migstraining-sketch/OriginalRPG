# Central Brain Handoff

Updated 2026-09-10 after reviewing GitHub main through `c7a8773`. This is a status/authority index, not a second copy of every system.

## Current owner instruction

**Review before gameplay coding. Gameplay implementation is on hold.** Review the updated package for Central Brain and resolve material concerns before a separately authorized implementation pass. Locked design status alone does not lift this hold. Recommendations remain proposals until accepted. Do not expand beyond the opening through Cooking unlock.

Read the [current review](reviews/2026-09-10-coordinated-package-review.md) and [START_HERE](START_HERE.md).

## Identity and production

This is an original fantasy RPG. Referenced media are inspiration/research only; their characters, locations, chronology, assets and lore are not canon. Unity handles gameplay. Original Blender assets follow stable layouts, scale, interactions and camera tests. The approved Inn floor-plan/greybox direction does not authorize final art or lift the current hold.

## Opening and authority

Immediate control in Garrick's Inn → contextual introduction → invitation and troll reveal → explicit job decision → regional travel/Woodland → treatment and Potion Making on success, or reagent-impossibility return → board/Hunting → return → Sylvie demonstration and explicit teaching agreement → Cooking unlock.

The timing in [OPENING_FLOW](OPENING_FLOW.md) is an unverified design target, not measured playtime.

- [DIALOGUE_PLAYER_AGENCY](DIALOGUE_PLAYER_AGENCY.md): wording status, information order, responses, commitment and NPC knowledge. Curiosity is not acceptance; Continue is not a roleplaying choice.
- [CORE_SYSTEMS_PROGRESSION](CORE_SYSTEMS_PROGRESSION.md) and [GROUP_COMBAT_PARTY_MVP](GROUP_COMBAT_PARTY_MVP.md): math, techniques, distributed unit activations, party/control states, committed intent and defeat. The previous global Player Phase/Enemy Phase summary is superseded for the new package.
- [OPENING_MOONCALF_MILK_FAILURE](OPENING_MOONCALF_MILK_FAILURE.md): actual reagent loss, clear objectives, no replacement source, missed Potion Making and board/Hunting continuation without a hidden countdown.
- [TRAVEL_WORLD_MAP_MVP](TRAVEL_WORLD_MAP_MVP.md) and [WORLD_BIOMES_EXPLORATION](WORLD_BIOMES_EXPLORATION.md): geography, front-door travel, ecology and optional observations.
- [INN_SPATIAL_BLOCKOUT](INN_SPATIAL_BLOCKOUT.md): population, public board/merchandise access, circulation and proportions. [INN_STARTING_AREA](INN_STARTING_AREA.md) retains rental/storage, kitchen and relationships.
- [SYSTEMS_HUNTING](SYSTEMS_HUNTING.md), [CONTRACTS](CONTRACTS.md), [HUNTING_PARTNERS](HUNTING_PARTNERS.md): investigation/outcomes. Ily is ready in design for Mud in the Moonrice; Sable/Nessa remain character direction for later passes. All three contracts remain equal in canon.
- [SYSTEMS_POTION_MAKING](SYSTEMS_POTION_MAKING.md) and [SYSTEMS_COOKING](SYSTEMS_COOKING.md): profession interactions/unlocks.
- [UI_HUD_PLAYER_INFORMATION](UI_HUD_PLAYER_INFORMATION.md): input/back semantics, targeting, persistent danger cues and storage transfers.
- [CHARACTERS](CHARACTERS.md), [DESIGN_PILLARS](DESIGN_PILLARS.md), [DESIGN_GUARDRAILS](DESIGN_GUARDRAILS.md): character/world identity and principles.

## Equipment details retained from the previous handoff

These established constraints are preserved where the shorter Core summary does not repeat them:

- Weapon and Body Armor are the functioning slots. Player has 0 innate Armor; starting padded/travel clothing supplies Armor 1.
- Garrick can offer an early Armor 2 upgrade. Values stay compressed so +1 Damage/Armor matters; include sidegrades.
- One equipped weapon; switching outside combat is free and in combat spends Primary Action. Exact attacks/signatures live in Core Systems.
- One general inventory for equipment, materials, ingredients, potions and adventure items. No restrictive carrying capacity, item-level/gear-score or rarity-color treadmill in the opening.
- Equipped weapon/body armor should eventually be visibly represented.

The owner specified Garrick's weapon loan for the prototype, and it is implemented. The old statement that the first weapon has no source must not cause its removal. Retain or explicitly revise the beat in the coordinated dialogue packet.

## Implementation is behind design

Latest gameplay source change: `72d6411`. Subsequent design revisions are not a newly built game. Existing code includes weapon signatures, Pounce, Mossback terrain interaction, targeting/camera fixes and the earlier dialogue-agency pass. These need regression testing during the new pass, not automatic classification as still-unfixed bugs.

The [implementation handoff](../WoodlandSpine/Docs/IMPLEMENTATION-HANDOFF.md) records prototype gaps. [Validation status](../WoodlandSpine/Validation/STATUS.md) records 4,463 editor checks and 290 scripted opening checks for the earlier build. Those results do not cover the new package or prove human pacing/UI acceptance.

[UNRESOLVED](UNRESOLVED.md) and the current review hold open decisions. Do not invent later-game systems to solve a small opening dependency.
