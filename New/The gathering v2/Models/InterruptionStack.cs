﻿using The_gathering_v2.Models.Cards;

namespace The_gathering_v2.Models;

public class InterruptionStack
{
    public Stack<ISpellCard>? Cards { get; set; }
}