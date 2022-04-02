namespace TheGatheringConsole.Models
{
    public class LandCard: Card
    {
        public int EnergyGeneration { get; set; }
        public int EnergAvailable { get; set; }
        public int CardTurnedAtRound { get; set; }
        public int AmountEnergyCost { get; set; }
    }
}