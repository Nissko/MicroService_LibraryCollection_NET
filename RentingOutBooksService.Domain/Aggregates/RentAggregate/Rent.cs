using RentingOutBooksService.Domain.Common;
using RentingOutBooksService.Domain.Exceptions;

namespace RentingOutBooksService.Domain.Aggregates.RentAggregate
{
    public class Rent
        : Entity, IAggregateRoot
    {
        /// <summary>
        /// Id Клиента
        /// </summary>
        public Guid ClientId => _clientId;
        private Guid _clientId;

        /// <summary>
        /// Статус заказа
        /// </summary>
        public RentStatus RentStatus { get; private set; }
        private Guid _rentStatusId;

        /// <summary>
        /// Книга
        /// </summary>
        public Guid BookId => _bookId;
        private Guid _bookId;

        /// <summary>
        /// Кол-во дней аренды
        /// </summary>
        private int _countRentDay;

        /// <summary>
        /// Начало аренды
        /// </summary>
        public DateTime StartDate => _rentDateStart;
        private DateTime _rentDateStart;

        /// <summary>
        /// Конец аренды
        /// </summary>
        public DateTime EndDate => _rentDateEnd;
        private DateTime _rentDateEnd;

        public Rent(int countRentDay, DateTime rentDateStart, Guid bookId, Guid clientId)
        {
            _clientId = clientId;
            _rentStatusId = RentStatus.AtWork.Id;
            _countRentDay = countRentDay;
            _rentDateStart = rentDateStart.ToUniversalTime();
            _rentDateEnd = DateEnd(_countRentDay, _rentDateStart);
            _bookId = bookId;
        }

        public void UpdateRent(Guid? bookId, int? countRentDays, DateTime? rentStartDate)
        {
            _bookId = bookId ?? _bookId;
            _countRentDay = countRentDays ?? _countRentDay;
            _rentDateStart = rentStartDate ?? _rentDateStart;
            _rentDateEnd = DateEnd(_countRentDay, _rentDateStart);
        }

        /// <summary>
        /// Вычисление значения даты окончания аренды
        /// </summary>
        private DateTime DateEnd(int countRend, DateTime rentStartDate)
        {
            _rentDateEnd = rentStartDate;
            _rentDateEnd = _rentDateEnd.AddDays(countRend);

            return _rentDateEnd;
        }

        /// <summary>
        /// Изменение статуса по Id
        /// </summary>
        public void SetStatusForId(Guid id)
        {
            var status = RentStatus.From(id);

            _rentStatusId = status.Id;
        }

        /// <summary>
        /// Изменение на статус, что аренда завершена
        /// </summary>
        public void SetAtFreeStatus()
        {
            if(_rentStatusId == RentStatus.ToFree.Id)
            {
                throw new RentDomainException("Аренда уже имеет статус - 'Завершен'");
            }

            _rentStatusId = RentStatus.ToFree.Id;
        }

        /// <summary>
        /// Изменение на статус, что арендовано
        /// </summary>
        public void SetAtWorkStatus()
        {
            if (_rentStatusId == RentStatus.AtWork.Id)
            {
                throw new RentDomainException("Аренда уже имеет статус - 'В работе'");
            }

            _rentStatusId = RentStatus.AtWork.Id;
        }
    }
}
