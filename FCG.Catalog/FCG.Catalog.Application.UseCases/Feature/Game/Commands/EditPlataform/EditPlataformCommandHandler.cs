using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditPlataform
{
    public class EditPlataformCommandHandler : IRequestHandler<EditPlataformCommand, PlataformDto>
    {
        private readonly IPlataformRepository _plataformRepository;

        public EditPlataformCommandHandler(IPlataformRepository plataformRepository)
        {
            _plataformRepository = plataformRepository;
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
