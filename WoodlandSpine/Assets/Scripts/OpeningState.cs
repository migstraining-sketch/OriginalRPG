using System;
namespace WoodlandSpine
{
    public enum Ingredient { Bloodleaf, Silvermoss, MooncalfMilk }
    public enum MooncalfOutcome { None, Peaceful, Frightened, Killed }
    [Serializable]
    public sealed class OpeningState
    {
        public ReactiveIntroState intro=new ReactiveIntroState();
        public bool metGarrick, invitedDownstairs, sawTroll, questAccepted;
        public bool bloodleafObtained, silvermossObtained, milkObtained;
        public bool mossbackSeen, mossbackPursued, mossbackSurvived, mossbackDefeated;
        public bool returnedToMarlow, potionCompleted, trollTreated, potionMakingUnlocked, healthRecipeUnlocked, huntingBoardUnlocked;
        public bool treatmentFailed;
        public bool marlowKnowsMossbackIncident;
        public MooncalfOutcome mooncalfOutcome;
        public bool AllGathered => bloodleafObtained && silvermossObtained && milkObtained;
        public bool ReadyToReport => AllGathered && !treatmentFailed;
        public bool Gather(Ingredient ingredient, Inventory inventory)
        {
            if(!questAccepted)return false;
            switch(ingredient)
            {
                case Ingredient.Bloodleaf: if(bloodleafObtained)return false;bloodleafObtained=true;inventory.bloodleaf++;break;
                case Ingredient.Silvermoss: if(silvermossObtained)return false;silvermossObtained=true;inventory.silvermoss++;break;
                case Ingredient.MooncalfMilk: if(milkObtained)return false;milkObtained=true;inventory.mooncalfMilk++;break;
            }
            return true;
        }
        public bool FinishPotion(Inventory inventory, PotionSession session)
        {
            if(treatmentFailed||potionCompleted||!returnedToMarlow||!session.Complete||inventory.bloodleaf<1||inventory.silvermoss<1||inventory.mooncalfMilk<1)return false;
            inventory.bloodleaf--;inventory.silvermoss--;inventory.mooncalfMilk--;inventory.cleanFieldFlask=true;inventory.experimentalPotion++;potionCompleted=true;return true;
        }
        public bool Treat(Inventory inventory)
        {
            if(!potionCompleted||trollTreated||inventory.experimentalPotion<1)return false;
            inventory.experimentalPotion--;trollTreated=true;inventory.healthPotions++;potionMakingUnlocked=true;healthRecipeUnlocked=true;return true;
        }
        public bool UnlockBoard(){if(!trollTreated)return false;huntingBoardUnlocked=true;return true;}
    }
}
