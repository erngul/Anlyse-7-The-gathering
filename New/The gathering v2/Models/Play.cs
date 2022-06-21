using The_gathering_v2.Models.Cards;
using The_gathering_v2.Models.TurnPhases;

namespace The_gathering_v2.Models;

public class Play : IObservable<Play>, IDisposable
{
    public Player Attacker { get; set; }
    public Player Defender { get; set; }
    public int RestoredLands = 0;
    public bool GameOver = false;
    public InterruptionStack InterruptionStack = new InterruptionStack();
    public List<IObserver<Play>> Observers = new()
    {
        new PreparationPhase(),
        new DrawingPhase(),
        new MainPhase(),
        new EndingPhase()
    };

    public IDisposable Subscribe(IObserver<Play> observer)
    {
        if (! Observers.Contains(observer))
            Observers.Add(observer);
        return this;
    }

    public void NotifyPreparationPhase()
    {
        Observers[0].OnNext(this);
    }
    public void NotifyDrawingPhase()
    {
        Observers[1].OnNext(this);
    }
    public void NotifyMainPhase()
    {
        Observers[2].OnNext(this);
    }
    public void NotifyEndingPhase()
    {
        Observers[3].OnNext(this);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void RestorePlayerLands()
    {
        foreach (var b in Attacker.Board.Cards)
        {
            if (b is LandCard landCard)
            {
                if (landCard.Used)
                {
                    RestoredLands++;
                    landCard.Used = false;
                }

            }
        }
        NotifyPreparationPhase();
    }

    public void GetACardFromTheDeck()
    {
        if (Attacker.Deck.Cards == null)
        {
            Console.WriteLine("Skips [drawing] phase and does not get a card from the deck.");
        }
        else
        {
            if (Attacker.Deck.Cards.Count > 0)
            {
                Attacker.Hand.Cards.Add(Attacker.Deck.Cards.Pop());
                Console.WriteLine("Gets a card from the deck.");
            }
            else
            {
                Console.WriteLine("No cards left in the deck. Game over.");
            }
        }
        NotifyDrawingPhase();
    }
    
}