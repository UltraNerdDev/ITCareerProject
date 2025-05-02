using Data.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    //Business logic for Parent table, realising logical connections between the data and the UI
    public class ParentBusiness : IDisposable
    {
        private readonly SchoolRegistryContext _context;

        public ParentBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public ParentBusiness()
        {
            _context = new SchoolRegistryContext();
        }

        //Get All method returning all of the Parent objects
        public List<Parent> GetAll()
        {
            return _context.Parents.ToList();
        }

        //Get method returning a single Parent object by given ID
        public Parent Get(int id)
        {
            return _context.Parents.Find(id);
        }

        //Add method for adding new Parent object to the database
        public void Add(Parent parent)
        {
            _context.Parents.Add(parent);
            _context.SaveChanges();
        }

        //Delete method for deleting Parent object from the database by given ID
        public void Delete(int id)
        {
            var item = _context.Parents.Find(id);
            if (item != null)
            {
                _context.Parents.Remove(item);
                _context.SaveChanges();
            }
        }

        //Update method for updating existing Parent object in the database by given ID
        public void Update(Parent parent)
        {
            var item = _context.Parents.Find(parent.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(parent);
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
