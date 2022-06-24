/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

using The_gathering_v2.Models.Cards;

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
            if (C is ISpellCard ispellCard)
            {
                ispellCard.Effect.OnDestroy(value);
            }
        }

        foreach (var C in value.Attacker.DiscardPile.Cards)
        {
            C.Reset();
            if (C is ISpellCard ispellCard)
            {
                ispellCard.Effect.OnDestroy(value);
            }
        }
    }
}