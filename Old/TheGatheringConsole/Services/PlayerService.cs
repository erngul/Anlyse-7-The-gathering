using System;
using System.Collections.Generic;
using TheGatheringConsole.Models;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Services
{
    public class PlayerService
    {
        public Boolean CheckPlayerPlayable(Player player)
        {
            if (player.Deck.Count <= 0)
            {
                Console.WriteLine($"Player {player.PlayerNumber} has no cards left in his deck. He lost.");
                return true;
            }

            if (player.Life <= 0)
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
                        if (creature.Defence <= 0)
                        {
                            player.DiscardPile.Add(card);
                            player.FloorCards.RemoveAt(i);
                            // LandCard landCard = (LandCard)card;
                            Console.WriteLine("card removed from floor with less than 0 defence");
                        }
                        else
                        {
                            creature.CreatureState = CreatureStateEnum.Defender;
                            card = creature;
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
                Console.WriteLine(
                    $"A {card.Color} Creature has been drawn with {creature.SpellEffect} and with {creature.Attack}/{creature.Defence} attack/defence.");
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
            int energy = player.EnergyReserve;
            foreach (var card in player.FloorCards)
            {
                if (card is LandCard)
                {
                    var landCard = (LandCard)card;
                    if (!landCard.EnergyUsedInRound)
                    {
                        energy += 1;
                    }
                }
            }

            return energy;
        }

        public int UseLandCardEnergy(Player player, int amountNeeded)
        {
            int energy = player.EnergyReserve;
            for (int i = 0; i < player.FloorCards.Count; i++)
            {
                var card = player.FloorCards[i];
                if (card is LandCard)
                {
                    var landCard = (LandCard)card;
                    if (!landCard.EnergyUsedInRound)
                    {
                        if (energy != amountNeeded)
                        {
                            landCard.EnergyUsedInRound = true;
                            player.FloorCards[i] = landCard;
                            energy += 1;
                        }
                    }
                }
            }

            return energy;
        }

        public void UseCreatureCards(Game game)
        {
            Random rnd = new Random();
            for (int i = 0; i < game.CurrentPlayer.FloorCards.Count; i++)
            {
                var card = game.CurrentPlayer.FloorCards[i];
                if (card is Creature creature)
                {
                    if (rnd.Next(2) == 1 && creature.CreatureState != CreatureStateEnum.Atacker)
                    {
                        creature.CreatureState = CreatureStateEnum.Atacker;
                        game.CurrentPlayer.FloorCards[i] = creature;
                        if (creature.SummonedInTurn != game.Turn)
                        {
                            game.SpellStack.Push((creature, game.CurrentPlayer));
                        }

                    }
                }
            }
        }

        public void SummonCreatureCards(Game game)
        {
            for (int i = 0; i < game.CurrentPlayer.HandCards.Count; i++)
            {
                var card = game.CurrentPlayer.HandCards[i];
                if (card is Creature creature)
                {
                    if (creature.SpellCost < CountEnergy(game.CurrentPlayer))
                    {
                        int landCardEnergyUsed = UseLandCardEnergy(game.CurrentPlayer, creature.SpellCost);
                        int reserveEnergyAmountNeeded = 0;
                        if (landCardEnergyUsed < creature.SpellCost)
                        {
                            reserveEnergyAmountNeeded = creature.SpellCost - landCardEnergyUsed;
                            game.CurrentPlayer.EnergyReserve -= reserveEnergyAmountNeeded;
                        }

                        creature.SummonedInTurn = game.Turn;
                        game.CurrentPlayer.FloorCards.Add(creature);
                        game.CurrentPlayer.HandCards.RemoveAt(i);
                        Console.WriteLine(
                            $"A {creature.Color} creature with {creature.Attack}/{creature.Defence}(attack/defence) creature has been summoned to the floor. " +
                            $"{landCardEnergyUsed} lands used for energy and {reserveEnergyAmountNeeded} reserve energy is used.");
                        if (creature.SpellEffect != SpellEfectEnum.None)
                        {
                            game.SpellStack.Push((creature, game.CurrentPlayer));
                            Console.WriteLine(
                                $"The creature has the effect that does the following {creature.SpellEffect}");
                            CheckIfEnemyHasCounterSpell(game.CurrentPlayer, game);
                        }
                        else
                        {
                            Console.WriteLine($"The creature has no effect.");
                        }
                    }
                }
            }
        }

        public void CheckIfEnemyHasCounterSpell(Player player, Game game)
        {
            foreach (var p in game.Players)
            {
                if (p.PlayerNumber != player.PlayerNumber)
                {
                    for (int i = 0; i < p.HandCards.Count; i++)
                    {
                        var card = p.HandCards[i];
                        if (card is SpellCard spellCard and not Creature)
                        {
                            if (spellCard.SpellEffect == SpellEfectEnum.CounterSpell)
                            {
                                game.SpellStack.Push((spellCard, p));
                                p.HandCards.RemoveAt(i);
                                Console.WriteLine($"Player {p.PlayerNumber} uses his counter spell card.");
                            }
                        }
                    }
                }
            }
        }

        public void SummonSpellCards(Game game)
        {
           
            for (int i = 0; i < game.CurrentPlayer.HandCards.Count; i++)
            {
                var card = game.CurrentPlayer.HandCards[i];
                if (card is SpellCard spellCard)
                {
                    if (spellCard.SpellCost < CountEnergy(game.CurrentPlayer))
                    {
                        int landCardEnergyUsed = UseLandCardEnergy(game.CurrentPlayer, spellCard.SpellCost);
                        int reserveEnergyAmountNeeded = 0;
                        if (landCardEnergyUsed < spellCard.SpellCost)
                        {
                            reserveEnergyAmountNeeded = spellCard.SpellCost - landCardEnergyUsed;
                            game.CurrentPlayer.EnergyReserve -= reserveEnergyAmountNeeded;
                        }

                        spellCard.SummonedInTurn = game.Turn;
                        game.CurrentPlayer.FloorCards.Add(spellCard);
                        game.CurrentPlayer.HandCards.RemoveAt(i);
                        Console.WriteLine(
                            $"A {spellCard.Color} spellcard with spell effect {spellCard.SpellEffect} is been used it needs {spellCard.SpellCost} energy cost. " +
                            $"{landCardEnergyUsed} lands used for energy and {reserveEnergyAmountNeeded} reserve energy is used.");
                        if (spellCard.SpellEffect != SpellEfectEnum.None)
                        {
                            game.SpellStack.Push((spellCard, game.CurrentPlayer));
                            Console.WriteLine(
                                $"The spellCard has the effect that does the following {spellCard.SpellEffect}");
                            CheckIfEnemyHasCounterSpell(game.CurrentPlayer, game);
                        }
                        else
                        {
                            Console.WriteLine($"The creature has no effect.");
                        }
                    }
                }
            }
        }

        public void PutLandCardEnergyToReserve(Player player)
        {
            int energy = player.EnergyReserve;
            for (int i = 0; i < player.FloorCards.Count; i++)
            {
                var card = player.FloorCards[i];
                if (card is LandCard)
                {
                    var landCard = (LandCard)card;
                    if (!landCard.EnergyUsedInRound)
                    {
                        landCard.EnergyUsedInRound = true;
                        player.FloorCards[i] = landCard;
                        energy += 1;
                    }
                }
            }

            Console.WriteLine($"{energy} land cards energy has been added to energy reserve.");
        }
    }
}