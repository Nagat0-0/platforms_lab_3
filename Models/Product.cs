using System;
using System.Collections.Generic;

namespace lab_3.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Cost { get; set; }

    public decimal DailyRentalPrice { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
