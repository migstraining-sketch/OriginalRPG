// Minimal math/data stand-ins for running the ORIGINAL combat source without a Unity license.
// This does not emulate rendering, physics, serialization, or the Unity lifecycle.
using System;
namespace UnityEngine
{
    public class ScriptableObject { public static T CreateInstance<T>() where T:new()=>new T(); }
    public class Shader {}
    public class CreateAssetMenuAttribute:Attribute {public string menuName;}
    public static class Resources {public static T Load<T>(string name)=>default(T);}
    public static class Debug {public static void Log(object value)=>Console.WriteLine(value);}
    public struct Vector3
    {
        public float x,y,z;public Vector3(float x,float y,float z){this.x=x;this.y=y;this.z=z;}
        public static Vector3 zero=>new Vector3();
        public float sqrMagnitude=>x*x+y*y+z*z;
        public static Vector3 operator +(Vector3 a,Vector3 b)=>new Vector3(a.x+b.x,a.y+b.y,a.z+b.z);
        public static Vector3 operator -(Vector3 a,Vector3 b)=>new Vector3(a.x-b.x,a.y-b.y,a.z-b.z);
    }
    public static class Mathf
    {
        public static int RoundToInt(float x)=>(int)Math.Round(x,MidpointRounding.ToEven);
        public static int CeilToInt(float x)=>(int)Math.Ceiling(x);
        public static float Abs(float x)=>Math.Abs(x);
        public static float Sqrt(float x)=>(float)Math.Sqrt(x);
        public static float Lerp(float a,float b,float t)=>a+(b-a)*Math.Clamp(t,0,1);
    }
}
namespace UnityEditor {public class MenuItem:Attribute {public MenuItem(string name){}}}
namespace WoodlandSpine
{
    public static class LogicHarness
    {
        public static int Main()
        {
            try
            {
                var rules=new SliceData{coat=new BodyData(),weapons=new[]{
                    new WeaponData{title="Simple Sword",damage=6,minRange=1,maxRange=1,geometry=WeaponGeometry.Adjacent},
                    new WeaponData{title="Hunting Spear",damage=6,minRange=1,maxRange=2,geometry=WeaponGeometry.Straight},
                    new WeaponData{title="Shortbow",damage=5,minRange=2,maxRange=4,geometry=WeaponGeometry.Ranged}},
                    wildlife=new EnemyData{title="Woodland creature",hp=10,armor=0,damage=4},
                    mossback=new EnemyData{title="Mossback",hp=34,armor=1,damage=6,mossback=true}};
                Editor.RuleValidation.Run(rules);
                Editor.OpeningValidation.Run();
                Editor.ReactiveIntroValidation.Run();
                foreach(WeaponData weapon in rules.weapons)
                {
                    var inventory=new Inventory{body=rules.coat,bandages=2};inventory.Receive(weapon);int hp=30;
                    foreach(bool boss in new[]{false,true})
                    {
                        var grid=EncounterLayout.Create(UnityEngine.Vector3.zero,boss);
                        var c=new CombatModel(grid,rules,inventory,boss?rules.mossback:rules.wildlife,new Hex(3,-5),new Hex(0,1),hp);
                        int turns=0;
                        while(c.phase==Phase.Player&&turns++<100)Play(c);
                        if(c.phase!=Phase.Won)throw new Exception($"{weapon.title} vs {c.enemy.title}: {c.phase}, player {c.playerHP}, enemy {c.enemyHP}, turn {turns}");
                        Console.WriteLine($"ENCOUNTER PASS: {weapon.title} vs {c.enemy.title}; {turns} turns; {c.playerHP} HP remaining");
                        hp=c.playerHP;
                        while(hp<30&&inventory.bandages>0){inventory.bandages--;hp=Math.Min(30,hp+rules.bandageHeal);}
                    }
                }
                Console.WriteLine("LOGIC_HARNESS_SUCCESS (math stand-ins; this check alone does not validate Unity runtime)");return 0;
            }
            catch(Exception e){Console.WriteLine(e);return 1;}
        }
        static void Play(CombatModel c)
        {
            var reach=c.grid.Reach(c.playerCell,c.movement,c.enemyCell,out _);Hex best=c.playerCell;float bestScore=float.NegativeInfinity;
            foreach(var pair in reach)
            {
                Hex h=pair.Key;float score=(c.grid.CanAttack(c.inventory.weapon,h,c.enemyCell)?100:0)-h.Distance(c.enemyCell)*3-pair.Value*.1f;
                if(c.preparing&&c.lane.Contains(h))score-=1000;
                if(c.inventory.weapon.geometry==WeaponGeometry.Ranged&&h.Distance(c.enemyCell)<2)score-=200;
                if(score>bestScore){bestScore=score;best=h;}
            }
            c.Move(best);if(c.playerHP<=10&&c.inventory.bandages>0)c.Item();else c.Attack();
            if(c.phase==Phase.Won)return;
            if(c.primary)c.Defend();
            if(!c.preparing)
            {
                reach=c.grid.Reach(c.playerCell,c.movement,c.enemyCell,out _);best=c.playerCell;int distance=best.Distance(c.enemyCell);
                foreach(var pair in reach)if(pair.Key.Distance(c.enemyCell)>distance){best=pair.Key;distance=best.Distance(c.enemyCell);}
                c.Move(best);
            }
            c.EndPlayer();c.ResolveEnemy();
        }
    }
}
