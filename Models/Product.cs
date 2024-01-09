using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryRepo.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int CategoryId { get; set; }

    public decimal? Price { get; set; }

    public int? StockQuantity { get; set; }

    public bool IsActive { get; set; }

    public string? ProductImage { get; set; }
    [NotMapped]
    public IFormFile? file { get; set; }
    public decimal? Gstrate { get; set; }

    public string? Code { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
