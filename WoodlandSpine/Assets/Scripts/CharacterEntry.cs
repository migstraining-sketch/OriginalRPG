using UnityEngine;

namespace WoodlandSpine
{
    public class CharacterEntry : MonoBehaviour
    {
        public SliceData rules;
        string traveller="Traveller";
        int color;
        readonly Color[] colors={new Color(.35f,.53f,.7f),new Color(.58f,.39f,.26f),new Color(.38f,.55f,.38f)};
        void Start(){var args=System.Environment.GetCommandLineArgs();if(System.Array.IndexOf(args,"--combat-camera-smoke")>=0||System.Array.IndexOf(args,"--slice-smoke")>=0||System.Array.IndexOf(args,"--intro-smoke")>=0||System.Array.IndexOf(args,"--opening-smoke")>=0)Enter();}
        void Enter(){var game=new GameObject("Slice systems").AddComponent<SliceGame>();game.rules=rules;game.playerName=string.IsNullOrWhiteSpace(traveller)?"Traveller":traveller.Trim();game.coatColor=colors[color];Destroy(gameObject);}
        void OnGUI()
        {
            float scale=Mathf.Min(Screen.width/1280f,Screen.height/800f);GUI.matrix=Matrix4x4.Scale(new Vector3(scale,scale,1));
            GUILayout.BeginArea(new Rect(Screen.width/scale/2-240,160,480,400),GUI.skin.box);
            GUILayout.Space(20);GUILayout.Label("WOODLAND SPINE — ORIGINAL RPG PROTOTYPE",new GUIStyle(GUI.skin.label){fontSize=18,fontStyle=FontStyle.Bold});
            GUILayout.Space(20);GUILayout.Label("Your name");traveller=GUILayout.TextField(traveller,24,GUILayout.Height(32));
            GUILayout.Space(12);GUILayout.Label("Travel coat color");color=GUILayout.SelectionGrid(color,new[]{"Blue","Ochre","Green"},3,GUILayout.Height(36));
            GUILayout.Space(20);GUILayout.Label("30 HP • Padded Travel Coat, Armor 1");
            GUILayout.Space(20);if(GUILayout.Button("Enter Garrick's Inn",GUILayout.Height(42)))Enter();
            GUILayout.Label("Placeholder geometry • WASD movement • E interaction");GUILayout.EndArea();
        }
    }
}

