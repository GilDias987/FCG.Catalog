using FCG.Catalog.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.LinkDiscountGame
{
    public class LinkDiscountGameValidator : AbstractValidator<LinkDiscountGameCommand>
    {
        private readonly IGameRepository _gameRepository;
        public LinkDiscountGameValidator(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id do jogo.")
              .GreaterThan(0)
              .WithMessage("O id deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _gameRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id do jogo informado não foi encontrado.");

            RuleFor(x => x.Discount)
                      .Must((model, context) =>
                      {
                          if (model.Discount.HasValue)
                          {
                              if (model.Discount < 0 || model.Discount > 100)
                                  return false;
                          }

                          return true;
                      })
                      .WithMessage("O percentual do desconto não pode ser negativo ou maior que 100.")
                         .Must((model, context) =>
                         {
                             if (model.Discount.HasValue)
                             {
                                 return !(model.Discount.Value != Math.Round(model.Discount.Value, 2));
                             }

                             return true;
                         })
                      .WithMessage("O percentual do desconto não pode conter mais de duas casas decimais.");
        }
    }
}
