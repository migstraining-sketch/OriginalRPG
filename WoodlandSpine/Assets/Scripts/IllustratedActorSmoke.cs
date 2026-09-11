using System.Collections;
using UnityEngine;

namespace WoodlandSpine
{
    public sealed partial class CoordinatedSmoke
    {
        IEnumerator GarrickArt()
        {
            var art=game.world.inn.garrick;var rig=game.view.GetComponent<SliceCamera>();
            Check(art!=null&&art.artwork!=null&&art.original!=null,"illustration and isolated original are present");
            Check(art.illustrated&&art.artwork.enabled&&!art.original.enabled,"illustration replaces original without duplicate body");
            Check(art.GetComponentsInChildren<Collider>().Length==0,"visual plane adds no collision or interaction blocker");
            Check(art.artwork.sharedMaterial.shader.isSupported,"cutout shader supported by actual player renderer");
            var texture=art.artwork.sharedMaterial.mainTexture;
            Check(texture.width==1024&&texture.height==1536,"import preserves source image aspect ratio");
            Check(art.transform.parent==game.world.inn.model.transform,"art shares inn region visibility");
            yield return Walk(new Vector3(0,.15f,-4.4f));yield return Walk(new Vector3(0,.15f,.55f));yield return null;
            Check(game.nearby!=null&&game.nearby.key=="garrick","existing public-side approach finds Garrick interaction");
            game.Interact(game.nearby.key);Check(game.dialogue!=null,"Garrick opening dialogue still opens from bar");game.CloseDialogue();
            rig.Adjust(0,-.4f,Vector2.zero);yield return null;yield return Capture("02-bar-illustrated");
            foreach(float turn in new[]{45f,45f,90f,90f,90f})
            {
                rig.Adjust(turn,0,Vector2.zero);yield return null;yield return null;
                Vector3 direction=game.view.transform.forward;direction.y=0;
                Check(Vector3.Dot(art.transform.forward,direction.normalized)>.999f,"cutout faces movable camera at yaw "+rig.Yaw);
            }
            rig.ResetView();yield return null;
            rig.Adjust(0,-.7f,new Vector2(0,-3.2f));yield return new WaitForSeconds(.6f);
            Check(game.view.orthographicSize<3,"exploration wheel supports close zoom");
            var head=game.view.WorldToViewportPoint(art.transform.position+Vector3.up*1.8f);
            Check(head.x>.1f&&head.x<.9f&&head.y>.1f&&head.y<.9f,"existing exploration pan frames Garrick at close zoom");
            yield return Capture("03-gameplay-close-zoom");
            // Diagnostic close-up uses the real game geometry/material, with a fixed test camera.
            rig.enabled=false;game.view.transform.position=new Vector3(1.8f,4,-1.1f);game.view.transform.LookAt(new Vector3(0,1.1f,3.1f));game.view.orthographicSize=2.8f;
            yield return null;yield return Capture("03-bar-close-up");
            art.SetIllustrated(false);Check(!art.artwork.enabled&&art.original.enabled,"comparison toggle restores only the original figure");yield return Capture("04-original-comparison");
            art.SetIllustrated(true);rig.enabled=true;rig.ResetView();yield return null;
            VerifyCutoutPixels(art.artwork.sharedMaterial);
            game.coordinated.travel.knowledge.Discover(Region.Woodland);yield return Travel(Region.Woodland);
            Check(!art.gameObject.activeInHierarchy,"inn cutout is hidden in Woodland");yield return Travel(Region.Inn);
            Check(art.gameObject.activeInHierarchy&&art.artwork.enabled&&!art.original.enabled,"illustrated Garrick survives return to inn");
        }
        void VerifyCutoutPixels(Material material)
        {
            Check(SystemInfo.graphicsDeviceType!=UnityEngine.Rendering.GraphicsDeviceType.Null,"pixel validation uses a real graphics device");
            const int layer=30;var cameraGo=new GameObject("Cutout GPU verification camera");var camera=cameraGo.AddComponent<Camera>();camera.enabled=false;
            camera.cullingMask=1<<layer;camera.clearFlags=CameraClearFlags.SolidColor;camera.backgroundColor=new Color(.45f,.12f,.4f);camera.orthographic=true;camera.orthographicSize=1.05f;camera.aspect=2f/3;
            camera.transform.position=new Vector3(80,.975f,-4);camera.transform.rotation=Quaternion.identity;
            var buffer=new RenderTexture(256,384,24);var pixels=new Texture2D(256,384,TextureFormat.RGB24,false);var old=RenderTexture.active;camera.targetTexture=buffer;
            var plane=new GameObject("Cutout GPU verification plane");plane.layer=layer;plane.transform.position=new Vector3(80,0,0);
            plane.AddComponent<MeshFilter>().sharedMesh=game.world.inn.garrick.GetComponent<MeshFilter>().sharedMesh;
            var renderer=plane.AddComponent<MeshRenderer>();renderer.sharedMaterial=material;renderer.enabled=false;
            var block=GameObject.CreatePrimitive(PrimitiveType.Cube);block.name="Cutout GPU verification occluder";block.layer=layer;block.transform.position=new Vector3(80,.35f,-.2f);block.transform.localScale=new Vector3(2,.7f,.1f);
            var blue=new Material(material);blue.mainTexture=Texture2D.whiteTexture;blue.color=Color.blue;blue.renderQueue=2000;block.GetComponent<Renderer>().sharedMaterial=blue;block.SetActive(false);
            try
            {
                System.Action read=()=>{camera.Render();RenderTexture.active=buffer;pixels.ReadPixels(new Rect(0,0,256,384),0,0);pixels.Apply();};
                read();Color background=pixels.GetPixel(30,300);renderer.enabled=true;read();
                Check(Vector4.Distance(pixels.GetPixel(30,300),background)<.035f,"green source background renders as empty space");
                Check(Vector4.Distance(pixels.GetPixel(128,245),background)>.12f,"opaque torso remains visible");
                int green=0;foreach(var c in pixels.GetPixels32())if(c.g>c.r+50&&c.g>c.b+50)green++;
                Check(green==0,"no key-green rectangle or edge pixels leak into GPU result");
                block.SetActive(true);read();Color occluded=pixels.GetPixel(115,100);
                Check(occluded.b>.8f&&occluded.r<.1f&&occluded.g<.1f,"opaque foreground geometry occludes the cutout");
            }
            finally
            {
                camera.targetTexture=null;RenderTexture.active=old;Destroy(buffer);Destroy(pixels);Destroy(plane);Destroy(block);Destroy(blue);Destroy(cameraGo);
            }
        }
    }
}
