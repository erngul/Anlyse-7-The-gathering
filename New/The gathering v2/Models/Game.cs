namespace The_gathering_v2.Models
{
    public class Game
    {
        public int CurrentTurn { get; set; }
        public Player? Attacker { get; set; }
        public Player? Defender { get; set; }
    }
}