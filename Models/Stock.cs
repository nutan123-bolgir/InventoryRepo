using System;
using System.Collections.Generic;

namespace InventoryRepo.Models;

public partial class Stock
{
    public int StockId { get; set; }

    public int? ProductId { get; set; }

    public int? QuantityInStock { get; set; }

    public virtual Product? Product { get; set; }
}
