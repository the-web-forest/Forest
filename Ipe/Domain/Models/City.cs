using MongoDB.Bson.Serialization.Attributes;

namespace Ipe.Domain.Models
{
	public class City
	{
		[BsonElement("name")]
		public string Name { get; set; }
	}
}

