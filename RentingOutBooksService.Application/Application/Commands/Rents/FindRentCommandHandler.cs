using MediatR;
using Microsoft.EntityFrameworkCore;
using RentingOutBooksService.Application.Common.Interfaces;
using RentingOutBooksService.Application.Exceptions;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class FindRentCommandHandler
        : IRequestHandler<FindRentCommand, IEnumerable<Rent>>
    {
        private readonly IRentMicroServiceContext _context;

        public FindRentCommandHandler(IRentMicroServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rent>> Handle(FindRentCommand command, CancellationToken cancellationToken)
        {
            var rentContext = _context.Rents.Where(t => t.Id == command.RentId)
                                            .Include(t => t.RentStatus);

            var result = rentContext ?? throw new RentApplicationException("Данной книги не существует");

            return result;
        }
    }
}
