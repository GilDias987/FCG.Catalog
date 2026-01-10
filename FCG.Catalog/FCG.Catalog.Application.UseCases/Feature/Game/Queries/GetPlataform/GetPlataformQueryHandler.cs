using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetPlataform
{
    public class GetPlataformQueryHandler : IRequestHandler<GetPlataformQuery, PlataformDto>
    {
        private readonly IPlataformRepository _plataformRepository;

        public GetPlataformQueryHandler(IPlataformRepository plataformRepository)
        {
            _plataformRepository = plataformRepository;
        }

        public async Task<PlataformDto> Handle(GetPlataformQuery request, CancellationToken cancellationToken)
        {
            var plataform = await _plataformRepository.GetByIdAsync(request.Id);
            if (plataform is null)
            {
                throw new ArgumentException("Plataforma não encontrada.");
            }

            return new PlataformDto { Id = plataform.Id, Title = plataform.Title };
        }
    }
}
