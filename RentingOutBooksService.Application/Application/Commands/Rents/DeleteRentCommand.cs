using MediatR;
using System.Runtime.Serialization;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class DeleteRentCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid RentId { get; private set; }

        public DeleteRentCommand(Guid rentId)
        {
            RentId = rentId;
        }
    }
}
