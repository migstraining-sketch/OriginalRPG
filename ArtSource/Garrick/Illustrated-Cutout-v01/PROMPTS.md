# Garrick cutout generation record

Built-in image-generation tool, 2026-09-10. Both edits preserve the approved illustrated Garrick preview. Source images and returned files were not locally edited.

## First attempt

Input: `../Illustrated-v01/Garrick-Illustrated-Preview.png`.

Edit the supplied approved Garrick illustration into ONE clean full-body 2D game character cutout on a genuinely TRANSPARENT background (RGBA alpha, not a checkerboard painting).
Keep only the full-body man on the left. Remove the large portrait on the right and remove all background, ground shadow, and surrounding decoration. Recenter the one complete standing man with even empty transparent margin around hair, elbows, and boots. Preserve his face, age, broad build, hairstyle, beard, illustrated linework, painted shading, cream rolled-sleeve shirt, dark vest/apron, belt/keys/towel, trousers and boots exactly as closely as possible. Preserve the casually held tankard and relaxed hand-at-belt pose. Keep both boots completely visible. No new accessories, no duplicate person, no cropped extremities, no pedestal, no scenery, no text, no outlines or halos added around the silhouette. Match the approved illustration, not realism, not a 3D render. This asset is for a standing 2D cutout at the inn bar in Unity. The entire image outside the character silhouette must have alpha zero. Aim for tall portrait proportions and high resolution.

Result: RGB PNG with a painted checkerboard, **not real alpha transparency**. Rejected for game use. Original generated file remains at `C:/Users/Miguel/.codex/generated_images/01a0806c-58ec-74a0-862e-1041cbcb2b45/exec-b559abe6-9788-4e31-8090-70480755ce71.png`.

## Corrective edit

Input: the first cutout above.

Edit this single full-body Garrick character image. Preserve the character's face, pose, proportions, tankard, clothing, keys, towel, boots, linework and painted shading. Replace ALL of the checkerboard background with one completely flat, perfectly uniform pure chroma green color #00FF00 (RGB 0,255,0). The entire outside of the character silhouette and all holes between arms, clothing and legs must use this same solid bright green. No checkerboard, no pattern, no gradient, no shadows on the green, no reflected green light, no green tint or green spill on Garrick. Crisp clean silhouette edges. Keep the whole character and both boots, with margin. Exactly one character. This is a game asset with a deliberately solid key color background which the game shader removes.

Final generated source: `exec-9a543d85-2611-42e1-bbad-805bdc1536c7.png` in the same generated-images folder, copied unmodified to `WoodlandSpine/Assets/Art/Characters/Garrick/Garrick-Keyed.png`. The game material removes the green at rendering time; the source PNG itself remains opaque RGB.
