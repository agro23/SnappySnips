using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
    public class Specialty
    {

        private int _id;
        private string _name;

        public Specialty(string name, int id = 0)
        {
          _name = name;
          _id = id;
        }

        public override bool Equals(System.Object otherSpecialty)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                Specialty newSpecialty = (Specialty) otherSpecialty;
                bool idEquality = (this.GetId() == newSpecialty.GetId());
                bool nameEquality = (this.GetName() == newSpecialty.GetName());
                return (idEquality && nameEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }


        public int GetId()
        {
            return _id;
        }

        public void SetId(int myId)
        {
            _id = myId;
        }

        public string GetName()
        {
            return _name;
        }

        public static string GetString()
        {
            return "this is a string from the model";
        }

        public static List<Specialty> GetAll()
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

        public List<Stylist> GetStylistsWithSpecialty()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT stylists.* FROM specialties
            JOIN skills ON (specialties.id = skills.specialty_id)
            JOIN stylists ON (skills.stylist_id = stylists.id)
            WHERE specialties.id = @SpecialtyId;";

            MySqlParameter specialtyId = new MySqlParameter();
            specialtyId.ParameterName = "@SpecialtyId";
            specialtyId.Value = this._id;
            cmd.Parameters.Add(specialtyId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Stylist> allStylistsWithSpecialty = new List<Stylist>{};

            int stylist_Id = 0;
            string stylistName = "";

            while(rdr.Read())
            {
                try
                {
                    stylist_Id = rdr.GetInt32(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception reading stylist id is: " + ex);
                }
                try
                {
                    stylistName = rdr.GetString(1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception reading stylist name is: " + ex);
                }
                Stylist newStylist = new Stylist(stylistName);
                allStylistsWithSpecialty.Add(newStylist);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allStylistsWithSpecialty;
        }

        public List<Client> GetClientsWithSpecialty()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT clients.* FROM specialties
            JOIN treatments ON (specialties.id = treatments.specialty_id)
            JOIN clients ON (treatments.client_id = clients.id)
            WHERE specialties.id = @SpecialtyId;";

            MySqlParameter specialtyId = new MySqlParameter();
            specialtyId.ParameterName = "@SpecialtyId";
            specialtyId.Value = this._id;
            cmd.Parameters.Add(specialtyId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Client> allClientsWithSpecialty = new List<Client>{};

            int client_Id = 0;
            string clientName = "";

            while(rdr.Read())
            {
                try
                {
                    client_Id = rdr.GetInt32(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception reading client id is: " + ex);
                }
                try
                {
                    clientName = rdr.GetString(1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception reading client name is: " + ex);
                }
                Client newClient = new Client(clientName, client_Id);
                allClientsWithSpecialty.Add(newClient);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allClientsWithSpecialty;
        }

        public List<Client> GetClients()
        {
            List<Client> allSpecialtyClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE specialty_id = @specialty_id;";

            MySqlParameter specialtyId = new MySqlParameter();
            specialtyId.ParameterName = "@specialty_id";
            specialtyId.Value = this._id;
            cmd.Parameters.Add(specialtyId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int clientSpecialtyId = rdr.GetInt32(2);
                Client newClient = new Client(clientName, clientSpecialtyId, clientId);
                allSpecialtyClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
                return allSpecialtyClients;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties; DELETE FROM skills; ALTER TABLE specialties AUTO_INCREMENT = 1; DELETE FROM treatments; ALTER TABLE treatments AUTO_INCREMENT = 1"; // should autincrement reset to 0 or 1?
            try
            {
              cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DeleteAll: " + ex);
            }

            conn.Close();
            if (conn != null)
              conn.Dispose();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@Name);";

            cmd.Parameters.Add(new MySqlParameter("@Name", _name));

            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
              conn.Dispose();
        }

        public static Specialty Find(int id) // New Code!
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            int specialtyId = id; // try THAT instead of 0!
            string specialtyName = "";

            try
            {
                var rdr = cmd.ExecuteReader() as MySqlDataReader;
                while (rdr.Read())
                {
                   specialtyId = rdr.GetInt32(0);
                   specialtyName = rdr.GetString(1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Find() Exception: " + ex);
            }

            Specialty foundSpecialty= new Specialty(specialtyName);
            foundSpecialty._id = specialtyId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundSpecialty;
        }

        public void Update(string newName, int myId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE specialties SET name = @newName WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = myId;
            cmd.Parameters.Add(searchId);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _name = newName;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteSpecialtyFromClient (int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM treatments WHERE id = @clientId;";

            MySqlParameter clientId = new MySqlParameter();
            clientId.ParameterName = "@clientId";
            clientId.Value = id;

            cmd.Parameters.Add(clientId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void Delete (int Id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties WHERE id = @thisId;";

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
    }
}
