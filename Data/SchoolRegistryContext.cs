using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SchoolRegistryContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ClassGroup> Classes { get; set; }
        public DbSet<Parent> Parents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-AVBG2ST\SQLEXPRESS;Database=SchoolRegistryDB;Integrated Security = true;TrustServerCertificate=true");
            //DESKTOP-AVBG2ST\SQLEXPRESS - Ves database  \\  Gummie\SQLEXPRESS - Alex database
            //zlaten si vesko <3, twa trqbwa da go dobawim nqkak si w git_ignore papkata ama ne q vijdam
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Making sure that all of the properties are required or optional.
            modelBuilder.Entity<Grade>()
                .Property(n => n.Id)
                .IsRequired(true);
        }
    }
}
