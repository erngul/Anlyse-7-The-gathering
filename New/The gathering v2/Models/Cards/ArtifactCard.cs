/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

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

        public override void Use(GeneralBoard generalBoard, Player player)
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
                        generalBoard.InterruptionStack.Push(new InterruptionStack(effect, generalBoard.Attacker));
                        Console.WriteLine(
                            $"Card has the effect {effect.Description} and is added to the interruption stack");
                    }
                }
            }
        }

        private bool UseColourlessEnergy(Player player, GeneralBoard generalBoard)
        {
            var energyReserve = player.EnergyReserve.FirstOrDefault(e => e.Amount >= CostToBePlayed);
            if (energyReserve != null)
            {
                energyReserve.Amount -= CostToBePlayed;
                return true;
            }



            for (int i = 0; i < CostToBePlayed; i++)
            {
                var landCard = player.Board.Cards.FirstOrDefault(c => c is LandCard && c.Used == false);
                if (landCard == null)
                {
                    Console.WriteLine($"Not enough energy in landcards and energy reserve");
                }
                generalBoard.UseLandEnergy(player, landCard.Color);
            }

            return UseColourlessEnergy(player, generalBoard);
        }
    }
}