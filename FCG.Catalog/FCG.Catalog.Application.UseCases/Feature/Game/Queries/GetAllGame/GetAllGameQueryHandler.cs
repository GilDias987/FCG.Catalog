using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllGame
{
    public class GetAllGameQueryHandler : IRequestHandler<GetAllGameQuery, List<GameDto>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "games:all";

        public GetAllGameQueryHandler(IGameRepository gameRepository, ICacheService cacheService)
        {
            _gameRepository = gameRepository;
            _cacheService = cacheService;
        }

        public async Task<List<GameDto>> Handle(GetAllGameQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cacheService.GetAsync<List<GameDto>>(CacheKey);

            if (cached is not null && cached.Any())
                return cached;

            var qryGame = _gameRepository.All;
            var lstGame = qryGame.ToList()
                .Select(s => new GameDto
                {
                    Id            = s.Id,
                    Title         = s.Title,
                    Description   = s.Description,
                    Price         = s.Price,
                    Discount      = s.Discount,
                    GenderId      = s.GenderId,
                    PlataformId   = s.PlataformId,
                    PriceDiscount = s.CalculatePriceWithDiscount().ToString("N2")
                }).ToList();

            if (!lstGame.Any())
                throw new ArgumentException("Nenhum registro encontrado.");

            await _cacheService.SetAsync(CacheKey, lstGame, TimeSpan.FromMinutes(10));

            return lstGame;
        }
    }
}
