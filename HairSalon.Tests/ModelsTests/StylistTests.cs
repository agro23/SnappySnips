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


    // [TestMethod]
    // public void GetAll_DatabaseStartsEmpty_0()
    // {
    //   //Arrange, Act
    //   int result = HairSalonModel.GetAll().Count;
    //
    //   //Assert
    //   Assert.AreEqual(0, result);
    // }

  }
}
