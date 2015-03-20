using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsGeneratorWebApplication.Models
{
    public class TextAreaModel : FormItemModel
    {
        [Display(Name = "Answer")]
        public string value { get; set; }

        public int type { get; set; }
    }
}