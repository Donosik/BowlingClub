﻿namespace MainBackend.DTO;

public class EditUserForm
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
}