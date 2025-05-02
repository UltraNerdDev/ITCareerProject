using Business;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace XUnitTestss
{
    public class ClassGroupBusinessTests
    {
        private SchoolRegistryContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SchoolRegistryContext>()
                .UseInMemoryDatabase(databaseName: "SomeUniqueTestDbName")
                .Options;

            //return new SchoolRegistryContext(options);
            var context = new SchoolRegistryContext(options);
            context.Database.EnsureDeleted(); // Clean up before each test
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void AddClassGroup_ShouldAddClassGroupToDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var classGroupBusiness = new ClassGroupBusiness(context);
            var classGroup = new ClassGroup
            {
                Name = "Class A",
                Year = 2025,
                TeacherId = 1
            };

            // Act
            classGroupBusiness.Add(classGroup);

            // Assert
            var result = classGroupBusiness.Get(classGroup.Id);
            Assert.NotNull(result);
            Assert.Equal("Class A", result.Name);
            Assert.Equal(2025, result.Year);
            classGroupBusiness.Delete(classGroup.Id); // Clean up after test
        }

        [Fact]
        public void GetAllClassGroups_ShouldReturnAllClassGroups()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var classGroupBusiness = new ClassGroupBusiness(context);
            // context.Classes.AddRange(
            //    new ClassGroup { Name = "Class A", Year = 2025, TeacherId = 1 },
            //    new ClassGroup { Name = "Class B", Year = 2026, TeacherId = 2 }
            //);
            var classGroup1 = new ClassGroup { Name = "Class A", Year = 2025, TeacherId = 1 };
            var classGroup2 = new ClassGroup { Name = "Class B", Year = 2026, TeacherId = 2 };
            classGroupBusiness.Add(classGroup1);
            classGroupBusiness.Add(classGroup2);
            context.SaveChanges();

            // Act
            var classGroups = classGroupBusiness.GetAll();

            // Assert
           // Assert.Equal(2, classGroups.Count);
            Assert.Contains(classGroups, c => c.Name == "Class A" && c.Year == 2025);
            Assert.Contains(classGroups, c => c.Name == "Class B" && c.Year == 2026);
            classGroupBusiness.Delete(classGroup1.Id); // Clean up after test
            classGroupBusiness.Delete(classGroup2.Id); // Clean up after test
        }

        [Fact]
        public void GetClassGroupById_ShouldReturnCorrectClassGroup()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var classGroupBusiness = new ClassGroupBusiness(context);
            var classGroup = new ClassGroup { Name = "Class C", Year = 2027, TeacherId = 2 };
            classGroupBusiness.Add(classGroup);
            context.SaveChanges();

            // Act
            var result = classGroupBusiness.Get(classGroup.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Class C", result.Name);
            Assert.Equal(2027, result.Year);
            classGroupBusiness.Delete(classGroup.Id); // Clean up after test
        }

        [Fact]
        public void UpdateClassGroup_ShouldModifyClassGroupInDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var classGroupBusiness = new ClassGroupBusiness(context);
            var classGroup = new ClassGroup { Name = "Class D", Year = 2028, TeacherId = 4 };
            context.Classes.Add(classGroup);
            context.SaveChanges();

            // Act
            classGroup.Name = "Class D Updated";
            classGroupBusiness.Update(classGroup);

            // Assert
            var result = context.Classes.FirstOrDefault(c => c.Id == classGroup.Id);
            Assert.NotNull(result);
            Assert.Equal("Class D Updated", result.Name);
        }

        [Fact]
        public void DeleteClassGroup_ShouldRemoveClassGroupFromDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var classGroupBusiness = new ClassGroupBusiness(context);
            var classGroup = new ClassGroup { Name = "Class E", Year = 2029, TeacherId = 2 };
            classGroupBusiness.Add(classGroup);
            context.SaveChanges();

            // Act
            classGroupBusiness.Delete(classGroup.Id);

            // Assert
            var result = context.Classes.FirstOrDefault(c => c.Id == classGroup.Id);
            Assert.Null(result);

        }
    }
}