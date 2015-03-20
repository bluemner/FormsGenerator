using System;
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
        public ActionResult Forms() {
            var model =
                from r in _questions
                orderby r.Type
                select r;

            return View(model);
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

        //dummy data for questions

        static List<FormItemModel> _questions = new List<FormItemModel>
        {
            new FormItemModel{
                ID = 1,
                Postion = 1,
                Question = "Do you like to dance?",
                Type= 1,
                Options = new string[]{"Yes", "NO"}
            },
            new FormItemModel{
                ID = 1,
                Postion = 2,
                Question = "Have you been to Paris?",
                Type= 1,
                Options = new string[]{"Yes", "NO", "2-4 times.", "10+ times."}
            },
            new FormItemModel{
                ID = 1,
                Postion = 3,
                Question = "Packer will win this year superball?",
                Type= 1,
                Options = new string[]{"Agree", "Disagree"}
            }

        };



	}
}