using UnityEngine;
namespace WoodlandSpine
{
    [CreateAssetMenu(menuName="Woodland/Slice rules")]
    public class SliceData : ScriptableObject
    {
        public int playerHP=30,movement=3,dash=3,bandageHeal=8,startingBandages=2;
        public WeaponData[] weapons;
        public BodyData coat;
        public EnemyData wildlife,mossback;
        public EnemyData reedback,juvenileMooncalf,nursingMooncow,protectiveAdult;
        public int ilyHP=20;
        public Shader placeholderShader;
        public GameObject innModel;
    }
}
