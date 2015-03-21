﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormsGeneratorWebApplication.Models;
using System.Net;

namespace FormsGeneratorWebApplication.Controllers
{
    public class FormsController : Controller
    {

        //
        // GET: /Forms/
  
        [HttpGet]
        public ActionResult Index() {
           return View();
        }

        [HttpGet]
        public ActionResult Forms(string guid) {
            return View(laodContentFromDataBase(guid));
        }
        [HttpGet]
        public ActionResult About() { 
            return View();
        }

        //TODO Define method to add iteams
        
        /// <summary>
        ///     
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddFormIteam(int guid) {
            // This will be a partial
            return View();
        }

        private FormsModel laodContentFromDataBase(string guid) {

            //TODO Laod the content from database
            return new FormsModel()
            {

            };
        }


	}
}