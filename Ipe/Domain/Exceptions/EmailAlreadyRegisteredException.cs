namespace Ipe.Domain.Errors
{
    public class EmailAlreadyRegisteredException : BaseException
    {
        public EmailAlreadyRegisteredException() : base("003", "Email Already Registered") { }
    }
}
