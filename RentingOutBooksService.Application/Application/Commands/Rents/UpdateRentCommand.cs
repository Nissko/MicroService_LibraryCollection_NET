using MediatR;
using System.Runtime.Serialization;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class UpdateRentCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public Guid? BookId { get; private set; }

        [DataMember]
        public Guid? RentStatus { get; private set; }

        [DataMember]
        public int? CountRentDays { get; private set; }

        [DataMember]
        public DateTime? RentStartDate { get; private set; }

        public UpdateRentCommand(Guid id, Guid? bookId, Guid? rentStatus, int? countRentDays, DateTime? rentStartDate)
        {
            Id = id;
            BookId = bookId;
            RentStatus = rentStatus;
            CountRentDays = countRentDays;
            RentStartDate = rentStartDate;
        }
    }
}
