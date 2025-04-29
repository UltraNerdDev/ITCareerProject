using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class SubjectBusiness
    {
        private SchoolRegistryContext context;

        public List<Subject> GetAll()
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Subjects.ToList();
            }
        }

        public Subject Get(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Subjects.Find(id);
            }
        }

        public void Add(Subject subject)
        {
            using (context = new SchoolRegistryContext())
            {
                context.Subjects.Add(subject);
                context.SaveChanges();
            }
        }

        public void Update(Subject subject)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Subjects.Find(subject.Id);
                if (item != null)
                {
                    context.Entry(item).CurrentValues.SetValues(subject);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Subjects.Find(id);
                if (item != null)
                {
                    context.Remove(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
