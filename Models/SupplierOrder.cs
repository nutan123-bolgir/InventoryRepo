using System;
using System.Collections.Generic;

namespace InventoryRepo.Models;

public partial class SupplierOrder
{
    public int SupplierOrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int SupplierId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
