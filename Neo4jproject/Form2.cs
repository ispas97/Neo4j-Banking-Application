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
    public partial class Form2 : Form
    {
        public GraphClient client = null;
        public DataProvider data = new DataProvider();
        Client newclient;
        public Form2(Client c)
        {
            newclient = c;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(newclient);
            form4.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(newclient);
            form5.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            data.DeleteAccount(newclient);
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
    }
}
