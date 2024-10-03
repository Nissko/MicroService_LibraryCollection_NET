using MediatR;
using RentingOutBooksService.Domain.Aggregates.RentAggregate;
using System.Runtime.Serialization;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class FindRentCommand
        : IRequest<IEnumerable<Rent>>
    {
        [DataMember]
        public Guid RentId { get; private set; }

        public FindRentCommand(Guid rentId)
        {
            RentId = rentId;
        }
    }
}
