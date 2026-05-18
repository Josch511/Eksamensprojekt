using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Core
{
    
    public class Cases
    {
        public int _id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<string> media { get; set; }
        public string status { get; set; }
        public DateOnly updatedAt { get; set; }
        public DateOnly createdAt { get; set; }
        public int? userId { get; set; }

        public int departmentId { get; set; }
        public int? typeId { get; set; }
        public int orderItemId { get; set; }
    }
}
