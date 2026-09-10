using UnityEngine;
namespace WoodlandSpine
{
    public class ReactiveIntro : MonoBehaviour
    {
        public SliceGame game;
        public bool sitting;
        public float idleDelay=70;
        float idleTime,labTime,crashTime;
        public float crashDelay=2.5f;
        Vector3 previousPosition;
        bool wasOutside;
        [System.NonSerialized] public InnConversation conversation;
        public OpeningState State=>game.opening.state;
        public ReactiveIntroState Story=>State.intro;
        public bool LabReady=>labTime>=4;
        public void Initialize(SliceGame value){game=value;previousPosition=game.player.transform.position;idleTime=labTime=crashTime=0;wasOutside=sitting=false;conversation=new InnConversation(game);}
        DialogueChoice C(string text,System.Action action)=>new DialogueChoice(text,action);
        void Observation(string speaker,string text)=>game.Exchange(speaker,text,game.CloseDialogue);
        void Bark(string text){game.notice=text;game.noticeUntil=Time.time+8;}
        public void Begin(FirstApproach approach){Stand();conversation.Begin(approach);}
        public void Advance(){if(game.dialogue?.continueAction!=null)game.dialogue.continueAction();else conversation.Resume();}
        public void Offer(){game.firstLab.Show();}
        public bool Handle(string key)
        {
            if(key=="seat"){sitting=!sitting;game.notice=sitting?"You settle into a seat.":"You stand up.";ApplySeat();return true;}
            if(key=="merchandise"){Merchandise();return true;}
            if(key=="basement"&&!State.invitedDownstairs)
            {
                Story.basementTried=true;Story.basementAttempts++;
                if(Story.marlowInterest==MarlowInterest.RefusedToHear){Observation("Marlow","That's my laboratory. It's private.");return true;}
                if(Story.basementAttempts==1&&Story.beat==IntroBeat.NotStarted)
                    Observation("Marlow","Um... excuse me.\n\nThat's my laboratory.\n\nYou're trying to enter it.");
                else Begin(FirstApproach.Basement);
                return true;
            }
            if(key=="kitchen"||key=="upstairs")
            {
                if(key=="kitchen")Story.kitchenKnown=true;
                if(Story.beat!=IntroBeat.Finished)Begin(key=="kitchen"?FirstApproach.Kitchen:FirstApproach.Rooms);
                else Observation("Garrick",key=="kitchen"?"Look from this side.":"Rooms aren't free.");
                return true;
            }
            if(key=="board"&&!State.huntingBoardUnlocked){Begin(FirstApproach.Board);return true;}
            if(key=="garrick"&&Story.beat!=IntroBeat.Finished){Begin(Story.idleAcknowledged&&Story.beat==IntroBeat.NotStarted?FirstApproach.Idle:FirstApproach.Garrick);return true;}
            if(key=="marlow"&&Story.marlowInterest==MarlowInterest.RefusedToHear){conversation.Show("reconsider");return true;}
            if(key=="marlow")
            {
                if(Story.beat!=IntroBeat.Finished)Begin(FirstApproach.Marlow);
                else Observation("Marlow","Come on, then.");
                return true;
            }
            if(key=="lab_notes"){Observation("Dated observations","Appetite, coloration, energy and wound closure, recorded each day. Failed preparations have corrections beside them.");return true;}
            if(key=="lab_equipment"){Observation("Expedition gear","Mud-worn cases, a repaired river net and a patched travel cloak. Routes and field observations cover a creased map.");return true;}
            if(key=="workbench"&&!State.questAccepted){Observation("Alchemy bench","Graduated measures, clean vessels and working notes. The tools are worn and carefully maintained.");return true;}
            return false;
        }
        void Merchandise()
        {
            Story.merchandiseKnown=true;
            game.Talk("Garrick's stock","Basic weapons and armor, with price tags. Bottles stand behind the bar.",
                C("Ask about the equipment.",()=>Begin(FirstApproach.Merchandise)),
                C("Take a weapon without paying.",()=>Begin(FirstApproach.TakeMerchandise)),
                C("Take a bottle without paying.",()=>{Story.Begin(FirstApproach.TakeMerchandise);Story.theftWarned=true;conversation.Show("drink_take");}),
                C("Leave it where it is.",game.CloseDialogue));
        }
        public void EnteredLab(){labTime=0;Stand();Story.beat=IntroBeat.Finished;game.notice="";}
        public void Stand(){if(!sitting)return;sitting=false;ApplySeat();}
        void ApplySeat(){if(game.playerVisual!=null){game.playerVisual.localScale=sitting?new Vector3(.65f,.6f,.65f):new Vector3(.65f,.9f,.65f);game.playerVisual.localPosition=new Vector3(0,sitting?.65f:.9f,0);}}
        public void Tick(float delta)
        {
            if(game.opening.inLab){if(game.mode==GameMode.Exploration&&!game.showInventory)labTime+=delta;return;}
            Vector3 p=game.player.transform.position;bool outside=game.coordinated!=null?game.coordinated.travel.knowledge.current!=Region.Inn:p.z>7;
            if(Story.node=="await_crash"&&!outside&&game.mode==GameMode.Exploration&&!game.showInventory){crashTime+=delta;if(crashTime>=crashDelay&&!Story.sampleBroken){Story.sampleBroken=true;game.world.DropSample();}if(crashTime>=crashDelay+.8f){Stand();conversation.Show("sample");}}
            if(outside&&!wasOutside)
            {
                if(!State.metGarrick)Story.leftBeforeIntroduction=true;
                if(!Story.exitRemarkMade){Story.exitRemarkMade=true;Bark("Garrick: Didn't even make it to the bar.");}
            }
            if(!outside&&wasOutside&&Story.leftBeforeIntroduction){Story.returnedAfterLeaving=true;Bark("Garrick: Back already?");}
            wasOutside=outside;
            bool moved=(p-previousPosition).sqrMagnitude>.0025f;previousPosition=p;if(moved)Stand();
            if(outside||State.metGarrick||game.mode!=GameMode.Exploration||game.showInventory||moved){idleTime=0;return;}
            idleTime+=delta;
            if(idleTime>=idleDelay&&!Story.idleAcknowledged)
            {
                Story.idleAcknowledged=true;
                Bark("Garrick: You know there aren't any maids here. You want a drink, you come to the bar.");
                // The player may keep sitting. Introduction starts only if they choose to engage.
            }
        }
    }
}




