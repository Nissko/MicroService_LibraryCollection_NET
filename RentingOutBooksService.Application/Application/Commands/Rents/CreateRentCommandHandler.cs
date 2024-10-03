using EventBus.Abstractions;
using MediatR;
using RentingOutBooksService.Application.Application.IntegrationEvents.Event.EventsToSendService;
using RentingOutBooksService.Application.Common.Interfaces;
using RentingOutBooksService.Application.Exceptions;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class CreateRentCommandHandler
        : IRequestHandler<CreateRentCommand, bool>
    {
        private readonly IRentMicroServiceContext _context;
        private readonly IEventBusPublisher _eventBusPublisher;

        public CreateRentCommandHandler(IRentMicroServiceContext context, IEventBusPublisher eventBusPublisher)
        {
            _context = context;
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task<bool> Handle(CreateRentCommand command, CancellationToken cancellationToken)
        {
            var clientContext = _context.Tenantries.Where(t => t.FIO == command.FIO).FirstOrDefault();

            var result = clientContext ?? throw new RentApplicationException("Клиент не найден");

            //Отправка в Rabbit
            _eventBusPublisher.Publish(new ChangeBookEvent(command.BookId));

            var rent = new Rent(command.CountRentDays, command.RentStartDate, command.BookId, result.Id);

            _context.Rents.Add(rent);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
