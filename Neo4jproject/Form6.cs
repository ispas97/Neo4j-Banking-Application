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
    public partial class Form6 : Form
    {

        public GraphClient client = null;
        public DataProvider data = new DataProvider();
        Client newclient;
        public Form6(Client c)
        {
            InitializeComponent();
            newclient = c;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String aboveAmount = textBox1.Text;
            List<Transaction> transactions=data.returnTransactions(aboveAmount);
            listBox1.Items.Clear();
            foreach (Transaction transact in transactions)
            {
                listBox1.Items.Add(transact.FromBankAccount+" "+transact.Amount+" "+transact.ToBankAccount+" "+transact.DateTime);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Transaction> translist=data.FindSuspiciousTransactions();
            listBox1.Items.Clear();
            foreach (Transaction transact in translist)
            {
                listBox1.Items.Add(transact.FromBankAccount + " " + transact.Amount + " " + transact.ToBankAccount + " " + transact.DateTime);
            }

        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
