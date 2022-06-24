/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

namespace The_gathering_v2.Models
{
    public interface ICard
    {
        public Color? Color { get; set; }
        public bool Used { get; set; }
        public void Use(GeneralBoard generalBoard, Player player);
        public void Reset();
    }
}