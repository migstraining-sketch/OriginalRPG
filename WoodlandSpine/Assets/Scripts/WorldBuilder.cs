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
        public Light daylight;
        public GameObject innPrefab;
        public InnVisual inn;
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
            inn=new InnVisual(this,innPrefab);
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
            var light=new GameObject("Afternoon light").AddComponent<Light>();daylight=light;light.type=LightType.Directional;light.intensity=1.25f;light.transform.rotation=Quaternion.Euler(50,-35,0);light.shadows=LightShadows.Soft;
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
        public void ShowGrid(EncounterSite site,CombatModel combat,bool attacking,bool moving=true,bool signature=false)
        {
            Dictionary<Hex,int> reach=combat.Reachable();
            foreach(var pair in site.tiles)
            {
                Hex h=pair.Key;Color color=new Color(.23f,.32f,.28f);
                if(combat.grid.difficult.Contains(h))color=new Color(.48f,.36f,.19f);
                if(combat.grid.blocked.Contains(h))color=new Color(.16f,.18f,.17f);
                else if((attacking||signature)&&combat.CanTarget(h,signature))color=new Color(.65f,.31f,.3f);
                else if(moving&&combat.CanAct&&reach.ContainsKey(h))color=new Color(.23f,.52f,.61f);
                var occupant=combat.At(h);
                if(occupant!=null)color=occupant.side==CombatSide.Allies?new Color(.6f,.83f,.77f):new Color(.55f,.43f,.36f);
                if(combat.Threatens(h))color=new Color(.96f,.62f,.13f);
                pair.Value.sharedMaterial=Material(color);pair.Value.enabled=true;
            }
        }
        public void HideGrid(EncounterSite site){foreach(var r in site.tiles.Values)r.enabled=false;}
        public void OpenBasement()
        {
            inn.OpenBasement();
        }
        public void DropSample()
        {
            if(sampleDropCount++>0)return;
            travelSample.AddComponent<SampleDrop>().world=this;
        }
    }
}

