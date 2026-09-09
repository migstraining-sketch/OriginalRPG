using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    public class Interaction : MonoBehaviour
    {
        public string key, caption;
    }
    public sealed class EncounterSite
    {
        public HexGrid grid;
        public Transform actor;
        public Hex start = new Hex(0,1);
        public bool cleared;
        public readonly Dictionary<Hex,Renderer> tiles = new Dictionary<Hex,Renderer>();
    }
    public class WorldBuilder
    {
        public readonly List<Interaction> interactions = new List<Interaction>();
        public EncounterSite wildlife, mossback;
        public Transform root;
        public Shader shader;
        public GameObject innMarlow;
        public GameObject basementHatch, travelSample;
        public int sampleDropCount;
        public Renderer[] innRenderers;
        readonly Dictionary<Color,Material> materials = new Dictionary<Color,Material>();
        public Material Material(Color color)
        {
            if(materials.TryGetValue(color,out Material found))return found;
            var m=new Material(shader!=null?shader:Shader.Find("Standard"));m.color=color;materials[color]=m;return m;
        }
        public GameObject Shape(string name, Vector3 pos, Vector3 scale, Color color, PrimitiveType type=PrimitiveType.Cube, bool solid=true)
        {
            var g=GameObject.CreatePrimitive(type);g.name=name;g.transform.SetParent(root);g.transform.position=pos;g.transform.localScale=scale;
            g.GetComponent<Renderer>().sharedMaterial=Material(color);
            if(!solid)Object.Destroy(g.GetComponent<Collider>());
            return g;
        }
        public Transform Label(string title, Vector3 pos, float size=.2f)
        {
            var g=new GameObject(title+" label");g.transform.SetParent(root);g.transform.position=pos;g.transform.rotation=Quaternion.Euler(55,0,0);
            var t=g.AddComponent<TextMesh>();t.text=title;t.fontSize=40;t.characterSize=size;t.anchor=TextAnchor.MiddleCenter;t.color=new Color(.94f,.91f,.77f);
            // Inn/lab readability comes from props and contextual inspection, not floating destination labels.
            if((pos.x<15&&pos.z<10)||(pos.x>25&&pos.x<45))g.SetActive(false);
            return g.transform;
        }
        public void Interact(GameObject g,string key,string caption, Vector3? promptPosition=null)
        {
            var target=promptPosition.HasValue?new GameObject(caption):g;
            if(promptPosition.HasValue){target.transform.SetParent(root);target.transform.position=promptPosition.Value;}
            var i=target.AddComponent<Interaction>();i.key=key;i.caption=caption;interactions.Add(i);
        }
        public void Build()
        {
            root=new GameObject("Placeholder world").transform;
            Color wood=new Color(.28f,.19f,.13f), wall=new Color(.47f,.39f,.28f), green=new Color(.18f,.29f,.18f);
            Shape("Inn main floor",new Vector3(1.5f,-.2f,0),new Vector3(15,.4f,14),wood);
            Shape("Inn west strip",new Vector3(-8.5f,-.2f,0),new Vector3(1,.4f,14),wood);
            Shape("Inn floor before stairwell",new Vector3(-7,-.2f,-5),new Vector3(2,.4f,4),wood);
            Shape("Inn floor beyond stairwell",new Vector3(-7,-.2f,6.5f),new Vector3(2,.4f,1),wood);
            Shape("West wall",new Vector3(-9,1.5f,0),new Vector3(.3f,3,14),wall);
            Shape("East wall",new Vector3(9,1.5f,0),new Vector3(.3f,3,14),wall);
            Shape("South low cutaway wall",new Vector3(0,.3f,-7),new Vector3(18,.6f,.3f),wall);
            Shape("North wall west",new Vector3(-5.5f,1.5f,7),new Vector3(7,3,.3f),wall);
            Shape("North wall east",new Vector3(5.5f,1.5f,7),new Vector3(7,3,.3f),wall);
            Shape("Bar",new Vector3(-5, .65f,3),new Vector3(5,1.3f,1.2f),new Color(.38f,.24f,.12f));
            var garrick=Shape("Garrick",new Vector3(-5,1.1f,4.7f),new Vector3(1.2f,1.1f,1.2f),new Color(.58f,.34f,.18f),PrimitiveType.Capsule);
            Interact(garrick,"garrick","Speak to the innkeeper",new Vector3(-5,0,1.9f));Label("GARRICK",new Vector3(-5,3,4.7f));
            Shape("Garrick broad apron",new Vector3(-5,1.1f,4.15f),new Vector3(.8f,1,.13f),new Color(.22f,.18f,.13f),solid:false);
            var board=Shape("Contract board",new Vector3(-3,1.8f,6.7f),new Vector3(2,1.5f,.2f),wood);
            Interact(board,"board","Inspect contract board",new Vector3(-3,0,5.5f));Label("CONTRACTS",new Vector3(-3,2.9f,6.7f),.12f);
            for(int n=0;n<3;n++)Shape("Pinned work posting",new Vector3(-3.6f+n*.6f,1.8f,6.55f),new Vector3(.4f,1,.04f),new Color(.78f,.72f,.55f),solid:false);
            var stock=Shape("Weapons and coat rack",new Vector3(-1.2f,1,4.6f),new Vector3(1.7f,2,.3f),wood);
            Shape("Sword for sale",new Vector3(-1.7f,1.4f,4.35f),new Vector3(.12f,1.1f,.12f),new Color(.57f,.59f,.57f),solid:false);
            Shape("Spear for sale",new Vector3(-.8f,1.1f,4.35f),new Vector3(.08f,2.1f,.08f),wood,solid:false);
            Shape("Coat on peg",new Vector3(-.3f,1.3f,4.35f),new Vector3(.45f,.9f,.2f),new Color(.4f,.39f,.31f),solid:false);
            Interact(stock,"merchandise","Look at merchandise",new Vector3(-1.2f,0,3.3f));
            Shape("Marlow table",new Vector3(-4,.6f,-2),new Vector3(2,1.2f,1.5f),wood);
            var marlow=Shape("Marlow seated",new Vector3(-4,.8f,-3.4f),new Vector3(.7f,.8f,.7f),new Color(.31f,.53f,.5f),PrimitiveType.Capsule);
            innMarlow=marlow;
            Interact(marlow,"marlow","Speak to the diner");Label("MARLOW",new Vector3(-4,2.4f,-3.4f)).SetParent(marlow.transform,true);
            Shape("Marlow's lunch plate",new Vector3(-4.3f,1.23f,-2),new Vector3(.65f,.07f,.65f),new Color(.7f,.65f,.49f),PrimitiveType.Cylinder,false);
            Shape("Bread and lunch",new Vector3(-4.3f,1.34f,-2),new Vector3(.45f,.17f,.3f),new Color(.64f,.4f,.18f),PrimitiveType.Sphere,false);
            Shape("Marlow's drinking mug",new Vector3(-3.45f,1.38f,-2.2f),new Vector3(.25f,.32f,.25f),new Color(.51f,.42f,.3f),PrimitiveType.Cylinder,false);
            travelSample=Shape("Corked sample in transit — not an experiment",new Vector3(-4.8f,1.4f,-2.3f),new Vector3(.16f,.35f,.16f),new Color(.46f,.57f,.45f),PrimitiveType.Cylinder,false);
            var basement=Shape("Basement stair door",new Vector3(-7,1.2f,-3),new Vector3(2,2.4f,.2f),wood);
            basementHatch=basement;
            Interact(basement,"basement","Try the laboratory door",new Vector3(-7,0,-4));
            for(int n=0;n<24;n++)Shape("Basement stair "+n,new Vector3(-7,-.125f-n*.25f,-2.83f+n*(8f/24)),new Vector3(2,.25f,8f/24+.02f),wood).layer=11;
            Shape("Stairwell rail east",new Vector3(-5.9f,.5f,1.5f),new Vector3(.15f,1,9),wood);
            Shape("Stairwell rail west",new Vector3(-8.1f,.5f,1.5f),new Vector3(.15f,1,9),wood);
            Shape("Stairwell end rail",new Vector3(-7,.5f,6),new Vector3(2.2f,1,.15f),wood);
            var kitchen=Shape("Kitchen door on back wall",new Vector3(5.5f,1.2f,6.75f),new Vector3(1.6f,2.4f,.2f),wood);
            Shape("Kitchen door serving hatch",new Vector3(5.5f,1.65f,6.6f),new Vector3(.65f,.5f,.1f),new Color(.15f,.12f,.09f),solid:false);
            Interact(kitchen,"kitchen","Try kitchen door",new Vector3(5.5f,0,5.3f));Label("KITCHEN",new Vector3(5.5f,2.6f,6.7f),.14f);
            for(int n=0;n<5;n++)Shape("Upstairs step",new Vector3(7,n*.18f,-4+n*.5f),new Vector3(2,.35f,.5f),wall);
            var upstairs=Shape("Rooms door",new Vector3(7,1.7f,-1.7f),new Vector3(2,2,.25f),wood);
            Interact(upstairs,"upstairs","Ask about upstairs rooms",new Vector3(7,0,-4.7f));Label("ROOMS",new Vector3(7,3,-1.7f),.15f);
            Shape("Common table",new Vector3(3,.55f,0),new Vector3(2.2f,1.1f,1.4f),wood);
            var bench=Shape("Bench",new Vector3(3,.25f,-1.4f),new Vector3(2.2f,.5f,.45f),wood);
            Interact(bench,"seat","Sit / stand",new Vector3(3,0,-2.2f));
            Label("GARRICK'S INN",new Vector3(0,.08f,-5),.21f);
            innRenderers=root.GetComponentsInChildren<Renderer>();
            Label("WOODLAND ↑",new Vector3(0,.1f,8.5f),.18f);
            Shape("Woodland ground",new Vector3(0,-.25f,46),new Vector3(26,.5f,78),green);
            Shape("Trail",new Vector3(0,.005f,45),new Vector3(3,.012f,76),new Color(.31f,.3f,.19f),solid:false);
            // Physical perimeter keeps encounter approach within each authored grid.
            Shape("West woodland boundary",new Vector3(-13,1,52),new Vector3(1,2,64),green);
            Shape("East woodland boundary",new Vector3(13,1,52),new Vector3(1,2,64),green);
            Shape("Woodland end",new Vector3(0,1,84),new Vector3(26,2,1),green);
            Label("INN ↓",new Vector3(0,.06f,17),.2f);
            wildlife=MakeSite(new Vector3(0,0,34),false);
            mossback=MakeSite(new Vector3(0,0,55),true);
            // Existing damp-stone prop sits inside this encounter footprint.
            mossback.grid.blocked.Add(mossback.grid.At(new Vector3(-7,0,46.5f)));
            Label("WOODLAND",new Vector3(0,.05f,22),.24f);
            var light=new GameObject("Afternoon light").AddComponent<Light>();light.type=LightType.Directional;light.intensity=1.25f;light.transform.rotation=Quaternion.Euler(50,-35,0);light.shadows=LightShadows.Soft;
            RenderSettings.ambientLight=new Color(.56f,.59f,.62f);RenderSettings.fog=false;
        }
        public EncounterSite MakeSite(Vector3 origin,bool boss)
        {
            var site=new EncounterSite{grid=EncounterLayout.Create(origin,boss)};
            foreach(Hex h in site.grid.blocked)
            {
                Vector3 p=site.grid.World(h);
                Shape("Major tree — blocks movement / sight / charge",p+Vector3.up*1.4f,new Vector3(1.5f,2.8f,1.5f),new Color(.28f,.21f,.12f),PrimitiveType.Cylinder);
                Shape("Placeholder canopy",p+Vector3.up*3,new Vector3(2.5f,1.2f,2.5f),new Color(.22f,.37f,.24f),PrimitiveType.Sphere,false);
            }
            foreach(Hex h in site.grid.difficult)
            {
                Shape("Mud — costs 2",site.grid.World(h)+Vector3.up*.014f,new Vector3(1.6f,.025f,1.5f),new Color(.29f,.24f,.16f),PrimitiveType.Cube,false);
            }
            site.actor=Shape(boss?"Mossback":"Woodland wildlife",site.grid.World(site.start)+Vector3.up*(boss?.85f:.55f),boss?new Vector3(1.7f,1.5f,1.9f):new Vector3(1,.9f,1.2f),boss?new Color(.36f,.43f,.27f):new Color(.56f,.4f,.27f),PrimitiveType.Sphere,false).transform;
            Label(boss?"MOSSBACK":"WOODLAND CREATURE",site.grid.World(site.start)+Vector3.up*2.4f,.14f).SetParent(site.actor,true);
            foreach(Hex h in site.grid.cells)
            {
                var go=new GameObject("Hex "+h);go.transform.SetParent(root);go.transform.position=site.grid.World(h)+Vector3.up*.04f;
                var mesh=new Mesh();var v=new Vector3[7];var triangles=new int[18];
                for(int i=0;i<6;i++){float a=Mathf.Deg2Rad*(60*i+30);v[i+1]=new Vector3(Mathf.Cos(a),0,Mathf.Sin(a))*HexGrid.Size*.92f;triangles[i*3]=0;triangles[i*3+1]=(i+1)%6+1;triangles[i*3+2]=i+1;}
                mesh.vertices=v;mesh.triangles=triangles;mesh.RecalculateNormals();go.AddComponent<MeshFilter>().sharedMesh=mesh;
                var renderer=go.AddComponent<MeshRenderer>();renderer.sharedMaterial=Material(new Color(.24f,.36f,.33f));renderer.enabled=false;site.tiles[h]=renderer;
            }
            return site;
        }
        public void ShowGrid(EncounterSite site,CombatModel combat,bool attacking,bool moving=true)
        {
            Dictionary<Hex,int> reach=combat.grid.Reach(combat.playerCell,combat.movement,combat.enemyCell,out _);
            foreach(var pair in site.tiles)
            {
                Hex h=pair.Key;Color color=new Color(.23f,.32f,.28f);
                if(combat.grid.difficult.Contains(h))color=new Color(.48f,.36f,.19f);
                if(combat.grid.blocked.Contains(h))color=new Color(.16f,.18f,.17f);
                else if(attacking&&combat.primary&&combat.grid.CanAttack(combat.inventory.weapon,combat.playerCell,h))color=new Color(.65f,.31f,.3f);
                else if(moving&&combat.phase==Phase.Player&&reach.ContainsKey(h))color=new Color(.23f,.52f,.61f);
                if(combat.lane.Contains(h))color=new Color(.96f,.62f,.13f);
                if(h.Equals(combat.enemyCell))color=new Color(.75f,.24f,.21f);
                if(h.Equals(combat.playerCell))color=new Color(.6f,.83f,.77f);
                pair.Value.sharedMaterial=Material(color);pair.Value.enabled=true;
            }
        }
        public void HideGrid(EncounterSite site){foreach(var r in site.tiles.Values)r.enabled=false;}
        public void OpenBasement()
        {
            basementHatch.transform.rotation=Quaternion.Euler(0,90,0);
            basementHatch.GetComponent<Collider>().enabled=false;
            basementHatch.name="Open laboratory access";
        }
        public void DropSample()
        {
            if(sampleDropCount++>0)return;
            travelSample.AddComponent<SampleDrop>().world=this;
        }
    }
}
