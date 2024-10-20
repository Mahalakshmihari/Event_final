
using EventManagement_Backend.Models;

namespace EventManagement_Frontend.Models
{
    public class SeatModel
    {
        public int SeatId { get; set; }

        public int EventId { get; set; }

        public string SeatNumber { get; set; } = null!;

        public bool? IsAvailble { get; set; }

       public virtual Event? Event { get; set; } = null!;
    }
}
