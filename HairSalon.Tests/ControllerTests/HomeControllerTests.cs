using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;

namespace HairSalon.Controllers.Tests
{
    [TestClass]
    public class HomeControllerTest
    {

      [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            //Arrange
            //Act
            ActionResult result = new HomeController().Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
          public void CreateForm_ReturnsCorrectView_True()
          {
              //Arrange
              //Act
              ActionResult result = new HomeController().CreateForm();

              //Assert
              Assert.IsInstanceOfType(result, typeof(ViewResult));
          }

          // [TestMethod] // enable this when ClientsController is ready and Stylist.GetClients() works.
          // public void StylistDetails_ReturnsCorrectView_True()
          // {
          //     //Arrange
          //     ActionResult result = new HomeController().StylistDetails();
          //     //Act
          //
          //     //Assert
          //     Assert.IsInstanceOfType(result, typeof(ViewResult));
          // }
    }
}
