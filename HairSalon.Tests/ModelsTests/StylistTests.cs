using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;


namespace HairSalon.Models.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
 {
    public StylistTests()
    {
      Console.WriteLine("Change the port number and database name to whatever you need it to be...");
      // DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=my_database_name_test;";
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=Qsw7FaaOzOyVqz2m;port=8889;database=andy_grossberg_test;";
      //Allow User Variables=True
    }

    public void Dispose()
    {
        Stylist.DeleteAll(); // This will ultimately have to kill everything.
    }

    [TestMethod]
    public void Test_GetATestString_True()
    {
      Assert.AreEqual("this is a string from the model", Stylist.GetString());
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      // Arrange, Act
      Stylist firstStylist = new Stylist("Jabba the Hutt");
      Stylist secondStylist = new Stylist("Jabba the Hutt");

      // Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void GetAll_DatabaseStartsEmpty_0()
    {
      //Arrange, Act
      int result = -1;
      try
      {
        result = Stylist.GetAll().Count;
      }
      catch(Exception ex)
      {
        Console.WriteLine("Exception in Starts Empty test: " + ex);
      }
      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientssWithStylist_ClientList()
    {
      Stylist testStylist = new Stylist("Boba Fett");
      testStylist.Save();

      Client firstClient = new Client("Frozen Han", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Leia's Grenade", testStylist.GetId());
      secondClient.Save();


      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      CollectionAssert.AreEqual(testClientList, resultClientList);
    }

  }
}
