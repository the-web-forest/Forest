namespace Ipe.Domain.Errors;

public class InvalidPasswordException : BaseException
{
    public InvalidPasswordException() : base("002", "Invalid Username Or Password")  { }
}
