using System;
namespace Bonsai.UseCases.Interfaces.Services.Payment
{
	public class PaymentOutput
	{
		public bool IsPaymentSuccess { get; set; }
		public string PaymentStatus { get; set; }
		public string PaymentRequest { get; set; }
		public string PaymentResponse { get; set; }
	}
}

