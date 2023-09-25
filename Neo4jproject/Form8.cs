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
    public partial class Form8 : Form
    {
        public GraphClient client = null;
        public DataProvider data = new DataProvider();
        Client newclient;
        public Form8(Client c)
        {
            InitializeComponent();
            newclient = c;
            List<Account> accounts = data.GetAccounts();
            foreach (Account acc in accounts)
            {
                listBox1.Items.Add(acc.FirstName + " " + acc.LastName + " " + acc.Amount);
            }
        }
        //public Form8(Client c,List<Account> accounts)
        //{
        //    InitializeComponent();
        //    newclient = c;
        //    List<Account> accnts = accounts;
        //    foreach (Account acc in accnts)
        //    {
        //        listBox1.Items.Add(acc.FirstName + " " + acc.LastName + " " + acc.Amount);
        //    }

        //}
        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           //// listBox1.SelectedIndexChange
        }
    }
}

