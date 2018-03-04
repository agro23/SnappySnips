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
            List<Specialty> tempSpecialty = new List<Specialty>();

            Specialty newSpecialty = new Specialty("Manicure");
            newSpecialty.Save();
            testSpecialty.Add(newSpecialty);
            Specialty newSpecialty1 = new Specialty("Pedicure");
            newSpecialty1.Save();
            testSpecialty.Add(newSpecialty1);
            Specialty newSpecialty2 = new Specialty("Nail Art");
            newSpecialty2.Save();
            testSpecialty.Add(newSpecialty2);

            Stylist rhonda = new Stylist("Rhonda");
            rhonda.Save();
            Stylist rita = new Stylist("Rita");
            rita.Save();
            Stylist rhoda = new Stylist("Rhoda");
            rhoda.Save();

            //Rhonda, Rita, and Rhoda can Manicure
            rhonda.AddSpecialtyToStylist(newSpecialty);
            rita.AddSpecialtyToStylist(newSpecialty);
            rhoda.AddSpecialtyToStylist(newSpecialty);
            tempSpecialty.Add(newSpecialty);

            //Rhonda and Rita can Pedicure
            rhonda.AddSpecialtyToStylist(newSpecialty1);
            rita.AddSpecialtyToStylist(newSpecialty1);
            tempSpecialty.Add(newSpecialty1);

            //Rhonda and Rhoda can do Nail Art
            rhonda.AddSpecialtyToStylist(newSpecialty2);
            rhoda.AddSpecialtyToStylist(newSpecialty2);
            tempSpecialty.Add(newSpecialty2);

            //Act

            // List<Specialty> r1 = new List<Specialty>{};
            // Console.WriteLine("Rhonda can: ");
            // r1 = rhonda.GetSkills(1);
            // Console.WriteLine("r1 Count is: " + r1.Count);
            // for (var i1=0; i1< r1.Count; i1++)
            // {
            //     Console.WriteLine(r1[i1].GetName());
            // }
            //
            // List<Specialty> r2 = new List<Specialty>{};
            // Console.WriteLine("Rita can: ");
            // r2 = rita.GetSkills(2);
            // for (var i2=0; i2< r2.Count; i2++)
            // {
            //     Console.WriteLine(r2[i2].GetName());
            // }
            //
            // List<Specialty> r3 = new List<Specialty>{};
            // Console.WriteLine("Rhoda can: ");
            // r3 = rhoda.GetSkills(3);
            // for (var i3=0; i3< r3.Count; i3++)
            // {
            //     Console.WriteLine(r3[i3].GetName());
            // }

            // testSpecialty.Add(newSpecialty);
            // tempSpecialty=Specialty.GetAll();

            Console.WriteLine("Specialty.Count is " + Specialty.GetAll().Count);
            Console.WriteLine("TestSpecialty.Count is " + testSpecialty.Count);
            Console.WriteLine("TempSpecialty.Count is " + tempSpecialty.Count);

            // for (var i=0; i< Specialty.GetAll().Count; i ++) {
            //     Console.WriteLine("Specialty name: " + tempSpecialty[i].GetName());
            // }

            // rhonda.AddSpecialtyToStylist(newSpecialty);
            Console.WriteLine("Stylist 1 is :" + Stylist.Find(1).GetName());
            Console.WriteLine("Stylist.Count is " + Stylist.GetAll().Count);
            Console.WriteLine("GetSpecialties.Count is " + rhonda.GetSpecialties().Count);
            // Console.WriteLine("testSpecialty.Count is " + testSpecialty.Count);
            // Console.WriteLine("tempSpecialty.Count is " + tempSpecialty.Count);

            //Assert
            // CollectionAssert.AreEqual(tempListOfSomeSort, rhonda.GetSpecialties());
            // CollectionAssert.AreEqual(tempSpecialty, rhonda.GetSpecialties());

            for (int s=0; s < tempSpecialty.Count; s++)
            {
                Console.WriteLine(tempSpecialty[s].GetName());
            }

            for (int s1=0; s1 < testSpecialty.Count; s1++)
            {
                Console.WriteLine(testSpecialty[s1].GetName());
            }

            List<Specialty> rhondaList = new List<Specialty>();
            rhondaList=rhonda.GetSpecialties();

            for (int s2=0; s2 < rhondaList.Count; s2++)
            {
                Console.WriteLine(rhondaList[s2].GetName());
            }

            for (int s3=0; s3 < rhondaList.Count; s3++)
            {
                Console.WriteLine("rhondaList[s3].GetName() = testSpecialty[s3].GetName() " + (rhondaList[s3].GetName().Equals(testSpecialty[s3].GetName())));
            }
            Console.WriteLine("rhondaList.Count = testSpecialty.Count " + (rhondaList.Count == testSpecialty.Count));
            // Assert.AreEqual(tempSpecialty, rhonda.GetSpecialties());
            Assert.AreEqual(tempSpecialty.Count, rhondaList.Count);


        }

        // [TestMethod]
        // public void Add_ClientsToSpecialty_Void()
        // // ***************************************************
        // // THIS WILL USE THE JOIN TABLE
        // // ***************************************************
        // {
        //     //Arrange
        //     List<Client> testList = new List<Client>();
        //     Client testClient;
        //
        //     testClient = new Client("Iron Man", 1);
        //     testClient.Save();
        //     testList.Add(testClient);
        //
        //     testClient = new Client("Hulk", 1);
        //     testClient.Save();
        //     testList.Add(testClient);
        //
        //     testClient = new Client("Thor", 1);
        //     testClient.Save();
        //     testList.Add(testClient);
        //
        //     testClient = new Client("Captain America", 1);
        //     testClient.Save();
        //     testList.Add(testClient);
        //
        //     testClient = new Client("Black Widow", 1);
        //     testClient.Save();
        //     testList.Add(testClient);
        //
        //     //Act
        //
        //     List<Client> newList = Specialty.Find(1).GetClients();
        //
        //     //Assert
        //     CollectionAssert.AreEqual(newList, Client.GetAll());
        // }

    }
}
