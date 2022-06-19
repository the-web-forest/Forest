namespace Bonsai.Domain.Errors
{
    public class PaymentDeniedException: BaseException
    {
        public PaymentDeniedException(): base("002", "Payment Denied") { }
    }
}
