namespace Ipe.Domain.Errors;

public class InvalidPasswordResetException : BaseException
{
    public InvalidPasswordResetException() : base("009", "Invalid Password Reset Request") { }
}
