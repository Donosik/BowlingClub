using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

    public async Task<ICollection<User>> GetUsers()
    {
        IEnumerable<User> users = await repositoryWrapper.normalDbWrapper.user.GetAll();
        return (ICollection<User>)users;
    }

    public async Task<ICollection<Worker>> GetWorkers()
    {
        IEnumerable<Worker> workers = await repositoryWrapper.normalDbWrapper.worker.GetAll();
        return (ICollection<Worker>)workers;
    }

#endregion

#region Create

    public async Task<bool> RegisterClient(RegisterForm registerForm)
    {
        User user = await repositoryWrapper.normalDbWrapper.user.GetUser(registerForm.Login);
        if (user != null)
            throw new LoginAlreadyExistsException("User with that login already exists");
        Person person = await repositoryWrapper.normalDbWrapper.person.GetPerson(registerForm.Email);
        try
        {
            await CheckRegisterForm(registerForm);
        }
        catch (Exception ex)
        {
            throw;
        }

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
                throw new PersonAlreadyHasAccountException("This person already has an client account");
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

    public async Task<bool> RegisterWorker(RegisterForm registerForm)
    {
        User user = await repositoryWrapper.normalDbWrapper.user.GetUser(registerForm.Login);
        if (user != null)
            return false;
        Person person = await repositoryWrapper.normalDbWrapper.person.GetPerson(registerForm.Email);
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

        if (form.Password.Length < 8)
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
            if ((user.Login == loginForm.Login) && (user.Password == loginForm.Password))
            {
                return user;
            }
        }

        return null;
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
            new Claim(ClaimTypes.Role, "User")
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