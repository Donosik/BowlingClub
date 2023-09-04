using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.DTO;
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

    public Task<ICollection<User>> GetUsers()
    {
        throw new NotImplementedException();
    }

#endregion

#region Create

    public async Task<bool> RegisterClient(RegisterForm registerForm)
    {
        User user = await repositoryWrapper.normalDbWrapper.user.GetUser(registerForm.Login);
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
            repositoryWrapper.normalDbWrapper.user.Create(newUser);
            repositoryWrapper.normalDbWrapper.client.Create(client);
            return await repositoryWrapper.normalDbWrapper.Save(2);
        }

        return true;
    }

    public async Task<bool> RegisterWorker(RegisterForm registerForm)
    {
        User user = await repositoryWrapper.normalDbWrapper.user.GetUser(registerForm.Login);
        if (user != null)
            return false;
        Person person = await repositoryWrapper.normalDbWrapper.person.GetPerson(registerForm.Email);
        if (person == null)
        {
            Person newPerson = new Person(registerForm);
            User newUser = new User(registerForm);
            newUser.Person = newPerson;
            Worker worker = new Worker();
            worker.Person = newPerson;
            repositoryWrapper.normalDbWrapper.person.Create(newPerson);
            repositoryWrapper.normalDbWrapper.user.Create(newUser);
            repositoryWrapper.normalDbWrapper.worker.Create(worker);
            return await repositoryWrapper.normalDbWrapper.Save(3);
        }
        else
        {
            if (person.Client != null)
                return false;
            User newUser = new User(registerForm);
            newUser.Person = person;
            newUser.IsClient = false;
            Worker worker = new Worker();
            worker.Person = person;
            repositoryWrapper.normalDbWrapper.user.Create(newUser);
            repositoryWrapper.normalDbWrapper.worker.Create(worker);
            return await repositoryWrapper.normalDbWrapper.Save(2);
        }

        return true;
    }

    public async Task<User> Login(LoginForm loginForm)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateToken(User user)
    {
        var issuer = configuration["JwtSettings:Issuer"];
        var audience = configuration["JwtSettings:Audience"];
        var key = configuration["JwtSettings:Key"];
        var expiration = DateTime.UtcNow.AddHours(8);

        //TODO: Claims to add
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "Test")
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
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
        await repositoryWrapper.normalDbWrapper.user.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

#endregion
}