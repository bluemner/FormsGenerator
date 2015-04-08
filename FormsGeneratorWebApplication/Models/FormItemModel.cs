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

        [Display(Name = "Position")]
        //[Required(ErrorMessageResourceName = "NoPosition", ErrorMessageResourceType = typeof(Resources.FormsResource))]
        public int postion { get; set; }

        [Display(Name = "Question")]
        //[Required(ErrorMessageResourceName = "QuestionRequired", ErrorMessageResourceType = typeof(Resources.FormsResource))]
        public string question { get; set; }

        [Display(Name = "Type")]
        //[Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(Resources.FormsResource))]
        public int type { get; set; }

        [Display(Name = "options")]
        public virtual IList<OptionsModel> options { get; set; }

        public virtual IList<string> selected { get; set; }

        public int selectedOption { get; set; }
        /// <summary>
        /// TODO: Add description 
        /// </summary>
        public virtual FormsModel FormsModel { get; set; }

    }

    
}
