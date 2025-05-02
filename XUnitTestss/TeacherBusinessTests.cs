using Business;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace XUnitTestss
{
    public class TeacherBusinessTests
    {
        public SchoolRegistryContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SchoolRegistryContext>()
                .UseInMemoryDatabase(databaseName: "TeacherBusinessTestDB")
                .Options;

            return new SchoolRegistryContext(options);
        }

        [Fact]
        public void AddTeacher_ShouldAddTeacherToDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var teacherBusiness = new TeacherBusiness(context);
            var teacher = new Teacher
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123456789"
            };

            // Act
            teacherBusiness.Add(teacher);

            // Assert
            var result = teacherBusiness.Get(teacher.Id);
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            teacherBusiness.Delete(teacher.Id); // Clean up after test
        }

        [Fact]
        public void GetAllTeachers_ShouldReturnAllTeachers()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var teacherBusiness = new TeacherBusiness(context);
            //context.Teachers.AddRange(
            //    new Teacher { FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com" },
            //    new Teacher { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com" }
            //);
            var teacher1= new Teacher { FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com",Phone = "23626" };
            var teacher2 = new Teacher { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", Phone = "327464235"        };
            teacherBusiness.Add(teacher1);
            teacherBusiness.Add(teacher2);
            context.SaveChanges();

            // Act
            var teachers = teacherBusiness.GetAll();

            // Assert
            //Assert.Equal(2, teachers.Count);
            Assert.Contains(teachers, t => t.FirstName == "Alice" && t.LastName == "Smith");
            Assert.Contains(teachers, t => t.FirstName == "Bob" && t.LastName == "Brown");
            teacherBusiness.Delete(teacher1.Id); // Clean up after test
            teacherBusiness.Delete(teacher2.Id); // Clean up after test
        }

        [Fact]
        public void GetTeacherById_ShouldReturnCorrectTeacher()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var teacherBusiness = new TeacherBusiness(context);
            var teacher = new Teacher { FirstName = "Charlie", LastName = "Johnson", Email = "charlie.johnson@example.com" , Phone = "83259832521" };
            teacherBusiness.Add(teacher);
            context.SaveChanges();

            // Act
            var result = teacherBusiness.Get(teacher.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Charlie", result.FirstName);
            Assert.Equal("Johnson", result.LastName);
            teacherBusiness.Delete(teacher.Id); // Clean up after test
        }

        [Fact]
        public void UpdateTeacher_ShouldModifyTeacherInDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var teacherBusiness = new TeacherBusiness(context);
            var teacher = new Teacher { FirstName = "David", LastName = "Williams", Email = "david.williams@example.com",Phone= "83259832521" };
            teacherBusiness.Add(teacher);
            context.SaveChanges();

            // Act
            teacher.FirstName = "Dave";
            teacherBusiness.Update(teacher);

            // Assert
            var result = teacherBusiness.Get(teacher.Id);
            Assert.NotNull(result);
            Assert.Equal("Dave", result.FirstName);
            teacherBusiness.Delete(teacher.Id); // Clean up after test
        }

        [Fact]
        public void DeleteTeacher_ShouldRemoveTeacherFromDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var teacherBusiness = new TeacherBusiness(context);
            var teacher = new Teacher { FirstName = "Eve", LastName = "Taylor", Email = "eve.taylor@example.com",Phone = "83259832521" };
            teacherBusiness.Add(teacher);
            context.SaveChanges();

            // Act
            teacherBusiness.Delete(teacher.Id);

            // Assert
            var result = context.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
            Assert.Null(result);
        }
    }
}