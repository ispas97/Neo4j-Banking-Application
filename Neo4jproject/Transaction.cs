using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jproject
{
    public class Transaction
    {
        public int id { get; set; }
        public int Amount {get;set;}

        public DateTime DateTime { get; set; }

        public String  FromBankAccount { get; set; }

        public String ToBankAccount { get; set; }


    }
}
