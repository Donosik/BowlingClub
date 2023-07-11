﻿using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class User : IEntity
{
    [Key]
    public int Id { get; set; }
    public int Login { get; set; }
}