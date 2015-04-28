using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormsGeneratorWebApplication.Models
{
    /// <summary>
    /// used to pass completed data to analytic view
    /// </summary>
    public class FormsListModel
    {
        //form to grab questions and response from
        public FormsModel form { get; set; }

        public IList<IList<int>> selectable { get; set; }

        public IList<IList<String>> text { get; set; }
    }
}