namespace EventManagement_Backend.PaymentDTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }

        public int EventId { get; set; }
        public string userId { get; set; }
        public bool IsBooked { get; set; }
    }
}
