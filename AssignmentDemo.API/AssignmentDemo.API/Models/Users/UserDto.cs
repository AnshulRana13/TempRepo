using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentDemo.API.Models.Users
{
    public class UserDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        // We can check if we can use EMail Class here for better strongly typed
        public string email { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public AddressDto address { get; set; }
        public CompanyDto company { get; set; }
    }
}
