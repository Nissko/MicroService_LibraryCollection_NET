namespace RentingOutBooksService.Application.Exceptions
{
    public class RentApplicationException : Exception
    {
        public RentApplicationException()
        { }

        public RentApplicationException(string message)
            : base(message)
        { }
    }
}
