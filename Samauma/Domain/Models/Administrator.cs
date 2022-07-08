using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Samauma.Domain.Models
{
    public class Administrator : Model
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}

