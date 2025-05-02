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
    //Business logic for Teacher table, realising logical connections between the data and the UI
    public class TeacherBusiness : IDisposable
    {
        private readonly SchoolRegistryContext _context;

        public TeacherBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }
        public TeacherBusiness()
        {
            _context = new SchoolRegistryContext();
        }

        //Get All method returning all of the Teacher objects
        public List<Teacher> GetAll()
        {
            return _context.Teachers.ToList();
        }

        //Get method returning a single Teacher object by given ID
        public Teacher Get(int id)
        {
            return _context.Teachers.Find(id);
        }

        //Add method for adding new Teacher object to the database
        public void Add(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }

        //Delete method for deleting Teacher object from the database by given ID
        public void Delete(int id)
        {
            var item = _context.Teachers.Find(id);
            if (item != null)
            {
                _context.Teachers.Remove(item);
                _context.SaveChanges();
            }
        }

        //Update method for updating existing Teacher object in the database by given ID
        public void Update(Teacher teacher)
        {
            var item = _context.Teachers.Find(teacher.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(teacher);
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
