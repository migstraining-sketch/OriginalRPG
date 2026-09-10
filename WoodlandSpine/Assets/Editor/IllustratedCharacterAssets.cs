using UnityEditor;
using UnityEngine;

namespace WoodlandSpine.Editor
{
    public sealed class IllustratedCharacterAssets : AssetPostprocessor
    {
        public const string TexturePath="Assets/Art/Characters/Garrick/Garrick-Keyed.png";
        void OnPreprocessTexture()
        {
            if(assetPath!=TexturePath)return;
            var importer=(TextureImporter)assetImporter;
            importer.textureType=TextureImporterType.Default;importer.textureCompression=TextureImporterCompression.Uncompressed;
            importer.npotScale=TextureImporterNPOTScale.None;
            importer.maxTextureSize=2048;importer.mipmapEnabled=true;importer.wrapMode=TextureWrapMode.Clamp;importer.filterMode=FilterMode.Bilinear;
        }
        public static void Prepare()
        {
            var importer=AssetImporter.GetAtPath(TexturePath) as TextureImporter;
            if(importer!=null&&importer.npotScale!=TextureImporterNPOTScale.None)
            {
                importer.npotScale=TextureImporterNPOTScale.None;importer.SaveAndReimport();
            }
            var texture=AssetDatabase.LoadAssetAtPath<Texture2D>(TexturePath);
            var shader=Shader.Find("Woodland/Illustrated Character");
            if(texture==null||shader==null)throw new System.Exception("Garrick visual trial image/shader missing");
            const string path="Assets/Resources/GarrickIllustrated.mat";
            var material=AssetDatabase.LoadAssetAtPath<Material>(path);
            if(material==null){material=new Material(shader);AssetDatabase.CreateAsset(material,path);}
            material.shader=shader;material.mainTexture=texture;material.color=new Color(.92f,.88f,.82f,1);
            material.SetFloat("_KeyLow",.035f);material.SetFloat("_KeyHigh",.16f);EditorUtility.SetDirty(material);
        }
    }
}
