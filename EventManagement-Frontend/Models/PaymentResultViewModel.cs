namespace EventManagement_Frontend.Models
{
    public class PaymentResultViewModel
    {
        public int PaymentId { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public bool Success { get; internal set; }
        public string Message { get; internal set; }
    }
}