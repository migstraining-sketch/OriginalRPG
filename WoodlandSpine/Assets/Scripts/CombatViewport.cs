using UnityEngine;
namespace WoodlandSpine
{
    public static class CombatViewport
    {
        public static Rect Pixels(float width,float height)
        {
            float scale=Mathf.Min(width/1280f,height/800f);
            return new Rect(16*scale,217*scale,width-32*scale,height-381*scale);
        }
        public static float Size(HexGrid grid,Quaternion rotation,float aspect)
        {
            float horizontal=0,vertical=0;Quaternion inverse=Quaternion.Inverse(rotation);
            foreach(Hex h in grid.cells)foreach(float elevation in new[]{0f,3f})
            {
                Vector3 p=inverse*(grid.World(h)-grid.origin+Vector3.up*elevation);
                horizontal=Mathf.Max(horizontal,Mathf.Abs(p.x)+1.3f);vertical=Mathf.Max(vertical,Mathf.Abs(p.y)+1.3f);
            }
            return Mathf.Max(vertical,horizontal/Mathf.Max(.1f,aspect));
        }
    }
}
