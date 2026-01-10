using FCG.Catalog.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGender
{
    public sealed class EditGenderValidator : AbstractValidator<EditGenderCommand>
    {
        private readonly IGenderRepository _genderRepository;
        public EditGenderValidator(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;

            RuleFor(c => c.Title).NotEmpty().WithMessage("Informe o titulo do gênero.");

            RuleFor(x => x.Id)
            .MustAsync(async (Id, cancellation) => (await _genderRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
            .WithMessage("O id do gênero não foi encontrado.");

            RuleFor(x => x.Title)
              .NotEmpty()
              .WithMessage("Informe o titulo do gênero.")
              .MustAsync(async (model, context, cancellationToken) =>
              {
                  var gender = await _genderRepository.GetByIdAsync(model.Id);
                  if (gender != null && gender.Title != model.Title)
                  {
                      var verificaGenero = await _genderRepository.ExistsByAsync(x => x.Title == model.Title);
                      return !verificaGenero;
                  }

                  return true;
              })
              .WithMessage("Já existe um gênero com esse título.");

        }
    }
}
