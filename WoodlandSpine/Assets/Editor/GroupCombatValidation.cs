using System;
using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine.Editor
{
    public static class GroupCombatValidation
    {
        static int checks;
        static void Check(bool value,string label){checks++;if(!value)throw new Exception("Group combat: "+label);}
        public static void Run(SliceData rules)
        {
            checks=0;
            foreach(var size in new[]{new[]{1,1},new[]{1,2},new[]{2,1},new[]{2,2},new[]{3,3},new[]{3,5},new[]{3,6}})
            {
                var enemies=new List<Combatant>();for(int i=0;i<size[1];i++)enemies.Add(new Combatant{id="e"+i});
                var slots=ActivationSchedule.Build(size[0],enemies);
                string actual="";foreach(var s in slots)actual+=s==null?"A":"E";
                string expected=size[0]==3&&size[1]==6?"AEEAEEAEE":size[0]==3&&size[1]==5?"AEEAEEAE":size[0]==3?"AEAEAE":size[0]==2&&size[1]==2?"AEAE":size[0]==2?"AEA":size[1]==2?"AEE":"AE";
                Check(actual==expected,"schedule "+size[0]+"v"+size[1]);
            }
            var baseEnemies=new List<Combatant>();for(int i=0;i<5;i++)baseEnemies.Add(new Combatant());
            string twoFive="";foreach(var slot in ActivationSchedule.Build(2,baseEnemies))twoFive+=slot==null?"A":"E";
            Check(twoFive=="AEEEAEE","2v5 remainder distributed first");
            var inv=new Inventory{body=rules.coat};inv.Receive(rules.weapons[0]);
            var battle=new CombatModel(new HexGrid(Vector3.zero),rules,inv,rules.wildlife,new Hex(0,-4),new Hex(0,2),30);
            var second=battle.AddEnemy(rules.wildlife,new Hex(3,0),"second");
            var ally=Ally(rules,"Ily",new Hex(-3,0));
            Check(battle.AddAlly(ally),"local ally participates");battle.CompleteSetup();
            Check(battle.choosingAlly&&!battle.Attack(),"side choice before acting");
            Check(battle.SelectAlly(ally)&&battle.Active==ally,"select Direct ally");
            Check(battle.Defend(),"Defend immediately");battle.EndPlayer();
            second.hp=0;
            Check(battle.Schedule.Count==4,"casualty does not rebucket");
            battle.ResolveEnemy();
            Check(ally.defending,"Defend survives another ally activation");
            Check(battle.Active==battle.Hero,"remaining ally gets side slot");battle.EndPlayer();
            Check(battle.round==2&&ally.defending,"dead enemy skipped; Defend survives round boundary");
            Check(battle.SelectAlly(ally)&&!ally.defending,"Defend expires at own activation");
            Check(battle.Reachable().ContainsKey(battle.Hero.cell)==false,"allies block movement");

            var cap=new CombatModel(new HexGrid(Vector3.zero),rules,inv,rules.wildlife,new Hex(),new Hex(0,3),30);
            Check(cap.AddAlly(Ally(rules,"A",new Hex(1,0)))&&cap.AddAlly(Ally(rules,"B",new Hex(-1,0))),"two companions fit");
            Check(!cap.AddAlly(Ally(rules,"local helper",new Hex(0,-1))),"no fourth ally");
            ally.hp=0;ally.RecoverAfterEncounter();Check(ally.hp==1&&ally.needsRest,"post victory/flee stable but ineligible");
            Check(!cap.AddAlly(ally),"injured unit not eligible");ally.Rest();Check(ally.hp==ally.maxHP&&!ally.needsRest,"Basic Rest clears eligibility");

            var ai=new CombatModel(new HexGrid(Vector3.zero),rules,inv,rules.wildlife,new Hex(0,-4),new Hex(0,0),30);
            var independent=Ally(rules,"Independent",new Hex(-2,0));independent.controller=CombatController.Independent;
            ai.AddAlly(independent);ai.CompleteSetup();ai.SelectAlly(independent);
            Check(ai.phase==Phase.Enemy,"Independent uses same side slot then AI execution");ai.ResolveEnemy();
            Check(independent.spent&&ai.units[1].hp<rules.wildlife.hp,"Independent legal spear attack");
            var bowInv=new Inventory{body=rules.coat};bowInv.Receive(rules.weapons[2]);
            foreach(Hex h in new HexGrid(Vector3.zero).cells)
            {
                var bow=new CombatModel(new HexGrid(Vector3.zero),rules,bowInv,rules.wildlife,new Hex(),h,30);
                Check(bow.CanTarget(h)==(bow.InvalidReason(h).Length==0),"Bow highlight/click/reason same authority "+h);
            }
            Check(CombatModel.Damage(4,1,true)==2&&CombatModel.Damage(1,4,true)==1,"deterministic mitigation floor");
            Debug.Log("GROUP_COMBAT_VALIDATION_PASSED: "+checks);
        }
        static Combatant Ally(SliceData rules,string id,Hex cell)
        {
            var inv=new Inventory{body=rules.coat};inv.Receive(rules.weapons[1]);
            return new Combatant{id=id,title=id,side=CombatSide.Allies,controller=CombatController.Direct,inventory=inv,hp=30,maxHP=30,cell=cell};
        }
    }
}
