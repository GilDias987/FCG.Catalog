using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGender;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGameLibrary
{
    public class AddGameLibraryCommandHandler : IRequestHandler<AddGameLibraryCommand, bool>
    {
        private readonly IUserGameRepository _userGameRepository;

        public AddGameLibraryCommandHandler(IUserGameRepository userGameRepository)
        {
            _userGameRepository = userGameRepository;
        }

        public async Task<bool> Handle(AddGameLibraryCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                await _userGameRepository.AddAsync(new Domain.Entities.UserGame(request.UserId, request.GameId));
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao vincular o jogo na biblioteca");
            }
        }
    }

}
