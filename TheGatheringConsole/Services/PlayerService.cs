using System;
using System.Collections.Generic;
using TheGatheringConsole.Models;

namespace TheGatheringConsole.Services
{
    public class PlayerService
    {
        public Boolean CheckPlayerPlayable(Player player)
        {
            if (player.Deck.Count > 0 && player.Life > 0)
            {
                return false;
            }

            return true;
        }

        public void CheckHand(Player player)
        {
            while (player.HandCards.Count > 7)
            {
                player.DiscardPile.Add(player.HandCards[player.HandCards.Count - 1]);
                player.HandCards.RemoveAt(player.HandCards.Count - 1);
            }
        }

        public void CheckFloorCards(Player player)
        {
            if (player.FloorCards.Count > 0)
            {
                for (int i = 0; i < player.FloorCards.Count; i++)
                {
                    var card = player.FloorCards[i];
                    if (card is Creature)
                    {
                        var creature = (Creature)card;
                        if (creature.Defence <=  0)
                        {
                            player.DiscardPile.Add(card);
                            player.FloorCards.RemoveAt(i);
                            // LandCard landCard = (LandCard)card;
                            Console.WriteLine("card removed");
                        }
                    }

                    if (card is LandCard)
                    {
                        var landCard = (LandCard)card;
                        landCard.EnergyUsedInRound = false;
                        player.FloorCards[i] = landCard;
                    }
                }
            }
        }

        public void DrawCard(Player player)
        {
            var card = player.Deck.Pop();
            player.HandCards.Add(card);
            if (card is LandCard)
            {
                Console.WriteLine($"A {card.Color} landcard has been drawn");
            }
            else if (card is Creature)
            {
                var creature = (Creature)card;
                Console.WriteLine($"A {card.Color} Creature has been drawn with {card.EffectsType} and with {creature.Attack}/{creature.Defence} attack/defence");
            }
            else if (card is SpellCard)
            {
                Console.WriteLine($"A {card.Color} SpellCard has been drawn");
            }

            Console.WriteLine($"");
        }

        public void PlaceLandCards(Player player)
        {
            for (int i = 0; i < player.HandCards.Count; i++)
            {
                var card = player.HandCards[i];
                if (card is LandCard)
                {
                    var landCard = (LandCard)card;
                    landCard.EnergyUsedInRound = true;
                    player.FloorCards.Add(landCard);
                    player.HandCards.RemoveAt(i);
                    Console.WriteLine($"A {landCard.Color} landcard has been has been set to the floor");
                }
            }
        }

        public void UseSpellOrCreatureCards(Player player)
        {
            for (int i = 0; i < player.HandCards.Count; i++)
            {
                var card = player.HandCards[i];
                if (card is Creature)
                {
                    var creature = (Creature)card;
                    player.FloorCards.Add(creature);
                    player.HandCards.RemoveAt(i);
                    Console.WriteLine($"A {creature.Color} creature with {creature.Attack}/{creature.Defence}(attack/defence) creature has been summoned to the floor");
                }
                else if (card is SpellCard)
                {
                    var spellCard = (SpellCard)card;
                    player.FloorCards.Add(spellCard);
                    player.HandCards.RemoveAt(i);
                    Console.WriteLine($"A {spellCard.Color} creature has been summoned to the floor");
                }
            }
        }
    }
}