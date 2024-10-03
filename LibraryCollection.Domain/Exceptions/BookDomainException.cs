namespace LibraryCollection.Domain.Exceptions
{
    public class BookDomainException : Exception
    {
        public BookDomainException() 
        { }

        public BookDomainException(string message) 
            : base(message)
        { }
    }
}
