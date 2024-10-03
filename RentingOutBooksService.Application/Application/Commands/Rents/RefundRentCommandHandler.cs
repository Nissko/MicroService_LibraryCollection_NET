using EventBus.Abstractions;
using MediatR;
using RentingOutBooksService.Application.Application.IntegrationEvents.Event.EventsToSendService;
using RentingOutBooksService.Application.Common.Interfaces;
using RentingOutBooksService.Application.Exceptions;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class RefundRentCommandHandler
        : IRequestHandler<RefundRentCommand, bool>
    {
        private readonly IRentMicroServiceContext _context;
        private readonly IEventBusPublisher _eventBusPublisher;

        public RefundRentCommandHandler(IRentMicroServiceContext context, IEventBusPublisher eventBusPublisher)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventBusPublisher = eventBusPublisher ?? throw new ArgumentNullException(nameof(eventBusPublisher));
        }

        public async Task<bool> Handle(RefundRentCommand command, CancellationToken cancellationToken)
        {
            var rentContext = await _context.Rents.FindAsync(command.RentId);

            var result = rentContext ?? throw new RentApplicationException("Не удалось найти аренду");

            result.SetAtFreeStatus();

            // Отправка в Rabbit
            _eventBusPublisher.Publish(new RefundBookEvent(result.BookId));

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
