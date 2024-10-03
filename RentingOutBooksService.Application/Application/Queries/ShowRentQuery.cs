using MediatR;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;
namespace RentingOutBooksService.Application.Application.Queries
{
    public class ShowRentQuery
        : IRequest<IEnumerable<Rent>>
    {
    }
}
