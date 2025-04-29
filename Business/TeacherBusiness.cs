using Data.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TeacherBusiness
    {
        private SchoolRegistryContext context;

        public List<Teacher> GetAll()
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Teachers.ToList();
            }
        }

        public Teacher Get(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Teachers.Find(id);
            }
        }

        public void Add(Teacher teacher)
        {
            using (context = new SchoolRegistryContext())
            {
                context.Teachers.Add(teacher);
                context.SaveChanges();
            }
        }

        public void Update(Teacher teacher)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Teachers.Find(teacher.Id);
                if (item != null)
                {
                    context.Entry(item).CurrentValues.SetValues(teacher);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Teachers.Find(id);
                if (item != null)
                {
                    context.Remove(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
