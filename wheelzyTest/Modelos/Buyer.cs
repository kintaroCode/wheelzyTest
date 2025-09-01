using System;
using System.Collections.Generic;

namespace wheelzyTest.Modelos;

public partial class Buyer
{
    public int BuyerId { get; set; }

    public string? Name { get; set; }

    public byte? Calification { get; set; }

    public int? LocationId { get; set; }

    public virtual ICollection<Amount> Amounts { get; set; } = new List<Amount>();

    public virtual Location? Location { get; set; }

    public virtual ICollection<StatusLog> StatusLogs { get; set; } = new List<StatusLog>();
}
