using MainBackend.Services.Wrapper;

namespace MainBackend.Services.Interfaces;

public interface IGeneratorService
{
    Task GenerateUsers(int howManyUsersToGenerate);
    Task GenerateShifts(int normalDayShifts, int weekendShifts);
    Task GenerateLanes(int howManyLanes);
}