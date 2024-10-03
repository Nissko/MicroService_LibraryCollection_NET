using EventBus.Abstractions;
using MediatR;
using RentingOutBooksService.Application.Application.IntegrationEvents.Event.EventsToSendService;
using RentingOutBooksService.Application.Common.Interfaces;
using RentingOutBooksService.Application.Exceptions;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class UpdateRentCommandHandler
        : IRequestHandler<UpdateRentCommand, bool>
    {
        private readonly IRentMicroServiceContext _context;
        private readonly IEventBusPublisher _eventBusPublisher;

        public UpdateRentCommandHandler(IRentMicroServiceContext context, IEventBusPublisher eventBusPublisher)
        {
            _context = context;
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task<bool> Handle(UpdateRentCommand command, CancellationToken cancellationToken)
        {
            var rentContext = await _context.Rents.FindAsync(command.Id);

            var result = rentContext ?? throw new RentApplicationException("Не удалось найти аренду");

            if(command.BookId != null )
            {
                // Отправка в Rabbit чтобы убрать статус аренды у старой книги
                _eventBusPublisher.Publish(new RefundBookEvent(result.BookId));

                // Отправка в Rabbit чтобы добавить статус аренды новой книге
                _eventBusPublisher.Publish(new ChangeBookEvent((Guid)command.BookId));
            }

            result.UpdateRent(command.BookId, command.CountRentDays, command.RentStartDate);

            if(command.RentStatus != null)
            {
                result.SetStatusForId(command.Id);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
