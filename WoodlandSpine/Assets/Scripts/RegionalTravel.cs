using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    public sealed class RegionalTravel : MonoBehaviour
    {
        public readonly TravelKnowledge knowledge=new TravelKnowledge();
        public bool visible,travelling;
        public bool Blocking=>visible||travelling;
        public float progress;
        [System.NonSerialized] public Region? selected;
        SliceGame game;
        bool exitLatched;
        readonly Dictionary<Region,GameObject> regions=new Dictionary<Region,GameObject>();
        public static string Title(Region region)=>region==Region.Inn?"Garrick's Inn":region==Region.Woodland?"Woodland":"Reedwater Paddies";
        public void Initialize(SliceGame value)
        {
            game=value;
            var w=game.world;
            var trail=w.Shape("Woodland trailhead",new Vector3(0,.05f,14),new Vector3(3,.1f,1.3f),new Color(.48f,.39f,.26f),solid:false);
            w.Interact(trail,"regional_exit","Return to the regional road",new Vector3(0,0,14));
            w.Shape("Road sign post",new Vector3(2.4f,.8f,15.2f),new Vector3(.16f,1.6f,.16f),new Color(.3f,.21f,.12f));
            w.Shape("Road sign board",new Vector3(2.4f,1.5f,15.2f),new Vector3(2,.65f,.12f),new Color(.38f,.29f,.18f),solid:false);
            w.Label("INN / REGIONAL ROAD",new Vector3(2.4f,2,15.2f),.12f);
            var farm=w.Shape("Reedwater road boundary",game.full.props.origins[0]+new Vector3(0,.05f,-15),new Vector3(3,.1f,1.3f),new Color(.48f,.39f,.26f),solid:false);
            w.Interact(farm,"regional_exit","Leave Reedwater Paddies");
            var authored=new List<Transform>();foreach(Transform t in w.root)authored.Add(t);
            foreach(Region r in System.Enum.GetValues(typeof(Region))){regions[r]=new GameObject(Title(r)+" location");regions[r].transform.SetParent(w.root);}
            // Classify top-level authored objects once. Inactive regions cannot bleed into a camera or collision query.
            foreach(Transform t in authored)
            {
                Vector3 p=t.position;
                Region region=p.x>100?Region.Reedwater:p.z>=10?Region.Woodland:Region.Inn;
                t.SetParent(regions[region].transform,true);
            }
            SetRegion(Region.Inn);
        }
        public void Open(){if(game.mode!=GameMode.Exploration)return;visible=true;selected=null;game.nearby=null;}
        public void PlaceInRegion(Transform actor,Region region){actor.SetParent(regions[region].transform,true);}
        public void Cancel(){if(travelling)return;visible=false;selected=null;}
        // A road crossing is an interaction too. Keep the actor on the authored floor,
        // and require a step back before opening again after Stay here.
        public void TickExit()
        {
            if(game.mode!=GameMode.Exploration||game.Modal||game.showInventory)return;
            Vector3 p=game.player.transform.position;
            bool inn=knowledge.current==Region.Inn;
            if(inn&&(game.opening.inLab||game.full.inRoom||p.y<-.5f||p.y>1))return;
            if(inn&&Mathf.Abs(p.x-1.5f)>1.2f)return; // Only the front doorway is a regional exit.
            Vector3 origin=knowledge.current==Region.Reedwater?game.full.props.origins[0]:Vector3.zero;
            float edge=inn?-5.65f:knowledge.current==Region.Woodland?14.2f:origin.z-14.5f;
            if(p.z>edge+1){exitLatched=false;return;}
            if(p.z>edge)return;
            game.player.Place(new Vector3(p.x,p.y,edge+.05f));
            if(exitLatched)return;
            exitLatched=true;game.Interact("regional_exit");
        }
        public bool Commit()
        {
            if(travelling||!selected.HasValue||!knowledge.CanTravel(selected.Value))return false;
            StartCoroutine(Journey(selected.Value));return true;
        }
        IEnumerator Journey(Region destination)
        {
            travelling=true;progress=0;
            while(progress<1){progress+=Time.deltaTime/1.4f;yield return null;}
            SetRegion(destination);
            Vector3 arrival=destination==Region.Inn?InnLayout.Arrival:destination==Region.Woodland?new Vector3(0,.1f,16):game.full.props.origins[0]+new Vector3(0,.1f,-13);
            game.player.Place(arrival);game.opening.inLab=false;game.full.inKitchen=false;game.full.inRoom=false;
            game.coordinated.party.Arrive(destination);
            game.coordinated.AfterTravel(destination);
            visible=false;travelling=false;selected=null;game.notice=Title(destination);
        }
        void SetRegion(Region region)
        {
            exitLatched=false;
            knowledge.current=region;
            foreach(var pair in regions)pair.Value.SetActive(pair.Key==region);
            // Story characters and collected props retain their logical visibility across visits.
            game.world.innMarlow.SetActive(region==Region.Inn&&!game.opening.state.invitedDownstairs&&!game.full.progress.marlowGone);
            game.opening.props.labMarlow.SetActive(region==Region.Inn&&game.opening.state.invitedDownstairs&&!game.full.progress.marlowGone);
            game.world.wildlife.actor.gameObject.SetActive(region==Region.Woodland&&!game.world.wildlife.cleared);
            game.world.mossback.actor.gameObject.SetActive(region==Region.Woodland&&!game.world.mossback.cleared);
            game.world.HideGrid(game.world.wildlife);game.world.HideGrid(game.world.mossback);
            game.world.HideGrid(game.full.props.sites[0]);
        }
        static Vector2 Point(Region r,Rect map)=>new Vector2(map.x+map.width*(r==Region.Inn?.48f:r==Region.Woodland?.8f:.18f),map.y+map.height*(r==Region.Inn?.7f:r==Region.Woodland?.24f:.36f));
        static void Paint(Rect rect,Color color){var old=GUI.color;GUI.color=color;GUI.DrawTexture(rect,Texture2D.whiteTexture);GUI.color=old;}
        public void Draw(float width,float height)
        {
            var map=new Rect(60,80,width-120,height-170);Paint(map,new Color(.25f,.32f,.25f));
            Paint(new Rect(map.x+map.width*.65f,map.y+35,map.width*.3f,map.height*.45f),new Color(.17f,.26f,.2f));
            Paint(new Rect(map.x+40,map.y+map.height*.2f,map.width*.28f,map.height*.45f),new Color(.42f,.43f,.25f));
            Paint(new Rect(map.x+map.width*.34f,map.y,25,map.height),new Color(.25f,.44f,.51f));
            GUI.Label(new Rect(80,30,800,35),"Regional Map • You are at "+Title(knowledge.current));
            foreach(Region r in System.Enum.GetValues(typeof(Region)))if(knowledge.Knows(r))
            {
                var p=Point(r,map);GUI.enabled=!travelling&&knowledge.CanTravel(r);
                if(GUI.Button(new Rect(p.x-90,p.y-20,180,45),Title(r)+(r==knowledge.current?" • Here":"")))selected=r;
            }
            GUI.enabled=true;
            if(selected.HasValue)
            {
                Vector2 from=Point(knowledge.current,map),to=Point(selected.Value,map);
                for(int i=1;i<20;i++){Vector2 p=Vector2.Lerp(from,to,i/20f);Paint(new Rect(p.x-2,p.y-2,4,4),new Color(.78f,.7f,.47f));}
                if(travelling){Vector2 p=Vector2.Lerp(from,to,progress);Paint(new Rect(p.x-6,p.y-6,12,12),Color.white);}
                else if(GUI.Button(new Rect(width/2-160,height-75,320,40),"Travel to "+Title(selected.Value)))Commit();
            }
            if(!travelling&&GUI.Button(new Rect(60,height-75,200,40),"Stay here [Esc]"))Cancel();
        }
    }
}
