using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormsGeneratorWebApplication;
using FormsGeneratorWebApplication.Controllers;

namespace FormsGeneratorWebApplication.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        //Arrange
        //Act
        //Assert
        [TestMethod]
        public void Login()
        {
            //Arrange
            AccountController controller = new AccountController();
            //Act
            ActionResult result = controller.Login("") as ActionResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Register()
        {
            //Arrange
            AccountController controller = new AccountController();
            //Act
            ActionResult result = controller.Register() as ActionResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ManageChangePasswordSucces()
        {
            //Arrange
            AccountController controller = new AccountController();
            //Act
            ActionResult result = controller.Manage(AccountController.ManageMessageId.ChangePasswordSuccess) as ActionResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ManageSetPasswordSuccess()
        {
            //Arrange
            AccountController controller = new AccountController();
            //Act
            ActionResult result = controller.Manage(AccountController.ManageMessageId.SetPasswordSuccess) as ActionResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ManageRemoveLoginSuccess()
        {
            //Arrange
            AccountController controller = new AccountController();
            //Act
            ActionResult result = controller.Manage(AccountController.ManageMessageId.RemoveLoginSuccess) as ActionResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ManageError()
        {
            //Arrange
            AccountController controller = new AccountController();
            //Act
            ActionResult result = controller.Manage(AccountController.ManageMessageId.Error) as ActionResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}