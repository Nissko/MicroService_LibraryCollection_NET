using RentingOutBooksService.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingOutBooksService.Domain.Aggregates.RentAggregate
{
    public class Tenantry
        : Entity, IAggregateRoot
    {
        private string _name;

        private string _surname;

        private string _patronymic;

        /// <summary>
        /// Вывод полного имени
        /// </summary>
        public string FIO;

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; private set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; private set; }

        public Tenantry(string name, string surname, string patronymic, string phone, string address)
        {
            _name = name;
            _surname = surname;
            _patronymic = patronymic;
            Phone = phone;
            Address = address;
            FIO = GetFullName();
        }

        public string GetFullName() => _surname + " " + _name + " " + _patronymic;
    }
}
