using MediatR;
using System.Runtime.Serialization;

namespace LibraryCollection.Application.Application.Commands.Books
{
    public class UpdateBookCommand
        : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string? Name { get; private set; }

        [DataMember]
        public string? Description { get; private set; }

        [DataMember]
        public int? NumberOfPage { get; private set; }

        [DataMember]
        public int? AgeRestrict { get; private set; }

        [DataMember]
        public DateTime? ReleaseDate { get; private set; }

        [DataMember]
        public decimal? Price { get; private set; }

        [DataMember]
        public decimal? Discount { get; private set; }

        public UpdateBookCommand(Guid id, string? name, string? description,
            int? numberOfPage, int? ageRestrict, DateTime? releaseDate,
            decimal? price, decimal? discount)
        {
            Id = id;
            Name = name;
            Description = description;
            NumberOfPage = numberOfPage;
            AgeRestrict = ageRestrict;
            ReleaseDate = releaseDate;
            Price = price;
            Discount = discount;
        }
    }
}
