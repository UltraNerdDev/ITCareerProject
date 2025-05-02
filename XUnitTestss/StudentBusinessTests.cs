using Business;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace XUnitTestss
{
    public class StudentBusinessTests
    {
        private SchoolRegistryContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SchoolRegistryContext>()
                .UseInMemoryDatabase(databaseName: "StudentBusinessTestDB")
                .Options;

            return new SchoolRegistryContext(options);
        }

        [Fact]
        public void AddStudent_ShouldAddStudentToDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var studentBusiness = new StudentBusiness();
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Age = 20,
                ClassGroupId = 1,
                ParentId = 7
            };

            // Act
            studentBusiness.Add(student);

            // Assert
            var result = studentBusiness.Get(student.Id);
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            studentBusiness.Delete(student.Id);
        }

        [Fact]
        public void GetAllStudents_ShouldReturnAllStudents()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var studentBusiness = new StudentBusiness();
            //context.Students.AddRange(
            //    new Student { FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com" },
            //    new Student { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com" }
            //);
            var student = new Student { FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com", ParentId = 7, ClassGroupId = 1 };
            var student2 = new Student { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", ParentId = 7, ClassGroupId = 1 };
            studentBusiness.Add(student);
            studentBusiness.Add(student2);
            context.SaveChanges();

            // Act
            var students = studentBusiness.GetAll();

            // Assert
            //Assert.Equal(2, students.Count);
            Assert.Contains(students, s => s.FirstName == "Alice" && s.LastName == "Smith");
            Assert.Contains(students, s => s.FirstName == "Bob" && s.LastName == "Brown");
            studentBusiness.Delete(student.Id);
            studentBusiness.Delete(student2.Id);
        }

        [Fact]
        public void GetStudentById_ShouldReturnCorrectStudent()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var studentBusiness = new StudentBusiness();
            var student = new Student { FirstName = "Charlie", LastName = "Johnson", Email = "charlie.johnson@example.com",ParentId=7,ClassGroupId=1 };
            studentBusiness.Add(student);
            context.SaveChanges();

            // Act
            var result = studentBusiness.Get(student.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Charlie", result.FirstName);
            Assert.Equal("Johnson", result.LastName);
            studentBusiness.Delete(student.Id);
        }

        [Fact]
        public void UpdateStudent_ShouldModifyStudentInDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var studentBusiness = new StudentBusiness();
            var student = new Student { FirstName = "David", LastName = "Williams", Email = "david.williams@example.com" };
            context.Students.Add(student);
            context.SaveChanges();

            // Act
            student.FirstName = "Dave";
            studentBusiness.Update(student);

            // Assert
            var result = context.Students.FirstOrDefault(s => s.Id == student.Id);
            Assert.NotNull(result);
            Assert.Equal("Dave", result.FirstName);
        }

        [Fact]
        public void DeleteStudent_ShouldRemoveStudentFromDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var studentBusiness = new StudentBusiness();
            var student = new Student { FirstName = "Eve", LastName = "Taylor", Email = "eve.taylor@example.com",ClassGroupId=1,ParentId=7 };
            studentBusiness.Add(student);
            context.SaveChanges();

            // Act
            studentBusiness.Delete(student.Id);

            // Assert
            var result = context.Students.FirstOrDefault(s => s.Id == student.Id);
            Assert.Null(result);
        }
    }
}