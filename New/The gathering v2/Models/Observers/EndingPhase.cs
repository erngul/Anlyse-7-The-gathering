namespace The_gathering_v2.Models.TurnPhases;

public class EndingPhase : IObserver<GeneralBoard>
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
        while (value.Attacker.Hand.Cards.Count > 7)
        {
            value.Attacker.DiscardPile.Cards.Add(value.Attacker.Hand.Cards[0]);
            value.Attacker.Hand.Cards.RemoveAt(0);
            Console.WriteLine($"Removed a card from hand to discard pile, because there are too many cards in hand.");
        }
        Console.WriteLine($"End Situation Attacker: There are {value.Attacker.Hand.Cards.Count} cards in hand and {value.Attacker.Board.Cards.Count} cards on the board.");
        Console.WriteLine($"End Situation Defender: There are {value.Defender.Hand.Cards.Count} cards in hand and {value.Defender.Board.Cards.Count} cards on the board.");

    }
}