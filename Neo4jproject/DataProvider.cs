

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;



namespace Neo4jproject
{
    public class DataProvider
    {
        private GraphClient client;
        public DataProvider()
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "edukacija");
            try
            {
                client.Connect();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

        public void DeleteAccount(Client myclient)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            String myPassword = ".*" + myclient.password + ".*";
            queryDict.Add("myPassword", myPassword);

            var queryText = " match (c: Client)-[:HAS_ACCOUNT]->(acc)-[:HAS_ADDRESS]->(add)" + " where exists(c.password) and c.password =~ {myPassword} return add";
            var query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Address> addresses = ((IRawGraphClient)client).ExecuteGetCypherResults<Address>(query).ToList();

            Address address = addresses[0];

            queryText = " match (c: Client)-[:HAS_ACCOUNT]->(acc)-[:HAS_PHONE_NUMBER]->(pn)" + " where exists(c.password) and c.password =~ {myPassword} return pn";
            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<PhoneNumber> phnnums = ((IRawGraphClient)client).ExecuteGetCypherResults<PhoneNumber>(query).ToList();

            PhoneNumber phnnum = phnnums[0];

            queryText = " match (c: Client)-[:HAS_ACCOUNT]->(acc)-[:HAS_SSN]->(ssn)" + " where exists(c.password) and c.password =~ {myPassword} return ssn";
            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<SSN> ssns = ((IRawGraphClient)client).ExecuteGetCypherResults<SSN>(query).ToList();

            SSN ssn = ssns[0];

            queryText = " match (c: Client)-[:HAS_ACCOUNT]->(acc)" + " where exists(c.password) and c.password =~ {myPassword} detach delete acc";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteCypher(query);

            queryText = " match (acc:Account)-[:HAS_ADDRESS]->(add:Address)" + "where exists(add.Street) and add.Street= '" + address.Street + "' return add";
            
            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Address> addresses2 = ((IRawGraphClient)client).ExecuteGetCypherResults<Address>(query).ToList();

            if (addresses2.Count == 0)
            {
                queryText = "match (add:Address) where exists(add.Street) and add.Street='" + address.Street + "' delete add ";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                ((IRawGraphClient)client).ExecuteCypher(query);
            }

            queryText = " match (acc:Account)-[:HAS_PHONE_NUMBER]->(pn:PhoneNumber)" + "where exists(pn.number) and pn.number= '" + phnnum.number + "' return pn";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<PhoneNumber> phnnums2 = ((IRawGraphClient)client).ExecuteGetCypherResults<PhoneNumber>(query).ToList();

            if (phnnums2.Count == 0)
            {
                queryText = "match (pn:PhoneNumber) where exists(pn.number) and pn.number='" + phnnum.number + "' delete pn ";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                ((IRawGraphClient)client).ExecuteCypher(query);
            }

            queryText = " match (acc:Account)-[:HAS_SSN]->(ssn:SSN)" + "where exists(ssn.number) and ssn.number= '" + ssn.number + "' return ssn";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<SSN> ssns2 = ((IRawGraphClient)client).ExecuteGetCypherResults<SSN>(query).ToList();

            if (ssns2.Count == 0)
            {
                queryText = "match (ssn:SSN) where exists(ssn.number) and ssn.number='" + ssn.number + "' delete ssn ";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                ((IRawGraphClient)client).ExecuteCypher(query);
            }

            queryText = " match (c: Client)" + " where exists(c.password) and c.password =~ {myPassword} delete c";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteCypher(query);


        }

        public bool CheckClient(Client myclient)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            String myPassword = ".*" + myclient.password + ".*";
            queryDict.Add("myPassword", myPassword);

            var queryText = " match (c: Client)" + " where exists(c.password) and c.password =~ {myPassword} return c";
               
            var query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Client> clients = ((IRawGraphClient)client).ExecuteGetCypherResults<Client>(query).ToList();

