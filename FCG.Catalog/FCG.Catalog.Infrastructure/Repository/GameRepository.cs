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
    public class GameRepository : EFRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Game?> GetGameIdAsync(int id)
        {
            return await _dbSet
                .Include(i => i.Gender)
                .Include(i => i.Plataform)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
