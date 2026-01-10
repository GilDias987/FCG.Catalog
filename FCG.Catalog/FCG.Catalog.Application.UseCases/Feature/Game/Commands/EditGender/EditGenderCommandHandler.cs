using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGender
{
    public class EditGenderCommandHandler : IRequestHandler<EditGenderCommand, GenderDto>
    {
        private readonly IGenderRepository _genderRepository;

        public EditGenderCommandHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<GenderDto> Handle(EditGenderCommand request, CancellationToken cancellationToken)
        {
            var objGender = await _genderRepository.GetByIdAsync(request.Id);
            if (objGender != null)
            {
                try
                {
                    objGender.Initialize(request.Title);

                    await _genderRepository.UpdateAsync(objGender);

                    return new GenderDto() { Id = objGender.Id, Title = objGender.Title };
                }
                catch (Exception)
                {
                    throw new Exception("Ao alterar o gênero ocorreu uma falha inesperada. Tente novamente mais tarde.");
                }
            }
            else
            {
                throw new ArgumentException("Gênero não foi encontrado.");
            }
        }
    }
}
