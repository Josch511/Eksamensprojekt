using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core
{
    public class User
    {
        public int? _id { get; set; }
        public string? name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? role { get; set; }
        public int? departmentId { get; set; }
    }
}