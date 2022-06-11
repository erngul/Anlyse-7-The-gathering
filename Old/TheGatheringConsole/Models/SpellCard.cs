using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Models
{
    public class SpellCard: ICard
    {
        public int SpellCost { get; set; }
        public SpellEfectEnum SpellEffect { get; set; }
        public int EffectValue { get; set; }
        public CardColorsEnum Color { get; set; }
        public CardEffectsTypeEnum EffectsType { get; set; }
        public int HashCode { get; set; }
        public int SummonedInTurn { get; set; }
    }
}