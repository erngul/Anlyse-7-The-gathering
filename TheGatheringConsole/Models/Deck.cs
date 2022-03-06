using System.Collections.Generic;

namespace TheGatheringConsole.Models
{
    public class Deck
    {
        public List<LandCard> LandCards { get; set; }
        public List<SpellCard> SpellCards { get; set; }
    }
}