using System;
using System.Collections.Generic;
using System.Linq;
using TheGatheringConsole.Models;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Services
{
    public class CreateCurrentStateService
    {
        
        static Random _R = new Random ();
        static T RandomEnumValue<T> ()
        {
            var v = Enum.GetValues (typeof (T));
            return (T) v.GetValue (_R.Next(v.Length));
        }
        public Game CreateGameState()
        {
            Game result = new Game();
            result.Players[0] = CreatePlayerState(1);
            result.Players[1] = CreatePlayerState(2);
            return result;
        }
        public Player CreatePlayerState(int playerNumber)
        {
            Player result = new Player();
            result.Deck = GenerateDeck();
            result.HandCards = new List<Card>();
            for (int i = 0; i < 7; i++)
            {
                result.HandCards.Add(result.Deck.Pop());
            }

            result.DiscardPile = new List<Card>();
            result.EnergyReserve = new Dictionary<CardColorsEnum, int>()
            {
                { CardColorsEnum.Red, 0 },
                { CardColorsEnum.Blue, 0 },
                { CardColorsEnum.Brown, 0 },
                { CardColorsEnum.White, 0 },
                { CardColorsEnum.Green, 0 }
            };
            result.FloorCards = new List<Card>();
            result.PlayerNumber = playerNumber;
            return result;
        }

        public Stack<Card> GenerateDeck()
        {
            Random rnd = new Random();
            Stack<Card> result = new Stack<Card>();
            for (int i = 0; i < 30; i++)
            {
                CardTypeEnum cardType = RandomEnumValue<CardTypeEnum> ();
                if (cardType == CardTypeEnum.SpellCard)
                {
                    SpellCard spellCard = new SpellCard()
                    {
                        Color = RandomEnumValue<CardColorsEnum>(),
                        EffectsType = RandomEnumValue<CardEffectsTypeEnum>(),
                        SpellCost = rnd.Next(0, 4),
                        SpellEffect = RandomEnumValue<SpellEfectEnum>(),
                    };
                    spellCard.Hash = spellCard.GetHashCode();
                    var sameCards = result.Where(c => c.Hash == spellCard.Hash).ToList();
                    if (sameCards.Count <= 3)
                    {
                        result.Push(spellCard);
                    }
                    else
                    {
                        i--;
                    }

                } 
                else if (cardType == CardTypeEnum.CreatureCard)
                {
                    Creature creatureCard = new Creature()
                    {
                        Color = RandomEnumValue<CardColorsEnum>(),
                        EffectsType = RandomEnumValue<CardEffectsTypeEnum>(),
                        SpellCost = rnd.Next(0, 4),
                        SpellEffect = RandomEnumValue<SpellEfectEnum>(),
                        Attack = rnd.Next(0,10),
                        Defence = rnd.Next(0,10)
                    };
                    creatureCard.Hash = creatureCard.GetHashCode();
                    var sameCards = result.Where(c => c.Hash == creatureCard.Hash).ToList();
                    if (sameCards.Count <= 3)
                    {
                        result.Push(creatureCard);
                    }
                    else
                    {
                        i--;
                    }

                }
                else if (cardType == CardTypeEnum.LandCard)
                {
                    LandCard landCard = new LandCard()
                    {
                        Color = RandomEnumValue<CardColorsEnum>(),
                        EffectsType = RandomEnumValue<CardEffectsTypeEnum>(),
                        EnergyUsedInRound = false
                    };
                    landCard.Hash = landCard.GetHashCode();
                    var sameCards = result.Where(c => c.Hash == landCard.Hash).ToList();
                    if (sameCards.Count <= 3)
                    {
                        result.Push(landCard);
                    }
                    else
                    {
                        i--;
                    }

                }
            }

            return result;
        }
    }
}