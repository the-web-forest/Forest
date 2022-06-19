namespace Ipe.Domain.Errors;

public class InvalidEmailValidationException : BaseException
{
    public InvalidEmailValidationException() : base("005", "Invalid Email Validation") { }
}
