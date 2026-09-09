using System;
using System.Collections;
using System.IO;
using UnityEngine;
namespace WoodlandSpine
{
    public sealed class CombatCameraSmoke : MonoBehaviour
    {
        public SliceGame game;
        int checks;
        void Check(bool valid,string message){if(!valid)throw new Exception(message);checks++;}
        IEnumerator Start()
        {
            yield return null;game.enabled=false;game.player.enabled=false;
            game.inventory.Receive(game.rules.weapons[1]);
            game.player.Place(game.world.wildlife.grid.World(new Hex(0,-4)));
            game.StartCombat(game.world.wildlife,game.rules.wildlife);
            string folder=Path.GetFullPath(Path.Combine(Application.dataPath,"../../..","Validation"));
            foreach(var screen in new[]{new Vector2Int(1100,510),new Vector2Int(1280,800),new Vector2Int(1920,1080)})
            {
                Screen.SetResolution(screen.x,screen.y,false);
                yield return new WaitForSeconds(.5f);yield return new WaitForEndOfFrame();
                Rect safe=CombatViewport.Pixels(Screen.width,Screen.height);
                Check(game.view.aspect<=1.801f,"camera aspect exceeds encounter bounds");
                foreach(Hex h in game.combat.grid.cells)foreach(float height in new[]{0f,3f})
                    Check(safe.Contains(game.view.WorldToScreenPoint(game.combat.grid.World(h)+Vector3.up*height)),"actor clipped by combat HUD");
                foreach(float x in new[]{-34f,34f})
                    Check(!safe.Contains(game.view.WorldToScreenPoint(game.combat.grid.origin+Vector3.right*x)),"neighboring site is in camera");
                game.Select(CombatChoice.Signature);yield return null;yield return new WaitForEndOfFrame();
                ScreenCapture.CaptureScreenshot(Path.Combine(folder,$"Combat-framing-{screen.x}x{screen.y}.png"));
                yield return new WaitForSeconds(.25f);
            }
            foreach(var weapon in game.rules.weapons)
            {
                game.inventory.Receive(weapon);game.mode=GameMode.Combat;game.combat.inventory=game.inventory;
                game.combat.BeginPlayer();game.combat.playerCell=new Hex(0,0);game.combat.enemyCell=weapon.geometry==WeaponGeometry.Ranged?new Hex(0,1):new Hex(0,2);game.combat.enemyHP=10;
                game.Select(CombatChoice.Signature);game.CancelSelection();Check(game.combat.primary&&game.combat.enemyHP==10,"cancel signature preserves action");
                game.Select(CombatChoice.Signature);game.ConfirmSelection();Check(game.combat.enemyHP==10-weapon.signatureDamage&&!game.combat.primary,"signature UI confirmation commits once");
                int hp=game.combat.enemyHP;game.ConfirmSelection();Check(game.combat.enemyHP==hp,"repeat confirmation cannot double signature");
            }
            game.mode=GameMode.Exploration;yield return null;yield return new WaitForEndOfFrame();
            Check(game.view.rect==new Rect(0,0,1,1),"exploration did not restore full viewport");
            File.WriteAllText(Path.Combine(folder,"combat-camera-result.txt"),$"PASS: {checks} actual-camera checks across three screen sizes; full-frame HUD screenshots captured; exploration viewport restored. Automated coverage, not a human mouse playtest.\n");
            Debug.Log("COMBAT_CAMERA_SMOKE_SUCCESS: "+checks);Application.Quit(0);
        }
    }
}


