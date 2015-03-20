using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FormsGeneratorWebApplication.Models
{
    public class FormItemModel
    {

        public int postion { get; set; }
        public string question { get; set; }
        public int type { get; set; }

    }


    public class Question
    {
        public int ID { get; set; }
        public string QuestionType { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string[] Options { get; set; }
    }

    public class QuestionDBContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
    }
}