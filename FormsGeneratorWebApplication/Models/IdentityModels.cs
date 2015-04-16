using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace FormsGeneratorWebApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public virtual IList<UserForm> forms { get; set; }
    }

    public class UserForm
    {
        [Key]
        public int key { get; set; }
        public Guid formGUID { get; set; }
        public virtual ApplicationUser user { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MyDbContextConnectionString2")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<UserForm>()
    //.ToTable("dbo.UserForm");
        }

        public DbSet<UserForm> UserForms { get; set; }
    }

    
}