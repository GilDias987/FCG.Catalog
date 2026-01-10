using FCG.Catalog.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGender
{
    public sealed class AddGenderValidator : AbstractValidator<AddGenderCommand>
    {
        private readonly IGenderRepository _genderRepository;
        public AddGenderValidator(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;

            RuleFor(x => x.Title)
              .NotEmpty()
              .WithMessage("Informe o titulo do genero.")
              .MustAsync(async (Title, cancellation) => {
                  if (string.IsNullOrEmpty(Title))
                      return true;
                  else
                  {
                      var existeGenero = await _genderRepository.ExistsByAsync(x => x.Title.ToUpper() == Title.ToUpper());
                      return !existeGenero;
                  }
              }
              )
              .WithMessage("Já existe um gênero de jogo com esse título.");
        }
    }
}
