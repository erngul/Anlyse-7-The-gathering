using System;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Models
{
    public class LandCard: ICard
    {
        public Boolean EnergyUsedInRound { get; set; }
        public CardColorsEnum Color { get; set; }
        public CardEffectsTypeEnum EffectsType { get; set; }
        public int HashCode { get; set; }
        public int SummonedInTurn { get; set; }
    }
}