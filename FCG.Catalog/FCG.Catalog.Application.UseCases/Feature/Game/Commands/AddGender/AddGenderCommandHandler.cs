using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGender
{

    public class AddGenderCommandHandler : IRequestHandler<AddGenderCommand, GenderDto>
    {
        private readonly IGenderRepository _genderRepository;

        public AddGenderCommandHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<GenderDto> Handle(AddGenderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objGender = await _genderRepository.AddAsync(new Domain.Entities.Gender(request.Title));
                return new GenderDto() { Id = objGender.Id, Title = objGender.Title };
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar o Genero ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
