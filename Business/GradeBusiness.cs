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
        private SchoolRegistryContext _context;

        public GradeBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public GradeBusiness()
        {
            
        }

        public List<Grade> GetAll()
        {
            using (_context = new SchoolRegistryContext())
            {                
                //return context.Grades.ToList();
                return _context.Grades
                     .Include(g => g.Student)
                     .Include(g => g.Subject)
                     .Include(g => g.Teacher)
                     .ToList();
            }
        }

        public Grade Get(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                // Example: fetching a single Enrollment with related entities loaded
                var enrollment = _context.Grades
                                        .Include(e => e.Student)
                                        .Include(e => e.Subject)
                                        .Include(e => e.Teacher)
                                        .FirstOrDefault(e => e.Id == id);
                return _context.Grades.Find(id);
            }
        }

        public void Add(Grade grade)
        {
            using (_context = new SchoolRegistryContext())
            {
                _context.Grades.Add(grade);
                _context.SaveChanges();
            }
        }

        public void Update(Grade grade)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Grades.Find(grade.Id);
                if (item != null)
                {
                    _context.Entry(item).CurrentValues.SetValues(grade);
                    _context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Grades.Find(id);
                if (item != null)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
            }
        }
    }
}
