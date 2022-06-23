using The_gathering_v2.Models.Cards;

namespace The_gathering_v2.Models.TurnPhases;

public class MainPhase  : IObserver<GeneralBoard>
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
        while (value.InterruptionStack.Cards.Count > 0)
        {
            var effect = value.InterruptionStack.Cards.Pop();
            effect.Effect(value);
        }

        foreach (var c in value.Attacker.Board.Cards)
        {
            if (c is Creature creature)
            {
                if (creature.Used)
                {
                    creature.Activate(value);
                }
            }
        }

    }
}