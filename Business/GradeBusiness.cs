using Data.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class GradeBusiness
    {
        private SchoolRegistryContext context;

        public List<Grade> GetAll()
        {
            using (context = new SchoolRegistryContext())
            {                
                //return context.Grades.ToList();
                return context.Grades
                     .Include(g => g.Student)
                     .Include(g => g.Subject)
                     .Include(g => g.Teacher)
                     .ToList();
            }
        }

        public Grade Get(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                // Example: fetching a single Enrollment with related entities loaded
                var enrollment = context.Grades
                                        .Include(e => e.Student)
                                        .Include(e => e.Subject)
                                        .Include(e => e.Teacher)
                                        .FirstOrDefault(e => e.Id == id);
                return context.Grades.Find(id);
            }
        }

        public void Add(Grade grade)
        {
            using (context = new SchoolRegistryContext())
            {
                context.Grades.Add(grade);
                context.SaveChanges();
            }
        }

        public void Update(Grade grade)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Grades.Find(grade.Id);
                if (item != null)
                {
                    context.Entry(item).CurrentValues.SetValues(grade);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Grades.Find(id);
                if (item != null)
                {
                    context.Remove(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
