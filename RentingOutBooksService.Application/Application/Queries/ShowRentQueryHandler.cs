using MediatR;
using Microsoft.EntityFrameworkCore;
using RentingOutBooksService.Application.Common.Interfaces;
using RentingOutBooksService.Application.Exceptions;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;

namespace RentingOutBooksService.Application.Application.Queries
{
    public class ShowRentQueryHandler
        : IRequestHandler<ShowRentQuery, IEnumerable<Rent>>
    {
        private readonly IRentMicroServiceContext _context;

        public ShowRentQueryHandler(IRentMicroServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rent>> Handle(ShowRentQuery command, CancellationToken cancellationToken)
        {
            var rentContext = _context.Rents.Include(t => t.RentStatus)
                                            .ToListAsync();

            var result = await rentContext ?? throw new RentApplicationException("Произошла ошибка при выводе аренд");

            return result;
        }
    }
}
