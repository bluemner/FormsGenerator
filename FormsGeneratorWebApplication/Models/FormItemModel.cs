﻿using System;
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
        public int type { get; set; }

    }

    //What is the point of this? this is  parent class explain
    public class Question
    {
        public int ID { get; set; }
        public string QuestionText { get; set; }
        public string[] Options { get; set; }
        
    }

    public class QuestionDBContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
    }
}
