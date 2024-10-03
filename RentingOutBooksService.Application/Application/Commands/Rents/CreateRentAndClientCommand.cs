using MediatR;
using System.Runtime.Serialization;

namespace RentingOutBooksService.Application.Application.Commands.Rents
{
    public class CreateRentAndClientCommand
        : IRequest<bool>
    {
        /*Клиент*/
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Surname { get; private set; }

        [DataMember]
        public string Patronymic { get; private set; }

        [DataMember]
        public string Phone { get; private set; }

        [DataMember]
        public string Address { get; private set; }

        /*Книга*/
        [DataMember]
        public Guid BookId { get; private set; }

        [DataMember]
        public int CountRentDays { get; private set; }

        [DataMember]
        public DateTime RentStartDate { get; private set; }

        public CreateRentAndClientCommand(string name, string surname, string patronymic, string phone, string address,
                                 Guid bookId, int countRentDays, DateTime rentStartDate)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Phone = phone;
            Address = address;
            BookId = bookId;
            CountRentDays = countRentDays;
            RentStartDate = rentStartDate;
        }
    }
}
