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
    //Business logic for Grade table, realising logical connections between the data and the UI
    public class GradeBusiness : IDisposable
    {
        private readonly SchoolRegistryContext _context;

        public GradeBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public GradeBusiness()
        {
            _context = new SchoolRegistryContext();
        }

        //Commented code does not work for the ToString() method

        //Get All method returning all of the Grade objects
        public List<Grade> GetAll()
        {
            return _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .ToList();
        }

        //Get method returning a single Grade object by given ID
        public Grade Get(int id)
        {
            //return _context.Grades
            //    .Include(g => g.Student)
            //    .Include(g => g.Subject)
            //    .FirstOrDefault(g => g.Id == id);
            return _context.Grades.Find(id);
        }

        //Add method for adding new Grade object to the database
        public void Add(Grade grade)
        {
            _context.Grades.Add(grade);
            _context.SaveChanges();
        }

        //Delete method for deleting existing Grade object in the database by given ID
        public void Delete(int id)
        {
            //var item = _context.Grades
            //    .Include(g => g.Student)
            //    .Include(g => g.Subject)
            //    .FirstOrDefault(g => g.Id == id);
            var item = _context.Grades.Find(id);
            if (item != null)
            {
                _context.Grades.Remove(item);
                _context.SaveChanges();
            }
        }

        //Update method for updating existing Grade object in the database by given ID
        public void Update(Grade grade)
        {
            var item = _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .FirstOrDefault(g => g.Id == grade.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(grade);
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
