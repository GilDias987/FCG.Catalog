using FCG.Catalog.Application.Dto.Game;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAdvancedGameSearch
{
    public class GetAdvancedGameQuery : IRequest<List<GameDto>>
    {
        public string? Filtro { get; set; }
        public int? PlataformId { get; set; }
        public int? GenderId { get; set; }
    }
}
