using Data.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ParentBusiness
    {
        private SchoolRegistryContext _context;

        public ParentBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public ParentBusiness()
        {
            
        }

        public List<Parent> GetAll()
        {
            using (_context = new SchoolRegistryContext())
            {
                return _context.Parents.ToList();
            }
        }

        public Parent Get(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                return _context.Parents.Find(id);
            }
        }

        public void Add(Parent parent)
        {
            using (_context = new SchoolRegistryContext())
            {
                _context.Parents.Add(parent);
                _context.SaveChanges();
            }
        }

        public void Update(Parent parent)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Parents.Find(parent.Id);
                if (item != null)
                {
                    _context.Entry(item).CurrentValues.SetValues(parent);
                    _context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (_context = new SchoolRegistryContext())
            {
                var item = _context.Parents.Find(id);
                if (item != null)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
            }
        }
    }
}
