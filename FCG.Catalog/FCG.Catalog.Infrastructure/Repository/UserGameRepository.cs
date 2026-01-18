using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Domain.Entities;
using FCG.Catalog.Infrastructure.Context;
using FCG.Catalog.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Infrastructure.Repository
{
    public class UserGameRepository : EFRepository<UserGame>, IUserGameRepository
    {
        public UserGameRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<UserGame?> GetUserGameIdAsync(int userId, int gameId)
        {
            return await _dbSet.FirstOrDefaultAsync(g => g.UserId == userId && g.GameId == gameId);
        }
    }
}
