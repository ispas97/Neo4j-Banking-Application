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
    public partial class Form4 : Form
    {
        public GraphClient client = null;
        public DataProvider data = new DataProvider();
        Client newclient;
        public Form4(Client c)
        {
            newclient = c;
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String amount = textBox2.Text;
            String toAccount = textBox1.Text;
            String fromAccount = textBox3.Text;
            data.DoTransaction(amount, toAccount, fromAccount,newclient);
        }
    }
}
