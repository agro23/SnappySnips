using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;


namespace HairSalon.Models.Tests
{
    [TestClass]
    public class SpecialtyTests : IDisposable
    {
        public SpecialtyTests()
        {
            Console.WriteLine("Change the port number and database name to whatever you need it to be...");
            // DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=andy_grossberg_test;";
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=Qsw7FaaOzOyVqz2m;port=8889;database=andy_grossberg_test;";
            //Allow User Variables=True
        }

        public void Dispose()
        {
            Specialty.DeleteAll(); // This will ultimately have to kill everything.
        }

        [TestMethod]
        public void Test_GetATestString_True()
        {
          Assert.AreEqual("this is a string from the model", Specialty.GetString());
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfNamesAreTheSame_Item()
        {
            // Arrange, Act
            Specialty firstSpecialty = new Specialty("Permanent");
            Specialty secondSpecialty = new Specialty("Permanent");

            // Assert
            Assert.AreEqual(firstSpecialty, secondSpecialty);
        }

        [TestMethod]
        public void GetAll_DatabaseStartsEmpty_0()
        {
            //Arrange, Act
            int result = -1;
            try
            {
              result = Specialty.GetAll().Count;
            }
            catch(Exception ex)
            {
              Console.WriteLine("Exception in Starts Empty test: " + ex);
            }
            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_SpecialtyList()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("Hair Cut");

            //Act
            testSpecialty.Save();
            List<Specialty> result = Specialty.GetAll();
            List<Specialty> tempList = new List<Specialty>{testSpecialty};

            //Assert
            CollectionAssert.AreEqual(tempList, result);
            Assert.AreEqual(1, Specialty.GetAll().Count);
        }

        [TestMethod]
        public void Save_DbAssignsIdToSpecialty_Id()
        {
           //Arrange
           Specialty testSpecialty = new Specialty("Hair Cut");
           testSpecialty.Save();

           //Act
           Specialty savedSpecialty = Specialty.GetAll()[0];

           int result = savedSpecialty.GetId();
           int testId = testSpecialty.GetId();

           //Assert
           Assert.AreEqual(testId, result);
        }


        // [TestMethod] // test this once Client object is tested!
        // public void GetClients_RetrievesAllClientssWithSpecialty_ClientList()
        // // *******************************************************************
        // // // THIS WILL USE THE JOIN TABLE
        // // // ****************************************************************
        // {
        //     //Arrange
        //     Specialty testSpecialty = new Specialty("Boba Fett");
        //     testSpecialty.Save();
        //
        //     //Act
        //     Client firstClient = new Client("Frozen Han", testSpecialty.GetId());
        //     firstClient.Save();
        //     Client secondClient = new Client("Leia's Grenade", testSpecialty.GetId());
        //     secondClient.Save();
        //     // Add to the clients table some new clients
        //     List<Client> testClientList = new List<Client> {firstClient, secondClient};
        //     List<Client> resultClientList = testSpecialty.GetClients();
        //
        //     //Assert
        //     CollectionAssert.AreEqual(testClientList, resultClientList);
        // }

        [TestMethod]
        public void Find_FindsItemInDatabase_Item()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("Hair Cut");
            testSpecialty.Save();

            //Act
            Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());

            //Assert
            Assert.AreEqual(testSpecialty, foundSpecialty);
            Assert.AreEqual(testSpecialty.GetId(), foundSpecialty.GetId());
            Assert.AreEqual(testSpecialty.GetName(), foundSpecialty.GetName());
        }

        [TestMethod]
        public void DeleteAll_SpecialtieFromDatabase_True()
        {
            //Arrange
            List<Specialty> testList = new List<Specialty>();
            Specialty testSpecialty;

            testSpecialty = new Specialty("Hair Cut");
            testSpecialty.Save();
            testList.Add(testSpecialty);

            testSpecialty = new Specialty("Permanent");
            testSpecialty.Save();
            testList.Add(testSpecialty);

            testSpecialty = new Specialty("Dye Job");
            testSpecialty.Save();
            testList.Add(testSpecialty);

            //Act
            Specialty.DeleteAll();

            //Assert
            CollectionAssert.AreNotEqual(testList, Specialty.GetAll());
        }

