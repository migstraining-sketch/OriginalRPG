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
            if(under&&arrived&&!S.sawTroll&&game.mode==GameMode.Exploration&&!game.showInventory&&Vector3.Distance(p,OpeningWorld.LabPoint(new Vector3(38.7f,.1f,3.3f)))<1.8f){DrawAttention();Reveal();return;}
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
            if(!S.sawTroll)
            {
                DrawAttention();
                if(key=="lab_marlow")Line("guide","Come here. I'll show you.",Reveal);
                else Reveal();
                return true;
            }
            Show();return true;
        }
        ReactiveIntroState A=>S.intro;
        void Line(string checkpoint,string text,System.Action next)
        {A.labCheckpoint=checkpoint;game.Exchange("Marlow",text,next);}
        void Reveal(){S.sawTroll=true;beat=LabBeat.Troll;Line("reveal","This is why I can't leave.",()=>Questions());}
        public void Show()
        {
            if(S.questAccepted){Briefing();return;}
            switch(A.labCheckpoint)
            {
                case "guide":Reveal();break;
                case "reveal":Questions();break;
                case "green":case "questions":Questions();break;
                case "setup":JobSetup();break;
                case "material":Material();break;
                case "ingredients":Ingredients();break;
                case "job":case "refused":JobGate();break;
                default:if(S.sawTroll)Questions();else Reveal();break;
            }
        }
        void Questions(string answer=null)
        {
            A.labCheckpoint="questions";
            game.Talk("Marlow","He should be green.",
                C("What's wrong with him?",()=>{A.symptomsKnown=true;Line("symptoms","I don't know. He stopped eating first. Then the colour began to fade. His regeneration's slowing too.",JobSetup);}),
                C("Why is he down here?",()=>{A.rescueKnown=true;game.Talk("Marlow","I found him in the river during the floods. He was trapped and couldn't get clear.",C("You pulled a troll out of the river?",()=>Line("drowning","He was drowning.",()=>{A.symptomsKnown=true;Line("symptoms","He stopped eating first. Now his colour is fading, and his regeneration's slowing.",JobSetup);})));}));
        }
        void JobSetup(){Line("setup","I think I can stabilize him.",Material);}
        void Material(){Line("material","But what I had left isn't enough. I need fresh material.",Ingredients);}
        void Ingredients()
        {
            A.ingredientsKnown=true;A.woodlandKnown=true;A.labCheckpoint="ingredients";
            game.Talk("Marlow","Bloodleaf. Silvermoss. Mooncalf Milk.",C("Mooncalf milk?",()=>Line("milk_source","The nursing Mooncow's milk. There's a herd in the woodland. I'll lend you a clean field flask for one serving.",()=>JobGate())));
            game.dialogue.continueAction=()=>JobGate();
        }
        void JobGate(string answer=null)
        {
            beat=LabBeat.Decision;A.labCheckpoint="job";
            if(A.marlowJob!=MarlowJob.Refused)A.marlowJob=MarlowJob.Offered;
            game.Talk("Marlow","If you bring them back, I can prepare the treatment.\n\nWill you help me?",
                C("I'll get them.",()=>{A.marlowJob=MarlowJob.Accepted;S.questAccepted=true;game.jobAccepted=true;A.Accept();beat=LabBeat.Accepted;Line("accepted","Thank you.",Briefing);}),
                C("No.",()=>{A.marlowJob=MarlowJob.Refused;A.Refuse();Line("refused","All right.",game.CloseDialogue);}),
                C("I'll think about it.",game.CloseDialogue));
        }
        void Briefing()=>Line("accepted","Bloodleaf has red veins. Silvermoss grows on damp, shaded stone. The herd grazes near water. Take this clean flask for a serving from the nursing Mooncow.\n\nGarrick can lend you a weapon. The front road leads toward the woodland.",game.CloseDialogue);        void Ale(int step)
        {
            if(step==0)Say("Marlow","Yes.",C("Why?",()=>Ale(1)));
            if(step==1)Say("Marlow","Ale.",C("...Ale?",()=>Ale(2)));
            if(step==2)Say("Marlow","It's a mutually beneficial arrangement.",C("You make ale?",()=>Ale(3)),C("I'll leave you to it.",game.CloseDialogue));
            if(step==3)Say("Marlow","Apparently.\n\nI was testing fermentation for preservation. It failed at that. Garrick tasted the result and wanted more.",C("You don't drink it?",()=>Ale(4)));
            if(step==4)Say("Marlow","I don't drink.\n\nI keep the inn supplied. He gives me somewhere to live and work.\n\nHe gestures toward the enclosures.\n\nAnd somewhere suitable for them.");
        }
    }
}




