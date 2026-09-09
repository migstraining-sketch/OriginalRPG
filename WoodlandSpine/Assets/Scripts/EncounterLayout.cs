using UnityEngine;
namespace WoodlandSpine
{
    public static class EncounterLayout
    {
        public static HexGrid Create(Vector3 origin,bool boss)
        {
            var grid=new HexGrid(origin);
            foreach(Hex h in boss?new[]{new Hex(0,-2),new Hex(-3,1),new Hex(3,-1)}:new[]{new Hex(-2,1),new Hex(3,-2)})grid.blocked.Add(h);
            foreach(Hex h in new[]{new Hex(1,-1),new Hex(2,-1),new Hex(2,0),new Hex(-2,-2),new Hex(-1,-2)})grid.difficult.Add(h);
            return grid;
        }
    }
}
