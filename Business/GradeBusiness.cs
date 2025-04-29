using Data.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class GradeBusiness
    {
        private SchoolRegistryContext context;

        public List<Grade> GetAll()
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Grades.ToList();
            }
        }

        public Grade Get(int id)
        {
            using (context = new SchoolRegistryContext())
            {
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
                    context.Entry(item).CurrentValues.SetValues( grade);
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
