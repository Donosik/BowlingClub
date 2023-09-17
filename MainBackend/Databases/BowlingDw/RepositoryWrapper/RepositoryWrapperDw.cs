﻿using MainBackend.Databases.BowlingDw.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDw.RepositoryWrapper;

public class RepositoryWrapperDw : IRepositoryWrapperDw
{
    public IFactInvoiceRepository invoice { get; }
    public IFactReservationRepository reservation { get; }
    public IFactWorkScheduleRepository workSchedule { get; }
    private readonly Context.BowlingDw dbContext;

    public RepositoryWrapperDw(Context.BowlingDw dbContext, IFactInvoiceRepository invoice,IFactReservationRepository reservation,IFactWorkScheduleRepository workSchedule)
    {
        this.dbContext = dbContext;
        this.invoice = invoice;
        this.reservation = reservation;
        this.workSchedule = workSchedule;
    }

    public async Task<bool> Save(int entities = 1)
    {
        int result = await dbContext.SaveChangesAsync();
        if (result >= entities)
            return true;
        return false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
            dbContext.Dispose();
    }
}