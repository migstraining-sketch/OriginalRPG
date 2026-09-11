using UnityEngine;
using UnityEngine.Rendering;

namespace WoodlandSpine
{
    // One-angle visual trial. Keeps the original actor's interaction/collision separate.
    [DefaultExecutionOrder(100)]
    public sealed class IllustratedInnActor : MonoBehaviour
    {
        public Camera viewingCamera;
        public bool illustrated=true;
        public bool faceCameraPitch;
        public Renderer original;
        public MeshRenderer artwork;
        Mesh ownedMesh;
        public void SetIllustrated(bool value)
        {
            illustrated=value;
            if(artwork!=null)artwork.enabled=value;
            if(original!=null)original.enabled=!value;
        }
        public void FaceCamera()
        {
            if(viewingCamera==null)return;
            Vector3 direction=viewingCamera.transform.forward;
            if(!faceCameraPitch)direction.y=0;
            if(direction.sqrMagnitude>.0001f)transform.rotation=Quaternion.LookRotation(direction,Vector3.up);
        }
        void LateUpdate()
        {
            if(Input.GetKeyDown(KeyCode.F8))SetIllustrated(!illustrated);
            FaceCamera();
        }
        void OnDestroy(){if(ownedMesh!=null)Destroy(ownedMesh);}
        public static IllustratedInnActor Install(Transform inn)
        {
            var source=inn.Find("GarrickProxy");
            var material=Resources.Load<Material>("GarrickIllustrated");
            if(source==null||material==null){Debug.LogWarning("Garrick illustrated preview unavailable; retaining original inn figure.");return null;}
            var go=new GameObject("Garrick — illustrated visual trial");go.transform.SetParent(inn,false);go.transform.localPosition=new Vector3(0,-.04f,3.22f);
            var actor=go.AddComponent<IllustratedInnActor>();actor.original=source.GetComponent<Renderer>();
            // Preserve the illustration's proportions instead of stretching it to a square texture.
            const float height=1.95f;float width=height*material.mainTexture.width/material.mainTexture.height;
            var mesh=new Mesh{name="Garrick standing art plane"};
            mesh.vertices=new[]{new Vector3(-width/2,0,0),new Vector3(width/2,0,0),new Vector3(-width/2,height,0),new Vector3(width/2,height,0)};
            mesh.uv=new[]{new Vector2(0,0),new Vector2(1,0),new Vector2(0,1),new Vector2(1,1)};
            mesh.triangles=new[]{0,2,1,2,3,1};mesh.RecalculateNormals();mesh.RecalculateBounds();actor.ownedMesh=mesh;
            go.AddComponent<MeshFilter>().sharedMesh=mesh;actor.artwork=go.AddComponent<MeshRenderer>();actor.artwork.sharedMaterial=material;
            actor.artwork.shadowCastingMode=ShadowCastingMode.Off;actor.artwork.receiveShadows=false;
            actor.SetIllustrated(true);return actor;
        }
    }
}
