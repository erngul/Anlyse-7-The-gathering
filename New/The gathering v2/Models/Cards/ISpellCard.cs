namespace The_gathering_v2.Models.Cards
{
    public interface ISpellCard : ICard
    {
        public abstract IEffect Effect { get; set; }
        public int CostToBePlayed { get; set; }
        public void OnDestroy(GeneralBoard generalBoard, Player player);
    }
}