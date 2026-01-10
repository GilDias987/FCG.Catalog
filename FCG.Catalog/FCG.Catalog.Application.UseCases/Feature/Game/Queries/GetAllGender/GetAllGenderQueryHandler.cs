using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetAllGender
{
    public class GetAllGenderQueryHandler : IRequestHandler<GetAllGenderQuery, List<GenderDto>>
    {
        private readonly IGenderRepository _genderRepository;

        public GetAllGenderQueryHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<List<GenderDto>> Handle(GetAllGenderQuery request, CancellationToken cancellationToken)
        {
            var qryGender = _genderRepository.All;
            var lstGender = qryGender.ToList().Select(s => new GenderDto
            {
                Id = s.Id,
                Title = s.Title,
            }).ToList();

            if (!lstGender.Any())
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return lstGender;
        }
    }
}
