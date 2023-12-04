using MainBackend.Services.Wrapper;

namespace MainBackend.Services.Interfaces;

public interface IGeneratorService
{
    Task GenerateUsers(int howManyUsersToGenerate);
    Task GenerateShifts(int normalDayShifts, int weekendShifts);
    Task GenerateLanes(int howManyLanes);
    Task GenerateReservations(int normalDayReservations,int weekendReservations);
    Task GenerateBarInventories(int howManyItems);
    Task GenerateInvoices(int howManyInvoices);
    Task GenerateAdmin();
}