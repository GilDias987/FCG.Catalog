using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllGender;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllPlataform
{
    public class GetAllPlataformQueryHandler : IRequestHandler<GetAllPlataformQuery, List<PlataformDto>>
    {
        private readonly IPlataformRepository _plataformRepository;

        public GetAllPlataformQueryHandler(IPlataformRepository plataformRepository)
        {
            _plataformRepository = plataformRepository;
        }

        public async Task<List<PlataformDto>> Handle(GetAllPlataformQuery request, CancellationToken cancellationToken)
        {
            var qryPlataform = _plataformRepository.All;
            var lstPlataform = qryPlataform.ToList().Select(s => new PlataformDto
            {
                Id = s.Id,
                Title = s.Title,
            }).ToList();

            if (!lstPlataform.Any())
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return lstPlataform;
        }
    }
}
