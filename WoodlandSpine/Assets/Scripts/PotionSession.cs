using System;
namespace WoodlandSpine
{
    public enum BrewStep { Bloodleaf, Silvermoss, Milk, CombineLeaf, CombineMoss, CombineMilk, HeatAndStir, Finish, Complete }
    [Serializable]
    public sealed class PotionSession
    {
        public BrewStep step;
        public int steadyStirs;
        public float milkMeasure=.5f, heat=.2f;
        public string feedback="Marlow: One preparation at a time. I'll watch your hands.";
        public bool Complete => step==BrewStep.Complete;
        public string Instruction => step switch
        {
            BrewStep.Bloodleaf=>"Prepare Bloodleaf: separate the useful red leaf from its tough vein.",
            BrewStep.Silvermoss=>"Prepare Silvermoss: brush away grit, then bruise the clean moss.",
            BrewStep.Milk=>"Measure one mark of Mooncalf Milk (1.0). Adjust the measure, then pour.",
            BrewStep.CombineLeaf=>"Begin the mixture with the prepared Bloodleaf.",
            BrewStep.CombineMoss=>"Now fold in the Silvermoss; keep the mixture even.",
            BrewStep.CombineMilk=>"Add the measured milk last, in a slow stream.",
            BrewStep.HeatAndStir=>"Keep gentle heat between 0.35 and 0.55; make three steady stirs.",
            BrewStep.Finish=>"The mixture is even. Take it off the heat and decant two doses.",
            _=>"Experimental Health Potion ready. The first dose is for the troll."
        };
        public bool Perform(string action)
        {
            bool valid=step switch
            {
                BrewStep.Bloodleaf=>action=="separate",
                BrewStep.Silvermoss=>action=="brush",
                BrewStep.Milk=>action=="measure" && Math.Abs(milkMeasure-1f)<=.05f,
                BrewStep.CombineLeaf=>action=="leaf",
                BrewStep.CombineMoss=>action=="moss",
                BrewStep.CombineMilk=>action=="milk",
                BrewStep.HeatAndStir=>action=="stir" && heat>=.35f && heat<=.55f,
                BrewStep.Finish=>action=="decant",
                _=>false
            };
            if(!valid){feedback="Marlow steadies your hand: Not yet. "+Instruction;return false;}
            if(step==BrewStep.HeatAndStir && ++steadyStirs<3){feedback=$"Steady stir {steadyStirs}/3. Keep the heat gentle.";return true;}
            step++;feedback=Complete?"Marlow: Bring it to him. Carefully.":"Marlow checks the preparation and nods. "+Instruction;return true;
        }
    }
}
