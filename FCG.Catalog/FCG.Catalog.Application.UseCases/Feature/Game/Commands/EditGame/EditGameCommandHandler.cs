using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using FCG.Catalog.Application.UseCases.Mapper;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGame
{
    public class EditGameCommandHandler : IRequestHandler<EditGameCommand, GameDto>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IPlataformRepository _plataformRepository;
        private readonly IGameSearchService _gameSearchService;

        public EditGameCommandHandler(IGameRepository gameRepository, IGenderRepository genderRepository, IPlataformRepository plataformRepository, IGameSearchService gameSearchService)
        {
            _gameRepository = gameRepository;
            _genderRepository = genderRepository;
            _plataformRepository = plataformRepository;
            _gameSearchService = gameSearchService;
        }

        public async Task<GameDto> Handle(EditGameCommand request, CancellationToken cancellationToken)
        {
            var objGame = await _gameRepository.GetByIdAsync(request.Id);
            objGame.Initialize(request.TiTle, request.Description, request.Price, request.Discount, request.GenderId, request.PlataformId);
            await _gameRepository.UpdateAsync(objGame);
            await _gameSearchService.UpdateAsync(objGame.ToDocument());

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
