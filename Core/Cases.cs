using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Core
{
    [BsonIgnoreExtraElements]
    public class Cases
    {
        [BsonId]
        public int _id { get; set; }
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public List<string> media { get; set; } = new();
        public string status { get; set; } = string.Empty;
        public DateOnly createdAt { get; set; }
        public int? userId { get; set; }
        public int departmentId { get; set; }
        public int? typeId { get; set; }
        public int orderItemId { get; set; }
        public int? assignedEmployeeId { get; set; }
        public DateTime? eta { get; set; }
        public List<CaseUpdate> caseUpdates { get; set; } = new();
        
        [BsonElement("caseContact")]
        public CaseContact? caseContact { get; set; }
        
        [BsonIgnore]
        public OrderItems? orderItem { get; set; }
    }
    
    public class CaseContact
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
    }
}
