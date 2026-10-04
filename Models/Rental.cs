using System;
using System.Collections.Generic;

namespace lab_3.Models;

public partial class Rental
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int ProductId { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Product? Product { get; set; }
}
