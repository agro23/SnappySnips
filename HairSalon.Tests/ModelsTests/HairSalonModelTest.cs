using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;


namespace HairSalon.Models.Tests
{
  [TestClass]
  public class HairSalonModelTest : IDisposable
 {
    public HairSalonModelTest()
    {
      Console.WriteLine("Change the port number and database name to whatever you need it to be...");
      // DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=my_database_name_test;";
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=Qsw7FaaOzOyVqz2m;port=8889;database=to_do_test;";
    }

    public void Dispose()
    {
        HairSalonModel.DeleteAll();
    }

    [TestMethod]
    public void Test_GetATestString_True()
    {
      Assert.AreEqual("this is a string from the model", HairSalonModel.GetString());
    }
  }
}
