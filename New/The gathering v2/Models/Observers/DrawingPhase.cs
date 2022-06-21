namespace The_gathering_v2.Models.TurnPhases;

public class DrawingPhase  : IObserver<Play>
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

    public void OnNext(Play value)
    {
        
    }
}