using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, bool>
    {
        private readonly IGameRepository _gameRepository;

        public DeleteGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var repGame = await _gameRepository.GetByIdAsync(request.Id);
            if (repGame != null)
            {
                await _gameRepository.DeleteAsync(repGame.Id);

                return true;
            }
            else
            {
                throw new ArgumentException("Jogo não foi encontrado.");
            }
        }
    }
}
