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
        private SchoolRegistryContext _context;

        public StudentBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public StudentBusiness()
        {
            
        }

        public List<Student> GetAll()
        {
            using (_context = new SchoolRegistryContext())
            {
                return _context.Students.ToList();
            }
        }

        public Student Get(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                return _context.Students.Find(id);
            }
        }

        public void Add(Student student)
        {
            using (_context = new SchoolRegistryContext())
            {
                _context.Students.Add(student);
                _context.SaveChanges();
            }
        }

        public void Update(Student student)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Students.Find(student.Id);
                if (item != null)
                {
                    _context.Entry(item).CurrentValues.SetValues(student);
                    _context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Students.Find(id);
                if (item != null)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
            }
        }
    }
}
