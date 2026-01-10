using FCG.Catalog.Application.Interface.Repository.Base;
using FCG.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.Interface.Repository
{
    public interface IPlataformRepository : IRepository<Plataform>
    {
        Task<Plataform?> GetPlataformIdAsync(int id);
    }
}
