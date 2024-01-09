using System;
using System.Collections.Generic;

namespace InventoryRepo.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
