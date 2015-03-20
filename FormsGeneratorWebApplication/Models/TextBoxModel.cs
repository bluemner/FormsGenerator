using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsGeneratorWebApplication.Models
{
    public class TextBoxModel : FormItemModel
    {
      
       [Display(Name = "Answer")]
      public string value {get;set;}
       
    }
}