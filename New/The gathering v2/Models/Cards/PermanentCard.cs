namespace The_gathering_v2.Models.Cards
{
    public abstract class PermanentCard : ISpellCard
    {
        public abstract Color? Color { get; set; }
        public bool Used { get; set; }
        public IEffect? Effect { get; set; }
        public int CostToBePlayed { get; set; }

        public abstract void Activate(GeneralBoard generalBoard);

        public abstract void OnDestroy(GeneralBoard generalBoard, Player player);

        public abstract void Use(GeneralBoard generalBoard);
        public abstract void Reset();
        public abstract void PlaceOnBoard(GeneralBoard generalBoard);
    }
}