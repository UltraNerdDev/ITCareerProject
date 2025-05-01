using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class ClassGroupBusiness
    {
        private SchoolRegistryContext _context;
      
        public ClassGroupBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public ClassGroupBusiness()
        {

        }

        public List<ClassGroup> GetAll()
        {
            using (_context = new SchoolRegistryContext())
            {
                return _context.Classes
                     .Include(g => g.Teacher)
                     .ToList();
                // return context.Classes.ToList();
            }
        }

        public ClassGroup Get(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                return _context.Classes.Find(id);
            }
        }

        public void Add(ClassGroup classGroup)
        {
            using (_context = new SchoolRegistryContext())
            {
                _context.Classes.Add(classGroup);
                _context.SaveChanges();
            }
        }

        public void Update(ClassGroup classGroup)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Classes.Find(classGroup.Id);
                if (item != null)
                {
                    _context.Entry(item).CurrentValues.SetValues(classGroup);
                    _context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Classes.Find(id);
                if (item != null)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
            }
        }
    }
}
