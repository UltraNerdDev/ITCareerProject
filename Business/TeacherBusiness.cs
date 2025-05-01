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
    public class TeacherBusiness
    {
        private SchoolRegistryContext _context;

        public TeacherBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public TeacherBusiness()
        {

        }

        public List<Teacher> GetAll()
        {
            using (_context = new SchoolRegistryContext())
            {
                return _context.Teachers                    
                    .Include(t => t.Subject)
                    .ToList();
              //  return context.Teachers.ToList();
            }
        }

        public Teacher Get(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                var enrollment = _context.Teachers
                                        .Include(e => e.Subject)
                                        .FirstOrDefault(e => e.Id == id);
                return _context.Teachers.Find(id);
            }
        }

        public void Add(Teacher teacher)
        {
            using (_context = new SchoolRegistryContext())
            {
                _context.Teachers.Add(teacher);
                _context.SaveChanges();
            }
        }

        public void Update(Teacher teacher)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Teachers.Find(teacher.Id);
                if (item != null)
                {
                    _context.Entry(item).CurrentValues.SetValues(teacher);
                    _context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Teachers.Find(id);
                if (item != null)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
            }
        }
    }
}
