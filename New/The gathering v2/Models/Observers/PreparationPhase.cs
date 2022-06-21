namespace The_gathering_v2.Models.TurnPhases;

public class PreparationPhase  : IObserver<Play>
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
        Console.WriteLine($"{value.RestoredLands} lands are restored.");
        // value.
        value.RestoredLands = 0;
        
    }
}