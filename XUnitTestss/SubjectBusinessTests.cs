using Business;
using Data.Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestss
{
    public class SubjectBusinessTests
    {
        public SchoolRegistryContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<SchoolRegistryContext>()
                .UseInMemoryDatabase(databaseName: "SchoolRegistryTestDB")
                .Options;

            return new SchoolRegistryContext(options);
        }

        [Fact]
        public void AddSubject_ShouldAddSubjectToDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var subjectBusiness = new SubjectBusiness(context);
            var subject = new Subject { Name = "Mathematics" };

            // Act
            subjectBusiness.Add(subject);

            // Assert
            var result = context.Subjects.FirstOrDefault(s => s.Name == "Mathematics");
            Assert.NotNull(result);
            Assert.Equal("Mathematics", result.Name);
        }

        [Fact]
        public void GetAllSubjects_ShouldReturnAllSubjects()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var subjectBusiness = new SubjectBusiness(context);
            context.Subjects.AddRange(
                new Subject { Name = "Mathematics" },
                new Subject { Name = "Physics" }
            );
            context.SaveChanges();

            // Act
            var subjects = subjectBusiness.GetAll();

            // Assert
            Assert.Equal(2, subjects.Count);
            Assert.Contains(subjects, s => s.Name == "Mathematics");
            Assert.Contains(subjects, s => s.Name == "Physics");
        }

        [Fact]
        public void GetSubjectById_ShouldReturnCorrectSubject()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var subjectBusiness = new SubjectBusiness(context);
            var subject = new Subject { Name = "Chemistry" };
            context.Subjects.Add(subject);
            context.SaveChanges();

            // Act
            var result = subjectBusiness.Get(subject.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Chemistry", result.Name);
        }

        [Fact]
        public void DeleteSubject_ShouldRemoveSubjectFromDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var subjectBusiness = new SubjectBusiness(context);
            var subject = new Subject { Name = "Biology" };
            context.Subjects.Add(subject);
            context.SaveChanges();

            // Act
            subjectBusiness.Delete(subject.Id);

            // Assert
            var result = context.Subjects.FirstOrDefault(s => s.Id == subject.Id);
            Assert.Null(result);
        }
    }
}
