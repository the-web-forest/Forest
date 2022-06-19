using System;
using System.Text.Json.Serialization;

namespace Bonsai.External.Services.Payment.Pagseguro.DTOS
{
	public class NewOrderRequestPayload
	{
		public class AmountData
		{
			[JsonPropertyName("value")]
			public double Value { get; set; }

			[JsonPropertyName("currency")]
			public string Currency { get; set; }
		}

		public class PaymentMethodCardData
        {
			[JsonPropertyName("encrypted")]
			public string Encrypted { get; set; }
		}

		public class PaymentMethodData
        {
			[JsonPropertyName("type")]
			public string Type { get; set; }

			[JsonPropertyName("installments")]
			public int Installments { get; set; }

			[JsonPropertyName("capture")]
			public bool Capture { get; set; }

			[JsonPropertyName("card")]
			public PaymentMethodCardData Card { get; set; }
		}

		[JsonPropertyName("reference_id")]
		public string ReferenceId { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("amount")]
		public AmountData Amount { get; set; }

		[JsonPropertyName("payment_method")]
		public PaymentMethodData PaymentMethod { get; set; }
	}
}

