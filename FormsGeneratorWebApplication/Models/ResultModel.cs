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

        //This is the email of the user who is to fill out the form refered to by userGUID.
        //DO NOT expose any user of the application
        public String email { get; set; }

        //this determines if the user has submitted the form
        public bool active { get; set; }
    }
}