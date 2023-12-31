﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.DTO;
using MainBackend.Exceptions;
using MainBackend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace MainBackend.Services.Classes;

public class UserService : IUserService
{
    private IRepositoryWrapper repositoryWrapper;
    private IConfiguration configuration;

#region Constructors

    public UserService(IRepositoryWrapper repositoryWrapper, IConfiguration configuration)
    {
        this.repositoryWrapper = repositoryWrapper;
        this.configuration = configuration;
    }

#endregion

#region Get

    public async Task<User> GetUser(int id)
    {
        return await repositoryWrapper.normalDbWrapper.user.Get(id);
    }

    public async Task<ICollection<User>> GetUsers()
    {
        IEnumerable<User> allUsers = await repositoryWrapper.normalDbWrapper.user.GetAll();
        return (ICollection<User>)allUsers;
    }

    public async Task<ICollection<User>> GetUsers(int usersPerPage, int currentPage)
    {
        IEnumerable<User> allUsers = await repositoryWrapper.normalDbWrapper.user.GetAll();
        int startIndex = (currentPage - 1) * usersPerPage;
        var paginatedUsers = allUsers.Skip(startIndex).Take(usersPerPage);
        return (ICollection<User>)paginatedUsers.ToList();
    }

    public async Task<ICollection<Worker>> GetWorkers()
    {
        IEnumerable<Worker> workers = await repositoryWrapper.normalDbWrapper.worker.GetAll();
        return (ICollection<Worker>)workers;
    }

    public async Task<ICollection<User>> GetClients()
    {
        return GetUsers().Result.Where(x => x.IsClient).ToList();
    }

#endregion

#region Create

    public async Task<bool> RegisterClient(RegisterForm registerForm)
    {
        User user = await repositoryWrapper.normalDbWrapper.user.GetUser(registerForm.Login);
        if (user != null)
            return false;
        Person person = await repositoryWrapper.normalDbWrapper.person.GetPerson(registerForm.Email);

        try
        {
            await CheckRegisterForm(registerForm);
        }
        catch (Exception ex)
        {
            return false;
        }

        registerForm.Password = HashPassword(registerForm.Password);

        // This person doesn't exist
        if (person == null)
        {
            Person newPerson = new Person(registerForm);
            User newUser = new User(registerForm);
            newUser.Person = newPerson;
            Client client = new Client();
            client.Person = newPerson;
            client.User = newUser;
            repositoryWrapper.normalDbWrapper.person.Create(newPerson);
            repositoryWrapper.normalDbWrapper.user.Create(newUser);
            repositoryWrapper.normalDbWrapper.client.Create(client);
            return await repositoryWrapper.normalDbWrapper.Save(3);
        }
        // Person exists in database
        else
        {
            if (person.Client != null)
                return false;
            User newUser = new User(registerForm);
            newUser.Person = person;
            Client client = new Client();
            client.Person = person;
            client.User = newUser;
            repositoryWrapper.normalDbWrapper.user.Create(newUser);
            repositoryWrapper.normalDbWrapper.client.Create(client);
            return await repositoryWrapper.normalDbWrapper.Save(2);
        }

        return true;
    }

    public async Task<bool> RegisterClientGoogle(RegisterFormGoogle registerForm)
    {
        User user = await repositoryWrapper.normalDbWrapper.user.GetUser(registerForm.Email);
        if (user != null)
            return false;
        Person person = await repositoryWrapper.normalDbWrapper.person.GetPerson(registerForm.Email);
        if (person == null)
        {
            Person newPerson = new Person(registerForm);
            User newUser = new User(registerForm);
            newUser.Person = newPerson;
            Client client = new Client();
            client.Person = newPerson;
            client.User = newUser;
            repositoryWrapper.normalDbWrapper.person.Create(newPerson);
            repositoryWrapper.normalDbWrapper.user.Create(newUser);
            repositoryWrapper.normalDbWrapper.client.Create(client);
            return await repositoryWrapper.normalDbWrapper.Save(3);
        }
        else
        {
            if (person.Client != null)
                return false;
            User newUser = new User(registerForm);
            newUser.Person = person;
            Client client = new Client();
            client.Person = person;
            client.User = newUser;
            repositoryWrapper.normalDbWrapper.user.Create(newUser);
            repositoryWrapper.normalDbWrapper.client.Create(client);
            return await repositoryWrapper.normalDbWrapper.Save(2);
        }

        return false;
    }

