using System;
using System.Collections.Generic;

namespace InventoryRepo.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? TotalPrice { get; set; }

    public int? SupplierorderId { get; set; }

    public decimal? Gstrate { get; set; }

    public virtual Supplierorder? Supplierorder { get; set; }
}
