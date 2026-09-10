using System;
using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    public enum Phase { Player, Enemy, Won, Lost, Fled }
    public sealed partial class CombatModel
    {
        public HexGrid grid;
        public SliceData rules;
        public Inventory inventory => Acting.inventory;
        public EnemyData enemy => target.enemy;
        public Hex playerCell { get=>Acting.cell; set=>Acting.cell=value; }
        public Hex enemyCell { get=>target.cell; set=>target.cell=value; }
        public int playerHP { get=>Acting.hp; set=>Acting.hp=value; }
        public int enemyHP { get=>target.hp; set=>target.hp=value; }
        public int movement { get=>Acting.movement; set=>Acting.movement=value; }
        public bool primary { get=>Acting.primary; set=>Acting.primary=value; }
        public bool defending { get=>Acting.defending; set=>Acting.defending=value; }
        public bool preparing { get=>target.preparing; set=>target.preparing=value; }
        public bool staggered { get=>target.staggered; set=>target.staggered=value; }
        public bool pouncing { get=>target.pouncing; set=>target.pouncing=value; }
        public Hex pounceTarget { get=>target.pounceTarget; set=>target.pounceTarget=value; }
        public Hex chargeOrigin { get=>target.chargeOrigin; set=>target.chargeOrigin=value; }
        public Phase phase;
        public Hex chargeDirection { get=>target.chargeDirection; set=>target.chargeDirection=value; }
        public List<Hex> lane => target.lane;
        public string log = "Your turn. Move, act, then move again if you have movement left.";
        public string Intent => staggered ? "Staggered — recovering next phase" : pouncing ? "Preparing to Pounce — marked landing hex" : preparing ? "Preparing to Charge — marked lane" : enemy.mossback ? "Approach / Slam • 6 damage" : "Approach / Swipe • 4 damage";
        public static int Damage(int attack, int armor, bool defend = false) => Math.Max(1,defend ? Mathf.CeilToInt((attack-armor)*.5f) : attack-armor);
        public CombatModel(HexGrid grid, SliceData rules, Inventory inventory, EnemyData enemy, Hex player, Hex foe, int hp)
        {
            this.grid=grid; this.rules=rules;
            Hero=new Combatant{id="player",title="Player",side=CombatSide.Allies,controller=CombatController.Direct,inventory=inventory,cell=player,hp=hp,maxHP=rules.playerHP};
            units.Add(Hero);active=Hero;
            target=AddEnemy(enemy,foe,"enemy-0");
            BeginPlayer();
            if(enemy.mossback) PrepareCharge();else if(enemy.pounce)PreparePounce();
        }
        public void BeginPlayer() { StartRound(); }
        public bool Move(Hex target)
        {
            if(!CanAct) return false;
            var reach=Reachable();
            if(!reach.TryGetValue(target,out int cost)||cost==0) return false;
            playerCell=target; movement-=cost; return true;
        }
        public bool Attack()
        {
            if(!target.Present||!CanTarget(enemyCell)) return false;
            primary=false; int damage=DamagePreview(); enemyHP=Math.Max(0,enemyHP-damage);
            log=$"{inventory.weapon.title} connects for {damage}.";
            if(enemyHP==0) log=$"{enemy.title} defeated.";
            CheckOutcome();
            return true;
        }
        public bool CanTarget(Hex target,bool signature=false)
            => InvalidReason(target,signature).Length==0;
        // Open-ground step is supplied by the technique. Mud still costs its full 2 Movement.
        int LungeCost(Hex step)=>grid.difficult.Contains(step)?grid.Cost(step):0;
        public bool Signature()
        {
            if(!target.Present||!CanTarget(enemyCell,true))return false;
            var weapon=inventory.weapon;Hex delta=enemyCell-playerCell;int distance=playerCell.Distance(enemyCell);
            Hex direction=new Hex(delta.q/distance,delta.r/distance);
            if(weapon.geometry==WeaponGeometry.Adjacent){Hex step=playerCell+direction;movement-=LungeCost(step);playerCell=step;}
            int damage=DamagePreview(true);primary=false;enemyHP=Math.Max(0,enemyHP-damage);
            log=$"{weapon.SignatureName} connects for {damage}.";
            if(weapon.geometry==WeaponGeometry.Straight&&enemyHP>0)
            {
                Hex push=enemyCell+direction;
                if(OpenFor(push,target)){enemyCell=push;log+=" Pushed one hex.";}
                else log+=" No room to push.";
            }
            CheckOutcome();
            return true;
        }
        bool PreparePounce()
        {
            int distance=enemyCell.Distance(playerCell);
            if(distance<1||distance>enemy.pounceRange||!grid.LineOfSight(enemyCell,playerCell))return false;
            pouncing=true;pounceTarget=playerCell;return true;
        }
        void ResolvePounce()
        {
            pouncing=false;
            if(!grid.Walkable(pounceTarget)||!grid.LineOfSight(enemyCell,pounceTarget)){log="The committed pounce is stopped by blocking terrain.";return;}
            var struck=At(pounceTarget);
            if(struck==null){enemyCell=pounceTarget;log="The creature pounces onto the marked hex. Empty ground.";return;}
            if(struck.side==CombatSide.Enemies){log="The pounce is stopped by an occupied landing hex.";return;}
            active=struck;
            // End adjacent on a hit; two units never share the landing hex.
            Hex landing=enemyCell;float best=float.MaxValue;
            foreach(Hex direction in Hex.Directions){Hex h=pounceTarget+direction;if(OpenFor(h,target)&&grid.LineOfSight(enemyCell,h)){float score=(grid.World(h)-grid.World(enemyCell)).sqrMagnitude;if(score<best){best=score;landing=h;}}}
            enemyCell=landing;Hit(enemy.damage);
        }
        public bool Defend() { if(!Spend()) return false; defending=true; log="Braced: half damage and resist the one-hex shove until your next turn."; return true; }
        public bool Dash() { if(!Spend()) return false; movement+=rules.dash; log=$"Dash: +{rules.dash} movement."; return true; }
        public bool Item(bool potion=false)
        {
            if((potion?inventory.healthPotions:inventory.bandages)<=0||playerHP>=Acting.maxHP||!Spend()) return false;
            int heal=potion?12:rules.bandageHeal;
            if(potion)inventory.healthPotions--;else inventory.bandages--;
            playerHP=Math.Min(Acting.maxHP,playerHP+heal);log=$"{(potion?"Health Potion":"Bandage")}: restored up to {heal} HP.";return true;
        }
        public bool Equip(WeaponData weapon)
        {
            if(weapon==inventory.weapon||!inventory.weapons.Contains(weapon)||!Spend()) return false;
            inventory.weapon=weapon;log="Weapon switched. Primary Action spent.";return true;
        }
        bool Spend() {if(!CanAct||!primary)return false;primary=false;return true;}
        public bool Flee()
        {
            if(Acting!=Hero||playerCell.Distance(new Hex())!=grid.radius||!Spend())return false;
            phase=Phase.Fled;log="You withdrew. The encounter remains.";return true;
        }
        public bool PrepareCharge()
        {
            if(!enemyCell.StraightTo(playerCell)||enemyCell.Equals(playerCell)||!grid.LineOfSight(enemyCell,playerCell))return false;
            int distance=enemyCell.Distance(playerCell); Hex delta=playerCell-enemyCell; chargeDirection=new Hex(delta.q/distance,delta.r/distance);
            chargeOrigin=enemyCell;
            lane.Clear();lane.Add(enemyCell); Hex h=enemyCell+chargeDirection;
            while(grid.cells.Contains(h)) {lane.Add(h);if(grid.blocked.Contains(h)||enemy.rush&&h.Distance(chargeOrigin)>2)break;h+=chargeDirection;}
            chargeOrigin=enemyCell;preparing=true;return true;
        }
        public void EndPlayer() {if(CanAct)FinishActivation(Acting);}
        public void ResolveEnemy()
        {
            if(phase!=Phase.Enemy)return;
            if(aiAlly!=null){ResolveIndependent();return;}
            active=NearestAlly(target.cell);
            if(active==null){CheckOutcome();return;}
            if(staggered) {staggered=false;log="Mossback regains its footing. You gained a turn.";}
            else if(preparing) ResolveCharge();
            else if(pouncing) ResolvePounce();
            else if(enemy.pounce&&PreparePounce())log="The creature crouches. Its landing hex is locked.";
            else
            {
                // Find an affordable step on a shortest weighted route to a free adjacent cell.
                var reach=grid.Reach(enemyCell,100,OccupiedExcept(target),out var previous);
                Hex destination=enemyCell;int best=int.MaxValue;
                foreach(Hex d in Hex.Directions) {Hex h=playerCell+d;if(reach.TryGetValue(h,out int c)&&c<best){best=c;destination=h;}}
                var path=new List<Hex>();Hex cursor=destination;
                while(!cursor.Equals(enemyCell)&&previous.TryGetValue(cursor,out Hex parent)){path.Add(cursor);cursor=parent;}
                path.Reverse();int budget=enemy.movement;
                foreach(Hex h in path)
                {
                    int cost=grid.Cost(h);if(cost>budget)break;enemyCell=h;budget-=cost;
                    if(enemy.pounce&&enemyCell.Distance(playerCell)<=enemy.pounceRange&&grid.LineOfSight(enemyCell,playerCell))break;
                    // Stop to wind up as soon as a clear straight lane becomes available.
                    if((enemy.mossback||enemy.rush)&&enemyCell.Distance(playerCell)>1&&enemyCell.StraightTo(playerCell)&&grid.LineOfSight(enemyCell,playerCell))break;
                }
                if((enemy.mossback||enemy.rush)&&enemyCell.Distance(playerCell)>1&&PrepareCharge())log="The marked lane is committed for the next activation.";
                else if(enemy.pounce&&PreparePounce())log="The creature crouches. Its landing hex is locked.";
                else if(enemyCell.Distance(playerCell)==1)Hit(enemy.damage);
                else log=$"{enemy.title} approaches.";
            }
            var resolved=target;
            if(!CheckOutcome())FinishActivation(resolved);
        }
        void Hit(int attack){int amount=Damage(attack,inventory.Armor,defending);playerHP=Math.Max(0,playerHP-amount);log=$"{enemy.title} hits for {amount}.";}
        void ResolveCharge()
        {
            preparing=false;
            log=enemy.title+" moves through the committed lane.";
            if(!enemyCell.Equals(chargeOrigin))
            {
                // A Drive may displace the attacker, but never changes the committed lane.
                var occupant=At(chargeOrigin);
                if(occupant!=null&&occupant!=target){log="The committed approach is blocked by a combatant.";lane.Clear();return;}
                enemyCell=chargeOrigin;
            }
            foreach(Hex h in lane)
            {
                if(grid.blocked.Contains(h)){staggered=enemy.mossback;log+=" Blocking terrain stops it.";break;}
                var occupant=At(h);
                if(occupant!=null&&occupant!=target)
                {
                    if(occupant.side==CombatSide.Enemies)break;
                    active=occupant;Hit(enemy.chargeDamage);
                    Hex push=occupant.cell+chargeDirection;
                    if(!defending&&OpenFor(push,occupant))occupant.cell=push;
                    // Stop before an occupied hex; never overlap units after a shove.
                    if(h.Equals(playerCell))break;
                    enemyCell=h;break;
                }
                enemyCell=h;
            }
            lane.Clear();
        }
    }
}