    public async Task<bool> RegisterWorker(RegisterForm registerForm)
    {
        User user = await repositoryWrapper.normalDbWrapper.user.GetUser(registerForm.Login);
        if (user != null)
            return false;
        Person person = await repositoryWrapper.normalDbWrapper.person.GetPerson(registerForm.Email);

        await CheckRegisterForm(registerForm);

        registerForm.Password = HashPassword(registerForm.Password);

        if (person == null)
        {
            Person newPerson = new Person(registerForm);
            User newUser = new User(registerForm, false);
            newUser.Person = newPerson;
            Worker worker = new Worker();
            worker.Person = newPerson;
            worker.User = newUser;
            repositoryWrapper.normalDbWrapper.person.Create(newPerson);
            repositoryWrapper.normalDbWrapper.user.Create(newUser);
            repositoryWrapper.normalDbWrapper.worker.Create(worker);
            return await repositoryWrapper.normalDbWrapper.Save(3);
        }
        else
        {
            if (person.Worker != null)
                return false;
            User newUser = new User(registerForm, false);
            newUser.Person = person;
            Worker worker = new Worker();
            worker.Person = person;
            worker.User = newUser;
            repositoryWrapper.normalDbWrapper.user.Create(newUser);
            repositoryWrapper.normalDbWrapper.worker.Create(worker);
            return await repositoryWrapper.normalDbWrapper.Save(2);
        }

        return true;
    }

    private async Task<bool> CheckRegisterForm(RegisterForm form)
    {
        if (form.Login.Length < 3)
            throw new Exception("Login too short");

        if (form.Password.Length < 4)
            throw new Exception("Password too short");

        if (form.FirstName.Length < 3)
            throw new Exception("First name is too short");

        if (form.LastName.Length < 3)
            throw new Exception("Last name is too short");

        if (!form.Email.Contains("@"))
            throw new Exception("Email doesn't contain @");
        return true;
    }

    public async Task<User> Login(LoginForm loginForm)
    {
        foreach (var user in await GetUsers())
        {
            if ((user.Login == loginForm.Login) && (user.Password == HashPassword(loginForm.Password)) &&
                user.IsActive && !user.IsGoogle)
            {
                return user;
            }
        }

        return null;
    }

    public async Task<User> LoginGoogle(string email)
    {
        var users = await GetUsers();
        users = users.Where(x => x.IsGoogle == true).ToList();
        var user = users.FirstOrDefault(x => x.Login == email);
        return user;
    }

    public async Task<string> GenerateToken(User user)
    {
        var issuer = configuration["JwtSettings:Issuer"];
        var audience = configuration["JwtSettings:Audience"];
        var key = configuration["JwtSettings:Key"];
        var expiration = DateTime.UtcNow.AddHours(8);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "Client")
        };

        if (!user.IsClient)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Worker"));
            User workerUser = await repositoryWrapper.normalDbWrapper.user.GetWorker(user.Id);
            if (workerUser.Person.Worker.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            //issuer: issuer,
            //audience: audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

#endregion

#region Put

    public async Task<bool> ChangeToAdmin(int workerId, bool isAdmin)
    {
        Worker worker = await repositoryWrapper.normalDbWrapper.worker.Get(workerId);
        if (worker == null)
            return false;
        worker.IsAdmin = isAdmin;
        repositoryWrapper.normalDbWrapper.worker.Edit(worker);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

    public async Task<bool> ChangePassword(int userId, string newPassword)
    {
        User user = await repositoryWrapper.normalDbWrapper.user.Get(userId);
        if (user == null)
            return false;
        user.Password = HashPassword(newPassword);
        repositoryWrapper.normalDbWrapper.user.Edit(user);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

    public async Task<bool> Deactivate(int workerId, bool deactivate)
    {
        User user = await repositoryWrapper.normalDbWrapper.user.Get(workerId);
        if (user == null)
            return false;
        user.IsActive = deactivate;
        repositoryWrapper.normalDbWrapper.user.Edit(user);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

    public async Task<bool> ChangeUser(int userId, EditUserForm user)
    {
        User oldUser = await repositoryWrapper.normalDbWrapper.user.Get(userId);
        if (oldUser == null)
            return false;
        oldUser.Login = user.Login;
        oldUser.Password = HashPassword(user.Password);
        oldUser.Person.FirstName = user.FirstName;
        oldUser.Person.LastName = user.LastName;
        oldUser.Person.Email = user.Email;
        oldUser.Person.DateOfBirth = user.DateOfBirth;
        oldUser.IsActive = user.IsActive;
        repositoryWrapper.normalDbWrapper.user.Edit(oldUser);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

#endregion

#region Delete

    public async Task<bool> DeleteUser(int id)
    {
        if (id <= 0)
            return false;
        User user = await repositoryWrapper.normalDbWrapper.user.Get(id);
        if (user == null)
            return false;
        Person person = user.Person;
        if (user.IsClient)
        {
            Client client = user.Person.Client;
            if (client != null)
            {
                repositoryWrapper.normalDbWrapper.client.Delete(client);
                repositoryWrapper.normalDbWrapper.user.Delete(user);
            }
        }
        else
        {
            Worker worker = user.Person.Worker;
            if (worker != null)
            {
                repositoryWrapper.normalDbWrapper.worker.Delete(worker);
                repositoryWrapper.normalDbWrapper.user.Delete(user);
            }
        }

        if (person.Client == null && person.Worker == null)
        {
            repositoryWrapper.normalDbWrapper.person.Delete(user.Person);
            return await repositoryWrapper.normalDbWrapper.Save(3);
        }

        return await repositoryWrapper.normalDbWrapper.Save(2);
    }

#endregion

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}