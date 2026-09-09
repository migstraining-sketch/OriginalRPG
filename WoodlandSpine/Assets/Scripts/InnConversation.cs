using System;
using System.Collections.Generic;
namespace WoodlandSpine
{
    // The supplied opening script lives here. Node keys preserve the exact interrupted exchange.
    public sealed class InnConversation
    {
        public sealed class Node
        {
            public string speaker;
            public Func<string> text;
            public string next;
            public DialogueChoice[] choices;
            public Action enter;
            public IntroBeat beat;
        }
        public readonly Dictionary<string,Node> nodes=new Dictionary<string,Node>();
        readonly SliceGame game;
        ReactiveIntroState S=>game.opening.state.intro;
        public InnConversation(SliceGame game){this.game=game;Build();}
        DialogueChoice C(string label,string next,Action action=null,Func<bool> visible=null)=>new DialogueChoice(label,()=>{action?.Invoke();Show(next);},visible);
        void N(string id,string who,string text,string next=null,IntroBeat beat=IntroBeat.Reaction,Action enter=null,params DialogueChoice[] choices)
            =>nodes[id]=new Node{speaker=who,text=()=>text,next=next,choices=choices,beat=beat,enter=enter};
        public void Show(string id)
        {
            if(id=="await_crash"){S.node=id;S.beat=IntroBeat.Welcome;game.CloseDialogue();return;}
            if(id=="refused_close"){S.node=id;S.beat=IntroBeat.Finished;game.CloseDialogue();return;}
            if(id=="depart") {S.node=id;S.beat=IntroBeat.Finished;game.CloseDialogue();game.firstLab.LeadDownstairs();return;}
            if(id=="close"){game.CloseDialogue();return;}
            if(id=="fight"){game.full.BeginTheftFight();return;}
            if(id=="name"&&game.full.finishBoutWithoutIntroduction){game.full.finishBoutWithoutIntroduction=false;S.node="depart";S.beat=IntroBeat.Finished;game.CloseDialogue();return;}
            if(id=="name"&&S.playerNameKnown){game.CloseDialogue();return;}
            var n=nodes[id];S.node=id;S.beat=n.beat;n.enter?.Invoke();
            game.Talk(n.speaker,n.text(),n.choices);
            if(n.next!=null)game.dialogue.continueAction=()=>Show(n.next);
            if(id=="ask_name")game.RequestName(()=>Show("welcome"));
        }
        public void Resume(){if(S.node=="await_crash"||S.node=="refused_close"){game.CloseDialogue();return;}if(!string.IsNullOrEmpty(S.node)&&S.node!="depart")Show(S.node);}
        public void Begin(FirstApproach approach)
        {
            if(!string.IsNullOrEmpty(S.node)&&S.node!="depart"){Resume();return;}
            S.Begin(approach);
            string entry=approach switch {FirstApproach.Kitchen=>"kitchen",FirstApproach.Rooms=>"rooms",FirstApproach.Basement=>"basement_again",FirstApproach.Board=>"board",FirstApproach.TakeMerchandise=>"take",FirstApproach.Merchandise=>"sale",FirstApproach.Marlow=>"marlow",FirstApproach.Idle=>"idle_name",_=>S.returnedAfterLeaving?"returned":"normal"};
            Show(entry);
        }
        void RefuseHear(){S.marlowInterest=MarlowInterest.RefusedToHear;}
        void Build()
        {
            N("normal","Garrick","New face.","name");
            N("returned","Garrick","Back already?","name");
            N("idle_name","Garrick","Garrick. I own the place.","ask_name");
            N("name","Garrick","Garrick. I own the place.","ask_name",IntroBeat.Name);
            N("rooms_name","Garrick","Garrick. I own the place.","ask_name",IntroBeat.Name);
            N("ask_name","Garrick","You got a name?",beat:IntroBeat.Name);
            N("welcome","Garrick","","await_crash",IntroBeat.Welcome);nodes["welcome"].text=()=>game.playerName+". Right.";
            N("kitchen","Garrick","Oi.","kitchen_question");
            N("kitchen_question","Garrick","Don't even know your name and you're already trying to get into my kitchen?",choices:new[]{C("Just looking around.","kitchen_look"),C("Your kitchen?","kitchen_yours"),C("Who's going to stop me?","kitchen_stop")});
            N("kitchen_look","Garrick","Look from this side.","name");
            N("kitchen_yours","Garrick","Technically.","name");
            N("kitchen_stop","Garrick","You really need that answered?","name");
            N("rooms","Garrick","Rooms aren't free.",choices:new[]{C("How much?","room_price"),C("I was just looking.","room_stairs"),C("I don't have enough.","room_figured")});
            N("room_price","Garrick","","rooms_name");nodes["room_price"].text=()=>game.full.progress.roomPrice+" coins for the cheapest room.";
            N("room_stairs","Garrick","Then you've taken a real interest in stairs.","rooms_name");
            N("room_figured","Garrick","Figured.","room_fix");
            N("room_fix","Garrick","Might be able to fix that.",choices:new[]{C("I'm looking for work.","room_situation",()=>S.interestedInWork=true),C("I was only asking.","rooms_name")});
            N("room_situation","Garrick","Your situation.","rooms_name");
            N("basement_again","Marlow","Garrick?","basement_garrick");
            N("basement_garrick","Garrick","I see 'em, Bottle-Brain.","basement_lock");
            N("basement_lock","Garrick","You always introduce yourself by trying locked doors?",choices:new[]{C("I didn't know it was his.","basement_answer")});
            N("basement_answer","Garrick","That's usually what the lock's for.","name");
            N("board","Garrick","Looking for work?",choices:new[]{C("What's this?","board_explain"),C("I'll take this one.","board_no",()=>S.interestedInWork=true)});
            N("board_explain","Garrick","People have problems. They pay somebody else to make 'em stop.","name");
            N("board_no","Garrick","No.",choices:new[]{C("Why not?","board_unknown")});
            N("board_unknown","Garrick","Because I don't know you.","board_other_end");
            N("board_other_end","Garrick","Don't know if you can handle what's on the other end of that, either.","board_moral");
            N("board_moral","Garrick","I'm not sending some fresh face out there just to find out what eats 'em.","name");
            N("take","Garrick","I'd leave that where it is.",enter:()=>S.theftWarned=true,choices:new[]{C("Is it for sale?","sale"),C("What happens if I don't?","take_test"),C("Put it back.","name")});
            N("sale","Garrick","Now you're asking the right question.","name");
            N("take_test","Garrick","You planning on finding out?",choices:new[]{C("Put it back.","name"),C("Keep taking it.","take_warning")});
            N("take_warning","Garrick","Put it back.",choices:new[]{C("Put it back.","name"),C("No.","take_kid")});
            N("take_kid","Garrick","Kid.","take_last");
            N("take_last","Garrick","Put it back.",choices:new[]{C("Put it back.","name"),C("Keep it anyway.","fight")});
            N("drink_take","Garrick","That's an interesting way to order a drink.","take_warning");
            N("marlow","Marlow","Hello.","marlow_looking");
            N("marlow_looking","Marlow","Sorry. Were you looking for someone?","marlow_garrick");
            N("marlow_garrick","Garrick","Hm. New face and already talking to Bottle-Brain.","marlow_hear");
            N("marlow_hear","Marlow","I can hear you.","marlow_why");
            N("marlow_why","Garrick","That's why I said it.","marlow_does");
            N("marlow_does","Marlow","He does this.","name");
            N("sample","Garrick","Bottle-Brain.","fine",IntroBeat.Sample,()=>{if(!S.sampleBroken){S.sampleBroken=true;game.world.DropSample();}});
            N("fine","Marlow","I'm fine.","wasnt",IntroBeat.Sample);
            N("wasnt","Garrick","Wasn't asking.","unfortunate",IntroBeat.Sample);
            N("unfortunate","Marlow","...That's unfortunate.","how_unfortunate",IntroBeat.Sample);
            N("how_unfortunate","Garrick","How unfortunate?","last_sample",IntroBeat.Sample);
            N("last_sample","Marlow","That was the last usable sample I had.","another",IntroBeat.Sample);
            N("another","Garrick","Can you make another?","fresh",IntroBeat.Sample);
            N("fresh","Marlow","If I had fresh material.","go_get",IntroBeat.Sample);
            N("go_get","Garrick","Then go get it.","cannot",IntroBeat.Sample);
            N("cannot","Marlow","I can't leave him.","request",IntroBeat.Sample,()=>S.cannotLeaveKnown=true);
            N("request","Garrick","Thought you already put in a request.","did",IntroBeat.WatchRequest);
            N("did","Marlow","I did.","watch",IntroBeat.WatchRequest);
            N("watch","Garrick","Watch still sitting on it?","urgent",IntroBeat.WatchRequest);
            N("urgent","Marlow","They have more urgent matters.","legs",IntroBeat.WatchReply,()=>S.watchKnown=true);
            N("legs","Garrick","Might've found you another pair of legs.","stranger",IntroBeat.PairOfLegs);
            N("stranger","Marlow","You don't know them.","nope",IntroBeat.Stranger);
            N("nope","Garrick","Nope.","but",IntroBeat.Nope);
            N("but","Garrick","","partners",IntroBeat.Nope);nodes["but"].text=()=>S.interestedInWork?"But they're looking for work.":"But they're standing right here.";
            N("partners","Marlow","That's not usually how I choose expedition partners.","not_going",IntroBeat.Nope);
            N("not_going","Garrick","Good thing you're not going.","problem",IntroBeat.Nope);
            N("problem","Marlow","That's rather the problem.","ask",IntroBeat.Nope);
            N("ask","Garrick","Then ask 'em.","hear",IntroBeat.Nope);
            N("hear","Marlow","Would you be willing to hear me out?",beat:IntroBeat.Invitation,choices:new[]{C("What do you need?","needs"),C("Does it pay?","payment"),C("Sure.","invitation"),C("No.","refuse",RefuseHear)});
            N("needs","Marlow","Several things from the woodland. It'll make more sense if you see why I can't go myself.","invitation",IntroBeat.Invitation,()=>S.woodlandKnown=true);
            N("payment","Marlow","Yes. There is payment for the work.","invitation",IntroBeat.Invitation,()=>{S.paymentAsked=true;S.paymentKnown=true;});
            N("invitation","Marlow","If you're willing, come downstairs. You can see the situation before you decide anything.",beat:IntroBeat.Invitation,choices:new[]{C("All right. Show me.","downstairs",()=>{S.marlowInterest=MarlowInterest.WillingToHear;game.opening.state.invitedDownstairs=true;}),C("No.","refuse",RefuseHear)});
            N("downstairs","Marlow","My laboratory's downstairs.","warning_call",IntroBeat.Invitation,choices:new[]{C("The locked room?","circumstances",visible:()=>S.basementTried)});
            N("circumstances","Marlow","Yes. Different circumstances now.","warning_call",IntroBeat.Invitation);
            N("warning_call","Garrick","Bottle-Brain.","warning",IntroBeat.Warning);
            N("warning","Garrick","You holler if they try anything funny.","depart",IntroBeat.Warning,()=>S.warnedAfterInvitation=true);
            N("refuse","Marlow","All right.","refused_close",IntroBeat.Invitation);
            N("reconsider","Marlow","Have you reconsidered hearing me out?",beat:IntroBeat.Invitation,choices:new[]{C("Sure.","invitation"),C("No.","refuse",RefuseHear)});
            N("lost","Garrick","There.","manners");N("manners","Garrick","Now that we've learned some manners...","proper_intro");N("proper_intro","Garrick","How about a proper introduction?","name");
            N("won","Garrick","Well.","man_enough");N("man_enough","Garrick","I'm man enough to know when I've been beat.","still_steal");N("still_steal","Garrick","Still can't let you steal from me.","name");
        }
    }
}



