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
    public void Equals_ReturnsTrueIfNamesAreTheSame_Item()
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
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Kermit the Frog");

      //Act
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> tempList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(tempList, result);
      Assert.AreEqual(1, Stylist.GetAll().Count);
    }

    [TestMethod]
    public void Save_DbAssignsIdToStylist_Id()
    {
     //Arrange
     Stylist testStylist = new Stylist("Sister Mary Elephant");
     testStylist.Save();

     //Act
     Stylist savedStylist = Stylist.GetAll()[0];

     int result = savedStylist.GetId();
     int testId = testStylist.GetId();

     //Assert
     Assert.AreEqual(testId, result);
    }


    [TestMethod] // test this once Client object is tested!
    public void GetClients_RetrievesAllClientssWithStylist_ClientList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Boba Fett");
      testStylist.Save();

      //Act
      Client firstClient = new Client("Frozen Han", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Leia's Grenade", testStylist.GetId());
      secondClient.Save();
      // Add to the clients table some new clients
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      //Assert
      CollectionAssert.AreEqual(testClientList, resultClientList);
    }

    [TestMethod]
    public void Find_FindsItemInDatabase_Item()
    {
    //Arrange
    Stylist testStylist = new Stylist("Mr. Potatohead");
    testStylist.Save();

    //Act
    Stylist foundStylist = Stylist.Find(testStylist.GetId());

    //Assert
    Assert.AreEqual(testStylist, foundStylist);
    Assert.AreEqual(testStylist.GetId(), foundStylist.GetId());
    Assert.AreEqual(testStylist.GetName(), foundStylist.GetName());
    }

    [TestMethod]
    public void Delete_StylistFromDatabase_Void()
    {
      //Arrange
      List<Stylist> testList = new List<Stylist>();
      Stylist testStylist;

      testStylist = new Stylist("Buffy the Vampire Slayer");
      testStylist.Save();
      testList.Add(testStylist);

      testStylist = new Stylist("Terminator T-1000");
      testStylist.Save();
      testList.Add(testStylist);

      testStylist = new Stylist("Fred Flintstone");
      testStylist.Save();
      testList.Add(testStylist);

      testStylist = new Stylist("Terminator T-800");
      testStylist.Save();
      testList.Add(testStylist);

      testStylist = new Stylist("George Jetson");
      testStylist.Save();
      testList.Add(testStylist);

      //Act
      Stylist.Delete(3); // Why is Stylist ahead by one? Do I reindex somewhere?!
      testList.RemoveAt(2);

      //Assert
      CollectionAssert.AreEqual(testList, Stylist.GetAll());
    }

  }
}
