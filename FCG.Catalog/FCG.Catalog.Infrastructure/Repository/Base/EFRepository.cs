using FCG.Catalog.Application.Interface.Repository.Base;
using FCG.Catalog.Domain.Entities;
using FCG.Catalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FCG.Catalog.Infrastructure.Repository.Base
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id) =>
            _dbSet.Find(id);

        /// <summary>
        /// FindAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        /// <summary>
        /// FirstOrDefaultAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T?> GetByOrDefaultIdAsync(int id) =>
            await _dbSet.FirstOrDefaultAsync(a => a.Id == id);

        /// <summary>
        /// GetByIdExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetByIdExists(int id) =>
            _dbSet.Any(a => a.Id == id);

        /// <summary>
        /// GetByIdExistsAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> GetByIdExistsAsync(int id) =>
            await _dbSet.AnyAsync(a => a.Id == id);

        /// <summary>
        /// ExistsByAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> ExistsByAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).AnyAsync();

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public T Add(T entidade)
        {
            entidade.SetDateCreation(DateTime.Now, DateTime.Now);
            _dbSet.Add(entidade);
            _context.SaveChanges();
            return entidade;
        }

        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entidade)
        {
            entidade.SetDateCreation(DateTime.Now, DateTime.Now);
            await _dbSet.AddAsync(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _dbSet.Remove(GetById(id));
            _context.SaveChanges();
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var entidade = await GetByIdAsync(id);
            _dbSet.Remove(entidade);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entidade"></param>
        public void Update(T entidade)
        {
            entidade.SetDateUpdate(DateTime.Now);
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entidade)
        {
            entidade.SetDateUpdate(DateTime.Now);
            _context.Entry(entidade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// All
        /// </summary>
        public IQueryable<T> All => _context.Set<T>();
    }
}
