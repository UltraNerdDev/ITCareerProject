using Business;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace XUnitTestss
{
    public class ParentBusinessTests
    {
        private SchoolRegistryContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SchoolRegistryContext>()
                .UseInMemoryDatabase(databaseName: "ParentBusinessTestDB")
                .Options;

            return new SchoolRegistryContext(options);
        }

        [Fact]
        public void AddParent_ShouldAddParentToDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var parentBusiness = new ParentBusiness();
            var parent = new Parent
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123456789"
            };

            // Act
            parentBusiness.Add(parent);

            // Assert
           // var result = context.Parents.FirstOrDefault(p => p.Email == "john.doe@example.com");
            var result = parentBusiness.Get(parent.Id);
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            parentBusiness.Delete(parent.Id);
        }

        [Fact]
        public void GetAllParents_ShouldReturnAllParents()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var parentBusiness = new ParentBusiness();
            // context.Parents.AddRange(
            //    new Parent { FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com" },
            //    new Parent { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com" }
            //);
            var parent1 = new Parent
            {
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice@gmail.com",
                PhoneNumber = "123456789"
            };
            var parent2 = new Parent
            {
                FirstName = "Bob",
                LastName = "Brown",
                Email = "bob@abv.bg",
                PhoneNumber = "123456789"
            };
            parentBusiness.Add(parent1);
            parentBusiness.Add(parent2);
            context.SaveChanges();

            // Act
            var parents = parentBusiness.GetAll();

            // Assert
            //Assert.Equal(2, parents.Count);
            Assert.Contains(parents, p => p.FirstName == "Alice" && p.LastName == "Smith");
            Assert.Contains(parents, p => p.FirstName == "Bob" && p.LastName == "Brown");
            parentBusiness.Delete(parent1.Id);
            parentBusiness.Delete(parent2.Id);
        }

        [Fact]
        public void GetParentById_ShouldReturnCorrectParent()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var parentBusiness = new ParentBusiness();
            //var parent = new Parent { FirstName = "Charlie", LastName = "Johnson", Email = "charlie.johnson@example.com" };
            var parent = new Parent
            {
                FirstName = "Charlie",
                LastName = "Johnson",
                Email = "alice@gmail.com",
                PhoneNumber = "123456789"
            };
            parentBusiness.Add(parent);
            context.SaveChanges();

            // Act
            var result = parentBusiness.Get(parent.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Charlie", result.FirstName);
            Assert.Equal("Johnson", result.LastName);
            parentBusiness.Delete(parent.Id);
        }

        [Fact]
        public void UpdateParent_ShouldModifyParentInDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var parentBusiness = new ParentBusiness();
            var parent = new Parent { FirstName = "David", LastName = "Williams", Email = "david.williams@example.com" };
            context.Parents.Add(parent);
            context.SaveChanges();

            // Act
            parent.FirstName = "Dave";
            parentBusiness.Update(parent);

            // Assert
            var result = context.Parents.FirstOrDefault(p => p.Id == parent.Id);
            Assert.NotNull(result);
            Assert.Equal("Dave", result.FirstName);
            parentBusiness.Delete(parent.Id);
        }

        [Fact]
        public void DeleteParent_ShouldRemoveParentFromDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var parentBusiness = new ParentBusiness();
            //var parent = new Parent { FirstName = "Eve", LastName = "Taylor", Email = "eve.taylor@example.com" };
            var parent = new Parent
            {
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice@gmail.com",
                PhoneNumber = "123456789"
            };
             parentBusiness.Add(parent);
            context.SaveChanges();

            // Act
            parentBusiness.Delete(parent.Id);

            // Assert
            var result = context.Parents.FirstOrDefault(p => p.Id == parent.Id);
            Assert.Null(result);

        }
    }
}