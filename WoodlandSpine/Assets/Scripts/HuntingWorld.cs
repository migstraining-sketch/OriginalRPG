using UnityEngine;
namespace WoodlandSpine
{
    public sealed class HuntingWorld
    {
        public EncounterSite[] sites=new EncounterSite[3];
        public Vector3[] origins={new Vector3(-34,0,30),new Vector3(34,0,30),new Vector3(64,0,30)};
        public Transform wheel, sylvie;
        public GameObject kitchenDoor, roofGap, channelBlock;
        public GameObject[] damage=new GameObject[3];
        WorldBuilder w;
        Color timber=new Color(.38f,.26f,.16f), grass=new Color(.26f,.35f,.2f);
        public HuntingWorld(WorldBuilder world,HuntDefinition[] definitions)
        {
            w=world;
            // These paths join the existing front-door trail, with gates cut into its perimeter.
            w.Shape("West and east farm path",new Vector3(18,-.22f,15),new Vector3(108,.4f,7),grass);
            for(int i=0;i<3;i++)
            {
                Vector3 p=origins[i];w.Shape(definitions[i].location,p+Vector3.down*.25f,new Vector3(27,.5f,33),grass);
                w.Shape("Site west boundary",p+new Vector3(-13,.6f,2),new Vector3(.3f,1.2f,28),timber);
                w.Shape("Site east boundary",p+new Vector3(13,.6f,2),new Vector3(.3f,1.2f,28),timber);
                w.Shape("Site north boundary",p+new Vector3(0,.6f,16),new Vector3(26,1.2f,.3f),timber);
                sites[i]=w.MakeSite(p,false);sites[i].actor.name=definitions[i].creature;
                var label=sites[i].actor.GetComponentInChildren<TextMesh>();if(label!=null)label.text=definitions[i].creature.ToUpperInvariant();
                var client=w.Shape(definitions[i].client,p+new Vector3(-6,.9f,-10),new Vector3(.7f,.9f,.7f),new Color(.54f,.46f,.33f),PrimitiveType.Capsule);
                w.Interact(client,"hunt_client_"+i,"Speak to "+definitions[i].client,p+new Vector3(-6,0,-11));
                for(int c=0;c<4;c++)
                {
                    Vector3 spot=p+new Vector3(-6+c*4,0,-6+(c%2)*2);
                    var clue=w.Shape("Evidence: "+definitions[i].clues[c],spot+Vector3.up*.12f,new Vector3(.8f,.2f,.7f),new Color(.54f,.5f,.34f),PrimitiveType.Cube,false);
                    w.Interact(clue,"hunt_clue_"+i+"_"+c,"Inspect signs",spot);
                }
                var trail=w.Shape("Tracks continue",p+new Vector3(5,.04f,2),new Vector3(.7f,.06f,2),timber,solid:false);
                w.Interact(trail,"hunt_trail_"+i,"Follow the signs",p+new Vector3(5,0,2));
                w.Interact(sites[i].actor.gameObject,"hunt_target_"+i,"Observe "+definitions[i].creature);
                var habitat=w.Shape("Habitat work site",p+new Vector3(5,.1f,6),new Vector3(3,.2f,2),timber,solid:false);
                w.Interact(habitat,"hunt_habitat_"+i,"Inspect the habitat",p+new Vector3(5,0,6));
                damage[i]=w.Shape("Unresolved damage",p+new Vector3(-5,.12f,5),new Vector3(4,.24f,4),new Color(.3f,.23f,.12f),solid:false);
                w.Label(definitions[i].client+" • "+(i==0?"PADDIES":i==1?"COOP":"MILL"),p+new Vector3(0,.06f,-11),.16f);
            }
            for(int n=0;n<6;n++)w.Shape("Moonrice row",origins[0]+new Vector3(-8+n*2,.18f,7),new Vector3(.3f,.35f,5),new Color(.46f,.58f,.25f),solid:false);
            w.Shape("Coop",origins[1]+new Vector3(-7,.7f,6),new Vector3(3,1.4f,3),timber,solid:false);
            roofGap=w.Shape("Open coop roof",origins[1]+new Vector3(-7,1.5f,6),new Vector3(3,.16f,1),timber,solid:false);
            for(int n=0;n<3;n++)w.Shape("Duskhen",origins[1]+new Vector3(-6+n,.25f,9),new Vector3(.4f,.5f,.6f),new Color(.55f,.4f,.31f),PrimitiveType.Sphere,false);
            w.Shape("Mill water",origins[2]+new Vector3(-5,.02f,4),new Vector3(3,.03f,16),new Color(.24f,.43f,.49f),solid:false);
            channelBlock=w.Shape("Branches blocking channel",origins[2]+new Vector3(-5,.35f,2),new Vector3(3,.7f,1),timber,solid:false);
            wheel=w.Shape("Mill wheel",origins[2]+new Vector3(-7,1.5f,5),new Vector3(3,3,.35f),timber,PrimitiveType.Cylinder,false).transform;
            wheel.rotation=Quaternion.Euler(90,0,0);
            for(int n=0;n<6;n++){var spoke=w.Shape("Wheel spoke",wheel.position,new Vector3(.15f,2.7f,.2f),new Color(.58f,.43f,.27f),solid:false);spoke.transform.rotation=Quaternion.Euler(0,0,n*30);spoke.transform.SetParent(wheel,true);}
            BuildKitchen();
            w.Label("PADDIES ←     COOP / MILL →",new Vector3(0,.06f,15),.16f);
        }
        void BuildKitchen()
        {
            // Doorway transitions are explicit like the existing basement; the room remains explorable.
            Vector3 p=new Vector3(36,0,-25);
            w.Shape("Kitchen floor",p+Vector3.down*.2f,new Vector3(14,.4f,12),timber);
            w.Shape("Kitchen west wall",p+new Vector3(-7,1,0),new Vector3(.25f,2,12),timber);
            w.Shape("Kitchen east wall",p+new Vector3(7,1,0),new Vector3(.25f,2,12),timber);
            w.Shape("Kitchen cutaway wall",p+new Vector3(0,.5f,-6),new Vector3(14,1,.25f),timber);
            w.Shape("Kitchen back wall",p+new Vector3(0,1.5f,6),new Vector3(14,3,.3f),new Color(.55f,.5f,.4f));
            var exit=w.Shape("Kitchen return door",p+new Vector3(-5,1,-5),new Vector3(1,2,.2f),timber);w.Interact(exit,"kitchen_exit","Return to common room",p+new Vector3(-5,0,-4));
            sylvie=w.Shape("Sylvie",p+new Vector3(1,.9f,2),new Vector3(.8f,.9f,.8f),new Color(.62f,.59f,.49f),PrimitiveType.Capsule).transform;
            w.Interact(sylvie.gameObject,"sylvie","Speak to the cook",p+new Vector3(1,0,.5f));
            var stove=w.Shape("Kitchen stove",p+new Vector3(4,.65f,3),new Vector3(2,1.3f,2),new Color(.2f,.22f,.22f));w.Interact(stove,"cooking","Use the stove",p+new Vector3(4,0,1.5f));
            w.Shape("Preparation counter",p+new Vector3(-1,.6f,3),new Vector3(3,1.2f,1),timber);
            var pantry=w.Shape("Shared pantry",p+new Vector3(-5,1,3),new Vector3(2,2,1),timber);w.Interact(pantry,"pantry","Contribute to the pantry",p+new Vector3(-5,0,1.5f));
            Vector3 room=new Vector3(60,0,-25);w.Shape("Rented room floor",room+Vector3.down*.2f,new Vector3(8,.4f,8),timber);
            w.Shape("Room west wall",room+new Vector3(-4,1,0),new Vector3(.25f,2,8),timber);
            w.Shape("Room east wall",room+new Vector3(4,1,0),new Vector3(.25f,2,8),timber);
            w.Shape("Room back wall",room+new Vector3(0,1,4),new Vector3(8,2,.25f),timber);
            w.Shape("Room cutaway wall",room+new Vector3(0,.5f,-4),new Vector3(8,1,.25f),timber);
            w.Shape("Bed",room+new Vector3(2,.4f,1),new Vector3(2,.8f,3),new Color(.58f,.48f,.34f));
            var door=w.Shape("Room exit",room+new Vector3(-2,1,-3),new Vector3(1,2,.2f),timber);w.Interact(door,"room_exit","Go downstairs",room+new Vector3(-2,0,-2));
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
