using System;
using System.Collections.Generic;

namespace EventManagement_Backend.Models;

public partial class Event
{
    public int EventId { get; set; }

    public int? CategoryId { get; set; }

    public string EventName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Location { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = null!;

    public decimal Rating { get; set; }

    public int TotalTickets { get; set; }

    public int AvailableTickets { get; set; }

    public virtual ICollection<Booking>? Bookings { get; set; } = new List<Booking>();

    public virtual Category? Category { get; set; } = null!;

    public virtual ICollection<Payment>? Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Seat>? Seats { get; set; } = new List<Seat>();
}
