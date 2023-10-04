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

    /*
    public virtual int? ClientUserId { get; set; }
    public virtual User? ClientUser { get; set; }
    public virtual int? WorkerUserId { get; set; }
    public virtual User? WorkerUser { get; set; }*/
    public virtual Client? Client { get; set; }
    public virtual Worker? Worker { get; set; }

    public Person()
    {
        //ClientUserId = null;
        //ClientUser = null;
        //WorkerUserId = null;
        //WorkerUser = null;
        Client = null;
        Worker = null;
    }
    public Person(RegisterForm registerForm)
    {
        FirstName = registerForm.FirstName;
        LastName = registerForm.LastName;
        Email = registerForm.Email;
        DateOfBirth = registerForm.DateOfBirth;
        //ClientUserId = null;
        //ClientUser = null;
        //WorkerUserId = null;
        //WorkerUser = null;
        Client = null;
        Worker = null;
    }
}