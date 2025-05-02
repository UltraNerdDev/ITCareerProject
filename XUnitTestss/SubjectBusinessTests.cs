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
            var subjectBusiness = new SubjectBusiness();
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
            var subjectBusiness = new SubjectBusiness();
            //context.Subjects.AddRange(
            //    new Subject { Name = "Mathematics" },
            //    new Subject { Name = "Physics" }
            //);
            var subject1 = new Subject { Name = "Mathematics" };
            var subject2 = new Subject { Name = "Physics" };
            subjectBusiness.Add(subject1);
            subjectBusiness.Add(subject2);
            context.SaveChanges();

            // Act
            var subjects = subjectBusiness.GetAll();

            // Assert
           // Assert.Equal(2, subjects.Count);
            Assert.Contains(subjects, s => s.Name == "Mathematics");
            Assert.Contains(subjects, s => s.Name == "Physics");
            subjectBusiness.Delete(subject1.Id); // Clean up after test
            subjectBusiness.Delete(subject2.Id); // Clean up after test
        }

        [Fact]
        public void GetSubjectById_ShouldReturnCorrectSubject()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var subjectBusiness = new SubjectBusiness();
            var subject = new Subject { Name = "Chemistry" };
            subjectBusiness.Add(subject);
            context.SaveChanges();

            // Act
            var result = subjectBusiness.Get(subject.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Chemistry", result.Name);
            subjectBusiness.Delete(subject.Id); // Clean up after test
        }
        [Fact]
        public void UpdateSubject_ShouldModifySubjectInDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var subjectBusiness = new SubjectBusiness();
            var subject = new Subject {Name = "Mat" };
            subjectBusiness.Add(subject);
            context.SaveChanges();

            // Act
            subject.Name = "Mathematics";
            subjectBusiness.Update(subject);

            // Assert
            var result = subjectBusiness.Get(subject.Id);
            Assert.NotNull(result);
            Assert.Equal("Mathematics", result.Name);
            subjectBusiness.Delete(subject.Id); // Clean up after test
        }

        [Fact]
        public void DeleteSubject_ShouldRemoveSubjectFromDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var subjectBusiness = new SubjectBusiness();
            var subject = new Subject { Name = "Biology" };
            subjectBusiness.Add(subject);
            context.SaveChanges();

            // Act
            subjectBusiness.Delete(subject.Id);

            // Assert
            var result = context.Subjects.FirstOrDefault(s => s.Id == subject.Id);
            Assert.Null(result);
        }
    }
}
