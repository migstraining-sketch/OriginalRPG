using UnityEngine;

namespace WoodlandSpine
{
    public class SliceHUD : MonoBehaviour
    {
        public SliceGame game;
        GUIStyle body,title,small,button;
        Vector2 dialogueScroll;
        DialogueSession lastDialogue;
        readonly PlayerPanel playerPanel=new PlayerPanel(); bool objectiveOpen=true;
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
            if(game.coordinated.travel.Blocking){game.coordinated.travel.Draw(width,height);return;}
            if(game.coordinated.storage.visible){game.coordinated.storage.Draw(game.inventory,width,height);return;}
            GUI.Box(new Rect(16,12,320,60),"");
            GUI.Label(new Rect(26,16,300,25),game.playerName+" • HP "+game.hp+"/"+game.rules.playerHP+" • Armor "+game.inventory.Armor,body);
            Color previous=GUI.color;GUI.color=new Color(.64f,.25f,.2f);GUI.DrawTexture(new Rect(26,47,290*Mathf.Clamp01((float)game.hp/game.rules.playerHP),12),Texture2D.whiteTexture);GUI.color=previous;
            if(game.mode==GameMode.Exploration||game.mode==GameMode.Combat)playerPanel.Strip(game,width);
            if(game.mode==GameMode.Exploration&&!game.showInventory&&!string.IsNullOrEmpty(game.Objective))
            {
                if(GUI.Button(new Rect(width-420,52,400,27),objectiveOpen?"Current intention ▾":"Current intention ▸"))objectiveOpen=!objectiveOpen;
                if(objectiveOpen)GUI.Label(new Rect(width-420,82,400,90),game.Objective,small);
            }
            if(game.mode==GameMode.Brewing){DrawBrewing(width,height);return;}
            if(game.mode==GameMode.Cooking){DrawCooking(width,height);return;}
            if(game.mode==GameMode.Dialogue)
            {
                if(lastDialogue!=game.dialogue){dialogueScroll=Vector2.zero;lastDialogue=game.dialogue;}
                float panel=Mathf.Min(height*.44f,Mathf.Max(230,body.CalcHeight(new GUIContent(game.dialogue.text),width*.7f-40)+game.dialogue.choices.Count*46+115));
                GUILayout.BeginArea(new Rect(width*.15f,height-panel-15,width*.7f,panel),GUI.skin.box);
                dialogueScroll=GUILayout.BeginScrollView(dialogueScroll);
                GUILayout.Label(game.dialogue.speaker,title);GUILayout.Label(game.dialogue.text,body);GUILayout.Space(12);
                if(game.enteringName)
                {
                    GUI.SetNextControlName("Player name");game.nameDraft=GUILayout.TextField(game.nameDraft,24,GUILayout.Height(34));
                    if(Button("Confirm name",!string.IsNullOrWhiteSpace(game.nameDraft)))game.SubmitPlayerName(game.nameDraft);
                }
                var choices=game.dialogue.choices.ToArray();
                foreach(var choice in choices)if(choice.visible==null||choice.visible())if(Button(choice.label)){choice.choose();break;}
                if(game.dialogue!=null&&game.dialogue.continueAction!=null)if(Button("Continue  [Space]"))game.dialogue.continueAction();
                GUILayout.Label("Choose a response. Esc does not make or hide a decision.",small);
                GUILayout.EndScrollView();GUILayout.EndArea();return;
            }
            if(game.mode==GameMode.Defeated)
            {
                GUILayout.BeginArea(new Rect(width/2-240,height/2-90,480,180),GUI.skin.box);GUILayout.Label("You fell",title);GUILayout.Label(game.notice,body);if(Button("Retry encounter"))game.Retry();GUILayout.EndArea();return;
            }
            if(game.showInventory){playerPanel.Draw(game,width,height,title,body);return;}
            if(game.mode==GameMode.Combat){BattleHUD.Draw(game,width,height,body,small);return;}
            GUILayout.BeginArea(new Rect(16,height-76,width-32,61),GUI.skin.box);
            GUILayout.Label(Time.time<game.noticeUntil||game.nearby==null?game.notice:"[E] "+game.nearby.caption,body);
            GUILayout.Label("WASD move • E interact • I player panel",small);
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
                if(Button(i>0?"Unavailable in this development slice":h.rewarded?"Resolved":h.accepted?"Current posting":"Take this posting",i==0&&!h.rewarded))game.full.AcceptHunt(i);
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







