using System;
using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    public enum Phase { Player, Enemy, Won, Lost, Fled }
    public sealed class CombatModel
    {
        public HexGrid grid;
        public SliceData rules;
        public Inventory inventory;
        public EnemyData enemy;
        public Hex playerCell, enemyCell;
        public int playerHP, enemyHP, movement;
        public bool primary, defending, preparing, staggered, pouncing;
        public Hex pounceTarget,chargeOrigin;
        public Phase phase;
        public Hex chargeDirection;
        public readonly List<Hex> lane = new List<Hex>();
        public string log = "Your turn. Move, act, then move again if you have movement left.";
        public string Intent => staggered ? "Staggered — recovering next phase" : pouncing ? "Preparing to Pounce — marked landing hex" : preparing ? "Preparing to Charge — marked lane" : enemy.mossback ? "Approach / Slam • 6 damage" : "Approach / Swipe • 4 damage";
        public static int Damage(int attack, int armor, bool defend = false) => Math.Max(1,defend ? Mathf.CeilToInt((attack-armor)*.5f) : attack-armor);
        public CombatModel(HexGrid grid, SliceData rules, Inventory inventory, EnemyData enemy, Hex player, Hex foe, int hp)
        {
            this.grid=grid; this.rules=rules; this.inventory=inventory; this.enemy=enemy;
            playerCell=player; enemyCell=foe; playerHP=hp; enemyHP=enemy.hp;
            BeginPlayer();
            if(enemy.mossback) PrepareCharge();else if(enemy.pounce)PreparePounce();
        }
        public void BeginPlayer() { phase=Phase.Player; movement=rules.movement; primary=true; defending=false; }
        public bool Move(Hex target)
        {
            if(phase!=Phase.Player) return false;
            var reach=grid.Reach(playerCell,movement,enemyCell,out _);
            if(!reach.TryGetValue(target,out int cost)||cost==0) return false;
            playerCell=target; movement-=cost; return true;
        }
        public bool Attack()
        {
            if(!CanTarget(enemyCell)) return false;
            primary=false; int damage=Damage(inventory.weapon.damage,enemy.armor); enemyHP=Math.Max(0,enemyHP-damage);
            log=$"{inventory.weapon.title} connects for {damage}.";
            if(enemyHP==0) {phase=Phase.Won;log=$"{enemy.title} defeated.";}
            return true;
        }
        public bool CanTarget(Hex target,bool signature=false)
        {
            if(phase!=Phase.Player||!primary||inventory.weapon==null)return false;
            if(!signature)return grid.CanAttack(inventory.weapon,playerCell,target);
            if(!grid.Walkable(playerCell)||!grid.Walkable(target)||!grid.LineOfSight(playerCell,target))return false;
            int distance=playerCell.Distance(target);
            switch(inventory.weapon.geometry)
            {
                case WeaponGeometry.Ranged:return distance==1;
                case WeaponGeometry.Straight:return distance>=1&&distance<=2&&playerCell.StraightTo(target);
                default:
                    if(distance!=2||!playerCell.StraightTo(target))return false;
                    Hex delta=target-playerCell;Hex step=playerCell+new Hex(delta.q/2,delta.r/2);
                    return grid.Walkable(step)&&!step.Equals(enemyCell)&&movement>=LungeCost(step);
            }
        }
        // Open-ground step is supplied by the technique. Mud still costs its full 2 Movement.
        int LungeCost(Hex step)=>grid.difficult.Contains(step)?grid.Cost(step):0;
        public bool Signature()
        {
            if(!CanTarget(enemyCell,true))return false;
            var weapon=inventory.weapon;Hex delta=enemyCell-playerCell;int distance=playerCell.Distance(enemyCell);
            Hex direction=new Hex(delta.q/distance,delta.r/distance);
            if(weapon.geometry==WeaponGeometry.Adjacent){Hex step=playerCell+direction;movement-=LungeCost(step);playerCell=step;}
            primary=false;int damage=Damage(weapon.signatureDamage,enemy.armor);enemyHP=Math.Max(0,enemyHP-damage);
            log=$"{weapon.SignatureName} connects for {damage}.";
            if(weapon.geometry==WeaponGeometry.Straight&&enemyHP>0)
            {
                Hex push=enemyCell+direction;
                if(grid.Walkable(push)&&!push.Equals(playerCell)){enemyCell=push;log+=" Pushed one hex.";}
                else log+=" No room to push.";
            }
            if(enemyHP==0)phase=Phase.Won;
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
            if(!playerCell.Equals(pounceTarget)){enemyCell=pounceTarget;log="The creature pounces onto the marked hex. Empty ground.";return;}
            // End adjacent on a hit; two units never share the landing hex.
            Hex landing=enemyCell;float best=float.MaxValue;
            foreach(Hex direction in Hex.Directions){Hex h=pounceTarget+direction;if(grid.Walkable(h)&&grid.LineOfSight(enemyCell,h)){float score=(grid.World(h)-grid.World(enemyCell)).sqrMagnitude;if(score<best){best=score;landing=h;}}}
            enemyCell=landing;Hit(enemy.damage);
        }
        public bool Defend() { if(!Spend()) return false; defending=true; log="Braced: half damage and resist the one-hex shove until your next turn."; return true; }
        public bool Dash() { if(!Spend()) return false; movement+=rules.dash; log=$"Dash: +{rules.dash} movement."; return true; }
        public bool Item(bool potion=false)
        {
            if((potion?inventory.healthPotions:inventory.bandages)<=0||playerHP>=rules.playerHP||!Spend()) return false;
            int heal=potion?12:rules.bandageHeal;
            if(potion)inventory.healthPotions--;else inventory.bandages--;
            playerHP=Math.Min(rules.playerHP,playerHP+heal);log=$"{(potion?"Health Potion":"Bandage")}: restored up to {heal} HP.";return true;
        }
        public bool Equip(WeaponData weapon)
        {
            if(weapon==inventory.weapon||!inventory.weapons.Contains(weapon)||!Spend()) return false;
            inventory.weapon=weapon;log="Weapon switched. Primary Action spent.";return true;
        }
        bool Spend() {if(phase!=Phase.Player||!primary)return false;primary=false;return true;}
        public bool Flee()
        {
            if(playerCell.Distance(new Hex())!=grid.radius||!Spend())return false;
            phase=Phase.Fled;log="You withdrew. The encounter remains.";return true;
        }
        public bool PrepareCharge()
        {
            if(!enemyCell.StraightTo(playerCell)||enemyCell.Equals(playerCell)||!grid.LineOfSight(enemyCell,playerCell))return false;
            int distance=enemyCell.Distance(playerCell); Hex delta=playerCell-enemyCell; chargeDirection=new Hex(delta.q/distance,delta.r/distance);
            lane.Clear();lane.Add(enemyCell); Hex h=enemyCell+chargeDirection;
            while(grid.cells.Contains(h)) {lane.Add(h);if(grid.blocked.Contains(h))break;h+=chargeDirection;}
            chargeOrigin=enemyCell;preparing=true;return true;
        }
        public void EndPlayer() {if(phase==Phase.Player)phase=Phase.Enemy;}
        public void ResolveEnemy()
        {
            if(phase!=Phase.Enemy)return;
            if(staggered) {staggered=false;log="Mossback regains its footing. You gained a turn.";}
            else if(preparing) ResolveCharge();
            else if(pouncing) ResolvePounce();
            else if(enemy.pounce&&PreparePounce())log="The creature crouches. Its landing hex is locked.";
            else
            {
                // Find an affordable step on a shortest weighted route to a free adjacent cell.
                var reach=grid.Reach(enemyCell,100,playerCell,out var previous);
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
                    if(enemy.mossback&&enemyCell.Distance(playerCell)>1&&enemyCell.StraightTo(playerCell)&&grid.LineOfSight(enemyCell,playerCell))break;
                }
                if(enemy.mossback&&enemyCell.Distance(playerCell)>1&&PrepareCharge())log="Mossback lowers its head. The amber lane is locked for next enemy phase.";
                else if(enemy.pounce&&PreparePounce())log="The creature crouches. Its landing hex is locked.";
                else if(enemyCell.Distance(playerCell)==1)Hit(enemy.damage);
                else log=$"{enemy.title} approaches.";
            }
            if(playerHP<=0){phase=Phase.Lost;return;}
            BeginPlayer();
        }
        void Hit(int attack){int amount=Damage(attack,inventory.Armor,defending);playerHP=Math.Max(0,playerHP-amount);log=$"{enemy.title} hits for {amount}.";}
        void ResolveCharge()
        {
            preparing=false;bool hit=false;
            if(!enemyCell.Equals(chargeOrigin)){if(playerCell.Equals(chargeOrigin)){Hit(enemy.chargeDamage);lane.Clear();return;}enemyCell=chargeOrigin;}log="Mossback charges through the marked lane.";
            foreach(Hex h in lane)
            {
                if(grid.blocked.Contains(h)){staggered=true;log+=" It strikes the tree and staggers!";break;}
                if(h.Equals(playerCell))
                {
                    if(!hit){Hit(enemy.chargeDamage);hit=true;}
                    Hex push=playerCell+chargeDirection;
                    if(!defending&&grid.Walkable(push))playerCell=push;
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



