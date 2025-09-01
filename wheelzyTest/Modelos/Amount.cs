using System;
using System.Collections.Generic;

namespace wheelzyTest.Modelos;

public partial class Amount
{
    public int AmountId { get; set; }

    public int? BuyerId { get; set; }

    public int? LocationId { get; set; }

    public decimal? Amount1 { get; set; }

    public virtual Buyer? Buyer { get; set; }

    public virtual Location? Location { get; set; }
}
