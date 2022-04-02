using System.Collections.Generic;

namespace TheGatheringConsole.Models
{
    public class Player
    {
        public Stack<Card> Deck { get; set; }
        public List<Card> Cards { get; set; }
        public int Life = 10;
        public int Energy
    }
}