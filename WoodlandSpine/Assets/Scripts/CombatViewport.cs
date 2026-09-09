using UnityEngine;
namespace WoodlandSpine
{
    public static class CombatViewport
    {
        public static Rect Pixels(float width,float height)
        {
            float scale=Mathf.Min(width/1280f,height/800f);
            // The editor Game view can be extremely wide and short. Do not let
            // its aspect ratio reveal disconnected prototype sites beside combat.
            float viewHeight=height-321*scale;
            float viewWidth=Mathf.Min(width-32*scale,viewHeight*1.8f);
            return new Rect((width-viewWidth)*.5f,217*scale,viewWidth,viewHeight);
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

