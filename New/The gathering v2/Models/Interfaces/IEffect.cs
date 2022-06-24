/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

namespace The_gathering_v2.Models;

public interface IEffect
{
    public int Turns { get; set; }
    public string? Description { get; set; }
    public void Effect(GeneralBoard generalBoard, Player player);
    public void Use(GeneralBoard generalBoard, Player player);
    public void OnDestroy(GeneralBoard generalBoard);
    public bool PreparationPhase { get; set; }
}