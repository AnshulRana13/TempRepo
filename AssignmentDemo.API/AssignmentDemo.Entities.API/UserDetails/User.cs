using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentDemo.Entities.API.UserDetails
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        // We can check if we can use EMail Class here for better strongly typed
        public string email { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public Address address { get; set; }
        public Company company { get; set; }

    }
}
