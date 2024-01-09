using System;
using System.Collections.Generic;

namespace InventoryRepo.Models;

public partial class Supplierorder
{
    public int SupplierorderId { get; set; }

    public int? SupplierId { get; set; }

    public decimal? GrandTotal { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual Supplier? Supplier { get; set; }
}
