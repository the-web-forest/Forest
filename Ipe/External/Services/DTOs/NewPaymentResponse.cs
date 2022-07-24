using System;
using System.Text.Json.Serialization;

namespace Ipe.External.Services.DTOs
{
    public class NewPaymentResponse
    {
        [JsonPropertyName("paymentId")]
        public string PaymentId { get; set; }
        [JsonPropertyName("paymentStatus")]
        public string PaymentStatus { get; set; }
    }
}