            if (clients.Count == 0) return false;
            else return true;
        }

        public bool CheckUsername(Client myclient)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            String myUsername = ".*" + myclient.username + ".*";
            queryDict.Add("myUsername", myUsername);

            var queryText = " match (c: Client)" + " where exists(c.username) and c.username =~ {myUsername} return c";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Client> clients = ((IRawGraphClient)client).ExecuteGetCypherResults<Client>(query).ToList();

            if (clients.Count == 0) return false;
            else return true;
        }

        public bool CheckUsernameAndPassword(Client myclient)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            String myUserName = ".*" + myclient.username + ".*";
            String myPassword = ".*" + myclient.password + ".*"; 
            queryDict.Add("myUserName", myUserName);
            queryDict.Add("myPassword", myPassword);

            var queryText = " match (c: Client)" + " where exists(c.username) and c.username =~ {myUserName} and c.password=~  {myPassword} return c";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Client> clients = ((IRawGraphClient)client).ExecuteGetCypherResults<Client>(query).ToList();

            if (clients.Count == 0) return false;
            else return true;
        }

        public void EditAddress(Client myclient,Address add)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            String myPassword = ".*" + myclient.password + ".*";
            queryDict.Add("myPassword", myPassword);

            var queryText = " match (n)-[:HAS_ACCOUNT]->(acc)-[:HAS_ADDRESS]->(add)" + " where exists(n.password) and n.password =~ {myPassword} return add";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Address> addresses = ((IRawGraphClient)client).ExecuteGetCypherResults<Address>(query).ToList();

            Address addr = addresses[0];

            String myStreet = ".*" + addr.Street + ".*";
            queryDict.Add("myStreet", myStreet);

            queryText = " match (acc:Account)-[:HAS_ADDRESS]->(add:Address)" + " where exists(add.Street) and add.Street =~ {myStreet} return acc";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

            if (accounts.Count > 1)
            {
                queryText = " match (n)-[:HAS_ACCOUNT]->(acc)-[hasadd:HAS_ADDRESS]->(add)" + " where exists(n.password) and n.password =~ {myPassword} delete hasadd";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                ((IRawGraphClient)client).ExecuteCypher(query);

                queryText = "create (a:Address{Street:'" +
                                      add.Street + "', City:'" +
                                      add.City + "', Country:'" +
                                      add.Country + "'}) with(a) set a.id = id(a) return a";


                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                List<Address> addresses3 = ((IRawGraphClient)client).ExecuteGetCypherResults<Address>(query).ToList();

                queryText = " match (c:Client)-[:HAS_ACCOUNT]->(acc:Account),(add:Address)" + " where exists(c.password) and c.password =~ {myPassword} and add.Street= '" +add.Street + "' create (acc)-[:HAS_ADDRESS]->(add) return add";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                addresses = ((IRawGraphClient)client).ExecuteGetCypherResults<Address>(query).ToList();
            }
            else
            {

                queryText = " match (a { Street: '" + addr.Street + "'}) set a = { Street:'" +
                                           add.Street + "', City:'" +
                                           add.City + "', Country:'" +
                                           add.Country + "'} return a ";

                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                List<Address> addrs = ((IRawGraphClient)client).ExecuteGetCypherResults<Address>(query).ToList();
            }
        }

        public void EditPhoneNumber(Client myclient, PhoneNumber pn)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            String myPassword = ".*" + myclient.password + ".*";
            queryDict.Add("myPassword", myPassword);

            var queryText = " match (n)-[:HAS_ACCOUNT]->(acc)-[:HAS_PHONE_NUMBER]->(pn)" + " where exists(n.password) and n.password =~ {myPassword} return pn";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<PhoneNumber> phns = ((IRawGraphClient)client).ExecuteGetCypherResults<PhoneNumber>(query).ToList();

            PhoneNumber pnum = phns[0];

            String myNumber = ".*" + pnum.number + ".*";
            queryDict.Add("myNumber", myNumber);

            queryText = " match (acc:Account)-[:HAS_PHONE_NUMBER]->(pn:PhoneNumber)" + " where exists(pn.number) and pn.number =~ {myNumber} return acc";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

            if (accounts.Count > 1)
            {
                queryText = " match (n)-[:HAS_ACCOUNT]->(acc)-[haspn:HAS_PHONE_NUMBER]->(pn)" + " where exists(n.password) and n.password =~ {myPassword} delete haspn";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                ((IRawGraphClient)client).ExecuteCypher(query);

                queryText = "create (pn:PhoneNumber{number:'" +
                                       pn.number + "'}) with(pn) set pn.id = id(pn) return pn";


                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                List<PhoneNumber> phnnum = ((IRawGraphClient)client).ExecuteGetCypherResults<PhoneNumber>(query).ToList();

                queryText = " match (c:Client)-[:HAS_ACCOUNT]->(acc:Account),(pn:PhoneNumber)" + " where exists(c.password) and c.password =~ {myPassword} and pn.number= '" + pn.number + "' create (acc)-[:HAS_PHONE_NUMBER]->(pn) return pn";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                phnnum = ((IRawGraphClient)client).ExecuteGetCypherResults<PhoneNumber>(query).ToList();
            }
            else
            {

                queryText = " match (a { number: '" + pnum.number + "'}) set a = { number:'" +
                                                  pn.number + "'} return a ";

                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                List<PhoneNumber> pnmbrs = ((IRawGraphClient)client).ExecuteGetCypherResults<PhoneNumber>(query).ToList();
            }
        }

        public void EditSSN(Client myclient, SSN ssn)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            String myPassword = ".*" + myclient.password + ".*";
            queryDict.Add("myPassword", myPassword);

            var queryText = " match (n)-[:HAS_ACCOUNT]->(acc)-[:HAS_SSN]->(ssn)" + " where exists(n.password) and n.password =~ {myPassword} return ssn";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<SSN> ssns = ((IRawGraphClient)client).ExecuteGetCypherResults<SSN>(query).ToList();

            SSN sssn = ssns[0];

            String myNumber = ".*" + sssn.number + ".*";
            queryDict.Add("myNumber", myNumber);

            queryText = " match (acc:Account)-[:HAS_SSN]->(ssn:SSN)" + " where exists(ssn.number) and ssn.number =~ {myNumber} return acc";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

            if (accounts.Count > 1)
            {
                queryText = " match (n)-[:HAS_ACCOUNT]->(acc)-[hasssn:HAS_SSN]->(ssn)" + " where exists(n.password) and n.password =~ {myPassword} delete hasssn";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                ((IRawGraphClient)client).ExecuteCypher(query);

                queryText = "create (ssn:SSN{number:'" +
                                       ssn.number + "'}) with(ssn) set ssn.id = id(ssn) return ssn";


                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                ssns = ((IRawGraphClient)client).ExecuteGetCypherResults<SSN>(query).ToList();

                queryText = " match (c:Client)-[:HAS_ACCOUNT]->(acc:Account),(ssn:SSN)" + " where exists(c.password) and c.password =~ {myPassword} and ssn.number= '" + ssn.number + "' create (acc)-[:HAS_SSN]->(ssn) return ssn";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                ssns = ((IRawGraphClient)client).ExecuteGetCypherResults<SSN>(query).ToList();
            }
            else
            {

                queryText = " match (a { number: '" + sssn.number + "'}) set a = { number:'" +
                                                  ssn.number + "'} return a ";

                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                ssns = ((IRawGraphClient)client).ExecuteGetCypherResults<SSN>(query).ToList();
            }
        }

        public void AddMoneyToAccount(String amount,Client myclient)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            String myPassword = ".*" + myclient.password + ".*";
            queryDict.Add("myPassword", myPassword);

            var queryText = " match (n)-[:HAS_ACCOUNT]->(acc)" + " where exists(n.password) and n.password =~ {myPassword} return acc";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

            Account myacc = accounts[0];

            int am1 = Int32.Parse(myacc.Amount);
            int am2 = Int32.Parse(amount);
            String newamount =(am1+am2).ToString();

            //String myStreet = ".*" + addr.Street + ".*";
            //queryDict.Add("myStreet", myStreet);
            //queryText = " match (a { id: " + myacc.id + "}) set a = { Amount:'" +
            //                           newamount + "'} return a ";

            queryText = " match (a { id: " + myacc.id + "}) set a = {FirstName:'" +
                                       myacc.FirstName + "', LastName:'" +
                                       myacc.LastName + "', Amount:'" +
                                       newamount + "', AccountID:'" +
                                       myacc.AccountID + "'} with(a) set a.id = id(a) return a ";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
            List<Account> accnts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();
        }

        public void CreateAccount(Client newclient,Account acc,SSN ssn,PhoneNumber pn,Address add)
        {

            String queryText = "create (a:Account{FirstName:'" +
                                       acc.FirstName + "', LastName:'" +
                                       acc.LastName + "', Amount:'" +
                                       acc.Amount + "', AccountID:'" +
                                       acc.AccountID + "'}) with(a) set a.id = id(a) return a";


            var query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
            List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

            Account createdacc = accounts[0];
            int myid = createdacc.id;
            String mySSN = ".*" + ssn.number + ".*";
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("myid", myid);
            queryDict.Add("mySSN", mySSN);


            queryText = " match (a: Account),(s: SSN)" + " where exists(a.id) and a.id = "+ myid +" and exists(s.number) and s.number =~ {mySSN}" +
                " create (a)-[k:HAS_SSN]->(s) return s";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<SSN> ssns = ((IRawGraphClient)client).ExecuteGetCypherResults<SSN>(query).ToList();

            if (ssns.Count == 0)
            {
                 queryText = "create (s:SSN{number:'" +
                                           ssn.number + "'}) with(s) set s.id = id(s) return s";

                 query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                 List<SSN> otherssns = ((IRawGraphClient)client).ExecuteGetCypherResults<SSN>(query).ToList();//1

                queryText = " match (a: Account),(s: SSN)" + " where exists(a.id) and a.id = "+myid+" and exists(s.number) and s.number =~ {mySSN}" + " create (a)-[k:HAS_SSN]->(s) return s";

                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                otherssns = ((IRawGraphClient)client).ExecuteGetCypherResults<SSN>(query).ToList();//2
            }


            String myUserName = ".*" + newclient.username + ".*";
            queryDict.Add("myUserName", myUserName);

            queryText = " match (a:Account),(c: Client)" + " where exists(a.id) and a.id =" + myid+ " and exists(c.username) and c.username =~ {myUserName}" +
                " create (c)-[ac:HAS_ACCOUNT]->(a)";

            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteCypher(query);

            String myPhoneNumber = ".*" + pn.number + ".*";
            queryDict.Add("myPhoneNumber", myPhoneNumber);


            queryText = " match (a:Account),(pn: PhoneNumber)" + " where exists(a.id) and a.id ="+ myid +" and exists(pn.number) and pn.number =~ {myPhoneNumber}"+
                " create (a)-[ph:HAS_PHONE_NUMBER]->(pn) return pn";

            query = new Neo4jClient.Cypher.CypherQuery(queryText,queryDict, CypherResultMode.Set);
            List<PhoneNumber> pns = ((IRawGraphClient)client).ExecuteGetCypherResults<PhoneNumber>(query).ToList();

            if (pns.Count==0)
            {
                queryText = "create (pn:PhoneNumber{number:'" +
                                       pn.number + "'}) with(pn) set pn.id = id(pn) return pn";

                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                List<PhoneNumber> phoneNumbers = ((IRawGraphClient)client).ExecuteGetCypherResults<PhoneNumber>(query).ToList();

                queryText = " match (a:Account),(pn: PhoneNumber)" + " where exists(a.id) and a.id = "+ myid +" and exists(pn.number) and pn.number =~ {myPhoneNumber}" +
                " create (a)-[ph:HAS_PHONE_NUMBER]->(pn) return pn";

                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                phoneNumbers = ((IRawGraphClient)client).ExecuteGetCypherResults<PhoneNumber>(query).ToList();
            }

            String myAddress = ".*" + add.Street + ".*";
            queryDict.Add("myAddress",myAddress);

            queryText = " match(a: Account),(add: Address)" + " where exists(a.id) and a.id ="+ myid +" and exists(add.Street) and add.Street =~ {myAddress}"+
                " create (a)-[:HAS_ADDRESS]->(add) return add";
            query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List <Address> ads = ((IRawGraphClient)client).ExecuteGetCypherResults<Address>(query).ToList();

            if (ads.Count == 0)
            {
                queryText = "create (a:Address{Street:'" +
                                       add.Street + "', City:'" +
                                       add.City + "', Country:'" +
                                       add.Country + "'}) with(a) set a.id = id(a) return a";


                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                List<Address> addresses = ((IRawGraphClient)client).ExecuteGetCypherResults<Address>(query).ToList();

                queryText = " match(a: Account),(add: Address)" + " where exists(a.id) and a.id ="+ myid+ " and exists(add.Street) and add.Street =~ {myAddress}" +
                " create (a)-[:HAS_ADDRESS]->(add) return add";
                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                addresses = ((IRawGraphClient)client).ExecuteGetCypherResults<Address>(query).ToList();
            }



        }

        public void DoTransaction(String amount,String toAccount,String fromAccount, Client myclient)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            String acc1 = ".*" + fromAccount + ".*";
            queryDict.Add("acc1", acc1);

            var queryText = " match (n)-[:HAS_ACCOUNT]->(acc)" + " where exists(n.username) and n.username =~ {acc1} return acc";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
            List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

            if (accounts.Count > 1)
                return;
            else
            {
                Account myacc1 = accounts[0];

                int am1 = Int32.Parse(myacc1.Amount);
                int am2 = Int32.Parse(amount);
                String newamount = (am1 - am2).ToString();

                queryText = " match (a { id: " + myacc1.id + "}) set a = {FirstName:'" +
                                           myacc1.FirstName + "', LastName:'" +
                                           myacc1.LastName + "', Amount:'" +
                                           newamount + "', AccountID:'" +
                                           myacc1.AccountID + "'} with(a) set a.id = id(a) return a ";

                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                List<Account> accnts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

                myacc1 = accnts[0];

                String acc2 = ".*" + toAccount + ".*";
                queryDict.Add("acc2", acc2);

                queryText = " match (n)-[:HAS_ACCOUNT]->(acc)" + " where exists(n.username) and n.username =~ {acc2} return acc";

                query = new Neo4jClient.Cypher.CypherQuery(queryText, queryDict, CypherResultMode.Set);
                List<Account> accounts2 = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();
                
                if (accounts2.Count > 1)
                    return;
                else
                {
                    Account myacc2 = accounts2[0];

                     am1 = Int32.Parse(myacc2.Amount);
                     am2 = Int32.Parse(amount);
                     newamount = (am1 + am2).ToString();

                    queryText = " match (a { id: " + myacc2.id + "}) set a = {FirstName:'" +
                                          myacc2.FirstName + "', LastName:'" +
                                          myacc2.LastName + "', Amount:'" +
                                          newamount + "', AccountID:'" +
                                          myacc2.AccountID + "'} with(a) set a.id=id(a) return a ";

                    query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                    List<Account> accnts2 = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

                    myacc2 = accnts2[0];

                    int transamount = Int32.Parse(amount);

                    queryText = "create (a:Transaction{Amount:" +
                                       transamount + ", DateTime:'" +
                                       DateTime.Now + "', FromBankAccount:'" +
                                       fromAccount + "', ToBankAccount:'" +
                                       toAccount + "'}) with(a) set a.id = id(a) return a";

                    query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                    List<Transaction> transactions = ((IRawGraphClient)client).ExecuteGetCypherResults<Transaction>(query).ToList();

                    Transaction trans = transactions[0];

                    queryText = " match (a1:Account),(t:Transaction)" + " where a1.id= " + myacc1.id + " and t.id= " + trans.id +
                        " create (t)-[:FROM_ACCOUNT]->(a1)";

                    query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                    ((IRawGraphClient)client).ExecuteCypher(query);

                    queryText = " match (a2:Account),(t:Transaction)" + " where a2.id= " + myacc2.id + " and t.id= " + trans.id +
                        " create (t)-[:TO_ACCOUNT]->(a2)";

                    query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                    ((IRawGraphClient)client).ExecuteCypher(query);



                }
            }
        }

        public List<Account> findFraudRings()
        {
            var queryText = "match (accountHolder:Account)-[]->(contactInformation) with contactInformation,count(accountHolder) as ringSize, collect(accountHolder) as fraudRing" +
                " where ringSize > 1 match p =(contactInformation)<-[]-(accountHolder2:Account),q =(accountHolder2)-[r: HAS_SSN | HAS_PHONE_NUMBER | HAS_ADDRESS]->(unsecuredAccount) return distinct accountHolder2";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
            List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

            return accounts;
        }

        public List<Transaction> returnTransactions(String aboveAmount)
        {

            int myaboveAmount = Int32.Parse(aboveAmount);
            var queryText = " match (n:Transaction) where n.Amount>"+myaboveAmount+" return n";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
            List<Transaction> trans = ((IRawGraphClient)client).ExecuteGetCypherResults<Transaction>(query).ToList();
            return trans;
        }
        public List<Transaction> FindSuspiciousTransactions()
        {
            var queryText = " match (n:Account)  return n";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
            List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();
            int max = 0;
            List<Transaction> maxlist = null;
            foreach (Account account in accounts)
            {
                queryText = " match (t:Transaction)  where t.ToBankAccount = '" +account.FirstName + "' return t";

                query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
                List<Transaction> transactions = ((IRawGraphClient)client).ExecuteGetCypherResults<Transaction>(query).ToList();
                if (transactions.Count>max)
                {
                    maxlist = transactions;
                    max = transactions.Count;
                }

            }
            return maxlist;
        }
        public List<Account> GetAccounts()
        {
            var queryText = " match (a:Account)  return a";

            var query = new Neo4jClient.Cypher.CypherQuery(queryText, new Dictionary<string, object>(), CypherResultMode.Set);
            List<Account> accounts = ((IRawGraphClient)client).ExecuteGetCypherResults<Account>(query).ToList();

            return accounts;
        }

    }

    

}
