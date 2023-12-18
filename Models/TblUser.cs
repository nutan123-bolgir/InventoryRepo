using System;
using System.Collections.Generic;

namespace InventoryRepo.models;

public partial class TblUser
{
    public int Code { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? Isactive { get; set; }

    public string? Role { get; set; }
}
