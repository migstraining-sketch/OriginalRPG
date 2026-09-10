using UnityEngine;

namespace WoodlandSpine
{
    public sealed class CompanionPresence
    {
        public readonly Combatant ily;
        public readonly Transform actor;
        public bool recruited,active,met,following,controlAsked;
        public Region location=Region.Reedwater;
        readonly SliceGame game;
        public CompanionPresence(SliceGame game)
        {
            this.game=game;
            ily=new Combatant{id="ily",title="Ily",side=CombatSide.Allies,controller=CombatController.Independent,hp=game.rules.ilyHP,maxHP=game.rules.ilyHP,inventory=new Inventory()};
            ily.inventory.Receive(game.rules.weapons[1]);
            var p=game.full.props.origins[0]+new Vector3(-3,.9f,-6);
            actor=game.world.Shape("Ily — irrigation hand",p,new Vector3(.65f,.9f,.65f),new Color(.52f,.43f,.27f),PrimitiveType.Capsule,false).transform;
            game.world.Interact(actor.gameObject,"ily","Speak to Ily");
            var spear=game.world.Shape("Ily's field spear",p+Vector3.right*.5f,new Vector3(.07f,2.2f,.07f),new Color(.34f,.26f,.15f),solid:false);spear.transform.SetParent(actor,true);
        }
        public bool Present=>location==game.coordinated.travel.knowledge.current;
        public void Arrive(Region destination)
        {
            if(active){location=destination;actor.position=game.player.transform.position+new Vector3(1.4f,.8f,1);}
            game.coordinated.travel.PlaceInRegion(actor,location);
            actor.gameObject.SetActive(Present);
        }
        public void Tick(float delta)
        {
            if(!Present||game.mode!=GameMode.Exploration||game.Modal)return;
            if(location==Region.Reedwater&&game.player.transform.position.z>-5&&Vector3.Distance(actor.position,game.player.transform.position)<8)following=true;
            if(!(active||following&&location==Region.Reedwater)||game.opening.inLab||game.full.inRoom||game.full.inKitchen)return;
            Vector3 destination=game.player.transform.position+new Vector3(1.4f,.8f,-1.4f);
            if(Vector3.Distance(actor.position,destination)>2.2f)actor.position=Vector3.MoveTowards(actor.position,destination,delta*4.4f);
        }
        public bool JoinCombat()
        {
            if(!Present||ily.needsRest||Vector3.Distance(actor.position,game.player.transform.position)>12)return false;
            ily.cell=game.combat.grid.NearestOpen(actor.position,new Hex(99,99));
            if(!game.combat.OpenFor(ily.cell,ily))
            {
                float distance=float.MaxValue;bool found=false;
                foreach(var h in game.combat.grid.cells)if(game.combat.OpenFor(h,ily))
                {float d=(game.combat.grid.World(h)-actor.position).sqrMagnitude;if(d<distance){distance=d;ily.cell=h;found=true;}}
                if(!found)return false;
            }
            ily.departed=false;
            if(!game.combat.AddAlly(ily))return false;
            game.battle.actors[ily]=actor;return true;
        }
        public void Recover()
        {
            if(game.combat.units.Contains(ily)){ily.RecoverAfterEncounter();actor.gameObject.SetActive(Present);}
        }
        public void Rest(){if(Present){ily.Rest();actor.gameObject.SetActive(true);}}
        DialogueChoice C(string label,System.Action action)=>new DialogueChoice(label,action);
        public void Conversation()
        {
            if(game.coordinated.mud.state.Complete)
            {
                if(!recruited)game.Talk("Ily",game.coordinated.mud.state.dead?"I'll shore up the bund. With the animal gone, it should hold this time.":"There. Water's moving, and it's digging clear of the rice. That's a repair worth doing.",
                    C("You could come with me. I could use someone practical.",()=>game.Talk("Ily","I move between jobs anyway. All right. Ask when you're setting out.",C("Travel with me.",()=>{recruited=true;active=true;game.CloseDialogue();}),C("We'll meet at the inn.",()=>{recruited=true;active=false;location=Region.Inn;actor.position=new Vector3(4,.9f,-5);game.coordinated.travel.PlaceInRegion(actor,location);actor.gameObject.SetActive(false);game.CloseDialogue();}))),C("I'll leave you to the repairs.",game.CloseDialogue));
                else game.Talk("Ily","Where are we headed?",C(active?"Wait here for now.":"Come along.",()=>{active=!active;following=false;game.CloseDialogue();}),C("About how we work together...",Control),C("I'll be back.",game.CloseDialogue));
                return;
            }
            met=true;
            game.Talk("Ily",game.coordinated.mud.state.patchOpen?"You opened that crusted patch? Give the water a way through and watch what it does.":"Third repair on the same edge. I can patch the break. Can't promise it'll stay patched.",
                C("I'm here about the crop damage.",()=>{following=true;game.Exchange("Ily","Then I'll walk with you. Toma's tired of chasing the same problem round the field.",game.CloseDialogue);}),
                C("If it turns on us, can we work together?",Control),C("I'll have a look around.",game.CloseDialogue));
        }
        public void Control()
        {
            game.Talk("Ily","If it turns on us—your calls, or mine?",
                C("Stay on my lead.",()=>{ily.controller=CombatController.Direct;controlAsked=true;following=true;game.CloseDialogue();}),
                C("Use your judgment.",()=>{ily.controller=CombatController.Independent;controlAsked=true;following=true;game.CloseDialogue();}));
        }
    }
}
