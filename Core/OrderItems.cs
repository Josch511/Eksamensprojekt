using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core
{
    public class OrderItems
    {
        [BsonId]
        public int _id { get; set; }

        public string serial { get; set; }
        public string image_url { get; set; }
        public string name { get; set; }
        public DateTime purchased_at { get; set; }

        [BsonElement("customer_id")]
        public int CustomerId { get; set; }
    }
}