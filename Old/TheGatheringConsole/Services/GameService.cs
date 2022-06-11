using System;
using TheGatheringConsole.Models;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole.Services
{
    public class GameService
    {
        public void PlaySpellStack(Game game)
        {
            while (game.SpellStack.Count > 0)
            {
                var currentSpell = game.SpellStack.Pop();
                if (currentSpell.Item1.SpellEffect == SpellEfectEnum.CounterSpell && game.SpellStack.Count > 0)
                {
                    var canceledSpell = game.SpellStack.Pop();
                    Console.WriteLine($"{canceledSpell} has been cancelled with a counter spell from player {game.CurrentPlayer.PlayerNumber}");
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
                        $"A {creature.Color} creature with {creature.Attack}/{creature.Defence} " +
                        $"has gotton extra attack damage from a spell and has now {creature.Attack + spellCard.EffectValue} attack.");
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
                        $"A {creature.Color} creature with {creature.Attack}/{creature.Defence} has gotton extra defence from a spell and has now {creature.Defence + spellCard.EffectValue} defence.");
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
    }
}