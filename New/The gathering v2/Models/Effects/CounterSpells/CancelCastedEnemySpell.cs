namespace The_gathering_v2.Models.Effects.CounterSpells;

public class CancelCastedEnemySpell : ICounterSpell
{
    public int Turns { get; set; }
    public string? Description { get; set; }
}