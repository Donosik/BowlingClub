using MainBackend.Services.Wrapper;

namespace MainBackend.Services.Interfaces;

public interface IGeneratorService
{
    Task GenerateUsers(int howManyUsersToGenerate);
    Task GenerateLanes(int howManyLanes);
    Task GenerateReservations(int normalDayReservations,int weekendReservations);
    Task GenerateInventoryItems(int howManyItems);
    Task GenerateInvoices(int howManyInvoices);
    Task GenerateAdmin();
}