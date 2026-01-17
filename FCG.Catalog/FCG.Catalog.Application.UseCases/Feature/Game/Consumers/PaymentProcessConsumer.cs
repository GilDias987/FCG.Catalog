using FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGameLibrary;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Consumers
{
    public class PaymentProcessConsumer : IConsumer<FCG.Shared.Contracts.PaymentProcessedEvent>
    {
        private readonly IMediator _mediator;
        public PaymentProcessConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Consume(ConsumeContext<FCG.Shared.Contracts.PaymentProcessedEvent> context)
        {
            return _mediator.Send(
                 new AddGameLibraryCommand
                 {
                     UserId = context.Message.UserId,
                     GameId = context.Message.GameId
                 }
            );
        }
    }
}
