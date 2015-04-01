using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormsGeneratorWebApplication.Models
{
    public class FormItemModel
    {
        [Key]
        public int id { get; set; }

        //[Display(Name="Position")]
        //[Required(ErrorMessage="No postion set")]
        public int postion { get; set; }

        //[Display(Name = "Question")]
        //[Required(ErrorMessage = "Question is required")]
        public string question { get; set; }

        //[Display(Name = "Type")]
        //[Required(ErrorMessage = "Type is required")]
        public int type { get; set; }

        public virtual FormsModel FormsModel { get; set; }

    }

    
}
