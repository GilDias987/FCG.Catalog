using FCG.Catalog.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.EditGame
{
    public sealed class EditGameValidator : AbstractValidator<EditGameCommand>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IPlataformRepository _plataformRepository;
        public EditGameValidator(IGenderRepository genderRepository, IPlataformRepository plataformRepository, IGameRepository gameRepository)
        {
            _genderRepository = genderRepository;
            _plataformRepository = plataformRepository;
            _gameRepository = gameRepository;

            RuleFor(x => x.Id)
              .MustAsync(async (Id, cancellation) => (await _gameRepository.GetByIdAsync(Id)) != null ? true : false)
              .WithMessage("O id do jogo informado não foi encontrado.");

            RuleFor(c => c.TiTle).NotEmpty().WithMessage("Informe o título.");

            RuleFor(c => c.Price)
               .Must((model, context) =>
               {
                   return !(model.Price != Math.Round(model.Price, 2));
               })
               .WithMessage("O preço não pode conter mais de duas casas decimais.");

            RuleFor(x => x.GenderId)
               .MustAsync(async (GeneroId, cancellation) => (await _genderRepository.GetByIdAsync(GeneroId)) != null ? true : false)
               .WithMessage("O id do genero do jogo não foi encontrado.");

            RuleFor(x => x.PlataformId)
               .MustAsync(async (PlataformaId, cancellation) => (await _plataformRepository.GetByIdAsync(PlataformaId)) != null ? true : false)
               .WithMessage("O id do plataforma do jogo não foi encontrado.");

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
