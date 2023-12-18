using System;
using System.Collections.Generic;

namespace InventoryRepo.Models;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public int? OrderId { get; set; }

    public string? Status { get; set; }

    public virtual Order? Order { get; set; }
}
