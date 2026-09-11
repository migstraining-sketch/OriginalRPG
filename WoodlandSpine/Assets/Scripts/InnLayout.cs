using UnityEngine;

namespace WoodlandSpine
{
    // Shared anchors belong to the imported inn, not dialogue prose or quest state.
    public static class InnLayout
    {
        public const float UpperFloor=3.65f,LabFloor=-2.62f;
        public static readonly Vector3 Arrival=new Vector3(1.5f,.15f,-4.4f);
        public static readonly Vector3 Marlow=new Vector3(-5.9f,.8f,1.15f);
        public static readonly Vector3 BasementApproach=new Vector3(-7.5f,.15f,2.65f);
        public static readonly Vector3 BasementBottom=new Vector3(-7.5f,LabFloor+.1f,7.3f);
        public static readonly Vector3 KitchenOutside=new Vector3(-3.8f,.15f,3.05f),KitchenInside=new Vector3(-3.8f,.15f,4.65f);
        public static readonly Vector3 StairsBottom=new Vector3(7.55f,.15f,-.8f),UpperLanding=new Vector3(0,UpperFloor+.15f,3.35f);
        public static readonly Vector3 ChestApproach=new Vector3(2.25f,UpperFloor+.15f,-3.25f);
        public static readonly Vector3 SampleFall=new Vector3(-5.1f,.09f,1.05f);
        public static readonly Vector3[] DownstairsRoute={BasementApproach,new Vector3(-7.5f,.15f,3.35f),BasementBottom,new Vector3(-6.1f,LabFloor+.1f,7.3f),new Vector3(-6.1f,LabFloor+.1f,5)};
        public static readonly Vector3[] UpstairsRoute={StairsBottom,new Vector3(7.55f,1.95f,3.35f),new Vector3(6.8f,2.15f,3.35f),new Vector3(3.8f,UpperFloor+.15f,3.35f),UpperLanding};
    }
}
