using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGame
{
    public class AddGameCommandHandler : IRequestHandler<AddGameCommand, GameDto>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IPlataformRepository _plataformRepository;

        public AddGameCommandHandler(IGameRepository gameRepository, IGenderRepository genderRepository, IPlataformRepository plataformRepository)
        {
            _gameRepository = gameRepository;
            _genderRepository = genderRepository;
            _plataformRepository = plataformRepository;
        }

        public async Task<GameDto> Handle(AddGameCommand request, CancellationToken cancellationToken)
        {

            var objGame = await _gameRepository.AddAsync(new Domain.Entities.Game(request.Tittle, request.Description, request.Price, request.Discount, request.GenderId, request.PlatformId));

            var dtoGame = new GameDto()
            {
                Id = objGame.Id,
                Title = objGame.Title,
                Description = objGame.Description,
                Price = objGame.Price,
                Discount = objGame.Discount,
                GenderId = objGame.GenderId,
                PlataformId = objGame.PlataformId,
                PriceDiscount = objGame.CalculatePriceWithDiscount().ToString("N2")
            };

            return dtoGame;
        }
    }
}
