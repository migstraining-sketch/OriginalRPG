using System;
using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    public sealed partial class CombatModel
    {
        public readonly List<Combatant> units = new List<Combatant>();
        public Combatant Hero { get; private set; }
        public Combatant Active => active;
        public Combatant Target => target;
        public bool choosingAlly { get; private set; }
        public int round { get; private set; }
        // Opening encounter policy; other encounter types may allow surviving allies to continue.
        public bool loseOnHeroDefeat = true;
        public IReadOnlyList<Combatant> Schedule => schedule;
        public int ScheduleIndex => slot;
        Combatant active, target, aiAlly;
        Combatant Acting => active ?? Hero;
        List<Combatant> schedule = new List<Combatant>();
        int slot;
        public bool CanAct => phase == Phase.Player && !choosingAlly && active != null && active.Present && !active.spent;

        public Combatant AddEnemy(EnemyData data, Hex cell, string id)
        {
            if(units.FindAll(u=>u.side==CombatSide.Enemies).Count>=6)throw new InvalidOperationException("Opening encounter supports up to six enemies.");
            var unit=new Combatant{id=id,title=data.title,side=CombatSide.Enemies,controller=CombatController.Independent,enemy=data,cell=cell,hp=data.hp,maxHP=data.hp};
            units.Add(unit);return unit;
        }

        public bool AddAlly(Combatant unit)
        {
            if(unit==null||unit.needsRest||!unit.Present||units.Contains(unit)||units.FindAll(u=>u.side==CombatSide.Allies).Count>=3)return false;
            unit.side=CombatSide.Allies;units.Add(unit);return true;
        }

        public void CompleteSetup()
        {
            round=0;StartRound();
            var saved=active;
            foreach(var unit in units)if(unit.side==CombatSide.Enemies&&unit.Present)
            {
                target=unit;active=NearestAlly(unit.cell);
                if(unit.enemy.mossback)PrepareCharge();else if(unit.enemy.pounce)PreparePounce();
            }
            active=saved;target=units.Find(u=>u.side==CombatSide.Enemies&&u.Present);
        }

        void StartRound()
        {
            if(CheckOutcome())return;
            round++;
            var enemies=new List<Combatant>();int allies=0;
            foreach(var unit in units)
            {
                unit.spent=false;
                if(!unit.Present)continue;
                if(unit.side==CombatSide.Allies)allies++;else enemies.Add(unit);
            }
            schedule=ActivationSchedule.Build(allies,enemies);slot=0;EnterSlot();
        }

        void EnterSlot()
        {
            choosingAlly=false;aiAlly=null;
            if(CheckOutcome())return;
            while(slot<schedule.Count)
            {
                var unit=schedule[slot];
                if(unit==null)
                {
                    var ready=ReadyAllies();
                    if(ready.Count==0){slot++;continue;}
                    active=null;phase=Phase.Player;choosingAlly=true;
                    if(ready.Count==1)SelectAlly(ready[0]);
                    return;
                }
                if(!unit.Present||unit.spent){slot++;continue;}
                target=unit;unit.Begin(unit.enemy.movement);phase=Phase.Enemy;
                return;
            }
            StartRound();
        }

        public List<Combatant> ReadyAllies()=>units.FindAll(u=>u.side==CombatSide.Allies&&u.Present&&!u.spent);

        public bool SelectAlly(Combatant unit)
        {
            if(!choosingAlly||!ReadyAllies().Contains(unit))return false;
            active=unit;choosingAlly=false;unit.Begin(rules.movement);
            if(target==null||!target.Present)target=units.Find(u=>u.side==CombatSide.Enemies&&u.Present);
            if(unit.controller==CombatController.Independent){aiAlly=unit;phase=Phase.Enemy;}
            else phase=Phase.Player;
            return true;
        }

        public bool SelectTarget(Combatant unit)
        {
            if(unit==null||unit.side!=CombatSide.Enemies||!unit.Present||!units.Contains(unit))return false;
            target=unit;return true;
        }

        void FinishActivation(Combatant unit)
        {
            unit.spent=true;slot++;EnterSlot();
        }

        public bool CheckOutcome()
        {
            if(Hero==null)return false;
            if(loseOnHeroDefeat&&!Hero.Present||!units.Exists(u=>u.side==CombatSide.Allies&&u.Present)){phase=Phase.Lost;return true;}
            if(!units.Exists(u=>u.side==CombatSide.Enemies&&u.Present)){phase=Phase.Won;return true;}
            return false;
        }

        public Combatant At(Hex cell)=>units.Find(u=>u.Present&&u.cell.Equals(cell));
        public bool OpenFor(Hex cell,Combatant unit)=>grid.Walkable(cell)&&(At(cell)==null||At(cell)==unit);
        HashSet<Hex> OccupiedExcept(Combatant unit)
        {
            var result=new HashSet<Hex>();foreach(var u in units)if(u!=unit&&u.Present)result.Add(u.cell);return result;
        }
        public Dictionary<Hex,int> Reachable()=>grid.Reach(Acting.cell,Acting.movement,OccupiedExcept(Acting),out _);

        Combatant NearestAlly(Hex from)
        {
            Combatant best=null;int distance=int.MaxValue;
            foreach(var unit in units)if(unit.side==CombatSide.Allies&&unit.Present&&from.Distance(unit.cell)<distance){best=unit;distance=from.Distance(unit.cell);}
            return best;
        }

        public bool Threatens(Hex cell)
        {
            foreach(var u in units)if(u.side==CombatSide.Enemies&&u.Present&&(u.preparing&&u.lane.Contains(cell)||u.pouncing&&u.pounceTarget.Equals(cell)))return true;
            return false;
        }

        public string InvalidReason(Hex cell,bool signature=false)
        {
            if(!CanAct)return choosingAlly?"Choose a Ready ally.":"Wait for your activation.";
            if(!primary)return "Primary Action already spent.";
            if(inventory.weapon==null)return "No weapon equipped.";
            if(!grid.Walkable(playerCell))return "Attacker hex is blocked.";
            if(!grid.Walkable(cell))return "Target hex is blocked.";
            var w=inventory.weapon;int distance=playerCell.Distance(cell);
            if(!signature&&w.geometry==WeaponGeometry.Ranged&&distance<w.minRange)return "Too close for Bow attack.";
            int min=signature?(w.geometry==WeaponGeometry.Adjacent?2:1):w.minRange;
            int max=signature?(w.geometry==WeaponGeometry.Ranged?1:2):w.maxRange;
            if(distance<min||distance>max)return "Out of range.";
            if((w.geometry==WeaponGeometry.Straight||signature&&w.geometry==WeaponGeometry.Adjacent)&&!playerCell.StraightTo(cell))return "Requires a straight hex line.";
            if(!grid.LineOfSight(playerCell,cell))return "Line of sight blocked.";
            if(signature&&w.geometry==WeaponGeometry.Adjacent)
            {
                var d=cell-playerCell;var step=playerCell+new Hex(d.q/2,d.r/2);
                if(!OpenFor(step,Acting))return "Lunge path blocked.";
                if(movement<LungeCost(step))return "Not enough Movement for difficult terrain.";
            }
            return "";
        }

        public int DamagePreview(bool signature=false)=>inventory.weapon==null?0:Damage(signature?inventory.weapon.signatureDamage:inventory.weapon.damage,target.Armor,target.defending);

        void ResolveIndependent()
        {
            var ally=aiAlly;active=ally;aiAlly=null;phase=Phase.Player;
            Combatant bestEnemy=null;Hex bestCell=ally.cell;float bestScore=float.MinValue;
            var reachable=Reachable();Hex original=ally.cell;
            foreach(var candidate in units)if(candidate.side==CombatSide.Enemies&&candidate.Present)
            foreach(var cell in reachable.Keys)
            {
                ally.cell=cell;target=candidate;
                float score=-cell.Distance(candidate.cell)*2-reachable[cell]*.1f;
                if(CanTarget(candidate.cell))score+=30;
                if(ally.inventory.weapon.geometry==WeaponGeometry.Straight&&cell.Distance(candidate.cell)==2)score+=3;
                if(Threatens(cell))score-=40;
                if(score>bestScore){bestScore=score;bestCell=cell;bestEnemy=candidate;}
            }
            ally.cell=original;
            if(bestEnemy!=null)
            {
                target=bestEnemy;Move(bestCell);
                // Drive is useful when an adjacent threat is crowding an ally; otherwise take full damage.
                bool crowding=units.Exists(u=>u.side==CombatSide.Allies&&u!=ally&&u.Present&&u.cell.Distance(target.cell)==1);
                if(crowding&&CanTarget(target.cell,true))Signature();
                else if(CanTarget(target.cell))Attack();
                else Defend();
            }
            if(!CheckOutcome())FinishActivation(ally);
        }
    }
}
