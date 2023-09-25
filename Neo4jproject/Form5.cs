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
    public partial class Form5 : Form
    {
        Client newclient;
        public GraphClient client = null;
        public DataProvider data = new DataProvider();
        public Form5(Client client)
        {
            newclient = client;
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String street = Street.Text;
            String city = textBox1.Text;
            String country = textBox2.Text;
            Address add = new Address();
            add.Street = street;
            add.City = city;
            add.Country = country;
            data.EditAddress(newclient,add);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String phoneNumber = PhoneNumber.Text;
            PhoneNumber pn = new PhoneNumber();
            pn.number = phoneNumber;
            data.EditPhoneNumber(newclient,pn);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String SSN = textBox3.Text;
            SSN ssn = new SSN();
            ssn.number = SSN;
            data.EditSSN(newclient, ssn);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String amount = Amount.Text;
            data.AddMoneyToAccount(amount, newclient);
        }
    }
}
