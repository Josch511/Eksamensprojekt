using MongoDB.Bson.Serialization.Attributes;

namespace Core
{ 

[BsonIgnoreExtraElements]
    public class CaseUpdate
    {
        public required string message { get; set; }
        public required DateTime timestamp { get; set; }
        public bool isComment { get; set; } = false;
        public string? commentMessage { get; set; }
    }
}