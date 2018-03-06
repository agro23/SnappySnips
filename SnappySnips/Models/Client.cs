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
        public void SetStylistId(int id)
        {
            _stylistId = id;
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

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._stylistId;
            cmd.Parameters.Add(stylistId);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

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

        // *********************************************************************************
        public List<Stylist> GetStylists()
        {
            List<Stylist> allClientStylists = new List<Stylist>{};
            allClientStylists.Add(Stylist.Find(this.GetStylistId()));
          // FOR NOW IT'S JUST A CALL TO GetStylist and put in a list for consistantcy
          //   // List<Stylist> allStylists = new List<Stylist> {};
          //   // return allStylists;
          //   //
          //
          // List<Stylist> allClientStylists = new List<Stylist> {};
          // MySqlConnection conn = DB.Connection();
          // conn.Open();
          // var cmd = conn.CreateCommand() as MySqlCommand;
          //
          // cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @Stylist_id;";
          // MySqlParameter stylistId = new MySqlParameter();
          // stylistId.ParameterName = "@Stylist_id";
          // stylistId.Value = this._id;
          // cmd.Parameters.Add(stylistId);
          //
          //
          // var rdr = cmd.ExecuteReader() as MySqlDataReader;
          // while(rdr.Read())
          // {
          //   int clientId = rdr.GetInt32(0);
          //   string clientName = rdr.GetString(1);
          //   int clientStylistId = rdr.GetInt32(2);
          //   Stylist newStylist = new Stylist(Stylist.Find(clientStylistId).GetName());
          //   Console.WriteLine("Stylist = " + Stylist.Find(clientStylistId).GetName());
          //   allClientStylists.Add(newStylist);
          // }
          // conn.Close();
          // if (conn != null)
          // {
          //     conn.Dispose();
          // }

            return allClientStylists;
        }
        // *********************************************************************************

        public void ChangeStylist(int stylist)
        {

            _stylistId = stylist;

            List<Client> allStylistClients = new List<Client> {};

            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            // cmd.CommandText = @"UPDATE clients SET (stylist_id) VALUES (@stylist_id) WHERE id  = @client_id;";
            cmd.CommandText = @"UPDATE clients SET stylist_id = @stylist_id WHERE id  = @client_id;";

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = _stylistId;
            cmd.Parameters.Add(stylistId);

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@client_id";
            clientId.Value = this._id;
            cmd.Parameters.Add(clientId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        // *********************************************************************************

        public List<Specialty> GetSpecialties()
        {
            // List<Specialty> allStylistSpecialties = new List<Specialty> {};
            // return allStylistSpecialties; // Return an empty list for now

            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT specialties.* FROM clients
            JOIN treatments ON (clients.id = treatments.client_id)
            JOIN specialties ON (treatments.specialty_id = specialties.id)
            WHERE clients.id = @ClientId;";

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@ClientId";
            clientId.Value = this._id;
            cmd.Parameters.Add(clientId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Specialty> allClientSpecialties = new List<Specialty>{};

            int specialty_Id = 0;
            string specialtyName = "";

            while(rdr.Read())
            {
                try
                {
                    specialty_Id = rdr.GetInt32(0);
                    Console.WriteLine("specialty_Id: " + specialty_Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception reading specialty id is: " + ex);
                }
                try
                {
                    specialtyName = rdr.GetString(1);
                    Console.WriteLine("specialtyName: " + specialtyName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception reading specialty name is: " + ex);
                }
                Specialty newSpecialty = new Specialty(specialtyName);
                allClientSpecialties.Add(newSpecialty);
                Console.WriteLine("In rdr new specialty is: " + newSpecialty.GetName());
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allClientSpecialties;
        }



        //*********************************************************************************
        public static List<Specialty> GetAllSpecialties()
        // this saves me from a lot of dictionary declarations in HomeController
        {
            List<Specialty> allSpecialties = new List<Specialty> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int specialtyId = rdr.GetInt32(0);
              string specialtyName = rdr.GetString(1);
              Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
              allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }
        //*********************************************************************************

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
                clientName = rdr.GetString(1);
                clientStylistId = rdr.GetInt32(2);
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

        public static void Delete(int Id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = Id;

            cmd.Parameters.Add(thisId);
            cmd.ExecuteNonQuery();
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
