using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLockerWebAPI
{
    public class User
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public string password { get; set; }

        public DateTime dob { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string town { get; set; }
        public string zipCode { get; set; }

        public string clubName { get; set; }

        public string nationalInsno { get; set; }

        public int account_Level_id { get; set; }

        public string subscription_Type_id { get; set; }
    }
}
