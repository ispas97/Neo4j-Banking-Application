using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jproject
{
    public class Account
    {
        public int id { get; set; }
        public int AccountID { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public SSN SSD { get; set; }

        public Address Address { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public String Amount { get; set; }


    }
}
