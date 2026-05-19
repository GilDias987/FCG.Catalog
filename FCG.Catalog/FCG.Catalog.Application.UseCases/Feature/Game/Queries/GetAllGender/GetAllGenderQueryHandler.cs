using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllGender
{
    public class GetAllGenderQueryHandler : IRequestHandler<GetAllGenderQuery, List<GenderDto>>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "genders:all";

        public GetAllGenderQueryHandler(IGenderRepository genderRepository, ICacheService cacheService)
        {
            _genderRepository = genderRepository;
            _cacheService = cacheService;
        }

        public async Task<List<GenderDto>> Handle(GetAllGenderQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cacheService.GetAsync<List<GenderDto>>(CacheKey);

            if (cached is not null && cached.Any())
                return cached;

            var qryGender = _genderRepository.All;
            var lstGender = qryGender.ToList().Select(s => new GenderDto
            {
                Id    = s.Id,
                Title = s.Title,
            }).ToList();

            if (!lstGender.Any())
                throw new ArgumentException("Nenhum registro encontrado.");

            await _cacheService.SetAsync(CacheKey, lstGender, TimeSpan.FromMinutes(10));

            return lstGender;
        }
    }
}
