using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.Interface.Service;
using MediatR;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditPlataform
{
    public class EditPlataformCommandHandler : IRequestHandler<EditPlataformCommand, PlataformDto>
    {
        private readonly IPlataformRepository _plataformRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "plataforms:all";

        public EditPlataformCommandHandler(IPlataformRepository plataformRepository, ICacheService cacheService)
        {
            _plataformRepository = plataformRepository;
            _cacheService = cacheService;
        }

        public async Task<PlataformDto> Handle(EditPlataformCommand request, CancellationToken cancellationToken)
        {
            var plataform = await _plataformRepository.GetByIdAsync(request.Id);
            if (plataform != null)
            {
                try
                {
                    plataform.Initialize(request.Title);

                    await _plataformRepository.UpdateAsync(plataform);

                    // Remover cache Redis.
                    await _cacheService.RemoveAsync(CacheKey);

                    return new PlataformDto() { Id = plataform.Id, Title = plataform.Title };
                }
                catch (Exception)
                {
                    throw new Exception("Ao alterar a plataforma ocorreu uma falha inesperada. Tente novamente mais tarde.");
                }
            }
            else
            {
                throw new ArgumentException("Plataforma não foi encontrado.");
            }
        }
    }
}
