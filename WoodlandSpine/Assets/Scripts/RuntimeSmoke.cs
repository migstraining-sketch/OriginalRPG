using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace WoodlandSpine
{
    // Opt-in executable integration check. Uses the same interactions/model as the playable UI.
    public class RuntimeSmoke : MonoBehaviour
    {
        public SliceGame game;
        void Awake()
        {
            Application.SetStackTraceLogType(LogType.Log,StackTraceLogType.None);
            QualitySettings.vSyncCount=0;Application.targetFrameRate=60;
            Application.logMessageReceived+=FailOnException;
        }
        void OnDestroy(){Application.logMessageReceived-=FailOnException;}
        void FailOnException(string message,string stack,LogType type){if(type==LogType.Exception)Application.Quit(1);}
        void Check(bool condition,string message){if(!condition)throw new Exception(message);Debug.Log("SMOKE PASS: "+message);}
        IEnumerator Start()
        {
            yield return null;
            string folder=Path.GetFullPath(Path.Combine(Application.dataPath,"../../..","Validation"));Directory.CreateDirectory(folder);
            game.enabled=false; // Drive the public commands deterministically; camera/physics still run.
            game.player.enabled=false; // Avoid a second, keyboard-driven CharacterController.Move per frame.
            bool success=false;
            try
            {
                Check(game.mode==GameMode.Exploration&&game.hp==30&&game.inventory.Armor==1,"spawn in inn with correct stats");
                foreach(string key in new[]{"kitchen","upstairs","basement","board"}){game.Interact(key);Check(game.mode==GameMode.Dialogue,"contextual "+key);game.CloseDialogue();}
                game.Interact("garrick");
                while(game.intro.Story.beat!=IntroBeat.Finished)game.dialogue.choices[0].choose();
                Check(game.opening.state.metGarrick,"met Garrick");Check(game.opening.state.invitedDownstairs,"lab invitation");
                game.Interact("basement");Check(game.opening.inLab&&game.mode==GameMode.Exploration,"enter playable lab");
                CheckPromptAccess();
                foreach(string key in new[]{"lab_notes","lab_specimens","lab_equipment"}){game.Interact(key);game.CloseDialogue();}
                game.intro.Tick(4.1f);game.Interact("troll");Choose("You brought him back from an expedition?");Choose("Can you treat him?");Choose("What would you need?");Choose("I'll gather the ingredients.");game.CloseDialogue();Check(game.opening.state.questAccepted,"accept ingredient expedition");
                Capture(folder,"Lab-before.png");
                game.Interact("lab_exit");Check(!game.opening.inLab&&game.mode==GameMode.Exploration,"return upstairs");
                game.Loan();game.dialogue.choices[0].choose();game.CloseDialogue();Check(game.inventory.weapon==game.rules.weapons[0],"receive sword");
                success=true;
            }
            catch(Exception e){Debug.LogException(e);}
            if(!success){Application.Quit(1);yield break;}
            yield return Walk(new Vector3(-6.5f,.1f,-.5f));
            yield return Walk(new Vector3(0,.1f,-.5f));
            yield return Walk(new Vector3(0,.1f,10));
            if(walkFailed){Application.Quit(1);yield break;}
            Check(game.player.transform.position.z>9,"walk through physical inn exit");
            yield return Walk(new Vector3(-4,.1f,17));
            game.Interact("bloodleaf");Choose("Pinch off the useful leaf tips");game.CloseDialogue();
            Check(game.inventory.bloodleaf==1&&!game.opening.props.usefulLeaf.activeSelf,"harvest useful leaf, preserve stem");
            // All weapon variants must win both authored encounters using legal commands.
            foreach(WeaponData weapon in game.rules.weapons)
            {
                success=false;
                try
                {
                    game.inventory.Receive(weapon);game.hp=30;game.inventory.bandages=2;
                    game.player.Place(new Vector3(0,.08f,25.1f));game.StartCombat(game.world.wildlife,game.rules.wildlife);
                    AutoBattle();Check(game.combat.phase==Phase.Won,weapon.title+" defeats wildlife");game.CheckResult();
                    game.UseBandage();game.UseBandage();
                    game.player.Place(new Vector3(0,.08f,54.1f));
                    if(weapon==game.rules.weapons[0])
                    {
                        game.opening.TickExploration(.1f);Choose("Give it space; step back");
                        Check(game.mode==GameMode.Exploration&&game.player.transform.position.z==51,"Mossback can initially be given space");
                        game.opening.TickExploration(2.1f);Check(game.opening.state.mossbackPursued,"Mossback pursues despite space");
                        game.player.Place(new Vector3(0,.08f,54.1f));game.opening.TickExploration(4.1f);
                        Check(game.mode==GameMode.Combat,"pursuit transitions into nearby tactical combat");
                    }
                    else game.StartCombat(game.world.mossback,game.rules.mossback);
                    AutoBattle();Check(game.combat.phase==Phase.Won,weapon.title+" defeats Mossback");game.CheckResult();
                    success=true;
                }
                catch(Exception e){Debug.LogException(e);}
                if(!success){Application.Quit(1);yield break;}
            }
            game.Interact("silvermoss");Choose("Lift a small clean portion");game.CloseDialogue();
            game.Interact("mooncalf");Choose("Lower weapon; stand sideways and wait");Choose("Gently collect a small measure");game.CloseDialogue();
            Check(game.opening.state.AllGathered&&game.opening.state.ReadyToReport,"all samples and Mossback survival recorded");
            game.opening.state.milkObtained=false;game.inventory.mooncalfMilk=0;game.opening.state.mooncalfOutcome=MooncalfOutcome.None;
            game.Interact("mooncalf");Choose("Shout and drive her away");game.CloseDialogue();
            game.Interact("milk_cache");Choose("Take one labelled sample");game.CloseDialogue();
            Check(game.opening.state.milkObtained&&game.opening.state.mooncalfOutcome==MooncalfOutcome.Frightened,"frightened Mooncalf alternative sample");
            // Exercise selection against a live encounter, including after committed movement/action.
            game.hp=30;game.player.Place(game.world.mossback.grid.World(new Hex(0,4)));game.StartCombat(game.world.mossback,game.rules.mossback);game.site.actor.gameObject.SetActive(true);
            foreach(CombatChoice choice in new[]{CombatChoice.Move,CombatChoice.Attack,CombatChoice.Item})
            {int moves=game.combat.movement;game.Select(choice);game.CancelSelection();Check(game.selection.choice==CombatChoice.None&&game.combat.primary&&game.combat.movement==moves,"cancel uncommitted "+choice);}
            game.combat.Move(new Hex(1,3));game.combat.Defend();int spent=game.combat.movement;game.CancelSelection();
            Check(!game.combat.primary&&game.combat.movement==spent,"cancel cannot rewind committed budgets");
            yield return new WaitForSeconds(1);
            Capture(folder,"Mossback.png");
            game.world.HideGrid(game.site);game.site.actor.gameObject.SetActive(false);game.mode=GameMode.Exploration;
            game.player.Place(new Vector3(0,.1f,23));
            yield return Walk(new Vector3(0,.1f,0));
            if(walkFailed){Application.Quit(1);yield break;}
            success=false;
            try
            {
                game.Interact("basement");game.Interact("lab_marlow");
                for(int i=0;i<4;i++)game.dialogue.choices[0].choose();game.CloseDialogue();
                Check(game.opening.state.returnedToMarlow,"Mossback observation debrief");game.Interact("workbench");
                Check(game.mode==GameMode.Brewing,"interactive workbench");game.opening.BrewAction("whole");Check(game.opening.brew.step==BrewStep.Bloodleaf,"supervised mistake is recoverable");
                game.opening.BrewAction("separate");game.mode=GameMode.Exploration;game.Interact("workbench");Check(game.opening.brew.step==BrewStep.Silvermoss,"brew resumes after stepping away");
                game.opening.BrewAction("brush");game.opening.brew.milkMeasure=1;game.opening.BrewAction("measure");
                game.opening.BrewAction("leaf");game.opening.BrewAction("moss");game.opening.BrewAction("milk");game.opening.brew.heat=.45f;
                game.opening.BrewAction("stir");game.opening.BrewAction("stir");game.opening.BrewAction("stir");game.opening.BrewAction("decant");
                Check(game.opening.state.potionCompleted&&game.inventory.bloodleaf==0&&game.inventory.experimentalPotion==1,"batch consumes samples once");
                game.Interact("troll");Choose("Give the experimental potion");Check(game.opening.state.trollTreated&&game.inventory.healthPotions==1,"treatment and remaining potion");
                success=true;
            }
            catch(Exception e){Debug.LogException(e);}
            if(!success){Application.Quit(1);yield break;}
            yield return new WaitForSeconds(4.5f);Capture(folder,"Lab-recovery.png");
            game.Interact("lab_exit");game.Interact("garrick");Choose("It worked. He's eating again.");
            Check(game.opening.state.huntingBoardUnlocked&&game.opening.state.healthRecipeUnlocked,"profession recipe and Hunting board unlock");
            game.Interact("board");Choose("Mud in the Moonrice");Check(game.dialogue.text.Contains("not implemented"),"future contract stub");
            File.WriteAllText(Path.Combine(folder,"runtime-result.txt"),"PASS: Unity runtime opening, lab, gathering (both milk routes), all weapon encounters, selection cancellation, physical inn exit/return, debrief, supervised brewing/resume, troll recovery, board unlock.\n");
            Debug.Log("RUNTIME_SMOKE_SUCCESS");Application.Quit(0);
        }
        bool walkFailed;
        IEnumerator Walk(Vector3 destination)
        {
            float elapsed=0;var motor=game.player.GetComponent<CharacterController>();
            while(Vector3.Distance(new Vector3(game.player.transform.position.x,.1f,game.player.transform.position.z),destination)>.25f&&elapsed<12)
            {Vector3 delta=destination-game.player.transform.position;delta.y=0;motor.Move(Vector3.ClampMagnitude(delta,5*Time.deltaTime)+Vector3.down*5*Time.deltaTime);elapsed+=Time.deltaTime;yield return null;}
            if(elapsed>=12)
            {
                walkFailed=true;Debug.LogError("SMOKE WALK BLOCKED: target "+destination+" actual "+game.player.transform.position);
                Vector3 probe=game.player.transform.position+(destination-game.player.transform.position).normalized*.5f;
                foreach(var collider in Physics.OverlapCapsule(probe+Vector3.up*.4f,probe+Vector3.up*1.4f,.5f))Debug.Log("SMOKE BLOCKER: "+collider.name+" bounds "+collider.bounds);
            }
        }
        void Choose(string label)
        {
            var choice=game.dialogue.choices.Find(c=>c.label==label);Check(choice!=null,"dialogue choice: "+label);choice.choose();
        }
        void CheckPromptAccess()
        {
            Physics.SyncTransforms();
            foreach(var interaction in game.world.interactions)
            {
                if(!interaction.gameObject.activeInHierarchy)continue;
                bool reachable=false;Vector3 p=interaction.transform.position;
                for(int i=0;i<12;i++)
                {
                    float angle=i*Mathf.PI/6;Vector3 foot=new Vector3(p.x+Mathf.Cos(angle)*1.4f,.1f,p.z+Mathf.Sin(angle)*1.4f);
                    if(Vector3.Distance(foot,p)>2.4f)continue;
                    if(!Physics.CheckCapsule(foot+Vector3.up*.4f,foot+Vector3.up*1.4f,.32f)){reachable=true;break;}
                }
                Check(reachable,"clear standing space for prompt: "+interaction.key);
            }
        }
        void Capture(string folder,string name)
        {
            Vector3 center=game.mode==GameMode.Combat?game.site.grid.origin:game.opening.inLab?new Vector3(36,.5f,0):game.player.transform.position;
            game.view.transform.position=center+new Vector3(0,15,-11);game.view.transform.LookAt(center);
            var target=new RenderTexture(1280,800,24);game.view.targetTexture=target;game.view.Render();RenderTexture.active=target;
            var texture=new Texture2D(1280,800,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,1280,800),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(folder,name),texture.EncodeToPNG());
            game.view.targetTexture=null;RenderTexture.active=null;Destroy(target);Destroy(texture);
        }
        void AutoBattle()
        {
            CombatModel c=game.combat;
            for(int turn=0;turn<100&&c.phase==Phase.Player;turn++)
            {
                // Choose a safe reachable firing position. Avoid the fixed charge lane first.
                var reachable=c.grid.Reach(c.playerCell,c.movement,c.enemyCell,out _);
                Hex best=c.playerCell;float bestScore=float.NegativeInfinity;
                foreach(var pair in reachable)
                {
                    Hex h=pair.Key;bool can=c.grid.CanAttack(c.inventory.weapon,h,c.enemyCell);
                    float score=(can?100:0)-h.Distance(c.enemyCell)*3-pair.Value*.1f;
                    if(c.preparing&&c.lane.Contains(h))score-=1000;
                    if(c.inventory.weapon.geometry==WeaponGeometry.Ranged&&h.Distance(c.enemyCell)<2)score-=200;
                    if(score>bestScore){bestScore=score;best=h;}
                }
                c.Move(best);
                if(c.playerHP<=10&&c.inventory.bandages>0)c.Item();else c.Attack();
                if(c.phase==Phase.Won)break;
                if(c.primary)c.Defend();
                // After attacking, use leftover movement to step out of melee when possible.
                if(!c.preparing)
                {
                    reachable=c.grid.Reach(c.playerCell,c.movement,c.enemyCell,out _);best=c.playerCell;int distance=best.Distance(c.enemyCell);
                    foreach(var pair in reachable)if(pair.Key.Distance(c.enemyCell)>distance){best=pair.Key;distance=best.Distance(c.enemyCell);}
                    c.Move(best);
                }
                c.EndPlayer();c.ResolveEnemy();
            }
            game.Refresh();
        }
    }
}
