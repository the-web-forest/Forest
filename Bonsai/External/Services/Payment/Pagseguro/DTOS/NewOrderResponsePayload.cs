using System;
using System.Text.Json.Serialization;

namespace Bonsai.External.Services.Payment.Pagseguro.DTOS
{
	public class NewOrderResponsePayload
	{

		public class PaymentResponseData
        {
			[JsonPropertyName("code")]
			public string Code { get; set; }

			[JsonPropertyName("message")]
			public string Message { get; set; }

			[JsonPropertyName("reference")]
			public string Reference { get; set; }
		}

		[JsonPropertyName("status")]
		public string Status { get; set; }

		[JsonPropertyName("payment_response")]
		public PaymentResponseData PaymentResponse { get; set; }
	}
}

