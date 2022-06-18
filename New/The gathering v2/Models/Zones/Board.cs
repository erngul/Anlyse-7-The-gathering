namespace The_gathering_v2.Models
{
    public class Board : IZone
    {
        public List<ICard>? Cards { get; set; }
    }
}