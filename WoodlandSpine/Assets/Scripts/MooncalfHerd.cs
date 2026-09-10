using UnityEngine;

namespace WoodlandSpine
{
    public sealed class MooncalfHerd
    {
        public readonly HerdState state=new HerdState();
        public readonly EncounterSite site;
        public readonly Transform[] actors=new Transform[3];
        readonly EnemyData[] data=new EnemyData[3];
        readonly int[] health=new int[3];
        float nextServing;
        readonly Combatant[] battleUnits=new Combatant[3];
        readonly SliceGame game;
        public MooncalfHerd(SliceGame game)
        {
            this.game=game;var w=game.world;
            // This encounter overlays the herd's own meadow, using the existing local terrain.
            site=w.MakeSite(new Vector3(4,0,72),false);Object.Destroy(site.actor.gameObject);
            actors[0]=game.opening.props.mooncalf;actors[0].position=new Vector3(3,.65f,71);actors[0].localScale*=.7f;
            for(int i=0;i<3;i++)
            {
                string name=i==0?"Mooncalf":i==1?"Nursing Mooncow":"Protective adult";
                data[i]=i==0?game.rules.juvenileMooncalf:i==1?game.rules.nursingMooncow:game.rules.protectiveAdult;health[i]=data[i].hp;
                if(i>0){actors[i]=w.Shape(name,new Vector3(i==1?6:1,.8f,74),new Vector3(1.5f,1.4f,1.8f),new Color(.66f,.69f,.57f),PrimitiveType.Sphere,false).transform;w.Interact(actors[i].gameObject,"herd_"+i,"Observe "+name);}
            }
            site.actor=actors[0];
            foreach(var interaction in w.interactions)if(interaction.key=="milk_cache")interaction.gameObject.SetActive(false);
        }
        DialogueChoice C(string label,System.Action action)=>new DialogueChoice(label,action);
        void Say(string who,string text)=>game.Exchange(who,text,game.CloseDialogue);
        public void Observe(int member)
        {
            if(health[member]<=0){Say("Herd meadow","The animal is dead. There is no milk to collect from its body.");return;}
            if(member==1)
            {
                game.Talk("Nursing Mooncow","The calf stays close to its mother. Another adult watches from the edge of the grass.",
                    C("Lower my weapon and wait at a distance.",()=>game.Talk("Herd meadow","The adults settle. The Mooncow returns to grazing, with the calf close beside her.",
                        C("Collect a small serving in Marlow's flask.",Collect),C("Give them space.",game.CloseDialogue))),
                    C("Attack the Mooncow.",()=>Attack(member)),C("Leave them in peace.",game.CloseDialogue));
            }
            else game.Talk(member==0?"Mooncalf":"Protective adult",member==0?"A juvenile noses through the grass beside a nursing Mooncow. Two adults lift their heads as you approach.":"The adult stands between you and the young animal.",
                C("Watch without approaching.",()=>Say("Field observation","The calf feeds, then returns to its mother. The herd gradually resumes grazing.")),
                C("Attack.",()=>Attack(member)),C("Give them room.",game.CloseDialogue));
        }
        void Collect()
        {
            if(!game.opening.state.questAccepted){Say("Field observation","You have no reason or suitable vessel to take a serving.");return;}
            if(game.opening.state.trollTreated&&state.milkTaken&&!state.sourceDead&&game.inventory.cleanFieldFlask)
            {
                if(Time.time<nextServing){Say("Herd meadow","Give the mother and calf time. You have taken a serving recently.");return;}
                game.inventory.mooncalfMilk++;game.inventory.cleanFieldFlask=false;nextServing=Time.time+game.full.gatherCooldown;Say("Mooncalf Milk","You take one serving for the learned preparation and leave the herd grazing.");return;
            }
            if(state.Collect(game.inventory.cleanFieldFlask))
            {game.opening.state.Gather(Ingredient.MooncalfMilk,game.inventory);game.inventory.cleanFieldFlask=false;nextServing=Time.time+game.full.gatherCooldown;Say("Mooncalf Milk","A measured serving fills the flask. You cork it and step back, leaving the calf with its mother.");}
            else Say("Herd meadow",state.milkTaken?"You already have the serving Marlow needs.":"You cannot collect a serving without a living nursing source and the clean field flask.");
        }
        public void Attack(int member)
        {
            if(game.inventory.weapon==null){Say("Unarmed","You need a weapon to attack.");return;}
            var checkpoint=(int[])health.Clone();
            var positions=new Vector3[actors.Length];for(int i=0;i<actors.Length;i++)positions[i]=actors[i].position;
            state.provoked=true;game.CloseDialogue();site.actor=actors[member];
            game.StartCombat(site,data[member],site.grid.NearestOpen(actors[member].position,new Hex(99,99)));
            for(int i=0;i<3;i++)
            {
                battleUnits[i]=null;if(health[i]<=0)continue;
                Combatant unit;
                if(i==member)unit=game.combat.Target;
                else
                {
                    var cell=site.grid.NearestOpen(actors[i].position,new Hex(99,99));
                    if(game.combat.At(cell)!=null)foreach(var h in site.grid.cells)if(game.combat.OpenFor(h,null)){cell=h;break;}
                    unit=game.combat.AddEnemy(data[i],cell,"herd-"+i);
                }
                unit.hp=health[i];battleUnits[i]=unit;game.battle.actors[unit]=actors[i];
            }
            game.coordinated.party.JoinCombat();game.combat.CompleteSetup();game.Refresh();
            game.retryEncounter=()=>{for(int i=0;i<health.Length;i++){health[i]=checkpoint[i];actors[i].position=positions[i];}state.sourceDead=health[1]<=0;Attack(member);};
        }
        public void SyncCombat()
        {
            if(game.site!=site||game.combat==null)return;
            for(int i=0;i<3;i++)if(battleUnits[i]!=null)health[i]=battleUnits[i].hp;
            state.sourceDead=health[1]<=0;
        }
        public void RestoreVisibility()
        {
            bool here=game.coordinated.travel.knowledge.current==Region.Woodland;
            for(int i=0;i<3;i++)actors[i].gameObject.SetActive(here&&health[i]>0);
            game.world.HideGrid(site);
            foreach(var interaction in game.world.interactions)if(interaction.key=="milk_cache")interaction.gameObject.SetActive(false);
        }
    }
}
