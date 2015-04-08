using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FormsGeneratorWebApplication.Models
{
    public class CheckBoxesModel : FormItemModel 
    {

        public IList<checkbox> checkboxes { get; set; }

        public int checkedOption { get; set; }
        [Display(Name = "Minimum Selection")]
        public int minCheckable { get; set; }
        [Display(Name = "Maximum Selection")]
        public int maxCheckable { get; set; }

    }

    public struct checkbox {

       [Display(Name = "Checked")]
       public bool isChecked;
       [Display(Name = "Answer")]
       public string answer;

    }

 }