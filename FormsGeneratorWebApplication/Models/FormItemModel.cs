using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace FormsGeneratorWebApplication.Models
{
    public class FormItemModel
    {
        [Display(Name="Position")]
        [Required(ErrorMessage="No postion set")]
        public int postion { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "Question is required")]
        public string question { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is required")]
        public int type { get; set; }
    }

}
