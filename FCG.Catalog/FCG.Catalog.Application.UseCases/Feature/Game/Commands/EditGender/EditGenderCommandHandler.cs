using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGender
{
    public class EditGenderCommandHandler : IRequestHandler<EditGenderCommand, GenderDto>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "genders:all";

        public EditGenderCommandHandler(IGenderRepository genderRepository, ICacheService cacheService)
        {
            _genderRepository = genderRepository;
            _cacheService = cacheService;
        }

        public async Task<GenderDto> Handle(EditGenderCommand request, CancellationToken cancellationToken)
        {
            var objGender = await _genderRepository.GetByIdAsync(request.Id);
            if (objGender != null)
            {
                try
                {
                    objGender.Initialize(request.Title);

                    await _genderRepository.UpdateAsync(objGender);

                    // Remover cache Redis.
                    await _cacheService.RemoveAsync(CacheKey);

                    return new GenderDto() { Id = objGender.Id, Title = objGender.Title };
                }
                catch (Exception)
                {
                    throw new Exception("Ao alterar o gênero ocorreu uma falha inesperada. Tente novamente mais tarde.");
                }
            }
            else
            {
                throw new ArgumentException("Gênero não foi encontrado.");
            }
        }
    }
}
