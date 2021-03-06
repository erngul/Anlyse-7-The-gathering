using System;
using System.Collections.Generic;
using TheGatheringConsole.Models;
using TheGatheringConsole.Models.Enums;
using TheGatheringConsole.Services;

namespace TheGatheringConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateCurrentStateService currentStateService = new CreateCurrentStateService();
            var game = currentStateService.CreateGameState();
            Console.WriteLine("Hello World!");
            Boolean gameNotOver = true;
            PlayerService playerService = new PlayerService();
            GameService gameService = new GameService();
            while (gameNotOver)
            {
                game.Turn += 1;
                Console.WriteLine($"--------------------------------------------------------");
                Console.WriteLine($"Turn {game.Turn}");
                foreach (var player  in game.Players)
                {
                    Console.WriteLine($"Player {player.PlayerNumber} is playing.");
                    game.CurrentPlayer = player;
                    gameService.PlaySpellStack(game);
                    if (playerService.CheckPlayerPlayable(player))
                    {
                        gameNotOver = false;
                        break;
                    }
                    playerService.CheckHand(player);
                    playerService.CheckFloorCards(player);
                    playerService.DrawCard(player);
                    playerService.PlaceLandCards(player, game.Turn);
                    playerService.SummonCreatureCards(game);
                    playerService.UseCreatureCards(game);
                    playerService.SummonSpellCards(game);
                    playerService.PutLandCardEnergyToReserve(player);
                    Console.WriteLine("");

                }
            }
            Console.WriteLine("Game Over");
        }
    }
}