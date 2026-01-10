using FCG.Catalog.Application.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeletePlataform
{
    public class DeletePlataformCommandHandler : IRequestHandler<DeletePlataformCommand, bool>
    {
        private readonly IPlataformRepository _plataformRepository;

        public DeletePlataformCommandHandler(IPlataformRepository plataformRepository)
        {
            _plataformRepository = plataformRepository;
        }
        public async Task<bool> Handle(DeletePlataformCommand request, CancellationToken cancellationToken)
        {
            var plataform = await _plataformRepository.GetByIdAsync(request.Id);
            if (plataform != null)
            {
                await _plataformRepository.DeleteAsync(plataform.Id);

                return true;
            }
            else
            {
                throw new ArgumentException("Plataforma não foi encontrado.");
            }
        }
    }
}
