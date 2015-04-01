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
        [Key]
        public int key { get; set; }

        //[Required(ErrorMessageResourceName = null)]
        public virtual IList<FormItemModel> FormItemIList { get; set; }

        [Display(Name = "Form Name")]
        //[Required(ErrorMessageResourceName = "EnterName", ErrorMessageResourceType = typeof(Resources.FormsResource))]
        public string Name { get; set; }

        [Display(Name = "Start Date")]
        //[Required(ErrorMessageResourceName = "EnterStartDate", ErrorMessageResourceType = typeof(Resources.FormsResource))]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        //[Required(ErrorMessageResourceName = "EnterEndDate", ErrorMessageResourceType = typeof(Resources.FormsResource))]
        public DateTime EndDate { get; set; }
        
    }
   
}