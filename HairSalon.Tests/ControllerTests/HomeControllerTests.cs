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

          // [TestMethod]
          // public void Index_HasCorrectModelType_XXXXXX()
          // {
          //     //Arrange
          //     HomeController controller = new HomeController();
          //     IActionResult actionResult = controller.Index();
          //     ViewResult indexView = controller.Index() as ViewResult;
          //
          //     //Act
          //     var result = indexView.ViewData.Model;
          //
          //     //Assert
          //     Assert.IsInstanceOfType(result, typeof(String)); // this is the message passed to the screen as a string.
          //
          //     // Assert.IsInstanceOfType(result, typeof(Dictionary<string, object>)); // this is the message passed to the screen as a dictionary.
          // }

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
