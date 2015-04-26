using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormsGeneratorWebApplication.Models
{
    /// <summary>
    /// used to pass completed forms to analytic view
    /// </summary>
    public class FormsListModel
    {
        //used to pass completed forms to analytic view
        public IList<FormsModel> listOfForms { get; set; }
    }
}