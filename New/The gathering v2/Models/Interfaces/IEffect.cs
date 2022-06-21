namespace The_gathering_v2.Models;

public interface IEffect
{
    public int Turns { get; set; }
    public string? Description { get; set; }
    public Play? Play { get; set; }
    public void Effect();
    public bool PreparationPhase { get; set; }
}