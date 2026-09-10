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
        }        void BuildKitchen()
        {
            var previous=w.root;var kitchen=new GameObject("Rear kitchen and service room").transform;kitchen.SetParent(previous);w.root=kitchen;
            w.Shape("Kitchen floor",new Vector3(0,-.2f,6.5f),new Vector3(6,.4f,5),timber);
            w.Shape("Kitchen west wall",new Vector3(-3,1.3f,6.5f),new Vector3(.2f,2.6f,5),timber);
            w.Shape("Kitchen rear wall",new Vector3(0,1.3f,9),new Vector3(6,2.6f,.2f),timber);
            var partition=w.Shape("Kitchen public partition",new Vector3(0,1.3f,4),new Vector3(6,2.6f,.2f),timber);
            var cover=w.Shape("Kitchen ceiling",new Vector3(0,3.3f,6.5f),new Vector3(6,.15f,5),timber);
            var exit=w.Shape("Kitchen service doorway",new Vector3(2.8f,1,5),new Vector3(.2f,2,1.2f),timber);
            w.Interact(exit,"kitchen_exit","Return to common room",new Vector3(1.8f,0,5));
            sylvie=w.Shape("Sylvie",new Vector3(-1,.9f,6.8f),new Vector3(.7f,.9f,.7f),new Color(.62f,.59f,.49f),PrimitiveType.Capsule).transform;
            w.Interact(sylvie.gameObject,"sylvie","Speak to the cook",new Vector3(-1,0,5.6f));
            var stove=w.Shape("Kitchen stove",new Vector3(1,.6f,8),new Vector3(1.5f,1.2f,1.3f),new Color(.2f,.22f,.22f));w.Interact(stove,"cooking","Use the stove",new Vector3(1,0,6.8f));
            w.Shape("Preparation counter",new Vector3(-1,.6f,8),new Vector3(2,1.2f,1),timber);
            var pantry=w.Shape("Shared pantry",new Vector3(-2.5f,.8f,5),new Vector3(.7f,1.6f,1),timber);w.Interact(pantry,"pantry","Contribute to the pantry",new Vector3(-1.5f,0,5));
            foreach(Transform child in kitchen.GetComponentsInChildren<Transform>(true))child.gameObject.layer=13;
            partition.layer=0;cover.layer=0;
            w.root=previous;            BuildRooms();
        }
        void BuildRooms()
        {
            var previous=w.root;var upper=new GameObject("Inn upper floor").transform;upper.SetParent(previous);w.root=upper;
            const float floor=3.8f;
            w.Shape("Upper floor",new Vector3(0,floor-.2f,0),new Vector3(18,.4f,14),timber);
            w.Shape("Upper west wall",new Vector3(-9,floor+1.3f,0),new Vector3(.25f,2.6f,14),timber);
            w.Shape("Upper east wall",new Vector3(9,floor+1.3f,0),new Vector3(.25f,2.6f,14),timber);
            w.Shape("Upper rear wall",new Vector3(0,floor+1.3f,7),new Vector3(18,2.6f,.25f),timber);
            for(int side=-1;side<=1;side+=2)
            for(int row=0;row<2;row++)
            {
                float z=row==0?-2:3;float x=side*3;
                w.Shape("Guest room divider",new Vector3(x,floor+.65f,z+2.5f),new Vector3(4.4f,1.3f,.18f),timber);
                w.Shape("Hall wall",new Vector3(side*.9f,floor+.65f,z+1.1f),new Vector3(.18f,1.3f,2.8f),timber);
                w.Shape("Hall wall beside door",new Vector3(side*.9f,floor+.65f,z-2),new Vector3(.18f,1.3f,1),timber);
                var bed=w.Shape("Guest bed",new Vector3(side*4,floor+.35f,z),new Vector3(1.5f,.7f,2.3f),new Color(.58f,.48f,.34f));
                bool rented=side==1&&row==0;
                if(rented)
                {
                    var chest=w.Shape("Your room chest",new Vector3(2,floor+.4f,-4.1f),new Vector3(1.2f,.8f,.7f),timber);
                    w.Interact(chest,"room_chest","Open room storage",new Vector3(2,floor,-3.2f));
                    w.Interact(bed,"room_rest","Rest in your room",new Vector3(3,floor,-2));
                }
                else
                {
                    var door=w.Shape("Occupied guest room",new Vector3(side*.9f,floor+1,z-1),new Vector3(.18f,2,1.3f),timber);
                    w.Interact(door,"guest_room","Inspect guest-room door",new Vector3(0,floor,z-1));
                }
            }
            var stairs=w.Shape("Downstairs landing",new Vector3(0,floor+.03f,-5.5f),new Vector3(2.6f,.06f,2.5f),timber,solid:false);
            w.Interact(stairs,"room_exit","Return downstairs",new Vector3(0,floor,-5.7f));
            foreach(Transform child in upper.GetComponentsInChildren<Transform>(true))child.gameObject.layer=12;
            w.root=previous;
        }        public void Resolve(int index,bool lethal)
        {
            damage[index].SetActive(false);
            if(lethal)sites[index].actor.gameObject.SetActive(false);
            else sites[index].actor.position=origins[index]+new Vector3(8,.65f,10);
            if(index==1)roofGap.transform.localScale=new Vector3(3,.16f,3);
            if(index==2)channelBlock.SetActive(false);
        }
    }
}
