using UnityEngine;
namespace WoodlandSpine
{
    // Owns the post-expedition hub and investigations; early dialogue stays in ReactiveIntro.
    public sealed class FullOpening : MonoBehaviour
    {
        public SliceGame game;
        public OpeningProgress progress=new OpeningProgress();
        public HuntDefinition[] definitions=HuntDefinition.Defaults();
        public CookingSession cooking;
        public HuntingWorld props;
        public EnemyData[] creatures;
        public bool boardVisible, inKitchen, inRoom, repeatBrew;
        public float gatherCooldown=120;
        readonly float[] gatherReadyAt=new float[3];
        bool departureShown;
        bool loanOffered;
        public bool finishBoutWithoutIntroduction;
        EncounterSite moonSite, garrickSite;
        EnemyData moonEnemy, garrickEnemy;
        OpeningState S=>game.opening.state;
        DialogueChoice C(string label,System.Action action)=>new DialogueChoice(label,action);
        DialogueChoice Back()=>C("I'll come back.",game.CloseDialogue);
        void Say(string who,string text,params DialogueChoice[] choices){if(choices.Length==0)game.Exchange(who,text,game.CloseDialogue);else game.Talk(who,text,choices);}
        public void Initialize(SliceGame value)
        {
            game=value;props=new HuntingWorld(game.world,definitions);creatures=new EnemyData[3];
            for(int i=0;i<3;i++)creatures[i]=Enemy(definitions[i].creature,18,4);
            moonEnemy=Enemy("Mooncalf",12,3);garrickEnemy=Enemy("Garrick",30,5);
        }
        EnemyData Enemy(string title,int hp,int damage){var e=ScriptableObject.CreateInstance<EnemyData>();e.title=title;e.hp=hp;e.damage=damage;return e;}
        public string Objective
        {
            get
            {
                if(progress.marlowGone&&!progress.huntingLearned)return "Marlow has left. Speak to Garrick.";
                if(progress.activeHunt>=0&&!progress.hunts[progress.activeHunt].rewarded)
                {
                    int i=progress.activeHunt;var h=progress.hunts[i];
                    return definitions[i].title+" • "+(h.resolved?"Return to "+definitions[i].client:!h.AllEvidence?"Inspect the four signs around the site":!h.interpreted?"Discuss the evidence with "+definitions[i].client:!h.followed?"Follow the signs beyond the damage":"Resolve the problem at the habitat or confront the animal");
                }
                if(progress.huntingLearned&&!progress.kitchenAccess)return "Return to Garrick with the contract's provisions.";
                if(progress.kitchenAccess&&!progress.cookingLearned)return progress.demonstrated?"Ask Sylvie about learning to cook.":"Take your provisions through the kitchen door. Speak to Sylvie.";
                if(progress.cookingLearned&&(!S.questAccepted||S.trollTreated||progress.marlowGone))return "";
                if(S.huntingBoardUnlocked&&S.trollTreated)return "Work is available on Garrick's board. A room and the lab are here when you need them.";
                return null;
            }
        }
        public bool Handle(string key)
        {
            if(key.StartsWith("hunt_")){HuntInteraction(key);return true;}
            if(key=="kitchen_exit"){inKitchen=false;game.player.Place(new Vector3(5.5f,.1f,5));game.CloseDialogue();return true;}
            if(key=="room_exit"){inRoom=false;game.player.Place(new Vector3(6,.1f,-5));game.CloseDialogue();return true;}
            if(key=="sylvie"||key=="cooking"){Sylvie();return true;}
            if(key=="pantry"){Pantry();return true;}
            if(key=="board"){Board();return true;}
            if(progress.marlowGone&&(key=="lab_marlow"||key=="marlow"||key=="troll"||key=="workbench"||key=="basement"))
            {Say("The laboratory","Marlow has gone. His workbench is covered, his travel gear gone. This part of the inn is closed.");return true;}
            if(key=="kitchen"&&progress.kitchenAccess){inKitchen=true;game.CloseDialogue();game.player.Place(new Vector3(31,.1f,-28));return true;}
            if(key=="upstairs"&&(S.intro.beat==IntroBeat.Finished||progress.defeatedGarrick)){Rooms();return true;}
            if(key=="garrick"&&(S.intro.beat==IntroBeat.Finished||progress.marlowGone))
            {
                if(progress.huntingLearned&&!progress.kitchenAccess)
                {Say("Garrick",progress.food.Contains("Eggs")?"Not bad. Those eggs for eating, or are you starting a flock?":"Not bad. And you've still got half the beast hanging off you.",C("I was going to find something to do with them.",()=>{progress.kitchenAccess=true;Say("Garrick","Take it through to her. I'd call her out, but I've survived this long by remembering one thing.\n\nMy inn. Her kitchen.");}));return true;}
                if(S.trollTreated&&!S.huntingBoardUnlocked)return false;
                Say("Garrick",progress.marlowGone?"Marlow left.":progress.defeatedGarrick?"You've proved you can handle yourself. Next time, use your words.":"What do you need?",
                    C(S.questAccepted&&game.inventory.weapon==null?"Something to take into the woods.":"Let me see your equipment.",S.questAccepted&&game.inventory.weapon==null?game.Loan:Shop),
                    C("About a room...",Rooms),C("What work is available?",Board),Back());return true;
            }
            if(key=="merchandise"&&S.intro.beat==IntroBeat.Finished){Shop();return true;}
            if(key=="lab_marlow"&&S.trollTreated){Say("Marlow","He's eating. Properly eating.\n\nYou can use the bench again. Bring material for your preparation and something useful for the laboratory. Herbs, feed, specimens. It needn't be money.",C("I'd like to prepare another Health Potion.",RepeatPotion),Back());return true;}
            if(key=="workbench"&&S.healthRecipeUnlocked){RepeatPotion();return true;}
            if(S.trollTreated&&(key=="bloodleaf"||key=="silvermoss"||key=="mooncalf"||key=="milk_cache")){GatherAgain(key);return true;}
            if(key=="mooncalf"&&S.questAccepted&&!S.milkObtained){Mooncalf();return true;}
            return false;
        }
        public void Board()
        {
            if(!S.huntingBoardUnlocked)
            {
                S.intro.boardKnown=true;
                Say("Pinned work","Three local problems: damaged paddies, missing Duskhen, a stopped mill. Each names a client, a place and a payment.",
                    C("Take a posting",()=>{if(S.intro.beat!=IntroBeat.Finished)game.intro.Begin(FirstApproach.Board);else Say("Garrick","Looking's fine. Taking one means someone's counting on you. I need to know you can handle yourself.",C("Let me prove it. A fair bout.",Challenge),Back());}),Back());return;
            }
            game.CloseDialogue();boardVisible=true;
        }
        public void AcceptHunt(int index)
        {
            if(!progress.Choose(index)){game.notice="Finish your current posting before taking another. The other jobs stay available.";return;}
            boardVisible=false;var d=definitions[index];Say(d.title,d.client+" • "+d.location+"\n\n"+d.problem+"\nPayment: "+progress.huntReward+" coins and provisions. Find the client at the site.");
        }
        void HuntInteraction(string key)
        {
            string[] bits=key.Split('_');int i=int.Parse(bits[2]);var h=progress.hunts[i];var d=definitions[i];
            if(!h.accepted){Say(d.client,"That problem's posted at Garrick's. Speak to him about taking the job.");return;}
            if(bits[1]=="clue")
            {int clue=int.Parse(bits[3]);h.Inspect(clue);Say("Field observation",d.clues[clue]+(h.AllEvidence?"\n\nYou have enough observations to discuss a likely explanation with "+d.client+".":""));return;}
            if(bits[1]=="client")
            {
                if(h.resolved)
                {
                    if(h.lethal&&i!=1&&!h.harvested){Say(d.client,"You dealt with it? Don't leave useful food to spoil. There's a clear place to prepare it at the habitat.");return;}
                    h.harvested=true;
                    if(progress.Reward(i,h.lethal?d.freshFood:d.peacefulFood))Say(d.client,"That's settled it. Thank you. Here's the promised payment, and something for your table.\n\n"+progress.huntReward+" coins • "+progress.food+" ×3\nHunting learned: understand and resolve a wildlife problem.");
                    else Say(d.client,i==0?"The paddies are recovering.":i==1?"No more birds missing. The eggs are coming again.":"Hear that? The wheel's still turning.");return;
                }
                if(!h.AllEvidence){Say(d.client,d.problem+" Walk around the damaged ground and look carefully before deciding what to do.");return;}
                if(h.interpreted){Say(d.client,"That fits what you've found. Follow the signs past the damaged ground.");return;}
                Say(d.client,"What do you make of what you found?",C(d.deduction,()=>{h.Interpret(true);Say(d.client,"That explains it. Follow the signs. If you can fix the cause, all the better.");}),C(d.wrongDeduction,()=>Say(d.client,"That doesn't fit all the signs. Have another look; I don't want us fixing the wrong problem.")),Back());return;
            }
            if(!h.interpreted){Say("Field observation","Study the damage and discuss what it means before committing to a solution.");return;}
            if(bits[1]=="trail"){h.followed=true;Say("Following the signs",d.creature+" signs continue toward the far side of the clearing. The habitat beyond the animal may offer a way to solve this without a fight.");return;}
            if(!h.followed){Say("Field observation","The signs on the east side of this clearing should establish where the animal is coming from.");return;}
            if(h.resolved){if(h.lethal&&i!=1&&!h.harvested)Say("Field preparation","Take only the useful edible cut and wrap it cleanly.",C("Harvest and wrap the "+d.freshFood,()=>{h.harvested=true;Say("Prepared",d.freshFood+" secured. Return to "+d.client+" to finish the job and collect payment.");}));else Say("Resolved",h.lethal?"The usable provisions are secured. Return to the client.":"The repaired habitat is holding. Return to the client for the promised provisions.");return;}
            if(bits[1]=="habitat")Say("Habitat",d.deduction,C(d.solution,()=>ResolveHunt(i,false)),Back());
            else Say(d.creature,"The animal has not attacked. You can leave it space and work on the habitat, or hunt it.",C("Hunt the "+d.creature,()=>{if(game.inventory.weapon==null){Say("Unarmed","You'll need a weapon. Garrick has basic equipment.");return;}game.CloseDialogue();game.StartCombat(props.sites[i],creatures[i]);}),C("Give it space.",game.CloseDialogue));
        }
        public void ResolveHunt(int i,bool lethal){if(!progress.hunts[i].Resolve(lethal))return;props.sites[i].cleared=true;props.Resolve(i,lethal);Say(definitions[i].title,lethal?"The immediate threat is gone. Inspect the cleared site, then return to the client.":"You finish the work and wait. The animal settles away from the damaged site. Return to the client.");}
        public bool CombatEnded(EncounterSite site,Phase phase)
        {
            if(site==garrickSite&&(phase==Phase.Won||phase==Phase.Lost))
            {
                game.world.HideGrid(site);site.actor.gameObject.SetActive(false);game.hp=Mathf.Max(1,game.hp);game.player.Place(new Vector3(0,.1f,-3));game.CloseDialogue();
                if(phase==Phase.Won){progress.defeatedGarrick=true;S.huntingBoardUnlocked=true;}
                game.intro.conversation.Show(phase==Phase.Won?"won":"lost");return true;
            }
            if(phase!=Phase.Won)return false;
            for(int i=0;i<3;i++)if(site==props.sites[i]){ResolveHunt(i,true);return true;}
            if(site==moonSite){S.mooncalfOutcome=MooncalfOutcome.Killed;game.opening.props.mooncalf.gameObject.SetActive(false);Say("Mooncalf","The Mooncalf is dead. The sealed sample beside the pasture remains available; you do not need to take anything from the carcass.");return true;}
            return false;
        }
        void Rooms(){if(progress.roomRented){Say("Garrick","Room's yours.",C("Go upstairs.",()=>{inRoom=true;game.CloseDialogue();game.player.Place(new Vector3(58,.1f,-26));}),Back());return;}Say("Garrick","Cheapest room's "+progress.roomPrice+" coins. You've got "+progress.coins+".",C("I'll take it.",()=>{if(progress.Rent())Rooms();else Say("Garrick","Come back when you've got the rest. The common room's here meanwhile.");}),Back());}
        void Shop()
        {
            Say("Garrick","Basic steel. Nothing fancy. You've got "+progress.coins+" coins.",C("Weapons — 12 coins",()=>{
                var options=new System.Collections.Generic.List<DialogueChoice>();foreach(var w in game.rules.weapons){var item=w;options.Add(C(item.Description+" • 12 coins",()=>{if(game.inventory.weapons.Contains(item)){Say("Garrick","You've already got that one.");return;}if(progress.coins<12){Say("Garrick","Twelve coins. Come back when you have them.");return;}progress.coins-=12;game.inventory.Store(item);Say("Garrick","There. Keep the edge clean.");}));}Say("Garrick", "Pick the one that suits the distance you want to keep.",options.ToArray());}),
                C("Reinforced coat — Armor 2, 16 coins",()=>{if(progress.coins<16||game.inventory.bodies.Exists(x=>x.title=="Reinforced Travel Coat")){Say("Garrick","You need sixteen coins and a reason to replace what you're wearing.");return;}progress.coins-=16;var coat=ScriptableObject.CreateInstance<BodyData>();coat.title="Reinforced Travel Coat";coat.armor=2;game.inventory.Store(coat);Say("Garrick","Should take a knock better.");}),C("Take a blade without paying.",Steal),Back());
        }
        void Steal(){game.intro.conversation.Show("take");}
        public void BeginTheftFight(){finishBoutWithoutIntroduction=S.invitedDownstairs&&game.firstLab.leading||game.firstLab.arrived;progress.criminal=true;progress.crimeWarned=true;Challenge();}
        void Challenge()
        {
            Say("Garrick","Outside. No one breaks my tables. We'll stop when one of us yields.",C("Meet him outside.",()=>{if(game.inventory.weapon==null)game.inventory.Receive(game.rules.weapons[0]);if(garrickSite==null)garrickSite=game.world.MakeSite(new Vector3(-20,0,15),false);garrickSite.actor.name="Garrick — practice bout";game.CloseDialogue();game.player.Place(new Vector3(-20,.1f,12));game.StartCombat(garrickSite,garrickEnemy);}),Back());
        }
        void Mooncalf()
        {
            if(S.mooncalfOutcome==MooncalfOutcome.Killed){Say("Pasture","The covered sample remains beside the pasture.");return;}
            Say("Mooncalf","Her ears turn toward you. Marlow said to keep your weapon lowered and let her approach.",
                C("Offer a handful of pasture grass and wait sideways.",()=>Say("Mooncalf","She sniffs the grass, then resumes grazing beside you.",C("Collect a small measure gently.",()=>{S.mooncalfOutcome=MooncalfOutcome.Peaceful;S.Gather(Ingredient.MooncalfMilk,game.inventory);Say("Mooncalf","You seal the measure. She goes back to the grass.");}))),
                C("Drive her away.",()=>{S.mooncalfOutcome=MooncalfOutcome.Frightened;game.opening.props.mooncalf.position=new Vector3(9,.8f,51);Say("Mooncalf","She bolts toward the far grass.",C("Leave her alone.",game.CloseDialogue),C("Chase her.",()=>{game.opening.props.mooncalf.position=new Vector3(10,.8f,53);Say("Mooncalf","She keeps fleeing. The sealed sample is still beside the pasture.");}));}),
                C("Attack her.",()=>{if(game.inventory.weapon==null){Say("Unarmed","You have no weapon equipped.");return;}if(moonSite==null){moonSite=game.world.MakeSite(new Vector3(0,0,49),false);moonSite.actor.gameObject.SetActive(false);moonSite.actor=game.opening.props.mooncalf;}game.CloseDialogue();game.StartCombat(moonSite,moonEnemy);}),Back());
        }
        void Sylvie()
        {
            if(!progress.kitchenAccess)return;
            if(!progress.demonstrated){Say("Sylvie","Out.",C("Garrick sent me.",()=>Say("Sylvie",SylvieIngredients.Describe(progress.food),C("Show me.",()=>BeginCooking(false)))));return;}
            if(!progress.cookingLearned){Say("Sylvie","Bring enough to cook with. And enough that I'm not teaching you at everyone else's expense.",C("All right. I'd like to learn.",()=>{progress.cookingLearned=true;Say("Sylvie","Don't look so pleased. You haven't cooked anything yet.\n\nCooking learned. You can practise here when you have ingredients and something for the pantry.",C("I'd like to try now.",()=>BeginCooking(true)),C("I'll come back with supplies.",game.CloseDialogue));}),C("You feed people who can't pay?",()=>Say("Sylvie","They're hungry, aren't they?",C("Fair enough. Teach me.",()=>{progress.cookingLearned=true;Say("Sylvie","Then bring enough to cook with, and something for them.");}))),Back());return;}
            Say("Sylvie","Pan's here. Ingredients aren't going to prepare themselves.",C("I'd like to practise.",()=>BeginCooking(true)),C("I've got something for the pantry.",Pantry),Back());
        }
        void Pantry(){if(progress.foodPortions<1){Say("Sylvie","Bring provisions. A contract's a decent place to start.");return;}Say("Shared pantry",progress.food+" ×"+progress.foodPortions,C("Contribute one portion.",()=>{progress.foodPortions--;progress.pantryContributions++;Say("Sylvie","That'll feed someone. Thank you.");}),Back());}
        void BeginCooking(bool practice)
        {
            if(cooking!=null&&cooking.step!=CookStep.Complete){game.CloseDialogue();game.mode=GameMode.Cooking;return;}
            if(progress.foodPortions<1||practice&&progress.pantryContributions<1){Say("Sylvie","An ingredient for your pan, and a contribution for the pantry. Then we'll cook.",C("Use the pantry.",Pantry),Back());return;}
            progress.foodPortions--;if(practice)progress.pantryContributions--;
            cooking=new CookingSession{ingredient=progress.food,practice=practice};game.CloseDialogue();game.mode=GameMode.Cooking;
        }
        public void CookAction()
        {
            if(!cooking.Advance()||cooking.step!=CookStep.Complete)return;
            progress.wellFed=true;
            if(cooking.practice){Say("Sylvie","There. You watched what it was doing. Eat it while it's warm.");return;}
            progress.demonstrated=true;
            Say("Sylvie","She sets a small plate in front of you. The first bite stops you halfway to the second.",C("...What did you do to this?",()=>Say("Sylvie","Cooked it.",C("No. Seriously.",()=>Say("Sylvie","So am I.",C("Could you teach me?",Sylvie),C("I'll let you get back to it.",game.CloseDialogue))))));
        }
        void GatherAgain(string key)
        {
            int patch=key=="bloodleaf"?0:key=="silvermoss"?1:2;
            if(Time.time<gatherReadyAt[patch]){Say("Gathering","Leave the patch and herd time to recover. Return in a little while.");return;}
            if(key=="bloodleaf")game.inventory.bloodleaf+=2;else if(key=="silvermoss")game.inventory.silvermoss++;else {if(S.mooncalfOutcome==MooncalfOutcome.Killed){Say("Pasture","The Mooncalf is gone. This pasture no longer provides fresh milk.");return;}game.inventory.mooncalfMilk++;}
            gatherReadyAt[patch]=Time.time+gatherCooldown;Say("Gathered",key=="bloodleaf"?"Two useful Bloodleaf portions: one for a preparation and one available to contribute.":"One fresh portion, carefully stored.");
        }
        void RepeatPotion()
        {
            if(repeatBrew){game.CloseDialogue();game.mode=GameMode.Brewing;return;}
            var inv=game.inventory;
            if(inv.bloodleaf<2||inv.silvermoss<1||inv.mooncalfMilk<1){Say("Marlow","One of each for the preparation, plus a useful contribution. A second portion of Bloodleaf will do. Gather carefully and let the patches recover.");return;}
            inv.bloodleaf-=2;inv.silvermoss--;inv.mooncalfMilk--;progress.labContributions++;repeatBrew=true;game.opening.brew=new PotionSession();game.CloseDialogue();game.mode=GameMode.Brewing;
        }
        public void Tick(float delta)
        {
            if(game.mode!=GameMode.Defeated)progress.TickIllness(delta,S.invitedDownstairs,S.trollTreated);
            if(progress.marlowGone&&!departureShown){departureShown=true;game.opening.props.labMarlow.SetActive(false);game.world.innMarlow.SetActive(false);game.opening.props.trollHead.localScale*=.95f;game.notice="The laboratory has fallen quiet.";}
            if(S.trollTreated&&!progress.paidByMarlow){progress.paidByMarlow=true;progress.coins+=progress.marlowPayment;}
            if(progress.hunts[2].resolved)props.wheel.Rotate(Vector3.up,delta*35,Space.Self);
            if(!loanOffered&&S.questAccepted&&game.inventory.weapon==null&&!game.opening.inLab&&game.mode==GameMode.Exploration&&game.player.transform.position.z>5&&game.player.transform.position.z<8){loanOffered=true;game.Loan();}
        }
    }
}

