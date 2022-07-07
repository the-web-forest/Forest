using Ipe.Domain.Errors;

namespace Ipe.Domain.Exceptions
{
    public class InvalidUserIdException : BaseException
    {
        public InvalidUserIdException() : base("010", "Invalid User Id") { }
    }
}

