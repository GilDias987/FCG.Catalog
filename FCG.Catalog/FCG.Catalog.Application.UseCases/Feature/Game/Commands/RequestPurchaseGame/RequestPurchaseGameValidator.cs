using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.UseCases.Feature.Game.Commands.LinkDiscountGame;
using FCG.Catalog.Application.UseCases.Service;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.RequestPurchaseGame
{
    public class RequestPurchaseGameValidator : AbstractValidator<RequestPurchaseGameCommand>
    {
        private readonly IGameRepository _gameRepository;
        private readonly UserApiService _userApiService;
        private readonly IUserGameRepository _userGameRepository;

        public RequestPurchaseGameValidator(IGameRepository gameRepository, UserApiService userApiService, IUserGameRepository userGameRepository)
        {
            _userApiService = userApiService;
            _gameRepository = gameRepository;
            _userGameRepository = userGameRepository;


            RuleFor(x => x.UserId)
                    .NotEmpty()
                    .WithMessage("Informe o id do usuário.")
                    .MustAsync(async (UserId, cancellation) => {
                        if (UserId == 0)
                            return true;
                        else
                        {
                            var user = await _userApiService.GetUserAsync(UserId);
                            return user != null;
                        }
                    }
                    )
                    .WithMessage("Informe um id de usuário válido.");

            RuleFor(x => x.GameId)
              .NotEmpty()
              .WithMessage("Informe o id do jogo.")
              .MustAsync(async (GameId, cancellation) => {
                  if (GameId == 0)
                      return true;
                  else
                  {
                      var game = await _gameRepository.GetByIdAsync(GameId);
                      return game != null;
                  }
              }
              )
              .WithMessage("Informe um id do jogo válido.");


            RuleFor(x => x.PaymentMethod)
               .NotEmpty()
               .WithMessage("Informe a forma de pagamento.")
               .MustAsync(async (PaymentMethod, cancellation) => {
                   List<string> lstMethodPlayment = new List<string> { "C", "B", "P" };
                   return lstMethodPlayment.Contains(PaymentMethod);
               }
               )
               .WithMessage("Informe uma forma de pagamento válida. C = Cartão de Crédito, B = Boleto, P = Pix");

            RuleFor(x => x.UserId)
             .MustAsync(async (model, email, cancellation) =>
             {
                 var userGame = await _userGameRepository.GetUserGameIdAsync(model.UserId, model.GameId);
                 return userGame == null;
             })
             .WithMessage("Esse usuário já possui o jogo.");



        }
    }

}
