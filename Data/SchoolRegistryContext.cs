using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    // DbContext class for the SchoolRegistry database, realising the DbSet properties for each table
    public class SchoolRegistryContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ClassGroup> Classes { get; set; }
        public DbSet<Parent> Parents { get; set; }

        //Constructor with parameter when in-memory-database is needed
        public SchoolRegistryContext(DbContextOptions<SchoolRegistryContext> options)
    : base(options)
        {

        }

        //Constructor without parameters when SQL database is needed
        public SchoolRegistryContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolRegistryDB;Integrated Security = true;TrustServerCertificate=true");
            //DESKTOP-AVBG2ST\SQLEXPRESS - Ves database
            //Gummie\SQLEXPRESS - Alex database
        }

        //OnModelCreating method is not required here, because of the anotations in the models
    }
}
