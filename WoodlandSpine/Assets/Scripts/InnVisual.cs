using UnityEngine;

namespace WoodlandSpine
{
    public sealed class InnVisual
    {
        public readonly GameObject model;
        public Transform sylvie;
        public IllustratedInnActor garrick;
        public GameObject kitchenDoor,playerDoor;
        readonly WorldBuilder w;
        Transform upper,kitchen,cutaway,basementDoor;
        readonly System.Collections.Generic.List<AmbientPatron> patrons=new System.Collections.Generic.List<AmbientPatron>();
        bool patronsReady;
        bool lightingInitialized,wasInside;
        public InnVisual(WorldBuilder world,GameObject prefab)
        {
            w=world;if(prefab==null)throw new System.InvalidOperationException("Inn model missing. Run Woodland > Generate initial scene and data.");
            model=Object.Instantiate(prefab,w.root);model.name="Garrick's Inn — imported Blender environment";
            upper=model.transform.Find("Upper");kitchen=model.transform.Find("Kitchen");cutaway=model.transform.Find("Cutaway");
            SetLayer(upper,12);SetLayer(model.transform.Find("BasementStairs"),11);SetLayer(model.transform.Find("UpperStairs"),14);
            foreach(var r in upper.GetComponentsInChildren<Renderer>())r.shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.Off;
            // The roof and front/east walls are sectioned for the gameplay camera. Collision stays present.
            foreach(var r in cutaway.GetComponentsInChildren<Renderer>())r.enabled=false;
            basementDoor=Pivot("BasementDoor",new Vector3(-8.125f,0,3.38f));
            var doorBox=basementDoor.gameObject.AddComponent<BoxCollider>();doorBox.center=new Vector3(.625f,1.15f,0);doorBox.size=new Vector3(1.25f,2.3f,.12f);
            w.basementHatch=basementDoor.gameObject;
            var kp=Pivot("KitchenDoor",new Vector3(-4.45f,0,3.8f));kitchenDoor=kp.gameObject;
            var kb=kp.gameObject.AddComponent<BoxCollider>();kb.center=new Vector3(.65f,1.15f,0);kb.size=new Vector3(1.3f,2.3f,.12f);
            Anchor("garrick","Speak to the innkeeper",new Vector3(0,0,.65f));
            garrick=IllustratedInnActor.Install(model.transform);
            Anchor("board","Inspect contract board",new Vector3(4.12f,0,.95f));
            Anchor("merchandise","Look at merchandise",new Vector3(-4.25f,0,-1.4f));
            w.innMarlow=w.Shape("Marlow seated",InnLayout.Marlow,new Vector3(.7f,.8f,.7f),new Color(.31f,.53f,.5f),PrimitiveType.Capsule,false);
            w.innMarlow.transform.SetParent(model.transform,true);w.Interact(w.innMarlow,"marlow","Speak to the diner");
            w.travelSample=w.Shape("Corked sample in transit",new Vector3(-5.04f,1.02f,1.92f),new Vector3(.12f,.23f,.12f),new Color(.46f,.57f,.45f),PrimitiveType.Cylinder,false);
            w.travelSample.transform.SetParent(model.transform,true);
            Anchor("basement","Try the laboratory door",InnLayout.BasementApproach);
            Anchor("kitchen","Try kitchen door",InnLayout.KitchenOutside);
            Anchor("kitchen_exit","Return to common room",InnLayout.KitchenInside);
            sylvie=Anchor("sylvie","Speak to the cook",new Vector3(-3.2f,0,5.05f)).transform;
            Anchor("cooking","Use the stove",new Vector3(-4.95f,0,5.65f));
            Anchor("pantry","Contribute to the pantry",new Vector3(-1.3f,0,5.5f));
            Anchor("upstairs","Ask about upstairs rooms",InnLayout.StairsBottom);
            Anchor("room_exit","Return downstairs",InnLayout.UpperLanding);
            Anchor("room_chest","Open room storage",InnLayout.ChestApproach);
            Anchor("room_rest","Rest in your room",new Vector3(2.65f,InnLayout.UpperFloor,-4));
            Anchor("basic_rest","Rest by the hearth — free",new Vector3(-7,0,-3.7f));
            Anchor("seat","Sit / stand",new Vector3(4.35f,0,-5.35f));
            Anchor("regional_exit","Leave through the front entrance",new Vector3(1.5f,0,-6));
            Anchor("back_property","Inspect the rear service door",new Vector3(4.3f,0,5.85f));
            Vector3[] seats={new Vector3(-6.4f,0,-2.5f),new Vector3(-4.8f,0,-2.5f),new Vector3(-6.2f,0,-.83f),new Vector3(7.45f,0,-2.6f),new Vector3(2.4f,0,.76f),new Vector3(-3.9f,0,-6.2f)};
            for(int i=0;i<seats.Length;i++){var p=Pivot("Patron"+i,seats[i]);var idle=p.gameObject.AddComponent<AmbientPatron>();idle.seat=i;patrons.Add(idle);}
            for(int side=-1;side<=1;side+=2)for(int row=0;row<2;row++)
            {
                float z=(row==0?-3.8f:.65f)-.65f;
                var door=w.Shape("Guest-room door",new Vector3(side*.85f,InnLayout.UpperFloor+1,z),new Vector3(.12f,2,1),new Color(.18f,.10f,.04f));door.transform.SetParent(upper,true);door.layer=12;
                if(side==1&&row==0)playerDoor=door;
                else Anchor("guest_room","Inspect guest-room door",new Vector3(0,InnLayout.UpperFloor,z));
            }
            AddLight("Hearth firelight",new Vector3(-7.5f,1.1f,-1.6f),new Color(1,.44f,.13f),2.2f,7);
            foreach(var p in new[]{new Vector3(-2.8f,2.65f,2.2f),new Vector3(3.1f,2.65f,2.2f),new Vector3(-5,2.65f,-3.8f),new Vector3(5.3f,2.65f,-3.8f),new Vector3(-3,2.8f,5.5f)})
                AddLight("Warm inn lantern",p,new Color(1,.66f,.30f),1.15f,5);
            AddLight("Upper hall lamplight",new Vector3(0,6,-1),new Color(1,.72f,.42f),1.3f,10);
            w.innRenderers=model.GetComponentsInChildren<Renderer>();
        }
        static void SetLayer(Transform t,int layer){foreach(var x in t.GetComponentsInChildren<Transform>())x.gameObject.layer=layer;}
        Transform Pivot(string name,Vector3 at)
        {var mesh=model.transform.Find(name);var p=new GameObject(name+" hinge").transform;p.SetParent(model.transform);p.position=at;mesh.SetParent(p,true);return p;}
        GameObject Anchor(string key,string caption,Vector3 p)
        {var go=new GameObject(caption);go.transform.SetParent(model.transform);go.transform.position=p;w.Interact(go,key,caption);return go;}
        void AddLight(string name,Vector3 at,Color color,float intensity,float range)
        {var l=new GameObject(name).AddComponent<Light>();l.transform.SetParent(model.transform);l.transform.position=at;l.color=color;l.type=LightType.Point;l.intensity=intensity;l.range=range;l.shadows=LightShadows.None;}
        public void OpenBasement(){basementDoor.localRotation=Quaternion.Euler(0,-95,0);basementDoor.GetComponent<Collider>().enabled=false;}
        public void Tick(SliceGame game)
        {
            if(!patronsReady){foreach(var patron in patrons)patron.game=game;patronsReady=true;}
            bool inside=game.coordinated.travel.knowledge.current==Region.Inn;
            if(!lightingInitialized||inside!=wasInside)
            {
                lightingInitialized=true;wasInside=inside;
                w.daylight.intensity=inside?.72f:1.25f;w.daylight.color=inside?new Color(1,.86f,.69f):Color.white;
                RenderSettings.ambientMode=UnityEngine.Rendering.AmbientMode.Flat;
                RenderSettings.ambientLight=inside?new Color(.40f,.35f,.28f):new Color(.56f,.59f,.62f);
                QualitySettings.pixelLightCount=8;
            }
            if(!inside)return;
            bool allowed=game.full.progress.kitchenAccess;
            kitchenDoor.transform.localRotation=Quaternion.Euler(0,allowed?-95:0,0);kitchenDoor.GetComponent<Collider>().enabled=!allowed;
            playerDoor.SetActive(!game.full.progress.roomRented);
            Vector3 p=game.player.transform.position;
            game.full.inRoom=p.y>InnLayout.UpperFloor-.45f;
            game.full.inKitchen=p.y>-.4f&&p.y<1&&p.x>-6&&p.x<-.2f&&p.z>3.85f;
        }
        public bool CanInteract(SliceGame game,string key)
        {
            if(game.coordinated.travel.knowledge.current!=Region.Inn)return true;
            if(key=="sylvie"||key=="pantry"||key=="cooking"||key=="kitchen_exit")return game.full.inKitchen;
            if(key=="kitchen")return !game.full.inKitchen&&!game.full.inRoom&&!game.opening.inLab;
            if(key=="room_chest"||key=="room_rest"||key=="room_exit"||key=="guest_room")return game.full.inRoom;
            return true;
        }
    }
}
