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
    //Business logic for Student table, realising logical connections between the data and the UI
    public class StudentBusiness : IDisposable
    {
        private readonly SchoolRegistryContext _context;

        public StudentBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public StudentBusiness()
        {
            _context = new SchoolRegistryContext();
        }

        //Get All method returning all of the Student objects
        public List<Student> GetAll()
        {
            return _context.Students
                .Include(s => s.ClassGroup)
                .Include(s => s.Parent)
                .Include(s => s.Grades)
                .ToList();
        }

        //Get method returning a single Student object by given ID
        public Student Get(int id)
        {
            return _context.Students
                .Include(s => s.ClassGroup)
                .Include(s => s.Parent)
                .Include(s => s.Grades)
                .FirstOrDefault(s => s.Id == id);
        }

        //Add method for adding new Student object to the database
        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        //Delete method for deleting Student object from the database by given ID
        public void Delete(int id)
        {
            var item = _context.Students
                .Include(s => s.ClassGroup)
                .Include(s => s.Parent)
                .Include(s => s.Grades)
                .FirstOrDefault(s => s.Id == id);
            if (item != null)
            {
                _context.Students.Remove(item);
                _context.SaveChanges();
            }
        }

        //Update method for updating existing Student object in the database by given ID
        public void Update(Student student)
        {
            var item = _context.Students
                .Include(s => s.ClassGroup)
                .Include(s => s.Parent)
                .Include(s => s.Grades)
                .FirstOrDefault(s => s.Id == student.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(student);
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
