using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGender
{

    public class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand, bool>
    {
        private readonly IGenderRepository _genderRepository;

        public DeleteGenderCommandHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        public async Task<bool> Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
        {
            var repGender = await _genderRepository.GetByIdAsync(request.Id);
            if (repGender != null)
            {
                await _genderRepository.DeleteAsync(repGender.Id);

                return true;
            }
            else
            {
                throw new ArgumentException("Gênero não foi encontrado.");
            }
        }
    }
}
