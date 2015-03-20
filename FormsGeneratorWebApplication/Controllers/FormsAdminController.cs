using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormsGeneratorWebApplication.Models;
namespace FormsGeneratorWebApplication.Controllers
{
    public class FormsAdminController : Controller
    {
        public static int TYPE_TEXT_BOX = 0;
        public static int TYPE_TEXT_AREA = 1;

        // GET: /FormsAdmin/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MakeForm() 
        {
          return View(new FormsModel());
        }

        [HttpGet]
        public ActionResult AddTextBox(int count) {
            ViewBag.FormCount = count;
            return PartialView("_TextBoxPartial", new TextBoxModel() { type = TYPE_TEXT_BOX });
        }

       [HttpGet]
        public ActionResult AddTextArea(int count) { 
            ViewBag.FormCount = count;
            return PartialView("_TextAreaPartial", new TextAreaModel() { type = TYPE_TEXT_AREA });
        }

	}
}