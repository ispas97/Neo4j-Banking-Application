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
    public partial class Form7 : Form
    {
        public GraphClient client = null;
        public DataProvider data = new DataProvider();
        Client newclient;
        public Form7(Client c)
        {
            InitializeComponent();
            newclient = c;
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(newclient);
            form6.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8(newclient);
            form8.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Account> accounts=data.findFraudRings();
            Form9 form9 = new Form9(newclient,accounts);
            form9.ShowDialog();

        }
    }
}
