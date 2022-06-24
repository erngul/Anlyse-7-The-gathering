/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

namespace The_gathering_v2.Models.TurnPhases;

public class DrawingPhase  : IObserver<GeneralBoard>
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
        Console.WriteLine($"Attacker has {value.Attacker.Hand.Cards.Count} cards in hand.");
    }
}