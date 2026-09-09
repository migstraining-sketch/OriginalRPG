using System;
using UnityEngine;
namespace WoodlandSpine.Editor
{
    public static class CombatViewportValidation
    {
        public static void Run()
        {
            int checks=0;var grid=EncounterLayout.Create(Vector3.zero,true);var rotation=Quaternion.LookRotation(new Vector3(0,-22,14));
            foreach(var screen in new[]{new Vector2(1280,800),new Vector2(1920,1080),new Vector2(1024,768),new Vector2(800,1280),new Vector2(2560,1080)})
            {
                Rect rect=CombatViewport.Pixels(screen.x,screen.y);float scale=Mathf.Min(screen.x/1280,screen.y/800);
                if(rect.yMin<=205*scale||rect.yMax>=screen.y-152*scale)throw new Exception("Battle viewport overlaps HUD");checks++;
                float size=CombatViewport.Size(grid,rotation,rect.width/rect.height);
                foreach(Hex cell in grid.cells)foreach(float elevation in new[]{0f,3f})
                {
                    Vector3 projected=Quaternion.Inverse(rotation)*(grid.World(cell)+Vector3.up*elevation);
                    if(Mathf.Abs(projected.y)>=size||Mathf.Abs(projected.x)>=size*rect.width/rect.height)throw new Exception("Battlefield target clipped");checks++;
                }
            }
            Debug.Log("COMBAT_VIEWPORT_VALIDATION_PASSED: "+checks+" assertions");
        }
    }
}
