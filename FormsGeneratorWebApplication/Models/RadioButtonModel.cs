using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsGeneratorWebApplication.Models
{
    public class RadioButtonModel : FormItemModel
    {

        public IList<string> options { get; set; }
        public int selectedOption { get; set; }
    }
}