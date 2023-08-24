﻿using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class BarInventory : IEntity
{
    public int id { get; set; }
    
    public string name { get; set; }
    public int quantity { get; set; }
    public float price { get; set; }

#region Constructors

    public BarInventory()
    {
        name = "";
        quantity = 0;
        price = 0;
    }

#endregion
}