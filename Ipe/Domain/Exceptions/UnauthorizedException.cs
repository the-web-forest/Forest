namespace Ipe.Domain.Errors
{
    public class UnauthorizedException: BaseException
    {
        public UnauthorizedException(): base("001", "Unauthorized") { }
    }
}
