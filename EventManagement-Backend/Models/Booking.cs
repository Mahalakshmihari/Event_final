using System;
using System.Collections.Generic;

namespace EventManagement_Backend.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? EventId { get; set; }

    public string? UserId { get; set; } = null!;

    public DateTime BookingDate { get; set; }

    public bool IsBooked { get; set; }

    public virtual Event? Event { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual AspNetUser? User { get; set; } = null!;
}