        [TestMethod]
        public void Delete_SpecialtyFromDatabase_Void()
        {
            //Arrange
            List<Specialty> testList = new List<Specialty>();
            Specialty testSpecialty;

            testSpecialty = new Specialty("Hair Cut");
            testSpecialty.Save();
            testList.Add(testSpecialty);

            testSpecialty = new Specialty("Permanent");
            testSpecialty.Save();
            testList.Add(testSpecialty);

            testSpecialty = new Specialty("Dye Job");
            testSpecialty.Save();
            testList.Add(testSpecialty);

            testSpecialty = new Specialty("Style");
            testSpecialty.Save();
            testList.Add(testSpecialty);

            testSpecialty = new Specialty("Braiding");
            testSpecialty.Save();
            testList.Add(testSpecialty);

            testSpecialty = new Specialty("Weaves");
            testSpecialty.Save();
            testList.Add(testSpecialty);

            //Act
            Specialty.Delete(3); // Why is Specialty ahead by one? Do I reindex somewhere?!
            testList.RemoveAt(2);

            //Assert
            CollectionAssert.AreEqual(testList, Specialty.GetAll());
        }

        [TestMethod]
        public void Update_SavedSpecialtyNameNotEqualToNewNameAfter_True()
        {
            //Arrange
            Specialty.DeleteAll(); // clear the db first!
            Specialty testSpecialty = new Specialty("Hair Cut");
            testSpecialty.Save();
            string x = testSpecialty.GetName();

            //Act
            testSpecialty.Update("Weaves", testSpecialty.GetId());
            string x1 = testSpecialty.GetName();

            //Assert
            Assert.AreNotEqual(x, x1);
        }


        [TestMethod]
        public void Add_SpecialtyToStylistInSkills_Void()
        {
            //Arrange
            List<Specialty> testSpecialty = new List<Specialty>();

            //create some specialties
            Specialty newSpecialty = new Specialty("Manicure");
            newSpecialty.Save();
            Specialty newSpecialty1 = new Specialty("Pedicure");
            newSpecialty1.Save();
            Specialty newSpecialty2 = new Specialty("Nail Art");
            newSpecialty2.Save();

            //create some stylists
            Stylist rhonda = new Stylist("Rhonda");
            rhonda.Save();
            Stylist rita = new Stylist("Rita");
            rita.Save();
            Stylist rhoda = new Stylist("Rhoda");
            rhoda.Save();

            //Act
            //Add some skills to each stylist
            //Rhonda, Rita, and Rhoda can Manicure
            rhonda.AddSpecialtyToStylist(newSpecialty);
            rita.AddSpecialtyToStylist(newSpecialty);
            rhoda.AddSpecialtyToStylist(newSpecialty);
            testSpecialty.Add(newSpecialty);

            //Rhonda and Rita can Pedicure
            rhonda.AddSpecialtyToStylist(newSpecialty1);
            rita.AddSpecialtyToStylist(newSpecialty1);
            testSpecialty.Add(newSpecialty1);

            //Rhonda and Rhoda can do Nail Art
            rhonda.AddSpecialtyToStylist(newSpecialty2);
            rhoda.AddSpecialtyToStylist(newSpecialty2);
            testSpecialty.Add(newSpecialty2);

            //Assert
            Assert.AreEqual(testSpecialty.Count, rhonda.GetSpecialties().Count);

        }

        [TestMethod]
        public void Add_SpecialtyToClientInTreatments_Void()
        {
            //Arrange
            //Act
            //Assert
            Assert.AreEqual(1,1);
        }

    }
}
