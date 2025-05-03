using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    //Business logic for custom queries, realising logical connections between the data and the UI
    public class CustomQueriesBusiness : IDisposable
    {
        private readonly SchoolRegistryContext _context;

        public CustomQueriesBusiness(SchoolRegistryContext context)
        {
            _context = context;
        }

        public CustomQueriesBusiness()
        {
            _context = new SchoolRegistryContext();
        }

        //Query 1: Get the total number of students
        public int GetTotalStudents()
        {
            return _context.Students.Count();
        }

        //Query 2: Get the total number of teachers
        public int GetTotalTeachers()
        {
            return _context.Teachers.Count();
        }

        //Query 3: Get the total number of classes
        public int GetTotalClasses()
        {
            return _context.Classes.Count();
        }

        //Query 4: Get the most popular subject (based on the number of students enrolled)
        public string GetMostPopularSubject()
        {
            var subject = _context.Grades
                .Include(g => g.Subject)
                .GroupBy(g => g.Subject)
                .OrderByDescending(g => g.Select(grade => grade.StudentId).Distinct().Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            return subject?.Name ?? "None";
        }

        //Query 5: Get the teacher with the most classes
        public string GetTeacherWithMostClasses()
        {
            var teacher = _context.Teachers
                .Include(t => t.Classes)
                .OrderByDescending(t => t.Classes.Count)
                .FirstOrDefault();

            return teacher != null ? $"{teacher.FirstName} {teacher.LastName}" : "None";
        }


        //Query 6: Get the total number of parents
        public int GetTotalParents()
        {
            return _context.Parents.Count();
        }

        //Query 7: Get the total number of subjects
        public int GetTotalSubjects()
        {
            return _context.Subjects.Count();
        }

        //Dispose method for closing the context
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
