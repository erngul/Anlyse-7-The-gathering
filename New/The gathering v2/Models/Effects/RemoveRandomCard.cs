namespace The_gathering_v2.Models.Effects;

public class RemoveRandomCard : IEffect
{
    public int Turns { get; set; }
    public string? Description { get; set; } = "To remove a random card from the defenders hand.";


    public void OnDestroy(GeneralBoard generalBoard)
    {
        throw new NotImplementedException();
    }

    public bool PreparationPhase { get; set; } = false;

    public void Effect(GeneralBoard generalBoard)
    {
        Random rnd = new Random();
        int randomNumber = rnd.Next(generalBoard.Defender.Hand.Cards.Count());
        var pickedCard =  generalBoard.Defender.Hand.Cards[randomNumber];
        generalBoard.Defender.DiscardPile.Cards.Add(pickedCard);
        generalBoard.Defender.Hand.Cards.Remove(pickedCard);
        Console.WriteLine($"The Defender Discards a card randomly");
        Console.WriteLine($"The card has been put in the discard pile");
    }
    public void Use(GeneralBoard generalBoard)
    {
        generalBoard.InterruptionStack.Cards.Push(this);
    }
}