using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core
{
    public class OrderItems
    {
        [BsonId]
        public int _id { get; set; }

        public string serial { get; set; }
        public string imageUrl { get; set; }
        public string name { get; set; }
        public DateTime purchasedAt { get; set; }
        public int userId { get; set; }
    }
}