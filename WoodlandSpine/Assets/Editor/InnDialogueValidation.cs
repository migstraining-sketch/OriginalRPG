using System;
using UnityEngine;
namespace WoodlandSpine.Editor
{
    public static class InnDialogueValidation
    {
        static int count;
        static void Check(bool value,string message){if(!value)throw new Exception("Opening script: "+message);count++;}
        static void Choose(SliceGame game,string text){var choice=game.dialogue.choices.Find(c=>c.label==text&&(c.visible==null||c.visible()));Check(choice!=null,"available player reply "+text);choice.choose();}
        public static void Run()
        {
            count=0;var root=new GameObject("Opening dialogue contract validation");
            try
            {
                var game=root.AddComponent<SliceGame>();game.playerName="Test traveller";game.opening=root.AddComponent<MarlowOpening>();game.full=root.AddComponent<FullOpening>();
                var graph=new InnConversation(game);var state=game.opening.state.intro;
                foreach(var node in graph.nodes.Values)if(node.next!=null)Check(graph.nodes.ContainsKey(node.next)||node.next=="depart","continuation destination exists");
                // These exact lines are the user's script contract, not paraphrased equivalents.
                string[,] lines={{"normal","New face."},{"name","Garrick. I own the place."},{"ask_name","You got a name?"},{"kitchen_question","Don't even know your name and you're already trying to get into my kitchen?"},{"kitchen_yours","Technically."},{"room_stairs","Then you've taken a real interest in stairs."},{"board_moral","I'm not sending some fresh face out there just to find out what eats 'em."},{"marlow_hear","I can hear you."},{"marlow_why","That's why I said it."},{"unfortunate","...That's unfortunate."},{"how_unfortunate","How unfortunate?"},{"last_sample","That was the last usable sample I had."},{"partners","That's not usually how I choose expedition partners."},{"not_going","Good thing you're not going."},{"not_much","Look, they're not much."},{"what_got","But they're what you've got."},{"time","Bottle-Brain, you're running out of time."},{"needs","A few things I can't leave to collect myself."},{"see_why","It'll make more sense when you see why."},{"sure","You don't know what I need yet."},{"refuse","All right."},{"your_call","Your call."}};
                for(int i=0;i<lines.GetLength(0);i++)Check(graph.nodes[lines[i,0]].text()==lines[i,1],"verbatim authority "+lines[i,0]);
                foreach(string reply in new[]{"Just looking around.","Your kitchen?","Who's going to stop me?"}){graph.Show("kitchen_question");Choose(game,reply);Check(game.dialogue.text.Contains(reply=="Your kitchen?"?"Technically.":reply=="Just looking around."?"Look from this side.":"You really need that answered?"),"correct kitchen answer");}
                game.full.progress.roomPrice=23;graph.Show("rooms");Choose(game,"How much?");Check(game.dialogue.text.Contains("23 coins"),"actual room price");
                graph.Show("name");Check(game.dialogue.text.Contains("You got a name?"),"name question accompanies introduction");Choose(game,"Test traveller.");Check(game.opening.state.metGarrick&&game.dialogue.text=="Test traveller. Right.","name captured once");
                graph.Show("name");Check(game.dialogue.text=="Test traveller. Right.","no duplicate name question");
                state.interestedInWork=false;graph.Show("but");Check(game.dialogue.text.Contains("But they're standing right here.")&&!game.dialogue.text.Contains("looking for work"),"does not invent interest in work");
                state.interestedInWork=true;graph.Show("but");Check(game.dialogue.text.Contains("But they're looking for work."),"work interest remembered");
                graph.Show("invitation");Check(!game.opening.state.invitedDownstairs&&!state.warnedAfterInvitation,"before location and warning");Choose(game,"Where?");Check(game.opening.state.invitedDownstairs&&!state.warnedAfterInvitation,"explicit lab invitation first");Choose(game,"All right.");Check(state.warnedAfterInvitation&&game.dialogue.choices.Exists(c=>c.label=="Mine?"),"boot exchange follows invitation");
                state.basementTried=true;graph.Show("downstairs");Choose(game,"The room you wouldn't let me into?");Check(game.dialogue.text.Contains("Under slightly different circumstances."),"remembers prior door attempt");
                graph.Show("offer_bridge");foreach(string reply in new[]{"What does he need?","How much does it pay?","Sure.","No thanks."})Check(game.dialogue.choices.Exists(c=>c.label==reply),"complete player offer choice "+reply);
                game.full.progress.marlowPayment=12;graph.Show("payment");Check(game.dialogue.text.Contains("10 coins")&&game.dialogue.text.Contains("Garrick: 12."),"payment negotiation uses actual final reward");
                graph.Show("take_last");Choose(game,"Put it back.");Check(!game.full.progress.criminal,"final opportunity to stop theft");
                graph.Show("how_unfortunate");game.CloseDialogue();graph.Resume();Check(game.dialogue.text.Contains("How unfortunate?"),"resumes interrupted exact exchange");
                Debug.Log("INN_DIALOGUE_VALIDATION_PASSED: "+count+" assertions");
            }
            finally{UnityEngine.Object.DestroyImmediate(root);}
        }
    }
}
