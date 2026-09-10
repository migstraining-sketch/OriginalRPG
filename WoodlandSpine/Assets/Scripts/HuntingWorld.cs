using UnityEngine;
namespace WoodlandSpine
{
    public sealed class HuntingWorld
    {
        public EncounterSite[] sites=new EncounterSite[3];
        public Vector3[] origins={new Vector3(200,0,0),new Vector3(400,0,0),new Vector3(600,0,0)};
        public Transform wheel, sylvie;
        public GameObject kitchenDoor, roofGap, channelBlock;
        public GameObject[] damage=new GameObject[3];
        WorldBuilder w;
        Color timber=new Color(.38f,.26f,.16f), grass=new Color(.26f,.35f,.2f);
        public HuntingWorld(WorldBuilder world,HuntDefinition[] definitions)
        {
            w=world;
            Vector3 p=origins[0];
            w.Shape("Reedwater Paddies",p+Vector3.down*.25f,new Vector3(36,.5f,42),grass);
            w.Shape("West hedgerow",p+new Vector3(-18,1,0),new Vector3(1,2,42),grass);
            w.Shape("East hedgerow",p+new Vector3(18,1,0),new Vector3(1,2,42),grass);
            w.Shape("North hedgerow",p+new Vector3(0,1,21),new Vector3(36,2,1),grass);
            sites[0]=w.MakeSite(p+new Vector3(3,0,8),false);
            sites[0].actor.name="Reedback";
            sites[0].actor.GetComponentInChildren<TextMesh>().text="REEDBACK";
            var client=w.Shape("Toma Reed",p+new Vector3(-6,.9f,-10),new Vector3(.7f,.9f,.7f),new Color(.54f,.46f,.33f),PrimitiveType.Capsule);
            w.Interact(client,"mud_client","Speak to Toma",p+new Vector3(-6,0,-11));
            for(int n=0;n<8;n++)w.Shape("Moonrice row",p+new Vector3(-11+n*1.1f,.18f,-3),new Vector3(.3f,.35f,8),new Color(.46f,.58f,.25f),solid:false);
            damage[0]=w.Shape("Flattened rice and turned soil",p+new Vector3(-7,.06f,-4),new Vector3(5,.1f,3),new Color(.3f,.23f,.12f),solid:false);
            BuildKitchen();
        }
        void BuildKitchen()
        {
            sylvie=w.inn.sylvie;kitchenDoor=w.inn.kitchenDoor;
        }
        public void Resolve(int index,bool lethal)
        {
            damage[index].SetActive(false);
            if(lethal)sites[index].actor.gameObject.SetActive(false);
            else sites[index].actor.position=origins[index]+new Vector3(8,.65f,10);
            if(index==1)roofGap.transform.localScale=new Vector3(3,.16f,3);
            if(index==2)channelBlock.SetActive(false);
        }
    }
}
