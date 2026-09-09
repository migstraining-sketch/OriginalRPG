using System.Collections;
using UnityEngine;

namespace WoodlandSpine
{
    public class SliceGame : MonoBehaviour
    {
        public SliceData rules;
        public GameMode mode=GameMode.Exploration;
        public Inventory inventory;
        public Explorer player;
        [System.NonSerialized] public WorldBuilder world;
        [System.NonSerialized] public EncounterSite site;
        [System.NonSerialized] public CombatModel combat;
        [System.NonSerialized] public DialogueSession dialogue;
        public Interaction nearby;
        public bool jobAccepted, returned, showInventory;
        public MarlowOpening opening;
        public ReactiveIntro intro;
        public FirstLabVisit firstLab;
        public FullOpening full;
        public Transform playerVisual;
        [System.NonSerialized] public CombatSelection selection=new CombatSelection();
        public int hp;
        public string notice="";
        public float noticeUntil;
        public string playerName="Traveller";
        public Color coatColor=new Color(.35f,.53f,.7f);
        float retreatUntil;
        int checkpointHP, checkpointBandages, checkpointPotions;
        Hex checkpointEnemy;
        Vector3 checkpoint;
        public Camera view;
        public string Objective => full?.Objective??(opening==null?"":opening.Objective);
        void Start()
        {
            if(rules==null)rules=Resources.Load<SliceData>("SliceRules");
            world=new WorldBuilder{shader=rules.placeholderShader};world.Build();hp=rules.playerHP;
            inventory=new Inventory{body=rules.coat,bandages=rules.startingBandages};inventory.Store(rules.coat);
            var actor=new GameObject("Player");actor.AddComponent<CharacterController>();player=actor.AddComponent<Explorer>();player.Initialize(this);
            var visual=world.Shape("Player coat",new Vector3(0,.9f,0),new Vector3(.65f,.9f,.65f),coatColor,PrimitiveType.Capsule,false);
            visual.transform.SetParent(actor.transform,false);visual.transform.localPosition=new Vector3(0,.9f,0);
            playerVisual=visual.transform;
            visual.layer=10;
            player.Place(new Vector3(0,.1f,-3));
            var cameraObject=new GameObject("Main Camera");cameraObject.tag="MainCamera";view=cameraObject.AddComponent<Camera>();view.orthographic=true;view.nearClipPlane=.1f;view.farClipPlane=150;view.backgroundColor=new Color(.12f,.16f,.19f);view.clearFlags=CameraClearFlags.SolidColor;
            cameraObject.transform.position=new Vector3(0,13,-13);cameraObject.AddComponent<SliceCamera>().game=this;
            cameraObject.AddComponent<AudioListener>();
            var backdrop=new GameObject("Camera background").AddComponent<Camera>();backdrop.depth=-100;backdrop.cullingMask=0;backdrop.clearFlags=CameraClearFlags.SolidColor;backdrop.backgroundColor=view.backgroundColor;
            opening=gameObject.AddComponent<MarlowOpening>();opening.Initialize(this);
            intro=gameObject.AddComponent<ReactiveIntro>();intro.Initialize(this);
            firstLab=gameObject.AddComponent<FirstLabVisit>();firstLab.game=this;
            full=gameObject.AddComponent<FullOpening>();full.Initialize(this);
            gameObject.AddComponent<SliceHUD>().game=this;
            var smokeArgs=System.Environment.GetCommandLineArgs();
            if(System.Array.IndexOf(smokeArgs,"--combat-camera-smoke")>=0)gameObject.AddComponent<CombatCameraSmoke>().game=this;
            if(System.Array.IndexOf(smokeArgs,"--opening-smoke")>=0||System.Array.IndexOf(smokeArgs,"--slice-smoke")>=0||System.Array.IndexOf(smokeArgs,"--intro-smoke")>=0)gameObject.AddComponent<FullOpeningSmoke>().game=this;
        }
        void Update()
        {
            if(player==null)return;
            intro.Tick(Time.deltaTime);
            firstLab.Tick(Time.deltaTime);full.Tick(Time.deltaTime);
            if(mode==GameMode.Dialogue&&dialogue.continueAction!=null&&(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Return))){dialogue.continueAction();return;}
            if(Input.GetKeyDown(KeyCode.Escape)||Input.GetMouseButtonDown(1))
            {
                if(mode==GameMode.Combat)CancelSelection();
                else if(mode==GameMode.Dialogue)CloseDialogue();
                else if(mode==GameMode.Brewing||mode==GameMode.Cooking)mode=GameMode.Exploration;
                full.boardVisible=false;
                showInventory=false;return;
            }
            if(Input.GetKeyDown(KeyCode.I)&&(mode==GameMode.Exploration||mode==GameMode.Combat))showInventory=!showInventory;
            if(mode==GameMode.Exploration)
            {
                if(full.boardVisible)return;
                nearby=null;float closest=2.4f;
                foreach(var i in world.interactions){if(!i.gameObject.activeInHierarchy)continue;float d=Vector3.Distance(player.transform.position,i.transform.position);if(d<closest){nearby=i;closest=d;}}
                if(Input.GetKeyDown(KeyCode.E)&&nearby!=null&&!showInventory){Interact(nearby.key);return;}
                if(showInventory||opening.inLab)return;
                Vector3 p=player.transform.position;
                // Leaving is unrestricted. The retained combat scaffold activates only for an equipped expedition.
                if(Time.time>retreatUntil&&opening.state.questAccepted&&inventory.weapon!=null)
                {
                    if(!world.wildlife.cleared&&Mathf.Abs(p.x)<12&&p.z>25&&p.z<46)StartCombat(world.wildlife,rules.wildlife);
                    else opening.TickExploration(Time.deltaTime);
                }
            }
            else if(mode==GameMode.Combat&&combat.phase==Phase.Player&&!showInventory)
            {
                if(Input.GetKeyDown(KeyCode.Alpha1))Select(CombatChoice.Attack);
                if(Input.GetKeyDown(KeyCode.M))Select(CombatChoice.Move);
                if(Input.GetKeyDown(KeyCode.Alpha2))Select(CombatChoice.Defend);
                if(Input.GetKeyDown(KeyCode.Alpha3))Select(CombatChoice.Item);
                if(Input.GetKeyDown(KeyCode.Alpha4))Select(CombatChoice.Dash);
                if(Input.GetKeyDown(KeyCode.Return))ConfirmSelection();
                if(Input.GetKeyDown(KeyCode.Space))EndTurn();
                if(Input.GetMouseButtonDown(0)&&CombatViewport.Pixels(Screen.width,Screen.height).Contains(Input.mousePosition))
                {
                    Ray ray=view.ScreenPointToRay(Input.mousePosition);var plane=new Plane(Vector3.up,Vector3.zero);
                    if(plane.Raycast(ray,out float distance))
                    {
                        Hex h=site.grid.At(ray.GetPoint(distance));
                        if(selection.choice==CombatChoice.Attack){if(h.Equals(combat.enemyCell)){if(!combat.Attack())combat.log="Target is outside your weapon geometry or your action is spent.";else selection.Cancel();}}
                        else if(selection.choice==CombatChoice.Move){if(!combat.Move(h))combat.log="Choose a blue reachable hex. Mud costs 2; units and trees block movement.";}
                        Refresh();CheckResult();
                    }
                }
            }
        }
        public void Talk(string speaker,string text,params DialogueChoice[] choices){dialogue=new DialogueSession(speaker,text,choices);mode=GameMode.Dialogue;showInventory=false;}
        public void Exchange(string speaker,string text,System.Action next){Talk(speaker,text);dialogue.continueAction=next;}
        public void CloseDialogue(){dialogue=null;mode=GameMode.Exploration;}
        DialogueChoice Done(string title="Leave conversation")=>new DialogueChoice(title,CloseDialogue);
        public void Interact(string key)
        {
            if(full!=null&&full.Handle(key))return;
            if(firstLab!=null&&firstLab.Handle(key))return;
            if(opening.Interact(key))return;
            switch(key)
            {
                case "kitchen":Talk("Garrick","Kitchen's through there. Your business isn't. Unless you've learned to wash dishes without breaking them.",Done());break;
                case "upstairs":Talk("Garrick","Rooms aren't free. We'll discuss one when you're staying. For now, common room's yours.",Done());break;
            }
        }
        public void Loan()
        {
            var choices=new System.Collections.Generic.List<DialogueChoice>();
            foreach(var weapon in rules.weapons)
            {
                WeaponData selected=weapon;
                choices.Add(new DialogueChoice(weapon.Description,()=>{inventory.Receive(selected);opening.state.metGarrick=true;Talk("Garrick","A loan. Try bringing yourself back with it. Woods have enough bones.",Done("Thanks"));}));
            }
            choices.Add(Done("Not yet"));
            Talk("Garrick","Hold up. You're not going into those woods empty-handed. Pick one. Sword gets close; spear keeps teeth back; bow needs room.",choices.ToArray());
        }
        public void StartCombat(EncounterSite encounter,EnemyData enemy,Hex? foe=null)
        {
            site=encounter;checkpoint=player.transform.position;checkpointHP=hp;checkpointBandages=inventory.bandages;checkpointPotions=inventory.healthPotions;checkpointEnemy=foe??site.start;
            Hex start=site.grid.NearestOpen(player.transform.position,checkpointEnemy);
            combat=new CombatModel(site.grid,rules,inventory,enemy,start,checkpointEnemy,hp);mode=GameMode.Combat;selection.Cancel();showInventory=false;
            if(full!=null)full.progress.combatSeen=true;
            notice=enemy.mossback?"Normally docile. This one pursues you. Use its locked charge lane and the trees.":"The nearby trail becomes the battlefield. Your exploration position sets your starting hex.";
            Refresh();
        }
        public void Refresh()
        {
            if(combat==null)return;
            player.Place(site.grid.World(combat.playerCell)+Vector3.up*.08f);
            site.actor.position=site.grid.World(combat.enemyCell)+Vector3.up*(combat.enemy.mossback?.85f:.55f);
            hp=combat.playerHP;world.ShowGrid(site,combat,selection.choice==CombatChoice.Attack,selection.choice==CombatChoice.Move);
        }
        public void Select(CombatChoice choice){if(mode==GameMode.Combat&&combat.phase==Phase.Player){selection.Select(choice);Refresh();}}
        public void CancelSelection(){selection.Cancel();showInventory=false;if(mode==GameMode.Combat)Refresh();}
        public void ConfirmSelection()
        {
            if(mode!=GameMode.Combat||combat.phase!=Phase.Player)return;
            bool committed=selection.choice==CombatChoice.Attack?combat.Attack():selection.choice==CombatChoice.Dash?combat.Dash():selection.choice==CombatChoice.Defend&&combat.Defend();
            if(committed)selection.Cancel();Refresh();CheckResult();
        }
        public void UseCombatItem(bool potion=false){if(combat.Item(potion))selection.Cancel();Refresh();}
        public void EndTurn(){if(mode==GameMode.Combat&&combat.phase==Phase.Player){selection.Cancel();combat.EndPlayer();Refresh();StartCoroutine(EnemyPhase());}}
        IEnumerator EnemyPhase(){yield return new WaitForSeconds(.65f);combat.ResolveEnemy();Refresh();CheckResult();}
        public void CheckResult()
        {
            if(combat.phase==Phase.Won)
            {
                site.cleared=true;site.actor.gameObject.SetActive(false);world.HideGrid(site);mode=GameMode.Exploration;
                if(site==world.mossback){opening.state.mossbackDefeated=true;opening.state.mossbackSurvived=true;}
                if(full!=null&&full.CombatEnded(site,combat.phase))return;
                notice=site==world.mossback?"Mossback defeated. Finish gathering, then return to Marlow's lab.":"Wildlife defeated. Look for Silvermoss by shaded stone and the Mooncalf beyond. The trail continues north.";
            }
            else if(combat.phase==Phase.Lost){if(full!=null&&full.CombatEnded(site,combat.phase))return;mode=GameMode.Defeated;notice="You fell. Retry restores your state from immediately before this encounter.";}
            else if(combat.phase==Phase.Fled){if(site==world.mossback)opening.state.mossbackSurvived=true;world.HideGrid(site);mode=GameMode.Exploration;player.Place(new Vector3(0,.1f,site.grid.origin.z-11));retreatUntil=Time.time+2;site.actor.position=site.grid.World(site.start)+Vector3.up*(site==world.mossback?.85f:.55f);notice="Retreated. Approach again when ready; the encounter resets.";}
        }
        public void Retry(){hp=checkpointHP;inventory.bandages=checkpointBandages;inventory.healthPotions=checkpointPotions;player.Place(checkpoint);StartCombat(site,combat.enemy,checkpointEnemy);}
        public void UseBandage(){if(hp<rules.playerHP&&inventory.bandages>0){hp=Mathf.Min(rules.playerHP,hp+rules.bandageHeal);inventory.bandages--;}}
        public void UseHealthPotion(){if(hp<rules.playerHP&&inventory.healthPotions>0){hp=Mathf.Min(rules.playerHP,hp+12);inventory.healthPotions--;}}
    }
}


