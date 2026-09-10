using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    // Connects approved opening systems. Their rules/state remain in dedicated small classes.
    public sealed class CoordinatedSlice : MonoBehaviour
    {
        public SliceGame game;
        public RegionalTravel travel;
        [System.NonSerialized] public CompanionPresence party;
        [System.NonSerialized] public MooncalfHerd herd;
        [System.NonSerialized] public MudInTheMoonrice mud;
        public readonly RoomStorage storage=new RoomStorage();
        public readonly List<string> observations=new List<string>();
        readonly Dictionary<string,string> discoveries=new Dictionary<string,string>();
        bool flaskLent,failureDeparturePending;
        public bool Modal=>travel!=null&&travel.Blocking||storage.visible;
        public void Initialize(SliceGame value)
        {
            game=value;game.full.progress.provisions=game.inventory.provisions;
            mud=new MudInTheMoonrice(game);party=new CompanionPresence(game);herd=new MooncalfHerd(game);
            BuildWoodland();
            travel=game.gameObject.AddComponent<RegionalTravel>();travel.Initialize(game);
        }
        DialogueChoice C(string label,System.Action action)=>new DialogueChoice(label,action);
        public bool Handle(string key)
        {
            if(game.full.progress.marlowGone&&(key=="lab_marlow"||key=="marlow"||key=="troll"||key=="workbench"||key=="basement"))return false;
            if(key=="regional_exit")
            {
                if(travel.knowledge.current==Region.Inn&&game.opening.state.questAccepted&&game.inventory.weapon==null){game.Loan();return true;}
                travel.Open();return true;
            }
            if(key=="back_property"){game.Exchange("Rear service door","This leads to the inn's local property. Regional journeys leave through the front entrance. The service area is not open in this prototype.",game.CloseDialogue);return true;}
            if(key=="basic_rest"||key=="room_rest")
            {
                if(key=="room_rest"&&!game.full.progress.roomRented)return true;
                game.hp=game.rules.playerHP;party.Rest();game.notice="You rest. HP restored; present companions are ready to fight again.";return true;
            }
            if(key=="room_chest")
            {
                if(!game.full.progress.roomRented||!game.full.inRoom){game.Exchange("Room chest","Not your room.",game.CloseDialogue);return true;}
                storage.visible=true;game.showInventory=false;return true;
            }
            if(key=="ily"){party.Conversation();return true;}
            if(key=="guest_room"){game.Exchange("Guest room","This room is occupied.",game.CloseDialogue);return true;}
            if(key.StartsWith("mud_")){mud.Handle(key);return true;}
            if(key=="mooncalf"||key.StartsWith("herd_")){herd.Observe(key=="mooncalf"?0:int.Parse(key.Substring(5)));return true;}
            if(key=="milk_cache")return true;
            if(discoveries.TryGetValue(key,out string note))
            {if(!observations.Contains(note))observations.Add(note);game.Exchange("Observation",note,game.CloseDialogue);return true;}
            if((key=="lab_marlow"||key=="troll")&&herd.state.SourceLost&&!herd.state.failureReported)
            {
                game.Talk("Marlow","You're back. Do you have the milk?",C("I killed the nursing Mooncow. I couldn't bring a serving.",()=>game.Exchange("Marlow","Then I can't finish this preparation.\n\nHe looks toward the little troll.\n\nI'll stay with him for now. There isn't anything else I can send you for.",()=>
                {
                    herd.state.failureReported=true;game.opening.state.treatmentFailed=true;game.opening.state.huntingBoardUnlocked=true;game.CloseDialogue();
                    game.notice="The treatment cannot be completed. Garrick's local work remains available.";
                })));return true;
            }
            if((key=="lab_marlow"||key=="troll"||key=="workbench")&&herd.state.failureReported)
            {game.Exchange("Marlow","I'll stay with him. Please give us a little room.",game.CloseDialogue);return true;}
            if(key=="lab_marlow"&&game.opening.state.AllGathered&&!game.opening.state.returnedToMarlow)
            {
                game.Exchange("Marlow","You've got them. Good. He's still with us. Bring them to the bench—we can prepare the treatment now.",()=>{game.opening.state.returnedToMarlow=true;game.CloseDialogue();});return true;
            }
            if(key=="lab_marlow"&&game.opening.state.trollTreated&&game.opening.state.mossbackPursued&&!game.opening.state.marlowKnowsMossbackIncident)
            {
                game.Talk("Marlow","He's eating again. I'll keep watching him.",C("Something happened in the woods.",()=>game.Talk("Marlow","What happened?",C("A Mossback attacked me.",()=>
                {
                    game.opening.state.marlowKnowsMossbackIncident=true;
                    game.Talk("Marlow","Was it cornered?",C("I left room. It followed me anyway.",()=>game.Exchange("Marlow","That's strange. They're not known to behave that way. I'll look into it when I can leave him safely.",game.CloseDialogue)));
                }))),C("I'll let you tend him.",game.CloseDialogue));return true;
            }
            return false;
        }
        public string Objective
        {
            get
            {
                if(herd.state.SourceLost&&!herd.state.failureReported)return "Return to Marlow. The herd's required milk serving can no longer be obtained.";
                if(game.full.progress.hunts[0].accepted&&!game.full.progress.hunts[0].rewarded)return mud.Objective;
                if(herd.state.failureReported&&!game.full.progress.huntingLearned)return "The treatment could not be completed. Local contracts are available at Garrick's board.";
                return null;
            }
        }
        public void Tick(float delta)
        {
            if(game.opening.state.questAccepted)
            {
                travel.knowledge.Discover(Region.Woodland);
                if(!flaskLent){flaskLent=true;game.inventory.cleanFieldFlask=true;}
            }
            if(game.full.progress.hunts[0].accepted)travel.knowledge.Discover(Region.Reedwater);
            game.world.inn.Tick(game);
            travel.TickExit();
            herd.SyncCombat();party.Tick(delta);mud.Tick(delta);
            if(game.mode==GameMode.Exploration&&!Modal&&party.Present&&party.following&&!party.controlAsked&&travel.knowledge.current==Region.Reedwater&&game.player.transform.position.z>3&&Vector3.Distance(party.actor.position,game.player.transform.position)<5)
                party.Control();
        }
        public bool CombatEnded(Phase phase)
        {
            if(phase==Phase.Won||phase==Phase.Fled)party.Recover();
            herd.SyncCombat();
            if(game.site==herd.site){herd.RestoreVisibility();return true;}
            if(game.site==game.full.props.sites[0]){mud.CombatEnded(phase);return true;}
            return false;
        }
        public void AfterTravel(Region destination)
        {
            if(herd.state.failureReported&&destination!=Region.Inn)failureDeparturePending=true;
            else if(failureDeparturePending&&destination==Region.Inn)
            {
                game.full.progress.marlowGone=true;game.opening.props.labMarlow.SetActive(false);game.world.innMarlow.SetActive(false);
            }
            herd.RestoreVisibility();
            game.opening.props.usefulLeaf.SetActive(!game.opening.state.bloodleafObtained);
        }
        void Discovery(string key,string name,Vector3 position,string note,Color color,Vector3 scale,PrimitiveType type=PrimitiveType.Sphere)
        {
            discoveries[key]=note;var prop=game.world.Shape(name,position,scale,color,type,false);game.world.Interact(prop,key,"Look closely");
        }
        void BuildWoodland()
        {
            var w=game.world;Color green=new Color(.25f,.37f,.23f);
            Discovery("observe_sunberry","Sunberry shrub",new Vector3(7,.6f,24),"Sunberry catches the light at the edge of this small clearing. Birds have pecked several berries. You leave the plant undisturbed.",new Color(.64f,.47f,.18f),new Vector3(1.8f,1.1f,1.4f));
            Discovery("observe_fungus","Fungus on rotten wood",new Vector3(-9,.35f,39),"Pale fungus runs along the damp underside of a fallen log. The exposed upper wood is dry and brittle.",new Color(.62f,.6f,.48f),new Vector3(1.6f,.35f,.6f));
            w.Shape("Rotten fallen trunk",new Vector3(-9,.35f,39.6f),new Vector3(3,.7f,.9f),new Color(.3f,.24f,.16f),solid:false);
            Discovery("observe_resin","Resin on bark",new Vector3(10,1,43),"Amber resin has set over a split in the bark. Insects have gathered near the fresher drops.",new Color(.72f,.4f,.16f),new Vector3(.4f,.6f,.3f));
            Discovery("observe_burrow","Small forager burrow",new Vector3(-9,.12f,26),"A narrow burrow opens beneath roots. Seed husks and small tracks lie around the mouth.",new Color(.16f,.15f,.12f),new Vector3(.8f,.2f,.7f));
            Discovery("observe_spring","Old split tree and spring",new Vector3(9,.1f,62),"Water wells up between the roots of an old split tree. One half is hollow; the other still carries leaves.",new Color(.32f,.5f,.51f),new Vector3(2,.15f,2));
            w.Shape("Small Woodland stream",new Vector3(-10,.015f,57),new Vector3(1.8f,.025f,22),new Color(.25f,.42f,.47f),solid:false);
            w.Shape("Resin-bearing trunk",new Vector3(10,1.25f,43.2f),new Vector3(.5f,2.5f,.5f),new Color(.32f,.25f,.17f),PrimitiveType.Cylinder);
            var split=w.Shape("Old split trunk",new Vector3(8.4f,1.4f,62),new Vector3(.7f,2.8f,.7f),new Color(.29f,.23f,.16f),PrimitiveType.Cylinder);split.transform.rotation=Quaternion.Euler(0,0,12);
            w.Shape("Broken half of old tree",new Vector3(9.3f,.65f,62.5f),new Vector3(.6f,1.3f,.6f),new Color(.29f,.23f,.16f),PrimitiveType.Cylinder);
            w.Shape("Leaves on living half",new Vector3(8,3,62),new Vector3(2.2f,1,2),green,PrimitiveType.Sphere,false);
            foreach(var p in new[]{new Vector3(10,0,43.2f),new Vector3(8.4f,0,62),new Vector3(9.3f,0,62.5f)})
            foreach(var encounter in new[]{w.wildlife,w.mossback,herd.site}){var cell=encounter.grid.At(p);if(encounter.grid.cells.Contains(cell))encounter.grid.blocked.Add(cell);}
            foreach(var point in new[]{new Vector3(-4,.025f,22),new Vector3(-7,.025f,28),new Vector3(-8,.025f,35),new Vector3(-7,.025f,42),new Vector3(-3,.025f,46)})
                w.Shape("Worn side trail around mixed canopy",point,new Vector3(3.5f,.025f,7),new Color(.28f,.29f,.18f),solid:false);
            // Ambient silhouettes stay outside authored combat cells; major obstacles use the grid's own trees.
            for(int i=0;i<12;i++)
            {
                Vector3 p=new Vector3(i%2==0?-11.5f:11.5f,0,20+i*4.7f);int family=i%3;
                w.Shape(family==0?"Broadleaf hardwood":family==1?"Pale-barked tree":"Evergreen",p+Vector3.up*1.4f,new Vector3(.5f,2.8f,.5f),family==1?new Color(.68f,.66f,.57f):new Color(.32f,.25f,.17f),PrimitiveType.Cylinder);
                w.Shape("Ambient crown",p+Vector3.up*3,new Vector3(family==2?1.8f:3.2f,family==2?3:1.4f,2.5f),green,PrimitiveType.Sphere,false);
            }
        }
    }
}
