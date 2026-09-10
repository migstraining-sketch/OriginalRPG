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
            if(game==null||game.mode!=GameMode.Exploration||game.showInventory||game.Modal)return;
            if(motor.isGrounded)lastGrounded=transform.position;
            if(transform.position.y<-20){Place(lastGrounded);game.notice="Back on solid ground.";return;}
            float x=(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)?1:0)-(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)?1:0);
            float z=(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)?1:0)-(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)?1:0);
            Vector3 forward=Vector3.ProjectOnPlane(game.view.transform.forward,Vector3.up).normalized;
            Vector3 right=Vector3.Cross(Vector3.up,forward);
            Vector3 move=(right*x+forward*z).normalized;
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
        float yaw,zoom=1;
        Vector3 pan;
        public float Yaw=>yaw;
        public void ResetView(){yaw=0;zoom=1;pan=Vector3.zero;framedGrid=null;}
        public void Adjust(float rotation,float zoomDelta,Vector2 drag)
        {
            yaw=Mathf.Repeat(yaw+rotation,360);
            zoom=Mathf.Clamp(zoom+zoomDelta,.6f,1.7f);
            if(game.mode!=GameMode.Combat)
            {
                Vector3 right=Vector3.ProjectOnPlane(transform.right,Vector3.up).normalized;
                Vector3 forward=Vector3.ProjectOnPlane(transform.forward,Vector3.up).normalized;
                pan=Vector3.ClampMagnitude(pan-right*drag.x-forward*drag.y,7);
            }
            framedGrid=null;
        }
        Vector3 lastTarget;
        bool wasLab,wasRoom,wasKitchen;
        void LateUpdate()
        {
            if(game==null||game.player==null)return;
            bool battle=game.mode==GameMode.Combat;
            if(!game.Modal&&!game.showInventory&&game.mode!=GameMode.Dialogue)
            {
                if(Input.GetKeyDown(KeyCode.Home))ResetView();
                bool pointerInView=game.view.pixelRect.Contains(Input.mousePosition);
                float turn=((Input.GetKey(KeyCode.R)?1:0)-(Input.GetKey(KeyCode.Q)?1:0))*70*Time.deltaTime;
                float scroll=pointerInView?-Input.mouseScrollDelta.y*.08f:0;
                Vector2 drag=Vector2.zero;
                if(pointerInView&&Input.GetMouseButton(2))
                {
                    if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift))drag=new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"))*.25f;
                    else turn+=Input.GetAxis("Mouse X")*3;
                }
                if(turn!=0||scroll!=0||drag!=Vector2.zero)Adjust(turn,scroll,drag);
            }
            Quaternion orbit=Quaternion.Euler(0,yaw,0);
            if(battle)
            {
                var rect=CombatViewport.Pixels(Screen.width,Screen.height);game.view.pixelRect=rect;game.view.aspect=rect.width/rect.height;
                transform.position=game.site.grid.origin+orbit*new Vector3(0,22,-14);
                transform.rotation=Quaternion.LookRotation(game.site.grid.origin-transform.position);
                if(framedGrid!=game.site.grid||framedWidth!=Screen.width||framedHeight!=Screen.height)
                {framedGrid=game.site.grid;framedWidth=Screen.width;framedHeight=Screen.height;framedSize=CombatViewport.Size(framedGrid,transform.rotation,rect.width/rect.height);}
                game.view.orthographicSize=framedSize*Mathf.Max(1,zoom);
                game.view.cullingMask=~(1<<9);return;
            }
            game.view.rect=new Rect(0,0,1,1);game.view.ResetAspect();
            Vector3 target=battle?game.site.grid.origin:game.player.transform.position+Vector3.up*.5f;
            if(!battle&&!game.opening.inLab&&game.player.transform.position.z<7)target+=Vector3.forward*1.5f;
            if(game.coordinated.travel.knowledge.current==Region.Inn&&!game.opening.inLab)target=new Vector3(0,.65f,0);
            if(game.full.inRoom)target=new Vector3(0,4.1f,0);
            if(game.mode==GameMode.Dialogue&&game.intro!=null&&game.intro.Story.beat>=IntroBeat.Sample&&game.intro.Story.beat<=IntroBeat.Warning)target=InnLayout.Marlow;
            if(game.opening.inLab)target=OpeningWorld.LabPoint(new Vector3(36,.5f,0));
            if(game.full.inKitchen)target=new Vector3(-3,.5f,5.5f);
            game.view.cullingMask=game.full.inRoom?(1<<12)|(1<<10)|(1<<14):game.opening.inLab?(1<<9)|(1<<10)|(1<<11):~((1<<9)|(1<<12));
            bool transition=wasLab!=game.opening.inLab||wasRoom!=game.full.inRoom||wasKitchen!=game.full.inKitchen||(target-lastTarget).sqrMagnitude>100;
            if(transition)pan=Vector3.zero;
            Vector3 desired=target+pan+orbit*new Vector3(0,13,-10);
            transform.position=transition?desired:Vector3.Lerp(transform.position,desired,1-Mathf.Exp(-Time.deltaTime*8));
            lastTarget=target;wasLab=game.opening.inLab;wasRoom=game.full.inRoom;wasKitchen=game.full.inKitchen;
            transform.rotation=Quaternion.LookRotation(target+pan-transform.position);
            game.view.orthographicSize=(game.full.inRoom?8f:game.full.inKitchen?5.8f:game.opening.inLab?9f:9.5f)*zoom;
        }
    }
}

