namespace EventManagement_Backend.PaymentDTOs
{
    public class ProcessPaymentDTO
    {
        public string UserId { get; set; }

        public int EventId { get; set; }

        public decimal TotalAmount { get; set; }

        public string CVV { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }
    }
}
