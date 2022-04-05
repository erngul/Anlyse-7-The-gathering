using System.Collections.Generic;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Models
{
    public class Game
    {
        public int Turn = 0;
        public Player[] Players = new Player[2];
        public Stack<(SpellCard, Player)> SpellStack { get; set; }
    }
}