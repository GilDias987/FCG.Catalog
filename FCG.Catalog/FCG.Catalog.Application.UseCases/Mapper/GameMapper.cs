using FCG.Catalog.Domain.Documents;
using FCG.Catalog.Domain.Entities;

namespace FCG.Catalog.Application.UseCases.Mapper
{
    public static class GameMapper
    {
        public static GameDocument ToDocument(this Game game)
        {
            return new GameDocument
            {
                Id = game.Id.ToString(),
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                Discount = game.Discount,
                GenderId = game.GenderId,
                PlataformId = game.PlataformId
            };
        }
    }
}
