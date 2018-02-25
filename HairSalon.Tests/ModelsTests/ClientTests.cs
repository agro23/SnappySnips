using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }
        public ClientTests()
        {
            Console.WriteLine("Change the port number and database name to whatever you need it to be...");
            // DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=my_database_name_test;";
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=Qsw7FaaOzOyVqz2m;port=8889;database=andy_grossberg_test;";
            //Allow User Variables=True < -- is this still necessary?
        }

        [TestMethod] // Does object come into existance?
        public void GetName_ReturnsValueIfObjectExistsThenNamesMatch_True()
        {
            // Arrange, Act
            Client firstClient = new Client("Han Solo", 1);
            string clientName = firstClient.GetName();
            string testName = "Han Solo";
            // Assert
            Assert.AreEqual(clientName, testName);
        }

        [TestMethod] // Does GetAll method work and is db empty?
        public void GetAll_DatabaseStartsEmpty_0()
        {
            //Arrange, Act
            int result = Client.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod] // is object's Equals override working?
        public void Equals_ReturnsTrueIfNamesAreTheSame_Item()
        {
            // Arrange, Act
            Client firstClient = new Client("Han Solo", 1);
            Client secondClient = new Client("Han Solo", 1);

            // Assert
            Assert.AreEqual(firstClient, secondClient);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {
            //Arrange
            Client testClient = new Client("Chewbacca", 1);

            //Act
            testClient.Save();
            List<Client> result = Client.GetAll();
            List<Client> testList = new List<Client>{testClient};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

    } // end class

} // end namespace
