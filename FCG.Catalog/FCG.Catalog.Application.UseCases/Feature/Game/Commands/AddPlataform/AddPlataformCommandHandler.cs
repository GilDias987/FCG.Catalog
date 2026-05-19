using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddPlataform
{
    public class AddPlataformCommandHandler : IRequestHandler<AddPlataformCommand, PlataformDto>
    {
        private readonly IPlataformRepository _plataformRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "plataforms:all";

        public AddPlataformCommandHandler(IPlataformRepository plataformRepository, ICacheService cacheService)
        {
            _plataformRepository = plataformRepository;
            _cacheService = cacheService;
        }

        public async Task<PlataformDto> Handle(AddPlataformCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var plataform = await _plataformRepository.AddAsync(new Domain.Entities.Plataform(request.Title));

                // Remover cache Redis.
                await _cacheService.RemoveAsync(CacheKey);

                return new PlataformDto() 
                { 
                    Id    = plataform.Id, 
                    Title = plataform.Title 
                };
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar uma Plataforma ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
