using System;
using UnityEditor;
using UnityEngine;

namespace WoodlandSpine.Editor
{
    public static class RuleValidation
    {
        static int count;
        static void Check(bool condition,string name){if(!condition)throw new Exception("FAILED: "+name);count++;}
        [MenuItem("Woodland/Validate deterministic rules")]
        public static void MenuRun()=>Run(Resources.Load<SliceData>("SliceRules"));
        public static void Run(SliceData rules)
        {
            count=0;
            Check(CombatModel.Damage(6,1)==5,"armor subtraction");Check(CombatModel.Damage(4,1,true)==2,"defend rounds up");
            Check(CombatModel.Damage(1,9)==1&&CombatModel.Damage(1,9,true)==1,"minimum damage");
            var grid=new HexGrid(Vector3.zero);var zero=new Hex();
            foreach(Hex h in grid.cells)Check(grid.At(grid.World(h)).Equals(h),"hex coordinate roundtrip "+h);
            foreach(Hex d in Hex.Directions)
            {Check(grid.CanAttack(rules.weapons[0],zero,d),"sword six directions");Check(grid.CanAttack(rules.weapons[1],zero,d*2),"spear six directions");}
            Check(!grid.CanAttack(rules.weapons[0],zero,new Hex(2,0)),"sword range cap");
            Check(!grid.CanAttack(rules.weapons[1],zero,new Hex(1,1)),"spear rejects bent range two");
            Check(!grid.CanAttack(rules.weapons[2],zero,new Hex(1,0)),"bow minimum range");Check(grid.CanAttack(rules.weapons[2],zero,new Hex(4,0)),"bow maximum range");
            grid.blocked.Add(new Hex(1,0));Check(!grid.CanAttack(rules.weapons[2],zero,new Hex(3,0)),"tree blocks line of sight");
            grid.difficult.Add(new Hex(0,1));var reach=grid.Reach(zero,3,new Hex(-1,0),out _);
            Check(reach[new Hex(0,1)]==2,"mud movement cost");Check(!reach.ContainsKey(new Hex(1,0)),"tree blocks movement");Check(!reach.ContainsKey(new Hex(-1,0)),"unit blocks movement");
            var inventory=new Inventory{body=rules.coat,bandages=2};inventory.Receive(rules.weapons[0]);inventory.weapons.Add(rules.weapons[2]);
            var c=new CombatModel(new HexGrid(Vector3.zero),rules,inventory,rules.wildlife,zero,new Hex(1,0),30);
            Check(c.Move(new Hex(0,1))&&c.movement==2,"move before action");Check(c.Attack()&&c.enemyHP==4,"first sword hit");Check(!c.Attack(),"one primary action");
            Check(c.Move(new Hex(-1,1))&&c.movement==1,"move after action");c.EndPlayer();Check(c.phase==Phase.Enemy&&!c.Move(zero),"phase blocks player commands");
            c.ResolveEnemy();Check(c.phase==Phase.Player&&c.primary&&c.movement==3,"player budget reset");
            Check(c.Attack()&&c.phase==Phase.Won,"two sword hits defeat wildlife");
            c=new CombatModel(new HexGrid(Vector3.zero),rules,inventory,rules.wildlife,zero,new Hex(1,0),30);
            Check(c.Defend()&&c.defending,"defend active");c.EndPlayer();c.ResolveEnemy();Check(c.playerHP==28&&!c.defending,"defend expiry after enemy");
            Check(c.Dash()&&c.movement==6&&!c.primary,"dash action budget");
            c.BeginPlayer();Check(c.Equip(rules.weapons[2])&&!c.primary,"combat equipment cost");
            c.BeginPlayer();Check(c.Item()&&c.playerHP==30&&inventory.bandages==1&&!c.primary,"item consumes action and inventory");
            c.BeginPlayer();Check(!c.Item()&&c.primary,"full health item does not waste action");
            grid=new HexGrid(Vector3.zero);grid.blocked.Add(new Hex(0,3));
            c=new CombatModel(grid,rules,inventory,rules.mossback,new Hex(0,1),zero,30);
            Check(c.preparing&&c.lane.Contains(new Hex(0,3)),"charge telegraphs through target to tree");
            c.Move(new Hex(1,0));c.EndPlayer();c.ResolveEnemy();Check(c.staggered&&c.playerHP==30&&c.enemyCell.Equals(new Hex(0,2)),"dodged charge hits tree");
            c.EndPlayer();c.ResolveEnemy();Check(!c.staggered&&c.playerHP==30,"stagger costs enemy phase");
            c=new CombatModel(grid,rules,inventory,rules.mossback,new Hex(0,1),zero,30);c.Defend();c.EndPlayer();c.ResolveEnemy();
            Check(c.playerHP==25&&c.playerCell.Equals(new Hex(0,1))&&!c.playerCell.Equals(c.enemyCell),"defend charge damage and brace");
            c=new CombatModel(grid,rules,inventory,rules.mossback,new Hex(0,1),zero,30);c.EndPlayer();c.ResolveEnemy();
            Check(c.playerHP==21&&c.playerCell.Equals(new Hex(0,2))&&!c.playerCell.Equals(c.enemyCell),"charge pushes one hex");
            c=new CombatModel(grid,rules,inventory,rules.wildlife,new Hex(0,-6),zero,30);Check(c.Flee()&&c.phase==Phase.Fled,"boundary flee");
            Debug.Log($"RULE_VALIDATION_PASSED: {count} assertions");
        }
    }
}
