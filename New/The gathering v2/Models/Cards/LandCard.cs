namespace The_gathering_v2.Models.Cards
{
    public class LandCard : ICard
    {
        public Color? Color { get; set; }
        public bool Used { get; set; } = false;
        public void Use(GeneralBoard generalBoard)
        {
            Used = true;
            Console.WriteLine("Takes energy from land.");
        }
        public void Reset()
        {
            if (Used)
            {
                Used = false;
                Console.WriteLine("land restored.");
            }
        }
    }
}