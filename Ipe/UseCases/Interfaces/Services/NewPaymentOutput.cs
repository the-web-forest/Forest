using System;
namespace Ipe.UseCases.Interfaces.Services
{
    public class NewPaymentOutput
    {
        public bool Success { get; set; }
        public string PaymentId { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
    }
}

