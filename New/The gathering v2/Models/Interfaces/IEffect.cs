namespace The_gathering_v2.Models;

public interface IEffect
{
    public int Turns { get; set; }
    public string? Description { get; set; }
}