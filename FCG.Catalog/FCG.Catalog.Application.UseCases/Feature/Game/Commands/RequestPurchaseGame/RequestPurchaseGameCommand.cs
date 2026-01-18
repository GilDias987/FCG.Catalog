using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Commands.RequestPurchaseGame
{
    public class RequestPurchaseGameCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public string PaymentMethod { get; set; }
    }
}
