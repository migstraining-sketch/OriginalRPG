using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace WoodlandSpine
{
    // Opt-in built-player verification. No saves or external profiles are loaded or modified.
    public sealed partial class CoordinatedSmoke : MonoBehaviour
    {
        public SliceGame game;
        int checks;string folder,scenario;
        IEnumerator Start()
        {
            Application.targetFrameRate=60;QualitySettings.vSyncCount=0;
            var args=Environment.GetCommandLineArgs();int index=Array.IndexOf(args,"--case");scenario=index>=0&&index+1<args.Length?args[index+1]:"success";
            folder=Path.GetFullPath(Path.Combine(Application.dataPath,"../../../Validation/Coordinated",scenario));Directory.CreateDirectory(folder);
            foreach(var name in new[]{"checks.txt","errors.txt","result.txt"})File.WriteAllText(Path.Combine(folder,name),"");
            Application.logMessageReceived+=OnLog;
            var stack=new Stack<IEnumerator>();stack.Push(Run());
            while(stack.Count>0)
            {
                object next=null;bool more=false;
                try{more=stack.Peek().MoveNext();if(more)next=stack.Peek().Current;}
                catch(Exception e){Debug.LogError("COORDINATED_RUNTIME_FAILED: "+scenario+" "+e);Application.Quit(1);yield break;}
                if(!more){stack.Pop();continue;}
                if(next is IEnumerator inner)stack.Push(inner);else yield return next;
            }
            File.WriteAllText(Path.Combine(folder,"result.txt"),"COORDINATED_RUNTIME_PASSED: "+scenario+" • "+checks+" checks\nScripted runtime verification; not a natural first-play timing or pacing review.");
            Debug.Log("COORDINATED_RUNTIME_PASSED: "+scenario+" • "+checks);Application.Quit(0);
        }
        void OnLog(string condition,string trace,LogType type){if(type==LogType.Exception||type==LogType.Error)File.AppendAllText(Path.Combine(folder,"errors.txt"),condition+"\n"+trace+"\n");}
        void Check(bool value,string message){if(!value)throw new Exception(message);checks++;File.AppendAllText(Path.Combine(folder,"checks.txt"),"PASS "+message+"\n");}
        void Choose(string text)
        {
            Check(game.dialogue!=null,"dialogue open for "+text);
            var choice=game.dialogue.choices.Find(c=>c.label==text&&(c.visible==null||c.visible()));Check(choice!=null,"authored choice "+text);choice.choose();
        }
        void Continue(){Check(game.dialogue?.continueAction!=null,"ordinary Continue available");game.dialogue.continueAction();}
        IEnumerator Capture(string title)
        {
            if(SystemInfo.graphicsDeviceType==UnityEngine.Rendering.GraphicsDeviceType.Null)yield break;
            yield return null;yield return new WaitForEndOfFrame();
            if(Array.IndexOf(Environment.GetCommandLineArgs(),"--capture-ui")>=0)
            {
                var frame=ScreenCapture.CaptureScreenshotAsTexture();
                if(frame!=null){File.WriteAllBytes(Path.Combine(folder,title+"-screen.png"),frame.EncodeToPNG());Destroy(frame);}
            }
            // Explicit off-screen render is reliable in an occluded test window. This captures world geometry, not IMGUI.
            var camera=game.view;var previousTarget=camera.targetTexture;var previousRect=camera.pixelRect;float previousAspect=camera.aspect;
            int width=Mathf.Max(1,(int)previousRect.width),height=Mathf.Max(1,(int)previousRect.height);
            var buffer=new RenderTexture(width,height,24);var image=new Texture2D(width,height,TextureFormat.RGB24,false);var previousActive=RenderTexture.active;
            camera.targetTexture=buffer;camera.rect=new Rect(0,0,1,1);camera.aspect=previousAspect;camera.Render();RenderTexture.active=buffer;
            image.ReadPixels(new Rect(0,0,width,height),0,0);image.Apply();File.WriteAllBytes(Path.Combine(folder,title+"-world.png"),image.EncodeToPNG());
            camera.targetTexture=previousTarget;camera.pixelRect=previousRect;camera.aspect=previousAspect;RenderTexture.active=previousActive;Destroy(buffer);Destroy(image);
        }
        IEnumerator Travel(Region destination)
        {
            game.CloseDialogue();game.coordinated.Tick(0);game.coordinated.travel.Open();game.coordinated.travel.selected=destination;
            Check(game.coordinated.travel.Commit(),"commit known destination "+destination);
            float timeout=Time.realtimeSinceStartup+5;while(game.coordinated.travel.Blocking&&Time.realtimeSinceStartup<timeout)yield return null;
            Check(game.coordinated.travel.knowledge.current==destination&&!game.Modal,"travel completes to "+destination);
        }
        IEnumerator Walk(Vector3 target)
        {
            float deadline=Time.time+12,wallDeadline=Time.realtimeSinceStartup+90;
            while(Time.time<deadline&&Time.realtimeSinceStartup<wallDeadline)
            {
                var delta=target-game.player.transform.position;delta.y=0;if(delta.magnitude<.22f)break;
                if(game.mode!=GameMode.Exploration)break;
                game.player.GetComponent<CharacterController>().Move((delta.normalized*4+Vector3.down*5)*Time.deltaTime);yield return null;
            }
            var error=target-game.player.transform.position;error.y=0;
            if(error.magnitude>=.35f){foreach(var obstacle in Physics.OverlapSphere(game.player.transform.position+Vector3.up*.8f,1.4f))Debug.Log("WALK_NEAR: "+obstacle.name+" "+obstacle.bounds);}
            Check(error.magnitude<.35f,"walkable route to "+target+"; reached "+game.player.transform.position+" in "+game.mode);
        }
        IEnumerator Run()
        {
            yield return null;yield return null;
            Check(game.mode==GameMode.Exploration&&game.hp==30&&game.inventory.Armor==1,"immediate control and starting equipment");
            Check(!game.world.wildlife.actor.gameObject.activeInHierarchy,"remote woodland not visible from Inn");
            yield return Capture("01-inn");
            if(scenario=="feedback"||scenario=="feedback-early")yield return Feedback();
            else if(scenario=="garrick-art")yield return GarrickArt();
            else if(scenario=="inn")yield return InnChecks();
            else if(scenario=="success")yield return Opening();
            else
            {
                // Explicit fixture skips repeated dialogue for independent edge-case runs.
                game.opening.state.questAccepted=true;game.opening.state.invitedDownstairs=true;game.opening.state.intro.beat=IntroBeat.Finished;
                game.inventory.Receive(game.rules.weapons[1]);game.coordinated.Tick(0);
                if(scenario=="failure")yield return Failure();else if(scenario=="stress")yield return Stress();else yield return Mud();
            }
        }
        IEnumerator CrossRoad()
        {
            float deadline=Time.realtimeSinceStartup+4;
            while(!game.Modal&&game.mode==GameMode.Exploration&&Time.realtimeSinceStartup<deadline)
            {game.player.GetComponent<CharacterController>().Move(new Vector3(0,-5,-4)*Time.deltaTime);yield return null;}
            Check(game.coordinated.travel.visible,"walking across road opens Regional Map without E");
        }
        IEnumerator Feedback()
        {
            var travel=game.coordinated.travel;
            var camera=game.view.GetComponent<SliceCamera>();
            camera.ResetView();yield return null;yield return null;
            float defaultZoom=game.view.orthographicSize;
            camera.Adjust(90,-.2f,new Vector2(2,1));yield return null;yield return null;
            Check(camera.Yaw==90&&game.view.orthographicSize<defaultZoom,"exploration camera rotates and zooms");
            camera.ResetView();yield return null;yield return null;
            Check(camera.Yaw==0&&Mathf.Approximately(defaultZoom,game.view.orthographicSize),"Home path restores camera framing");
            game.player.Place(new Vector3(-4,.15f,-5.8f));yield return null;
            Check(!travel.visible,"southern common-room floor is not a Regional Map exit");
            game.player.Place(InnLayout.Arrival);yield return null;
            yield return CrossRoad();
            Check(game.player.transform.position.z>-5.7f,"inn crossing stays inside the floor");
            travel.Cancel();
            for(int i=0;i<10;i++){game.player.GetComponent<CharacterController>().Move(new Vector3(0,0,-.2f));yield return null;}
            Check(!travel.Blocking&&game.player.transform.position.z>-5.9f,"cancel holds boundary without reopening or entering void");
            yield return Walk(InnLayout.Arrival);
            yield return CrossRoad();travel.Cancel();
            game.opening.state.questAccepted=true;game.opening.state.intro.beat=IntroBeat.Finished;
            game.inventory.Receive(game.rules.weapons[1]);
            yield return Travel(Region.Woodland);
            yield return CrossRoad();
            Check(travel.knowledge.CanTravel(Region.Inn),"Garrick's Inn selectable after physical Woodland crossing");
            Check(game.player.transform.position.z>=14,"Woodland crossing holds on ground");
            travel.selected=Region.Inn;Check(travel.Commit(),"return selected from naturally opened map");
            while(travel.Blocking)yield return null;
            Check(travel.knowledge.current==Region.Inn,"return reaches Inn");
            yield return Travel(Region.Woodland);
            game.world.wildlife.cleared=true;game.world.wildlife.actor.gameObject.SetActive(false);
            game.player.Place(new Vector3(0,.1f,55));yield return null;
            Check(!game.opening.MossbackAwake,"outward trail passes visibly resting Mossback");
            yield return Capture("resting-mossback");
            if(scenario=="feedback-early")
            {
                game.world.wildlife.cleared=false;
                game.inventory.weapon=null;
                game.player.Place(game.world.mossback.actor.position+Vector3.left*2);yield return null;yield return null;
                Check(game.opening.MossbackAwake&&!game.opening.pastureVisited&&!game.world.wildlife.cleared&&game.mode==GameMode.Dialogue,"approaching resting Mossback wakes it unarmed before pasture or wildlife victory");
                game.inventory.weapon=game.rules.weapons[1];
            }
            else
            {
                game.player.Place(new Vector3(-9,.1f,70));yield return null;
                Check(game.opening.pastureVisited&&!game.opening.state.AllGathered,"pasture reached with no ingredient completion");
                game.player.Place(new Vector3(-9,.1f,65));yield return null;yield return null;
                Check(game.opening.MossbackAwake&&game.mode==GameMode.Dialogue,"Mossback visibly wakes on physical return without ingredient gate");
            }
            Choose("Keep my distance.");
            game.player.Place(new Vector3(0,.1f,55));
            float deadline=Time.realtimeSinceStartup+10;
            while(game.mode!=GameMode.Combat&&Time.realtimeSinceStartup<deadline)yield return null;
            Check(game.mode==GameMode.Combat&&game.opening.state.mossbackPursued,"awake Mossback pursues and starts combat");
            foreach(float angle in new[]{0f,90f,180f,270f})
            {
                camera.ResetView();camera.Adjust(angle,-.4f,Vector2.zero);yield return null;yield return null;
                foreach(var h in game.site.grid.cells)
                {
                    var p=game.view.WorldToViewportPoint(game.site.grid.World(h));
                    Check(p.x>0&&p.x<1&&p.y>0&&p.y<1,"rotated combat board stays inside gameplay viewport");
                }
                Vector3 screen=game.view.WorldToScreenPoint(game.site.actor.position);
                Check(game.battle.Pick(screen,out var picked)&&picked.Equals(game.combat.enemyCell),"Mossback remains clickable after camera rotation");
            }
            camera.ResetView();yield return null;
            yield return Capture("rotatable-combat");
        }
        IEnumerator NavigateCommon(Vector3 target)
        {
            const float step=.25f;Vector3 origin=new Vector3(-8.5f,.15f,-6.5f);
            System.Func<Vector2Int,Vector3> point=c=>origin+new Vector3(c.x*step,0,c.y*step);
            System.Func<Vector3,Vector2Int> cell=v=>new Vector2Int(Mathf.RoundToInt((v.x-origin.x)/step),Mathf.RoundToInt((v.z-origin.z)/step));
            System.Func<Vector3,bool> clear=v=>{
                foreach(var c in Physics.OverlapCapsule(v+Vector3.up*.4f,v+Vector3.up*1.45f,.30f))
                    if(c.GetComponent<Explorer>()==null&&!c.isTrigger)return false;
                return true;
            };
            var start=cell(game.player.transform.position);var end=cell(target);var queue=new Queue<Vector2Int>();var prev=new Dictionary<Vector2Int,Vector2Int>();queue.Enqueue(start);prev[start]=start;
            var directions=new[]{Vector2Int.up,Vector2Int.right,Vector2Int.down,Vector2Int.left};
            while(queue.Count>0&&!prev.ContainsKey(end))
            {
                var c=queue.Dequeue();foreach(var d in directions)
                {var n=c+d;if(n.x<0||n.x>68||n.y<0||n.y>40||prev.ContainsKey(n)||!clear(point(n))||!clear((point(c)+point(n))*.5f))continue;prev[n]=c;queue.Enqueue(n);}
            }
            Check(prev.ContainsKey(end),"continuous common-room route to "+target);
            var route=new List<Vector3>();for(var c=end;c!=start;c=prev[c])route.Add(point(c));route.Reverse();
            // Only keep turning points; the actual CharacterController walks every segment.
            for(int n=0;n<route.Count;n++)if(n==route.Count-1||n==0||(route[n]-route[n-1]).normalized!=(route[n+1]-route[n]).normalized)yield return Walk(route[n]);
            yield return Walk(target);
        }
        IEnumerator InnChecks()
        {
            Check(game.world.inn.model.GetComponentsInChildren<MeshRenderer>().Length<45,"imported inn uses combined render groups");
            Check(game.world.inn.model.GetComponentsInChildren<AmbientPatron>().Length==6,"six imported patrons retain ambient behavior");
            Check(game.world.inn.model.GetComponentsInChildren<MeshCollider>().Length>=4,"authored floor and stair collision imported");
            foreach(var renderer in game.world.inn.model.GetComponentsInChildren<MeshRenderer>())
                foreach(var mat in renderer.sharedMaterials)
                    if(mat.shader.name=="Standard")Check(mat.mainTexture!=null,"baked inn texture assigned: "+mat.name);
            yield return Walk(new Vector3(0,.15f,-4.4f));yield return Walk(new Vector3(0,.15f,.55f));
            Check(Vector3.Distance(game.player.transform.position,game.world.interactions.Find(i=>i.key=="garrick").transform.position)<1,"bar reached on public side");
            yield return Capture("02-bar");
            yield return Walk(new Vector3(4.12f,.15f,.55f));yield return Capture("03-board");
            yield return Walk(new Vector3(6,.15f,.1f));yield return Walk(InnLayout.StairsBottom);
            foreach(var point in InnLayout.UpstairsRoute)yield return Walk(point);
            Check(game.full.inRoom&&game.player.transform.position.y>3.5f,"walked both connected upstairs flights");yield return Capture("04-upper-landing");
            game.full.progress.roomRented=true;yield return null;
            yield return Walk(new Vector3(0,3.8f,-4.45f));yield return Walk(new Vector3(1.5f,3.8f,-4.45f));yield return Walk(new Vector3(1.5f,3.8f,-3.25f));yield return Walk(InnLayout.ChestApproach);
            game.Interact("room_chest");Check(game.coordinated.storage.visible,"new model chest opens existing storage");game.Back();yield return Capture("05-bedroom");
            yield return Walk(new Vector3(1.5f,3.8f,-3.25f));yield return Walk(new Vector3(1.5f,3.8f,-4.45f));yield return Walk(new Vector3(0,3.8f,-4.45f));yield return Walk(InnLayout.UpperLanding);
            for(int i=InnLayout.UpstairsRoute.Length-1;i>=0;i--)yield return Walk(InnLayout.UpstairsRoute[i]);
            Check(!game.full.inRoom&&game.player.transform.position.y<.5f,"returned down the real stairs");
            yield return NavigateCommon(InnLayout.KitchenOutside);
            game.full.progress.kitchenAccess=true;yield return null;yield return Walk(InnLayout.KitchenInside);
            Check(game.full.inKitchen,"walked through unlocked kitchen door");yield return Capture("06-kitchen");
            yield return Walk(InnLayout.KitchenOutside);Check(!game.full.inKitchen,"walked back to common room");
            yield return NavigateCommon(InnLayout.BasementApproach);
            Check(game.world.basementHatch.GetComponent<Collider>().enabled&&!game.opening.state.invitedDownstairs,"private basement remains locked before invitation");
            var motor=game.player.GetComponent<CharacterController>();float until=Time.time+1;
            while(Time.time<until){motor.Move((Vector3.forward*2+Vector3.down*5)*Time.deltaTime);yield return null;}
            Check(game.player.transform.position.z<3.3f,"closed basement leaf physically stops entry");
            yield return Walk(InnLayout.BasementApproach);yield return NavigateCommon(new Vector3(-4.25f,.15f,-1.4f));
            yield return Capture("07-merchandise");
        }
        IEnumerator Opening()
        {
            game.Interact("garrick");Continue();Continue();Check(game.enteringName,"name entered during Garrick conversation");game.SubmitPlayerName("Migs");Continue();
            yield return new WaitForSeconds(game.intro.crashDelay+1.2f);
            for(int n=0;n<45&&game.dialogue?.continueAction!=null;n++)Continue();
            Choose("Sure.");Choose("All right. Show me.");
            for(int n=0;n<12&&game.dialogue?.continueAction!=null;n++)Continue();
            Check(!game.opening.state.questAccepted&&game.firstLab.leading,"invitation does not accept job");
            yield return new WaitForSeconds(13);
            yield return NavigateCommon(InnLayout.BasementApproach);
            foreach(var point in InnLayout.DownstairsRoute)yield return Walk(point);
            Check(game.opening.inLab&&game.firstLab.arrived,"physical basement descent and Marlow relocation");
            game.Interact("lab_marlow");Continue();Continue();
            Check(game.dialogue.choices.Count==2,"small forward troll decision");var decision=game.dialogue;game.Back();Check(game.dialogue==decision&&game.mode==GameMode.Dialogue,"Esc preserves visible dialogue");
            yield return Capture("02-lab-choice");Choose("What's wrong with him?");Continue();Continue();Continue();Continue();Choose("I'll get them.");Continue();Continue();game.coordinated.Tick(0);
            Check(game.inventory.cleanFieldFlask&&game.coordinated.travel.knowledge.Knows(Region.Woodland),"accepted job lends flask and reveals Woodland");
            for(int n=InnLayout.DownstairsRoute.Length-1;n>=0;n--)yield return Walk(InnLayout.DownstairsRoute[n]);
            yield return NavigateCommon(InnLayout.Arrival);
            game.Loan();game.dialogue.choices[0].choose();game.CloseDialogue();
            game.Interact("regional_exit");game.coordinated.travel.selected=Region.Woodland;yield return Capture("03-regional-map");game.Back();Check(!game.Modal,"map cancel preserves Inn");
            yield return Travel(Region.Woodland);
            game.Interact("bloodleaf");Choose("Pinch off the useful leaf tips");Continue();
            // Start at the actual encounter approach; the battle uses this exploration position.
            game.player.Place(new Vector3(0,.1f,25));game.StartCombat(game.world.wildlife,game.rules.wildlife);yield return Battle();Check(game.world.wildlife.cleared,"basic creature defeated through combat rules");
            game.Interact("silvermoss");Choose("Lift a small clean portion");Continue();
            game.coordinated.herd.Observe(1);Choose("Lower my weapon and wait at a distance.");Choose("Collect a small serving in Marlow's flask.");Continue();Check(game.opening.state.AllGathered&&!game.coordinated.herd.state.provoked,"peaceful milk collection from nursing source");
            game.player.Place(new Vector3(0,.1f,47));game.StartCombat(game.world.mossback,game.rules.mossback);yield return Capture("04-mossback");yield return Battle();Check(game.world.mossback.cleared,"Mossback combat won");
            yield return Travel(Region.Inn);game.Interact("lab_marlow");Continue();Check(game.opening.state.returnedToMarlow&&!game.opening.state.marlowKnowsMossbackIncident,"treatment first without unsolicited anomaly report");
            game.Interact("workbench");var b=game.opening.brew;game.opening.BrewAction("separate");game.opening.BrewAction("brush");b.milkMeasure=1;game.opening.BrewAction("measure");game.opening.BrewAction("leaf");game.opening.BrewAction("moss");game.opening.BrewAction("milk");b.heat=.45f;for(int i=0;i<3;i++)game.opening.BrewAction("stir");game.opening.BrewAction("decant");
            game.Interact("troll");Choose("Give the experimental potion");Check(game.opening.state.trollTreated&&game.opening.state.potionMakingUnlocked,"successful treatment unlocks learned recipe");
            game.Interact("garrick");Choose("It worked. He's eating again.");game.CloseDialogue();Check(game.opening.state.huntingBoardUnlocked,"normal treatment opens board");
            yield return null;yield return RoomChecks();
        }
        IEnumerator RoomChecks()
        {
            game.CloseDialogue();game.coordinated.party.location=Region.Inn;game.coordinated.party.Arrive(Region.Inn);game.coordinated.party.ily.hp=1;game.coordinated.party.ily.needsRest=true;game.hp=7;
            game.Interact("basic_rest");Check(game.hp==30&&game.coordinated.party.ily.hp==20&&!game.coordinated.party.ily.needsRest&&!game.full.progress.roomRented,"free Basic Rest before rental recovers present companions");
            int before=game.full.progress.coins;Check(game.full.progress.Rent()&&game.full.progress.coins==before-game.full.progress.roomPrice,"existing rental cost charged once");Check(!game.full.progress.Rent(),"no repeat rental charge");
            game.Interact("upstairs");Choose("Go upstairs.");Check(game.full.inRoom&&game.player.transform.position.y>3,"rented room is upstairs");yield return Capture("05-upstairs");
            yield return Walk(new Vector3(0,3.8f,-4.45f));yield return Walk(new Vector3(1.5f,3.8f,-4.45f));yield return Walk(new Vector3(1.5f,3.8f,-3.25f));yield return Walk(InnLayout.ChestApproach);
            game.Interact("room_chest");Check(game.coordinated.storage.visible,"physical chest opens storage");var item=InventoryItems.List(game.inventory).Find(i=>i.weapon!=null);Check(InventoryItems.Transfer(game.inventory,game.coordinated.storage.chest,item,1),"store owned weapon");game.Back();Check(!game.coordinated.storage.visible,"chest closes");
            game.Interact("room_exit");yield return Travel(Region.Woodland);yield return Travel(Region.Inn);Check(item.Count(game.coordinated.storage.chest)==1,"storage survives regional travel");
            game.Interact("upstairs");Choose("Go upstairs.");game.Interact("room_chest");yield return Capture("06-storage");Check(InventoryItems.Transfer(game.coordinated.storage.chest,game.inventory,item,1),"withdraw same weapon");game.Back();
        }
        IEnumerator Failure()
        {
            yield return Travel(Region.Woodland);game.player.Place(new Vector3(4,.1f,64));game.coordinated.herd.Attack(0);
            Check(game.combat.units.FindAll(u=>u.side==CombatSide.Enemies).Count==3,"juvenile provocation starts one three-member herd battle");Check(!game.coordinated.herd.state.SourceLost,"provocation alone is not milk failure");yield return Capture("01-herd-group");
            var source=game.combat.units.Find(u=>u.title=="Nursing Mooncow");source.hp=0;game.coordinated.herd.SyncCombat();Check(game.coordinated.herd.state.SourceLost&&game.Objective.Contains("Return to Marlow"),"source death replaces impossible objective immediately");
            game.combat.Hero.cell=new Hex(0,-game.combat.grid.radius);Check(game.combat.Flee(),"party can Flee at boundary");game.CheckResult();yield return Travel(Region.Inn);
            game.Interact("lab_marlow");Choose("I killed the nursing Mooncow. I couldn't bring a serving.");Continue();Check(game.opening.state.treatmentFailed&&game.opening.state.huntingBoardUnlocked&&!game.opening.state.potionMakingUnlocked,"genuine failure continues to board without Potion Making");
            game.full.AcceptHunt(1);Check(!game.full.progress.hunts[1].accepted,"unavailable route cannot be accepted");game.full.AcceptHunt(0);game.CloseDialogue();yield return Travel(Region.Reedwater);Check(game.coordinated.travel.knowledge.current==Region.Reedwater,"failure route continues into Hunt");
        }
        IEnumerator Mud()
        {
            game.opening.state.huntingBoardUnlocked=true;game.full.AcceptHunt(0);game.CloseDialogue();yield return Travel(Region.Reedwater);
            Check(!game.coordinated.mud.state.Inferred,"arrival does not solve Hunt");yield return Capture("01-paddies");
            if(scenario=="manage")
            {
                game.Interact("mud_patch");Choose("Loosen the crusted feeding patch.");Continue();game.Interact("mud_runnel");Choose("Open the muddy connection.");Continue();
                game.player.Place(game.full.props.origins[0]+new Vector3(9,.1f,12));float timeout=Time.realtimeSinceStartup+20;
                while(!game.coordinated.mud.state.Complete&&Time.realtimeSinceStartup<timeout){if(game.mode==GameMode.Dialogue)Choose("Use your judgment.");yield return null;}
                Check(game.coordinated.mud.state.Complete&&!game.coordinated.mud.state.Inferred&&game.full.progress.huntingLearned,"early preparation plus witnessed feeding completes Manage");yield return Capture("02-feeding");
            }
            else
            {
                game.coordinated.party.following=true;game.coordinated.party.Control();Choose(scenario=="direct"?"Stay on my lead.":"Use your judgment.");
                game.player.Place(game.full.props.origins[0]+new Vector3(3,.1f,3));game.coordinated.party.actor.position=game.player.transform.position+new Vector3(-2,.8f,0);game.coordinated.mud.Attack();
                Check(game.combat.units.Count==3&&game.combat.choosingAlly,"Ily joins physically and shares allied slots");yield return Capture("02-ily-combat");yield return Battle();
                Check(game.coordinated.mud.state.dead&&!game.coordinated.mud.state.Complete&&!game.full.progress.huntingLearned,"kill alone does not complete Cull");game.Interact("mud_target");Choose("Harvest Reedback.");Continue();Check(game.coordinated.mud.state.Complete&&game.full.progress.huntingLearned,"manual harvest completes Cull");
            }
            game.Interact("mud_client");Continue();Check(game.full.progress.foodPortions==3,"route-specific food reward supplied once");game.Interact("ily");Choose("You could come with me. I could use someone practical.");Choose("Travel with me.");Check(game.coordinated.party.recruited&&game.coordinated.party.active,"optional spoken recruitment distinguishes active party");
            yield return Travel(Region.Inn);Check(game.coordinated.party.Present&&game.coordinated.party.actor.gameObject.activeInHierarchy,"active companion physically arrives at Inn");game.Interact("garrick");Choose("I was going to find something to do with them.");Continue();Check(game.full.progress.kitchenAccess,"Garrick hands off to Sylvie");game.Interact("kitchen");yield return Capture("03-kitchen");Check(game.full.inKitchen,"existing kitchen access remains usable");
        }
        IEnumerator Battle()
        {
            var c=game.combat;int limit=0;
            while(game.mode==GameMode.Combat&&limit++<120)
            {
                if(c.phase==Phase.Enemy){c.ResolveEnemy();game.Refresh();game.CheckResult();yield return null;continue;}
                if(c.choosingAlly){c.SelectAlly(c.ReadyAllies()[0]);game.Refresh();yield return null;continue;}
                if(!c.CanAct)break;
                var actor=c.Active;var original=actor.cell;Hex best=original;Combatant target=null;float score=float.MinValue;
                foreach(var enemy in c.units)if(enemy.side==CombatSide.Enemies&&enemy.Present)
                foreach(var pair in c.Reachable())
                {
                    actor.cell=pair.Key;float value=-pair.Key.Distance(enemy.cell)*3-pair.Value*.1f;
                    if(c.CanTarget(enemy.cell))value+=100;if(c.Threatens(pair.Key))value-=300;
                    if(value>score){score=value;best=pair.Key;target=enemy;}actor.cell=original;
                }
                c.Move(best);c.SelectTarget(target);
                if(actor.hp<=12&&actor.inventory.bandages>0)c.Item();else if(c.CanTarget(target.cell))c.Attack();else if(c.CanTarget(target.cell,true))c.Signature();else c.Defend();
                if(c.phase!=Phase.Won)
                {
                    var reach=c.Reachable();Hex escape=actor.cell;
                    if(c.Threatens(escape))foreach(var cell in reach.Keys)if(!c.Threatens(cell)){escape=cell;break;}
                    c.Move(escape);c.EndPlayer();
                }
                game.Refresh();game.CheckResult();yield return null;
            }
            Check(c.phase==Phase.Won,"deterministic battle reached victory with ordinary HP/actions");
        }
        Hex FreeNear(CombatModel c,Hex preferred)
        {
            Hex best=preferred;int distance=int.MaxValue;
            foreach(var h in c.grid.cells)if(c.OpenFor(h,null)&&h.Distance(preferred)<distance){best=h;distance=h.Distance(preferred);}
            return best;
        }
        IEnumerator Stress()
        {
            yield return Travel(Region.Woodland);game.player.Place(new Vector3(0,.1f,25));game.StartCombat(game.world.wildlife,game.rules.wildlife);
            var c=game.combat;c.Hero.cell=FreeNear(c,new Hex(0,-4));
            for(int i=0;i<2;i++)
            {
                var inventory=new Inventory{body=game.rules.coat};inventory.Receive(game.rules.weapons[i+1]);
                var ally=new Combatant{id="test-ally-"+i,title="Test ally "+(i+1),side=CombatSide.Allies,controller=CombatController.Direct,hp=30,maxHP=30,inventory=inventory,cell=FreeNear(c,new Hex(i==0?-2:2,-3))};
                Check(c.AddAlly(ally),"stress fixture ally joins within cap");game.battle.actors[ally]=game.world.Shape(ally.title,c.grid.World(ally.cell)+Vector3.up*.9f,new Vector3(.65f,.9f,.65f),Color.cyan,PrimitiveType.Capsule,false).transform;
            }
            Hex[] cells={new Hex(-3,0),new Hex(3,-2),new Hex(3,1),new Hex(0,3),new Hex(-3,3)};
            for(int i=0;i<5;i++)
            {
                var enemy=c.AddEnemy(game.rules.wildlife,FreeNear(c,cells[i]),"test-enemy-"+i);enemy.title="Woodland creature "+(i+2);
                game.battle.actors[enemy]=game.world.Shape(enemy.title,c.grid.World(enemy.cell)+Vector3.up*.55f,new Vector3(1,.9f,1.2f),new Color(.56f,.4f,.27f),PrimitiveType.Sphere,false).transform;
            }
            c.CompleteSetup();game.Refresh();yield return null;yield return Capture("01-three-v-six");
            Check(c.units.Count==9&&c.Schedule.Count==9,"3v6 full participant stress");
            foreach(float angle in new[]{0f,45f,90f,180f})
            {
                game.view.transform.position=c.grid.origin+Quaternion.Euler(0,angle,0)*new Vector3(0,22,-14);game.view.transform.LookAt(c.grid.origin);
                game.view.orthographicSize=CombatViewport.Size(c.grid,game.view.transform.rotation,game.view.aspect);
                foreach(var unit in c.units)if(unit.side==CombatSide.Enemies)
                {
                    Vector3 screen=game.view.WorldToScreenPoint(game.battle.actors[unit].position);
                    Check(game.battle.Pick(screen,out var picked)&&picked.Equals(unit.cell),"individual enemy pick at camera angle "+angle);
                }
            }
            var missing=c.units[c.units.Count-1];missing.departed=true;Check(c.Schedule.Count==9,"departed unit does not rebuild current schedule");
            int safety=0;while(c.round==1&&safety++<20)
            {
                if(c.choosingAlly)c.SelectAlly(c.ReadyAllies()[0]);
                if(c.phase==Phase.Enemy)c.ResolveEnemy();else if(c.CanAct){c.Defend();c.EndPlayer();}
            }
            Check(c.round==2&&c.Schedule.Count==8,"next round reflects 3v5 after departure");
            var allyUnit=c.units.Find(u=>u.side==CombatSide.Allies&&u!=c.Hero);allyUnit.hp=0;allyUnit.RecoverAfterEncounter();Check(allyUnit.hp==1&&allyUnit.needsRest,"defeated companion recovers stable and ineligible");allyUnit.Rest();Check(allyUnit.hp==30&&!allyUnit.needsRest,"Basic Rest restores eligibility");
        }
    }
}
