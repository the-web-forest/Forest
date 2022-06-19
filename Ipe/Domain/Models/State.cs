using MongoDB.Bson.Serialization.Attributes;

namespace Ipe.Domain.Models
{
	public class State: Model
	{
		[BsonElement("initials")]
		public string Initials { get; set; }

		[BsonElement("name")]
		public string Name { get; set; }

		[BsonElement("cities")]
		public List<City> Cities { get; set; }
	}
}

