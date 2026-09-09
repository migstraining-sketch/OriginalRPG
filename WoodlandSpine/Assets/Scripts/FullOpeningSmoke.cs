using System;
using System.Collections;
using System.IO;
using UnityEngine;
namespace WoodlandSpine
{
    public sealed class FullOpeningSmoke : MonoBehaviour
    {
        public SliceGame game;
        int checks;
        string folder;
        void Awake(){Application.targetFrameRate=60;QualitySettings.vSyncCount=0;Application.logMessageReceived+=Log;}
        void OnDestroy(){Application.logMessageReceived-=Log;}
        void Log(string message,string trace,LogType type){if(type==LogType.Exception){Debug.LogError("OPENING_SMOKE_FAILURE: "+message);Application.Quit(1);}}
        void Check(bool condition,string label){if(!condition)throw new Exception(label);checks++;Debug.Log("OPENING CHECK "+label);}
        void First(){Check(game.dialogue!=null,"dialogue exists");if(game.dialogue.continueAction!=null)game.dialogue.continueAction();else{var choice=game.dialogue.choices.Find(c=>c.visible==null||c.visible());Check(choice!=null,"response available");choice.choose();}}
        void Choose(string text){var c=game.dialogue.choices.Find(x=>x.label==text);Check(c!=null,"choice: "+text);c.choose();}
        IEnumerator Start()
        {
            yield return null;game.enabled=false;game.player.enabled=false;
            folder=Path.GetFullPath(Path.Combine(Application.dataPath,"../../..","Validation"));
            Check(game.Objective==""&&game.mode==GameMode.Exploration,"arrival is free control");
            game.Interact("seat");game.intro.Tick(game.intro.idleDelay+.1f);Check(game.intro.sitting&&game.mode==GameMode.Exploration&&game.dialogue==null,"idle bark permits continued sitting");game.intro.Stand();
            game.opening.state=new OpeningState();game.intro.Initialize(game);game.notice="";game.noticeUntil=0;
            game.Interact("basement");Check(game.dialogue.text.Contains("You're trying to enter it.")&&game.intro.Story.beat==IntroBeat.NotStarted,"first basement attempt is Marlow's objection only");game.CloseDialogue();
            game.Interact("basement");Check(game.dialogue.text.Contains("I see 'em, Bottle-Brain."),"repeated basement attempt brings Garrick in");game.CloseDialogue();
            game.opening.state=new OpeningState();game.intro.Initialize(game);
            yield return Walk(new Vector3(0,.1f,5.5f));yield return Walk(new Vector3(-3,.1f,5.5f));Check(Vector3.Distance(game.player.transform.position,game.world.interactions.Find(x=>x.key=="board").transform.position)<2.4f,"contract board remains physically accessible beside stairwell");
            game.Interact("board");Check(game.intro.Story.beat==IntroBeat.NotStarted,"looking at board does not trigger intervention");game.CloseDialogue();
            yield return Walk(new Vector3(0,.1f,5.5f));yield return Walk(new Vector3(0,.1f,-3));
            game.Interact("garrick");int safety=0;while(game.intro.Story.beat!=IntroBeat.Finished&&safety++<100)First();
            Check(game.firstLab.leading,"Marlow physically leads to basement");
            yield return new WaitForSeconds(13);
            yield return Walk(new Vector3(0,.1f,-5));yield return Walk(new Vector3(-7,.1f,-5));yield return Walk(new Vector3(-7,.1f,-3.6f));
            yield return Walk(new Vector3(-7,-5.9f,5));yield return Walk(new Vector3(-4.5f,-5.9f,5.2f));
            Check(game.opening.inLab&&game.mode==GameMode.Exploration,"physical descent enters lab without dialogue or teleport");
            Check(game.firstLab.arrived&&!game.world.innMarlow.activeSelf,"Marlow relocated downstairs");
            game.Interact("lab_ale");for(int i=0;i<5;i++)First();game.CloseDialogue();
            game.firstLab.Tick(13);yield return new WaitForSeconds(1.5f);Check(game.firstLab.drewAttention&&game.mode==GameMode.Exploration,"troll draws attention without taking control");
            Capture("Opening-connected-lab.png");
            game.Interact("troll");safety=0;while(game.firstLab.beat!=LabBeat.Decision&&safety++<25)First();
            Check(!game.opening.state.questAccepted,"help decision follows rescue illness and research");Choose("I can't do this.");game.CloseDialogue();game.Interact("lab_marlow");Choose("I'll bring them back.");game.CloseDialogue();
            Check(game.opening.state.questAccepted,"can reconsider after refusal");
            yield return Walk(new Vector3(-4.5f,-5.9f,5.2f));yield return Walk(new Vector3(-7,-5.9f,5.2f));yield return Walk(new Vector3(-7,.1f,-3.6f));yield return Walk(new Vector3(-7,.1f,-5));yield return Walk(new Vector3(0,.1f,-5));
            Check(!game.opening.inLab,"physical ascent returns to inn");
            game.Loan();First();game.CloseDialogue();yield return Walk(new Vector3(0,.1f,11));Check(game.player.transform.position.z>7,"front doorway physical exit");
            game.opening.state.Gather(Ingredient.Bloodleaf,game.inventory);game.opening.state.Gather(Ingredient.Silvermoss,game.inventory);game.opening.state.Gather(Ingredient.MooncalfMilk,game.inventory);
            foreach(var w in game.rules.weapons)
            {
                game.inventory.Receive(w);foreach(var site in new[]{game.world.wildlife,game.world.mossback})
                {game.hp=30;game.inventory.bandages=2;game.player.Place(site.grid.World(new Hex(0,-4)));game.StartCombat(site,site==game.world.mossback?game.rules.mossback:game.rules.wildlife);yield return null;yield return new WaitForEndOfFrame();CheckCombatFraming();AutoBattle();Check(game.combat.phase==Phase.Won,w.title+" wins "+game.combat.enemy.title);game.CheckResult();}
            }
            game.Interact("lab_marlow");for(int i=0;i<5;i++)First();game.CloseDialogue();Check(game.opening.state.returnedToMarlow,"naturalist debrief completes");
            game.Interact("workbench");var brew=game.opening.brew;brew.milkMeasure=1;brew.heat=.45f;
            foreach(string action in new[]{"separate","brush","measure","leaf","moss","milk","stir","stir","stir","decant"})game.opening.BrewAction(action);
            Check(game.opening.state.potionCompleted,"first medicine brewed");game.Interact("troll");First();game.full.Tick(0);Check(game.opening.state.trollTreated&&game.inventory.healthPotions==1,"troll dose and remainder");
            game.Interact("garrick");Choose("It worked. He's eating again.");game.CloseDialogue();Check(game.opening.state.huntingBoardUnlocked,"board opens after Marlow");
            for(int i=0;i<3;i++)
            {
                game.full.AcceptHunt(i);game.CloseDialogue();
                for(int clue=0;clue<4;clue++){game.Interact("hunt_clue_"+i+"_"+clue);game.CloseDialogue();}
                game.Interact("hunt_client_"+i);First();game.CloseDialogue();game.Interact("hunt_trail_"+i);game.CloseDialogue();
                if(i==0){var site=game.full.props.sites[i];game.inventory.Receive(game.rules.weapons[1]);game.hp=30;game.player.Place(site.grid.World(new Hex(0,-3)));game.StartCombat(site,game.full.creatures[i]);AutoBattle();Check(game.combat.phase==Phase.Won,"contract lethal battle");game.CheckResult();}
                else {game.Interact("hunt_habitat_"+i);First();}
                game.CloseDialogue();if(i==0){game.Interact("hunt_habitat_"+i);First();game.CloseDialogue();}game.Interact("hunt_client_"+i);Check(game.full.progress.hunts[i].rewarded,"contract client reward "+i);game.CloseDialogue();
            }
            Check(!game.full.props.channelBlock.activeSelf,"mill channel visibly clears");
            game.Interact("garrick");First();game.CloseDialogue();Check(game.full.progress.kitchenAccess,"Garrick opens kitchen socially");
            game.Interact("kitchen");Check(game.full.inKitchen,"kitchen enter");game.Interact("sylvie");First();First();Check(game.mode==GameMode.Cooking,"Sylvie demonstration begins");
            game.full.cooking.heat=.55f;while(game.full.cooking.step!=CookStep.Complete)game.full.CookAction();
            Check(game.full.progress.wellFed&&game.full.progress.demonstrated,"demonstration provides Well Fed");First();First();First();Choose("All right. I'd like to learn.");Check(game.full.progress.cookingLearned,"Cooking learned");game.CloseDialogue();
            yield return new WaitForSeconds(.5f);Capture("Opening-kitchen.png");
            game.Interact("pantry");First();game.CloseDialogue();game.Interact("sylvie");First();Check(game.mode==GameMode.Cooking&&game.full.cooking.practice,"optional practice uses same cooking session");
            game.mode=GameMode.Exploration;game.Interact("kitchen_exit");game.Interact("upstairs");First();game.CloseDialogue();Check(game.full.progress.roomRented,"room rentable after earnings");
            game.player.Place(new Vector3(0,.1f,15));
            for(int i=0;i<3;i++){Vector3 client=game.full.props.origins[i]+new Vector3(-6,.1f,-11);yield return Walk(new Vector3(client.x,.1f,15));yield return Walk(client);yield return Walk(new Vector3(client.x,.1f,15));yield return Walk(new Vector3(0,.1f,15));}
            Check(game.full.progress.hunts[0].rewarded&&game.full.progress.hunts[1].rewarded,"earlier contract states survive visiting other sites");
            // Exercise the actual shop callbacks: buying must not equip or discard gear.
            var carried=game.inventory;game.inventory=new Inventory{body=game.rules.coat};game.inventory.Store(game.rules.coat);
            game.inventory.Receive(game.rules.weapons[0]);game.full.progress.coins=50;
            game.Interact("merchandise");Choose("Weapons — 12 coins");Choose(game.rules.weapons[1].Description+" • 12 coins");
            Check(game.inventory.weapon==game.rules.weapons[0]&&game.inventory.weapons.Contains(game.rules.weapons[1])&&game.full.progress.coins==38,"bought weapon carried without replacing equipped weapon");
            game.CloseDialogue();game.Interact("merchandise");Choose("Reinforced coat — Armor 2, 16 coins");
            Check(game.inventory.body==game.rules.coat&&game.inventory.bodies.Count==2&&game.inventory.Armor==1&&game.full.progress.coins==22,"bought coat preserves worn armor and stores both garments");
            game.CloseDialogue();game.Interact("merchandise");Choose("Reinforced coat — Armor 2, 16 coins");
            Check(game.full.progress.coins==22&&game.inventory.bodies.Count==2,"duplicate coat purchase does not charge coins");
            game.CloseDialogue();game.inventory=carried;
            foreach(string food in new[]{"Fresh Reedback Haunch","Preserved Reedback Cut","Fresh Duskhen Eggs","Fresh Brookmaw Tail","Naturally Shed Brookmaw Tail"})
            {
                game.full.progress.demonstrated=false;game.full.progress.food=food;game.Interact("sylvie");Choose("Garrick sent me.");
                string species=food.Contains("Reedback")?"Reedback":food.Contains("Eggs")?"Duskhen":"Brookmaw";
                Check(game.dialogue.text.Contains(species),"Sylvie identifies exact species for "+food);
                Check(!game.dialogue.text.Contains("Killed")&&!game.dialogue.text.Contains("Blocked"),"Sylvie does not infer an unreported resolution");game.CloseDialogue();
            }
            File.WriteAllText(Path.Combine(folder,"opening-runtime-result.txt"),"PASS: "+checks+" assertions. Actual stair traversal, farm routes and NPC relocation, dialogue order and refusal/reconsideration, all weapon woodland fights, first brew, all three contracts (one lethal, two nonlethal), kitchen, cooking, and room rental. Scripted callbacks; not human dialogue/pacing acceptance.\n");
            Debug.Log("FULL_OPENING_RUNTIME_SUCCESS: "+checks);Application.Quit(0);
        }
        IEnumerator Walk(Vector3 target)
        {
            float elapsed=0;var motor=game.player.GetComponent<CharacterController>();
            while((new Vector2(game.player.transform.position.x-target.x,game.player.transform.position.z-target.z)).magnitude>.16f&&elapsed<18)
            {Vector3 delta=target-game.player.transform.position;delta.y=0;motor.Move(Vector3.ClampMagnitude(delta,4*Time.deltaTime)+Vector3.down*6*Time.deltaTime);game.firstLab.Tick(Time.deltaTime);elapsed+=Time.deltaTime;yield return null;}
            Check(elapsed<18,"walking route "+target+" reached from actual "+game.player.transform.position);
            Check(Mathf.Abs(game.player.transform.position.y-target.y)<.7f,"walking route vertical level");
        }
        void CheckCombatFraming()
        {
            Rect safe=CombatViewport.Pixels(Screen.width,Screen.height);
            bool fits=true;
            foreach(Hex h in game.combat.grid.cells)
                foreach(float height in new[]{0f,3f})
                    fits &= safe.Contains(game.view.WorldToScreenPoint(game.combat.grid.World(h)+Vector3.up*height));
            Check(fits,"actual combat camera keeps all cells and actors outside HUD");
        }
        void AutoBattle()
        {
            var c=game.combat;
            for(int turn=0;turn<100&&c.phase==Phase.Player;turn++)
            {
                var reach=c.grid.Reach(c.playerCell,c.movement,c.enemyCell,out _);Hex best=c.playerCell;float score=float.MinValue;
                foreach(var p in reach){float value=(c.grid.CanAttack(c.inventory.weapon,p.Key,c.enemyCell)?100:0)-p.Key.Distance(c.enemyCell)*3-p.Value*.1f;if(c.preparing&&c.lane.Contains(p.Key))value-=1000;if(c.inventory.weapon.geometry==WeaponGeometry.Ranged&&p.Key.Distance(c.enemyCell)<2)value-=200;if(value>score){score=value;best=p.Key;}}
                c.Move(best);if(c.playerHP<=10&&c.inventory.bandages>0)c.Item();else {int before=c.enemyHP;bool valid=c.primary&&c.grid.CanAttack(c.inventory.weapon,c.playerCell,c.enemyCell);game.Select(CombatChoice.Attack);game.ConfirmSelection();if(valid){Check(c.enemyHP==Math.Max(0,before-CombatModel.Damage(c.inventory.weapon.damage,c.enemy.armor)),"confirmed attack deals fixed damage once");int after=c.enemyHP;game.ConfirmSelection();Check(c.enemyHP==after,"repeat confirmation cannot attack twice");}else Check(c.enemyHP==before&&c.primary,"invalid confirmation preserves action");}if(c.phase==Phase.Won)break;if(c.primary)c.Defend();
                if(!c.preparing){reach=c.grid.Reach(c.playerCell,c.movement,c.enemyCell,out _);best=c.playerCell;int distance=best.Distance(c.enemyCell);foreach(var pair in reach)if(pair.Key.Distance(c.enemyCell)>distance){best=pair.Key;distance=best.Distance(c.enemyCell);}c.Move(best);}
                c.EndPlayer();c.ResolveEnemy();
            }
            game.Refresh();
        }
        void Capture(string file)
        {
            var r=new RenderTexture(1280,800,24);game.view.targetTexture=r;game.view.Render();RenderTexture.active=r;var t=new Texture2D(1280,800,TextureFormat.RGB24,false);t.ReadPixels(new Rect(0,0,1280,800),0,0);t.Apply();File.WriteAllBytes(Path.Combine(folder,file),t.EncodeToPNG());game.view.targetTexture=null;RenderTexture.active=null;Destroy(r);Destroy(t);
        }
    }
}


