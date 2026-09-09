namespace WoodlandSpine
{
    // Ingredient evidence only. No inference about the player's contract resolution.
    public static class SylvieIngredients
    {
        public static string Describe(string food) => food switch
        {
            "Fresh Reedback Haunch" => "Reedback.\n\nFresh. Reedwater animal, too. Been feeding on mudgrubs.",
            "Preserved Reedback Cut" => "Reedback. Preserved. Salt-cured. Few weeks, maybe.",
            "Fresh Duskhen Eggs" => "Duskhen.\n\nFresh.",
            "Fresh Brookmaw Tail" => "Brookmaw tail.\n\nHarvested fresh.",
            "Naturally Shed Brookmaw Tail" => "Brookmaw tail.\n\nShed. Clean separation. No cut. No tearing. Recent, too.",
            _ => "Set it here. Let me have a look."
        };
    }
}
