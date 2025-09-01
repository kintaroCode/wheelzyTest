using System;
using System.Collections.Generic;

namespace wheelzyTest.Modelos;

public partial class StatusLog
{
    public int StatusId { get; set; }

    public int BuyerId { get; set; }

    public int CarId { get; set; }

    public string? Status { get; set; }

    public DateTime? StatusDate { get; set; }

    public string? ChangeBy { get; set; }

    public virtual Buyer Buyer { get; set; } = null!;

    public virtual Car Car { get; set; } = null!;
}
