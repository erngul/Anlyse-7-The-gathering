/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

using The_gathering_v2.Models.Cards;

namespace The_gathering_v2.Models;

public class InterruptionStack
{
    public InterruptionStack( IEffect cards, Player player)
    {
        Player = player;
        Cards = cards;
    }

    public Player Player { get; set; }
    public IEffect Cards { get; set; }
}