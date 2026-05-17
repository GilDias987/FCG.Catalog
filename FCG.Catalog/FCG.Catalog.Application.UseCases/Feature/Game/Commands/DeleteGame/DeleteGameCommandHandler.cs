using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, bool>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameSearchService _gameSearchService;

        public DeleteGameCommandHandler(IGameRepository gameRepository, IGameSearchService gameSearchService)
        {
            _gameRepository = gameRepository;
            _gameSearchService = gameSearchService;
        }

        public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var repGame = await _gameRepository.GetByIdAsync(request.Id);


            if (repGame != null)
            {
                await _gameRepository.DeleteAsync(repGame.Id);
                await _gameSearchService.DeleteAsync(request.Id.ToString());
                return true;
            }
            else
            {
                throw new ArgumentException("Jogo não foi encontrado.");
            }
        }
    }
}
