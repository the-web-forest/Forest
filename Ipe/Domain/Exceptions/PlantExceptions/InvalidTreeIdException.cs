namespace Ipe.Domain.Errors
{
    public class InvalidTreeIdException : BaseException
    {
        public InvalidTreeIdException() : base("011", "Invalid Tree Id") { }
    }
}