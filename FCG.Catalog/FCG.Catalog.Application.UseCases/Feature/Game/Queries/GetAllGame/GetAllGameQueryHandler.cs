using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllGame
{
    public class GetAllGameQueryHandler : IRequestHandler<GetAllGameQuery, List<GameDto>>
    {
        private readonly IGameRepository _gameRepository;

        public GetAllGameQueryHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameDto>> Handle(GetAllGameQuery request, CancellationToken cancellationToken)
        {
            var qryGame = _gameRepository.All;

            var lstGame = qryGame.ToList()
                .Select(s => new GameDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Price = s.Price,
                    Discount = s.Discount,
                    GenderId = s.GenderId,
                    PlataformId = s.PlataformId,
                    PriceDiscount = s.CalculatePriceWithDiscount().ToString("N2")
                }).ToList(); 

            if (!lstGame.Any())
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return lstGame;
        }
    }
}
