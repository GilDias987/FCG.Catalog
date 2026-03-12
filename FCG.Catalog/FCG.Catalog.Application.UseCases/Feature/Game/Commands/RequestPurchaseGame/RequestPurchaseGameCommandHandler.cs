using FCG.Catalog.Application.Interface.Repository;
using FCG.Catalog.Application.UseCases.Service;
using FCG.Shared.Contracts;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.RequestPurchaseGame
{
    public class RequestPurchaseGameCommandHandler : IRequestHandler<RequestPurchaseGameCommand, bool>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IGameRepository _gameRepository;
        private readonly UserApiService _userApiService;

        public RequestPurchaseGameCommandHandler(ISendEndpointProvider sendEndpointProvider, IGameRepository gameRepository, UserApiService userApiService)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _gameRepository = gameRepository;
            _userApiService = userApiService;
        }

        public async Task<bool> Handle(RequestPurchaseGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await _userApiService.GetUserAsync(request.UserId);
               
                var game = await _gameRepository.GetByIdAsync(request.GameId);

                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:payment-create-queue"));

                await endpoint.Send(new OrderPlacedEvent
                {
                    Email = usuario.Email,
                    Game = game.Title,
                    PaymentMethod = request.PaymentMethod,
                    Name = usuario.Name,
                    GameId = game.Id,
                    UserId = request.UserId,
                    Price = game.CalculatePriceWithDiscount()
                });

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel criar pedido de compra do jogo.");
            }
        }
    }
}
