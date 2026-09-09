using UnityEngine;

namespace WoodlandSpine
{
    public class SliceHUD : MonoBehaviour
    {
        public SliceGame game;
        GUIStyle body,title,small,button;
        Vector2 dialogueScroll;
        DialogueSession lastDialogue;
        void Init()
        {
            body=new GUIStyle(GUI.skin.label){fontSize=17,wordWrap=true};title=new GUIStyle(body){fontSize=22,fontStyle=FontStyle.Bold};small=new GUIStyle(body){fontSize=14};
            button=new GUIStyle(GUI.skin.button){fontSize=16,wordWrap=true};GUI.skin.button=button;
        }
        bool Button(string text,bool enabled=true){GUI.enabled=enabled;bool result=GUILayout.Button(text,GUILayout.MinHeight(34));GUI.enabled=true;return result;}
        void OnGUI()
        {
            if(game==null||game.world==null)return;if(body==null)Init();
            float scale=Mathf.Min(Screen.width/1280f,Screen.height/800f);GUI.matrix=Matrix4x4.Scale(new Vector3(scale,scale,1));
            float width=Screen.width/scale,height=Screen.height/scale;
            if(game.full.boardVisible){DrawBoard(width,height);return;}
            if(game.mode==GameMode.Combat||!string.IsNullOrEmpty(game.Objective)||game.showInventory)
            {
            GUILayout.BeginArea(new Rect(16,12,width-32,game.mode==GameMode.Combat?80:140),GUI.skin.box);
            GUILayout.Label($"{game.playerName}   •   HP {game.hp}/{game.rules.playerHP}   •   Armor {game.inventory.Armor}   •   {game.inventory.weapon?.title??"Unarmed"}",title);
            if(game.mode!=GameMode.Combat)GUILayout.Label(game.Objective,body);
            if(game.mode==GameMode.Combat)GUILayout.Label($"{game.combat.phase} Phase   |   Movement {game.combat.movement}   |   Primary Action: {(game.combat.primary?"ready":"spent")}   |   {game.combat.enemy.title} {game.combat.enemyHP} HP",body);
            else GUILayout.Label("WASD move   •   E interact   •   I inventory   •   Esc close",small);
            GUILayout.EndArea();
            }
            if(game.mode==GameMode.Brewing){DrawBrewing(width,height);return;}
            if(game.mode==GameMode.Cooking){DrawCooking(width,height);return;}
            if(game.mode==GameMode.Dialogue)
            {
                if(lastDialogue!=game.dialogue){dialogueScroll=Vector2.zero;lastDialogue=game.dialogue;}
                float panel=Mathf.Min(height-170,Mathf.Max(230,body.CalcHeight(new GUIContent(game.dialogue.text),width*.7f-40)+game.dialogue.choices.Count*46+115));
                GUILayout.BeginArea(new Rect(width*.15f,height-panel-15,width*.7f,panel),GUI.skin.box);
                dialogueScroll=GUILayout.BeginScrollView(dialogueScroll);
                GUILayout.Label(game.dialogue.speaker,title);GUILayout.Label(game.dialogue.text,body);GUILayout.Space(12);
                var choices=game.dialogue.choices.ToArray();
                foreach(var choice in choices)if(choice.visible==null||choice.visible())if(Button(choice.label)){choice.choose();break;}
                if(game.dialogue!=null&&game.dialogue.continueAction!=null)if(Button("Continue  [Space]"))game.dialogue.continueAction();
                GUILayout.Label("Esc — return to the room. Conversations remember where you stopped.",small);
                GUILayout.EndScrollView();GUILayout.EndArea();return;
            }
            if(game.mode==GameMode.Defeated)
            {
                GUILayout.BeginArea(new Rect(width/2-240,height/2-90,480,180),GUI.skin.box);GUILayout.Label("You fell",title);GUILayout.Label(game.notice,body);if(Button("Retry encounter"))game.Retry();GUILayout.EndArea();return;
            }
            if(game.showInventory)
            {
                GUILayout.BeginArea(new Rect(width/2-260,160,520,height-185),GUI.skin.box);
                GUILayout.Label("Equipment & inventory",title);GUILayout.Label($"Body: {game.inventory.body.title} • Armor {game.inventory.Armor}",body);
                var p=game.full.progress;
                GUILayout.Label($"Coins {p.coins}"+(p.wellFed?" • Well Fed":""),body);
                foreach(var food in p.provisions)if(food.Value>0)if(Button(food.Key+" ×"+food.Value+(p.food==food.Key?" • selected":""),p.food!=food.Key))p.food=food.Key;
                if(p.huntingLearned)GUILayout.Label("Hunting learned",small);
                if(p.cookingLearned)GUILayout.Label("Cooking learned • practise in Sylvie's kitchen",small);
                if(game.opening.state.questAccepted)GUILayout.Label($"Bloodleaf {game.inventory.bloodleaf} • Silvermoss {game.inventory.silvermoss} • Milk {game.inventory.mooncalfMilk}",small);
                if(game.inventory.experimentalPotion>0)GUILayout.Label("Experimental potion: reserved for the troll",small);
                if(game.opening.state.potionMakingUnlocked)GUILayout.Label("Potion Making unlocked • Health Potion recipe learned",body);
                GUILayout.Label(game.mode==GameMode.Combat?"Equipping another weapon spends your Primary Action.":"Carried equipment",small);
                foreach(var weapon in game.inventory.weapons)
                {
                    bool active=weapon==game.inventory.weapon;
                    if(Button((active?"Equipped: ":"Equip: ")+weapon.Description,!active&&(game.mode!=GameMode.Combat||game.combat.primary&&game.combat.phase==Phase.Player)))
                    {if(game.mode==GameMode.Combat){game.combat.Equip(weapon);game.selection.Cancel();game.Refresh();}else game.inventory.weapon=weapon;}
                }
                if(Button($"Bandage ×{game.inventory.bandages} • heal {game.rules.bandageHeal}",game.inventory.bandages>0&&game.hp<game.rules.playerHP&&(game.mode!=GameMode.Combat||game.combat.primary&&game.combat.phase==Phase.Player)))
                {if(game.mode==GameMode.Combat)game.UseCombatItem();else game.UseBandage();}
                if(Button($"Health Potion ×{game.inventory.healthPotions} • heal 12",game.inventory.healthPotions>0&&game.hp<game.rules.playerHP&&(game.mode!=GameMode.Combat||game.combat.primary&&game.combat.phase==Phase.Player)))
                {if(game.mode==GameMode.Combat)game.UseCombatItem(true);else game.UseHealthPotion();}
                if(Button("Close"))game.showInventory=false;GUILayout.EndArea();return;
            }
            bool battle=game.mode==GameMode.Combat;
            GUILayout.BeginArea(new Rect(16,height-(battle?205:76),width-32,battle?190:61),GUI.skin.box);
            if(game.mode==GameMode.Combat)
            {
                var c=game.combat;GUILayout.Label(c.Intent,body);GUILayout.Label(c.log,small);
                bool turn=c.phase==Phase.Player;
                GUILayout.BeginHorizontal();
                if(Button("Move [M]",turn))game.Select(CombatChoice.Move);
                if(Button("Attack [1]",turn&&c.primary))game.Select(CombatChoice.Attack);
                if(Button("Defend [2]",turn&&c.primary))game.Select(CombatChoice.Defend);
                if(Button("Item [3]",turn&&c.primary))game.Select(CombatChoice.Item);
                if(Button("Dash [4]",turn&&c.primary))game.Select(CombatChoice.Dash);
                if(Button("Flee",turn&&c.primary&&c.playerCell.Distance(new Hex())==c.grid.radius)){c.Flee();game.CheckResult();}
                if(Button("End turn [Space]",turn))game.EndTurn();GUILayout.EndHorizontal();
                if(game.selection.choice==CombatChoice.Item)
                {
                    GUILayout.BeginHorizontal();
                    if(Button($"Use bandage ×{game.inventory.bandages}",turn&&c.primary&&game.inventory.bandages>0&&game.hp<game.rules.playerHP))game.UseCombatItem();
                    if(Button($"Use Health Potion ×{game.inventory.healthPotions}",turn&&c.primary&&game.inventory.healthPotions>0&&game.hp<game.rules.playerHP))game.UseCombatItem(true);
                    GUILayout.EndHorizontal();
                }
                if(game.selection.choice==CombatChoice.Dash||game.selection.choice==CombatChoice.Defend)
                    if(Button($"Confirm {game.selection.choice} [Enter] — spends Primary Action",turn&&c.primary))game.ConfirmSelection();
                if(game.selection.choice==CombatChoice.Attack)
                    if(Button($"Attack {c.enemy.title} [Enter]",turn&&c.primary&&c.grid.CanAttack(c.inventory.weapon,c.playerCell,c.enemyCell)))game.ConfirmSelection();
                GUILayout.Label(game.selection.choice==CombatChoice.Attack?$"Click enemy in red range • damage {CombatModel.Damage(game.inventory.weapon.damage,c.enemy.armor)}":game.selection.choice==CombatChoice.Move?"Click blue hex • brown costs 2 • amber charge lane":"Choose an action, then confirm it when ready.",small);
                GUILayout.Label("Esc / right-click: cancel selection • committed movement and actions stay spent",small);
            }
            else {GUILayout.Label(Time.time<game.noticeUntil||game.nearby==null?game.notice:"[E] "+game.nearby.caption,body);GUILayout.Label("WASD move   •   E interact   •   I inventory",small);}
            GUILayout.EndArea();
        }
        void DrawBrewing(float width,float height)
        {
            var b=game.opening.brew;
            GUILayout.BeginArea(new Rect(width/2-310,165,620,height-185),GUI.skin.box);
            GUILayout.Label("Supervised preparation",title);GUILayout.Label(b.Instruction,body);GUILayout.Space(12);
            switch(b.step)
            {
                case BrewStep.Bloodleaf:
                    if(Button("Separate leaf tips from tough vein"))game.opening.BrewAction("separate");
                    if(Button("Drop the whole cutting in"))game.opening.BrewAction("whole");break;
                case BrewStep.Silvermoss:
                    if(Button("Brush away grit and bruise the moss"))game.opening.BrewAction("brush");
                    if(Button("Leave the grit in"))game.opening.BrewAction("grit");break;
                case BrewStep.Milk:
                    GUILayout.Label($"Measure: {b.milkMeasure:0.00} marks • target 1.00",body);
                    b.milkMeasure=GUILayout.HorizontalSlider(b.milkMeasure,0,2);
                    if(Button("Pour the measured milk into a separate cup"))game.opening.BrewAction("measure");break;
                case BrewStep.CombineLeaf:case BrewStep.CombineMoss:case BrewStep.CombineMilk:
                    if(Button("Add prepared Bloodleaf"))game.opening.BrewAction("leaf");
                    if(Button("Fold in Silvermoss"))game.opening.BrewAction("moss");
                    if(Button("Slowly add measured milk"))game.opening.BrewAction("milk");break;
                case BrewStep.HeatAndStir:
                    GUILayout.Label($"Heat: {b.heat:0.00} • gentle band 0.35–0.55 • stirs {b.steadyStirs}/3",body);
                    b.heat=GUILayout.HorizontalSlider(b.heat,0,1);
                    if(Button("Make one steady stir"))game.opening.BrewAction("stir");break;
                case BrewStep.Finish:
                    if(Button("Remove from heat and decant two doses"))game.opening.BrewAction("decant");break;
            }
            GUILayout.Space(14);GUILayout.Label(b.feedback,small);GUILayout.Space(12);
            if(Button("Step away [Esc] — keep this preparation"))game.mode=GameMode.Exploration;
            GUILayout.EndArea();
        }
        void DrawBoard(float width,float height)
        {
            GUILayout.BeginArea(new Rect(35,100,width-70,height-150),GUI.skin.box);GUILayout.Label("Local work",title);GUILayout.Space(15);GUILayout.BeginHorizontal();
            for(int i=0;i<3;i++)
            {
                var d=game.full.definitions[i];var h=game.full.progress.hunts[i];
                GUILayout.BeginVertical(GUI.skin.box,GUILayout.Width((width-100)/3));GUILayout.Label(d.title,title);GUILayout.Label(d.client,body);GUILayout.Label(d.location,small);GUILayout.Space(15);GUILayout.Label(d.problem,body);GUILayout.Space(20);GUILayout.Label(game.full.progress.huntReward+" coins + provisions",body);
                if(Button(h.rewarded?"Resolved":h.accepted?"Current posting":"Take this posting",!h.rewarded))game.full.AcceptHunt(i);
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();GUILayout.Space(20);if(Button("Leave the board [Esc]"))game.full.boardVisible=false;GUILayout.EndArea();
        }
        void DrawCooking(float width,float height)
        {
            var c=game.full.cooking;
            GUILayout.BeginArea(new Rect(width/2-310,170,620,height-195),GUI.skin.box);
            GUILayout.Label(c.practice?"Your preparation — Sylvie supervises":"Sylvie's demonstration",title);GUILayout.Label(c.ingredient,body);GUILayout.Label(c.step.ToString(),small);GUILayout.Space(15);GUILayout.Label(c.Instruction,body);
            if(c.step==CookStep.Cook){GUILayout.Label($"Heat: {c.heat:0.00}",body);c.heat=GUILayout.HorizontalSlider(c.heat,0,1);}
            GUILayout.Space(20);if(Button(c.practice?"Perform this step":"Follow Sylvie's next step"))game.full.CookAction();GUILayout.Label(c.feedback,small);
            if(Button("Step away — keep this preparation"))game.mode=GameMode.Exploration;GUILayout.EndArea();
        }
    }
}

