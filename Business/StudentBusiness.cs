using Data.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class StudentBusiness
    {
        private SchoolRegistryContext context;

        public List<Student> GetAll()
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Students.ToList();
            }
        }

        public Student Get(int id)
        {
            using (context = new SchoolRegistryContext())
            {
                return context.Students.Find(id);
            }
        }

        public void Add(Student student)
        {
            using (context = new SchoolRegistryContext())
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        public void Update(Student student)
        {
            using (context = new SchoolRegistryContext())
            {
                var item = context.Students.Find(student.Id);
                if (item != null)
                {
                    context.Entry(item).CurrentValues.SetValues(student);
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
