using The_gathering_v2.Models.Colors;

namespace The_gathering_v2.Models.Cards
{
    public class ArtifactCard : PermanentCard
    {
        public override Color? Color { get; set; } = new Neutral();
        public IEffect? Effect { get; set; }
        public List<IEffect>? Effects { get; set; }

        public override void Activate(GeneralBoard generalBoard)
        {
            throw new NotImplementedException();
        }

        public override void OnDestroy(GeneralBoard generalBoard, Player player)
        {
            throw new NotImplementedException();
        }

        public override void Use(GeneralBoard generalBoard)
        {
            Used = true;
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void PlaceOnBoard(GeneralBoard generalBoard)
        {
            if (UseColourlessEnergy(generalBoard.Attacker, generalBoard))
            {
                generalBoard.Attacker.Board.Cards.Add(this);
                generalBoard.Attacker.Hand.Cards.Remove(this);
                Console.WriteLine("Plays 1 permanent.");
                Console.WriteLine($"Artifact has {Effects.Count} effects");

                if (Effects != null)
                {
                    foreach (var effect in Effects)
                    {
                        generalBoard.InterruptionStack.Cards.Push(effect);
                        Console.WriteLine(
                            $"Card has the effect {effect.Description} and is added to the interruption stack");
                    }
                }
            }
        }

        private bool UseColourlessEnergy(Player player, GeneralBoard generalBoard)
        {
            bool result = false;
            for (int i = 0; i < CostToBePlayed; i++)
            {
                
            var energyReserve = player.EnergyReserve.FirstOrDefault(e => e.Amount >= CostToBePlayed);
            var landCard = player.Board.Cards.FirstOrDefault(c => c is LandCard && c.Used == false);
            if (energyReserve != null)
            {
                energyReserve.Amount -= CostToBePlayed; 
            }
            else if(landCard != null)
            {
                var energyReserveCard = player.EnergyReserve.FirstOrDefault(e => e.Color.Name == landCard.Color.Name);
                    if (energyReserveCard == null)
                    {
                        energyReserveCard = new EnergyReserve()
                        {
                            Amount = 0,
                            Color = landCard.Color
                        };
                        player.EnergyReserve.Add(energyReserveCard);
                    }
                    energyReserveCard.Amount++;
                    landCard.Use(generalBoard);
            }
            else
            {
                return false;
            }

            }
            return true;
        }
    }
}