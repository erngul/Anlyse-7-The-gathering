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
            PlayerService PlayerService = new PlayerService();
            while (gameNotOver)
            {
                game.Turn += 1;
                foreach (var player  in game.Players)
                {
                    if (PlayerService.CheckPlayerPlayable(player))
                    {
                        gameNotOver = false;
                    }

                    PlayerService.CheckHand(player);
                    PlayerService.CheckFloorCards(player);
                    PlayerService.DrawCard(player);
                    PlayerService.PlaceLandCards(player);
                    PlayerService.UseSpellOrCreatureCards(player);
                }
            }
            Console.WriteLine("GameOver");

            // Deck deck1 = new Deck();
            // deck1.Cards = new List<Card[]>();
            // List<Card[]> cards = new List<Card[]>()
            // {
            //     new Card[3]{
            //         new LandCard()
            //         {
            //             Color = CardColorsEnum.Blue,
            //             sucuk = "LandCard"
            //         },
            //         new LandCard()
            //         {
            //             Color = CardColorsEnum.Blue,
            //             sucuk = "LandCard"
            //         },
            //         new LandCard()
            //         {
            //             Color = CardColorsEnum.Blue,
            //             sucuk = "LandCard"
            //         }
            //     },
                // new SpellCard()
                // {
                //     Color = CardColorsEnum.Blue,
                //     sucuk = "SpellCard"
                // }
            // };
            // foreach (var card in cards)
            // {
            //     if (card is LandCard)
            //     {
            //         LandCard landCard = (LandCard)card;
            //         Console.WriteLine(landCard.sucuk);
            //     }
            //     else if (card is SpellCard)
            //     {
            //         SpellCard landCard = (SpellCard)card;
            //         Console.WriteLine(landCard.sucuk);
            //     }
            // }
        }
    }
}