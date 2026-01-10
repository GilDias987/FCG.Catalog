using FCG.Catalog.Application.Interface.Repository.Base;
using FCG.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.Interface.Repository
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game?> GetGameIdAsync(int id);
    }
}
