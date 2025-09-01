using System;
using System.Collections.Generic;

namespace wheelzyTest.Modelos;

public partial class Location
{
    public int LocationId { get; set; }

    public string Zipcode { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public virtual ICollection<Amount> Amounts { get; set; } = new List<Amount>();

    public virtual ICollection<Buyer> Buyers { get; set; } = new List<Buyer>();

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
