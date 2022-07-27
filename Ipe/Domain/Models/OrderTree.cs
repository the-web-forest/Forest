using MongoDB.Bson.Serialization.Attributes;

namespace Ipe.Domain.Models
{
    public class OrderTree
    {
        [BsonElement("treeId")]
        public string TreeId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("hastags")]
        public List<string> Hastags { get; set; }
    }
}
