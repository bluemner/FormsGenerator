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
using System.Threading.Tasks;
namespace FormsGeneratorWebApplication.Controllers
{
    [Authorize]
    public class FormsAdminController : Controller
    {
        public static int TYPE_TEXT_BOX = 0;
        public static int TYPE_TEXT_AREA = 1;
        public static int TYPE_TEXT_RADIO = 2;
        public static int TYPE_TEXT_CHECKBOX = 3;

        public const int TEMPLATE_FORM_TRUE_OR_FALSE = 0;
        public const int TEMPLATE_FORM_TEXTAREA = 1;
        public const int TEMPLATE_FORM_TEXTBOX = 2;




        //private static FormsDbContext db = new FormsDbContext();
        private static UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(new ApplicationDbContext());
        private static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(store);


        // GET: /FormsAdmin/
        public ActionResult Index()
        {
            FormsDbContext db = new FormsDbContext();
            //Emailer("bluemner@uwm.edu, njjakusz@uwm.edu");
            var curUser = userManager.FindById(User.Identity.GetUserId());
            var formList = new List<FormsModel>();
            var totalList = new List<int>();
            var completedList = new List<int>();
            var index = 0;
            foreach (UserForm uF in curUser.forms)
            {
                Func<FormsModel, bool> compare = delegate(FormsModel form)
                {
                    if (form.adminGUID == uF.formGUID)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                };
                var formToReturn = db.FormModels.First<FormsModel>(compare);
                formList.Add(formToReturn);
                totalList.Add(0);
                completedList.Add(0);
                foreach (ResultModel rM in db.ResultModels)
                {
                    if (rM.adminGUID == formToReturn.adminGUID)
                    {
                        totalList[index]++;
                        if (rM.active == false)
                        {
                            completedList[index]++;
                        }
                    }
                }
                index++;
            }
            var adminFormModel = new AdminFormModel()
            {
                forms = formList,
                total = totalList,
                completed = completedList
            };

            //Analytic("39951b69-b8c1-4a0b-8f19-02223bd6b403");
            //reminder("6099f5d9-b3bf-4846-b8fa-5d3191f64e18");

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
            FormsDbContext db = new FormsDbContext();

            model.adminGUID = createGuid();
            //var x = 3;
            // This logic works, but when the view passes the model to 
            // this function, the model's FormItemList is null
            var curUser = userManager.FindById(User.Identity.GetUserId());
            curUser.forms.Add(new UserForm() { formGUID = model.adminGUID, user = curUser });
            //model.user = user;
            db.FormModels.Add(model);
            Task<IdentityResult> task = userManager.UpdateAsync(curUser);
            //IdentityResult done = await task;
            //store.Context.SaveChanges();
            db.SaveChanges();
            Console.WriteLine("make forms ran");

            ViewBag.GUID = model.adminGUID.ToString();
            return View("Email");
        }

        [HttpGet]
        public ActionResult EditForm(String guid)
        {
            FormsDbContext db = new FormsDbContext();

            var compGUID = Guid.Parse(guid);
            Func<FormsModel, bool> compare = delegate(FormsModel form)
            {
                if (form.adminGUID == compGUID)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };

            FormsModel result = db.FormModels.First<FormsModel>(compare);

            return View(result);
        }

