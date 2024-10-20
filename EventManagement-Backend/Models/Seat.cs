using System;
using System.Collections.Generic;

namespace EventManagement_Backend.Models;

public partial class Seat
{
    public int SeatId { get; set; }

    public int EventId { get; set; }

    public string SeatNumber { get; set; } = null!;

    public bool? IsAvailble { get; set; }

    public virtual Event? Event { get; set; } = null!;
}
