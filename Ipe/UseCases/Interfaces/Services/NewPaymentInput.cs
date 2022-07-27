using System;
namespace Ipe.UseCases
{
    public class NewPaymentInput
    {
        public string OrderId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public string CardToken { get; set; }
    }
}

