﻿using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Models
{
    public class SpellCard: Card
    {
        public int SpellCost { get; set; }
        public SpellEfectEnum SpellEffect { get; set; }
        public int EffectValue { get; set; }
    }
}