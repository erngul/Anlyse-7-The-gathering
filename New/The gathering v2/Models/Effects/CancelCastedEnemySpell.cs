﻿/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

namespace The_gathering_v2.Models.Effects.CounterSpells;

public class CancelCastedEnemySpell : IEffect
{
    public int Turns { get; set; }
    public string? Description { get; set; }
    public void Use(GeneralBoard generalBoard, Player player)
    {
        throw new NotImplementedException();
    }

    public void OnDestroy(GeneralBoard generalBoard)
    {
        throw new NotImplementedException();
    }

    public bool PreparationPhase { get; set; }

    public void Effect(GeneralBoard generalBoard, Player player)
    {
        throw new NotImplementedException();
    }
}