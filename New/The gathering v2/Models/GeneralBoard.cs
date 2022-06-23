using The_gathering_v2.Models.Cards;
using The_gathering_v2.Models.TurnPhases;

namespace The_gathering_v2.Models;

public class GeneralBoard : IObservable<GeneralBoard>, IDisposable
{
    public Player Attacker { get; set; }
    public Player Defender { get; set; }
    public bool GameOver = false;
    public InterruptionStack InterruptionStack { get; set; } = new InterruptionStack()
    {
        Cards = new Stack<IEffect>()
    };
    public List<IObserver<GeneralBoard>> Observers = new()
    {
        new PreparationPhase(),
        new DrawingPhase(),
        new MainPhase(),
        new EndingPhase()
    };

    public IDisposable Subscribe(IObserver<GeneralBoard> observer)
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
    }

    public void AddLandCardToBoard()
    {
        var land = Attacker.Hand.Cards.FirstOrDefault(c => c is LandCard);
        if (land == null)
        {
            Console.WriteLine("No land cars available in hand.");
            return;
        }
        Attacker.Board.Cards.Add(land);
        Attacker.Hand.Cards.Remove(land);
        Console.WriteLine("Plays 1 land.");
    }

    public void UseLandEnergy(Player player, Color color)
    {
        var land = player.Board.Cards.FirstOrDefault(c => c is LandCard && c.Used == false && c.Color.Name == color.Name);
        if (land == null)
        {
            Console.WriteLine($"No unused land cards available in board with {color.Name} color.");
            return;
        }

        var energyReserve = player.EnergyReserve.FirstOrDefault(e => e.Color.Equals(color));
        if (energyReserve == null)
        {
            energyReserve = new EnergyReserve()
            {
                Amount = 0,
                Color = color
            };
            player.EnergyReserve.Add(energyReserve);
        }
        energyReserve.Amount++;
        land.Use(this);
    }

    public bool consumeEnergyForSpellCard(Player player, ISpellCard spellCard)
    {
        var energyReserve = player.EnergyReserve.FirstOrDefault(e => e.Color.Equals(spellCard.Color));
        if (energyReserve != null)
        {
            if (energyReserve.Amount >= spellCard.CostToBePlayed)
            {
                energyReserve.Amount -= spellCard.CostToBePlayed;
                Console.WriteLine($"{spellCard.CostToBePlayed} energy has been taken from the {spellCard.Color} energyReserve.");
                return true;
            }
        }
        var landCards = player.Board.Cards.Where(c => c is LandCard && c.Used == false && c.Color.Name == spellCard.Color.Name).ToList();
        if (landCards.Count < spellCard.CostToBePlayed)
        {
            Console.WriteLine($"Not enough Land cards available in board with {spellCard.Color} color.");
            return false;
        }

        for (int i = 0; i < spellCard.CostToBePlayed; i++)
        {
            UseLandEnergy(player, spellCard.Color);
        }
        return true;
    }
    public void AddPermanentCardToBoard(int cardNumber)
    {
        var permanent = Attacker.Hand.Cards[cardNumber];

        if (permanent is PermanentCard permanentCard)
        {
            permanentCard.PlaceOnBoard(this);
        }
    }

    public void AddInstantaneousCardFromHandToInterruptionStack(int cardNumber)
    {
        var instantaneousCard = Attacker.Hand.Cards[cardNumber];
        if (instantaneousCard is InstantaneousCard instant)
        {
            if (consumeEnergyForSpellCard(Attacker, instant))
            {
                instant.Use(this);
            }
        }
    }

    public void UsePermanentCard(int cardNumber)
    {
        var permanent = Defender.Board.Cards[cardNumber];
        if (permanent is PermanentCard permanentCard)
        {
            if (consumeEnergyForSpellCard(Attacker, permanentCard))
            {
                permanentCard.Use(this);
            }
        }
    }
}