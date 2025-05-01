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
        private SchoolRegistryContext _context;

        public SubjectBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public SubjectBusiness()
        {
            
        }

        public List<Subject> GetAll()
        {
            using (_context = new SchoolRegistryContext())
            {
                return _context.Subjects.ToList();
            }
        }

        public Subject Get(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                return _context.Subjects.Find(id);
            }
        }

        public void Add(Subject subject)
        {
            using (_context = new SchoolRegistryContext())
            {
                _context.Subjects.Add(subject);
                _context.SaveChanges();
            }
        }

        public void Update(Subject subject)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Subjects.Find(subject.Id);
                if (item != null)
                {
                    _context.Entry(item).CurrentValues.SetValues(subject);
                    _context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Subjects.Find(id);
                if (item != null)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
            }
        }
    }
}
