
using MongoDB.Bson.Serialization.Attributes;

namespace Bonsai.Domain.Models
{
	public class Order: Model
	{
		[BsonElement("orderId")]
		public string OrderId { get; set; }

		[BsonElement("description")]
		public string Description { get; set; }

		[BsonElement("amount")]
		public string Amount { get; set; }

		[BsonElement("paymentStatus")]
		public string PaymentStatus { get; set; }

		[BsonElement("paymentRequest")]
		public string PaymentRequest { get; set; }

		[BsonElement("paymentResponse")]
		public string PaymentResponse { get; set; }
	}
}

