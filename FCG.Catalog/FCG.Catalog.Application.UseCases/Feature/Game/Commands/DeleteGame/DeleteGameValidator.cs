using FCG.Catalog.Application.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.DeleteGame
{
    public sealed class DeleteJogoValidator : AbstractValidator<DeleteGameCommand>
    {

        private readonly IGameRepository _gameRepository;
        public DeleteJogoValidator(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id do jogo.")
              .GreaterThan(0)
              .WithMessage("O id do jogo deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _gameRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id do jogo informado não foi encontrado.");

        }
    }
}
