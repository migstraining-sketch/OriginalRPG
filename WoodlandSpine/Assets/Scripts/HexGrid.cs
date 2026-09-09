using System;
using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    [Serializable]
    public struct Hex : IEquatable<Hex>
    {
        public int q, r;
        public Hex(int q, int r) { this.q = q; this.r = r; }
        public static readonly Hex[] Directions = { new Hex(1,0), new Hex(1,-1), new Hex(0,-1), new Hex(-1,0), new Hex(-1,1), new Hex(0,1) };
        public static Hex operator +(Hex a, Hex b) => new Hex(a.q+b.q,a.r+b.r);
        public static Hex operator -(Hex a, Hex b) => new Hex(a.q-b.q,a.r-b.r);
        public static Hex operator *(Hex a, int b) => new Hex(a.q*b,a.r*b);
        public int Distance(Hex b) { Hex d = this-b; return (Math.Abs(d.q)+Math.Abs(d.r)+Math.Abs(d.q+d.r))/2; }
        public bool Equals(Hex b) => q == b.q && r == b.r;
        public override bool Equals(object b) => b is Hex h && Equals(h);
        public override int GetHashCode() => q*397 ^ r;
        public override string ToString() => $"({q},{r})";
        public bool StraightTo(Hex b) { Hex d = b-this; return d.q == 0 || d.r == 0 || d.q+d.r == 0; }
        public static Hex Round(float q, float r)
        {
            float s = -q-r; int x = Mathf.RoundToInt(q), z = Mathf.RoundToInt(r), y = Mathf.RoundToInt(s);
            float dx = Mathf.Abs(x-q), dz = Mathf.Abs(z-r), dy = Mathf.Abs(y-s);
            if (dx > dz && dx > dy) x = -y-z; else if (dz > dy) z = -x-y;
            return new Hex(x,z);
        }
    }
    public class HexGrid
    {
        public const float Size = 1.1f;
        public readonly Vector3 origin;
        public readonly int radius;
        public readonly HashSet<Hex> cells = new HashSet<Hex>(), blocked = new HashSet<Hex>(), difficult = new HashSet<Hex>();
        public HexGrid(Vector3 origin, int radius = 6)
        {
            this.origin = origin; this.radius = radius;
            for(int q=-radius;q<=radius;q++) for(int r=-radius;r<=radius;r++)
                if(new Hex(q,r).Distance(new Hex())<=radius) cells.Add(new Hex(q,r));
        }
        public Vector3 World(Hex h) => origin + new Vector3(Size*Mathf.Sqrt(3)*(h.q+h.r*.5f),0,Size*1.5f*h.r);
        public Hex At(Vector3 p) { p -= origin; return Hex.Round((Mathf.Sqrt(3)/3*p.x-p.z/3)/Size,2f/3*p.z/Size); }
        public bool Walkable(Hex h) => cells.Contains(h) && !blocked.Contains(h);
        public int Cost(Hex h) => difficult.Contains(h) ? 2 : 1;
        public Dictionary<Hex,int> Reach(Hex start, int budget, Hex occupied, out Dictionary<Hex,Hex> previous)
        {
            previous = new Dictionary<Hex,Hex>(); var costs = new Dictionary<Hex,int>{{start,0}}; var queue = new Queue<Hex>(); queue.Enqueue(start);
            while(queue.Count>0)
            {
                Hex h=queue.Dequeue();
                foreach(Hex d in Hex.Directions)
                {
                    Hex n=h+d; int cost=costs[h]+Cost(n);
                    if(!Walkable(n)||n.Equals(occupied)||cost>budget) continue;
                    if(costs.TryGetValue(n,out int old)&&old<=cost) continue;
                    costs[n]=cost; previous[n]=h; queue.Enqueue(n);
                }
            }
            return costs;
        }
        public bool LineOfSight(Hex a, Hex b)
        {
            int n=a.Distance(b);
            // Both edge ties must be clear: a shot cannot clip through a tree corner.
            for(int i=1;i<n;i++) for(int side=-1;side<=1;side+=2)
            {
                float t=(float)i/n;
                Hex h=Hex.Round(Mathf.Lerp(a.q,b.q,t)+side*.0001f,Mathf.Lerp(a.r,b.r,t)+side*.0001f);
                if(blocked.Contains(h)) return false;
            }
            return !blocked.Contains(b);
        }
        public bool CanAttack(WeaponData weapon, Hex a, Hex b)
        {
            if(weapon==null) return false;
            int n=a.Distance(b);
            return n>=weapon.minRange&&n<=weapon.maxRange&&
                (weapon.geometry!=WeaponGeometry.Straight||a.StraightTo(b))&&LineOfSight(a,b);
        }
        public Hex NearestOpen(Vector3 position, Hex occupied)
        {
            Hex best = new Hex(); float score=float.MaxValue;
            foreach(Hex h in cells) if(Walkable(h)&&!h.Equals(occupied))
            { float s=(World(h)-position).sqrMagnitude; if(s<score) {score=s;best=h;} }
            return best;
        }
    }
}
