﻿using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
    }

    public async Task<User> GetUser(string login)
    {
        return await dbContext.Set<User>().FirstOrDefaultAsync(u => u.Login == login);
    }
}