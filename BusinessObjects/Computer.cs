using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Computer
{
    public int ComputerId { get; set; }

    public string ComputerName { get; set; } = null!;

    public bool ComputerStatus { get; set; }

    public virtual ICollection<CurrentComputer> CurrentComputers { get; set; } = new List<CurrentComputer>();
}
