using System;
using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    public enum WeaponGeometry { Adjacent, Straight, Ranged }
    [Serializable]
    public class Inventory
    {
        public List<WeaponData> weapons = new List<WeaponData>();
        public List<BodyData> bodies = new List<BodyData>();
        public WeaponData weapon;
        public BodyData body;
        public int bandages;
        public int bloodleaf, silvermoss, mooncalfMilk, experimentalPotion, healthPotions;
        public int Armor => body == null ? 0 : body.armor;
        public void Store(WeaponData value) { if (!weapons.Contains(value)) weapons.Add(value); }
        public void Store(BodyData value) { if (!bodies.Contains(value)) bodies.Add(value); }
        public void Receive(WeaponData value) { if (!weapons.Contains(value)) weapons.Add(value); weapon = value; }
    }
    public enum GameMode { Exploration, Dialogue, Combat, Defeated, Brewing, Cooking }
}

