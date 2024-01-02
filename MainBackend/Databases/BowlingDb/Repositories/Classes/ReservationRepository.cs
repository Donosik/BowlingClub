using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Reservation>> GetAllOfClient(Client client)
    {
        if(client==null)
            return null;
        return await GetQuery().Include(r => r.Client).ThenInclude(x=>x.User).Include(r => r.Lane).Include(x=>x.Client).ThenInclude(x=>x.Person).Include(x=>x.Invoice).Where(r => r.Client == client)
            .ToListAsync();
    }

    public async Task<ICollection<Reservation>> GetAllOfClient(Client client, int usersPerPage, int currentPage,bool onlyNew,bool onlyWithoutInvoice)
    {
        if(client==null)
            return null;
        int startIndex = (currentPage - 1) * usersPerPage;
        var query = GetQuery().Include(r => r.Client).ThenInclude(x => x.User).Include(r => r.Lane)
            .Include(x => x.Client).ThenInclude(x => x.Person).Include(x=>x.Invoice).Where(r => r.Client == client).Skip(startIndex)
            .Take(usersPerPage);
        if (onlyNew)
            query = query.Where(x => x.StartTime >= DateTime.Today.AddDays(-1));
        if (onlyWithoutInvoice)
            query = query.Where(x => x.Invoice == null);
        query = query.OrderBy(x => x.StartTime);
        return await query.ToListAsync();
    }

    public async Task<ICollection<Reservation>> GetAllWithIncludes()
    {
        return await GetQuery().Include(r => r.Client).ThenInclude(x=>x.User).ThenInclude(x=>x.Person).Include(r => r.Lane).Include(x=>x.Invoice).Select(x=>new Reservation
        {
            Id = x.Id,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            Client = new Client
            {
                Person = new Person
                {
                    FirstName = x.Client.Person.FirstName,
                    LastName = x.Client.Person.LastName,
                },
                User = new User
                {
                    Id = x.Client.User.Id,
                }
            },
            Lane = new Lane
            {
                LaneNumber = x.Lane.LaneNumber
            },
            Invoice = x.Invoice
        }).ToListAsync();
    }

    public async Task<ICollection<Reservation>> GetAllWithIncludes(int usersPerPage, int currentPage,bool onlyNew,bool onlyWithoutInvoice)
    {
        int startIndex = (currentPage - 1) * usersPerPage;
        var query = GetQuery().Include(r => r.Client).ThenInclude(x => x.User).ThenInclude(x => x.Person)
            .Include(r => r.Lane).Include(x=>x.Invoice).Select(x => new Reservation
            {
                Id = x.Id,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Client = new Client
                {
                    Person = new Person
                    {
                        FirstName = x.Client.Person.FirstName,
                        LastName = x.Client.Person.LastName,
                    },
                    User = new User
                    {
                        Id = x.Client.User.Id,
                    }
                },
                Lane = new Lane
                {
                    LaneNumber = x.Lane.LaneNumber
                },
                Invoice = x.Invoice
            }).Skip(startIndex).Take(usersPerPage);

        if(onlyNew)
            query = query.Where(x => x.StartTime >= DateTime.Today.AddDays(-1));
        if (onlyWithoutInvoice)
            query = query.Where(x => x.Invoice == null);
        query = query.OrderBy(x => x.StartTime);
        
        return await query.ToListAsync();
    }
}