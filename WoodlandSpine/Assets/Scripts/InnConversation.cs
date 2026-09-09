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
            if(id=="depart") {S.node=id;S.beat=IntroBeat.Finished;game.CloseDialogue();game.firstLab.LeadDownstairs();return;}
            if(id=="close"){game.CloseDialogue();return;}
            if(id=="fight"){game.full.BeginTheftFight();return;}
            if(id=="name"&&game.full.finishBoutWithoutIntroduction){game.full.finishBoutWithoutIntroduction=false;S.node="depart";S.beat=IntroBeat.Finished;game.CloseDialogue();return;}
            if(id=="name"&&game.opening.state.metGarrick)id="welcome";
            var n=nodes[id];S.node=id;S.beat=n.beat;n.enter?.Invoke();
            // Keep short NPC exchanges together, ending at a genuine player response or staging beat.
            var lines=new List<string>{n.speaker+": "+n.text()};string speaker=n.speaker;var last=n;
            while(lines.Count<4&&last.next!=null&&nodes.TryGetValue(last.next,out var following)&&following.beat==n.beat&&following.enter==null)
            {lines.Add(following.speaker+": "+following.text());if(following.speaker!=speaker)speaker="Garrick / Marlow";last=following;if(last.choices.Length>0)break;}
            string text=lines.Count==1?n.text():string.Join("\n\n",lines);
            if(last.next!=null){string next=last.next;game.Exchange(speaker,text,()=>Show(next));}else game.Talk(speaker,text,last.choices);
        }
        public void Resume(){if(!string.IsNullOrEmpty(S.node)&&S.node!="depart")Show(S.node);}
        public void Begin(FirstApproach approach)
        {
            if(!string.IsNullOrEmpty(S.node)&&S.node!="depart"){Resume();return;}
            S.Begin(approach);
            string entry=approach switch {FirstApproach.Kitchen=>"kitchen",FirstApproach.Rooms=>"rooms",FirstApproach.Basement=>"basement_again",FirstApproach.Board=>"board",FirstApproach.TakeMerchandise=>"take",FirstApproach.Merchandise=>"sale",FirstApproach.Marlow=>"marlow",FirstApproach.Idle=>"idle_name",_=>S.returnedAfterLeaving?"returned":"normal"};
            Show(entry);
        }
        void Build()
        {
            N("normal","Garrick","New face.","name");
            N("returned","Garrick","Back already?","name");
            N("idle_name","Garrick","Garrick. I own the inn.","ask_name");
            N("name","Garrick","Garrick. I own the place.","ask_name",IntroBeat.Name);
            N("rooms_name","Garrick","Garrick. Place is mine.","ask_name",IntroBeat.Name);
            N("ask_name","Garrick","You got a name?",beat:IntroBeat.Name,choices:new[]{C(game.playerName+".","welcome",()=>game.opening.state.metGarrick=true)});
            N("welcome","Garrick","",beat:IntroBeat.Welcome,choices:new[]{
                C("Thanks.","sample"),
                C("Got any work?","shared_work",()=>{S.interestedInWork=true;S.boardKnown=true;},()=>!S.boardKnown),
                C("About the work on that board...","shared_work",()=>S.interestedInWork=true,()=>S.boardKnown&&!S.interestedInWork),
                C("Do you have rooms?","shared_room",()=>S.roomsKnown=true,()=>!S.roomsKnown),
                C("You sell equipment?","shared_stock",()=>S.merchandiseKnown=true,()=>!S.merchandiseKnown),
                C("Can I get a drink here?","shared_drink")});
            nodes["welcome"].text=()=>game.playerName+". Right.";
            N("shared_work","Garrick","Work comes through the board. People have problems. They pay somebody else to make 'em stop.","sample",IntroBeat.Welcome);
            N("shared_room","Garrick","", "sample",IntroBeat.Welcome);nodes["shared_room"].text=()=>"Upstairs. Cheapest room's "+game.full.progress.roomPrice+" coins.";
            N("shared_stock","Garrick","Basic weapons and armor. Ask before you take anything.","sample",IntroBeat.Welcome);
            N("shared_drink","Garrick","At the bar. That's what it's here for.","sample",IntroBeat.Welcome);
            N("kitchen","Garrick","Oi.","kitchen_question");
            N("kitchen_question","Garrick","Don't even know your name and you're already trying to get into my kitchen?",choices:new[]{C("Just looking around.","kitchen_look"),C("Your kitchen?","kitchen_yours"),C("Who's going to stop me?","kitchen_stop")});
            N("kitchen_look","Garrick","Look from this side.","name");
            N("kitchen_yours","Garrick","Technically.","name");
            N("kitchen_stop","Garrick","You really need that answered?","name");
            N("rooms","Garrick","Rooms aren't free.",choices:new[]{C("How much?","room_price"),C("I was just looking.","room_stairs"),C("I don't have enough.","room_figured",()=>S.interestedInWork=true)});
            N("room_price","Garrick","","rooms_name");nodes["room_price"].text=()=>game.full.progress.roomPrice+" coins for the cheapest room.";
            N("room_stairs","Garrick","Then you've taken a real interest in stairs.","rooms_name");
            N("room_figured","Garrick","Figured.","room_fix");
            N("room_fix","Garrick","Might be able to fix that.",choices:new[]{C("The money?","room_situation")});
            N("room_situation","Garrick","Your situation.","rooms_name");
            N("basement_again","Marlow","Garrick?","basement_garrick");
            N("basement_garrick","Garrick","I see 'em, Bottle-Brain.","basement_lock");
            N("basement_lock","Garrick","You always introduce yourself by trying locked doors?",choices:new[]{C("I didn't know it was his.","basement_answer")});
            N("basement_answer","Garrick","That's usually what the lock's for.","name");
            N("board","Garrick","Looking for work?",enter:()=>S.interestedInWork=true,choices:new[]{C("What's this?","board_explain"),C("I'll take this one.","board_no")});
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
            N("sample","Garrick","Bottle-Brain.","unfortunate",IntroBeat.Sample,()=>{if(!S.sampleBroken){S.sampleBroken=true;game.world.DropSample();}});
            N("unfortunate","Marlow","...That's unfortunate.","how_unfortunate",IntroBeat.Sample);
            N("how_unfortunate","Garrick","How unfortunate?","last_sample",IntroBeat.Sample);
            N("last_sample","Marlow","That was the last usable sample I had.","request",IntroBeat.Sample);
            N("request","Garrick","Thought you already put in a request.","did",IntroBeat.WatchRequest);
            N("did","Marlow","I did.","watch",IntroBeat.WatchRequest);
            N("watch","Garrick","Watch still sitting on it?","urgent",IntroBeat.WatchRequest);
            N("urgent","Marlow","They have more urgent matters.","legs",IntroBeat.WatchReply);
            N("legs","Garrick","Might've found you another pair of legs.","stranger",IntroBeat.PairOfLegs);
            N("stranger","Marlow","You don't know them.","nope",IntroBeat.Stranger);
            N("nope","Garrick","Nope.","but",IntroBeat.Nope);
            N("but","Garrick","","partners",IntroBeat.Nope);nodes["but"].text=()=>S.interestedInWork?"But they're looking for work.":"But they're standing right here.";
            N("partners","Marlow","That's not usually how I choose expedition partners.","not_going",IntroBeat.Nope);
            N("not_going","Garrick","Good thing you're not going.","not_much",IntroBeat.Nope);
            N("not_much","Garrick","Look, they're not much.","what_got",IntroBeat.Nope);
            N("what_got","Garrick","But they're what you've got.","garrick_protest",IntroBeat.Nope);
            N("garrick_protest","Marlow","Garrick...","time",IntroBeat.Nope);
            N("time","Garrick","Bottle-Brain, you're running out of time.","invitation",IntroBeat.Nope);
            N("invitation","Marlow","If you're actually considering this... you should see what I'm working on first.",beat:IntroBeat.Invitation,choices:new[]{C("Where?","downstairs")});
            N("downstairs","Marlow","Downstairs. My laboratory.",beat:IntroBeat.Invitation,enter:()=>game.opening.state.invitedDownstairs=true,
                choices:new[]{C("The room you wouldn't let me into?","yes",visible:()=>S.basementTried),C("All right.","warning",visible:()=>!S.basementTried)});
            N("yes","Marlow","Yes.","circumstances",IntroBeat.Invitation);
            N("circumstances","Marlow","Under slightly different circumstances.","warning",IntroBeat.Invitation);
            N("warning","Garrick","They try anything funny down there, you holler.","boot",IntroBeat.Warning,()=>S.warnedAfterInvitation=true);
            N("boot","Garrick","Then there's a big boot coming for their face.",beat:IntroBeat.Warning,choices:new[]{C("Mine?","threatened")});
            N("threatened","Garrick","You see anybody else I just threatened?","wanted_work",IntroBeat.Warning);
            N("wanted_work","Garrick","","there_work",IntroBeat.Warning);nodes["wanted_work"].text=()=>S.interestedInWork?"You wanted work.":"There's work.";
            N("there_work","Garrick","There's work.",beat:IntroBeat.Warning,choices:new[]{C("Sure.","sure"),C("What does he need?","needs"),C("How much does it pay?","payment",()=>S.interestedInWork=true),C("No thanks.","refuse",()=>S.Refuse())});
            // Without prior interest, show the offer once rather than inventing that the player asked for work.
            nodes["threatened"].next="offer_bridge";
            N("offer_bridge","Garrick","",beat:IntroBeat.Warning,choices:nodes["there_work"].choices);
            nodes["offer_bridge"].text=()=>S.interestedInWork?"You wanted work.\n\nThere's work.":"There's work.";
            N("needs","Marlow","A few things I can't leave to collect myself.","see_why",IntroBeat.Warning);
            N("see_why","Marlow","It'll make more sense when you see why.",beat:IntroBeat.Warning,choices:new[]{C("Sure.","introduce_marlow"),C("How much does it pay?","payment"),C("No thanks.","refuse",()=>S.Refuse())});
            N("payment","Marlow","","payment_raise",IntroBeat.Warning);nodes["payment"].text=()=>"I can pay you "+Math.Max(0,game.full.progress.marlowPayment-2)+" coins.";
            N("payment_raise","Garrick","","payment_marlow",IntroBeat.Warning);nodes["payment_raise"].text=()=>game.full.progress.marlowPayment+".";
            N("payment_marlow","Marlow","Garrick.","payment_today",IntroBeat.Warning);
            N("payment_today","Garrick","You want it done today.",beat:IntroBeat.Warning,choices:new[]{C("Sure.","introduce_marlow"),C("What does he need?","needs"),C("No thanks.","refuse",()=>S.Refuse())});
            N("sure","Marlow","You don't know what I need yet.",beat:IntroBeat.Warning,choices:new[]{C("You said you're running out of time.","introduce_marlow"),C("Work's work.","introduce_marlow",()=>S.interestedInWork=true)});
            N("introduce_marlow","Marlow","Marlow, by the way.","depart",IntroBeat.Warning,()=>{S.marlowIntroduced=true;S.Accept();});
            N("refuse","Marlow","All right.","your_call",IntroBeat.Warning);
            N("your_call","Garrick","Your call.","depart",IntroBeat.Warning);
            N("lost","Garrick","There.","manners");N("manners","Garrick","Now that we've learned some manners...","proper_intro");N("proper_intro","Garrick","How about a proper introduction?","name");
            N("won","Garrick","Well.","man_enough");N("man_enough","Garrick","I'm man enough to know when I've been beat.","still_steal");N("still_steal","Garrick","Still can't let you steal from me.","name");
        }
    }
}
