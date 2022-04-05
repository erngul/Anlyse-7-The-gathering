using System;
using System.Collections.Generic;
using TheGatheringConsole.Models;

namespace TheGatheringConsole.Services
{
    public class PlayerService
    {
        public Boolean CheckPlayerPlayable(Player player)
        {
            if (player.Deck.Count <= 0 )
            {
                Console.WriteLine($"Player {player.PlayerNumber} has no cards left in his deck. He lost.");
                return true;
            }
            if ( player.Life <= 0)
            {
                Console.WriteLine($"Player {player.PlayerNumber} has no more life left. He lost");
                return true;
            }

            return false;
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

        public void CheckDeck(Player player)
        {
            
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
                Console.WriteLine($"A {card.Color} Creature has been drawn with {creature.SpellEffect} and with {creature.Attack}/{creature.Defence} attack/defence.");
            }
            else if (card is SpellCard)
            {
                var spellCard = (SpellCard)card;
                Console.WriteLine($"A {card.Color} SpellCard has been drawn with {spellCard.SpellEffect}.");
            }
        }

        public void PlaceLandCards(Player player, int turn)
        {
            for (int i = 0; i < player.HandCards.Count; i++)
            {
                var card = player.HandCards[i];
                if (card is LandCard)
                {
                    var landCard = (LandCard)card;
                    landCard.SummonedInTurn = turn;
                    landCard.EnergyUsedInRound = true;
                    player.FloorCards.Add(landCard);
                    player.HandCards.RemoveAt(i);
                    Console.WriteLine($"A {landCard.Color} landcard has been has been set to the floor");
                }
            }
        }
        public int CountEnergy(Player player)
        {
            
        }
        public void SummonSpellOrCreatureCards(Player player, int turn)
        {
            for (int i = 0; i < player.HandCards.Count; i++)
            {
                var card = player.HandCards[i];
                if (card is Creature)
                {
                    var creature = (Creature)card;
                    foreach (var VARIABLE in COLLECTION)
                    {
                        
                    }
                    creature.SummonedInTurn = turn;
                    player.FloorCards.Add(creature);
                    player.HandCards.RemoveAt(i);
                    Console.WriteLine($"A {creature.Color} creature with {creature.Attack}/{creature.Defence}(attack/defence) creature has been summoned to the floor");
                }
                else if (card is SpellCard)
                {
                    var spellCard = (SpellCard)card;
                    spellCard.SummonedInTurn = turn;
                    player.FloorCards.Add(spellCard);
                    player.HandCards.RemoveAt(i);
                    Console.WriteLine($"A {spellCard.Color} spellcard has been summoned to the floor");
                }
            }
        }
    }
}