using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ipe.Domain.Models
{
	public abstract class Model
	{
		[BsonId]
		[BsonElement("_id")]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("createdAt")]
		public DateTime CreatedAt { get; set; }

		[BsonElement("updatedAt")]
		public DateTime UpdatedAt { get; set; }
	}
}

