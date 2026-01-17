using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.UseCases.Service;
using FCG.Shared.Contracts;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.RequestPurchaseGame
{
    public class RequestPurchaseGameCommandHandler : IRequestHandler<RequestPurchaseGameCommand, bool>
    {
        private readonly IBus _bus;
        private readonly IGameRepository _gameRepository;
        private readonly UserApiService _userApiService;

        public RequestPurchaseGameCommandHandler(IBus bus, IGameRepository gameRepository, UserApiService userApiService)
        {
            _bus = bus;
            _gameRepository = gameRepository;
            _userApiService = userApiService;
        }

        public async Task<bool> Handle(RequestPurchaseGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await _userApiService.GetUserAsync(request.UserId);
               
                var game = await _gameRepository.GetByIdAsync(request.GameId);
                await _bus.Publish(new OrderPlacedEvent  { Email = usuario.Email, 
                                                           Game = game.Title,
                                                           PaymentMethod = request.PaymentMethod,
                                                           Name =  usuario.Name,
                                                           GameId = game.Id,
                                                           UserId = request.UserId, 
                                                           Price = game.CalculatePriceWithDiscount() });

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel criar pedido de compra do jogo.");
            }
        }
    }
}
