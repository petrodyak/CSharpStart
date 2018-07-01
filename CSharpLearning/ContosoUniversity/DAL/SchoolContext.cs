using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.Design;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("SchoolContext") {
            this.Configuration.LazyLoadingEnabled = false;
        }
        /*This code creates a DbSet property for each entity set. 
         * In Entity Framework terminology, an entity set typically 
         * corresponds to a database table, and an entity corresponds to a row in the table.*/
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        //The modelBuilder.Conventions.Remove statement in the OnModelCreating method prevents table names from being pluralized.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}