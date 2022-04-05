using System.Drawing;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Models
{
    public class Card
    {
        // public string Name { get; set; }
        public CardColorsEnum Color { get; set; }
        public CardEffectsTypeEnum EffectsType { get; set; }
        public int Hash { get; set; }
        public int SummonedInTurn { get; set; }

    }
}