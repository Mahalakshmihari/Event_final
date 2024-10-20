using System;
using System.Collections.Generic;

namespace EventManagement_Backend.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Event>? Events { get; set; } = new List<Event>();
}
