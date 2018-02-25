using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private string _name;
        private int _id;
        private int _stylistId;

        public Client(string name, int stylistId, int id = 0)
        {
            _name = name;
            _stylistId = stylistId;
            _id = id;
        }

        public override bool Equals(System.Object otherClient)
        {
          if (!(otherClient is Client))
          {
              return false;
          }
          else
          {
               Client newClient = (Client) otherClient;
               bool idEquality = this.GetId() == newClient.GetId();
               bool nameEquality = this.GetName() == newClient.GetName();
               bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
               return (idEquality && nameEquality && stylistEquality);
           }
        }
        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public int GetStylistId()
        {
            return _stylistId;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@name, @stylist_id);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            Console.WriteLine("Inside Save() Name is " + this._name);

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._stylistId;
            cmd.Parameters.Add(stylistId);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            Console.WriteLine("id in Save() is " + _id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int clientStylistId = rdr.GetInt32(2);
                Client newClient = new Client(clientName, clientStylistId, clientId);
                allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            int clientStylistId = 0;

            while(rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                Console.WriteLine("Got Client id " + clientId);
                clientName = rdr.GetString(1);
                Console.WriteLine("Got Client Name " + clientName);
                clientStylistId = rdr.GetInt32(2);
                Console.WriteLine("Got Stylist id " + clientStylistId);
            }
            Client newClient = new Client(clientName, clientStylistId, clientId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newClient;
        }

        public void Update(string newName, int stylistsId, int myId)
        // Client.Update() must also take a stylistId even if it stays the same
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET name = @newName, stylist_id = @stylistId WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = myId;
            cmd.Parameters.Add(searchId);

            Console.WriteLine("So, I think I have ID # " + myId + " now and name is: " + newName +".");
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylistId";
            stylistId.Value = stylistsId;
            cmd.Parameters.Add(stylistId);

            cmd.ExecuteNonQuery();
            _name = newName;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients; ALTER TABLE clients AUTO_INCREMENT = 1;"; // should autincrement reset to 0 or 1?";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
