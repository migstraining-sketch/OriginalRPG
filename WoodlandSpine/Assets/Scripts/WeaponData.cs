using UnityEngine;
namespace WoodlandSpine
{
    [CreateAssetMenu(menuName="Woodland/Weapon")]
    public class WeaponData : ScriptableObject
    {
        public string title;
        public int damage=6,minRange=1,maxRange=1;
        public WeaponGeometry geometry;
        public string Description=>$"{title} | {damage} damage | range {minRange}–{maxRange}"+(geometry==WeaponGeometry.Straight?" straight":geometry==WeaponGeometry.Ranged?" • line of sight":"");
    }
}
