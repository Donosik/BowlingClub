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

#region Create

    public async Task<bool> Register(RegisterForm registerForm)
    {
        throw new NotImplementedException();
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
}