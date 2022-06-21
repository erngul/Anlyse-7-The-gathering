namespace The_gathering_v2.Models.Effects;

public class RemoveRandomCard : IEffect
{
    public int Turns { get; set; }
    public string? Description { get; set; }
    public Play? Play { get; set; }
    public bool PreparationPhase { get; set; } = false;

    public void Effect()
    {
        Random rnd = new Random();
        int randomNumber = rnd.Next(Play.Defender.Hand.Cards.Count());
        var pickedCard =  Play.Defender.Hand.Cards[randomNumber];
        Play.Defender.DiscardPile.Cards.Add(pickedCard);
        Play.Defender.Hand.Cards.Remove(pickedCard);
    }
}