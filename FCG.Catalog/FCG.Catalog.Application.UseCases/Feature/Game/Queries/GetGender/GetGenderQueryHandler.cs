using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetGame;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Queries.GetGender
{
    public class GetGenderQueryHandler : IRequestHandler<GetGenderQuery, GenderDto>
    {
        private readonly IGenderRepository _genderRepository;

        public GetGenderQueryHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<GenderDto> Handle(GetGenderQuery request, CancellationToken cancellationToken)
        {
            var gender = await _genderRepository.GetByIdAsync(request.Id);
            if (gender is null)
            {
                throw new ArgumentException("Gênero não encontrado.");
            }

            return new GenderDto { Id = gender.Id, Title = gender.Title };
        }
    }
}
