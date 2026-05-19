using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeletePlataform
{
    public class DeletePlataformCommandHandler : IRequestHandler<DeletePlataformCommand, bool>
    {
        private readonly IPlataformRepository _plataformRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "plataforms:all";

        public DeletePlataformCommandHandler(IPlataformRepository plataformRepository, ICacheService cacheService)
        {
            _plataformRepository = plataformRepository;
            _cacheService = cacheService;
        }
        public async Task<bool> Handle(DeletePlataformCommand request, CancellationToken cancellationToken)
        {
            var plataform = await _plataformRepository.GetByIdAsync(request.Id);
            if (plataform != null)
            {
                await _plataformRepository.DeleteAsync(plataform.Id);

                // Remover cache Redis.
                await _cacheService.RemoveAsync(CacheKey);

                return true;
            }
            else
            {
                throw new ArgumentException("Plataforma não foi encontrado.");
            }
        }
    }
}
