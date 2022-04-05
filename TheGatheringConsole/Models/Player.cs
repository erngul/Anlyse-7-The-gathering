using System.Collections.Generic;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Models
{
    public class Player
    {
        public Stack<Card> Deck { get; set; }
        public List<Card> HandCards { get; set; }
        public int Life = 10;
        public int EnergyReserve { get; set; }
        // public Dictionary<CardColorsEnum, int> EnergyReserve { get; set; }
        public List<Card> DiscardPile { get; set; }
        public List<Card> FloorCards { get; set; }
        public int PlayerNumber { get; set; }
    }
}