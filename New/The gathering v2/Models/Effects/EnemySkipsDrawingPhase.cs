/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/
namespace The_gathering_v2.Models.Effects;



public class EnemySkipsDrawingPhase : IEffect
{
    public int Turns { get; set; } = 0;
    public bool PreparationPhase { get; set; } = true;
    public string? Description { get; set; } = "Enemy Skips drawing phase.";
    public Deck Deck { get; set; }
    public void Use(GeneralBoard generalBoard, Player player)
    {
        generalBoard.InterruptionStack.Push(new InterruptionStack(this, player));
    }

    public void OnDestroy(GeneralBoard generalBoard)
    {
        if (Turns == 0)
        {
            generalBoard.Defender.Deck = Deck;
        }
        else
        {
            Turns -= 1;
        }
    }


    public void Effect(GeneralBoard generalBoard, Player player)
    {
        Deck = generalBoard.Defender.Deck;
        generalBoard.Defender.Deck = null;
    }
}