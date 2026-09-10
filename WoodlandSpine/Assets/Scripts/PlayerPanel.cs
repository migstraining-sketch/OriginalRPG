using UnityEngine;

namespace WoodlandSpine
{
    public sealed class PlayerPanel
    {
        readonly string[] tabs={"Inventory","Equipment","Character","Techniques","Journal"};
        int tab,journalTab;
        Vector2 scroll;
        public void Strip(SliceGame game,float width)
        {
            for(int i=0;i<tabs.Length;i++)if(GUI.Button(new Rect(width-615+i*119,12,115,30),tabs[i])){tab=i;game.showInventory=true;scroll=Vector2.zero;}
        }
        public void Draw(SliceGame game,float width,float height,GUIStyle title,GUIStyle body)
        {
            var bag=game.mode==GameMode.Combat?game.combat.inventory:game.inventory;
            GUILayout.BeginArea(new Rect(width/2-380,105,760,height-140),GUI.skin.box);
            GUILayout.Label(tabs[tab],title);scroll=GUILayout.BeginScrollView(scroll);
            if(tab==0)
            {
                foreach(var entry in InventoryItems.List(bag))GUILayout.Label(entry.name+" ×"+entry.Count(bag),body);
                GUILayout.Label("Coins "+game.full.progress.coins,body);
                if(game.mode!=GameMode.Combat)
                    foreach(var food in bag.provisions)
                        if(food.Value>0&&GUILayout.Button((game.full.progress.food==food.Key?"Selected for cooking: ":"Select for cooking: ")+food.Key))game.full.progress.food=food.Key;
                if(game.mode!=GameMode.Combat)
                {
                    GUI.enabled=game.hp<game.rules.playerHP&&bag.bandages>0;if(GUILayout.Button("Use a bandage"))game.UseBandage();
                    GUI.enabled=game.hp<game.rules.playerHP&&bag.healthPotions>0;if(GUILayout.Button("Drink a Health Potion"))game.UseHealthPotion();GUI.enabled=true;
                }
            }
            if(tab==1)
            {
                GUILayout.Label("Weapon: "+(bag.weapon?.title??"None"),body);
                GUILayout.Label("Body: "+(bag.body?.title??"None")+" • Armor "+bag.Armor,body);
                GUILayout.Label(game.mode==GameMode.Combat?"Changing weapons spends this combatant's Primary Action.":"Equipment can be changed freely here.",body);
                foreach(var weapon in bag.weapons)
                {
                    GUI.enabled=weapon!=bag.weapon&&(game.mode!=GameMode.Combat||game.combat.CanAct&&game.combat.primary);
                    if(GUILayout.Button("Equip "+weapon.Description)){if(game.mode==GameMode.Combat){game.combat.Equip(weapon);game.Refresh();}else bag.weapon=weapon;}
                }
                foreach(var armor in bag.bodies){GUI.enabled=game.mode!=GameMode.Combat&&armor!=bag.body;if(GUILayout.Button("Wear "+armor.title+" • Armor "+armor.armor))bag.body=armor;}
                GUI.enabled=true;
            }
            if(tab==2)
            {
                GUILayout.Label(game.playerName+" • HP "+game.hp+"/"+game.rules.playerHP+" • Armor "+game.inventory.Armor,body);
                GUILayout.Label("Movement 3 • one Primary Action • movement may be split",body);
                var party=game.coordinated.party;
                if(party.recruited)
                {
                    GUILayout.Label("Roster: Ily • "+(party.active?"Traveling with you":"Not in active party")+" • "+party.ily.hp+"/"+party.ily.maxHP+(party.ily.needsRest?" • Needs Basic Rest":""),body);
                    GUI.enabled=game.mode==GameMode.Exploration&&party.Present;
                    if(GUILayout.Button(party.active?"Ask Ily to wait here":"Travel with Ily")){party.active=!party.active;party.following=false;}
                    if(GUILayout.Button("Ily: "+party.ily.controller+" — change preference"))party.ily.controller=party.ily.controller==CombatController.Direct?CombatController.Independent:CombatController.Direct;
                    GUI.enabled=true;
                }
            }
            if(tab==3)
            {
                if(bag.weapon!=null){GUILayout.Label(bag.weapon.SignatureName,title);GUILayout.Label(bag.weapon.SignatureHint,body);}
                if(game.full.progress.huntingLearned)GUILayout.Label("Hunting learned",body);
                if(game.opening.state.potionMakingUnlocked)GUILayout.Label("Potion Making • Health Potion recipe learned",body);
                if(game.full.progress.cookingLearned)GUILayout.Label("Cooking • practice with Sylvie",body);
            }
            if(tab==4)
            {
                journalTab=GUILayout.Toolbar(journalTab,new[]{"Quests","Hunt Notes","Knowledge"});
                if(journalTab==0)
                {
                    GUILayout.Label(game.Objective,body);
                    if(game.opening.state.treatmentFailed)GUILayout.Label("Marlow's treatment — failed: no viable milk serving. Potion Making was not learned.",body);
                    if(game.opening.state.trollTreated)GUILayout.Label("Marlow's treatment — completed. The troll is eating again.",body);
                    if(game.coordinated.mud.state.Complete)GUILayout.Label("Mud in the Moonrice — "+(game.coordinated.mud.state.dead?"Cull and Harvest":"feeding redirected"),body);
                }
                if(journalTab==1)GUILayout.Label(game.coordinated.mud.Notes,body);
                if(journalTab==2)foreach(var note in game.coordinated.observations)GUILayout.Label(note,body);
            }
            GUILayout.EndScrollView();if(GUILayout.Button("Close [Esc]"))game.showInventory=false;GUILayout.EndArea();
        }
    }
}
