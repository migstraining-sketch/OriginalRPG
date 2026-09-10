using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Rendering;

namespace WoodlandSpine.Editor
{
    // Offline Blender export: metres, Unity axes, baked albedo and authored collision.
    [ScriptedImporter(2,"innmodel")]
    public sealed class InnModelImporter : ScriptedImporter
    {
        [Serializable] class Header { public int version; public Surface[] materials; }
        [Serializable] class Surface { public string name,texture; public float metal,rough; public float[] emission; }
        static string Text(BinaryReader r)=>System.Text.Encoding.UTF8.GetString(r.ReadBytes(r.ReadInt32()));
        static Vector3 V(BinaryReader r)=>new Vector3(r.ReadSingle(),r.ReadSingle(),r.ReadSingle());
        public override void OnImportAsset(AssetImportContext ctx)
        {
            using(var r=new BinaryReader(File.OpenRead(ctx.assetPath)))
            {
                var h=JsonUtility.FromJson<Header>(Text(r));if(h.version!=1)throw new InvalidDataException("Unsupported inn model version");
                var root=new GameObject("Garrick's Inn — Blender v02");ctx.AddObjectToAsset("root",root);ctx.SetMainObject(root);
                var mats=new Material[h.materials.Length];
                for(int i=0;i<mats.Length;i++)
                {
                    var s=h.materials[i];var m=new Material(Shader.Find("Standard")){name=s.name};
                    string path=Path.GetDirectoryName(ctx.assetPath).Replace('\\','/')+"/"+s.texture;
                    ctx.DependsOnSourceAsset(path);m.mainTexture=AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                    if(m.mainTexture==null)throw new InvalidDataException("Missing baked inn texture: "+path);
                    m.SetFloat("_Metallic",s.metal);m.SetFloat("_Glossiness",1-s.rough);
                    if(s.emission!=null&&s.emission.Length>=3&&s.emission[0]+s.emission[1]+s.emission[2]>0)
                    {m.EnableKeyword("_EMISSION");m.SetColor("_EmissionColor",new Color(s.emission[0],s.emission[1],s.emission[2]));}
                    mats[i]=m;ctx.AddObjectToAsset("material"+i,m);
                }
                int count=r.ReadInt32();var groups=new Dictionary<string,Transform>();
                for(int i=0;i<count;i++)
                {
                    string name=Text(r);var go=new GameObject(name);go.transform.SetParent(root.transform);groups[name]=go.transform;
                    int n=r.ReadInt32();var vertices=new Vector3[n];var normals=new Vector3[n];var uv=new Vector2[n];
                    for(int v=0;v<n;v++){vertices[v]=V(r);normals[v]=V(r);uv[v]=new Vector2(r.ReadSingle(),r.ReadSingle());}
                    var mesh=new Mesh{name=name,indexFormat=IndexFormat.UInt32};mesh.vertices=vertices;mesh.normals=normals;mesh.uv=uv;
                    int subs=r.ReadInt32();mesh.subMeshCount=subs;var used=new Material[subs];
                    var proxyVertices=new List<Vector3>();var proxyNormals=new List<Vector3>();var proxyUv=new List<Vector2>();Material proxyMaterial=null;
                    for(int s=0;s<subs;s++)
                    {
                        int materialIndex=r.ReadInt32();used[s]=mats[materialIndex];var ids=new int[r.ReadInt32()];for(int t=0;t<ids.Length;t++)ids[t]=r.ReadInt32();
                        // The legacy export combined Garrick into Common. Isolate only his
                        // uniquely named source material; furniture and collision stay untouched.
                        if(name=="Common"&&h.materials[materialIndex].name.StartsWith("Garrick scale silhouette",StringComparison.Ordinal))
                        {
                            proxyMaterial=used[s];foreach(int vertex in ids){proxyVertices.Add(vertices[vertex]);proxyNormals.Add(normals[vertex]);proxyUv.Add(uv[vertex]);}
                            mesh.SetTriangles(Array.Empty<int>(),s);
                        }
                        else mesh.SetTriangles(ids,s);
                    }
                    if(proxyMaterial!=null)
                    {
                        var proxy=new GameObject("GarrickProxy");proxy.transform.SetParent(root.transform);
                        var pm=new Mesh{name="Garrick original scale figure"};pm.SetVertices(proxyVertices);pm.SetNormals(proxyNormals);pm.SetUVs(0,proxyUv);
                        var indices=new int[proxyVertices.Count];for(int t=0;t<indices.Length;t++)indices[t]=t;pm.SetTriangles(indices,0);pm.RecalculateBounds();
                        proxy.AddComponent<MeshFilter>().sharedMesh=pm;proxy.AddComponent<MeshRenderer>().sharedMaterial=proxyMaterial;ctx.AddObjectToAsset("garrick-proxy",pm);
                    }
                    mesh.RecalculateBounds();go.AddComponent<MeshFilter>().sharedMesh=mesh;go.AddComponent<MeshRenderer>().sharedMaterials=used;
                    ctx.AddObjectToAsset("mesh"+i,mesh);
                }
                // One static collision mesh per visibility group, not hundreds of loose plank colliders.
                var cv=new Dictionary<string,List<Vector3>>();var ct=new Dictionary<string,List<int>>();
                count=r.ReadInt32();
                for(int i=0;i<count;i++)
                {
                    string group=Text(r);Text(r);if(!cv.ContainsKey(group)){cv[group]=new List<Vector3>();ct[group]=new List<int>();}
                    int offset=cv[group].Count,n=r.ReadInt32();for(int v=0;v<n;v++)cv[group].Add(V(r));
                    n=r.ReadInt32();for(int v=0;v<n;v++)ct[group].Add(offset+r.ReadInt32());
                }
                foreach(var pair in cv)
                {
                    var mesh=new Mesh{name=pair.Key+" collision",indexFormat=IndexFormat.UInt32};mesh.SetVertices(pair.Value);mesh.SetTriangles(ct[pair.Key],0);mesh.RecalculateBounds();
                    groups[pair.Key].gameObject.AddComponent<MeshCollider>().sharedMesh=mesh;ctx.AddObjectToAsset("collision"+pair.Key,mesh);
                }
                if(r.BaseStream.Position!=r.BaseStream.Length)throw new InvalidDataException("Unexpected trailing inn model data");
            }
        }
    }
}
