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
    //Business logic for ClassGroup table, realising logical connections between the data and the UI
    public class ClassGroupBusiness : IDisposable
    {
        private readonly SchoolRegistryContext _context;

        public ClassGroupBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public ClassGroupBusiness()
        {
            _context = new SchoolRegistryContext();
        }

        //Get All method returning all of the ClassGroup objects
        public List<ClassGroup> GetAll()
        {
            return _context.Classes.ToList();
        }

        //Get method returning a single ClassGroup object by given ID
        public ClassGroup Get(int id)
        {
            return _context.Classes.Find(id);
        }

        //Add method for adding new ClassGroup object to the database
        public void Add(ClassGroup classGroup)
        {
            _context.Classes.Add(classGroup);
            _context.SaveChanges();
        }

        //Delete method for deleting existing ClassGroup object in the database by given ID
        public void Delete(int id)
        {
            var item = _context.Classes.Find(id);
            if (item != null)
            {
                _context.Classes.Remove(item);
                _context.SaveChanges();
            }
        }

        //Update method for updating existing ClassGroup object in the database by given ID
        public void Update(ClassGroup classGroup)
        {
            var item = _context.Classes.Find(classGroup.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(classGroup);
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
