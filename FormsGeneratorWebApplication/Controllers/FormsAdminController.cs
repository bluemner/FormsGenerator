using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
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

        public const int TEMPLATE_FORM_TRUE_OR_FALSE = 0;
        public const int TEMPLATE_FORM_TEXTAREA = 1;
        public const int TEMPLATE_FORM_TEXTBOX = 2;
        



        private FormsDbContext db = new FormsDbContext();
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        // GET: /FormsAdmin/
        public ActionResult Index()
        {
            //Emailer("bluemner@uwm.edu, njjakusz@uwm.edu");
            var adminFormModel = new AdminFormModel()
            {
                forms = db.FormModels.ToList<FormsModel>()
            };





            return View(adminFormModel);
        }

        [HttpGet]
        public ActionResult MakeForm()
        {

            return View(new FormsModel());

        }
 
        [HttpPost]
        public ActionResult MakeForm(FormsModel model)
        {
            model.adminGUID = createGuid();
            var x = 3;
            // This logic works, but when the view passes the model to 
            // this function, the model's FormItemList is null
            //var user = userManager.FindById(User.Identity.GetUserId());
            //user.forms.Add(model);
            //model.user = user;
            db.FormModels.Add(model);
            db.SaveChanges();
            Console.WriteLine("make forms ran");

            ViewBag.GUID = model.adminGUID.ToString();
            return View("Email");
        }

        private Guid createGuid()
        {
            return Guid.NewGuid();
        }

        [HttpGet]
        public ActionResult AddTextBox(int count)
        {
            ViewBag.TextBoxCount = count.ToString();
            return PartialView("_EditTextBoxPartial", new TextBoxModel() { type = TYPE_TEXT_BOX});
        }

        [HttpGet]
        public ActionResult AddTextArea(int count)
        {
            ViewBag.TextBoxCount = count;
            return PartialView("_TextAreaPartial", new TextAreaModel() { type = TYPE_TEXT_AREA });
        }

        [HttpGet]
        public ActionResult AddCheckBoxes(int count, int numberOfSubElements)
        {
            var CheckboxModel = new CheckBoxesModel();
            CheckboxModel.type = TYPE_TEXT_CHECKBOX;
            CheckboxModel.checkboxes = new List<checkbox>();

            for (int i = 0; i < numberOfSubElements; ++i)
            {
                CheckboxModel.checkboxes.Add(new checkbox());
            }

            ViewBag.TextBoxCount = count;
            return PartialView("_CheckBoxesPartial", CheckboxModel);
        }

        [HttpGet]
        public ActionResult AddRadioButton(int count, int numberOfSubElements)
        {
            var RadioMod = new RadioButtonModel();
            RadioMod.options = new List<OptionsModel>();
            RadioMod.type = TYPE_TEXT_RADIO;
            for (int i = 0; i < numberOfSubElements; ++i)
            {
                RadioMod.options.Add(new OptionsModel(){ option ="" });
            }
            RadioMod.question = "Can pigs fly?";

            ViewBag.TextBoxCount = count;
            return PartialView("_RadioButtonPartial", RadioMod);
        }
        //creates a simple form for user if creating from template
        [HttpGet]
        public ActionResult Template(int type, int numberOfSubElements)
        {
            ViewBag.TextBoxCount = numberOfSubElements;
            // Create instance of a form
            var formModel = new FormsModel();
            formModel.FormItemIList = new List<FormItemModel>();
            switch (type)
            {
                case TEMPLATE_FORM_TRUE_OR_FALSE:
                    // populate form model with true or false
                    for (int i = 0; i < numberOfSubElements; ++i)
                    {
                        var RadioMod = new RadioButtonModel();
                        RadioMod.options = new List<OptionsModel>();

                        RadioMod.options.Add(new OptionsModel() { option = "True" });
                        RadioMod.options.Add(new OptionsModel() { option = "False" });

                        RadioMod.question = "Can pigs fly?";
                        formModel.FormItemIList.Add(RadioMod);
                    }
                    break;
                case TEMPLATE_FORM_TEXTAREA:
                    //populate form model with textarea questions
                    for (int i = 0; i < numberOfSubElements; ++i)
                    {
                        var TextAreaMod = new TextAreaModel();
                        TextAreaMod.value = "Pigs do fly.";
                        formModel.FormItemIList.Add(TextAreaMod);
                    }
                    break;
                case TEMPLATE_FORM_TEXTBOX:
                    //populate form model with textbox questions
                    for(int i = 0; i < numberOfSubElements; ++i)
                    {
                        var TextBoxMod = new TextBoxModel();
                        TextBoxMod.value = "Pigs do fly.";
                        formModel.FormItemIList.Add(TextBoxMod);
                    }
                    break;   
            }

            return View("MakeForm", formModel);
        }
        //TODO: low- priority. 
        //makes a copy of a pre-existing form for sake of convenience.
        [HttpGet]
        public ActionResult CopyForm(int type, int numberOfSubElements)
        {
            //ViewBag.TextBoxCount = numberOfSubElements;
            //// Create instance of a form
            //var fromModel = new FormsModel();
            //switch (type)
            //{
            //    case 0:

            //        // populate form model with true or false
            //        for (int i = 0; i < numberOfSubElements; ++i)
            //        {
            //            var RadioMod = new RadioButtonModel();
            //            RadioMod.options = new List<string>();

            //            RadioMod.options.Add("True");
            //            RadioMod.options.Add("False");

            //            RadioMod.question = "Can pigs fly?";
            //            fromModel.FormItemIList.Add(RadioMod);
            //        }
            //        return View(fromModel);
            //}
            return PartialView("Error");
        }

        [HttpGet]
        public ActionResult Email( FormsModel guid )
        {
            ViewBag.GUID = guid.adminGUID.ToString();
           /// ViewBag.GUID = "ABC123";
            return View();
        }

        [HttpPost]
        public ActionResult Email(String recipients, String guid)
        {
            //if (ModelState.IsValid)
            //{
                //TODO: Create new guid
                //      Create form for data base
                // form + guid
            var newGuid = Guid.Parse(guid);
            String link = Request.Url.ToString();
            link = link.Remove(link.IndexOf("FormsAdmin"));
            link = link + "Forms/Forms?guid=";
            Func<FormsModel, bool> compare = delegate(FormsModel form)
            {
                if (form.adminGUID == newGuid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            FormsModel copy = db.FormModels.First<FormsModel>(compare);
            var recipientList = recipients.Split(',');
            foreach (String r in recipientList)
            {
                var formModel = FormsModel.clone(copy);
                formModel.adminGUID = createGuid();
                var resultModel = new ResultModel();
                resultModel.adminGUID = newGuid;
                resultModel.userGUID = formModel.adminGUID;
                resultModel.active = true;
                db.FormModels.Add(formModel);
                db.ResultModels.Add(resultModel);
                db.SaveChanges();
                String uniqueLink = link + formModel.adminGUID.ToString();
                EmailLink(r, uniqueLink);
            }

            

            return View();
            //}
            //else
            //{
              //  return View();
            //}
        }

        private void EmailLink(String recipient, String link)
        {
                MailMessage mail = new MailMessage();
            mail.To.Add(recipient);
            mail.From = new MailAddress("formsgenerator@gmail.com");
            mail.Subject = "Please complete this Survey at your earliest convenience";
            String Body = "Complete the survey here:  " + link;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential("formsgenerator@gmail.com", "weakpassword");
                client.EnableSsl = true;
                client.Send(mail);
        }

    }
}