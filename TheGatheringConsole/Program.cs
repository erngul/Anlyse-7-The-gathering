using System;
using System.Collections.Generic;
using TheGatheringConsole.Models;
using TheGatheringConsole.Models.Enums;

namespace TheGatheringConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");
            List<Card[]> cards = new List<Card[]>()
            {
                new LandCard()
                {
                    Color = CardColorsEnum.Blue,
                    sucuk = "LandCard"
                },
                new SpellCard()
                {
                    Color = CardColorsEnum.Blue,
                    sucuk = "SpellCard"
                }
            };
            foreach (var card in cards)
            {
                if (card is LandCard)
                {
                    LandCard landCard = (LandCard)card;
                    Console.WriteLine(landCard.sucuk);
                }
                else if (card is SpellCard)
                {
                    SpellCard landCard = (SpellCard)card;
                    Console.WriteLine(landCard.sucuk);
                }
            }
        }
    }
}