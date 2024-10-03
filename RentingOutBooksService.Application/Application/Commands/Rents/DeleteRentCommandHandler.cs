using EventBus.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentingOutBooksService.Application.Application.IntegrationEvents.Event.EventsToSendService;
using RentingOutBooksService.Application.Common.Interfaces;
using RentingOutBooksService.Application.Exceptions;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class DeleteRentCommandHandler
        : IRequestHandler<DeleteRentCommand, bool>
    {
        private readonly IRentMicroServiceContext _context;
        private readonly IEventBusPublisher _eventBusPublisher;

        public DeleteRentCommandHandler(IRentMicroServiceContext context, IEventBusPublisher eventBusPublisher)
        {
            _context = context;
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task<bool> Handle(DeleteRentCommand command, CancellationToken cancellationToken)
        {
            var deleteRent = _context.Rents.Where(t => t.Id == command.RentId).Include(t => t.RentStatus).ToList();

            var result = deleteRent ?? throw new RentApplicationException("Удаляемая аренда не найдена");

            if (result[0].RentStatus.Id == RentStatus.AtWork.Id)
            {
                // Отправка в Rabbit
                _eventBusPublisher.Publish(new RefundBookEvent(result[0].BookId));
            }

            _context.Rents.Remove(result[0]);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
