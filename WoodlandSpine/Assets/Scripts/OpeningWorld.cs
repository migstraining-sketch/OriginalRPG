using UnityEngine;
namespace WoodlandSpine
{
    public sealed class OpeningWorld
    {
        public static readonly Vector3 LabEntry=new Vector3(-6.1f,InnLayout.LabFloor+.1f,5), InnEntry=InnLayout.BasementApproach;
        public static Vector3 LabPoint(Vector3 p)=>p+new Vector3(-36,InnLayout.LabFloor,0);
        public Transform labRoot;
        public Transform troll, mooncalf, trollHead;
        public GameObject labMarlow, food, usefulLeaf;
        public Renderer trollBody, trollFace;
        readonly WorldBuilder world;
        public OpeningWorld(WorldBuilder world){this.world=world;var previous=world.root;labRoot=new GameObject("Laboratory beneath inn").transform;labRoot.SetParent(previous);world.root=labRoot;BuildLab();world.root=previous;labRoot.position=new Vector3(-36,InnLayout.LabFloor,0);foreach(Transform t in labRoot.GetComponentsInChildren<Transform>(true))t.gameObject.layer=9;BuildGathering();}
        GameObject Box(string name,Vector3 p,Vector3 s,Color c,bool solid=true)=>world.Shape(name,p,s,c,PrimitiveType.Cube,solid);
        public void BuildLab()
        {
            Color timber=new Color(.31f,.25f,.18f),stone=new Color(.38f,.41f,.4f),pale=new Color(.67f,.68f,.53f);
            Box("Laboratory floor",new Vector3(36,-.2f,0),new Vector3(16,.4f,16),stone);
            Box("Lab west wall",new Vector3(28,1.4f,0),new Vector3(.3f,2.4f,16),stone);
            Box("Lab east wall",new Vector3(44,1.4f,0),new Vector3(.3f,2.4f,16),stone);
            Box("Lab north wall",new Vector3(36,1.4f,8),new Vector3(16,2.4f,.3f),stone);
            Box("Lab cutaway wall",new Vector3(36,.25f,-7),new Vector3(16,.5f,.3f),stone);
            var stairs=Box("Stair landing",new Vector3(29,.02f,5.5f),new Vector3(2,.04f,1),timber,false);
            world.Interact(stairs,"lab_exit","Stairs to the inn",new Vector3(29.9f,0,6.4f));
            var bench=Box("Alchemy workbench",new Vector3(33,.6f,1),new Vector3(3,1.2f,1.4f),timber);
            world.Interact(bench,"workbench","Use alchemy workbench",new Vector3(33,0,-.2f));world.Label("ALCHEMY WORKBENCH",new Vector3(33,2.1f,1),.14f);
            world.Shape("Mixing vessel",new Vector3(33,1.4f,1),new Vector3(.6f,.3f,.6f),new Color(.45f,.24f,.17f),PrimitiveType.Cylinder,false);
            for(int i=0;i<3;i++)world.Shape("Graduated measure",new Vector3(32+i*.7f,1.35f,.7f),new Vector3(.18f,.3f,.18f),new Color(.65f,.7f,.64f),PrimitiveType.Cylinder,false);
            var notes=Box("Research notes and observations",new Vector3(30,.65f,0),new Vector3(2,1.3f,1),timber);
            for(int i=0;i<3;i++)Box("Dated observation sheet",new Vector3(29.5f+i*.45f,1.32f,0),new Vector3(.35f,.02f,.7f),new Color(.8f,.78f,.64f),false);
            world.Interact(notes,"lab_notes","Read treatment notes",new Vector3(30,0,-1.3f));
            var storage=Box("Plant specimen shelves",new Vector3(42,.8f,-1),new Vector3(2,1.6f,1),timber);
            for(int i=0;i<4;i++)world.Shape("Preserved specimen",new Vector3(41.3f+i*.45f,1.9f,-1),new Vector3(.25f,.5f,.25f),new Color(.32f,.55f,.39f),PrimitiveType.Cylinder,false);
            world.Interact(storage,"lab_specimens","Inspect plant specimens",new Vector3(42,0,-2.3f));
            var pack=Box("Expedition pack and folded net",new Vector3(39,.5f,-4.5f),new Vector3(1.5f,1,1),new Color(.39f,.36f,.23f));
            Box("Walking staff",new Vector3(40.2f,1,-4.5f),new Vector3(.12f,2,.12f),timber,false);
            world.Interact(pack,"lab_equipment","Inspect expedition equipment");
            var habitats=Box("Ventilated creature habitats",new Vector3(33,1,5.5f),new Vector3(2,1.2f,1),new Color(.32f,.37f,.29f));
            for(int n=0;n<5;n++)Box("Habitat frame bar",new Vector3(32.2f+n*.4f,1.4f,4.95f),new Vector3(.05f,.8f,.05f),timber,false);
            world.Interact(habitats,"lab_habitats","Inspect small habitats",new Vector3(33,0,4.2f));
            var ale=world.Shape("Fermentation cask",new Vector3(42,1,5.7f),new Vector3(1,1,1),timber,PrimitiveType.Cylinder);
            world.Interact(ale,"lab_ale","Read fermentation notes",new Vector3(42,0,4.6f));
            Box("Marlow's narrow bed",new Vector3(42,.35f,-4.8f),new Vector3(1.6f,.7f,2.6f),new Color(.38f,.43f,.4f));
            Box("Folded travel blanket",new Vector3(42,.75f,-4.4f),new Vector3(1.5f,.1f,1.5f),new Color(.33f,.45f,.45f),false);
            Box("Creature sketch sheet",new Vector3(30,1.8f,6.7f),new Vector3(1.6f,1.2f,.04f),new Color(.77f,.74f,.6f),false);
            for(int n=0;n<4;n++)Box("Hand-drawn anatomical marks",new Vector3(29.6f+n*.25f,1.8f+(n%2)*.2f,6.66f),new Vector3(.22f,.035f,.02f),timber,false);
            Box("Baby troll resting mat",new Vector3(40,.12f,3.5f),new Vector3(3,.24f,2.2f),new Color(.45f,.36f,.27f));
            troll=world.Shape("Sick baby troll",new Vector3(40,.6f,3.5f),new Vector3(1.1f,.75f,1.4f),pale,PrimitiveType.Sphere,false).transform;
            trollBody=troll.GetComponent<Renderer>();
            trollHead=world.Shape("Troll broad face",new Vector3(40,.95f,2.8f),new Vector3(.85f,.7f,.65f),pale,PrimitiveType.Sphere,false).transform;
            trollFace=trollHead.GetComponent<Renderer>();
            for(int side=-1;side<=1;side+=2)world.Shape("Troll ear",new Vector3(40+side*.5f,1,2.8f),new Vector3(.3f,.3f,.15f),pale,PrimitiveType.Sphere,false).transform.SetParent(trollHead,true);
            world.Interact(troll.gameObject,"troll","Check the baby troll",new Vector3(40,0,1.5f));world.Label("BABY TROLL",new Vector3(40,2,3.6f),.15f);
            food=Box("Food bowl — untouched",new Vector3(39,.25f,2.2f),new Vector3(.5f,.25f,.5f),new Color(.66f,.4f,.19f),false);
            labMarlow=world.Shape("Marlow in laboratory",new Vector3(36,.9f,3),new Vector3(.7f,.9f,.7f),new Color(.31f,.53f,.5f),PrimitiveType.Capsule);
            world.Interact(labMarlow,"lab_marlow","Talk to Marlow");world.Label("MARLOW",new Vector3(36,2.5f,3),.17f).SetParent(labMarlow.transform,true);
            labMarlow.SetActive(false);world.Label("MARLOW'S LABORATORY",new Vector3(36,.06f,-5.5f),.18f);
        }
        void BuildGathering()
        {
            var stem=world.Shape("Bloodleaf living stem",new Vector3(-4,.35f,18),new Vector3(.12f,.7f,.12f),new Color(.2f,.42f,.23f),PrimitiveType.Cylinder,false);
            usefulLeaf=world.Shape("Useful red leaf tips",new Vector3(-4,.7f,18),new Vector3(1.1f,.2f,.55f),new Color(.7f,.16f,.13f),PrimitiveType.Sphere,false);
            world.Shape("Bloodleaf lower leaves",new Vector3(-4,.35f,18),new Vector3(.8f,.12f,.5f),new Color(.33f,.48f,.24f),PrimitiveType.Sphere,false);
            world.Interact(stem,"bloodleaf","Inspect red-veined plant");
            var rock=world.Shape("Damp shaded stone",new Vector3(-7,.6f,46.5f),new Vector3(2.5f,1.2f,1.8f),new Color(.3f,.36f,.36f),PrimitiveType.Sphere);
            world.Shape("Silvermoss fronds",new Vector3(-7,.55f,45.6f),new Vector3(1.8f,.25f,.5f),new Color(.65f,.73f,.64f),PrimitiveType.Sphere,false);
            world.Interact(rock,"silvermoss","Inspect pale moss by damp stone",new Vector3(-7,0,44.5f));
            Box("Shade over damp stone",new Vector3(-8,2.8f,47),new Vector3(4,.5f,3),new Color(.22f,.34f,.23f),false);
            mooncalf=world.Shape("Mooncalf",new Vector3(5,.8f,49),new Vector3(1.2f,1.25f,1.6f),new Color(.72f,.73f,.63f),PrimitiveType.Sphere,false).transform;
            world.Shape("Mooncalf curious face",new Vector3(5,1.6f,48.7f),new Vector3(.7f,.8f,.7f),new Color(.75f,.76f,.68f),PrimitiveType.Sphere,false).transform.SetParent(mooncalf,true);
            for(int side=-1;side<=1;side+=2)world.Shape("Mooncalf long ear",new Vector3(5+side*.45f,1.8f,48.7f),new Vector3(.2f,.6f,.18f),new Color(.64f,.67f,.55f),PrimitiveType.Capsule,false).transform.SetParent(mooncalf,true);
            world.Interact(mooncalf.gameObject,"mooncalf","Observe Mooncalf");world.Label("MOONCALF",new Vector3(5,2.7f,49),.15f).SetParent(mooncalf,true);
        }
        public void RecoverTroll(float amount)
        {
            Color c=Color.Lerp(new Color(.67f,.68f,.53f),new Color(.34f,.59f,.3f),amount);
            trollBody.sharedMaterial=world.Material(c);trollFace.sharedMaterial=world.Material(c);
            trollHead.position=Vector3.Lerp(LabPoint(new Vector3(40,.95f,2.8f)),LabPoint(new Vector3(39.7f,1.22f,2.6f)),amount);
            food.name="Food bowl — troll reaches for a mouthful";
        }
        public void AttendPatient()
        {
            labMarlow.transform.position=LabPoint(new Vector3(38.7f,.65f,3.3f));
            labMarlow.transform.localScale=new Vector3(.7f,.65f,.7f);
            labMarlow.transform.LookAt(LabPoint(new Vector3(40,.65f,3.5f)));
        }
    }
}
