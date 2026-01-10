using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.LinkDiscountGame
{
    public class LinkDiscountGameCommandHandler : IRequestHandler<LinkDiscountGameCommand, GameDto>
    {
        private readonly IGameRepository _gameRepository;

        public LinkDiscountGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<GameDto> Handle(LinkDiscountGameCommand request, CancellationToken cancellationToken)
        {
            var objGame = await _gameRepository.GetByIdAsync(request.Id);
            objGame.ApplyDiscount(request.Discount);
            await _gameRepository.UpdateAsync(objGame);

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
