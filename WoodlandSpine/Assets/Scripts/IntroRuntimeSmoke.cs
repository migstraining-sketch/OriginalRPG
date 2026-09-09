using System;
using System.Collections;
using System.IO;
using UnityEngine;
namespace WoodlandSpine
{
    public class IntroRuntimeSmoke : MonoBehaviour
    {
        public SliceGame game;
        int checks;
        string folder;
        bool failed;
        void Awake(){QualitySettings.vSyncCount=0;Application.targetFrameRate=60;Application.SetStackTraceLogType(LogType.Log,StackTraceLogType.None);Application.logMessageReceived+=OnLog;}
        void OnDestroy(){Application.logMessageReceived-=OnLog;}
        void OnLog(string message,string stack,LogType type){if(type==LogType.Exception){failed=true;Application.Quit(1);}}
        void Check(bool value,string label){if(!value)throw new Exception("INTRO FAILED: "+label);checks++;Debug.Log("INTRO PASS: "+label);}
        void Choose(string label){var choice=game.dialogue.choices.Find(c=>c.label==label);Check(choice!=null,"choice "+label);choice.choose();}
        void ResetBranch()
        {
            game.opening.state=new OpeningState();game.opening.inLab=false;game.CloseDialogue();game.showInventory=false;game.notice="";
            game.player.Place(new Vector3(0,.1f,-3));game.intro.Initialize(game);
            game.world.innMarlow.SetActive(true);game.opening.props.labMarlow.SetActive(false);
            game.world.basementHatch.transform.rotation=Quaternion.identity;game.world.basementHatch.GetComponent<Collider>().enabled=true;
            var drop=game.world.travelSample.GetComponent<SampleDrop>();if(drop!=null)DestroyImmediate(drop);
            game.world.travelSample.SetActive(true);game.world.travelSample.transform.position=new Vector3(-4.8f,1.4f,-2.3f);game.world.travelSample.transform.rotation=Quaternion.identity;game.world.sampleDropCount=0;
            foreach(Transform child in game.world.root)if(child.name=="Broken sample fragment")child.gameObject.SetActive(false);
        }
        void FinishIntro()
        {
            int safety=0;while(game.intro.Story.beat!=IntroBeat.Finished&&safety++<20)
            {
                Check(!game.dialogue.text.Contains("Bloodleaf"),"no shopping list during inn introduction");
                if(game.intro.Story.beat<IntroBeat.Invitation)Check(!game.opening.state.invitedDownstairs,"basement remains private before invitation");
                if(game.intro.Story.beat==IntroBeat.Invitation)Check(game.opening.state.invitedDownstairs&&!game.intro.Story.warnedAfterInvitation,"invitation precedes warning");
                game.dialogue.choices[0].choose();
            }
            Check(game.opening.state.metGarrick&&game.opening.state.invitedDownstairs&&game.intro.Story.warnedAfterInvitation,"branch converges to shared invitation");
            Check(game.mode==GameMode.Exploration,"control after introduction");
        }
        IEnumerator Start()
        {
            yield return null;game.enabled=false;game.player.enabled=false;
            folder=Path.GetFullPath(Path.Combine(Application.dataPath,"../../..","Validation"));Directory.CreateDirectory(folder);
            Check(game.mode==GameMode.Exploration&&string.IsNullOrEmpty(game.Objective)&&string.IsNullOrEmpty(game.notice),"immediate control and no mandatory opening objective");
            yield return new WaitForSeconds(.2f);Capture("Intro-spawn.png");
            string[] keys={"garrick","kitchen","upstairs","basement","board","marlow"};
            Vector3[][] routes={
                new[]{new Vector3(0,.1f,1.7f),new Vector3(-5,.1f,1.7f)},
                new[]{new Vector3(0,.1f,4.9f),new Vector3(5.5f,.1f,4.9f)},
                new[]{new Vector3(4.7f,.1f,-3.8f),new Vector3(6.5f,.1f,-4.7f)},
                new[]{new Vector3(0,.1f,-5.5f),new Vector3(-5.6f,.1f,-5.5f),new Vector3(-5.6f,.1f,-2.8f),new Vector3(-7,.1f,-2.8f)},
                new[]{new Vector3(0,.1f,-5.5f),new Vector3(-8,.1f,-5.5f),new Vector3(-8,.1f,4.5f)},
                new[]{new Vector3(-2.3f,.1f,-4.2f),new Vector3(-3.8f,.1f,-4.2f)}};
            for(int branch=0;branch<keys.Length;branch++)
            {
                ResetBranch();foreach(Vector3 point in routes[branch])yield return Walk(point);
                if(failed)yield break;
                var prompt=game.world.interactions.Find(i=>i.key==keys[branch]);Check(Vector3.Distance(prompt.transform.position,game.player.transform.position)<2.4f,"reachable first-interaction prompt "+keys[branch]);
                game.Interact(keys[branch]);Check(game.mode==GameMode.Dialogue,"first branch "+keys[branch]);
                var first=game.intro.Story.first;game.CloseDialogue();game.Interact(keys[branch]);Check(first==game.intro.Story.first,"interrupted branch retains context "+keys[branch]);
                FinishIntro();
                if(keys[branch]=="upstairs")Check(!game.intro.Story.welcome.Contains("Rooms upstairs"),"rooms explanation not repeated");
                if(keys[branch]=="board")Check(!game.intro.Story.welcome.Contains("Work comes through"),"board explanation not repeated");
            }
            ResetBranch();game.Interact("merchandise");Check(!game.intro.Story.theftWarned&&game.inventory.weapon==null,"looking at stock is allowed");
            Choose("Try to take a blade without paying");Check(game.intro.Story.theftWarned&&game.inventory.weapon==null,"taking warns without granting item");FinishIntro();
            ResetBranch();game.Interact("seat");game.intro.Tick(5);Check(game.mode==GameMode.Exploration,"no five-second idle interruption");
            game.intro.Tick(64);Check(game.mode==GameMode.Exploration,"idle waits for configured threshold");game.intro.Tick(1.1f);Check(game.intro.Story.first==FirstApproach.Idle,"seated idle acknowledgement at 70 seconds simulated");FinishIntro();
            ResetBranch();game.enabled=true;yield return Walk(new Vector3(0,.1f,11));Check(game.mode==GameMode.Exploration&&game.inventory.weapon==null&&game.intro.Story.leftBeforeIntroduction,"actually leave unarmed without interruption");
            yield return Walk(new Vector3(0,.1f,-1));game.enabled=false;Check(game.intro.Story.returnedAfterLeaving,"return remembered");game.Interact("garrick");Check(game.dialogue.text.Contains("back in"),"return context used in greeting");
            // Pause on the incident for a rendered capture; it must not replay when resumed.
            while(game.intro.Story.beat!=IntroBeat.Sample)game.dialogue.choices[0].choose();
            int drops=game.world.sampleDropCount;game.CloseDialogue();game.Interact("garrick");Check(game.world.sampleDropCount==drops,"sample is not dropped again on resume");
            yield return new WaitForSeconds(1);
            Capture("Intro-incident.png");yield return new WaitForSeconds(.2f);FinishIntro();
            game.Interact("basement");Check(game.opening.inLab&&game.mode==GameMode.Exploration&&game.dialogue==null,"lab entry returns control without exposition");
            game.Interact("troll");Check(!game.opening.state.sawTroll&&game.mode==GameMode.Exploration,"short lab exploration grace period");
            yield return Walk(new Vector3(35,.1f,-3));game.intro.Tick(4.1f);
            foreach(string key in new[]{"lab_notes","lab_specimens","lab_equipment","lab_habitats","lab_ale"}){game.Interact(key);Check(game.mode==GameMode.Dialogue,"lab inspect "+key);game.CloseDialogue();}
            Capture("Intro-lab.png");yield return new WaitForSeconds(.2f);
            game.Interact("troll");Check(game.dialogue.text=="He was drowning.","restrained troll reveal");Choose("You brought him back from an expedition?");Choose("Can you treat him?");Choose("What would you need?");
            Choose("I can't take this on.");Choose("Leave him to his work");Check(game.mode==GameMode.Exploration&&game.intro.Story.offerRefused&&!game.opening.state.questAccepted&&game.Objective=="","refusal preserves control and consequence hook");
            game.Interact("lab_exit");Check(!game.opening.inLab,"refusal does not trap player in lab");game.Interact("basement");game.intro.Tick(4.1f);game.Interact("lab_marlow");
            Choose("I'll gather the ingredients.");game.CloseDialogue();Check(game.opening.state.questAccepted&&!game.intro.Story.offerRefused&&game.Objective.Contains("HELP MARLOW PREPARE A TREATMENT"),"acceptance activates simple treatment objective");
            // Exercise retained combat selection only; no woodland story expansion in this test.
            game.inventory.Receive(game.rules.weapons[0]);game.player.Place(game.world.wildlife.grid.World(new Hex(0,0)));game.StartCombat(game.world.wildlife,game.rules.wildlife);
            foreach(CombatChoice choice in new[]{CombatChoice.Move,CombatChoice.Attack,CombatChoice.Item,CombatChoice.Dash,CombatChoice.Defend})
            {game.Select(choice);game.site.grid.CanAttack(game.inventory.weapon,game.combat.playerCell,game.combat.enemyCell);game.CancelSelection();Check(game.combat.primary&&game.combat.movement==3&&game.selection.choice==CombatChoice.None,"cancel before commit "+choice);}
            game.Select(CombatChoice.Dash);game.ConfirmSelection();game.CancelSelection();Check(!game.combat.primary&&game.combat.movement==6,"committed Dash is not rewound");
            game.combat.BeginPlayer();game.combat.Move(new Hex(-1,0));game.CancelSelection();Check(game.combat.movement==2,"committed movement not rewound");
            File.WriteAllText(Path.Combine(folder,"intro-runtime-result.txt"),$"PASS: {checks} runtime assertions. A–O reactive opening cases, merchandise, simulated idle threshold, physical route/exit/return, invitation gate, lab grace/reveal, refusal/acceptance, cancellation.\n");
            Debug.Log($"INTRO_RUNTIME_SUCCESS: {checks} assertions");Application.Quit(0);
        }
        IEnumerator Walk(Vector3 destination)
        {
            float elapsed=0;var motor=game.player.GetComponent<CharacterController>();
            while(Vector3.Distance(new Vector3(game.player.transform.position.x,.1f,game.player.transform.position.z),destination)>.25f&&elapsed<12)
            {
                Vector3 delta=destination-game.player.transform.position;delta.y=0;motor.Move(Vector3.ClampMagnitude(delta,5*Time.deltaTime)+Vector3.down*5*Time.deltaTime);
                if(!game.enabled)game.intro.Tick(Time.deltaTime);elapsed+=Time.deltaTime;yield return null;
            }
            if(elapsed>=12){failed=true;Debug.LogError("INTRO WALK BLOCKED "+destination+" actual "+game.player.transform.position);Application.Quit(1);}
        }
        void Capture(string name)
        {
            var target=new RenderTexture(1280,800,24);var previous=RenderTexture.active;
            game.view.targetTexture=target;game.view.Render();RenderTexture.active=target;
            var pixels=new Texture2D(1280,800,TextureFormat.RGB24,false);pixels.ReadPixels(new Rect(0,0,1280,800),0,0);pixels.Apply();
            File.WriteAllBytes(Path.Combine(folder,name),pixels.EncodeToPNG());game.view.targetTexture=null;RenderTexture.active=previous;Destroy(target);Destroy(pixels);
        }
    }
}
