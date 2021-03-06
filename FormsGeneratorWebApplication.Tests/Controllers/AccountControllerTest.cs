﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormsGeneratorWebApplication;
using FormsGeneratorWebApplication.Controllers;
using FormsGeneratorWebApplication.Models;

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

        [TestMethod]
        public void ExternalLogin()
        {
            //Arrange
            AccountController controller = new AccountController();
            //Act
            ActionResult result = controller.ExternalLogin("", "");
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LinkLogin()
        {
            //Arrange
            AccountController controller = new AccountController();
            //Act
            ActionResult result = controller.LinkLogin("");
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LogOff()
        {
            //Arrange
            AccountController controller = new AccountController();
            //Act
            ActionResult result = controller.LogOff();
            
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LoginAsync()
        {
            //Arrange
            AccountController controller = new AccountController();
            var userMgr = controller.UserManager;
            ApplicationUser user = new ApplicationUser();
            user.UserName = "user";
            user.Id = "1";
            userMgr.Create(user, "12345");
            LoginViewModel model = new LoginViewModel();
            model.UserName = "user";
            model.Password = "12345";
            model.RememberMe = false;
            //Act
            Task<ActionResult> result = controller.Login(model, "") as Task<ActionResult>;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}