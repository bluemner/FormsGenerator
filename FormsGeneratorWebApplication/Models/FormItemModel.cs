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

        public String answer { get; set; }

        [Display(Name = "options")]
        public virtual IList<OptionsModel> options { get; set; }

        public virtual IList<SelectedModel> selected { get; set; }

        public int selectedOption { get; set; }
        
        //foreign key for the database
        public virtual FormsModel FormsModel { get; set; }

        public static FormItemModel clone(FormItemModel toClone)
        {
            FormItemModel result = new FormItemModel();
            result.postion = toClone.postion;
            result.question = toClone.question;
            result.type = toClone.type;
    
            if (toClone.options != null)
            {
                result.options = new List<OptionsModel>();
                foreach (OptionsModel o in toClone.options)
                {
                    result.options.Add(new OptionsModel { option = o.option, question = result });
                }
            }
            result.selected = new List<SelectedModel>();
            if (toClone.selected != null)
            {
                foreach (SelectedModel s in toClone.selected)
                {
                    result.selected.Add(new SelectedModel { selected = s.selected, question = result});
                }
            }
            result.selectedOption = toClone.selectedOption;
            return result;
        }
    }

    
}
