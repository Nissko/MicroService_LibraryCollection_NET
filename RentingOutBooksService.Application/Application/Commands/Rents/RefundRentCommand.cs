using MediatR;
using System.Runtime.Serialization;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class RefundRentCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid RentId { get; private set; }

        public RefundRentCommand(Guid rentId)
        {
            RentId = rentId;
        }
    }
}
