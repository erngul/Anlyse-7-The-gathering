namespace The_gathering_v2.Models.TurnPhases;

public class PreparationPhase  : IObserver<GeneralBoard>
{
    public Game? Game { get; set; }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(GeneralBoard value)
    {
        
        // value.
        foreach (var C in value.Attacker.Board.Cards)
        {
            C.Reset();
        }
    }
}