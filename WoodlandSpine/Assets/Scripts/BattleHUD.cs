using UnityEngine;

namespace WoodlandSpine
{
    public static class BattleHUD
    {
        static bool Button(string text,bool enabled=true)
        {GUI.enabled=enabled;bool result=GUILayout.Button(text,GUILayout.Height(32));GUI.enabled=true;return result;}
        public static void Draw(SliceGame game,float width,float height,GUIStyle body,GUIStyle small)
        {
            var c=game.combat;
            GUILayout.BeginArea(new Rect(16,height-205,width-32,190),GUI.skin.box);
            GUILayout.BeginHorizontal();
            foreach(var unit in c.units)if(unit.side==CombatSide.Allies)
            {
                string state=!unit.Present?"Defeated":unit==c.Current&&!c.choosingAlly?"Active":unit.spent?"Spent":"Ready";
                if(Button($"{unit.title} {unit.hp}/{unit.maxHP} • {state} • {unit.controller}",c.choosingAlly&&unit.Present&&!unit.spent))game.ChooseAlly(unit);
            }
            GUILayout.EndHorizontal();
            if(c.choosingAlly)GUILayout.Label("Choose which Ready ally acts.",body);
            else GUILayout.Label(c.phase==Phase.Enemy?$"Round {c.round} • {c.Current?.title} is acting":$"Round {c.round} • {c.Active?.title} • Move {c.movement} • Primary {(c.primary?"ready":"spent")}",small);
            GUILayout.BeginHorizontal();bool turn=c.CanAct;
            if(Button("Move [M]",turn))game.Select(CombatChoice.Move);
            if(Button("Attack [1]",turn&&c.primary))game.Select(CombatChoice.Attack);
            if(Button((c.inventory.weapon?.SignatureName??"Technique")+" [5]",turn&&c.primary))game.Select(CombatChoice.Signature);
            if(Button("Defend [2]",turn&&c.primary))game.Select(CombatChoice.Defend);
            if(Button("Item [3]",turn&&c.primary))game.Select(CombatChoice.Item);
            if(Button("Dash [4]",turn&&c.primary))game.Select(CombatChoice.Dash);
            if(Button("Flee",turn&&c.Active==c.Hero&&c.primary&&c.Hero.cell.Distance(new Hex())==c.grid.radius)){c.Flee();game.CheckResult();}
            if(Button("End [Space]",turn))game.EndTurn();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if(game.selection.choice==CombatChoice.Item)
            {
                if(Button($"Bandage ×{c.inventory.bandages}",turn&&c.primary&&c.inventory.bandages>0&&c.playerHP<c.Active.maxHP))game.UseCombatItem();
                if(Button($"Potion ×{c.inventory.healthPotions}",turn&&c.primary&&c.inventory.healthPotions>0&&c.playerHP<c.Active.maxHP))game.UseCombatItem(true);
            }
            else if(game.selection.choice==CombatChoice.Defend||game.selection.choice==CombatChoice.Dash)
            {if(Button("Confirm "+game.selection.choice+" [Enter]",turn&&c.primary))game.ConfirmSelection();}
            else if(game.selection.choice==CombatChoice.Attack||game.selection.choice==CombatChoice.Signature)
            {
                bool sig=game.selection.choice==CombatChoice.Signature;string reason=c.InvalidReason(c.Target.cell,sig);
                if(Button(c.Target.title+" • "+(reason.Length>0?reason:c.DamagePreview(sig)+" damage [Enter]"),c.Target.Present&&reason.Length==0))game.ConfirmSelection();
            }
            else GUILayout.Label(c.log,small);
            GUILayout.EndHorizontal();
            GUILayout.Label(string.IsNullOrEmpty(game.battle.hover)?"Click a unit to focus • Amber marks every committed threat • Esc cancels selection":game.battle.hover,small);
            GUILayout.EndArea();
            // Compact labels are informational and do not intercept battlefield clicks.
            foreach(var unit in c.units)if(unit.side==CombatSide.Enemies&&unit.Present)
            {
                Vector3 p=game.view.WorldToScreenPoint(c.grid.World(unit.cell)+Vector3.up*2.2f);
                float scale=Mathf.Min(Screen.width/1280f,Screen.height/800f);
                GUI.Label(new Rect(p.x/scale-90,(Screen.height-p.y)/scale-25,180,55),$"{unit.title} {unit.hp} HP\n{unit.Intent}",small);
            }
        }
    }
}
