# Character Creator 5 — one-model handoff

The owner offered to handle exports. Start with **one included adult male base**, in its default appearance or basic clothing. Do not spend time designing the full cast or perfecting Garrick yet: the first test is whether the mesh, skeleton, face controls and materials reach Blender correctly.

1. Save the Character Creator project so the source remains editable.
2. Choose **File → Export → FBX → Clothed Character**.
3. Choose the **Blender** target preset and **Mesh Only**. Reallusion documents that Mesh Only produces a T-pose while retaining bone skinning; it does not mean an unrigged model.
4. Leave **Delete Hidden Mesh** off, so the body remains available for clothing fitting. Leave remeshing/material merging off for this initial editable handoff. Use normal/base mesh resolution; HD subdivision is unnecessary for the first test.
5. Export into a new folder named `CC5-Base-Test`. Keep the FBX, textures folder, JSON and any generated key/sidecar files together. Send the folder path; there is no need to upload it elsewhere.

If the export panel differs or export is blocked, send its screenshot before changing settings or buying anything. The trial/export entitlement has not been verified. This checklist is based on official documentation, not a completed CC5-to-Blender test on this machine.

After receipt, Codex will import the FBX, inspect texture resolution and missing files, check bones/weights/face controls, render it under the same review lighting and determine how to transfer the workwear. Do not assume the existing Quaternius clothing can be dropped onto a CC5 skeleton without refitting and rebinding. Keep imported commercial source assets local until their redistribution terms are established; current tracked foundation sources are CC0.

References checked 2026-09-10:
- [CC5 static bone-skinned FBX export](https://manual.reallusion.com/Character-Creator-5/Content/ENU/5.0/17-Export/Exporting_FBX_Avatars.htm)
- [CC5 export settings and accompanying files](https://manual.reallusion.com/Character-Creator-5/Content/ENU/5.0/17-Export/Export-FBX.htm)
- [Reallusion Blender export workflow](https://magazine.reallusion.com/2024/10/28/master-character-creator-to-blender-ultimate-auto-setup-guide/)
