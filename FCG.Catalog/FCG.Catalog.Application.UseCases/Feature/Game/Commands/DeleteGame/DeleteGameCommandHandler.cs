using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, bool>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameSearchService _gameSearchService;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "games:all";

        public DeleteGameCommandHandler(IGameRepository gameRepository, IGameSearchService gameSearchService, ICacheService cacheService)
        {
            _gameRepository = gameRepository;
            _gameSearchService = gameSearchService;
            _cacheService = cacheService;
        }

        public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var repGame  = await _gameRepository.GetByIdAsync(request.Id);
            if (repGame != null)
            {
                await _gameRepository.DeleteAsync(repGame.Id);
                await _gameSearchService.DeleteAsync(request.Id.ToString());

                // Remover cache Redis.
                await _cacheService.RemoveAsync(CacheKey);

                return true;
            }
            else
            {
                throw new ArgumentException("Jogo não foi encontrado.");
            }
        }
    }
}
