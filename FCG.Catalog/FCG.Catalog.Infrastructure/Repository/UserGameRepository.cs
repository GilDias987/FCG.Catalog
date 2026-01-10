using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Domain.Entities;
using FCG.Catalog.Infrastructure.Context;
using FCG.Catalog.Infrastructure.Repository.Base;
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
    }
}
