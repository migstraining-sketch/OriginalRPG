# Dialogue correction — supplied Garrick/Marlow opening

The authoritative text for this pass is preserved verbatim in Docs/OPENING-DIALOGUE-AUTHORITY.md. This corrects the previous implementation's paraphrases and omissions. The first lab visit remains the separately supplied scene from the previous pass.

## What changed

- InnConversation.cs holds the opening's dialogue nodes and player choices. ReactiveIntro now handles context, prompts, seating and ambient remarks instead of carrying a second improvised copy of the lab scene.
- Normal greeting now uses New face / Garrick. I own the place / You got a name / player name / [NAME]. Right.
- Restored all three kitchen replies, all three room replies (with the actual room price), Marlow's initial basement objection and Garrick's response to a repeat attempt, board explanation/refusal, merchandise and bottle warnings, and the Marlow-first exchange.
- Reading remains allowed. Idling produces an ambient remark without a forced dialogue or forced standing. Leaving and returning use the supplied remarks.
- Restored the unfortunate/how unfortunate/last sample exchange and the full argument after Nope, including expedition partners, not going, not much/what you've got, and running out of time.
- Garrick says looking for work only when the player has expressed that interest. Otherwise he says standing right here.
- Preserved the explicit laboratory invitation and previous-door-attempt branch before the boot warning.
- Restored the four offer responses and their branches: What does he need?, How much does it pay?, Sure., No thanks. The payment branch reads the live final reward. With the current provisional 8-coin payment, Marlow offers 6 and Garrick raises it to 8. The payment amount itself remains tunable.
- Refusal is All right / Your call, without begging or shaming. Agreeing here is only agreeing to hear him out; it does not accept the ingredient quest before the lab visit.
- Short NPC exchanges share a dialogue panel, without invented Listen responses. Esc resumes from the same dialogue node rather than restarting the scene. Added a placeholder glass-break sound.
- Optional Garrick bout outcomes now use the supplied loss/win dialogue, and a loss is nonlethal and resumes the opening. The existing outdoor bout staging and prototype difficulty remain; this pass does not claim to implement the requested around-the-bar combat staging or a complete later town-watch consequence system.

## Specific evidence

InnDialogueValidation checks the supplied wording, complete offer/kitchen choices, current room/payment data, known-name suppression, work-interest context, prior door attempts, invitation before warning and interrupted-node resume. These checks protect the script itself, not just whether a quest can advance.

The current built-player smoke additionally tests that continued sitting remains allowed and that the first basement attempt does not prematurely summon Garrick, before exercising the normal dialogue-to-lab route and retained full opening.

Refer to Validation/STATUS.md and the current Unity/player logs for the final counts. Automated checks establish wording and branch behavior, not human approval of delivery, gestures or pacing.

## Playtest

Start a fresh game. Test normal Garrick first, then fresh runs for kitchen, upstairs, basement twice, board reading/taking, merchandise and Marlow-first. At the offer, test payment and No thanks in addition to Sure. Compare against the preserved authority document. The next production step is only to address discrepancies found in that comparison.

## Final verification

Unity build succeeded. 456 Editor assertions passed, including 120 script-specific checks. The final built-player regression passed 190 assertions. No failures in the final run. Source archive and Windows build refreshed. The player test uses scripted choices; human performance/pacing acceptance remains pending.
