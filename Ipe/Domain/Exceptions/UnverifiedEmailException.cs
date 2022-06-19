namespace Ipe.Domain.Errors;

public class UnverifiedEmailException : BaseException
{
    public UnverifiedEmailException() : base("004", "Unverified Email") { }
}
