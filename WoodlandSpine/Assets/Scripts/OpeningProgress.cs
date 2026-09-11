using System;
namespace WoodlandSpine
{
    [Serializable] public sealed class HuntProgress
    {
        public bool accepted, interpreted, followed, resolved, lethal, harvested, rewarded;
        public int evidence;
        public bool AllEvidence => (evidence & 15)==15;
        public void Inspect(int clue){if(accepted&&clue>=0&&clue<4)evidence|=1<<clue;}
        public bool Interpret(bool correct){if(!AllEvidence||!correct)return false;interpreted=true;return true;}
        public bool Resolve(bool killed){if(resolved||!interpreted||!followed)return false;resolved=true;lethal=killed;return true;}
    }
    [Serializable] public sealed class OpeningProgress
    {
        public HuntProgress[] hunts={new HuntProgress(),new HuntProgress(),new HuntProgress()};
        public int activeHunt=-1, coins=10, roomPrice=18, marlowPayment=8, huntReward=15;
        public bool paidByMarlow, huntingLearned, kitchenAccess, cookingLearned, demonstrated, wellFed, roomRented;
        public bool marlowGone, crimeWarned, criminal, defeatedGarrick, combatSeen;
        public float illnessElapsed, illnessSeconds=2700;
        public string food="";
        [NonSerialized] public System.Collections.Generic.Dictionary<string,int> provisions=new System.Collections.Generic.Dictionary<string,int>();
        public int foodPortions {get=>provisions.TryGetValue(food,out int count)?count:0;set=>provisions[food]=Math.Max(0,value);}
        public int pantryContributions, labContributions;
        public bool Choose(int index){if(index<0||index>=3||hunts[index].rewarded)return false;if(activeHunt>=0&&!hunts[activeHunt].rewarded)return activeHunt==index;activeHunt=index;hunts[index].accepted=true;return true;}
        public bool Reward(int index,string ingredient){var h=hunts[index];if(!h.resolved||h.rewarded)return false;h.rewarded=true;huntingLearned=true;coins+=huntReward;food=ingredient;foodPortions+=3;return true;}
        public bool Rent(){if(roomRented||coins<roomPrice)return false;coins-=roomPrice;roomRented=true;return true;}
        public void TickIllness(float seconds,bool started,bool treated){if(!started||treated||marlowGone)return;illnessElapsed+=Math.Max(0,seconds);if(illnessElapsed>=illnessSeconds)marlowGone=true;}
    }
    public enum CookStep { Inspect, Prepare, SetUp, Cook, Read, Remove, Finish, Complete }
    [Serializable] public sealed class CookingSession
    {
        public CookStep step;
        public bool practice;
        public string ingredient, feedback="Sylvie: Watch the ingredient, not the clock.";
        public float heat=.3f;
        public bool Eggs => ingredient!=null&&ingredient.Contains("Eggs");
        public bool Preserved => ingredient!=null&&(ingredient.Contains("Preserved")||ingredient.Contains("Shed"));
        public string Instruction => step switch
        {
            CookStep.Inspect=>Eggs?"Inspect the shells and crack each egg into a separate cup.":Preserved?"Inspect the preserved or shed cut. Trim and clean it carefully.":"Inspect the fresh cut: firm flesh, clean surface, no spoilage.",
            CookStep.Prepare=>Eggs?"Beat the eggs gently.":Preserved?"Soak briefly, drain and slice evenly.":"Trim the cut and slice evenly across the grain.",
            CookStep.SetUp=>"Set a clean pan and a little fat over the stove.",
            CookStep.Cook=>Eggs?"Use gentle heat (0.25–0.45).":"Use moderate heat (0.45–0.65).",
            CookStep.Read=>Eggs?"The curds are soft and just set. Read the texture before removing.":"The surface is golden and the centre has cooked through. Read the colour and texture.",
            CookStep.Remove=>"Remove the pan from heat while the food is still tender.",
            CookStep.Finish=>"Season and serve a modest portion.",
            _=>"Well Fed. No healing effect."
        };
        public bool Advance(){if(step==CookStep.Complete)return false;if(step==CookStep.Cook&&(heat<(Eggs?.25f:.45f)||heat>(Eggs?.45f:.65f))){feedback="Sylvie adjusts your hand: Watch the heat. "+Instruction;return false;}step++;feedback=Instruction;return true;}
    }
}
