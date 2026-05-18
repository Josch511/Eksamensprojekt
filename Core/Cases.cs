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
        public DateOnly updated_at { get; set; }
        public DateOnly created_at { get; set; }
        public int? user_id { get; set; }

        public int department_id { get; set; }
        public int? type_id { get; set; }
        public int order_item_id { get; set; }
        public int? assigned_employee_id { get; set; }
    }
}
