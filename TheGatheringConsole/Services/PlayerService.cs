using System;
using System.Collections.Generic;
using TheGatheringConsole.Models;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Services
{
    public class PlayerService
    {
        public void PlaySpellStack(Player player, Game game)
        {
            while (game.SpellStack.Count > 0)
            {
                var currentSpell = game.SpellStack.Pop();
                if (currentSpell.Item1.SpellEffect == SpellEfectEnum.CounterSpell && game.SpellStack.Count > 0)
                {
                    var canceledSpell = game.SpellStack.Pop();
                    Console.WriteLine($"{canceledSpell} has been cancelled with a counter spell from player {player}");
                }
                else if (currentSpell.Item1 is Creature creature)
                {
                    if (creature.CreatureState == CreatureStateEnum.Atacker)
                    {
                        AttackEnemy(creature, currentSpell.Item2, game);
                    }
                }
                else
                {
                    UseSpellEffect(currentSpell.Item1, currentSpell.Item2, game);
                }
            }
        }

        public void AttackEnemy(Creature creature, Player player, Game game)
        {
            foreach (var p in game.Players)
            {
                if (p.PlayerNumber != player.PlayerNumber)
                {
                    foreach (var card in p.FloorCards)
                    {
                        if (card is Creature enemycreature)
                        {
                            if (creature.CreatureState == CreatureStateEnum.Defender)
                            {
                                enemycreature.Defence -= creature.Attack;
                                Console.WriteLine(
                                    $"Creature with {enemycreature.Attack}/{enemycreature.Defence + creature.Attack} from user {p.PlayerNumber} has taken {creature.Attack} damage from a enemy creature with {creature.Attack}/{creature.Defence} and has now {enemycreature.Defence} defence left.");
                                return;
                            }
                        }
                    }

                    p.Life -= creature.Attack;
                    Console.WriteLine(
                        $"Player {p.PlayerNumber} with {p.Life + creature.Attack} life has taken {creature.Attack} damage from a enemy creature with {creature.Attack}/{creature.Defence} and has now {p.Life} life left.");
                }
            }
        }

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

        public void UseCreatureCards(Player player, Game game)
        {
            Random rnd = new Random();
            for (int i = 0; i < player.FloorCards.Count; i++)
            {
                var card = player.FloorCards[i];
                if (card is Creature creature)
                {
                    if (rnd.Next(2) == 1)
                    {
                        creature.CreatureState = CreatureStateEnum.Atacker;
                        player.FloorCards[i] = creature;
                    }
                }
            }
        }

        public void SummonCreatureCards(Player player, Game game)
        {
            for (int i = 0; i < player.HandCards.Count; i++)
            {
                var card = player.HandCards[i];
                if (card is Creature creature)
                {
                    if (creature.SpellCost < CountEnergy(player))
                    {
                        int landCardEnergyUsed = UseLandCardEnergy(player, creature.SpellCost);
                        int reserveEnergyAmountNeeded = 0;
                        if (landCardEnergyUsed < creature.SpellCost)
                        {
                            reserveEnergyAmountNeeded = creature.SpellCost - landCardEnergyUsed;
                            player.EnergyReserve -= reserveEnergyAmountNeeded;
                        }

                        creature.SummonedInTurn = game.Turn;
                        player.FloorCards.Add(creature);
                        player.HandCards.RemoveAt(i);
                        Console.WriteLine(
                            $"A {creature.Color} creature with {creature.Attack}/{creature.Defence}(attack/defence) creature has been summoned to the floor. " +
                            $"{landCardEnergyUsed} lands used for energy and {reserveEnergyAmountNeeded} reserve energy is used.");
                        if (creature.SpellEffect != SpellEfectEnum.None)
                        {
                            game.SpellStack.Push((creature, player));
                            Console.WriteLine(
                                $"The creature has the effect that does the following {creature.SpellEffect}");
                            CheckIfEnemyHasCounterSpell(player, game);
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

        public void SummonSpellCards(Player player, Game game)
        {
           
            for (int i = 0; i < player.HandCards.Count; i++)
            {
                var card = player.HandCards[i];
                if (card is SpellCard spellCard)
                {
                    if (spellCard.SpellCost < CountEnergy(player))
                    {
                        int landCardEnergyUsed = UseLandCardEnergy(player, spellCard.SpellCost);
                        int reserveEnergyAmountNeeded = 0;
                        if (landCardEnergyUsed < spellCard.SpellCost)
                        {
                            reserveEnergyAmountNeeded = spellCard.SpellCost - landCardEnergyUsed;
                            player.EnergyReserve -= reserveEnergyAmountNeeded;
                        }

                        spellCard.SummonedInTurn = game.Turn;
                        player.FloorCards.Add(spellCard);
                        player.HandCards.RemoveAt(i);
                        Console.WriteLine(
                            $"A {spellCard.Color} spellcard with spell effect {spellCard.SpellEffect} is been used it needs {spellCard.SpellCost} energy cost. " +
                            $"{landCardEnergyUsed} lands used for energy and {reserveEnergyAmountNeeded} reserve energy is used.");
                        if (spellCard.SpellEffect != SpellEfectEnum.None)
                        {
                            game.SpellStack.Push((spellCard, player));
                            Console.WriteLine(
                                $"The spellCard has the effect that does the following {spellCard.SpellEffect}");
                            CheckIfEnemyHasCounterSpell(player, game);
                        }
                        else
                        {
                            Console.WriteLine($"The creature has no effect.");
                        }
                    }
                }
            }
        }

        public void UseSpellEffect(SpellCard card, Player player, Game game)
        {
            switch (card.SpellEffect)
            {
                case SpellEfectEnum.AddAttackDamage:
                    AddAttackDamageForFloorCards(card, player);
                    break;
                case SpellEfectEnum.AddDefence:
                    AddDefenceForFloorCards(card, player);
                    break;
                case SpellEfectEnum.AddAttackDamageAndShield:
                    AddAttackDamageForFloorCards(card, player);
                    AddDefenceForFloorCards(card, player);
                    break;
                case SpellEfectEnum.RemoveRandomCardFromOpenent:
                    RemoveRandomCardFromOpenent(player, game);
                    break;
            }
        }

        public void AddAttackDamageForFloorCards(SpellCard spellCard, Player player)
        {
            for (int i = 0; i < player.FloorCards.Count; i++)
            {
                var card = player.FloorCards[i];
                if (card is Creature creature)
                {
                    Console.WriteLine(
                        $"Creature with {creature.Attack}/{creature.Defence} has gotton extra attack damage from a spell and has now {creature.Attack + spellCard.EffectValue} attack.");
                    creature.Attack += spellCard.EffectValue;
                    player.FloorCards[i] = creature;
                }
            }
        }

        public void AddDefenceForFloorCards(SpellCard spellCard, Player player)
        {
            for (int i = 0; i < player.FloorCards.Count; i++)
            {
                var card = player.FloorCards[i];
                if (card is Creature creature)
                {
                    Console.WriteLine(
                        $"Creature with {creature.Attack}/{creature.Defence} has gotton extra defence from a spell and has now {creature.Defence + spellCard.EffectValue} defence.");
                    creature.Defence += spellCard.EffectValue;
                    player.FloorCards[i] = creature;
                }
            }
        }

        public void RemoveRandomCardFromOpenent(Player player, Game game)
        {
            Random rnd = new Random();
            foreach (var p in game.Players)
            {
                if (p.PlayerNumber != player.PlayerNumber)
                {
                    if (p.HandCards.Count > 0)
                    {
                        int randomNumber = rnd.Next(0, p.HandCards.Count - 1);
                        p.HandCards.RemoveAt(randomNumber);
                        Console.WriteLine(
                            $"The creature has the effect that removes a random card from the opponent hand.");
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

            Console.WriteLine($"{energy} cards energy has been added to energy reserve.");
        }
    }
}