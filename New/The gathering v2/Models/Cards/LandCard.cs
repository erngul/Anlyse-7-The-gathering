namespace The_gathering_v2.Models.Cards
{
    public class LandCard : ICard
    {
        public Color? Color { get; set; }
        public bool Used { get; set; }
    }
}