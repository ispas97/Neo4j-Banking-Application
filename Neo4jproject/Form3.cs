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
    public partial class Form3 : Form
    {
        public static int id = 0;
        public GraphClient client=null;
        public DataProvider data = new DataProvider();
        Client newclient;
        public Form3(Client c)
        {
            newclient = c;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            id++;
            int AccountID = id;
            String firstName = FirstName.Text;
            String lastName = LastName.Text;
            String street = Street.Text;
            String city = textBox1.Text;
            String country = textBox2.Text;
            String phoneNumber = PhoneNumber.Text;
            String SSN = textBox3.Text;
            String amount = Amount.Text;
            Account acc = new Account();
            SSN ssn = new SSN();
            PhoneNumber pn = new PhoneNumber();
            Address add = new Address();
            acc.AccountID = AccountID;
            acc.FirstName = firstName;
            acc.LastName = lastName;
            acc.Amount = amount;
            ssn.number = SSN;
            pn.number = phoneNumber;
            add.Street = street;
            add.City = city;
            add.Country = country;
            data.CreateAccount(newclient,acc,ssn,pn,add);
           // acc.PhoneNumber = phoneNumber;
            Form1 form1 = new Form1();
            form1.ShowDialog();
            //String queryText = "create (a:Account{firstName:'" +
            //                           firstName + "', lastName:'" +
            //                           lastName + "', address:'" +
            //                           address + "', phoneNumber:'" +
            //                           phoneNumber + "', amount:'" +
            //                           amount + "'}) with(a) set a.id = id(a) return a";


            //var query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
            //List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();


            //Client c = new Client();
            //c.username = username;
            //c.password = password;
            //Form3 form3 = new Form3(c);
            //form3.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void FirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
