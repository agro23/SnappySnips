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

        [TestMethod]
        public void DeleteAll_ClientsFromDatabase_True()
        {
            //Arrange
            List<Client> testList = new List<Client>();
            Client testClient;

            testClient = new Client("Iron Man", 1);
            testClient.Save();
            testList.Add(testClient);

            testClient = new Client("Hulk", 1);
            testClient.Save();
            testList.Add(testClient);

            testClient = new Client("Thor", 1);
            testClient.Save();
            testList.Add(testClient);

            //Act
            Client.DeleteAll();

            //Assert
            CollectionAssert.AreNotEqual(testList, Client.GetAll());
        }

        [TestMethod]
        public void Find_ClientInDbToMatchList_True()
        {
            //Arrange
            Client.DeleteAll(); // clear the db first!
            List<Client> testList = new List<Client>();

            Client testClient1 = new Client("Iron Man", 1);
            testClient1.Save();
            testList.Add(testClient1);

            Client testClient2 = new Client("Hulk", 1);
            testClient2.Save();
            testList.Add(testClient2);

            Client testClient3 = new Client("Thor", 1);
            testClient3.Save();
            testList.Add(testClient3);

            //Act
            string x = Client.Find(1).GetName(); // should get first record
            string x1 = testList[0].GetName();

            //Assert
            Assert.AreEqual(x, x1);
        }

        [TestMethod]
        public void Update_SavedClientNameNotEqualToNewNameAfter_True()
        {
            //Arrange
            Client.DeleteAll(); // clear the db first!
            Client testClient = new Client("Bruce Banner", 1);
            testClient.Save();
            string x = testClient.GetName();

            //Act
            testClient.Update("Hulk", testClient.GetStylistId(), testClient.GetId());
            string x1 = testClient.GetName();

            //Assert
            Assert.AreNotEqual(x, x1);
        }

        [TestMethod]
        public void Delete_ClientFromDatabase_Void()
        {
            //Arrange
            List<Client> testList = new List<Client>();
            Client testClient;

            testClient = new Client("Iron Man", 1);
            testClient.Save();
            testList.Add(testClient);

            testClient = new Client("Hulk", 1);
            testClient.Save();
            testList.Add(testClient);

            testClient = new Client("Thor", 1);
            testClient.Save();
            testList.Add(testClient);

            testClient = new Client("Captain America", 1);
            testClient.Save();
            testList.Add(testClient);

            testClient = new Client("Black Widow", 1);
            testClient.Save();
            testList.Add(testClient);

            //Act
            Client.Delete(3); // Why is Stylist ahead by one? Do I reindex somewhere?!
            testList.RemoveAt(2);

            //Assert
            CollectionAssert.AreEqual(testList, Client.GetAll());
        }

    } // end class

} // end namespace
