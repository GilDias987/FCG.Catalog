using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetGame
{
    public class GetGameQueryHandler : IRequestHandler<GetGameQuery, GameDto>
    {
        private readonly IGameRepository _gameRepository;

        public GetGameQueryHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<GameDto> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {

            var game = await _gameRepository.GetByIdAsync(request.Id);

            if (game is null)
            {
                throw new ArgumentException("Jogo não encontrado.");
            }

            return new GameDto { Id = game.Id, 
                                 Title = game.Title, 
                                 Description = game.Description, 
                                 Price = game.Price, 
                                 Discount = game.Discount,
                                 PlataformId = game.PlataformId, 
                                 GenderId = game.GenderId,
                                 PriceDiscount = game.CalculatePriceWithDiscount().ToString("N2")
            };

        }
    }
}
