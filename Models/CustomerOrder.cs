using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace InventoryRepo.Models;

public partial class CustomerOrder
{
    public int CustomerOrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public string? PaymentType { get; set; }

    public decimal? GrandTotal { get; set; }

    public Customer  Customer { get; set; }

    public Product Product {  get; set; }
    public virtual ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
}
