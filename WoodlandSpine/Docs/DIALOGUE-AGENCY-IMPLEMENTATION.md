# Approved dialogue and agency implementation — 2026-09-09

Implemented the Central Brain-approved spine in docs/DIALOGUE_PLAYER_AGENCY.md at main 9b86105 in response to the project owner's direct request to update the game. The design document's older hold-for-implementation-packet language is preserved; this change does not edit or promote unresolved design questions. Recommended connective wording is used where supplied. The optional post-warning tail is omitted. No new payment amount is set: the existing provisional reward remains a tuning value, not locked dialogue canon.

## Changed behavior

- Character entry chooses coat color. Garrick asks for a name at “You got a name?”; the field accepts a nonblank name up to 24 characters. Confirm button or Enter submits it. “New face.” and “Garrick. I own the place.” precede the question. “[NAME]. Right.” has ordinary Continue, then returns control without a Thanks menu.
- The physical crash waits 2.5 seconds of free exploration in the inn, then another 0.8 seconds before the reaction. Leaving pauses the event. The sample breaks once. Re-engaging Garrick does not ask the name again.
- The exact problem-setup chain now includes fresh material, inability to leave, and the Watch before “Might've found you another pair of legs.” Work interest is recorded only by explicit interest/taking a posting; being poor or looking at the board does not imply it.
- “Would you be willing to hear me out?” offers the four approved responses. Information/payment questions do not commit the player. Only “All right. Show me.” grants willing-to-hear and lab access. Refusing closes the conversation and leaves Marlow upstairs. Reconsideration is explicit.
- The laboratory invitation precedes the warning. “The locked room?” is optional and hidden until the basement was discovered. No Where/Mine ladder. The optional post-warning exchange remains omitted pending voice approval.
- Talking to Marlow before the troll reveal leads to “Come here. I'll show you.” and the intended reveal. Approaching/interacting with the troll also works. Ambient exploration remains available. Existing placeholder movement takes Marlow toward the patient.
- Troll reveal uses “This is why I can't leave.” then “He should be green.” Optional questions replace the mandatory rescue/illness ladder. Learned symptoms/rescue/source/payment information is retained.
- Stabilization and ingredient information precede the real “Will you help me?” gate. Only “I'll get them.” sets quest acceptance. “No.” records job refusal; reconsideration preserves the reveal and ingredients. Acceptance gives “Thank you.” and the practical briefing, then ordinary Continue restores control.
- Generic Marlow exchanges with no actual choice now use Continue instead of a synthetic Step away choice. Existing escape-to-leave remains.

## State and resumption

ReactiveIntroState now keeps name-known, separate MarlowInterest/MarlowJob enums, payment, woodland/ingredient, symptom/rescue, inability-to-leave/Watch knowledge and a lab checkpoint, alongside existing contextual discoveries/history. The older questAccepted flag remains the downstream gameplay gate and is written only at the final acceptance choice. Hear refusal does not set job refusal.

Inn lines advance individually rather than packing multiple speakers into a choice node. Closing a displayed line preserves its coherent checkpoint. Pending crash, refused hearing, downstairs access, revealed troll and actual job decision each resume differently. State remains session-local; save/load is outside this pass.

## Validation and remaining scope

See Validation/STATUS.md for actual test results. Editor contracts now test this new authority rather than obsolete Thanks/Where/Mine responses. Runtime coverage includes name entry, free-control/crash interval, hearing refusal without a downstairs lead, reconsideration, physical stairs, Marlow-first reveal, optional lab questions, actual job refusal/reconsideration and retained combat/profession flow. The hidden-window full-frame captures were blank and are not visual acceptance evidence; name-entry and choice-state behavior are covered by tests. Human UI/pacing playtesting remains required.

Exact payment/economy, final voice polish of optional answers, final character acting and human pacing acceptance remain open. The 2.5/0.8-second staging delays are tunable prototype choices. This implementation does not finish the earlier Cooking/Hunting fidelity gaps or add later progression.

