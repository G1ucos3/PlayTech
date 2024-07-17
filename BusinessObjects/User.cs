using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? UserEmail { get; set; }

    public int UserBalance { get; set; }

    public string UserPassword { get; set; } = null!;

    public int UserRoles { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();
}
