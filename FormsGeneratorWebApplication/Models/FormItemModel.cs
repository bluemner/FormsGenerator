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

        public int ID { get; set; }
        public int Postion { get; set; }
        public string Question { get; set; }
        public int Type { get; set; }
        public string[] Options { get; set; }

    }

}
