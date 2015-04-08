using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsGeneratorWebApplication.Models
{
    public class ResultModel
    {
        [Key]
        public int key { get; set; }

        //This is the GUID of the base form that the admin made
        public Guid adminGUID { get; set; }

        //This is the GUID of the form that the user will fill out
        public Guid userGUID { get; set; }

        //this determines if the user has submitted the form
        public bool active { get; set; }
    }
}