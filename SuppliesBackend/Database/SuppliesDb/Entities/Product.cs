﻿using SuppliesBackend.Database.Generic.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Entities;

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Order Order { get; set; }
}