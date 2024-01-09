using System;
using System.Collections.Generic;

namespace InventoryRepo.Models;

public partial class OrderedProduct
{
    public int OrderedProductId { get; set; }

    public int? CustomerOrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? GstRate { get; set; }

    public decimal? GrandTotal { get; set; }

    public string? ProductName { get; set; }

    public decimal? Total { get; set; }

    public Product Product { get; set; }
    public virtual CustomerOrder? CustomerOrder { get; set; }
}
