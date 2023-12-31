﻿using System.ComponentModel.DataAnnotations;
using MainBackend.DTO;

namespace MainBackend.Databases.BowlingDb.Entities;

public class User : IEntity
{
    public int Id { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }
    public bool IsClient { get; set; }
    public bool IsActive { get; set; }
    public bool IsGoogle { get; set; }

    public virtual Person Person { get; set; }

    public User()
    {
    }

    public User(RegisterForm registerForm, bool isClient = true)
    {
        Login = registerForm.Login;
        Password = registerForm.Password;
        IsClient = isClient;
        IsActive = true;
        IsGoogle = false;
    }
    
    public User(RegisterFormGoogle registerForm)
    {
        Login = registerForm.Email;
        Password = "";
        IsClient = true;
        IsActive = true;
        IsGoogle = true;
    }
}