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

}
