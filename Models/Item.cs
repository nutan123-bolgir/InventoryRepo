﻿using System;
using System.Collections.Generic;

namespace InventoryRepo.Models;

public partial class Item
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
}
