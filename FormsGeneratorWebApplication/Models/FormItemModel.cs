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
        public int postion { get; set; }
        public string question { get; set; }
    }


    public class Question
    {
        public int ID { get; set; }
        public string QuestionText { get; set; }
        [Required(ErrorMessage = "Please provide a Question.", AllowEmptyStrings=false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Text)]
        public string[] Options { get; set; }
        
    }

    public class QuestionDBContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
    }
}