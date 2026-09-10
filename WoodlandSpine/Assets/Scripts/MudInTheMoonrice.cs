using UnityEngine;

namespace WoodlandSpine
{
    public sealed class MudInTheMoonrice
    {
        public readonly MudInvestigation state=new MudInvestigation();
        readonly SliceGame game;
        readonly EnemyData creature;
        readonly Vector3 origin,feeding;
        readonly Transform patch,runnel;
        float feedingTime;
        bool credited;
        public readonly string[] clues={
            "The shoots are flattened and pushed aside. There is little clean bite damage. Something dug beneath them.",
            "Pale mudgrubs wriggle in the freshly turned soil. There are many beneath the rice bed.",
            "Broad paired tracks cross an older bund repair and turn toward wetter ground."};
        public readonly string[] signs={"Broad outbound tracks pass through the low, broken bund.","Mud rubbed high on the reeds marks a turn toward the wet margin.","Fresh digging exposes more mudgrubs. The animal is close and still feeding."};
        public MudInTheMoonrice(SliceGame game)
        {
            this.game=game;origin=game.full.props.origins[0];feeding=origin+new Vector3(11,.55f,13);
            creature=game.rules.reedback;
            var w=game.world;
            Vector3[] evidence={new Vector3(-7,0,-5),new Vector3(-3,0,-4),new Vector3(0,0,0)};
            for(int i=0;i<3;i++)
            {
                var prop=w.Shape(i==0?"Churned feeding hollow":i==1?"Exposed mudgrubs":"Broad tracks across old bund repair",origin+evidence[i]+Vector3.up*.07f,new Vector3(1.4f,.12f,1),new Color(.45f,.39f,.25f),solid:false);
                w.Interact(prop,"mud_clue_"+i,"Inspect feeding signs");
                if(i==1)for(int n=0;n<4;n++)w.Shape("Pale mudgrub",origin+evidence[i]+new Vector3(n*.17f-.2f,.17f,0),new Vector3(.08f,.08f,.23f),new Color(.78f,.72f,.51f),PrimitiveType.Capsule,false);
            }
            Vector3[] trail={new Vector3(0,0,1),new Vector3(4,0,4),new Vector3(6,0,8)};
            for(int i=0;i<3;i++)
            {
                var prop=w.Shape(i==1?"Muddy flank rub on reeds":"Fresh outbound sign",origin+trail[i]+Vector3.up*.09f,new Vector3(.7f,.16f,1.2f),new Color(.36f,.28f,.17f),solid:false);
                w.Interact(prop,"mud_track_"+i,"Examine the trail");
            }
            w.Interact(game.full.props.sites[0].actor.gameObject,"mud_target","Observe Reedback");
            patch=w.Shape("Crusted wet margin",feeding-Vector3.up*.48f,new Vector3(4,.12f,3),new Color(.4f,.32f,.2f),solid:false).transform;
            w.Interact(patch.gameObject,"mud_patch","Inspect crusted wet ground",feeding+new Vector3(0,-.55f,-2));
            runnel=w.Shape("Compacted muddy runnel",origin+new Vector3(9,.06f,9),new Vector3(1.2f,.1f,6),new Color(.38f,.31f,.21f),solid:false).transform;
            w.Interact(runnel.gameObject,"mud_runnel","Inspect the small runnel",origin+new Vector3(10,0,8));
            w.Shape("Lowland irrigation channel",origin+new Vector3(14,.01f,3),new Vector3(1.8f,.03f,27),new Color(.25f,.43f,.45f),solid:false);
            w.Shape("Irrigation tools",origin+new Vector3(-2,.2f,-6),new Vector3(.15f,.2f,1.5f),new Color(.35f,.26f,.16f),solid:false);
        }
        public string Notes
        {
            get
            {
                string text="";for(int i=0;i<3;i++)if((state.evidence&(1<<i))!=0)text+=clues[i]+"\n";
                if(state.GrubsInferred)text+="The digging exposes mudgrubs beneath the crop. Those seem to be the food it is after.\n";
                else if(state.Inferred)text+="It seems to be digging through the crop for food beneath the mud.\n";
                for(int i=0;i<3;i++)if((state.trail&(1<<i))!=0)text+=signs[i]+"\n";
                return text;
            }
        }
        public string Objective=>state.Complete?"Mud in the Moonrice • Report the resolved crop problem to Toma.":state.dead?"Harvest the Reedback for its usable haunch.":state.Prepared?"Watch what the Reedback does with the opened wet margin.":"Mud in the Moonrice • Investigate the damage and deal with its cause.";
        DialogueChoice C(string label,System.Action action)=>new DialogueChoice(label,action);
        void Say(string who,string text)=>game.Exchange(who,text,game.CloseDialogue);
        public void Handle(string key)
        {
            if(key.StartsWith("mud_clue_")){int i=int.Parse(key.Substring(9));state.Inspect(i);Say("Observation",clues[i]+(state.Inferred?"\n\n"+(state.GrubsInferred?"The animal appears to be digging for mudgrubs beneath the crop.":"The digging seems aimed beneath the crop, rather than at the rice itself."):""));return;}
            if(key.StartsWith("mud_track_")){int i=int.Parse(key.Substring(10));state.Track(i);Say("Trail sign",signs[i]);return;}
            if(key=="mud_client"){Client();return;}
            if(key=="mud_patch")
            {
                if(state.patchOpen){Say("Wet margin","The crust is broken. Mudgrubs are exposed in soft wet soil.");return;}
                game.Talk("Wet margin","Beneath the crust, the mud is soft. A few pale grubs show through a narrow crack.",C("Loosen the crusted feeding patch.",()=>{state.patchOpen=true;patch.GetComponent<Renderer>().sharedMaterial=game.world.Material(new Color(.23f,.2f,.13f));Assist(patch.position);Say("Wet margin","You loosen the crust. More mudgrubs wriggle through the opened soil.");}),C("Leave it for now.",game.CloseDialogue));return;
            }
            if(key=="mud_runnel")
            {
                if(state.runnelOpen){Say("Runnel","Water follows the shallow channel away from the rice.");return;}
                game.Talk("Small runnel","Compacted mud closes the shallow connection between this wet margin and the worked ground.",C("Open the muddy connection.",()=>{state.runnelOpen=true;runnel.GetComponent<Renderer>().sharedMaterial=game.world.Material(new Color(.24f,.4f,.43f));Assist(runnel.position);Say("Runnel","Water seeps along the opened connection toward the wet margin.");}),C("Leave it for now.",game.CloseDialogue));return;
            }
            if(key=="mud_target")
            {
                if(state.dead)
                {
                    if(state.harvested){Say("Reedback remains","The usable haunch has been prepared and packed.");return;}
                    game.Talk("Reedback carcass","The animal is dead. The usable meat has not yet been taken.",C("Harvest Reedback.",()=>{if(state.Harvest()){var p=game.full.progress;p.food="Fresh Reedback Haunch";p.foodPortions+=3;Credit();}Say("Harvest","Fresh Reedback Haunch stored.");}),C("Step away.",game.CloseDialogue));return;
                }
                game.Talk("Reedback",state.feedingObserved?"It digs repeatedly in the opened wet margin, clear of the rice.":"The Reedback noses into soft ground and digs. It has not turned to attack you.",
                    C("Watch from a distance.",()=>{game.CloseDialogue();game.notice=state.Prepared?"Watch the opened patch. The animal has a clear connection to it.":"It digs, pauses, then digs again. Simply chasing it away will not change what brings it here.";}),
                    C("Attack the Reedback.",Attack),C("Leave it space.",game.CloseDialogue));
            }
        }
        void Assist(Vector3 p){var party=game.coordinated.party;if(party.Present&&!party.ily.needsRest&&Vector3.Distance(party.actor.position,p)<12)party.actor.position=p+new Vector3(-1,.9f,0);}
        public void Attack()
        {
            if(game.inventory.weapon==null){Say("Unarmed","You need a weapon to attack.");return;}
            game.CloseDialogue();game.StartCombat(game.full.props.sites[0],creature,game.full.props.sites[0].grid.NearestOpen(game.full.props.sites[0].actor.position,new Hex(99,99)));
            game.coordinated.party.JoinCombat();game.combat.CompleteSetup();game.Refresh();
            game.retryEncounter=Attack;
        }
        public void CombatEnded(Phase phase)
        {
            if(phase==Phase.Won){state.Killed();var h=game.full.progress.hunts[0];h.resolved=false;h.lethal=true;h.harvested=false;game.full.props.sites[0].actor.gameObject.SetActive(true);game.full.props.sites[0].actor.localScale=new Vector3(1,.3f,1.2f);game.notice="Reedback defeated. Harvest is a separate action.";}
            if(phase==Phase.Fled)game.notice="The Reedback still has reason to return to the crop. The job is unfinished.";
        }
        public void Tick(float delta)
        {
            if(game.coordinated.travel.knowledge.current!=Region.Reedwater||game.mode!=GameMode.Exploration||game.Modal||state.dead||!state.Prepared)return;
            var actor=game.full.props.sites[0].actor;
            if(Vector3.Distance(actor.position,feeding)>.1f){actor.position=Vector3.MoveTowards(actor.position,feeding,delta*1.5f);feedingTime=0;return;}
            // Several digging motions, witnessed from nearby, establish lasting feeding behavior.
            feedingTime+=delta;actor.rotation=Quaternion.Euler(Mathf.Sin(feedingTime*3)*9,0,0);
            if(feedingTime>=5&&Vector3.Distance(game.player.transform.position,feeding)<9&&state.ObserveFeeding())
            {Credit();game.notice="The Reedback settles into repeated feeding away from the rice. The crop problem is resolved.";}
        }
        void Credit()
        {
            if(!state.Complete)return;
            var h=game.full.progress.hunts[0];h.resolved=true;h.lethal=state.dead;h.harvested=state.harvested;
            if(!credited){credited=true;game.full.progress.huntingLearned=true;}
            game.full.props.damage[0].SetActive(false);
        }
        void Client()
        {
            var p=game.full.progress;var h=p.hunts[0];
            if(state.Complete)
            {
                if(!h.rewarded)
                {
                    h.rewarded=true;p.coins+=p.huntReward;
                    if(!state.dead){p.food="Preserved Reedback Cut";p.foodPortions+=3;}
                    Say("Toma",state.dead?"That'll stop it coming through the crop. You've taken the useful meat? Good. Here's your payment.":"It's feeding over there now? Good. If it stays clear of the rice, we can repair this properly. Here's your payment—and preserved Reedback from an earlier cull. Something for your table.");
                }
                else Say("Toma","The new shoots should take, now the ground can settle.");
                return;
            }
            Say("Toma","A Reedback keeps coming through that low bund. Flattens the rice, tears the ground up. We chase it off; it comes back. I need the damage to stop.");
        }
    }
}
