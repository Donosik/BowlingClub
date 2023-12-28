using System.ComponentModel.DataAnnotations;
using MainBackend.DTO;

namespace MainBackend.Databases.BowlingDb.Entities;

public class Person : IEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public virtual Client? Client { get; set; }
    public virtual Worker? Worker { get; set; }

    public Person()
    {
        Client = null;
        Worker = null;
    }
    public Person(RegisterForm registerForm)
    {
        FirstName = registerForm.FirstName;
        LastName = registerForm.LastName;
        Email = registerForm.Email;
        DateOfBirth = registerForm.DateOfBirth;
        Client = null;
        Worker = null;
    }
    
    public Person(RegisterFormGoogle registerForm)
    {
        FirstName = registerForm.FirstName;
        LastName = registerForm.LastName;
        Email = registerForm.Email;
        DateOfBirth = DateTime.MinValue;
        Client = null;
        Worker = null;
    }
}