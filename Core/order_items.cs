using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class order_items
    {
        public int _id { get; set; }
        public string serial { get; set; }
        public string image_url { get; set; }
        public string name { get; set; }
        public DateTime purchased_at { get; set; }
        public int UserCustomer_id { get; set; }
    }
}
