using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using FCG.Catalog.Application.UseCases.Mapper;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGame
{
    public class AddGameCommandHandler : IRequestHandler<AddGameCommand, GameDto>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameSearchService _gameSearchService;

        public AddGameCommandHandler(IGameRepository gameRepository, IGameSearchService gameSearchService)
        {
            _gameRepository = gameRepository;
            _gameSearchService = gameSearchService;
        }

        public async Task<GameDto> Handle(AddGameCommand request, CancellationToken cancellationToken)
        {
            var objGame = await _gameRepository.AddAsync(new Domain.Entities.Game(request.Title, request.Description, request.Price, request.Discount, request.GenderId, request.PlatformId));

            await _gameSearchService.IndexAsync(objGame.ToDocument());

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
