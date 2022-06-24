/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

using The_gathering_v2.Models.PlayerState;

namespace The_gathering_v2.Models
{
    public class Player
    {

        public int Life { get; set; } = 10;
        public string? Name {get; set;}
        public Hand? Hand { get; set; }
        public DiscardPile? DiscardPile { get; set; } = new DiscardPile();
        public Deck? Deck { get; set; }
        public Board? Board { get; set; }
        public IPlayerState? PlayerState { get; set; }
        public List<EnergyReserve> EnergyReserve { get; set; } = new List<EnergyReserve>();
        public bool GameOver { get; set; } = false;

        public void ChangePlayerState()
        {
            if (PlayerState is Attacker)
            {
                PlayerState = new Defender();
            }
            else
            {
                PlayerState = new Attacker();
            }
        }
    }
}