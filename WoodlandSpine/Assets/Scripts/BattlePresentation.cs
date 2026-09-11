using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    // Explicit bindings, populated by encounter setup. No scene searches in input or rendering.
    public sealed class BattlePresentation
    {
        public readonly Dictionary<Combatant,Transform> actors=new Dictionary<Combatant,Transform>();
        readonly SliceGame game;
        public string hover="";
        public BattlePresentation(SliceGame game){this.game=game;}
        public void Begin()
        {
            actors.Clear();actors.Add(game.combat.Hero,game.player.transform);
            actors.Add(game.combat.Target,game.site.actor);
        }
        public void Refresh()
        {
            foreach(var pair in actors)
            {
                var unit=pair.Key;var actor=pair.Value;
                if(unit==game.combat.Hero){game.player.Place(game.site.grid.World(unit.cell)+Vector3.up*.08f);continue;}
                actor.gameObject.SetActive(unit.Present);
                actor.position=game.site.grid.World(unit.cell)+Vector3.up*(unit.side==CombatSide.Allies?.9f:unit.enemy.mossback?.85f:.55f);
            }
        }
        public bool Pick(Vector3 screen,out Hex cell)
        {
            cell=default;if(!game.view.pixelRect.Contains(screen))return false;
            var ray=game.view.ScreenPointToRay(screen);float nearest=float.MaxValue;Combatant hit=null;
            foreach(var pair in actors)
            {
                if(!pair.Key.Present)continue;
                var renderer=pair.Value.GetComponent<Renderer>();
                if(renderer!=null&&renderer.bounds.IntersectRay(ray,out float distance)&&distance<nearest){nearest=distance;hit=pair.Key;}
            }
            if(hit!=null){cell=hit.cell;return true;}
            return CombatTargeting.Pick(game.view,game.site.grid,null,default,screen,out cell);
        }
        public void Pointer(bool clicked)
        {
            hover="";var c=game.combat;
            if(!Pick(Input.mousePosition,out Hex cell))return;
            var unit=c.At(cell);bool signature=game.selection.choice==CombatChoice.Signature;
            if(unit!=null&&unit.side==CombatSide.Enemies)
            {
                hover=unit.title+" • "+unit.hp+"/"+unit.maxHP+" HP • "+unit.Intent;
                if(game.selection.choice==CombatChoice.Attack||signature)
                {
                    string reason=c.InvalidReason(cell,signature);
                    hover+=" • "+(reason.Length>0?reason:CombatModel.Damage(signature?c.inventory.weapon.signatureDamage:c.inventory.weapon.damage,unit.Armor,unit.defending)+" damage");
                }
                if(clicked)
                {
                    c.SelectTarget(unit);
                    if(game.selection.choice==CombatChoice.Attack||signature)game.ConfirmSelection();
                }
            }
            else if(clicked&&unit!=null&&unit.side==CombatSide.Allies&&c.choosingAlly)game.ChooseAlly(unit);
            else if(clicked&&game.selection.choice==CombatChoice.Move&&!c.Move(cell))c.log="Choose a reachable hex. Mud costs 2; occupied and blocked hexes cannot be crossed.";
            if(clicked){game.Refresh();game.CheckResult();}
        }
    }
}
