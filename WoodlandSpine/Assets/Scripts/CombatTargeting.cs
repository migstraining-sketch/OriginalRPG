using UnityEngine;
namespace WoodlandSpine
{
    public static class CombatTargeting
    {
        public static bool Pick(Camera view,HexGrid grid,Transform actor,Hex actorHex,Vector3 screen,out Hex target)
        {
            target=default;
            if(!view.pixelRect.Contains(screen))return false;
            Ray ray=view.ScreenPointToRay(screen);
            // Pick the raised model before projecting onto its ground plane.
            var renderer=actor==null?null:actor.GetComponent<Renderer>();
            if(renderer!=null&&renderer.bounds.IntersectRay(ray)){target=actorHex;return true;}
            var plane=new Plane(Vector3.up,grid.origin);
            if(!plane.Raycast(ray,out float distance))return false;
            target=grid.At(ray.GetPoint(distance));return grid.cells.Contains(target);
        }
    }
}
