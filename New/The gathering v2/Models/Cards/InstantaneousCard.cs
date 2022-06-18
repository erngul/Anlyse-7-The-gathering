namespace The_gathering_v2.Models.Cards
{
    public class InstantaneousCard : ISpellCard
    {
        public Color? Color { get; set; }
        public IEffect Effect { get; set; }
    }
}