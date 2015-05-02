using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsGeneratorWebApplication.Models
{
    public class AdminFormModel
    {


        [Key]
        public int key { get; set; }

        public IList<FormsModel> forms { get; set; }

        public IList<int> total { get; set; }

        public IList<int> completed { get; set; }
        //comment!
    }
}