using MongoDB.Bson.Serialization.Attributes;

namespace Ipe.Domain.Models
{
    public class Order : Model
    {
        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }   
        
        [BsonElement("paymentId")]
        public string PaymentId { get; set; }

        [BsonElement("trees")]
        public List<OrderTree> Trees { get; set; }

        [BsonElement("value")]
        public double Value { get; set; }
    }
}