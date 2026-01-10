using FCG.Catalog.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGender
{
    public sealed class DeleteGenderValidator : AbstractValidator<DeleteGenderCommand>
    {
        private readonly IGenderRepository _genderRepository;
        public DeleteGenderValidator(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id do genero.")
              .GreaterThan(0)
              .WithMessage("O id do genero deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _genderRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id do genero informado não foi encontrado.");

        }
    }
}
