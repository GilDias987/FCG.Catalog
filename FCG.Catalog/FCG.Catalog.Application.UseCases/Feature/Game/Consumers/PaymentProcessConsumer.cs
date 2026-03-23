using FCG.Catalog.Application.UseCases.Feature.Game.Commands.AddGameLibrary;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FCG.Catalog.Application.UseCases.Feature.Game.Consumers
{
    public class PaymentProcessConsumer : IConsumer<FCG.Shared.Contracts.PaymentProcessedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PaymentProcessConsumer> _logger;

        public PaymentProcessConsumer(IMediator mediator, ILogger<PaymentProcessConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public Task Consume(ConsumeContext<FCG.Shared.Contracts.PaymentProcessedEvent> context)
        {
            _logger.LogInformation("Pagamento processado, o jogo {Game} foi vinculado ao usuário {Name}!", context.Message.Game, context.Message.Name);

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
