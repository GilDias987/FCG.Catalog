using FCG.Catalog.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeletePlataform
{
    public sealed class DeletePlataformValidator : AbstractValidator<DeletePlataformCommand>
    {
        private readonly IPlataformRepository _plataformRepository;

        public DeletePlataformValidator(IPlataformRepository plataformRepository)
        {
            _plataformRepository = plataformRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id da plataforma.")
              .GreaterThan(0)
              .WithMessage("O id da plataforma deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _plataformRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id da plataforma informado não foi encontrado.");
        }
    }
}
