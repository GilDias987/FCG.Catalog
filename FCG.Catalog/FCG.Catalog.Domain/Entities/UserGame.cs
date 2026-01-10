using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Domain.Entities
{

    public class UserGame : BaseEntity
    {
        #region Propriedades de Navegação
        public int UserId { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
        #endregion
    }
}
