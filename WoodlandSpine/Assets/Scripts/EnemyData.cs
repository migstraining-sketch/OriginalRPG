using UnityEngine;
namespace WoodlandSpine
{
    [CreateAssetMenu(menuName="Woodland/Enemy")]
    public class EnemyData : ScriptableObject
    {
        public string title;
        public int hp=10,armor,movement=3,damage=4,chargeDamage=10;
        public bool mossback,pounce;
        public int pounceRange=3;
    }
}

