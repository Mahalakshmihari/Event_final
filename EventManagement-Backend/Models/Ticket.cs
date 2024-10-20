using System;
using System.Collections.Generic;

namespace EventManagement_Backend.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int SeatId { get; set; }

    public int BookingId { get; set; }

    public int NumberOfTickets { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Booking? Booking { get; set; } = null!;
}
