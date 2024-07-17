using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductPrice { get; set; }

    public int ProductQuantity { get; set; }

    public string? ProductType { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
