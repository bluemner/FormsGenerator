﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormsGeneratorWebApplication.Models
{
    /// <summary>
    ///  Used To Create An Instance Of a Form
    /// </summary>
    public class FormsModel
    {
        [Key]
        public int key { get; set; }

    
        public virtual IList<FormItemModel> FormItemIList { get; set; }

        public virtual ApplicationUser user { get; set; }
        
        [Display(Name = "Form Name")]
        [Required(ErrorMessage="Please enter a name")]
        public string Name { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Please a start date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Please a end date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }

        //this is the GUID of the form
        public Guid adminGUID { get; set; }

    }
   
}