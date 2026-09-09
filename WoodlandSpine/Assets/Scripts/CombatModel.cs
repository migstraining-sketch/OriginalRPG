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
        public bool primary, defending, preparing, staggered;
        public Phase phase;
        public Hex chargeDirection;
        public readonly List<Hex> lane = new List<Hex>();
        public string log = "Your turn. Move, act, then move again if you have movement left.";
        public string Intent => staggered ? "Staggered — recovering next phase" : preparing ? "Preparing to Charge — leave the amber lane!" : enemy.mossback ? "Approach / Slam • 6 damage" : "Approach / Swipe • 4 damage";
        public static int Damage(int attack, int armor, bool defend = false) => Math.Max(1,defend ? Mathf.CeilToInt((attack-armor)*.5f) : attack-armor);
        public CombatModel(HexGrid grid, SliceData rules, Inventory inventory, EnemyData enemy, Hex player, Hex foe, int hp)
        {
            this.grid=grid; this.rules=rules; this.inventory=inventory; this.enemy=enemy;
            playerCell=player; enemyCell=foe; playerHP=hp; enemyHP=enemy.hp;
            BeginPlayer();
            if(enemy.mossback) PrepareCharge();
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
            if(phase!=Phase.Player||!primary||!grid.CanAttack(inventory.weapon,playerCell,enemyCell)) return false;
            primary=false; int damage=Damage(inventory.weapon.damage,enemy.armor); enemyHP=Math.Max(0,enemyHP-damage);
            log=$"{inventory.weapon.title} connects for {damage}.";
            if(enemyHP==0) {phase=Phase.Won;log=$"{enemy.title} defeated.";}
            return true;
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
            lane.Clear(); Hex h=enemyCell+chargeDirection;
            while(grid.cells.Contains(h)) {lane.Add(h);if(grid.blocked.Contains(h))break;h+=chargeDirection;}
            preparing=true;return true;
        }
        public void EndPlayer() {if(phase==Phase.Player)phase=Phase.Enemy;}
        public void ResolveEnemy()
        {
            if(phase!=Phase.Enemy)return;
            if(staggered) {staggered=false;log="Mossback regains its footing. You gained a turn.";}
            else if(preparing) ResolveCharge();
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
                    // Stop to wind up as soon as a clear straight lane becomes available.
                    if(enemy.mossback&&enemyCell.Distance(playerCell)>1&&enemyCell.StraightTo(playerCell)&&grid.LineOfSight(enemyCell,playerCell))break;
                }
                if(enemy.mossback&&enemyCell.Distance(playerCell)>1&&PrepareCharge())log="Mossback lowers its head. The amber lane is locked for next enemy phase.";
                else if(enemyCell.Distance(playerCell)==1)Hit(enemy.damage);
                else log=$"{enemy.title} approaches.";
            }
            if(playerHP<=0){phase=Phase.Lost;return;}
            BeginPlayer();
        }
        void Hit(int attack){int amount=Damage(attack,inventory.Armor,defending);playerHP=Math.Max(0,playerHP-amount);log=$"{enemy.title} hits for {amount}.";}
        void ResolveCharge()
        {
            preparing=false;bool hit=false;log="Mossback charges through the marked lane.";
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
