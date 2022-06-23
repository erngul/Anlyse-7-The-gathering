namespace The_gathering_v2.Models.Effects;

public class SkipDrawingPhase : IEffect
{
    public int Turns { get; set; }
    public bool PreparationPhase { get; set; } = true;
    public string? Description { get; set; } = "Enemy Skips drawing phase.";
    public Deck Deck { get; set; }
    public void Use(GeneralBoard generalBoard)
    {
        throw new NotImplementedException();
    }

    public void OnDestroy(GeneralBoard generalBoard)
    {
        generalBoard.Defender.Deck = Deck;
    }


    public void Effect(GeneralBoard generalBoard)
    {
        Deck = generalBoard.Defender.Deck;
        generalBoard.Defender.Deck = null;
    }
}