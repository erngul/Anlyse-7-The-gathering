namespace The_gathering_v2.Models.Effects.CounterSpells;

public class CancelCastedEnemySpell : IEffect
{
    public int Turns { get; set; }
    public string? Description { get; set; }
    public Play? Play { get; set; }
    public bool PreparationPhase { get; set; }

    public void Effect()
    {
        throw new NotImplementedException();
    }
}