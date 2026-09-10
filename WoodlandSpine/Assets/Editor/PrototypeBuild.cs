using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace WoodlandSpine.Editor
{
    public static class PrototypeBuild
    {
        [MenuItem("Woodland/Generate initial scene and data")]
        public static void Setup()
        {
            Directory.CreateDirectory("Assets/Resources");Directory.CreateDirectory("Assets/Scenes");
            var rules=AssetDatabase.LoadAssetAtPath<SliceData>("Assets/Resources/SliceRules.asset");
            if(rules==null)
            {
                rules=ScriptableObject.CreateInstance<SliceData>();
                var sword=Weapon("Simple Sword",6,1,1,WeaponGeometry.Adjacent);
                var spear=Weapon("Hunting Spear",6,1,2,WeaponGeometry.Straight);
                var bow=Weapon("Shortbow",5,2,4,WeaponGeometry.Ranged);
                rules.weapons=new[]{sword,spear,bow};rules.coat=ScriptableObject.CreateInstance<BodyData>();AssetDatabase.CreateAsset(rules.coat,"Assets/Resources/TravelCoat.asset");
                rules.wildlife=Enemy("Woodland creature",10,0,4,false);rules.mossback=Enemy("Mossback",34,1,6,true);
                AssetDatabase.CreateAsset(rules,"Assets/Resources/SliceRules.asset");
            }
            if(rules.reedback==null){rules.reedback=Enemy("Reedback",18,0,4,false);rules.reedback.pounce=false;rules.reedback.rush=true;rules.reedback.chargeDamage=6;EditorUtility.SetDirty(rules.reedback);}
            if(rules.juvenileMooncalf==null){rules.juvenileMooncalf=Enemy("Mooncalf",8,0,2,false);rules.juvenileMooncalf.pounce=false;EditorUtility.SetDirty(rules.juvenileMooncalf);}
            if(rules.nursingMooncow==null){rules.nursingMooncow=Enemy("Nursing Mooncow",12,0,4,false);rules.nursingMooncow.pounce=false;EditorUtility.SetDirty(rules.nursingMooncow);}
            if(rules.protectiveAdult==null){rules.protectiveAdult=Enemy("Protective adult",12,0,4,false);rules.protectiveAdult.pounce=false;EditorUtility.SetDirty(rules.protectiveAdult);}
            if(!File.Exists("Assets/Scenes/Opening.unity"))
            {
                var scene=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);
                new GameObject("Character entry").AddComponent<CharacterEntry>().rules=rules;
                EditorSceneManager.SaveScene(scene,"Assets/Scenes/Opening.unity");
            }
            rules.innModel=AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Art/Inn/GarricksInn.innmodel");
            if(rules.innModel==null)throw new Exception("Imported Blender inn is missing");
            rules.placeholderShader=Shader.Find("Woodland/Placeholder");EditorUtility.SetDirty(rules);
            EditorBuildSettings.scenes=new[]{new EditorBuildSettingsScene("Assets/Scenes/Opening.unity",true)};
            PlayerSettings.companyName="Original RPG Prototype";PlayerSettings.productName="Woodland Spine";
            PlayerSettings.defaultScreenWidth=1280;PlayerSettings.defaultScreenHeight=800;PlayerSettings.fullScreenMode=FullScreenMode.Windowed;PlayerSettings.runInBackground=true;
            PlayerSettings.SetScriptingBackend(UnityEditor.Build.NamedBuildTarget.Standalone,ScriptingImplementation.Mono2x);
            AssetDatabase.SaveAssets();AssetDatabase.Refresh();
            RuleValidation.Run(rules);
            OpeningValidation.Run();
            ReactiveIntroValidation.Run();
            FullOpeningValidation.Run();
            InnDialogueValidation.Run();
            CombatViewportValidation.Run();CombatRefinementValidation.Run(rules);
            GroupCombatValidation.Run(rules);
            CoordinatedValidation.Run(rules);
            var report=BuildPipeline.BuildPlayer(new BuildPlayerOptions{scenes=new[]{"Assets/Scenes/Opening.unity"},locationPathName="Builds/Windows/WoodlandSpine.exe",target=BuildTarget.StandaloneWindows64,options=BuildOptions.Development});
            if(report.summary.result!=UnityEditor.Build.Reporting.BuildResult.Succeeded)throw new Exception("Player build failed: "+report.summary.result);
            Debug.Log("SLICE_BUILD_SUCCESS");
        }
        static WeaponData Weapon(string title,int damage,int min,int max,WeaponGeometry geometry)
        {var w=ScriptableObject.CreateInstance<WeaponData>();w.title=title;w.damage=damage;w.minRange=min;w.maxRange=max;w.geometry=geometry;w.signatureDamage=geometry==WeaponGeometry.Ranged?3:4;AssetDatabase.CreateAsset(w,"Assets/Resources/"+title.Replace(" ","")+".asset");return w;}
        static EnemyData Enemy(string title,int hp,int armor,int damage,bool mossback)
        {var e=ScriptableObject.CreateInstance<EnemyData>();e.title=title;e.hp=hp;e.armor=armor;e.damage=damage;e.mossback=mossback;e.pounce=!mossback;AssetDatabase.CreateAsset(e,"Assets/Resources/"+title.Replace(" ","")+".asset");return e;}
    }
}

