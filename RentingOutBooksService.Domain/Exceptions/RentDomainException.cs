namespace RentingOutBooksService.Domain.Exceptions
{
    public class RentDomainException : Exception
    {
        public RentDomainException() 
        { }

        public RentDomainException(string message)
            : base(message)
        { }
    }
}
