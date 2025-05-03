using Business;
using Data.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestss
{
    public class CustomQueriesBusinessTests : IDisposable
    {
        private readonly SchoolRegistryContext _context;
        private readonly CustomQueriesBusiness _customQueries;

        public CustomQueriesBusinessTests()
        {
            var options = new DbContextOptionsBuilder<SchoolRegistryContext>()
                .UseInMemoryDatabase(databaseName: "CustomQueriesBusinessTestDB")
                .Options;

            //creating the context
            _context = new SchoolRegistryContext(options);
            _customQueries = new CustomQueriesBusiness(_context);
            //ensuring the in-memory database is clear every time it is being deployed
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            //Seed data for testing
            var subject1 = new Subject { Name = "Mathematics" };
            var subject2 = new Subject { Name = "Physics" };
            _context.Subjects.AddRange(subject1, subject2);

            var teacher1 = new Teacher { FirstName = "Alice", LastName = "Brown", Email = "alice.brown@example.com", Phone = "123456789" };
            var teacher2 = new Teacher { FirstName = "Bob", LastName = "White", Email = "bob.white@example.com", Phone = "123456789" };
            _context.Teachers.AddRange(teacher1, teacher2);

            var classGroup1 = new ClassGroup { Name = "Class A", Year = 2025, Teacher = teacher1 };
            var classGroup2 = new ClassGroup { Name = "Class B", Year = 2026, Teacher = teacher2 };
            _context.Classes.AddRange(classGroup1, classGroup2);

            var parent1 = new Parent { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var parent2 = new Parent { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" };
            _context.Parents.AddRange(parent1, parent2);

            var student1 = new Student { FirstName = "Charlie", LastName = "Johnson", ClassGroup = classGroup1, Parent = parent1, Email = "charlie.johnson@example.com" };
            var student2 = new Student { FirstName = "Daisy", LastName = "Williams", ClassGroup = classGroup2, Parent = parent2, Email = "daisy.williams@example.com" };
            _context.Students.AddRange(student1, student2);

            var grade1 = new Grade { Value = 95.0, Date = new DateOnly(2025, 5, 1), Student = student1, Subject = subject1, Teacher = teacher1 };
            var grade2 = new Grade { Value = 88.0, Date = new DateOnly(2025, 5, 2), Student = student2, Subject = subject2, Teacher = teacher2 };
            _context.Grades.AddRange(grade1, grade2);

            _context.SaveChanges();
        }

        [Fact]
        public void GetTotalStudents_ShouldReturnCorrectCount()
        {
            // Act
            var result = _customQueries.GetTotalStudents();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetTotalSubjects_ShouldReturnCorrectCount()
        {
            // Act
            var result = _customQueries.GetTotalSubjects();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetTotalTeachers_ShouldReturnCorrectCount()
        {
            // Act
            var result = _customQueries.GetTotalTeachers();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetTotalClasses_ShouldReturnCorrectCount()
        {
            // Act
            var result = _customQueries.GetTotalClasses();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetMostPopularSubject_ShouldReturnCorrectSubject()
        {
            // Act
            var result = _customQueries.GetMostPopularSubject();

            // Assert
            Assert.Equal("Mathematics", result);
        }

        [Fact]
        public void GetTeacherWithMostClasses_ShouldReturnCorrectTeacher()
        {
            // Act
            var result = _customQueries.GetTeacherWithMostClasses();

            // Assert
            Assert.Equal("Alice Brown", result);
        }

        [Fact]
        public void GetTotalParents_ShouldReturnCorrectCount()
        {
            // Act
            var result = _customQueries.GetTotalParents();

            // Assert
            Assert.Equal(2, result);
        }

        public void Dispose()
        {
            _context.Dispose();
            _customQueries.Dispose();
        }
    }
}
