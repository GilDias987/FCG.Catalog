using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddPlataform
{
    public class AddPlataformCommandHandler : IRequestHandler<AddPlataformCommand, PlataformDto>
    {
        private readonly IPlataformRepository _plataformRepository;

        public AddPlataformCommandHandler(IPlataformRepository plataformRepository)
        {
            _plataformRepository = plataformRepository;
        }

        public async Task<PlataformDto> Handle(AddPlataformCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var plataform = await _plataformRepository.AddAsync(new Domain.Entities.Plataform(request.Title));

                return new PlataformDto() { Id = plataform.Id, Title = plataform.Title };
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar uma Plataforma ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
