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

    public static void DeleteAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM stylists; ALTER TABLE stylists AUTO_INCREMENT = 1;"; // should autincrement reset to 0 or 1?
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

Console.WriteLine("So, in Stylist-Find I think I have ID # " + id);

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);

        Console.WriteLine("I'm looking for record: " + id);

        Console.WriteLine("Got this far in Find!");
        int stylistId = 0;
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

        Console.WriteLine("stylistName = " + stylistName);
        Console.WriteLine("stylistId = " + stylistId);
        Stylist foundStylist= new Stylist(stylistName);

        conn.Close();
        if (conn != null)
        {
           conn.Dispose();
        }

        return foundStylist;
      }

      public void Edit(string newName)
      {
        // WHY EVEN SEND name HERE?
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;"; // Really? Where am I getting ID from???

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
        Console.WriteLine("So, I think I have ID # " + _id + " and name is: " + newName +". Should I look for this._id? " + this._id);
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

Console.WriteLine("So, in Delete I think I have ID # " + Id);

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
