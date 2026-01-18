using FCG.Catalog.Domain.Common.Exceptions;
using FCG.Catalog.Domain.Common.Validation;
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

        public UserGame(int userId, int gameId)
        {
            Inicializar(userId, gameId);
        }

        public void Inicializar(int userId, int gameId)
        {
            UserId = userId;
            GameId = gameId;
        }
    }
}
