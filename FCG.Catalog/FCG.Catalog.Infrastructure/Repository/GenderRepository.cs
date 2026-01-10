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
    public class GenderRepository : EFRepository<Gender>, IGenderRepository
    {
        public GenderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Gender?> GetGenderIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> CheckIfGenreExistsAsync(string title)
        {
            var gender = await _dbSet.FirstOrDefaultAsync(g => g.Title.ToLower() == title.ToLower());
            return gender != null ? true : false;
        }
    }
}
