using FCG.Catalog.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditPlataform
{
    public sealed class EditPlataformValidator : AbstractValidator<EditPlataformCommand>
    {
        private readonly IPlataformRepository _plataformRepository;
        public EditPlataformValidator(IPlataformRepository plataformRepository)
        {
            _plataformRepository = plataformRepository;

            RuleFor(c => c.Title).NotEmpty().WithMessage("Informe o titulo do gênero.");

            RuleFor(x => x.Id)
            .MustAsync(async (Id, cancellation) => (await _plataformRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
            .WithMessage("O id do gênero não foi encontrado.");

            RuleFor(x => x.Title)
              .NotEmpty()
              .WithMessage("Informe o titulo do gênero.")
              .MustAsync(async (model, context, cancellationToken) =>
              {
                  var plataform = await _plataformRepository.GetByIdAsync(model.Id);
                  if (plataform != null && plataform.Title != model.Title)
                  {
                      var verificaGenero = await _plataformRepository.ExistsByAsync(x => x.Title == model.Title);
                      return !verificaGenero;
                  }

                  return true;
              })
              .WithMessage("Já existe um gênero com esse título.");

        }
    }
}
