using System;
using System.Collections.Generic;

namespace InventoryRepo.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? CategoryId { get; set; }

    public decimal? Price { get; set; }

    public int? StockQuantity { get; set; }

    public bool? IsActive { get; set; }

    public string? ProductImage { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
