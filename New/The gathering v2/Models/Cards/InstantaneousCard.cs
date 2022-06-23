namespace The_gathering_v2.Models.Cards
{
    public class InstantaneousCard : ISpellCard
    {
        public Color? Color { get; set; }
        public bool Used { get; set; }
        public IEffect Effect { get; set; }
        public int CostToBePlayed { get; set; }
        public void OnDestroy(GeneralBoard generalBoard, Player player)
        {
            throw new NotImplementedException();
        }

        public void Use(GeneralBoard generalBoard)
        {
            generalBoard.InterruptionStack.Cards.Push(Effect);
            generalBoard.Attacker.Hand.Cards.Remove(this);
            Console.WriteLine("Plays 1 InstantaneousCard.");
            Used = true;
        }

        public void Reset()
        {
            Used = false;
        }
    }
}