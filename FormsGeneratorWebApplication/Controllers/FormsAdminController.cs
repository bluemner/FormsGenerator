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
namespace FormsGeneratorWebApplication.Controllers
{
    public class FormsAdminController : Controller
    {
        public static int TYPE_TEXT_BOX = 0;
        public static int TYPE_TEXT_AREA = 1;
        public static int TYPE_TEXT_RADIO = 2;
        public static int TYPE_TEXT_CHECKBOX = 3;


        private FormsDbContext db = new FormsDbContext();

        // GET: /FormsAdmin/
        public ActionResult Index()
        {

            var adminFormModel = new AdminFormModel()
            {
                forms = new List<FormsModel>()
            };



            return View(adminFormModel);
        }

        [HttpGet]
        public ActionResult MakeForm() 
        {

            return View(new FormsModel());
        
        }
        [HttpPost]
        public ActionResult MakeForm(FormsModel model){


            var x = 3;
            // This logic works, but when the view passes the model to 
            // this function, the model's FormItemList is null
           db.FormModels.Add(model);
           db.SaveChanges();
           Console.WriteLine("make forms ran");
               return View("Index");
        }
        
        [HttpGet]
        public ActionResult AddTextBox(int count) {
            ViewBag.TextBoxCount = count.ToString();
            return PartialView("_EditTextBoxPartial", new TextBoxModel());
        }

       [HttpGet]
        public ActionResult AddTextArea(int count) {
            ViewBag.TextBoxCount = count;
            return PartialView("_TextAreaPartial", new TextAreaModel() { type = TYPE_TEXT_AREA });
        }

       [HttpGet]
       public ActionResult AddCheckBoxes(int count, int numberOfSubElements)
       {
           var CheckboxModel = new CheckBoxesModel();
           CheckboxModel.checkboxes = new List<checkbox>();

           for(int i=0; i< numberOfSubElements; ++i){
              CheckboxModel.checkboxes.Add(new checkbox());
           }
           
           ViewBag.TextBoxCount = count;
           return PartialView("_CheckBoxesPartial",CheckboxModel);
       }

       [HttpGet]
       public ActionResult AddRadioButton(int count, int numberOfSubElements)
       {
           var RadioMod = new RadioButtonModel();
           RadioMod.options = new List<string>();

           for (int i = 0; i < numberOfSubElements; ++i)
           {
               RadioMod.options.Add("option"+i);
           }

           ViewBag.TextBoxCount = count;
           return PartialView("_RadioButtonPartial", RadioMod);
       }

	}
}