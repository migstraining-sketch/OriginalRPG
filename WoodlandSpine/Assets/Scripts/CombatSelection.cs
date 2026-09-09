namespace WoodlandSpine
{
    public enum CombatChoice { None, Move, Attack, Item, Dash, Defend }
    // Selection contains no battle state; cancelling cannot refund a committed action.
    public sealed class CombatSelection
    {
        public CombatChoice choice {get;private set;}
        public void Select(CombatChoice value){choice=value;}
        public void Cancel(){choice=CombatChoice.None;}
    }
}
