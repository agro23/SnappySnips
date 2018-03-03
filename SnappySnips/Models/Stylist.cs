using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {

    private int _id;
    private string _name;

    public Stylist(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
      Stylist newStylist = (Stylist) otherStylist;
      bool idEquality = (this.GetId() == newStylist.GetId());
      bool nameEquality = (this.GetName() == newStylist.GetName());
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

    public static List<Stylist> GetAll()
    {
        List<Stylist> allStylists = new List<Stylist> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int stylistId = rdr.GetInt32(0);
          string stylistName = rdr.GetString(1);
          Stylist newStylist = new Stylist(stylistName, stylistId);
          allStylists.Add(newStylist);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allStylists;
    }

    public List<Client> GetClients()
    {
      // List<Client> allStylistClients = new List<Client> {};
      // return allStylistClients;

      List<Client> allStylistClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, clientId);
        allStylistClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
        return allStylistClients;
    }

    public List<Specialty> GetSpecialties()
    {
        // List<Specialty> allStylistSpecialties = new List<Specialty> {};
        // return allStylistSpecialties; // Return an empty list for now

        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;


        // cmd.CommandText = @"SELECT specialties.* FROM stylists
        //   JOIN skills ON (stylists.id = skills.stylist_id)
        //   JOIN specialties ON (skills.specialty_id = specialties.id)
        //   WHERE stylists.id = @StylistId;"; // nope ******

          // cmd.CommandText = @"SELECT items.* FROM categories
          //                 JOIN categories_items ON (categories.id = categories_items.category_id)
          //                 JOIN items ON (categories_items.item_id = items.id)
          //                 WHERE categories.id = @CategoryId;";

        // cmd.CommandText = @"SELECT specialties.* FROM stylists
        //   JOIN skills ON (stylists.id = skills.stylist_id)
        //   JOIN specialties ON (skills.specialty_id = specialties.id)
        //   WHERE stylists.id = @StylistId;"; // NOPE ******

        // cmd.CommandText = @"SELECT * FROM specialties
        //   JOIN skills ON (specialties.id = skills.specialty_id)
        //   JOIN stylists ON (stylists.id = stylist_id)
        //   WHERE specialties.id = @StylistId;"; // NOPE*****




        //
        // cmd.CommandText =  @"SELECT specialties.* FROM stylists
        //         JOIN skills ON (stylists.id = skills.stylist_id)
        //         JOIN items ON (categories_items.item_id = items.id)
        //         WHERE categories.id = @CategoryId;";

        cmd.CommandText = @"SELECT * FROM stylists
          JOIN skills ON (stylists.id = skills.stylist_id)
          JOIN specialties ON (skills.id = specialties.id)
          WHERE stylists.id = @StylistId;";


          // cmd.CommandText = @"SELECT items.* FROM rooms
          //   JOIN contents ON (rooms.id = contents.rooms)
          //   JOIN items ON (contents.items = items.id)
          //   WHERE rooms.id = @RoomId;";

        MySqlParameter stylistId = new MySqlParameter();
        stylistId.ParameterName = "@StylistId";
        stylistId.Value = this._id;
        cmd.Parameters.Add(stylistId);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Specialty> allStylistSpecialties = new List<Specialty>{};

        while(rdr.Read())
        {
          int skill_Id = rdr.GetInt32(0);
          int stylist_Id = rdr.GetInt32(1);
          int specialty_Id = rdr.GetInt32(2);
          Specialty newSpecialty = new Specialty(Specialty.Find(specialty_Id).GetName());

          allStylistSpecialties.Add(newSpecialty);
          Console.WriteLine(newSpecialty.GetName());
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }

        //Just pass somet crap back!
        Specialty someSpecialty = new Specialty("Curling Hair");
        allStylistSpecialties.Add(someSpecialty);
        Console.WriteLine("the list in stylist is this many: " + allStylistSpecialties.Count);

        return allStylistSpecialties;
    }

    public void AddSpecialtyToStylist(Specialty newSpecialty)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO skills (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

        MySqlParameter stylists = new MySqlParameter();
        stylists.ParameterName = "@StylistId";
        stylists.Value = _id;
        cmd.Parameters.Add(stylists);

        MySqlParameter specialties = new MySqlParameter();
        specialties.ParameterName = "@SpecialtyId";
        specialties.Value = newSpecialty.GetId();
        cmd.Parameters.Add(specialties);

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
        cmd.CommandText = @"DELETE FROM stylists; DELETE FROM clients; ALTER TABLE stylists AUTO_INCREMENT = 1; ALTER TABLE clients AUTO_INCREMENT = 1;"; // should autincrement reset to 0 or 1?
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

    public static void DeleteAllClientsFromStylist(int Id)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = @Id"; // should autincrement reset to 0 or 1?

        cmd.Parameters.Add(new MySqlParameter("@Id", Id));

        try
        {
          cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in DeleteAllClientsFromStylist: " + ex);
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
        cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@Name);";

        cmd.Parameters.Add(new MySqlParameter("@Name", _name));

        cmd.ExecuteNonQuery();
        _id = (int)cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
          conn.Dispose();
    }

    public static Stylist Find(int id) // New Code!
    {
        // Item foundItem= new Item("testDescription");
        // return foundItem;

        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);

        int stylistId = id; // try THAT instead of 0!
        string stylistName = "";

        try
        {
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
               stylistId = rdr.GetInt32(0);
               stylistName = rdr.GetString(1);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Find() Exception: " + ex);
        }

        Stylist foundStylist= new Stylist(stylistName);
        foundStylist._id = stylistId;

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return foundStylist;
    }

    public void Update(string newName, int myId)
    {
        // WHY EVEN SEND name HERE?
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;"; // Really? Where am I getting ID from???

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        // searchId.Value = _id;
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

    public static void Delete(int Id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      // cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";
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
