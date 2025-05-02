using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    //Business logic for Subject table, realising logical connections between the data and the UI
    public class SubjectBusiness : IDisposable
    {
        private readonly SchoolRegistryContext _context;

        public SubjectBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }
        public SubjectBusiness()
        {
            _context = new SchoolRegistryContext();
        }

        //Get All method returning all of the Subject objects
        public List<Subject> GetAll()
        {
            return _context.Subjects.ToList();
        }

        //Get method returning a single Subject object by given ID
        public Subject Get(int id)
        {
            return _context.Subjects.Find(id);
        }

        //Add method for adding new Subject object to the database
        public void Add(Subject subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();
        }

        //Delete method for deleting Subject object from the database by given ID
        public void Delete(int id)
        {
            var item = _context.Subjects.Find(id);
            if (item != null)
            {
                _context.Subjects.Remove(item);
                _context.SaveChanges();
            }
        }

        //Update method for updating existing Subject object in the database by given ID
        public void Update(Subject subject)
        {
            var item = _context.Subjects.Find(subject.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(subject);
                _context.SaveChanges();
            }
        }

        //Dispose method for closing the context
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
