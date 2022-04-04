using System.Collections.Generic;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Models
{
    public class Player
    {
        public Stack<Card> Deck { get; set; }
        public List<Card> HandCards { get; set; }
        public int Life = 10;
        public Dictionary<CardColorsEnum, int> EnergyReserve { get; set; }
        public List<Card> DiscardPile { get; set; }
        public List<Card> FloorCards { get; set; }
    }
}