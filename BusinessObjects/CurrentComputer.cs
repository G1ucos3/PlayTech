using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class CurrentComputer
{
    public int UserId { get; set; }

    public int ComputerId { get; set; }

    public DateTime? StartTime { get; set; }

    public virtual Computer Computer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
