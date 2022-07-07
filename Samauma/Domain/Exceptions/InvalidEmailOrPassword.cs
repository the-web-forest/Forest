namespace Samauma.Domain.Errors
{
    public class InvalidEmailOrPassword : BaseException
    {
        public InvalidEmailOrPassword() : base("001", "Invalid Email Or Password") { }
    }
}
