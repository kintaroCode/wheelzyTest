using System;
using System.Collections.Generic;

namespace wheelzyTest.Modelos;

public partial class Car
{
    public int CarId { get; set; }

    public string Plate { get; set; } = null!;

    public int Year { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string? Submodel { get; set; }

    public string? Status { get; set; }

    public int? LocationId { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<StatusLog> StatusLogs { get; set; } = new List<StatusLog>();
}
