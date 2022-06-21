namespace The_gathering_v2.Models.Cards
{
    public class PermanentCard : ISpellCard
    {
        public Color? Color { get; set; }
        public bool Used { get; set; }
        public IEffect? Effect { get; set; }
    }
}