using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Bonsai.Domain.Models
{
	public abstract class Model
	{
		[BsonId(IdGenerator = typeof(ObjectIdGenerator))]
		[BsonElement("_id")]
		[BsonRepresentation(BsonType.ObjectId)]
		public ObjectId Id { get; set; }

		[BsonElement("createdAt")]
		public DateTime CreatedAt { get; set; }

		[BsonElement("updatedAt")]
		public DateTime UpdatedAt { get; set; }
	}
}

