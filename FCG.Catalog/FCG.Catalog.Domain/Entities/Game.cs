using FCG.Catalog.Domain.Common.Exceptions;
using FCG.Catalog.Domain.Common.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Domain.Entities
{
    public class Game : BaseEntity
    {
        #region Propriedades Base
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public decimal? Price { get; private set; }
        public decimal? Discount { get; private set; }
        #endregion

        #region Propriedades de Navegação
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public int PlataformId { get; set; }
        public Plataform Plataform { get; set; }
        public ICollection<UserGame> UserGames { get; set; }

        #endregion

        #region Construtor EF
        public Game()
        {
        }
        #endregion

        private void ValidateDiscount(decimal discount)
        {
            Guard.Against<DomainException>(discount < 0 || discount > 100, "O percentual do desconto não pode ser negativo ou maior que 100");
        }

        public Game(string title, string description, decimal? price, decimal? discount, int genderId, int plataformId)
        {
            Initialize(title, description, price, discount, genderId, plataformId);
        }

        public void Initialize(string title, string description, decimal? price, decimal? discount, int genderId, int plataformId)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(title), "O titulo do jogo não pode ser vazio.");
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(description), "A descricao do jogo não pode ser vazia.");
            Guard.Against<DomainException>(description.Length < 5, "A descricao deve possuir mais que 5 caracteres");
            Guard.AgainstEmptyId(genderId, "Genero Id");
            Guard.AgainstEmptyId(plataformId, "Plataforma Id");

            if (price.HasValue)
                Guard.Against<DomainException>(price.Value < 0, "O preço do jogo não pode ser menor que 0");

            if (discount.HasValue)
                ValidateDiscount(discount.Value);

            Title = title;
            Description = description;
            Price = price;
            Discount = discount;
            GenderId = genderId;
            PlataformId = plataformId;

        }

        public decimal CalculatePriceWithDiscount()
        {
            if (Discount.HasValue && Price.HasValue)
            {
                var descontoValor = (Price.Value * Discount.Value) / 100;
                return Price.Value - descontoValor;
            }

            return Price ?? 0;
        }

        public void ApplyDiscount(decimal? newDiscount)
        {
            if (newDiscount.HasValue)
            {
                ValidateDiscount(newDiscount.Value);
            }

            Discount = newDiscount.HasValue ? newDiscount.Value : null;
        }
    }
}
