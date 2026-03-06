using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetGame;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllUserGames
{
    public class GetAllUserGamesQueryHandler : IRequestHandler<GetAllUserGamesQuery, List<GameDto>>
    {
        private readonly IUserGameRepository _userGameRepository;

        public GetAllUserGamesQueryHandler(IUserGameRepository userGameRepository)
        {
            _userGameRepository = userGameRepository;
        }

        public async Task<List<GameDto>> Handle(GetAllUserGamesQuery request, CancellationToken cancellationToken)
        {

            var userGame = await _userGameRepository.ListUserGameAsync(request.UserId);

            if (!userGame.Any())
            {
                throw new ArgumentException("Esse usuário não possui nenhum jogo vinculado a biblioteca.");
            }

            return userGame.Select(u => new GameDto
            {
                Id = u.Game.Id,
                Title = u.Game.Title,
                Description = u.Game.Description,
                Price = u.Game.Price,
                Discount = u.Game.Discount,
                PlataformId = u.Game.PlataformId,
                GenderId = u.Game.GenderId,
                PriceDiscount = u.Game.CalculatePriceWithDiscount().ToString("N2")
            }).ToList();
        }
    }
}
