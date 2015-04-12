using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormsGeneratorWebApplication.Models;
using FormsGeneratorWebApplication.Utilities;
using System.Net;

namespace FormsGeneratorWebApplication.Controllers
{
    public class FormsController : Controller
    {
        private FormsDbContext db = new FormsDbContext();
        //
        // GET: /Forms/
  
        [HttpGet]
        public ActionResult Index() {
           return View();
        }

        [HttpGet]
        public ActionResult Forms(Guid guid) {
            return View(loadContentFromDataBase(guid));
        }

        [HttpPost]
        public ActionResult Forms(FormsModel model)
        {
            return View();
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
        public ActionResult AddFormItem(int guid) {
            // This will be a partial
            return View();
        }

        private FormsModel loadContentFromDataBase(Guid guid) {

            //TODO Load the content from database
            //FormsModel model = new FormsModel();
            //model.FormItemIList = new List<FormItemModel>();
            //model.FormItemIList.Add(new TextBoxModel());
            //model.FormItemIList.ElementAt<FormItemModel>(0).id = 1;
            //model.FormItemIList.ElementAt<FormItemModel>(0).postion = 1;
            //model.FormItemIList.ElementAt<FormItemModel>(0).question = "What is your name?";
            //model.FormItemIList.ElementAt<FormItemModel>(0).type = 0;
            //model.key = 1;
            //model.Name = "Survey 1";
            //model.StartDate = DateTime.Now;
            //model.EndDate = DateTime.Now.AddDays(1);
            //return model;
            
            //return new FormsModel
            //{

            //};
            Func<FormsModel, bool> compare = delegate(FormsModel form)
            {
                if (form.adminGUID == guid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            FormsModel result = db.FormModels.First<FormsModel>(compare);
            return result;
        }
	}
}