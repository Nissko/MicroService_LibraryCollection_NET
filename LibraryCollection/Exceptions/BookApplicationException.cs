using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCollection.Application.Exceptions
{
    public class BookApplicationException
        : Exception
    {
        public BookApplicationException()
        { }

        public BookApplicationException(string message)
            : base(message)
        { }
    }
}
