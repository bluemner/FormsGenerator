using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsGeneratorWebApplication.Models
{
    public class SelectedModel
    {
        [Key]
        public int key { get; set; }

        //this is the string for the selected option that this model represents
        public virtual string selected { get; set; }

        //this is the question that this selected option belongs to
        public virtual FormItemModel question { get; set; }
    }
}