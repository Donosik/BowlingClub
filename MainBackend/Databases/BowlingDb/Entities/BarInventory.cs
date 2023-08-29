﻿using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class BarInventory : IEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
    
    public virtual ICollection<Invoice> Invoices { get; set; }
}