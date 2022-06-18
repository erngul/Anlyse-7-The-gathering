namespace The_gathering_v2.Models.Cards
{
    public interface ISpellCard : ICard
    {
        public IEffect Effect { get; set; }
    }
}