using System.Drawing;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Models
{
    public interface ICard
    {
        // public string Name { get; set; }
        public CardColorsEnum Color { get; set; }
        public CardEffectsTypeEnum EffectsType { get; set; }
        public int HashCode { get; set; }
        public int SummonedInTurn { get; set; }
    }
}