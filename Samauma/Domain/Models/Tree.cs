using MongoDB.Bson.Serialization.Attributes;

namespace Samauma.Domain.Models
{
    public class Tree : Model
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("value")]
        public int? Value { get; set; }
        [BsonElement("biome")]
        public string Biome { get; set; }
    }
}
