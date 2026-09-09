using System;
using UnityEngine;
namespace WoodlandSpine.Editor
{
    public static class CombatRefinementValidation
    {
        static int checks;
        static void Check(bool value,string label){if(!value)throw new Exception("Refinement: "+label);checks++;}
        public static void Run(SliceData rules)
        {
            checks=0;var grid=new HexGrid(Vector3.zero);var zero=new Hex();
            CombatModel Battle(int weapon,Hex foe){var inv=new Inventory{body=rules.coat};inv.Receive(rules.weapons[weapon]);return new CombatModel(grid,rules,inv,rules.wildlife,zero,foe,30);}
            foreach(Hex h in grid.cells)
            {
                int n=h.Distance(zero);var c=Battle(2,h);
                Check(c.CanTarget(h)==(n>=2&&n<=4),"Bow all axis and off-axis cells "+h);
                Check(c.CanTarget(h,true)==(n==1),"Quick Shot exactly adjacent "+h);
            }
            Check(!grid.CanAttack(rules.weapons[2],new Hex(7,0),new Hex(4,0)),"invalid attacker rejected");
            Check(!grid.CanAttack(rules.weapons[2],new Hex(4,0),new Hex(7,0)),"invalid target rejected");
            foreach(Hex d in Hex.Directions)
            {
                var c=Battle(0,d*2);Check(c.Signature()&&c.playerCell.Equals(d)&&c.movement==3&&c.enemyHP==6&&!c.primary,"Lunge open ground");Check(!c.Signature(),"no double signature");
                grid.difficult.Add(d);c=Battle(0,d*2);c.movement=1;Check(!c.Signature()&&c.primary&&c.playerCell.Equals(zero),"Lunge cannot erase mud budget");c.movement=2;Check(c.Signature()&&c.movement==0,"Lunge pays full mud cost");grid.difficult.Clear();
                grid.blocked.Add(d);c=Battle(0,d*2);Check(!c.Signature()&&c.primary,"Lunge blocked");Check(!Battle(2,d*3).CanTarget(d*3),"Bow LOS blocked");grid.blocked.Clear();
                c=Battle(1,d*2);Check(c.Signature()&&c.enemyCell.Equals(d*3)&&c.enemyHP==6&&c.movement==3,"Drive damage push");
                grid.blocked.Add(d*3);c=Battle(1,d*2);Check(c.Signature()&&c.enemyCell.Equals(d*2)&&c.enemyHP==6,"blocked push retains damage");grid.blocked.Clear();
                c=Battle(2,d);Check(!c.Attack()&&c.Signature()&&c.enemyHP==7&&c.playerCell.Equals(zero)&&c.movement==3,"Quick Shot fixed damage no movement");
                grid.blocked.Add(d);Check(!Battle(2,d).Signature(),"Quick Shot blocked target");grid.blocked.Clear();
            }
            Check(!Battle(0,new Hex(1,1)).Signature()&&!Battle(1,new Hex(1,1)).Signature(),"bent Lunge Drive rejected");
            var p=Battle(0,new Hex(3,0));Check(p.pouncing&&p.pounceTarget.Equals(zero),"opening pounce tell");p.Move(new Hex(0,1));p.EndPlayer();p.ResolveEnemy();Check(p.enemyCell.Equals(zero)&&p.playerHP==30&&!p.pouncing,"pounce commits and misses without followup attack");
            p=Battle(0,new Hex(3,0));p.Defend();p.EndPlayer();p.ResolveEnemy();Check(p.playerHP==28&&p.enemyCell.Distance(p.playerCell)==1,"defend pounce without overlap");
            var inv2=new Inventory{body=rules.coat};inv2.Receive(rules.weapons[1]);grid.blocked.Add(new Hex(0,4));
            var boss=new CombatModel(grid,rules,inv2,rules.mossback,new Hex(0,1),zero,30);var lane=boss.lane.ToArray();Check(boss.Signature()&&boss.preparing&&boss.lane.Count==lane.Length,"Drive preserves locked charge");boss.Move(new Hex(1,0));boss.EndPlayer();boss.ResolveEnemy();Check(boss.staggered&&boss.playerHP==30,"displaced charge rejoins and hits original obstacle");grid.blocked.Clear();
            var layout=EncounterLayout.Create(Vector3.zero,true);Check(layout.blocked.Count>=2,"Mossback multiple obstacles");
            foreach(Hex obstacle in layout.blocked)
            {var g=new HexGrid(Vector3.zero);g.blocked.Add(obstacle);Hex foe=obstacle+new Hex(0,-3),player=obstacle+new Hex(0,-1);var c=new CombatModel(g,rules,inv2,rules.mossback,player,foe,30);Check(c.preparing&&c.lane.Contains(obstacle),"authored obstacle has bait lane");c.Move(player+new Hex(1,0));c.EndPlayer();c.ResolveEnemy();Check(c.staggered,"authored obstacle collision grants tempo");}
            var boundary=Battle(1,new Hex(6,0));boundary.playerCell=new Hex(4,0);Check(boundary.Signature()&&boundary.enemyCell.Equals(new Hex(6,0))&&boundary.enemyHP==6,"edge push fails harmlessly");
            var lethal=Battle(2,new Hex(1,0));lethal.enemyHP=3;Check(lethal.Signature()&&lethal.phase==Phase.Won,"signature victory phase");
            var enemyPhase=Battle(0,new Hex(2,0));enemyPhase.EndPlayer();Check(!enemyPhase.Signature()&&enemyPhase.enemyHP==10,"enemy phase rejects signature");
            CameraPicking(grid);
            Debug.Log("COMBAT_REFINEMENT_VALIDATION_PASSED: "+checks);
        }
        static void CameraPicking(HexGrid ignored)
        {
            var cameraObject=new GameObject("Validation camera");var view=cameraObject.AddComponent<Camera>();view.orthographic=true;view.orthographicSize=16;view.pixelRect=new Rect(0,0,1280,800);
            var actor=GameObject.CreatePrimitive(PrimitiveType.Sphere);actor.transform.localScale=new Vector3(1,1.2f,1);
            try
            {
                foreach(float y in new[]{0f,5f})foreach(float angle in new[]{0f,60f,135f,240f})
                {
                    var grid=new HexGrid(new Vector3(0,y,0));view.transform.position=grid.origin+Quaternion.Euler(0,angle,0)*new Vector3(0,18,-14);view.transform.LookAt(grid.origin);
                    foreach(Hex h in grid.cells)
                    {
                        actor.transform.position=grid.World(h)+Vector3.up*.7f;
                        Check(CombatTargeting.Pick(view,grid,actor.transform,h,view.WorldToScreenPoint(actor.transform.position),out Hex picked)&&picked.Equals(h),"raised model picks true hex at any camera angle");
                        Check(CombatTargeting.Pick(view,grid,null,h,view.WorldToScreenPoint(grid.World(h)),out picked)&&picked.Equals(h),"screen ground roundtrip on encounter elevation");
                    }
                    foreach(Hex d in Hex.Directions)foreach(float side in new[]{.49f,.51f})
                    {Vector3 world=Vector3.Lerp(grid.World(new Hex()),grid.World(d),side);Check(CombatTargeting.Pick(view,grid,null,new Hex(),view.WorldToScreenPoint(world),out Hex picked)&&picked.Equals(side<.5f?new Hex():d),"near-border picking agrees with grid");}
                }
            }
            finally{UnityEngine.Object.DestroyImmediate(cameraObject);UnityEngine.Object.DestroyImmediate(actor);}
        }
    }
}

