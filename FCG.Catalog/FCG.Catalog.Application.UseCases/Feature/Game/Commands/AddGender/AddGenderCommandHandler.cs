using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGender
{

    public class AddGenderCommandHandler : IRequestHandler<AddGenderCommand, GenderDto>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "genders:all";

        public AddGenderCommandHandler(IGenderRepository genderRepository, ICacheService cacheService)
        {
            _genderRepository = genderRepository;
            _cacheService = cacheService;
        }

        public async Task<GenderDto> Handle(AddGenderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objGender = await _genderRepository.AddAsync(new Domain.Entities.Gender(request.Title));

                // Remover cache Redis.
                await _cacheService.RemoveAsync(CacheKey);

                return new GenderDto() 
                { 
                    Id    = objGender.Id, 
                    Title = objGender.Title 
                };
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar o Genero ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
