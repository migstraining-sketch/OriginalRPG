using System;
using UnityEditor;
using UnityEngine;
namespace WoodlandSpine.Editor
{
    public static class OpeningValidation
    {
        static int checks;
        static void Check(bool value,string title){if(!value)throw new Exception("Opening validation: "+title);checks++;}
        [MenuItem("Woodland/Validate Marlow opening state")]
        public static void Run()
        {
            checks=0;var state=new OpeningState();var inventory=new Inventory();var brew=new PotionSession();
            Check(!state.Gather(Ingredient.Bloodleaf,inventory),"gather requires accepted expedition");
            Check(!state.UnlockBoard(),"board remains locked before treatment");
            state.questAccepted=true;
            foreach(Ingredient ingredient in Enum.GetValues(typeof(Ingredient)))
            {Check(state.Gather(ingredient,inventory),"first gathering "+ingredient);Check(!state.Gather(ingredient,inventory),"no duplicate gathering "+ingredient);}
            Check(state.AllGathered&&state.ReadyToReport,"ingredients permit immediate treatment before optional report");
            state.mossbackSurvived=true;Check(state.ReadyToReport,"retreat does not withhold treatment when supplies exist");
            state.mossbackDefeated=true;Check(state.ReadyToReport,"Mossback defeated and supplies gathered");
            Check(!state.FinishPotion(inventory,brew),"no premature potion");state.returnedToMarlow=true;
            Check(!brew.Perform("whole")&&brew.step==BrewStep.Bloodleaf,"supervision blocks wrong preparation");
            Check(brew.Perform("separate")&&brew.Perform("brush"),"prepare plant portions");
            Check(!brew.Perform("measure")&&brew.step==BrewStep.Milk,"wrong measure retained safely");
            brew.milkMeasure=1;Check(brew.Perform("measure"),"correct measure");
            Check(!brew.Perform("milk")&&brew.step==BrewStep.CombineLeaf,"sequence cannot be skipped");
            Check(brew.Perform("leaf")&&brew.Perform("moss")&&brew.Perform("milk"),"correct combine order");
            brew.heat=.9f;Check(!brew.Perform("stir")&&brew.steadyStirs==0,"supervision blocks high heat");
            brew.heat=.45f;Check(brew.Perform("stir")&&brew.Perform("stir")&&brew.Perform("stir"),"three gentle stirs");
            Check(brew.Perform("decant")&&brew.Complete,"decant batch");
            Check(state.FinishPotion(inventory,brew)&&inventory.experimentalPotion==1,"finish reserves troll dose");
            Check(inventory.bloodleaf==0&&inventory.silvermoss==0&&inventory.mooncalfMilk==0,"ingredients consumed once");
            Check(!state.FinishPotion(inventory,brew),"no duplicate batch");
            Check(state.Treat(inventory)&&inventory.healthPotions==1,"remaining dose awarded");
            Check(state.potionMakingUnlocked&&state.healthRecipeUnlocked,"profession and recipe unlock");
            Check(!state.Treat(inventory)&&inventory.healthPotions==1,"treatment idempotent");
            Check(state.UnlockBoard()&&state.huntingBoardUnlocked,"Garrick unlock");
            var selection=new CombatSelection();foreach(CombatChoice choice in Enum.GetValues(typeof(CombatChoice)))
            {selection.Select(choice);selection.Cancel();Check(selection.choice==CombatChoice.None,"cancel "+choice);}
            Debug.Log($"OPENING_VALIDATION_PASSED: {checks} assertions");
        }
    }
}
