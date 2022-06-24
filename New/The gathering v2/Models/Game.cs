/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

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
        public bool AllPlayersPlayed { get; set; } = false;
        public Player? PlayerA { get; set; }
        public Player? PlayerB { get; set; }
        public GeneralBoard GeneralBoard { get; set; }
        

        public void CreateCurrentState()
        {
            CurrentTurn = 2;
            PlayerA = new Player()
            {
                Name = "PlayerA",
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
                            Effect = new EnemyRemovesRandomCard(),
                            AttackValue = 2,
                            DefenceValue = 2,
                            CostToBePlayed = 2
                        },
                        new LandCard()
                        {
                            Color = new Blue()
                        },
                        new Creature()
                        {
                            Color = new Blue(),
                            Effect = new EnemyRemovesRandomCard(),
                            AttackValue = 2,
                            DefenceValue = 2,
                            CostToBePlayed = 2
                        },
                        new Creature()
                        {
                            Color = new Blue(),
                            Effect = new EnemyRemovesRandomCard(),
                            AttackValue = 2,
                            DefenceValue = 2,
                            CostToBePlayed = 2
                        },
                        new LandCard()
                        {
                            Color = new Blue()
                        },
                        new LandCard()
                        {
                            Color = new Red()
                        },
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
                Deck = new Deck()
                {
                    Cards = new Stack<ICard>()
                },
                
            };
            PlayerA.Deck.Cards.Push(new Creature()
            {
                Color = new Red(),
                Effect = new EnemyRemovesRandomCard(),
                AttackValue = 2,
                DefenceValue = 2,
                CostToBePlayed = 2
            });
            PlayerA.Deck.Cards.Push(new Creature()
            {
                Color = new Red(),
                Effect = new EnemyRemovesRandomCard(),
                AttackValue = 2,
                DefenceValue = 2,
                CostToBePlayed = 2
            });
            
            
            
            PlayerB = new Player()
            {
                Name = "PlayerB",
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
                        new ArtifactCard()
                        {
                            Effects = new List<IEffect>()
                            {
                                new AllEnemyCreaturesDealHalfDamage(),
                                new EnemySkipsDrawingPhase()
                                {
                                    Turns = 1
                                }
                            },
                            CostToBePlayed = 2
                        },
                        new ArtifactCard()
                        {
                            Effects = new List<IEffect>()
                            {
                                new AllEnemyCreaturesDealHalfDamage(),
                                new EnemySkipsDrawingPhase()
                                {
                                    Turns = 1
                                }
                            },
                            CostToBePlayed = 2
                        },
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
                        },
                        new LandCard()
                        {
                            Color = new Green()
                        },
                        new LandCard()
                        {
                            Color = new Green()
                        },
                    }
                },
                Deck = new Deck()
                {
                    Cards = new Stack<ICard>()
                },
                
            };
            PlayerB.Deck.Cards.Push(new Creature()
            {
                Color = new Red(),
                Effect = new EnemyRemovesRandomCard(),
                AttackValue = 2,
                DefenceValue = 2,
                CostToBePlayed = 2
            });
            PlayerB.Deck.Cards.Push(new Creature()
            {
                Color = new Red(),
                Effect = new EnemyRemovesRandomCard(),
                AttackValue = 2,
                DefenceValue = 2,
                CostToBePlayed = 2
            });
            GeneralBoard = new GeneralBoard()
            {
                Attacker = PlayerA,
                Defender = PlayerB
            };
        }

        public void StartGame()
        {
        }

        public void ChangePlayerState()
        {
            PlayerA.ChangePlayerState();
            PlayerB.ChangePlayerState();
            if (PlayerA.PlayerState is Attacker)
            {
                GeneralBoard.Attacker = PlayerA;
                GeneralBoard.Defender = PlayerB;
            }
            else
            {
                GeneralBoard.Attacker = PlayerB;
                GeneralBoard.Defender = PlayerA;
            }
        }
        
        public void NextTurn()
        {
            if (AllPlayersPlayed)
            {
                CurrentTurn++;
                Console.WriteLine("");
                Console.WriteLine($"Current turn: {CurrentTurn}");
                AllPlayersPlayed = false;
            }
            ChangePlayerState();
            Console.WriteLine("");
            Console.WriteLine($"The Attacker = {GeneralBoard.Attacker.Name} and the Defender = {GeneralBoard.Defender.Name}");
            Console.WriteLine("");
        }
        
    }
}