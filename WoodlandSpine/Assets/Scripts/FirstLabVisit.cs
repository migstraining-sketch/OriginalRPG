using System.Collections;
using UnityEngine;
namespace WoodlandSpine
{
    public enum LabBeat { Exploring, Troll, Rescue, Drowning, Illness, Research, WhyStay, Watch, Briefing, Decision, Accepted }
    public sealed class FirstLabVisit : MonoBehaviour
    {
        public SliceGame game;
        public LabBeat beat;
        public bool leading, arrived, drewAttention;
        public int touches;
        public float explorationSeconds=12;
        float explored;
        OpeningState S=>game.opening.state;
        DialogueChoice C(string text,System.Action action)=>new DialogueChoice(text,action);
        void Say(string who,string text,params DialogueChoice[] choices){if(choices.Length==0)game.Exchange(who,text,game.CloseDialogue);else game.Talk(who,text,choices);}
        public void LeadDownstairs(){if(leading||arrived)return;leading=true;StartCoroutine(Lead());}
        IEnumerator Lead()
        {
            var actor=game.world.innMarlow.transform;
            // Pick up the remaining sample before leaving the table.
            actor.localScale=new Vector3(.7f,.55f,.7f);yield return new WaitForSeconds(.7f);actor.localScale=new Vector3(.7f,.9f,.7f);
            var remains=game.world.Shape("Wrapped sample remains",actor.position+new Vector3(.4f,0,0),new Vector3(.22f,.18f,.22f),new Color(.55f,.52f,.42f),solid:false);remains.transform.SetParent(actor,true);
            Vector3[] route={new Vector3(-4,.9f,-4.8f),new Vector3(-7,.9f,-4.8f),new Vector3(-7,.9f,-3.5f),new Vector3(-7,-5.1f,5),new Vector3(-4.5f,-5.1f,5.3f),new Vector3(0,-5.1f,3)};
            for(int i=0;i<route.Length;i++)
            {
                if(i==3){game.world.OpenBasement();game.notice="Marlow: Come on, then.";}
                while(Vector3.Distance(actor.position,route[i])>.03f){actor.position=Vector3.MoveTowards(actor.position,route[i],Time.deltaTime*2.3f);yield return null;}
            }
            actor.gameObject.SetActive(false);game.opening.props.labMarlow.SetActive(true);arrived=true;leading=false;
        }
        public void Tick(float delta)
        {
            Vector3 p=game.player.transform.position;
            bool under=p.y<-1&&Mathf.Abs(p.x)<10&&p.z<8&&p.z>-8;
            if(under!=game.opening.inLab)
            {
                game.opening.inLab=under;
                foreach(var r in game.world.innRenderers)if(r!=null&&!r.name.StartsWith("Basement stair "))r.enabled=!under;
                if(under){game.intro.EnteredLab();game.notice="";}
            }
            if(!under||p.y>-5.5f||!arrived||game.mode!=GameMode.Exploration||game.showInventory||S.questAccepted||drewAttention)return;
            explored+=delta;if(explored>=explorationSeconds)DrawAttention();
        }
        public void DrawAttention()
        {
            if(drewAttention)return;drewAttention=true;beat=LabBeat.Troll;
            game.notice="A weak, uneven sound comes from the mat. Marlow turns immediately.";
            StartCoroutine(Attend());
            // Quiet placeholder creature sound, generated locally; replace with authored audio later.
            var clip=AudioClip.Create("Weak troll murmur",12000,1,24000,false);var samples=new float[12000];
            for(int i=0;i<samples.Length;i++){float t=i/24000f;samples[i]=Mathf.Sin(t*2*Mathf.PI*(150+20*Mathf.Sin(t*12)))*Mathf.Sin(Mathf.PI*i/samples.Length)*.05f;}
            clip.SetData(samples,0);AudioSource.PlayClipAtPoint(clip,game.view.transform.position,.6f);
        }
        IEnumerator Attend()
        {
            var actor=game.opening.props.labMarlow.transform;var target=OpeningWorld.LabPoint(new Vector3(38.7f,.9f,3.3f));
            while(Vector3.Distance(actor.position,target)>.03f){actor.position=Vector3.MoveTowards(actor.position,target,Time.deltaTime*3);yield return null;}
            game.opening.props.AttendPatient();
        }
        public bool Handle(string key)
        {
            if(key=="lab_ale"){Say("Fermentation notes","A crossed-out preservation experiment sits beside a surprisingly precise brewing schedule.",C("Garrick lets you keep all this down here?",()=>Ale(0)));return true;}
            if(key=="lab_habitats"||key=="lab_specimens")
            {Say(key=="lab_habitats"?"Creature habitats":"Field specimens",key=="lab_habitats"?"Each enclosure has its own soil, shelter and feeding notes. The latches are carefully secured.":"Dated specimens sit beside field sketches and corrected descriptions. The collection has travelled.",C("Look without disturbing anything.",game.CloseDialogue),C(key=="lab_habitats"?"Open an enclosure.":"Unstopper a specimen jar.",()=>{touches++;Say("Marlow",touches==1?"Please don't.":"...Definitely don't.");}));return true;}
            if(S.questAccepted)return false;
            if(key!="lab_marlow"&&key!="troll")return false;
            if(!arrived){Say("Marlow","Come down when you're ready.");return true;}
            if(!drewAttention)
            {
                if(key=="troll"&&explored>=4)DrawAttention();
                else {Say("Marlow","Mind the cases. There's room to come through.",C("Garrick lets you keep all this down here?",()=>Ale(0)),C("I'll have a look around.",game.CloseDialogue));return true;}
            }
            Show();return true;
        }
        void Next(){beat++;Show();}
        public void Show()
        {
            switch(beat)
            {
                case LabBeat.Troll:
                    S.sawTroll=true;
                    Say("Marlow","He settles beside the pale creature, one hand resting near its head. The food bowl is untouched.",C("That's a troll.",()=>Say("Marlow","Yes.",C("Why do you have a troll?",Next))));break;
                case LabBeat.Rescue:
                    Say("Marlow","There'd been flooding. I found him caught in the water under a bridge, out on an expedition. He couldn't get clear.",C("You jumped in after a troll?",Next));break;
                case LabBeat.Drowning:
                    Say("Marlow","He was drowning.",C("He doesn't look the right colour.",Next));break;
                case LabBeat.Illness:
                    Say("Marlow","He should be green. His appetite went first. Then the colour began fading, and he grew quieter.\n\nThis scratch should have closed already. It hasn't.",C("Do you know what's causing it?",Next));break;
                case LabBeat.Research:
                    Say("Marlow","Not exactly. I've been recording everything I can. I think I can treat the symptoms and restore his regenerative response.\n\nThat's what the sample was for. I need fresh ingredients to finish the preparation.",C("Why don't you gather them yourself?",Next));break;
                case LabBeat.WhyStay:
                    Say("Marlow","Normally I would. I know the woodland; I know where to find what I need.\n\nBut his condition can change quickly. The others have their own work, and they don't know his care. I won't leave him like this.",C("So you asked the watch.",Next));break;
                case LabBeat.Watch:
                    Say("Marlow","Yes. They're overwhelmed. People need them. I understand why a sick pet comes after that.\n\nHe looks back at the troll.\n\nIt doesn't make this less of an emergency.",C("Tell me what you'd need me to find.",Next));break;
                case LabBeat.Briefing:
                    Say("Marlow","Bloodleaf, Silvermoss, and Mooncalf Milk. All in the woodland beyond the front door.\n\nThe trail runs north. Red-veined Bloodleaf grows near its edge; Silvermoss likes damp, shaded stone. The Mooncalf pasture is farther on. I can show you the details if you'll go.",C("And you stay here with him.",Next));break;
                case LabBeat.Decision:
                    Say("Marlow",S.intro.offerRefused?"If you've reconsidered, I still need those ingredients.":"Yes. I can look after him while you gather them.\n\nWill you help?",C("I'll bring them back.",()=>{S.questAccepted=true;game.jobAccepted=true;S.intro.Accept();beat=LabBeat.Accepted;Briefing();}),C("I can't do this.",()=>{S.intro.Refuse();Say("Marlow","I understand. Thank you for hearing me out.");}));break;
                case LabBeat.Accepted:Briefing();break;
            }
        }
        void Briefing()=>Say("Marlow","Bloodleaf: red veins. Pinch the fresh tips; leave the stem rooted. Silvermoss: pale fronds on damp stone, out of the sun. Leave most of the patch.\n\nFor the Mooncalf, offer grass, lower your weapon and turn sideways. Let her settle. Mossbacks normally leave you alone if you give them space.\n\nAsk Garrick for a weapon before you leave.",C("Bloodleaf, Silvermoss, milk. I'll be back.",game.CloseDialogue));
        void Ale(int step)
        {
            if(step==0)Say("Marlow","Yes.",C("Why?",()=>Ale(1)));
            if(step==1)Say("Marlow","Ale.",C("...Ale?",()=>Ale(2)));
            if(step==2)Say("Marlow","It's a mutually beneficial arrangement.",C("You make ale?",()=>Ale(3)),C("I'll leave you to it.",game.CloseDialogue));
            if(step==3)Say("Marlow","Apparently.\n\nI was testing fermentation for preservation. It failed at that. Garrick tasted the result and wanted more.",C("You don't drink it?",()=>Ale(4)));
            if(step==4)Say("Marlow","I don't drink.\n\nI keep the inn supplied. He gives me somewhere to live and work.\n\nHe gestures toward the enclosures.\n\nAnd somewhere suitable for them.");
        }
    }
}
