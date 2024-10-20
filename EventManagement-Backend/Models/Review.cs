using System;
using System.Collections.Generic;

namespace EventManagement_Backend.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public string? UserId { get; set; } = null!;

    public int? EventId { get; set; }

    public decimal Rating { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime ReviewDate { get; set; }

    public virtual Event? Event { get; set; } = null!;

    public virtual AspNetUser? User { get; set; } = null!;
}
