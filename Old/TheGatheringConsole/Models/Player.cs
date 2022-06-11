using System.Collections.Generic;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Models
{
    public class Player
    {
        public Stack<ICard> Deck { get; set; }
        public List<ICard> HandCards { get; set; }
        public int Life = 10;
        public int EnergyReserve { get; set; }
        // public Dictionary<CardColorsEnum, int> EnergyReserve { get; set; }
        public List<ICard> DiscardPile { get; set; }
        public List<ICard> FloorCards { get; set; }
        public int PlayerNumber { get; set; }
    }
}