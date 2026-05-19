using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllUserGames
{
    public class GetAllUserGamesQueryHandler : IRequestHandler<GetAllUserGamesQuery, List<GameDto>>
    {
        private readonly IUserGameRepository _userGameRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "userGames:all";

        public GetAllUserGamesQueryHandler(IUserGameRepository userGameRepository, ICacheService cacheService)
        {
            _userGameRepository = userGameRepository;
            _cacheService = cacheService;
        }

        public async Task<List<GameDto>> Handle(GetAllUserGamesQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cacheService.GetAsync<List<GameDto>>(CacheKey);

            if (cached is not null && cached.Any())
                return cached;

            var qryUserGame = await _userGameRepository.ListUserGameAsync(request.UserId);
            var lstUserGame = qryUserGame.ToList()
                .Select(s => new GameDto
                {
                    Id            = s.Game.Id,
                    Title         = s.Game.Title,
                    Description   = s.Game.Description,
                    Price         = s.Game.Price,
                    Discount      = s.Game.Discount,
                    PlataformId   = s.Game.PlataformId,
                    GenderId      = s.Game.GenderId,
                    PriceDiscount = s.Game.CalculatePriceWithDiscount().ToString("N2")
                }).ToList();

            if (!lstUserGame.Any())
            {
                throw new ArgumentException("Esse usuário não possui nenhum jogo vinculado a biblioteca.");
            }

            await _cacheService.SetAsync(CacheKey, lstUserGame, TimeSpan.FromMinutes(10));

            return lstUserGame;
        }
    }
}
