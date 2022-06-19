namespace Ipe.Domain.Errors;

public class InvalidEmailException : BaseException
{
    public InvalidEmailException() : base("008", "Invalid Email") { }
}
