using UnityEngine;
namespace WoodlandSpine
{
    public class MarlowOpening : MonoBehaviour
    {
        public SliceGame game;
        public OpeningState state=new OpeningState();
        public PotionSession brew=new PotionSession();
        [System.NonSerialized] public OpeningWorld props;
        public bool inLab;
        float pursuitTime, recoveryTime=-1;
        public bool pastureVisited { get; private set; }
        public bool MossbackAwake=>state.mossbackSeen;
        public string Objective
        {
            get
            {
                if(state.huntingBoardUnlocked)return "Local work is available. Inspect the postings near Garrick.";
                if(state.trollTreated)return "The troll is recovering. Return upstairs and tell Garrick.";
                if(state.potionCompleted)return "Give the experimental potion to the baby troll.";
                if(state.returnedToMarlow)return "Use the alchemy workbench beside Marlow. Your preparation resumes if you step away.";
                if(state.ReadyToReport)return "Bring the ingredients back to Marlow.";
                if(state.questAccepted)return $"HELP MARLOW PREPARE A TREATMENT\n{(state.bloodleafObtained?"✓":"•")} Find Bloodleaf    {(state.silvermossObtained?"✓":"•")} Find Silvermoss    {(state.milkObtained?"✓":"•")} Obtain Mooncalf Milk";
                return "";
            }
        }
        public void Initialize(SliceGame game){this.game=game;props=new OpeningWorld(game.world);}
        DialogueChoice Choice(string label,System.Action action)=>new DialogueChoice(label,action);
        DialogueChoice Leave()=>Choice("Step away",game.CloseDialogue);
        void Say(string who,string text,params DialogueChoice[] choices){if(choices.Length==0)game.Exchange(who,text,game.CloseDialogue);else game.Talk(who,text,choices);}
        public bool Interact(string key)
        {
            if(game.intro!=null&&game.intro.Handle(key))return true;
            switch(key)
            {
                case "garrick":Garrick();break;
                case "marlow":Invite();break;
                case "basement":if(state.invitedDownstairs)EnterLab();else Say("Marlow","My laboratory. Let me explain before you go down there.",Choice("Listen to Marlow",Invite),Leave());break;
                case "lab_exit":ExitLab();break;
                case "lab_marlow":Marlow();break;
                case "troll":Troll();break;
                case "lab_notes":Say("Marlow's notes","Rescued from the river. Appetite declining. Skin losing its green. Regeneration markedly weaker. Trial ratios, feeding observations and revised preparations fill the dated pages. A town-watch request is marked pending: urgent human emergencies take precedence.");break;
                case "lab_specimens":Say("Specimen labels","Bloodleaf: red veins, useful leaf tips; keep the living stem. Silvermoss: pale fronds on damp, shaded stone; brush clean. Mooncalf Milk: one measured portion. Field notes: Mossbacks usually remain docile when given space.");break;
                case "lab_equipment":Say("Expedition equipment","A repaired river net, specimen cases, a drying cloak and well-maintained instruments. These are tools that have seen years of fieldwork.");break;
                case "bloodleaf":Bloodleaf();break;
                case "silvermoss":Silvermoss();break;
                case "mooncalf":game.coordinated.herd.Observe(0);break;

                case "workbench":Workbench();break;
                case "board":Board();break;
                default:return false;
            }
            return true;
        }
        void Garrick()
        {
            state.metGarrick=true;
            if(state.trollTreated)
            {
                Say("Garrick","How'd it go down there?",Choice("It worked. He's eating again.",()=>
                {state.UnlockBoard();game.returned=true;Say("Garrick","Good. Keep the weapon. You've shown you can come back from the woods. There's work on the board when you're ready.",Choice("Inspect the board",Board),Leave());}),Leave());
            }
            else Say("Garrick","Welcome. Bottle-Brain's been carrying his worries upstairs again. Talk to him, if you've a moment.",
                Choice(state.questAccepted?"I need a weapon for Marlow's expedition.":"Could I borrow something for the woods?",game.Loan),
                Choice("Bottle-Brain?",()=>Say("Garrick","Marlow. Made the best ale I've served while trying to grow something in a bottle. He gets my basement. I get the ale. Don't tell him I called it good.")),Leave());
        }
        void Invite()
        {
            Say("Marlow","I have a patient downstairs. I've only come up for fresh water. If you can spare a moment, I could use another pair of hands.",
                Choice("I'll meet you downstairs.",()=>{state.invitedDownstairs=true;game.world.innMarlow.SetActive(false);props.labMarlow.SetActive(true);Say("Marlow","The hatch beside my table. Mind the last step.");}),Leave());
        }
        public void EnterLab()
        {
            if(!state.invitedDownstairs)return;
            if(!state.intro.warnedAfterInvitation&&state.intro.beat!=IntroBeat.Finished){game.intro.Advance();return;}
            game.CloseDialogue();game.notice="The door is open. Follow the stairs down.";
        }
        public void ExitLab(){game.CloseDialogue();game.notice="The stairs lead up along the west side of the laboratory.";}
        void Troll()
        {
            state.sawTroll=true;
            if(state.trollTreated){Say("Marlow","He's interested in food again. The color will take time. I'll keep watching him.");return;}
            if(state.potionCompleted)
            {
                Say("Marlow","A little first. Let him swallow before you offer more.",Choice("Give the experimental potion",()=>
                {if(state.Treat(game.inventory)){game.CloseDialogue();recoveryTime=0;game.notice="He lifts his head toward the bowl. A little green begins returning to his skin.";}}),Leave());return;
            }
            if(state.questAccepted){Say("Marlow","Still no appetite. I'll stay with him. Bring the ingredients when you can.");return;}
            Say("Marlow","He was drowning.\n\nHealthy trolls are green. He's gone pale, stopped eating, and his regeneration is weakening. I've been working on a treatment. I won't leave him unattended while he's deteriorating.",Choice("What do you need?",Marlow),Leave());
        }
        void Marlow()
        {
            if(state.trollTreated){Say("Marlow","It worked.\n\nTake the remaining Health Potion. You've learned the preparation; the Health Potion recipe is now in your notebook. I'll stay with him. Tell Garrick, would you?");return;}
            if(state.potionCompleted){Say("Marlow","Let's try the first dose on him. The other is yours if the preparation does what we expect.");return;}
            if(state.returnedToMarlow){Say("Marlow","Everything is laid out at the workbench. I'll supervise. We'll prepare each ingredient before we combine anything.",Choice("Go to the workbench",game.CloseDialogue),Leave());return;}
            if(state.ReadyToReport){state.returnedToMarlow=true;Say("Marlow","You found them. Good. Bring them to the bench. We can do this.");return;}
            if(!state.sawTroll){Say("Marlow","Look at him first. His color and appetite tell us more than that stack of notes.",Choice("I'll check on him",game.CloseDialogue));return;}
            if(!state.questAccepted)
            {
                Say("Marlow","I need Bloodleaf, Silvermoss and Mooncalf Milk. The watch has my request, but urgent human emergencies come first. I can tend him here if you gather them.",
                    Choice("I'll gather the ingredients.",()=>{state.questAccepted=true;game.jobAccepted=true;Advice();}),Choice("Show me what to look for.",Advice),Leave());return;
            }
            Say("Marlow",state.AllGathered?"You had to turn back? The far clearing is the way through that stretch. Be careful if something there is pursuing you.":"Bring one useful portion of each. My specimen labels and field notes are here if you'd like to look again.",Choice("Remind me what to look for.",Advice),Leave());
        }
        void Advice()
        {
            Say("Marlow","Bloodleaf has red veins. Silvermoss grows on damp, shaded stone. The nursing Mooncow in the woodland herd can provide the milk. Take the clean field flask. Garrick can lend you a weapon.",Leave());
        }        void Bloodleaf()
        {
            if(!state.questAccepted){Say("Field observation","A red-veined plant. Marlow's specimens may help identify which part is useful.");return;}
            if(state.bloodleafObtained){Say("Bloodleaf","The living stem and lower leaves remain. You already have enough useful tips.");return;}
            Say("Bloodleaf","Red veins match Marlow's specimen. The fresh tips can be taken without uprooting the plant.",Choice("Pinch off the useful leaf tips",()=>{state.Gather(Ingredient.Bloodleaf,game.inventory);props.usefulLeaf.SetActive(false);Say("Gathered","Bloodleaf stored. The plant is still rooted and growing.");}),Leave());
        }
        void Silvermoss()
        {
            if(!state.questAccepted){Say("Damp stone","Pale fronds cover the shaded side. There may be a specimen like this in Marlow's lab.");return;}
            if(state.silvermossObtained){Say("Silvermoss","You have enough. Most of the patch remains on the damp stone.");return;}
            Say("Silvermoss","Fine silver-green fronds cling to the cool, shaded stone. Marlow's notes said to leave most of the patch intact.",Choice("Lift a small clean portion",()=>{state.Gather(Ingredient.Silvermoss,game.inventory);Say("Gathered","Silvermoss stored. You leave the surrounding moss undisturbed.");}),Leave());
        }
        void Workbench()
        {
            if(state.potionCompleted){Say("Health Potion preparation",state.healthRecipeUnlocked?"Recipe learned: prepare the leaf tips, clean and bruise the moss, measure the milk; combine leaf, moss, milk; gentle heat and steady stirring. More batches and recipes are outside this opening.":"Two doses prepared. Bring the first to the troll.");return;}
            if(!state.returnedToMarlow){Say("Marlow","We need the ingredients before we can begin.");return;}
            game.dialogue=null;game.showInventory=false;game.mode=GameMode.Brewing;
        }
        public void BrewAction(string action)
        {
            brew.Perform(action);
            if(game.full.repeatBrew){if(brew.Complete){game.full.repeatBrew=false;game.inventory.healthPotions+=2;game.mode=GameMode.Exploration;game.notice="Two Health Potion doses prepared.";}return;}
            if(state.FinishPotion(game.inventory,brew)){game.mode=GameMode.Exploration;game.notice="Experimental Health Potion prepared. Give the first dose to the troll.";}
        }
        void Board()
        {
            if(!state.huntingBoardUnlocked){Say("Garrick","Those jobs can wait. Help Bottle-Brain with his patient first.");return;}
            Say("Hunting board","Work is now available. Three postings await a later prototype pass.",Posting("Mud in the Moonrice"),Posting("Three Missing by Morning"),Posting("When the Wheel Stopped"),Leave());
        }
        DialogueChoice Posting(string title)=>Choice(title,()=>Say(title,"Posting available. The contract content is not implemented yet.",Choice("Back to board",Board),Leave()));
        public void TickExploration(float delta)
        {
            if(inLab||game.coordinated.travel.knowledge.current!=Region.Woodland||game.world.mossback.cleared)return;
            Vector3 p=game.player.transform.position;
            if(p.z>69)pastureVisited=true;
            if(!state.mossbackSeen)
            {
                // Physical approach, not the contents of the reagent inventory, wakes it.
                bool close=Vector3.Distance(p,game.world.mossback.actor.position)<3;
                bool returning=pastureVisited&&p.z<66&&p.z>45;
                game.world.mossback.actor.localScale=new Vector3(1.9f,.72f+Mathf.Sin(Time.time*1.6f)*.025f,2.1f);
                var restPosition=game.world.mossback.actor.position;restPosition.y=.4f;game.world.mossback.actor.position=restPosition;
                if(!close&&!returning)return;
                game.world.mossback.actor.localScale=new Vector3(1.7f,1.5f,1.9f);
                restPosition.y=.85f;game.world.mossback.actor.position=restPosition;
                game.world.mossback.actor.GetComponentInChildren<TextMesh>().text="MOSSBACK";
            }
            if(p.z<45||p.z>70||Mathf.Abs(p.x)>13)return;
            if(!state.mossbackSeen)
            {
                state.mossbackSeen=true;
                Say("Field observation","The moss-covered shape stirs, lifts its head, and gets to its feet. A Mossback. There is room to pass along the trail.",Choice("Keep my distance.",game.CloseDialogue),Choice("Watch quietly from here.",game.CloseDialogue));return;
            }
            pursuitTime+=delta;
            if(pursuitTime<2)return;
            state.mossbackPursued=true;game.notice="The Mossback turns and follows, despite the space you left. It keeps approaching.";
            var site=game.world.mossback;
            Hex target=site.grid.NearestOpen(p,new Hex(99,99));
            Hex from=site.grid.NearestOpen(site.actor.position,target);
            var reach=site.grid.Reach(from,100,target,out var previous);Hex goal=from;int best=int.MaxValue;
            foreach(Hex d in Hex.Directions)if(reach.TryGetValue(target+d,out int cost)&&cost<best){best=cost;goal=target+d;}
            while(previous.TryGetValue(goal,out Hex parent)&&!parent.Equals(from))goal=parent;
            site.actor.position=Vector3.MoveTowards(site.actor.position,site.grid.World(goal)+Vector3.up*.85f,delta*1.7f);
            // Player can retreat out of the clearing. Hostility begins only while nearby.
            if(pursuitTime>6 && site.grid.cells.Contains(site.grid.At(p)))
            {Hex foe=site.grid.NearestOpen(site.actor.position,target);game.StartCombat(site,game.rules.mossback,foe);}
        }
        void Update()
        {
            if(recoveryTime<0)return;
            recoveryTime+=Time.deltaTime;props.RecoverTroll(Mathf.Clamp01(recoveryTime/4));
            if(recoveryTime>=4){recoveryTime=-1;game.notice="Marlow: It worked. The troll reaches for food. Potion Making and Health Potion recipe unlocked; one potion is yours.";}
        }
    }
}

