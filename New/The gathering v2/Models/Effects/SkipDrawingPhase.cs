﻿namespace The_gathering_v2.Models.Effects;

public class SkipDrawingPhase : IEffect
{
    public int Turns { get; set; }
    public string? Description { get; set; }
}