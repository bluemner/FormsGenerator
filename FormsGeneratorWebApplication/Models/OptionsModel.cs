using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsGeneratorWebApplication.Models
{
    public class OptionsModel
    {
        [Key]
        public int key { get; set; }

        //this is the option string that populates the options list for form items
        public string option { get; set; }

        //this is a foreign key that points to the form item
        public virtual FormItemModel question { get; set; }

    }
}