using System;

namespace HairSalon.Models
{
  public class HairSalonModel
  {

    private int _id;
    private string _name;

    public HairSalonModel(string name, int id = 0)
    {
      _name = name;
      _id = id;
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

    public static void DeleteAll()
    {
        // MySqlConnection conn = DB.Connection();
        // conn.Open();
        //
        // var cmd = conn.CreateCommand() as MySqlCommand;
        // cmd.CommandText = @"DELETE FROM categories; ALTER TABLE categories AUTO_INCREMENT = 1;";
        // try
        // {
        //   cmd.ExecuteNonQuery();
        // }
        // catch (Exception ex)
        // {
        //   Console.WriteLine("Exception in DeleteAll");
        // }
        //
        // conn.Close();
        // if (conn != null)
        //   conn.Dispose();
    }


  }
}
