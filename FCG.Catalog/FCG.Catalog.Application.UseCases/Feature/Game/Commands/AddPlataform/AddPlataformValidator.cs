using FCG.Catalog.Application.Dto.Game;
using FCG.Catalog.Application.Interface.Repository;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddPlataform
{
    public sealed class AddPlataformValidator : AbstractValidator<AddPlataformCommand>
    {
        private readonly IPlataformRepository _plataformRepository;

        public AddPlataformValidator(IPlataformRepository plataformRepository)
        {
            _plataformRepository = plataformRepository;

            RuleFor(c => c.Title).NotEmpty().WithMessage("Informe o título.");

            RuleFor(x => x.Title)
              .NotEmpty()
              .WithMessage("Informe o titulo da plataforma.")
              .MustAsync(async (Titulo, cancellation) => {
                  if (string.IsNullOrEmpty(Titulo))
                      return true;
                  else
                  {
                      var existsPlataform = await _plataformRepository.ExistsByAsync(x => x.Title.ToUpper() == Titulo.ToUpper());
                      return !existsPlataform;
                  }
              }
              )
              .WithMessage("Já existe uma plataforma com esse título.");
        }
    }
}
