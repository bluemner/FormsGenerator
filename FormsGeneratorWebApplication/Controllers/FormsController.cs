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
        public ActionResult Forms(String guid) {
            return View(loadContentFromDataBase(Guid.Parse(guid)));
        }

        [HttpPost]
        public ActionResult Forms(FormsModel model)
        {
            return View("Sucess");
        }
        [HttpGet]
        public ActionResult About() { 
            return View();
        }

        [HttpGet]
        public ActionResult OutOfTime() {
            return View();
        }

        [HttpGet]
        public ActionResult Sucess() {
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
            //model.FormItemIList.Add(new TextAreaModel());
            //model.FormItemIList.ElementAt<FormItemModel>(0).id = 1;
            //model.FormItemIList.ElementAt<FormItemModel>(0).postion = 1;
            //model.FormItemIList.ElementAt<FormItemModel>(0).question = "What is your name?";
            //model.FormItemIList.ElementAt<FormItemModel>(0).type = 0;

            //model.FormItemIList.Add(new TextBoxModel());
            //model.FormItemIList.ElementAt<FormItemModel>(1).id = 2;
            //model.FormItemIList.ElementAt<FormItemModel>(1).postion = 2;
            //model.FormItemIList.ElementAt<FormItemModel>(1).question = "Do you like banana?";
            //model.FormItemIList.ElementAt<FormItemModel>(1).type = 1;

            //model.FormItemIList.Add(new RadioButtonModel() { 
            //    id =3,
            //    postion =3,
            //    question ="Do you like radio button?",
            //    type= 2
            //});

            //IList<OptionsModel> op = new List<OptionsModel>();
            //var p = new OptionsModel();
            //p.option = "IDK";
            //p.question = model.FormItemIList.ElementAt<FormItemModel>(2);
            //op.Add(p);
            //p = new OptionsModel();
            //p.option = "Yes, I love It.";
            //op.Add(p);
            //p = new OptionsModel();
            //p.option = "No, I dont.";
            //op.Add(p);
            //model.FormItemIList.ElementAt<FormItemModel>(2).options = op;

            //model.FormItemIList.Add(new CheckBoxesModel()
            //{
            //    id = 4,
            //    postion = 4,
            //    question = "Which checkbox option you want to select?",
            //    type = 3,
            //    maxCheckable = 3,
            //    minCheckable = 1
            //});

            //IList<OptionsModel> list = new List<OptionsModel>();
            //OptionsModel c = new OptionsModel();
            //c.option = "IDK";
            //list.Add(c);
            //c = new OptionsModel();
            //c.option = "Yes, I love It.";
            //list.Add(c);
            //c = new OptionsModel();
            //c.option = "No, I dont.";
            //list.Add(c);
            //model.FormItemIList.ElementAt<FormItemModel>(3).options = list;

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