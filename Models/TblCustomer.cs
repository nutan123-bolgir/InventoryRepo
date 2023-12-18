using System;
using System.Collections.Generic;

namespace InventoryRepo.models;

public partial class TblCustomer
{
    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public decimal? Creditlimit { get; set; }

    public bool? Isactive { get; set; }

    public string? Taxcode { get; set; }
}
