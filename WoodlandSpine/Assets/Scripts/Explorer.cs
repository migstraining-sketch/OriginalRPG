using UnityEngine;

namespace WoodlandSpine
{
    [RequireComponent(typeof(CharacterController))]
    public class Explorer : MonoBehaviour
    {
        public SliceGame game;
        public float speed=5;
        CharacterController motor;
        Vector3 lastGrounded;
        public void Initialize(SliceGame game){this.game=game;motor=GetComponent<CharacterController>();motor.height=1.8f;motor.radius=.35f;motor.center=new Vector3(0,.9f,0);}
        void Update()
        {
            if(game==null||game.mode!=GameMode.Exploration||game.showInventory||game.full!=null&&game.full.boardVisible)return;
            if(motor.isGrounded)lastGrounded=transform.position;
            if(transform.position.y<-20){Place(lastGrounded);game.notice="Back on solid ground.";return;}
            float x=(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)?1:0)-(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)?1:0);
            float z=(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)?1:0)-(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)?1:0);
            Vector3 move=new Vector3(x,0,z).normalized;
            motor.Move((move*speed+Vector3.down*5)*Time.deltaTime);
            if(move.sqrMagnitude>.01f)transform.rotation=Quaternion.LookRotation(move);
        }
        public void Place(Vector3 position){motor.enabled=false;transform.position=position;motor.enabled=true;if(position.y>-20)lastGrounded=position;}
    }
    public class SliceCamera : MonoBehaviour
    {
        public SliceGame game;
        HexGrid framedGrid;
        int framedWidth,framedHeight;
        float framedSize;
        void LateUpdate()
        {
            if(game==null||game.player==null)return;
            bool battle=game.mode==GameMode.Combat;
            if(battle)
            {
                var rect=CombatViewport.Pixels(Screen.width,Screen.height);game.view.pixelRect=rect;game.view.aspect=rect.width/rect.height;
                transform.position=game.site.grid.origin+new Vector3(0,22,-14);
                transform.rotation=Quaternion.LookRotation(game.site.grid.origin-transform.position);
                if(framedGrid!=game.site.grid||framedWidth!=Screen.width||framedHeight!=Screen.height)
                {framedGrid=game.site.grid;framedWidth=Screen.width;framedHeight=Screen.height;framedSize=CombatViewport.Size(framedGrid,transform.rotation,rect.width/rect.height);}
                game.view.orthographicSize=framedSize;
                game.view.cullingMask=~(1<<9);return;
            }
            game.view.rect=new Rect(0,0,1,1);game.view.ResetAspect();
            Vector3 target=battle?game.site.grid.origin:game.player.transform.position+Vector3.up*.5f;
            if(!battle&&!game.opening.inLab&&game.player.transform.position.z<7)target+=Vector3.forward*1.5f;
            if(game.mode==GameMode.Dialogue&&game.intro!=null&&game.intro.Story.beat>=IntroBeat.Sample&&game.intro.Story.beat<=IntroBeat.Warning)target=new Vector3(-4,.5f,-3);
            if(game.opening.inLab)target=OpeningWorld.LabPoint(new Vector3(36,.5f,0));
            game.view.cullingMask=game.opening.inLab?(1<<9)|(1<<10)|(1<<11):~(1<<9);
            Vector3 desired=target+(battle?new Vector3(0,22,-14):new Vector3(0,13,-10));
            transform.position=Vector3.Lerp(transform.position,desired,1-Mathf.Exp(-Time.deltaTime*8));
            transform.rotation=Quaternion.LookRotation(target-transform.position);
            GetComponent<Camera>().orthographicSize=battle?13.5f:9f;
        }
    }
}

