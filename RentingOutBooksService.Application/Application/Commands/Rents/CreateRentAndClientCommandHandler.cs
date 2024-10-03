using EventBus.Abstractions;
using MediatR;
using RentingOutBooksService.Application.Application.IntegrationEvents.Event.EventsToSendService;
using RentingOutBooksService.Application.Common.Interfaces;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class CreateRentAndClientCommandHandler
        : IRequestHandler<CreateRentAndClientCommand, bool>
    {
        private readonly IRentMicroServiceContext _context;
        private readonly IEventBusPublisher _eventBusPublisher;

        public CreateRentAndClientCommandHandler(IRentMicroServiceContext context, IEventBusPublisher eventBusPublisher)
        {
            _context = context;
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task<bool> Handle(CreateRentAndClientCommand command, CancellationToken cancellationToken)
        {
            var client = new Tenantry(command.Name, command.Surname, command.Patronymic, command.Phone, command.Address);

            _context.Tenantries.Add(client);
            await _context.SaveChangesAsync(cancellationToken);

            //Отправка в Rabbit
            _eventBusPublisher.Publish(new ChangeBookEvent(command.BookId));

            var rent = new Rent(command.CountRentDays, command.RentStartDate, command.BookId, client.Id);

            _context.Rents.Add(rent);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
