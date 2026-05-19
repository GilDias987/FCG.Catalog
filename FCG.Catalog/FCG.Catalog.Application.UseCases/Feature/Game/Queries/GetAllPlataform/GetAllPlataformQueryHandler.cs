using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllPlataform
{
    public class GetAllPlataformQueryHandler : IRequestHandler<GetAllPlataformQuery, List<PlataformDto>>
    {
        private readonly IPlataformRepository _plataformRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "plataforms:all";

        public GetAllPlataformQueryHandler(IPlataformRepository plataformRepository, ICacheService cacheService)
        {
            _plataformRepository = plataformRepository;
            _cacheService = cacheService;
        }

        public async Task<List<PlataformDto>> Handle(GetAllPlataformQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cacheService.GetAsync<List<PlataformDto>>(CacheKey);

            if (cached is not null && cached.Any())
                return cached;

            var qryPlataform = _plataformRepository.All;
            var lstPlataform = qryPlataform.ToList()
                .Select(s => new PlataformDto
                {
                    Id    = s.Id,
                    Title = s.Title,
                }).ToList();

            if (!lstPlataform.Any())
                throw new ArgumentException("Nenhum registro encontrado.");

            await _cacheService.SetAsync(CacheKey, lstPlataform, TimeSpan.FromMinutes(10));

            return lstPlataform;
        }
    }
}
