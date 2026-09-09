using UnityEngine;
namespace WoodlandSpine
{
    public class SampleDrop : MonoBehaviour
    {
        [System.NonSerialized] public WorldBuilder world;
        float elapsed;
        Vector3 start;
        void Start(){start=transform.position;}
        void Update()
        {
            elapsed+=Time.deltaTime;float t=Mathf.Clamp01(elapsed/.65f);
            transform.position=Vector3.Lerp(start,new Vector3(-5.25f,.08f,-2.5f),t*t);transform.Rotate(0,0,Time.deltaTime*180);
            if(t<1)return;
            var clip=AudioClip.Create("Placeholder glass break",6000,1,24000,false);var samples=new float[6000];
            for(int i=0;i<samples.Length;i++){float time=i/24000f;float noise=Mathf.Sin(i*12.9898f)*Mathf.Sin(i*78.233f);samples[i]=noise*Mathf.Exp(-time*22)*.18f;}
            clip.SetData(samples,0);AudioSource.PlayClipAtPoint(clip,Camera.main.transform.position,.8f);
            for(int n=0;n<4;n++)world.Shape("Broken sample fragment",new Vector3(-5.3f+n*.11f,.035f,-2.4f+(n%2)*.17f),new Vector3(.1f,.04f,.16f),new Color(.51f,.6f,.52f),solid:false);
            gameObject.SetActive(false);
        }
    }
}
