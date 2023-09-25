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
    public partial class Form1 : Form
    {

        private GraphClient client;
        public DataProvider data = new DataProvider();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "edukacija");
            try
            {
                client.Connect();
                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String username = UserName.Text;
            String password = Password.Text;
            //String queryText = "create (c:JA{username:'" +
            //                           username + "', password:'" +
            //                           password + "'}) with(c) set c.id = id(c) return c";


            //var query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
            //List<Client> users = ((IRawGraphClient)client).ExecuteGetCypherResults<Client>(query).ToList();

            Client c = new Client();
            c.username = username;
            c.password = password;
            bool q = data.CheckUsernameAndPassword(c);
            if (q == false)
                MessageBox.Show("Wrong username or password!");
            else
            {

                if (c.username == "admin")
                {
                    Form7 form7 = new Form7(c);
                    form7.ShowDialog();
                }
                else
                {
                    Form2 form2 = new Form2(c);
                    form2.ShowDialog();
                }               
            }
        }

        private void UserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String username = ForUserName.Text;
            String password = ForPassword.Text;
            Client c = new Client();
            c.username = username;
            c.password = password;
            bool q = data.CheckUsername(c);
            bool p = data.CheckClient(c);
            if (!p && !q)
            {
                String queryText = "create (c:Client{username:'" +
                                           username + "', password:'" +
                                           password + "'}) with(c) set c.id = id(c) return c";


                var query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                List<Client> users = ((IRawGraphClient)client).ExecuteGetCypherResults<Client>(query).ToList();

                Form3 form3 = new Form3(c);
                form3.ShowDialog();
            }
            else
            {
                MessageBox.Show("Username or password taken!");
            }
        }
    }
}
