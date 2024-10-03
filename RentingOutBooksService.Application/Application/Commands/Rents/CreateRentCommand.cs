using MediatR;
using System.Runtime.Serialization;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class CreateRentCommand
        : IRequest<bool>
    {
        [DataMember]
        public string FIO { get; private set; }

        [DataMember]
        public Guid BookId { get; private set; }

        [DataMember]
        public int CountRentDays { get; private set; }

        [DataMember]
        public DateTime RentStartDate { get; private set; }

        public CreateRentCommand(string fio, Guid bookId, int countRentDays, DateTime rentStartDate)
        {
            FIO = fio;
            BookId = bookId;
            CountRentDays = countRentDays;
            RentStartDate = rentStartDate;
        }
    }
}
