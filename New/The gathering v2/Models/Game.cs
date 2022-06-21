using System.Collections;
using The_gathering_v2.Models.Cards;
using The_gathering_v2.Models.Colors;
using The_gathering_v2.Models.Effects;
using The_gathering_v2.Models.PlayerState;

namespace The_gathering_v2.Models
{
    public class Game
    {
        public int CurrentTurn { get; set; }
        public Player? Player1 { get; set; }
        public Player? Player2 { get; set; }
        public Stack<ICard>? InterruptionStack { get; set; }
        public Play Play { get; set; }
        

        public void CreateCurrentState()
        {
            Play = new Play();
            CurrentTurn = 2;
            Player1 = new Player()
            {
                Life = 10,
                PlayerState = new Attacker(),
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

        public void StartGame()
        {
            
        }

        public void ChangePlayerState()
        {
            Player1.ChangePlayerState();
            Player2.ChangePlayerState();
            if (Player1.PlayerState is Attacker)
            {
                Play.Attacker = Player1;
                Play.Defender = Player2;
            }
            else
            {
                Play.Attacker = Player2;
                Play.Defender = Player1;
            }
        }

        public void NextTurn()
        {
            CurrentTurn++;
            ChangePlayerState();
        }
        
    }
}