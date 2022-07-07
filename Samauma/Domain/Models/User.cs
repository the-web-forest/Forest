using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Samauma.Domain.Models;

namespace Samauma.Domain.Models
{
	public class User: Model
	{
		[BsonElement("name")]
		public string Name { get; set; }

		[BsonElement("email")]
		public string Email { get; set; }

		[BsonElement("password")]
		public string Password { get; set; }

		[BsonElement("city")]
		public string City { get; set; }

		[BsonElement("state")]
		public string State { get; set; }

		[BsonElement("emailVerified")]
		public bool EmailVerified { get; set; }
	}
}

