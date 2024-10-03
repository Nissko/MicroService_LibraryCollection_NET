using RentingOutBooksService.Domain.Common;
using RentingOutBooksService.Domain.Exceptions;

namespace RentingOutBooksService.Domain.Aggregates.RentAggregate
{
    public class RentStatus
        : Enumeration
    {
        public static RentStatus AtWork = new RentStatus(Guid.Parse("dd6cb431-62b5-4ec4-be61-0cd77fd0eba1"), 
                                                        "В работе".ToLowerInvariant());
        public static RentStatus ToFree = new RentStatus(Guid.Parse("fe87c6bc-d43b-4e8e-91ea-5095742854d8"), 
                                                        "Возвращен".ToLowerInvariant());

        public RentStatus(Guid id, string name)
        : base(id, name)
        {
        }

        public static IEnumerable<RentStatus> List() =>
        new[] { AtWork, ToFree };

        public static RentStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new RentDomainException($"Возможные значения для статуса аренды: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static RentStatus From(Guid id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new RentDomainException($"Возможные значения для статуса аренды: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
