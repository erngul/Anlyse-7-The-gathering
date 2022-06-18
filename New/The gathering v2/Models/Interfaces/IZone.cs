using System.Collections.Generic;

namespace The_gathering_v2.Models
{
    public interface IZone
    {
        public List<ICard>? Cards { get; set; }
    }
}