using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SuppliesBackend.Database.SuppliesDb.Entities;
using SuppliesBackend.Database.SuppliesDb.RepositoryWrapper;
using SuppliesBackend.DTO;
using SuppliesBackend.Services.Interfaces;

namespace SuppliesBackend.Services.Classes;

public class UserService : IUserService
{
    private readonly IRepositoryWrapper repositoryWrapper;
    private readonly IConfiguration configuration;

    public UserService(IRepositoryWrapper repositoryWrapper,IConfiguration configuration)
    {
        this.repositoryWrapper = repositoryWrapper;
        this.configuration = configuration;
    }


    public async Task<bool> Register(LoginForm loginForm)
    {
        var users = await repositoryWrapper.user.GetAll();
        var existingUser = users.FirstOrDefault(x => x.Login == loginForm.Login);
        if (existingUser != null)
            return false;
        User newUser = new User();
        newUser.Login = loginForm.Login;
        newUser.Password = HashPassword(loginForm.Password);
        repositoryWrapper.user.Create(newUser);
        return await repositoryWrapper.Save();
    }

    public async Task<User> Login(LoginForm loginForm)
    {
        var allUsers = await repositoryWrapper.user.GetAll();
        foreach (var user in allUsers)
        {
            if (user.Login == loginForm.Login && user.Password == HashPassword(loginForm.Password))
                return user;
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
            new Claim(ClaimTypes.Role, "Client")
        };
        
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

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}