        [HttpPost]
        public ActionResult EditForm(FormsModel model)
        {
            FormsDbContext db = new FormsDbContext();
            //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            //foreach (FormItemModel item in model.FormItemIList)
            //{
            //    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            //}
            Func<FormsModel, bool> keyCompare = delegate(FormsModel form)
            {
                if (form.key == model.key)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            FormsModel result = db.FormModels.First<FormsModel>(keyCompare);
            var deleteQuestionList = new List<FormItemModel>();
            if (result.FormItemIList != null)
            {
                foreach (FormItemModel fIM in result.FormItemIList)
                {
                    deleteQuestionList.Add(fIM);
                    if (fIM.options != null)
                    {
                        var deleteOptionsList = new List<OptionsModel>();
                        foreach (OptionsModel oM in fIM.options)
                        {
                            deleteOptionsList.Add(oM);
                        }
                        foreach (OptionsModel oM in deleteOptionsList)
                        {
                            db.OptionsModels.Remove(oM);
                        }
                    }
                    if (fIM.selected != null)
                    {
                        var deleteSelectedList = new List<SelectedModel>();
                        foreach (SelectedModel oM in fIM.selected)
                        {
                            deleteSelectedList.Add(oM);
                        }
                        foreach (SelectedModel oM in deleteSelectedList)
                        {
                            db.SelectedModels.Remove(oM);
                        }
                    }
                }
                foreach (FormItemModel fIM in deleteQuestionList)
                {
                    db.FormItemModels.Remove(fIM);
                }
            }
            db.FormModels.Remove(result);
            db.SaveChanges();
            db.FormModels.Add(model);
            db.SaveChanges();
            var guid = model.adminGUID;
            var deleteList = new List<ResultModel>();
            foreach (ResultModel rM in db.ResultModels)
            {
                if (rM.adminGUID == guid)
                {
                    deleteList.Add(rM);
                }
            }
            var deleteResultModels = new List<ResultModel>();
            foreach (ResultModel rM in deleteList)
            {
                Func<FormsModel, bool> compare = delegate(FormsModel form)
                {
                    if (form.adminGUID == rM.userGUID)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                };
                var deleteThisForm = db.FormModels.First<FormsModel>(compare);
                if (deleteThisForm.FormItemIList != null)
                {
                    foreach (FormItemModel fIM in deleteThisForm.FormItemIList)
                    {
                        deleteQuestionList.Add(fIM);
                        if (fIM.options != null)
                        {
                            var deleteOptionsList = new List<OptionsModel>();
                            foreach (OptionsModel oM in fIM.options)
                            {
                                deleteOptionsList.Add(oM);
                            }
                            foreach (OptionsModel oM in deleteOptionsList)
                            {
                                db.OptionsModels.Remove(oM);
                            }
                        }
                        if (fIM.selected != null)
                        {
                            var deleteSelectedList = new List<SelectedModel>();
                            foreach (SelectedModel oM in fIM.selected)
                            {
                                deleteSelectedList.Add(oM);
                            }
                            foreach (SelectedModel oM in deleteSelectedList)
                            {
                                db.SelectedModels.Remove(oM);
                            }
                        }
                    }
                    foreach (FormItemModel fIM in deleteQuestionList)
                    {
                        fIM.FormsModel = null;
                        try
                        {
                            db.FormItemModels.Remove(fIM);
                        }
                        catch
                        {

                        }
                    }
                }
                deleteResultModels.Add(rM);
                db.FormModels.Remove(deleteThisForm);
                db.SaveChanges();
            }
            foreach(ResultModel rM in deleteResultModels)
            {
                db.ResultModels.Remove(rM);
                db.SaveChanges();
            }

            //4. call SaveChanges

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
            return PartialView("_EditTextBoxPartial", new FormItemModel() { type = TYPE_TEXT_BOX });
        }

        [HttpGet]
        public ActionResult AddTextArea(int count)
        {
            ViewBag.TextBoxCount = count;
            return PartialView("_TextAreaPartial", new FormItemModel() { type = TYPE_TEXT_AREA, options = new List<OptionsModel>() });
        }

        [HttpGet]
        public ActionResult AddCheckBoxes(int count, int numberOfSubElements)
        {
            var CheckboxModel = new FormItemModel();
            CheckboxModel.type = TYPE_TEXT_CHECKBOX;
            CheckboxModel.selected = new List<SelectedModel>();
            CheckboxModel.options = new List<OptionsModel>();

            for (int i = 0; i < numberOfSubElements; ++i)
            {
                CheckboxModel.selected.Add(new SelectedModel());
            }
            for (int i = 0; i < numberOfSubElements; ++i)
            {
                CheckboxModel.options.Add(new OptionsModel());
            }
            ViewBag.TextBoxCount = count;
            return PartialView("_CheckBoxesPartial", CheckboxModel);
        }

        [HttpGet]
        public ActionResult AddRadioButton(int count, int numberOfSubElements)
        {
            var RadioMod = new FormItemModel();
            RadioMod.options = new List<OptionsModel>();
            RadioMod.type = TYPE_TEXT_RADIO;
            for (int i = 0; i < numberOfSubElements; ++i)
            {
                RadioMod.options.Add(new OptionsModel() { option = "" });
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
                    for (int i = 0; i < numberOfSubElements; ++i)
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
        public ActionResult Email(FormsModel guid)
        {
            ViewBag.GUID = guid.adminGUID.ToString();
            /// ViewBag.GUID = "ABC123";
            return View();
        }

        [HttpPost]
        public ActionResult Email(String recipients, String guid)
        {
            FormsDbContext db = new FormsDbContext();

            //if (ModelState.IsValid)
            //{
            //TODO: Create new guid
            //      Create form for data base
            // form + guid
            recipients = recipients.Replace("\r\n", "");
            if (!String.IsNullOrEmpty(recipients))
            {
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
                    if (String.IsNullOrEmpty(r))
                    {
                        continue;
                    }
                    else
                    {
                        var formModel = FormsModel.clone(copy);
                        formModel.adminGUID = createGuid();
                        var resultModel = new ResultModel();
                        resultModel.adminGUID = copy.adminGUID;
                        resultModel.userGUID = formModel.adminGUID;
                        resultModel.active = true;
                        resultModel.email = r;
                        db.FormModels.Add(formModel);
                        db.ResultModels.Add(resultModel);
                        db.SaveChanges();
                        String uniqueLink = link + formModel.adminGUID.ToString();
                        EmailLink(r, uniqueLink);
                    }
                }

                ViewBag.GUID = link + guid;
            }

            return View("Sucess");
            //}
            //else
            //{
            //  return View();
            //}
        }

        [HttpGet]
        public ActionResult Sucess()
        {
            ViewBag.GUID = "";
            return View();
        }

        private void EmailLink(String recipient, String link)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Analytic(string guid)
        {
            FormsDbContext db = new FormsDbContext();

            var form = Guid.Parse(guid);
            var resultsList = db.ResultModels.ToList<ResultModel>();
            var correctList = new List<ResultModel>();
            //building the list of resultModels that are linked to base form
            foreach (ResultModel rL in resultsList)
            {
                if (rL.adminGUID == form && rL.active == false)
                {
                    correctList.Add(rL);
                }
            }
            Func<FormsModel, bool> compare = delegate(FormsModel f)
            {
                if (f.adminGUID == form)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            //getting base form
            var baseForm = db.FormModels.First<FormsModel>(compare);
            var formsList = new FormsListModel() { selectable = new List<IList<int>>(), form = baseForm, text = new List<IList<String>>() };
            //building the lists
            foreach (FormItemModel q in baseForm.FormItemIList)
            {
                if (q.type > 1)
                {
                    formsList.selectable.Add(new List<int>());
                    for (int i = 0; i < q.options.Count; ++i)
                    {
                        formsList.selectable.Last<IList<int>>().Add(0);
                    }
                }
                else
                {
                    formsList.text.Add(new List<string>());
                }
            }
            //populating the list
            foreach (ResultModel r in correctList)
            {
                Func<FormsModel, bool> compare1 = delegate(FormsModel f)
                {
                    if (f.adminGUID == r.userGUID)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                };

                var resultForm = db.FormModels.First<FormsModel>(compare1);
                var selectCounter = 0;
                var textCounter = 0;
                foreach (FormItemModel item in resultForm.FormItemIList)
                {
                    //radio buttons
                    if (item.type == 2)
                    {
                        var question = formsList.selectable.ElementAt<IList<int>>(selectCounter);
                        question[item.selectedOption] = question.ElementAt<int>(item.selectedOption) + 1;
                        selectCounter++;
                    }
                    //text
                    //text boxes are not working yet
                    else if (item.type < 2)
                    {
                        var question = formsList.text.ElementAt<IList<String>>(textCounter);
                        question.Add(item.answer);
                        textCounter++;
                    }
                    //checkboxes
                    else
                    {
                        var question = formsList.selectable.ElementAt<IList<int>>(selectCounter);
                        foreach (SelectedModel sM in item.selected)
                        {
                            var resp = sM.selected;
                            int index = 0;
                            var found = false;
                            var optionsList = item.options;
                            for (int i = 0; i < optionsList.Count; ++i)
                            {
                                if (resp == optionsList[i].option)
                                {
                                    index = i;
                                    found = true;
                                    break;
                                }
                            }
                            if (found)
                            {
                                question[index]++;
                            }
                        }

                        selectCounter++;
                    }
                }
            }

            for (var i = 0; i < formsList.form.FormItemIList.Count; ++i)
            {
                var formItem = formsList.form.FormItemIList[i];
                if (formItem.selected != null)
                {
                    for (var j = 0; j < formItem.selected.Count; j++)
                    {
                        formItem.selected[j].question = null;
                    }
                }
                if (formItem.options != null)
                {
                    for (var j = 0; j < formItem.options.Count; j++)
                    {
                        formItem.options[j].question = null;
                    }
                }
                formItem.FormsModel = null;
            }

                //ViewBag.Jsn = Json(new
                //{
                //    form = formsList.form,
                //    selectable = formsList.selectable,
                //    text = formsList.text

                //});
            return View(formsList);
        }

        private void reminder(String guid)
        {
            FormsDbContext db = new FormsDbContext();

            var baseGUID = Guid.Parse(guid);

            var resultsList = db.ResultModels.ToList<ResultModel>();
            var correctList = new List<ResultModel>();
            String link = Request.Url.ToString();
            link = link.Remove(link.IndexOf("FormsAdmin"));
            link = link + "Forms/Forms?guid=";
            foreach (ResultModel rL in resultsList)
            {
                if (rL.adminGUID == baseGUID && rL.active == true)
                {
                    EmailLink(rL.email, link + rL.userGUID.ToString());
                }
            }
        }
        [HttpGet]
        public DownloadFileActionResult DownloadAnalytic(string guid)
        {
            FormsDbContext db = new FormsDbContext();

            var form = Guid.Parse(guid);
            var resultsList = db.ResultModels.ToList<ResultModel>();
            var correctList = new List<ResultModel>();
            var textdt = new DataTable();
            //var selectdt = new DataTable();

            foreach (ResultModel rL in resultsList)
            {
                if (rL.adminGUID == form && rL.active == false)
                {
                    correctList.Add(rL);
                }
            }
            Func<FormsModel, bool> compare = delegate(FormsModel f)
            {
                if (f.adminGUID == form)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            var baseForm = db.FormModels.First<FormsModel>(compare);
            var formsList = new FormsListModel() { selectable = new List<IList<int>>(), form = baseForm, text = new List<IList<String>>() };
            foreach (FormItemModel q in baseForm.FormItemIList)
            {
                if (q.type > 1)
                {
                    formsList.selectable.Add(new List<int>());
                    //textdt.Columns.Add(q.question, typeof(int));
                    for (int i = 0; i < q.options.Count; ++i)
                    {
                        formsList.selectable.Last<IList<int>>().Add(0);
                    }
                }
                else
                {
                    formsList.text.Add(new List<string>());
                    //textdt.Columns.Add(q.question, typeof(string));

                }
            }

            foreach (ResultModel r in correctList)
            {
                Func<FormsModel, bool> compare1 = delegate(FormsModel f)
                {
                    if (f.adminGUID == r.userGUID)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                };

                var resultForm = db.FormModels.First<FormsModel>(compare1);
                var selectCounter = 0;
                var textCounter = 0;
                foreach (FormItemModel item in resultForm.FormItemIList)
                {
                    //radio buttons
                    if (item.type == 2)
                    {
                        var question = formsList.selectable.ElementAt<IList<int>>(selectCounter);
                        question[item.selectedOption] = question.ElementAt<int>(item.selectedOption) + 1;
                        selectCounter++;
                    }
                    //text
                    //text boxes are not working yet
                    else if (item.type < 2)
                    {
                        var question = formsList.text.ElementAt<IList<String>>(textCounter);
                        if (item.options.Count > 0)
                        {
                            question.Add(item.options[0].option);
                        }
                        textCounter++;
                    }
                    //checkboxes
                    else
                    {
                        var question = formsList.selectable.ElementAt<IList<int>>(selectCounter);
                        foreach (SelectedModel sM in item.selected)
                        {
                            var resp = sM.selected;
                            int index = 0;
                            var found = false;
                            var optionsList = item.options;
                            for (int i = 0; i < optionsList.Count; ++i)
                            {
                                if (resp == optionsList[i].option)
                                {
                                    index = i;
                                    found = true;
                                    break;
                                }
                            }
                            if (found)
                            {
                                question[index]++;
                            }
                        }

                        selectCounter++;
                    }
                }
            }

            var sCount = 0;
            var tCount = 0;
            int max =0;

            for (int j = 0; j < formsList.selectable.Count; ++j)
            {
                if (formsList.selectable[j].Count > max)
                {
                    max = formsList.selectable[j].Count;
                }
            }
            for(int j=0;j<formsList.text.Count;++j)
            {
                if (formsList.text[j].Count > max)
                {
                    max = formsList.text[j].Count;
                }
            }
            for (int i = 0; i < max; ++i)
            {
                var row = textdt.NewRow();
                textdt.Rows.Add(row);
            }
            for (int i = 0; i < formsList.form.FormItemIList.Count;++i)
            {
                if(formsList.form.FormItemIList[i].type >1) //selectable
                {
                    textdt.Columns.Add(formsList.form.FormItemIList[i].question, typeof(string));
                    foreach(DataColumn column in textdt.Columns)
                    {
                        if(column.ColumnName == formsList.form.FormItemIList[i].question)
                        {
                            int j = 0;
                            foreach(DataRow row in textdt.Rows)
                            {
                                if (j < formsList.selectable[sCount].Count)
                                {
                                    row[column] = formsList.form.FormItemIList[i].options[j].option + formsList.selectable[sCount][j].ToString();
                                }
                                j++;
                            }
                        }
                    }
                    sCount++;
                }
                else//text
                {
                    textdt.Columns.Add(formsList.form.FormItemIList[i].question, typeof(string));
                    foreach (DataColumn column in textdt.Columns)
                    {
                        if (column.ColumnName == formsList.form.FormItemIList[i].question)
                        {
                            int j = 0;
                            foreach (DataRow row in textdt.Rows)
                            {
                                if (j < formsList.text[tCount].Count)
                                {
                                    row[column] = formsList.form.FormItemIList[i].options[j].option + formsList.text[tCount][j];
                                }
                                j++;
                            }
                        }
                    }
                }
            }
                //foreach (FormItemModel q in baseForm.FormItemIList)
                //{

                //    if (q.type > 1)
                //    {
                //        textdt.Columns.Add(q.question, typeof(string));
                //        for (var j = 0; j < formsList.selectable[sCount].Count; ++j)
                //        {
                //            var row = textdt.NewRow();
                //            row[q.question] = q.options[j] + " - " + formsList.selectable[sCount][j].ToString();
                //            textdt.Rows.Add(row);
                //        }
                //        sCount++;

                //    }
                //    else
                //    {
                //        textdt.Columns.Add(q.question, typeof(string));
                //        for (var j = 0; j < formsList.text[tCount].Count; ++j)
                //        {
                //            var row = textdt.NewRow();
                //            row[q.question] = formsList.text[tCount][j].ToString();
                //            textdt.Rows.Add(row);

                //        }
                //        tCount++;

                //    }
                //}

            return new DownloadFileActionResult(textdt, "Download.xls");
        }
    }
}