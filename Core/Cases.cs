using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Core
{
    
    public class Cases
    {
        public int _id { get; set; }
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public List<string> media { get; set; } = new();
        public string status { get; set; } = string.Empty;
        public DateOnly updatedAt { get; set; }
        public DateOnly createdAt { get; set; }
        public int? userId { get; set; }
        public int departmentId { get; set; }
        public int? typeId { get; set; }
        public int orderItemId { get; set; }
        public int? assignedEmployeeId { get; set; }
        public List<CaseUpdate> caseUpdates { get; set; } = new();
    }
}
