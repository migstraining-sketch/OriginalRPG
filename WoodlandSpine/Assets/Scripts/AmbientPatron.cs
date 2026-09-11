using UnityEngine;

namespace WoodlandSpine
{
    public sealed class AmbientPatron : MonoBehaviour
    {
        public SliceGame game;
        public int seat;
        bool heardCrash;
        float glanceUntil;
        Quaternion rest;
        void Start(){rest=transform.rotation;}
        void Update()
        {
            if(game==null)return;
            if(!heardCrash&&game.opening.state.intro.sampleBroken){heardCrash=true;glanceUntil=Time.time+3;}
            if(Time.time<glanceUntil)
            {Vector3 direction=InnLayout.Marlow-transform.position;direction.y=0;if(direction.sqrMagnitude>.01f)transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime*3);}
            else transform.rotation=rest*Quaternion.Euler(Mathf.Sin(Time.time*(.6f+seat*.08f)+seat)*2,Mathf.Sin(Time.time*.4f+seat)*4,0);
        }
    }
}
