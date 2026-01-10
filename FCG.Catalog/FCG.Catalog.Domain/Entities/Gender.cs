using FCG.Catalog.Domain.Common.Exceptions;
using FCG.Catalog.Domain.Common.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Domain.Entities
{
    public class Gender : BaseEntity
    {
        #region Propriedades Base
        public string Title { get; private set; }
        #endregion

        #region Propriedades Navegacao
        public ICollection<Game> Games { get; set; }
        #endregion

        #region Construtor EF
        public Gender()
        {
        }
        #endregion

        public Gender(string title)
        {
            Initialize(title);
        }

        public void Initialize(string title)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(title), "O título do gênero não pode ser vazio.");
            Title = title.Trim();
        }
    }
}
