namespace The_gathering_v2.Models.Cards
{
    public class Creature : PermanentCard
    {
        public int AttackValue { get; set; }
        public int DefenceValue { get; set; }
        public override Color? Color { get; set; }
        public IEffect? Effect { get; set; }

        public override void Activate(GeneralBoard generalBoard)
        {
            var defencebleCards = generalBoard.Defender.Board.Cards.Where(c => c is Creature creature).ToList();
            if (defencebleCards.Count > 0)
            {
                if(defencebleCards[0] is Creature creature)
                {
                    creature.DefenceValue -= AttackValue;
                    if (creature.DefenceValue <= 0)
                    {
                        creature.OnDestroy(generalBoard, generalBoard.Defender);
                        generalBoard.Defender.Board.Cards.Remove(creature);
                        return;
                    }
                }
            }
            generalBoard.Defender.Life -= AttackValue;
        }

        public override void OnDestroy(GeneralBoard generalBoard, Player player)
        {
            player.DiscardPile.Cards.Add(this);
        }

        public override void Use(GeneralBoard generalBoard)
        {
            Used = true;
        }

        public override void Reset()
        {
            if (Used)
            {
                Used = false;
                Console.WriteLine("Creature restored.");
            }
        }

        public override void PlaceOnBoard(GeneralBoard generalBoard)
        {
            if (generalBoard.consumeEnergyForSpellCard(generalBoard.Attacker, this))
            {
                generalBoard.Attacker.Board.Cards.Add(this);
                generalBoard.Attacker.Hand.Cards.Remove(this);
                Console.WriteLine(
                    $"Places 1 Creature with {AttackValue} attack value and {DefenceValue} Defence value. On the board.");
                if (Effect != null)
                {
                    Effect.Use(generalBoard);
                    Console.WriteLine(
                        $"Card has the effect {Effect.Description} and is added to the interruption stack");
                }
            }
        }
    }
}