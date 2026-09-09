using System;
using UnityEngine;
namespace WoodlandSpine.Editor
{
    public static class CombatViewportValidation
    {
        public static void Run()
        {
            int checks=0;var grid=EncounterLayout.Create(Vector3.zero,true);var rotation=Quaternion.LookRotation(new Vector3(0,-22,14));
            foreach(var screen in new[]{new Vector2(1280,800),new Vector2(1920,1080),new Vector2(1024,768),new Vector2(800,1280),new Vector2(2560,1080),new Vector2(1100,510)})
            {
                Rect rect=CombatViewport.Pixels(screen.x,screen.y);float scale=Mathf.Min(screen.x/1280,screen.y/800);
                if(rect.yMin<=205*scale||rect.yMax>=screen.y-92*scale)throw new Exception("Battle viewport overlaps HUD");checks++;
                if(rect.width/rect.height>1.801f||Mathf.Abs(rect.center.x-screen.x*.5f)>.01f)throw new Exception("Wide Game view exposes adjacent sites");checks++;
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


