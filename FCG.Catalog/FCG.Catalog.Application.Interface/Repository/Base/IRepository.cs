using FCG.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FCG.Catalog.Application.Interface.Repository.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// GetByOrDefaultIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetByOrDefaultIdAsync(int id);

        /// <summary>
        /// GetByIdExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool GetByIdExists(int id);

        /// <summary>
        /// GetByIdExistsAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> GetByIdExistsAsync(int id);

        /// <summary>
        /// ExistsByAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> ExistsByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        Task UpdateAsync(T entidade);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// All
        /// </summary>
        IQueryable<T> All { get; }
    }
}
