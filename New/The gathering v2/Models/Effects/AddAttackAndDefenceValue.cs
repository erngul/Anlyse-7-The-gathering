namespace The_gathering_v2.Models.Effects;

public class AddAttackAndDefenceValue : IEffect
{
    public int Turns { get; set; }
    public string? Description { get; set; }
}