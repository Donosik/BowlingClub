namespace MainBackend.Services.Interfaces;

public interface IUserService
{
    Task Create(int login);
}