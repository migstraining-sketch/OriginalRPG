# WoodlandSpine — complete opening blockout

Open this project in Unity **6000.5.6f1**, open **Assets/Scenes/Opening.unity**, and press Play. Do not play an empty Untitled scene. The scene builds its placeholder world when Play starts.

For the standalone version, run **Builds/Windows/WoodlandSpine.exe**. Keep the entire Windows folder together, including the Data directory and DLLs.

The latest Garrick/Marlow dialogue correction follows **Docs/OPENING-DIALOGUE-AUTHORITY.md**. See **Validation/DIALOGUE-CORRECTION.md** for its exact changes and limits. Read **PLAYTEST-OPENING.md** for the current route. The older INTRO and MARLOW guides/results describe superseded passes.

## Current implementation

- Character name and coat colour, immediate control inside the inn, contextual introductions and unrestricted initial departure.
- Spoken player responses separated from NPC exchanges. Marlow's invitation precedes Garrick's warning. Marlow collects the sample remains and walks to unlock the laboratory.
- A real staircase descends six metres into the lab beneath the inn. Walking down or back up changes camera cutaway; the player is not teleported. Marlow permanently relocates downstairs.
- Free lab exploration; optional ale exchange; protective reactions to opening jars/enclosures; a quiet placeholder creature sound draws Marlow to the troll. Rescue, illness, research, inability to leave and watch backlog precede the ingredient briefing and genuine accept/refuse decision.
- Weapon loan, connected woodland, ordinary creature, three ingredients with peaceful/frightened/violent Mooncalf choices, unexplained Mossback pursuit, tactical hex combat on the terrain, return and naturalist debrief.
- Supervised Health Potion preparation, troll treatment, visible recovery, player remainder, payment and board access. Repeat preparation consumes ingredients plus a herb contribution. Gathering patches replenish on individual cooldowns.
- Three equally presented contracts. Separate observation, interpretation, following, habitat/kill resolution and food outcomes. Nonlethal outcomes supported for all three. Lethal Reedback/Brookmaw routes require harvesting at the habitat before client payment. Mill wheel resumes moving, coop roof is secured, and redirected animals remain elsewhere in the site.
- Garrick opens kitchen access after a completed contract. Sylvie evaluates the actual ingredient, demonstrates the common cooking steps, delivers the taste exchange and offers instruction. Cooking unlocks on acceptance. Optional practice uses the same session system and requires a pantry contribution. Well Fed is a status only; it does not heal.
- Small coin/weapon/body shop and optional paid room. Taking stock after a warning can flag a criminal incident and lead to a prototype bout. Winning Garrick's bout gives early board access, permitting Hunting/Cooking before Marlow.

## Controls

- WASD / arrows: move. E: nearby interaction. I: inventory and equipment.
- Dialogue: click an actual player reply. Space/Enter or Continue advances an NPC exchange. Esc leaves the conversation; narrative milestones remain recorded.
- Combat: M Move, 1 Attack, 2 Defend, 3 Item, 4 Dash. Click a highlighted hex to move or the enemy to attack. After selecting Attack, you can also click Attack [enemy] or press Enter. Confirm Dash/Defend with Enter. Space ends the turn. Esc/right-click cancels an uncommitted selection.
- Brewing/Cooking: use the displayed controls. Stepping away preserves that preparation.
- Inventory lists coins, ingredient counts and edible provisions. Select a provision to use it for cooking or contribution.

## Rules and tuning

30 player HP, zero innate Armor; the starting coat supplies Armor 1. Weapon and Body slots only. Sword 6 adjacent; Spear 6 in a straight direction at 1–2; Bow 5 at 2–4 with LOS. No random miss/critical/damage rolls. Damage is max(1, attack minus armor), halved and rounded up while defending.

Three movement plus one Primary Action; split movement; mud costs two; Dash adds three; switching weapons in combat uses the action. Obstacles stop sight and Mossback charge; hitting a tree staggers it for a phase. Bow can reposition without opportunity attacks. Flee permits retry; it no longer completes the mandatory Mossback milestone.

Weapon/enemy/main combat values remain assets in Assets/Resources. OpeningProgress holds provisional room/coin/illness defaults; HuntDefinition supplies contract data, and FullOpening exposes creature data and gathering cooldown during Play. These are deliberately small systems, not final profession/economy design.

The illness clock starts with the invitation and runs while the game is running, including conversations. Default **45 minutes**, tunable through OpeningProgress. Treatment stops it. Expiry removes Marlow and closes his opening content for the rest of the session. This duration was not specified in the supplied text: it is a visible prototype assumption, not a locked design decision. There is no later replacement Potion Making route yet.

## Validation

Tools/Build-Prototype.ps1 imports, runs combat/opening/intro/progression assertions in Unity and builds Windows. Tools/Compile-Source.ps1 checks source compilation against the installed Unity assemblies. Run the built player with --opening-smoke and an absolute -logFile path for the current automated gameplay test. The older --intro-smoke and --slice-smoke flags redirect to this current test; their old test classes are retained as historical coverage only.

Read Validation/DIALOGUE-CORRECTION.md for the latest dialogue evidence and Validation/OPENING-RESULT.md for the preceding full-opening pass. Automated scripts drive gameplay callbacks and walk selected real routes. They do not certify dialogue quality, natural pacing, every mouse interaction or a full human playthrough.

## Deliberate limits

All geometry, gestures and sound are placeholders. No Blender assets, voiced dialogue, polished art or final animations. Kitchen and rented-room access still use explicit same-scene doorway transfers; only the first lab descent is now continuous physical traversal. Garrick's prototype bout uses a nearby outdoor blockout and a simple yield outcome; a larger crime/guard/reputation system is not implemented. Investigation clues are simple inspectable props, and nonlethal repairs are authored interactions rather than simulation. The three jobs use the common combat rules with different authored evidence; creature-specific advanced combat is not implemented.

Consequences and inventory persist across visits in the running session, not across quitting or stopping Play. Rooms can be rented and entered; sleeping/rest quality is deferred. Well Fed has no stat effect. No additional professions, recipe catalogue, later progression or explanation for Mossback behaviour was added. The 20–35 minute target is not yet established by human playtesting.

Next smallest step: play the revised invitation and first lab visit and adjust only the wording, pause lengths and staging that still feel wrong before committing to custom assets.



