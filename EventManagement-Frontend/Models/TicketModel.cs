using System.ComponentModel.DataAnnotations;

namespace EventManagement_Frontend.Models
{
    public class TicketModel
    {
        public int TicketId { get; set; }
        [Required(ErrorMessage = "Booking ID is required")]
        public int BookingID { get; set; }

        [Required(ErrorMessage = "Seat number is required")]
        public int SeatId { get; set; }

        [Required(ErrorMessage = "Number of ticket is required")]
        public int NumberOfTickets { get; set; }

        [Required(ErrorMessage = "Total Amount is required")]
        
        public decimal TotalAmount { get; set; }
    }
}
