using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGender
{
    public class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand, bool>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "genders:all";

        public DeleteGenderCommandHandler(IGenderRepository genderRepository, ICacheService cacheService)
        {
            _genderRepository = genderRepository;
            _cacheService = cacheService;
        }
        public async Task<bool> Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
        {
            var repGender = await _genderRepository.GetByIdAsync(request.Id);
            if (repGender != null)
            {
                await _genderRepository.DeleteAsync(repGender.Id);

                // Remover cache Redis.
                await _cacheService.RemoveAsync(CacheKey);

                return true;
            }
            else
            {
                throw new ArgumentException("Gênero não foi encontrado.");
            }
        }
    }
}
