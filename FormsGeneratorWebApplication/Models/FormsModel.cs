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

        public static FormsModel clone(FormsModel toClone)
        {
            FormsModel result = new FormsModel();
            result.FormItemIList = new List<FormItemModel>();
            if (toClone.FormItemIList != null)
            {
                foreach (FormItemModel i in toClone.FormItemIList)
                {
                    result.FormItemIList.Add(FormItemModel.clone(i));
                }
            }
            result.Name = toClone.Name;
            result.StartDate = toClone.StartDate;
            result.EndDate = toClone.EndDate;
            result.Status = toClone.Status;
            return result;
        }

    }
   
}