using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ipe.Domain.Models
{
    public class PasswordReset : Model
	{
		[BsonElement("role")]
		public string Role { get; set; }

		[BsonElement("email")]
		public string Email { get; set; }

		[BsonElement("token")]
		public string Token { get; set; }

		[BsonElement("activated")]
		public bool Reseted { get; set; }

		[BsonElement("activatedAt")]
		public DateTime? ResetedAt { get; set; }
	}
}

