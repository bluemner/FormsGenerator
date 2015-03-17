using System;
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

        IList<FormItem> FormItemIList { get; set; }
        
        [Display(Name = "Form Name")]
        [Required(ErrorMessage="Please enter a name")]
        string Name { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Please a start date")]
        DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Please a end date")]
        DateTime EndDate { get; set; }

        

    }
}