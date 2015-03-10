using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Forms(String guid) {

            return View();
        }

        //TODO Define method to add iteams
        
        /// <summary>
        ///     
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddFormIteam(int type) {
            // This will be a partial
            return View();
        }
	}
}