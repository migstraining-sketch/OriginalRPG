using UnityEngine;
namespace WoodlandSpine
{
    [CreateAssetMenu(menuName="Woodland/Weapon")]
    public class WeaponData : ScriptableObject
    {
        public string title;
        public int damage=6,minRange=1,maxRange=1;
        public WeaponGeometry geometry;
        public int signatureDamage=4;
        public string SignatureName => geometry==WeaponGeometry.Adjacent?"Lunge":geometry==WeaponGeometry.Straight?"Drive":"Quick Shot";
        public string SignatureHint => geometry==WeaponGeometry.Adjacent?"Range 2 straight; step 1 toward target. Open: free step; mud: 2 Movement.":geometry==WeaponGeometry.Straight?"Range 1–2 straight; push 1 away if clear.":"Range 1 only; clear sight required. No reposition.";
        public string Description=>$"{title} | {damage} damage | range {minRange}–{maxRange}"+(geometry==WeaponGeometry.Straight?" straight":geometry==WeaponGeometry.Ranged?" • line of sight":"");
    }
}


