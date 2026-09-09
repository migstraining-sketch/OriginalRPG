using System;
using UnityEngine;
namespace WoodlandSpine.Editor
{
    public static class InnDialogueValidation
    {
        static int count;
        static void Check(bool value,string message){if(!value)throw new Exception("Dialogue authority: "+message);count++;}
        static void Choose(SliceGame game,string text){var c=game.dialogue.choices.Find(x=>x.label==text&&(x.visible==null||x.visible()));Check(c!=null,"choice available: "+text);c.choose();}
        public static void Run()
        {
            count=0;var root=new GameObject("Agency validation");
            try
            {
                var game=root.AddComponent<SliceGame>();game.opening=root.AddComponent<MarlowOpening>();game.full=root.AddComponent<FullOpening>();game.firstLab=root.AddComponent<FirstLabVisit>();game.firstLab.game=game;
                var graph=new InnConversation(game);var state=game.opening.state.intro;
                foreach(var node in graph.nodes.Values)if(node.next!=null)Check(graph.nodes.ContainsKey(node.next)||node.next=="depart"||node.next=="await_crash"||node.next=="refused_close","node destination exists");
                graph.Show("normal");Check(game.dialogue.text=="New face.","exact first line");game.dialogue.continueAction();Check(game.dialogue.text=="Garrick. I own the place.","exact introduction");game.dialogue.continueAction();Check(game.enteringName&&!state.playerNameKnown&&game.dialogue.choices.Count==0,"name requested at question, no assumed response");
                game.SubmitPlayerName(" ");Check(!state.playerNameKnown,"blank name rejected");game.SubmitPlayerName("Migs");Check(state.playerNameKnown&&game.dialogue.text=="Migs. Right."&&game.dialogue.choices.Count==0,"name acknowledged without Thanks choice");game.dialogue.continueAction();Check(game.mode==GameMode.Exploration&&state.node=="await_crash"&&!state.sampleBroken,"breathing window before crash");
                string[] ids={"sample","fine","wasnt","unfortunate","how_unfortunate","last_sample","another","fresh","go_get","cannot","request","did","watch","urgent","legs"};
                string[] lines={"Bottle-Brain.","I'm fine.","Wasn't asking.","...That's unfortunate.","How unfortunate?","That was the last usable sample I had.","Can you make another?","If I had fresh material.","Then go get it.","I can't leave him.","Thought you already put in a request.","I did.","Watch still sitting on it?","They have more urgent matters.","Might've found you another pair of legs."};
                for(int i=0;i<ids.Length;i++){Check(graph.nodes[ids[i]].text()==lines[i],"locked prose "+ids[i]);if(i+1<ids.Length)Check(graph.nodes[ids[i]].next==ids[i+1],"locked setup order "+ids[i]);}
                state.interestedInWork=false;graph.Show("rooms");Choose(game,"I don't have enough.");Check(!state.interestedInWork,"poverty is not work interest");graph.Show("board");Check(!state.interestedInWork,"looking at board is not work interest");Choose(game,"I'll take this one.");Check(state.interestedInWork,"taking posting expresses work interest");
                state.interestedInWork=false;graph.Show("hear");Check(game.dialogue.choices.Count==4&&!game.opening.state.questAccepted,"four hear-me-out choices without job acceptance");Choose(game,"Does it pay?");Check(state.paymentAsked&&state.paymentKnown&&!state.interestedInWork&&state.marlowInterest==MarlowInterest.Unknown&&!game.opening.state.invitedDownstairs,"payment is not commitment");game.dialogue.continueAction();Choose(game,"No.");game.dialogue.continueAction();Check(state.marlowInterest==MarlowInterest.RefusedToHear&&!game.firstLab.leading&&!game.opening.state.invitedDownstairs,"hear refusal never leads downstairs");
                graph.Show("reconsider");Choose(game,"Sure.");Choose(game,"All right. Show me.");Check(state.marlowInterest==MarlowInterest.WillingToHear&&game.opening.state.invitedDownstairs&&!game.opening.state.questAccepted&&state.marlowJob==MarlowJob.NotOffered,"lab willingness separate from job");
                Check(!game.dialogue.choices.Exists(c=>c.visible==null||c.visible()),"locked-room question hidden before discovery");state.basementTried=true;graph.Show("downstairs");Choose(game,"The locked room?");Check(game.dialogue.text=="Yes. Different circumstances now.","contextual room response");game.dialogue.continueAction();game.dialogue.continueAction();Check(game.dialogue.text=="You holler if they try anything funny."&&state.warnedAfterInvitation,"warning after invitation, no Mine response");
                graph.Show("how_unfortunate");game.CloseDialogue();graph.Resume();Check(game.dialogue.text=="How unfortunate?","coherent interruption checkpoint");
                game.opening.state.sawTroll=true;game.firstLab.Show();Check(game.dialogue.choices.Count>=4&&!game.opening.state.questAccepted,"optional patient questions");Choose(game,"What's wrong with him?");Check(state.symptomsKnown&&game.dialogue.text.StartsWith("I don't know."),"symptoms remembered");Choose(game,"What do you need?");game.dialogue.continueAction();game.dialogue.continueAction();game.dialogue.continueAction();Check(state.marlowJob==MarlowJob.Offered&&state.ingredientsKnown&&!game.opening.state.questAccepted,"ingredients precede commitment gate");Check(!game.dialogue.choices.Exists(c=>c.label=="What are you paying?"),"upstairs payment question not repeated");
                Choose(game,"No.");Check(state.marlowJob==MarlowJob.Refused&&!game.opening.state.questAccepted,"job refusal distinct");game.CloseDialogue();game.firstLab.Show();Check(game.dialogue.text.Contains("reconsidered"),"job refusal remembered");Choose(game,"I'll get them.");Check(state.marlowJob==MarlowJob.Accepted&&game.opening.state.questAccepted&&game.dialogue.text=="Thank you.","only commitment accepts quest");game.dialogue.continueAction();Check(game.dialogue.choices.Count==0&&game.dialogue.continueAction!=null,"briefing returns through ordinary Continue");
                Debug.Log("INN_DIALOGUE_VALIDATION_PASSED: "+count+" assertions");
            }
            finally{UnityEngine.Object.DestroyImmediate(root);}
        }
    }
}
