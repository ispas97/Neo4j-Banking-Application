using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace Neo4jproject
{
    public partial class Form9 : Form
    {
        public GraphClient client = null;
        public DataProvider data = new DataProvider();
        Client newclient;
        public Form9(Client c,List<Account> accs)
        {
            InitializeComponent();
            newclient = c;
            List<Account> accounts = accs;
            foreach (Account acc in accounts)
            {
                listBox1.Items.Add(acc.FirstName + " " + acc.LastName);
            }
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
