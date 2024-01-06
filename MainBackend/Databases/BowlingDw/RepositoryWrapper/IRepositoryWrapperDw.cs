using MainBackend.Databases.BowlingDw.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDw.RepositoryWrapper;

public interface IRepositoryWrapperDw : IDisposable
{
    IFactInvoiceRepository invoice { get; }
    IFactReservationRepository reservation { get; }
    
    Task<bool> Save(int entities = 1);
    void Dispose();
}