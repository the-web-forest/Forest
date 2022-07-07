using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ipe.Domain.Models
{
	public class MailVerification : Model
	{
		[BsonElement("role")]
		public string Role { get; set; }

		[BsonElement("email")]
		public string Email { get; set; }

		[BsonElement("token")]
		public string Token { get; set; }

		[BsonElement("activated")]
		public bool Activated { get; set; }

		[BsonElement("activatedAt")]
		public DateTime? ActivatedAt { get; set; }
	}
}

