using System.Collections;
using The_gathering_v2.Models.Cards;
using The_gathering_v2.Models.Colors;
using The_gathering_v2.Models.Effects;

namespace The_gathering_v2.Models
{
    public class Game
    {
        public int CurrentTurn { get; set; }
        public Player? Attacker { get; set; }
        public Player? Defender { get; set; }
        public Stack<ICard>? InterruptionStack { get; set; }

        public void CreateCurrentState()
        {
            CurrentTurn = 2;
            Attacker = new Player()
            {
                Life = 10,
                DiscardPile = new DiscardPile()
                {
                    Cards = new List<ICard>()
                },
                Hand = new Hand()
                {
                    Cards = new List<ICard>()
                    {
                        new Creature()
                        {
                            Color = new Blue(),
                            Effect = new RemoveRandomCard()
                        },
                        new LandCard()
                        {
                            Color = new Blue()
                        },
                        new Creature()
                        {
                            Color = new Red()
                        }
                    }
                },
                Board = new Board()
                {
                    Cards = new List<ICard>()
                    {
                        new LandCard()
                        {
                            Color = new Blue()
                        },
                        new LandCard()
                        {
                            Color = new Blue()
                        }
                    }
                },
                

            };
        }
    }
}