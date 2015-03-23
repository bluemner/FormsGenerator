using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FormsGeneratorWebApplication.Models;

namespace FormsGeneratorWebApplication.Utilities
{
    //http://www.ryadel.com/2014/10/20/asp-net-setup-mvc5-website-mysql-entity-framework-6-code-first-vs2013/
    public class FormsDbContext : DbContext
    {
        public FormsDbContext() : base("MyDbContextConnectionString")//Needs proper connection string from web.config
        {
            Database.SetInitializer<FormsDbContext>(new MyDbInitializer());
        }
        
        public DbSet<FormsModel> FormModels { get; set; }
        public DbSet<CheckBoxesModel> CheckBoxModels { get; set; }
        public DbSet<TextAreaModel> TextAreaModels { get; set; }
        public DbSet<TextBoxModel> TextBoxModels { get; set; }
        public DbSet<RadioButtonModel> RadioButtonModels { get; set; }
        //public DbSet<ResultModel> ResultModels { get; set; }

 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class MyDbInitializer : DropCreateDatabaseAlways<FormsDbContext>//Fill this with mock data
    {
        protected override void Seed(FormsDbContext context)
        {
            // create mocked data here
            //context.Forms.Add(new FormsModel {...});
            
            base.Seed(context);
        }
    }
}