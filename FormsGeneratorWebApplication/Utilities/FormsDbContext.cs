using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FormsGeneratorWebApplication.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FormsGeneratorWebApplication.Utilities
{
    //http://www.ryadel.com/2014/10/20/asp-net-setup-mvc5-website-mysql-entity-framework-6-code-first-vs2013/
    public class FormsDbContext : IdentityDbContext
    {
        public FormsDbContext() : base("MyDbContextConnectionString")//Needs proper connection string from web.config
        {
            Database.SetInitializer<FormsDbContext>(new MyDbInitializer());
        }
        
        public DbSet<FormsModel> FormModels { get; set; }
        public DbSet<FormItemModel> FormItemModels { get; set; }
        public DbSet<OptionsModel> OptionsModels { get; set; }
        public DbSet<SelectedModel> SelectedModels { get; set; }
        public DbSet<ResultModel> ResultModels { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class MyDbInitializer : System.Data.Entity.CreateDatabaseIfNotExists<FormsDbContext>//Fill this with mock data
    {
        protected override void Seed(FormsDbContext context)
        {
            // create mocked data here
            //context.Forms.Add(new FormsModel {...});
            
            base.Seed(context);
        }
    }
}