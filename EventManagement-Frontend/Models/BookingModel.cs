namespace EventManagement_Frontend.Models
{
    public class BookingModel
    {
        public int BookingId { get; set; }
        public string? UserId { get; set; } = null;

        public DateTime BookingDate { get; set; }

        public bool IsBooked { get; set; }
    }
}
