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
    public class PlataformRepository : EFRepository<Plataform>, IPlataformRepository
    {
        public PlataformRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Plataform?> GetPlataformIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
