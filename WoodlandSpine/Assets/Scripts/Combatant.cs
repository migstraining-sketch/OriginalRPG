using System;
using System.Collections.Generic;

namespace WoodlandSpine
{
    public enum CombatSide { Allies, Enemies }
    public enum CombatController { Direct, Independent }

    // State belongs to a unit; presentation and controller ownership do not own the rules.
    public sealed class Combatant
    {
        public string id, title;
        public CombatSide side;
        public CombatController controller;
        public Inventory inventory;
        public EnemyData enemy;
        public Hex cell, pounceTarget, chargeOrigin, chargeDirection;
        public int hp, maxHP, movement;
        public bool primary, defending, preparing, staggered, pouncing, spent, departed;
        public bool needsRest;
        public readonly List<Hex> lane = new List<Hex>();
        public bool Present => !departed && hp > 0;
        public int Armor => side == CombatSide.Allies ? inventory.Armor : enemy.armor;
        public string Intent => staggered ? "Staggered — recovering" : pouncing ? "Pounce — marked landing hex" : preparing ? (enemy.mossback ? "Charge — marked lane" : "Rush — marked lane") : "Pursue / attack";

        public void Begin(int allowance)
        {
            defending = false;
            movement = allowance;
            primary = true;
        }

        public void RecoverAfterEncounter()
        {
            if (side == CombatSide.Allies && hp <= 0) { hp = 1; needsRest = true; }
        }

        public void Rest() { hp = maxHP; needsRest = false; }
    }

    // Null slots belong to the allied side. Enemy slots keep their original identities.
    // The list is frozen for a round; callers skip missing/defeated units without rebucketing.
    public static class ActivationSchedule
    {
        public static List<Combatant> Build(int allies, IList<Combatant> enemies)
        {
            var schedule = new List<Combatant>();
            if (allies <= 0) return schedule;
            int count = enemies.Count, cursor = 0;
            for (int i = 0; i < allies; i++)
            {
                schedule.Add(null);
                int bucket = count > allies ? count / allies + (i < count % allies ? 1 : 0) : (i < count ? 1 : 0);
                for (int n = 0; n < bucket; n++) schedule.Add(enemies[cursor++]);
            }
            return schedule;
        }
    }
}
