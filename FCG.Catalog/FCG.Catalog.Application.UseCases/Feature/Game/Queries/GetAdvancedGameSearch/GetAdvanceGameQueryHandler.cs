using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAdvancedGameSearch
{
    public class GetAdvanceGameQueryHandler : IRequestHandler<GetAdvancedGameQuery, List<GameDto>>
    {
        private readonly IGameSearchService _gameSearchService;

        public GetAdvanceGameQueryHandler(IGameSearchService gameSearchService)
        {
            _gameSearchService = gameSearchService;
        }

        public async Task<List<GameDto>> Handle(GetAdvancedGameQuery request, CancellationToken cancellationToken)
        {
            var game = await _gameSearchService.SearchAsync(request.Filtro, request.GenderId, request.PlataformId);

            var lstGame = game.ToList()
                 .Select(s => new GameDto
                 {
                     Id = Convert.ToInt32(s.Id),
                     Title = s.Title,
                     Description = s.Description,
                     Price = s.Price,
                     Discount = s.Discount,
                     GenderId = s.GenderId,
                     PlataformId = s.PlataformId,
                     PriceDiscount = CalculatePriceWithDiscount(s.Price, s.Discount).ToString("N2")
                 }).ToList();

            return lstGame;

        }

        #region Calculate Discount
        private decimal CalculatePriceWithDiscount(decimal? Discount, decimal? Price)
        {
            if (Discount.HasValue && Price.HasValue)
            {
                var descontoValor = (Price.Value * Discount.Value) / 100;
                return Price.Value - descontoValor;
            }

            return Price ?? 0;
        }
        #endregion
    }
}
