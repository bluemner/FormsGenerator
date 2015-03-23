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
        [HttpPost]
        public ActionResult MakeForms(FormsModel model ){
           var formItemList = model.FormItemIList;
            
           for (int i = 0; i < formItemList.Count; ++i) { 
                var currentItem = formItemList[i];

                if (currentItem.GetType() == typeof(CheckBoxesModel))
                {
                    var casted = ((CheckBoxesModel)(currentItem));
                }
                else if (currentItem.GetType() == typeof(TextAreaModel))
                {
                  
                }
                else if (currentItem.GetType() == typeof(TextBoxModel))
                {
               
                }
           }
               return View("Sucess");
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
               RadioMod.options.Add("");
           }

           ViewBag.TextBoxCount = count;
           return PartialView("_RadioButtonPartial", RadioMod);
       }

	}
}