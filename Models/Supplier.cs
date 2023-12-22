using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryRepo.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? SupplierName { get; set; }

    public string? ContactPerson { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public string? SupplierPhoto { get; set; }
    [NotMapped]
    public IFormFile? file { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
